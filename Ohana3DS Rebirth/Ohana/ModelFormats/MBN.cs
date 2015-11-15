using System;
using System.Collections.Generic;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.ModelFormats
{
    class MBN
    {
        private enum vtxAttributeType
        {
            position = 0,
            unk1 = 1,
            unk2 = 2,
            textureCoordinate0 = 3,
            normal = 4,
            boneIndex = 5,
            unk3 = 6
        }

        private class vtxAttribute
        {
            public vtxAttributeType type;
            public uint length;
            public uint offset;
            public float scale;
        }

        private class vtxEntry
        {
            public List<vtxAttribute> attributes = new List<vtxAttribute>();
            public uint length;
            public uint stride;
        }

        private class idxEntry
        {
            public List<uint> nodeList = new List<uint>();
            public uint primitiveCount;
            public uint nameId;
            public int meshIndex;
            public ushort[] buffer;
        }

        public static RenderBase.OModelGroup load(string fileName)
        {
            FileStream data = new FileStream(fileName, FileMode.Open);
            BinaryReader input = new BinaryReader(data);

            RenderBase.OModelGroup models;
            RenderBase.OModel model;

            string extension = Path.GetExtension(fileName).ToLower();
            string bchFile = fileName.Replace(extension, ".bch");
            bool isBCHLoaded = false;
            if (File.Exists(bchFile))
            {
                models = BCH.load(bchFile);
                model = models.model[0];
                models.model.Clear();
                isBCHLoaded = true;
            }
            else
            {
                models = new RenderBase.OModelGroup();
                model = new RenderBase.OModel();
                model.name = "mdl";
                model.addMaterial(MeshUtils.getGenericMaterial());
            }

            ushort format = input.ReadUInt16();
            bool isFaceWithinHeader = format == 4;
            input.ReadUInt16(); //-1?
            uint contentFlags = input.ReadUInt32();
            bool hasNameTable = (contentFlags & 2) > 0;
            uint mode = input.ReadUInt32();
            uint meshCount = input.ReadUInt32();

            List<vtxEntry> vtxDescriptors = new List<vtxEntry>();
            List<idxEntry> idxDescriptors = new List<idxEntry>();

            for (int i = 0; i < meshCount; i++)
            {
                if (mode == 1 && i == 0) vtxDescriptors.Add(getVtxDescriptor(input));

                uint facesCount = input.ReadUInt32();
                for (int j = 0; j < facesCount; j++)
                {
                    idxEntry face = new idxEntry();
                    face.meshIndex = i;
                    uint nodesCount = input.ReadUInt32();
                    for (int k = 0; k < nodesCount; k++) face.nodeList.Add(input.ReadUInt32());
                    face.primitiveCount = input.ReadUInt32();
                    if (hasNameTable) face.nameId = input.ReadUInt32();
                    if (isFaceWithinHeader)
                    {
                        face.buffer = new ushort[face.primitiveCount];
                        for (int k = 0; k < face.primitiveCount; k++) face.buffer[k] = input.ReadUInt16();
                    }
                    idxDescriptors.Add(face);
                }

                if (mode == 0) vtxDescriptors.Add(getVtxDescriptor(input));
            }

            List<string> objNameTable = new List<string>();
            
            if (hasNameTable)
            {
                for (int i = 0; i < meshCount; i++)
                {
                    byte index = input.ReadByte();
                    objNameTable.Add(IOUtils.readString(input, (uint)data.Position, true));
                }
            }

            if (!isFaceWithinHeader) align(input);
            byte[] vtxBuffer = null;
            vtxEntry currVertex = null;
            int faceIndex = 0;

            for (int i = 0; i < meshCount; i++)
            {
                if (mode == 0 || i == 0)
                {
                    currVertex = vtxDescriptors[i];
                    vtxBuffer = new byte[vtxDescriptors[i].length];
                    input.Read(vtxBuffer, 0, vtxBuffer.Length);
                    if (!isFaceWithinHeader) align(input);
                }

                List<RenderBase.CustomVertex> vertexBuffer = new List<RenderBase.CustomVertex>();
                RenderBase.OModelObject obj;
                if (isBCHLoaded)
                {
                    obj = model.modelObject[0];
                    model.modelObject.RemoveAt(0);
                }
                else
                {
                    obj = new RenderBase.OModelObject();
                    obj.name = "mesh_" + i.ToString();
                }

                for (int j = 0; j < currVertex.attributes.Count; j++)
                {
                    switch (currVertex.attributes[j].type)
                    {
                        case vtxAttributeType.textureCoordinate0: obj.texUVCount = 1; break;
                        case vtxAttributeType.normal: obj.hasNormal = true; break;
                        case vtxAttributeType.boneIndex: obj.hasNode = true; break;
                    }
                }
                
                for (;;)
                {
                    int indexBufferPos = 0;
                    for (int j = 0; j < idxDescriptors[faceIndex].primitiveCount; j++)
                    {
                        ushort index;
                        if (isFaceWithinHeader)
                            index = idxDescriptors[faceIndex].buffer[indexBufferPos++];
                        else
                            index = input.ReadUInt16();

                        RenderBase.OVertex vertex = new RenderBase.OVertex();
                        vertex.diffuseColor = 0xffffffff;
                        for (int k = 0; k < currVertex.attributes.Count; k++)
                        {
                            int pos = (int)(index * currVertex.stride + currVertex.attributes[k].offset);
                            float scale = currVertex.attributes[k].scale;
                            switch (currVertex.attributes[k].type)
                            {
                                case vtxAttributeType.position:
                                    vertex.position = new RenderBase.OVector3(
                                        BitConverter.ToInt16(vtxBuffer, pos) * scale,
                                        BitConverter.ToInt16(vtxBuffer, pos + 2) * scale,
                                        BitConverter.ToInt16(vtxBuffer, pos + 4) * scale);
                                    break;
                                case vtxAttributeType.textureCoordinate0:
                                    vertex.texture0 = new RenderBase.OVector2(
                                        BitConverter.ToInt16(vtxBuffer, pos) * scale,
                                        BitConverter.ToInt16(vtxBuffer, pos + 2) * scale);
                                    break;
                                case vtxAttributeType.normal:
                                    vertex.normal = new RenderBase.OVector3(
                                        BitConverter.ToInt16(vtxBuffer, pos) * scale,
                                        BitConverter.ToInt16(vtxBuffer, pos + 2) * scale,
                                        BitConverter.ToInt16(vtxBuffer, pos + 4) * scale);
                                    break;
                                case vtxAttributeType.boneIndex:
                                    vertex.addNode((int)idxDescriptors[faceIndex].nodeList[vtxBuffer[pos]]);
                                    vertex.addWeight(1f);
                                    break;
                            }
                        }

                        MeshUtils.calculateBounds(model, vertex);
                        obj.addVertex(vertex);
                        vertexBuffer.Add(RenderBase.convertVertex(vertex));
                    }

                    faceIndex++;
                    if (!isFaceWithinHeader) align(input);
                    if (faceIndex >= idxDescriptors.Count) break;
                    if (idxDescriptors[faceIndex].meshIndex == i) continue;
                    break;
                }

                obj.renderBuffer = vertexBuffer.ToArray();
                model.modelObject.Add(obj);
            }

            models.addModel(model);

            data.Close();
            return models;
        }

        /// <summary>
        ///     Reads a Vertex Descriptor from the mbn file.
        /// </summary>
        /// <param name="input">The Binary Reader of the mbn file</param>
        /// <returns></returns>
        private static vtxEntry getVtxDescriptor(BinaryReader input)
        {
            vtxEntry vtx = new vtxEntry();
            uint attributesCount = input.ReadUInt32();
            for (int j = 0; j < attributesCount; j++)
            {
                vtxAttribute att = new vtxAttribute();

                att.type = (vtxAttributeType)input.ReadUInt32();
                att.length = input.ReadUInt32();
                att.scale = input.ReadSingle();
                att.offset = vtx.stride;

                vtx.attributes.Add(att);
                switch (att.type)
                {
                    case vtxAttributeType.position: vtx.stride += 6; break;
                    case vtxAttributeType.unk1: vtx.stride += 4; break;
                    case vtxAttributeType.unk2: vtx.stride += 4; break;
                    case vtxAttributeType.textureCoordinate0: vtx.stride += 4; break;
                    case vtxAttributeType.normal: vtx.stride += 6; break;
                    case vtxAttributeType.boneIndex: vtx.stride += 2; break;
                    case vtxAttributeType.unk3: vtx.stride += 2; break;
                }
            }
            vtx.length = input.ReadUInt32();
            return vtx;
        }

        /// <summary>
        ///     Aligns the reader to skip the 0xffff padding that .mbn files uses on the stream section.
        /// </summary>
        /// <param name="input">The Binary Reader of the mbn file</param>
        private static void align(BinaryReader input)
        {
            while ((input.BaseStream.Position & 0x1f) != 0) input.ReadByte();
        }
    }
}
