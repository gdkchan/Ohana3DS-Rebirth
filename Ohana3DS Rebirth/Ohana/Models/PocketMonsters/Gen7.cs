using Ohana3DS_Rebirth.Ohana.Models.PICA200;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.PocketMonsters
{
    class Gen7
    {
        /// <summary>
        ///     Loads a Pokémon Sun/Moon Model file.
        /// </summary>
        /// <param name="fileName">File Name of the Model file</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(string fileName)
        {
            return load(new FileStream(fileName, FileMode.Open));
        }

        /// <summary>
        ///     Loads a Pokémon Sun/Moon Model  file.
        ///     Note that Model must start at offset 0x0.
        /// </summary>
        /// <param name="data">Stream of the Model file.</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            RenderBase.OModelGroup mdls = new RenderBase.OModelGroup();

            BinaryReader input = new BinaryReader(data);

            input.ReadUInt32();

            uint[] sectionsCnt = new uint[5];

            for (int i = 0; i < 5; i++)
            {
                sectionsCnt[i] = input.ReadUInt32(); //Count for each section on the file (total 5)
            }

            uint baseAddr = (uint)data.Position;

            const int MODEL_SECT = 0;
            const int TEXTURE_SECT = 1;

            for (int sect = 0; sect < 5; sect++)
            {
                uint count = sectionsCnt[sect];

                for (int i = 0; i < count; i++)
                {
                    data.Seek(baseAddr + i * 4, SeekOrigin.Begin);
                    data.Seek(input.ReadUInt32(), SeekOrigin.Begin);

                    byte nameStrLen = input.ReadByte();
                    string name = IOUtils.readStringWithLength(input, nameStrLen);
                    uint descAddress = input.ReadUInt32();

                    data.Seek(descAddress, SeekOrigin.Begin);

                    switch (sect)
                    {
                        case MODEL_SECT:
                            RenderBase.OModel mdl = new RenderBase.OModel();

                            mdl.name = name;

                            input.BaseStream.Seek(0x10, SeekOrigin.Current);

                            ulong mdlMagic = input.ReadUInt64(); //gfmodel string
                            uint mdlLength = input.ReadUInt32();
                            input.ReadUInt32(); //-1

                            string[] effectNames = getStrTable(input);
                            string[] textureNames = getStrTable(input);
                            string[] materialNames = getStrTable(input);
                            string[] meshNames = getStrTable(input);

                            input.BaseStream.Seek(0x20, SeekOrigin.Current); //2 float4 (Maybe 2 Quaternions?)

                            mdl.transform = new RenderBase.OMatrix();
                            mdl.transform.M11 = input.ReadSingle();
                            mdl.transform.M21 = input.ReadSingle();
                            mdl.transform.M31 = input.ReadSingle();
                            mdl.transform.M41 = input.ReadSingle();

                            mdl.transform.M12 = input.ReadSingle();
                            mdl.transform.M22 = input.ReadSingle();
                            mdl.transform.M32 = input.ReadSingle();
                            mdl.transform.M42 = input.ReadSingle();

                            mdl.transform.M13 = input.ReadSingle();
                            mdl.transform.M23 = input.ReadSingle();
                            mdl.transform.M33 = input.ReadSingle();
                            mdl.transform.M43 = input.ReadSingle();

                            mdl.transform.M14 = input.ReadSingle();
                            mdl.transform.M24 = input.ReadSingle();
                            mdl.transform.M34 = input.ReadSingle();
                            mdl.transform.M44 = input.ReadSingle();

                            input.BaseStream.Seek(0x20, SeekOrigin.Current); //???

                            uint bonesCount = input.ReadUInt32(); //Not sure
                            input.BaseStream.Seek(0xc, SeekOrigin.Current);

                            //Probably wrong, maybe right
                            for (int b = 0; b < bonesCount; b++)
                            {
                                byte strName = input.ReadByte();
                                string bname = IOUtils.readStringWithLength(input, nameStrLen);
                                input.ReadByte();
                                input.ReadByte();
                                input.ReadByte();

                                RenderBase.OVector3 scale = new RenderBase.OVector3(
                                    input.ReadSingle(), 
                                    input.ReadSingle(), 
                                    input.ReadSingle());

                                input.BaseStream.Seek(0x18, SeekOrigin.Current);
                            }

                            //Materials
                            input.BaseStream.Seek(descAddress + mdlLength + 0x20, SeekOrigin.Begin);

                            for (int m = 0; m < materialNames.Length; m++)
                            {
                                RenderBase.OMaterial mat = new RenderBase.OMaterial();

                                mat.name = materialNames[m];

                                ulong matMagic = input.ReadUInt64(); //material string
                                uint matLength = input.ReadUInt32();
                                input.ReadUInt32(); //-1

                                long matStart = data.Position;

                                string[] unkNames = new string[4];

                                for (int n = 0; n < 4; n++)
                                {
                                    uint maybeHash = input.ReadUInt32();
                                    byte nameLen = input.ReadByte();

                                    unkNames[n] = IOUtils.readStringWithLength(input, nameLen);
                                }

                                data.Seek(0xb0, SeekOrigin.Current);

                                //Texture 0 name
                                mat.name0 = IOUtils.readStringWithLength(input, input.ReadByte());

                                mdl.material.Add(mat);

                                input.BaseStream.Seek(matStart + matLength, SeekOrigin.Begin);
                            }

                            //Meshes
                            for (int m = 0; m < meshNames.Length; m++)
                            {
                                ulong meshMagic = input.ReadUInt64(); //material string
                                uint meshLength = input.ReadUInt32();
                                input.ReadUInt32(); //-1

                                long meshStart = data.Position;
                                //Mesh name and other stuff goes here

                                input.BaseStream.Seek(0x80, SeekOrigin.Current);

                                subMeshInfo info = getSubMeshInfo(input);

                                for (int sm = 0; sm < info.count; sm++)
                                {
                                    RenderBase.OMesh obj = new RenderBase.OMesh();

                                    obj.isVisible = true;
                                    obj.name = meshNames[m];
                                    obj.materialId = (ushort)m;

                                    //NOTE: All Addresses on commands are set to 0x99999999 and are probably relocated by game engine
                                    PICACommandReader vtxCmdReader = info.cmdBuffers[sm * 3 + 0];
                                    PICACommandReader idxCmdReader = info.cmdBuffers[sm * 3 + 2];

                                    uint vshAttributesBufferStride = vtxCmdReader.getVSHAttributesBufferStride(0);
                                    uint vshTotalAttributes = vtxCmdReader.getVSHTotalAttributes(0);
                                    PICACommand.vshAttribute[] vshMainAttributesBufferPermutation = vtxCmdReader.getVSHAttributesBufferPermutation();
                                    uint[] vshAttributesBufferPermutation = vtxCmdReader.getVSHAttributesBufferPermutation(0);
                                    PICACommand.attributeFormat[] vshAttributesBufferFormat = vtxCmdReader.getVSHAttributesBufferFormat();

                                    for (int attribute = 0; attribute < vshTotalAttributes; attribute++)
                                    {
                                        switch (vshMainAttributesBufferPermutation[vshAttributesBufferPermutation[attribute]])
                                        {
                                            case PICACommand.vshAttribute.normal: obj.hasNormal = true; break;
                                            case PICACommand.vshAttribute.tangent: obj.hasTangent = true; break;
                                            case PICACommand.vshAttribute.color: obj.hasColor = true; break;
                                            case PICACommand.vshAttribute.textureCoordinate0: obj.texUVCount = Math.Max(obj.texUVCount, 1); break;
                                            case PICACommand.vshAttribute.textureCoordinate1: obj.texUVCount = Math.Max(obj.texUVCount, 2); break;
                                            case PICACommand.vshAttribute.textureCoordinate2: obj.texUVCount = Math.Max(obj.texUVCount, 3); break;
                                        }
                                    }

                                    PICACommand.indexBufferFormat idxBufferFormat = idxCmdReader.getIndexBufferFormat();
                                    uint idxBufferTotalVertices = idxCmdReader.getIndexBufferTotalVertices();

                                    obj.hasNode = true;
                                    obj.hasWeight = true;

                                    long vtxBufferStart = data.Position;

                                    input.BaseStream.Seek(info.vtxLengths[sm], SeekOrigin.Current);

                                    long idxBufferStart = data.Position;

                                    for (int faceIndex = 0; faceIndex < idxBufferTotalVertices; faceIndex++)
                                    {
                                        ushort index = 0;

                                        switch (idxBufferFormat)
                                        {
                                            case PICACommand.indexBufferFormat.unsignedShort: index = input.ReadUInt16(); break;
                                            case PICACommand.indexBufferFormat.unsignedByte: index = input.ReadByte(); break;
                                        }

                                        long dataPosition = data.Position;
                                        long vertexOffset = vtxBufferStart + (index * vshAttributesBufferStride);
                                        data.Seek(vertexOffset, SeekOrigin.Begin);

                                        RenderBase.OVertex vertex = new RenderBase.OVertex();
                                        vertex.diffuseColor = 0xffffffff;
                                        for (int attribute = 0; attribute < vshTotalAttributes; attribute++)
                                        {
                                            //gdkchan self note: The Attribute type flags are used for something else on Bone Weight (and bone index?)
                                            PICACommand.vshAttribute att = vshMainAttributesBufferPermutation[vshAttributesBufferPermutation[attribute]];
                                            PICACommand.attributeFormat format = vshAttributesBufferFormat[vshAttributesBufferPermutation[attribute]];
                                            if (att == PICACommand.vshAttribute.boneWeight) format.type = PICACommand.attributeFormatType.unsignedByte;
                                            RenderBase.OVector4 vector = getVector(input, format);

                                            switch (att)
                                            {
                                                case PICACommand.vshAttribute.position:
                                                    vertex.position = new RenderBase.OVector3(vector.x, vector.y, vector.z);
                                                    break;
                                                case PICACommand.vshAttribute.normal:
                                                    vertex.normal = new RenderBase.OVector3(vector.x, vector.y, vector.z);
                                                    break;
                                                case PICACommand.vshAttribute.tangent:
                                                    vertex.tangent = new RenderBase.OVector3(vector.x, vector.y, vector.z);
                                                    break;
                                                case PICACommand.vshAttribute.color:
                                                    uint r = MeshUtils.saturate(vector.x);
                                                    uint g = MeshUtils.saturate(vector.y);
                                                    uint b = MeshUtils.saturate(vector.z);
                                                    uint a = MeshUtils.saturate(vector.w);
                                                    vertex.diffuseColor = b | (g << 8) | (r << 16) | (a << 24);
                                                    break;
                                                case PICACommand.vshAttribute.textureCoordinate0:
                                                    vertex.texture0 = new RenderBase.OVector2(vector.x, vector.y);
                                                    break;
                                                case PICACommand.vshAttribute.textureCoordinate1:
                                                    vertex.texture1 = new RenderBase.OVector2(vector.x, vector.y);
                                                    break;
                                                case PICACommand.vshAttribute.textureCoordinate2:
                                                    vertex.texture2 = new RenderBase.OVector2(vector.x, vector.y);
                                                    break;
                                                    /*case PICACommand.vshAttribute.boneIndex:
                                                        vertex.node.Add(nodeList[(int)vector.x]);
                                                        if (format.attributeLength > 0) vertex.node.Add(nodeList[(int)vector.y]);
                                                        if (format.attributeLength > 1) vertex.node.Add(nodeList[(int)vector.z]);
                                                        if (format.attributeLength > 2) vertex.node.Add(nodeList[(int)vector.w]);
                                                        break;
                                                    case PICACommand.vshAttribute.boneWeight:
                                                        vertex.weight.Add(vector.x * 0.01f);
                                                        if (format.attributeLength > 0) vertex.weight.Add(vector.y * 0.01f);
                                                        if (format.attributeLength > 1) vertex.weight.Add(vector.z * 0.01f);
                                                        if (format.attributeLength > 2) vertex.weight.Add(vector.w * 0.01f);
                                                        break;*/
                                            }
                                        }

                                        MeshUtils.calculateBounds(mdl, vertex);
                                        obj.vertices.Add(vertex);

                                        data.Seek(dataPosition, SeekOrigin.Begin);
                                    }

                                    input.BaseStream.Seek(idxBufferStart + info.idxLengths[sm], SeekOrigin.Begin);

                                    mdl.mesh.Add(obj);
                                }

                                input.BaseStream.Seek(meshStart + meshLength, SeekOrigin.Begin);
                            }

                            mdls.model.Add(mdl);

                            break;

                        case TEXTURE_SECT:
                            data.Seek(descAddress + 0x18, SeekOrigin.Begin);
                            int texLength = input.ReadInt32();

                            data.Seek(descAddress + 0x28, SeekOrigin.Begin);
                            string texName = IOUtils.readStringWithLength(input, 0x40);

                            data.Seek(descAddress + 0x68, SeekOrigin.Begin);
                            ushort width = input.ReadUInt16();
                            ushort height = input.ReadUInt16();
                            ushort texBytesPerPx = input.ReadUInt16(); //Not sure
                            ushort texFormat = input.ReadUInt16();

                            data.Seek(0x10, SeekOrigin.Current);
                            byte[] texBuffer = input.ReadBytes(texLength);

                            Bitmap tex = TextureCodec.decode(texBuffer, width, height, (RenderBase.OTextureFormat)texFormat);

                            mdls.texture.Add(new RenderBase.OTexture(tex, texName));
                            break;
                    }
                }

                baseAddr += count * 4;
            }

            data.Close();

            return mdls;
        }

        private static RenderBase.OVector4 getVector(BinaryReader input, PICACommand.attributeFormat format)
        {
            RenderBase.OVector4 output = new RenderBase.OVector4();

            switch (format.type)
            {
                case PICACommand.attributeFormatType.signedByte:
                    output.x = (sbyte)input.ReadByte();
                    if (format.attributeLength > 0) output.y = (sbyte)input.ReadByte();
                    if (format.attributeLength > 1) output.z = (sbyte)input.ReadByte();
                    if (format.attributeLength > 2) output.w = (sbyte)input.ReadByte();
                    break;
                case PICACommand.attributeFormatType.unsignedByte:
                    output.x = input.ReadByte();
                    if (format.attributeLength > 0) output.y = input.ReadByte();
                    if (format.attributeLength > 1) output.z = input.ReadByte();
                    if (format.attributeLength > 2) output.w = input.ReadByte();
                    break;
                case PICACommand.attributeFormatType.signedShort:
                    output.x = input.ReadInt16();
                    if (format.attributeLength > 0) output.y = input.ReadInt16();
                    if (format.attributeLength > 1) output.z = input.ReadInt16();
                    if (format.attributeLength > 2) output.w = input.ReadInt16();
                    break;
                case PICACommand.attributeFormatType.single:
                    output.x = input.ReadSingle();
                    if (format.attributeLength > 0) output.y = input.ReadSingle();
                    if (format.attributeLength > 1) output.z = input.ReadSingle();
                    if (format.attributeLength > 2) output.w = input.ReadSingle();
                    break;
            }

            return output;
        }

        private static string[] getStrTable(BinaryReader input)
        {
            uint count = input.ReadUInt32();
            uint baseAddress = (uint)input.BaseStream.Position;

            string[] output = new string[count];

            for (int i = 0; i < count; i++)
            {
                input.BaseStream.Seek(baseAddress + i * 0x44, SeekOrigin.Begin);

                uint maybeHash = input.ReadUInt32();
                output[i] = IOUtils.readStringWithLength(input, 0x40);
            }

            input.BaseStream.Seek(baseAddress + count * 0x44, SeekOrigin.Begin);

            return output;
        }

        private struct subMeshInfo
        {
            public List<PICACommandReader> cmdBuffers;
            public List<ushort[]> nodeLists;
            public List<uint> vtxLengths;
            public List<uint> idxLengths;

            public int count;
        }

        private static subMeshInfo getSubMeshInfo(BinaryReader input)
        {
            subMeshInfo output = new subMeshInfo();

            output.cmdBuffers = new List<PICACommandReader>();
            output.nodeLists = new List<ushort[]>();
            output.vtxLengths = new List<uint>();
            output.idxLengths = new List<uint>();

            int currCmdIdx = 0;
            int totalCmds = 0;

            while ((currCmdIdx + 1 < totalCmds) || currCmdIdx == 0)
            {
                uint cmdLength = input.ReadUInt32();
                currCmdIdx = input.ReadInt32();
                totalCmds = input.ReadInt32();
                input.ReadInt32();

                output.cmdBuffers.Add(new PICACommandReader(input.BaseStream, cmdLength / 4));
            }

            output.count = totalCmds / 3;

            for (int i = 0; i < output.count; i++)
            {
                uint maybeHash = input.ReadUInt32();
                input.BaseStream.Seek(input.ReadUInt32(), SeekOrigin.Current); //SubMesh name, we don't need this

                ushort[] nodeList = new ushort[20];

                for (int n = 0; n < 20; n++) nodeList[n] = input.ReadByte();

                output.nodeLists.Add(nodeList);

                input.BaseStream.Seek(0xc, SeekOrigin.Current);

                uint unkCount0 = input.ReadUInt32();
                uint unkCount1 = input.ReadUInt32();
                output.vtxLengths.Add(input.ReadUInt32());
                output.idxLengths.Add(input.ReadUInt32());
            }

            return output;
        }
    }
}
