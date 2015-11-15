//Fantasy Life ZMDL Model Import/Export made for Ohana3DS by gdkchan
//Please give credits if you use in your project
using System;
using System.Collections.Generic;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.ModelFormats
{
    /// <summary>
    ///     Fantasy Life ZMDL class.
    ///     Use it to load model files from Fantasy Life.
    ///     The model need to have the "zmdl" Magic at the beggining.
    /// </summary>
    class ZMDL
    {
        private enum vshAttribute
        {
            position = 0,
            normal = 1,
            color = 2,
            textureCoordinate0 = 3,
            textureCoordinate1 = 4,
            textureCoordinate2 = 5,
            boneIndex = 6,
            boneWeight = 7
        }

        private struct attributeEntry
        {
            public int offset; //Offset within the vertex
            public ushort attributeLength; //ex: X/Y = 2; X/Y/Z = 3...
            public int stride;
            public RenderBase.OVector4 floats;
        }

        /// <summary>
        ///     Loads a Fantasy Life ZMDL model.
        /// </summary>
        /// <param name="fileName">File Name of the CGFX file</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(string fileName)
        {
            return load(new MemoryStream(File.ReadAllBytes(fileName)));
        }

        /// <summary>
        ///     Loads a Fantasy Life ZMDL model.
        ///     Note that ZMDL must start at offset 0x0 (don't try using it for ZMDLs inside containers).
        /// </summary>
        /// <param name="data">Stream of the ZMDL file.</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);

            RenderBase.OModelGroup models = new RenderBase.OModelGroup();
            RenderBase.OModel model = new RenderBase.OModel();
            model.name = "model";

            string zmdlMagic = IOUtils.readString(input, 0, 4);
            data.Seek(0x20, SeekOrigin.Begin);
            uint materialsOffset = input.ReadUInt32();
            uint skeletonOffset = input.ReadUInt32();
            uint modelOffset = input.ReadUInt32();
            ushort materialsCount = input.ReadUInt16();
            ushort bonesCount = input.ReadUInt16();
            ushort modelObjectsCount = input.ReadUInt16();
            ushort unknowCount = input.ReadUInt16();

            //Materials
            List<byte> materialObjectBinding = new List<byte>();
            for (int materialIndex = 0; materialIndex < materialsCount; materialIndex++)
            {
                RenderBase.OMaterial material = MeshUtils.getGenericMaterial();

                material.name = IOUtils.readString(input, (uint)(materialsOffset + materialIndex * 0xb4));
                data.Seek(materialsOffset + (materialIndex * 0xb4) + 0x94, SeekOrigin.Begin);
                uint textureReferenceOffset = input.ReadUInt32();
                uint objectReferenceIndexOffset = input.ReadUInt32();

                data.Seek(objectReferenceIndexOffset, SeekOrigin.Begin);
                materialObjectBinding.Add(input.ReadByte());

                material.name0 = IOUtils.readString(input, textureReferenceOffset);
                data.Seek(textureReferenceOffset + 0x40, SeekOrigin.Begin);
                while ((data.Position & 3) != 0) input.ReadByte(); //Align Word
                data.Seek(0x30, SeekOrigin.Current); //Unknow matrix (possibly UV transform)
                input.ReadUInt32();
                input.ReadByte();
                input.ReadByte();
                byte wrap = input.ReadByte();
                input.ReadByte();

                model.addMaterial(material);
            }

            //Skeleton
            for (int boneIndex = 0; boneIndex < bonesCount; boneIndex++)
            {
                RenderBase.OBone bone = new RenderBase.OBone();
                bone.name = IOUtils.readString(input, (uint)(skeletonOffset + boneIndex * 0xcc));

                data.Seek(skeletonOffset + (boneIndex * 0xcc) + 0x40, SeekOrigin.Begin);
                data.Seek(0x64, SeekOrigin.Current); //Unknow matrices, probably transform and other stuff

                bone.translation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                bone.rotation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                bone.scale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());               
                bone.absoluteScale = new RenderBase.OVector3(bone.scale);

                bone.parentId = input.ReadInt16();
                input.ReadUInt16();

                model.addBone(bone);
            }

            //Meshes
            for (int objIndex = 0; objIndex < modelObjectsCount; objIndex++)
            {
                data.Seek(modelOffset + objIndex * 0xc4, SeekOrigin.Begin);

                attributeEntry[] attributes = new attributeEntry[9];
                for (int attribute = 0; attribute < 9; attribute++)
                {
                    attributes[attribute].floats = new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    attributes[attribute].offset = input.ReadByte() * 4;
                    attributes[attribute].attributeLength = input.ReadByte();
                    attributes[attribute].stride = input.ReadUInt16() * 4;
                }

                int vertexStride = attributes[8].stride;
                uint facesHeaderOffset = input.ReadUInt32();
                uint facesHeaderEntries = input.ReadUInt32();
                uint vertexBufferOffset = input.ReadUInt32();
                uint vertexBufferLength = input.ReadUInt32() * 4;

                RenderBase.OModelObject obj = new RenderBase.OModelObject();
                obj.name = String.Format("mesh_{0}", objIndex);

                List<RenderBase.CustomVertex> vertexBuffer = new List<RenderBase.CustomVertex>();
                for (int faceIndex = 0; faceIndex < facesHeaderEntries; faceIndex++)
                {
                    data.Seek(facesHeaderOffset + faceIndex * 0x14, SeekOrigin.Begin);

                    uint boneNodesOffset = input.ReadUInt32();
                    uint boneNodesEntries = input.ReadUInt32();
                    uint indexBufferOffset = input.ReadUInt32();
                    uint indexBufferPrimitiveCount = input.ReadUInt32();
                    input.ReadUInt32();

                    data.Seek(boneNodesOffset, SeekOrigin.Begin);
                    List<byte> nodeList = new List<byte>();
                    for (int n = 0; n < boneNodesEntries; n++) nodeList.Add(input.ReadByte());

                    data.Seek(indexBufferOffset, SeekOrigin.Begin);
                    for (int face = 0; face < indexBufferPrimitiveCount; face++)
                    {
                        ushort index = input.ReadUInt16();

                        RenderBase.OVertex vertex = new RenderBase.OVertex();
                        vertex.diffuseColor = 0xffffffff;

                        long position = data.Position;
                        long vertexOffset = vertexBufferOffset + index * vertexStride;
                        data.Seek(vertexOffset + attributes[(int)vshAttribute.position].offset, SeekOrigin.Begin);
                        vertex.position = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());

                        if (attributes[(int)vshAttribute.normal].attributeLength > 0)
                        {
                            data.Seek(vertexOffset + attributes[(int)vshAttribute.normal].offset, SeekOrigin.Begin);
                            vertex.normal = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        }

                        if (attributes[(int)vshAttribute.color].attributeLength > 0)
                        {
                            data.Seek(vertexOffset + attributes[(int)vshAttribute.color].offset, SeekOrigin.Begin);
                            uint r = (byte)(input.ReadSingle() * 0xff);
                            uint g = (byte)(input.ReadSingle() * 0xff);
                            uint b = (byte)(input.ReadSingle() * 0xff);
                            uint a = (byte)(input.ReadSingle() * 0xff);
                            vertex.diffuseColor = b | (g << 8) | (r << 16) | (a << 24);
                        }

                        if (attributes[(int)vshAttribute.textureCoordinate0].attributeLength > 0)
                        {
                            data.Seek(vertexOffset + attributes[(int)vshAttribute.textureCoordinate0].offset, SeekOrigin.Begin);
                            vertex.texture0 = new RenderBase.OVector2(input.ReadSingle(), input.ReadSingle());
                        }

                        for (int boneIndex = 0; boneIndex < attributes[(int)vshAttribute.boneIndex].attributeLength; boneIndex++)
                        {
                            data.Seek(vertexOffset + attributes[(int)vshAttribute.boneIndex].offset + (boneIndex * 4), SeekOrigin.Begin);
                            int b = (int)input.ReadSingle();
                            if (b < nodeList.Count) vertex.addNode(nodeList[b]); //Check, just to be sure
                        }

                        for (int boneWeight = 0; boneWeight < attributes[(int)vshAttribute.boneWeight].attributeLength; boneWeight++)
                        {
                            data.Seek(vertexOffset + attributes[(int)vshAttribute.boneWeight].offset + (boneWeight * 4), SeekOrigin.Begin);
                            vertex.addWeight(input.ReadSingle());
                        }

                        MeshUtils.calculateBounds(model, vertex);
                        obj.addVertex(vertex);
                        vertexBuffer.Add(RenderBase.convertVertex(vertex));

                        data.Seek(position, SeekOrigin.Begin);
                    }
                }

                int materialId = materialObjectBinding.IndexOf((byte)objIndex);
                if (materialId > -1) obj.materialId = (ushort)materialId;
                obj.hasNormal = true;
                obj.texUVCount = 1;
                obj.hasNode = true;
                obj.hasWeight = true;
                obj.renderBuffer = vertexBuffer.ToArray();
                model.addObject(obj);
            }

            data.Close();

            models.addModel(model);
            return models;
        }
    }
}
