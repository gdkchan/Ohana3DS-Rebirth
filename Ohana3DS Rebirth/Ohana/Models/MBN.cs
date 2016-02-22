/*
 * Importer for the *.mbn format used on Super Smash Bros for 3DS.
 * Made by gdkchan for Ohana3DS.
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models
{
    class MBN
    {
        private enum vtxAttributeType
        {
            position = 0,
            normal = 1,
            color = 2,
            textureCoordinate0 = 3,
            textureCoordinate1 = 4,
            boneIndex = 5,
            boneWeight = 6,
            unk1 = 7
        }

        private enum vtxAttributeQuantization
        {
            single = 0,
            unsignedByte = 1,
            signedByte = 2,
            signedShort = 3
        }

        private class vtxAttribute
        {
            public vtxAttributeType type;
            public vtxAttributeQuantization format;
            public uint offset;
            public float scale;
        }

        private class vtxEntry
        {
            public List<vtxAttribute> attributes = new List<vtxAttribute>();
            public uint length;
            public uint stride;
            public byte[] buffer;
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
                model.name = "model";
                model.material.Add(new RenderBase.OMaterial());
            }

            ushort format = input.ReadUInt16();
            bool isDataWithinHeader = format == 4;
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
                    if (isDataWithinHeader)
                    {
                        face.buffer = new ushort[face.primitiveCount];
                        for (int k = 0; k < face.primitiveCount; k++) face.buffer[k] = input.ReadUInt16();
                        alignWord(input);
                    }

                    idxDescriptors.Add(face);
                }

                if (mode == 0)
                {
                    if (isDataWithinHeader)
                    {
                        vtxEntry desc = getVtxDescriptor(input);
                        desc.buffer = new byte[desc.length];
                        input.Read(desc.buffer, 0, desc.buffer.Length);
                        vtxDescriptors.Add(desc);
                        alignWord(input);
                    }
                    else
                        vtxDescriptors.Add(getVtxDescriptor(input));
                }
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

            if (!isDataWithinHeader) align(input);
            byte[] vtxBuffer = null;
            vtxEntry currVertex = null;
            int faceIndex = 0;

            for (int i = 0; i < meshCount; i++)
            {
                if (mode == 0 || i == 0)
                {
                    currVertex = vtxDescriptors[i];
                    if (!isDataWithinHeader)
                    {
                        vtxBuffer = new byte[vtxDescriptors[i].length];
                        input.Read(vtxBuffer, 0, vtxBuffer.Length);
                        align(input);
                    }
                    else
                        vtxBuffer = currVertex.buffer;
                }

                RenderBase.OMesh obj;
                if (isBCHLoaded)
                {
                    obj = model.mesh[0];
                    model.mesh.RemoveAt(0);
                }
                else
                {
                    obj = new RenderBase.OMesh();
                    obj.name = "mesh_" + i.ToString();
                }

                for (int j = 0; j < currVertex.attributes.Count; j++)
                {
                    switch (currVertex.attributes[j].type)
                    {
                        case vtxAttributeType.normal: obj.hasNormal = true; break;
                        case vtxAttributeType.color: obj.hasColor = true; break;
                        case vtxAttributeType.textureCoordinate0: obj.texUVCount = 1; break;
                        case vtxAttributeType.textureCoordinate1: obj.texUVCount = 2; break;
                        case vtxAttributeType.boneIndex: obj.hasNode = true; break;
                        case vtxAttributeType.boneWeight: obj.hasWeight = true; break;
                    }
                }

                for (;;)
                {
                    int indexBufferPos = 0;
                    for (int j = 0; j < idxDescriptors[faceIndex].primitiveCount; j++)
                    {
                        ushort index;
                        if (isDataWithinHeader)
                            index = idxDescriptors[faceIndex].buffer[indexBufferPos++];
                        else
                            index = input.ReadUInt16();

                        RenderBase.OVertex vertex = new RenderBase.OVertex();
                        vertex.diffuseColor = 0xffffffff;
                        for (int k = 0; k < currVertex.attributes.Count; k++)
                        {
                            vtxAttribute att = currVertex.attributes[k];
                            int pos = (int)(index * currVertex.stride + att.offset);
                            float scale = att.scale;
                            switch (currVertex.attributes[k].type)
                            {
                                case vtxAttributeType.position: vertex.position = getVector3(vtxBuffer, pos, att.format, scale); break;
                                case vtxAttributeType.normal: vertex.normal = getVector3(vtxBuffer, pos, att.format, scale); break;
                                case vtxAttributeType.color:
                                    RenderBase.OVector4 c = getVector4(vtxBuffer, pos, att.format, scale);
                                    uint r = MeshUtils.saturate(c.x * 0xff);
                                    uint g = MeshUtils.saturate(c.y * 0xff);
                                    uint b = MeshUtils.saturate(c.z * 0xff);
                                    uint a = MeshUtils.saturate(c.w * 0xff);
                                    vertex.diffuseColor = b | (g << 8) | (r << 16) | (a << 24);
                                    break;
                                case vtxAttributeType.textureCoordinate0: vertex.texture0 = getVector2(vtxBuffer, pos, att.format, scale); break;
                                case vtxAttributeType.textureCoordinate1: vertex.texture1 = getVector2(vtxBuffer, pos, att.format, scale); break;
                                case vtxAttributeType.boneIndex:
                                    byte n0 = vtxBuffer[pos];
                                    byte n1 = vtxBuffer[pos + 1];
                                    vertex.node.Add((int)idxDescriptors[faceIndex].nodeList[n0]);
                                    vertex.node.Add((int)idxDescriptors[faceIndex].nodeList[n1]);
                                    break;
                                case vtxAttributeType.boneWeight:
                                    RenderBase.OVector2 w = getVector2(vtxBuffer, pos, att.format, scale);
                                    vertex.weight.Add(w.x);
                                    vertex.weight.Add(w.y);
                                    break;
                            }
                        }

                        MeshUtils.calculateBounds(model, vertex);
                        obj.vertices.Add(vertex);
                    }

                    faceIndex++;
                    if (!isDataWithinHeader) align(input);
                    if (faceIndex >= idxDescriptors.Count) break;
                    if (idxDescriptors[faceIndex].meshIndex == i) continue;
                    break;
                }

                model.mesh.Add(obj);
            }

            models.model.Add(model);

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
                if (att.type != vtxAttributeType.color) while ((vtx.stride & 1) != 0) vtx.stride++;
                att.format = (vtxAttributeQuantization)input.ReadUInt32();
                att.scale = input.ReadSingle();
                att.offset = vtx.stride;

                vtx.attributes.Add(att);

                uint len = 0;
                switch (att.format)
                {
                    case vtxAttributeQuantization.single: len = 4; break;
                    case vtxAttributeQuantization.unsignedByte: len = 1; break;
                    case vtxAttributeQuantization.signedByte: len = 1; break;
                    case vtxAttributeQuantization.signedShort: len = 2; break;
                }
                switch (att.type)
                {
                    case vtxAttributeType.position: vtx.stride += 3 * len; break;
                    case vtxAttributeType.normal: vtx.stride += 3 * len; break;
                    case vtxAttributeType.color: vtx.stride += 4 * len; break;
                    case vtxAttributeType.textureCoordinate0: vtx.stride += 2 * len; break;
                    case vtxAttributeType.textureCoordinate1: vtx.stride += 2 * len; break;
                    case vtxAttributeType.boneIndex: vtx.stride += 2 * len; break;
                    case vtxAttributeType.boneWeight: vtx.stride += 2 * len; break;
                    case vtxAttributeType.unk1: vtx.stride += 2 * len; break;
                    default: throw new Exception("MBN: Unknown Vertex Attribute type, can't calculate Stride! STOP!");
                }
            }

            while ((vtx.stride & 1) != 0) vtx.stride++;
            vtx.length = input.ReadUInt32();
            return vtx;
        }

        /// <summary>
        ///     Reads a quantized Vector2 from the Vertex Stream.
        ///     It is usually used to store Texture Coordinates.
        /// </summary>
        /// <param name="buffer">The Vertex Stream buffer</param>
        /// <param name="pos">Position to read from the buffer</param>
        /// <param name="q">Quantization of the data</param>
        /// <param name="scale">Scale of the vector</param>
        /// <returns></returns>
        private static RenderBase.OVector2 getVector2(byte[] buffer, int pos, vtxAttributeQuantization q, float scale)
        {
            switch (q)
            {
                case vtxAttributeQuantization.single:
                    return new RenderBase.OVector2(
                        BitConverter.ToSingle(buffer, pos) * scale,
                        BitConverter.ToSingle(buffer, pos + 4) * scale);
                case vtxAttributeQuantization.unsignedByte:
                    return new RenderBase.OVector2(
                        buffer[pos] * scale,
                        buffer[pos + 1] * scale);
                case vtxAttributeQuantization.signedByte:
                    return new RenderBase.OVector2(
                        (sbyte)buffer[pos] * scale,
                        (sbyte)buffer[pos + 1] * scale);
                case vtxAttributeQuantization.signedShort:
                    return new RenderBase.OVector2(
                        BitConverter.ToInt16(buffer, pos) * scale,
                        BitConverter.ToInt16(buffer, pos + 2) * scale);
            }

            return null;
        }

        /// <summary>
        ///     Reads a quantized Vector3 from the Vertex Stream.
        ///     It is usually used to store Geometry Position and Normal.
        /// </summary>
        /// <param name="buffer">The Vertex Stream buffer</param>
        /// <param name="pos">Position to read from the buffer</param>
        /// <param name="q">Quantization of the data</param>
        /// <param name="scale">Scale of the vector</param>
        /// <returns></returns>
        private static RenderBase.OVector3 getVector3(byte[] buffer, int pos, vtxAttributeQuantization q, float scale)
        {
            switch (q)
            {
                case vtxAttributeQuantization.single:
                    return new RenderBase.OVector3(
                        BitConverter.ToSingle(buffer, pos) * scale,
                        BitConverter.ToSingle(buffer, pos + 4) * scale,
                        BitConverter.ToSingle(buffer, pos + 8) * scale);
                case vtxAttributeQuantization.unsignedByte:
                    return new RenderBase.OVector3(
                        buffer[pos] * scale,
                        buffer[pos + 1] * scale,
                        buffer[pos + 2] * scale);
                case vtxAttributeQuantization.signedByte:
                    return new RenderBase.OVector3(
                        (sbyte)buffer[pos] * scale,
                        (sbyte)buffer[pos + 1] * scale,
                        (sbyte)buffer[pos + 2] * scale);
                case vtxAttributeQuantization.signedShort:
                    return new RenderBase.OVector3(
                        BitConverter.ToInt16(buffer, pos) * scale,
                        BitConverter.ToInt16(buffer, pos + 2) * scale,
                        BitConverter.ToInt16(buffer, pos + 4) * scale);
            }

            return null;
        }

        /// <summary>
        ///     Reads a quantized Vector4 from the Vertex Stream.
        ///     It is usually used to store Vertex Color.
        /// </summary>
        /// <param name="buffer">The Vertex Stream buffer</param>
        /// <param name="pos">Position to read from the buffer</param>
        /// <param name="q">Quantization of the data</param>
        /// <param name="scale">Scale of the vector</param>
        /// <returns></returns>
        private static RenderBase.OVector4 getVector4(byte[] buffer, int pos, vtxAttributeQuantization q, float scale)
        {
            switch (q)
            {
                case vtxAttributeQuantization.single:
                    return new RenderBase.OVector4(
                        BitConverter.ToSingle(buffer, pos) * scale,
                        BitConverter.ToSingle(buffer, pos + 4) * scale,
                        BitConverter.ToSingle(buffer, pos + 8) * scale,
                        BitConverter.ToSingle(buffer, pos + 12) * scale);
                case vtxAttributeQuantization.unsignedByte:
                    return new RenderBase.OVector4(
                        buffer[pos] * scale,
                        buffer[pos + 1] * scale,
                        buffer[pos + 2] * scale,
                        buffer[pos + 3] * scale);
                case vtxAttributeQuantization.signedByte:
                    return new RenderBase.OVector4(
                        (sbyte)buffer[pos] * scale,
                        (sbyte)buffer[pos + 1] * scale,
                        (sbyte)buffer[pos + 2] * scale,
                        (sbyte)buffer[pos + 3] * scale);
                case vtxAttributeQuantization.signedShort:
                    return new RenderBase.OVector4(
                        BitConverter.ToInt16(buffer, pos) * scale,
                        BitConverter.ToInt16(buffer, pos + 2) * scale,
                        BitConverter.ToInt16(buffer, pos + 4) * scale,
                        BitConverter.ToInt16(buffer, pos + 6) * scale);
            }

            return null;
        }

        /// <summary>
        ///     Aligns the reader to skip the 0xffff padding that .mbn files uses on the stream section.
        ///     It will align to the nearest 32 bytes boundary.
        /// </summary>
        /// <param name="input">The Binary Reader of the mbn file</param>
        private static void align(BinaryReader input)
        {
            while ((input.BaseStream.Position & 0x1f) != 0) input.ReadByte();
        }

        /// <summary>
        ///     Aligns the reader to skip the 0xffff padding that .mbn files uses on the stream section.
        ///     It will align to the nearest 32-bits Word.
        /// </summary>
        /// <param name="input">The Binary Reader of the mbn file</param>
        private static void alignWord(BinaryReader input)
        {
            while ((input.BaseStream.Position & 3) != 0) input.ReadByte();
        }
    }
}
