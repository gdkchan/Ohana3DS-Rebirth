using System.Collections.Generic;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Models.NewLovePlus
{
    class Mesh
    {
        /// <summary>
        ///     Loads a SMES file.
        /// </summary>
        /// <param name="fileName">File Name of the SMES file</param>
        /// <returns></returns>
        public static void load(string fileName, RenderBase.OModel model)
        {
            load(new MemoryStream(File.ReadAllBytes(fileName)), model);
        }

        /// <summary>
        ///     Loads a SMES file.
        ///     Note that SMES must start at offset 0x0.
        /// </summary>
        /// <param name="data">Stream of the SMES file.</param>
        /// <returns></returns>
        public static void load(Stream data, RenderBase.OModel model, bool ignoreMaterial = false)
        {
            BinaryReader input = new BinaryReader(data);

            //The node indices points to the index directly relative to the tree index on the Skeleton
            //Therefore, we must build a table to translate from the Skeleton Index to the absolute Bone Tree Index
            List<int> nodeBinding = new List<int>();
            buildNodeBinding(model.skeleton, 0, ref nodeBinding);

            string smesMagic = IOUtils.readStringWithLength(input, 4);
            uint dataTableOffset = input.ReadUInt32();
            uint meshCount = input.ReadUInt32();
            for (int meshIndex = 0; meshIndex < meshCount; meshIndex++)
            {
                //Mesh descriptor
                data.Seek(0xc + meshIndex * 0x20, SeekOrigin.Begin);
                uint vector3TableEntries = input.ReadUInt32();
                uint vector3TableOffset = input.ReadUInt32();
                uint attributesCount = input.ReadUInt32();
                uint primitiveCount = input.ReadUInt32();
                uint vertexBufferOffset = input.ReadUInt32();
                uint indexSectionOffset = input.ReadUInt32();
                uint renderPriority = input.ReadUInt32();
                uint materialIndex = input.ReadUInt32();

                //Index Buffer section
                data.Seek(indexSectionOffset, SeekOrigin.Begin);

                RenderBase.OMesh obj = new RenderBase.OMesh();
                if (!ignoreMaterial) obj.materialId = (ushort)materialIndex;
                obj.isVisible = true;
                obj.hasNormal = true;
                obj.hasTangent = true;
                obj.texUVCount = 1;

                ushort[] nodes = null;
                uint skinningMode = 0;
                bool hasData = true;
                while (hasData)
                {
                    string magic = IOUtils.readStringWithLength(input, 4);
                    uint sectionLength = input.ReadUInt32();
                    long startOffset = data.Position;
                    
                    switch (magic)
                    {
                        case "SKIN": skinningMode = input.ReadUInt32(); break; //0 = rigid, 1 = smooth
                        case "BONI":
                            nodes = new ushort[input.ReadUInt16()];
                            for (int n = 0; n < nodes.Length; n++) nodes[n] = input.ReadUInt16();
                            obj.hasNode = true;
                            obj.hasWeight = true;
                            break;
                        case "IDX ":
                            input.ReadUInt16();
                            ushort indicesCount = input.ReadUInt16();

                            //Note: The Stride seems to always be 0x40
                            uint vertexStride = (indexSectionOffset - vertexBufferOffset) / primitiveCount;
                            for (int i = 0; i < indicesCount; i++)
                            {
                                ushort index = input.ReadUInt16();

                                long position = data.Position;
                                data.Seek(vertexBufferOffset + index * vertexStride, SeekOrigin.Begin);

                                RenderBase.OVertex vertex = new RenderBase.OVertex();
                                vertex.diffuseColor = 0xffffffff;
                                vertex.position = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                                vertex.normal = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                                vertex.tangent = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                                vertex.texture0 = new RenderBase.OVector2(input.ReadSingle(), input.ReadSingle());

                                if (model.skeleton.Count > 0)
                                {
                                    if (nodes != null && nodes.Length > 0)
                                    {
                                        byte b0 = input.ReadByte();
                                        byte b1 = input.ReadByte();
                                        byte b2 = input.ReadByte();
                                        byte b3 = input.ReadByte();

                                        if (b0 < nodes.Length) vertex.node.Add(nodeBinding[nodes[b0]]);
                                        if (skinningMode > 0)
                                        {
                                            if (b1 < nodes.Length) vertex.node.Add(nodeBinding[nodes[b1]]);
                                            if (b2 < nodes.Length) vertex.node.Add(nodeBinding[nodes[b2]]);
                                            if (b3 < nodes.Length) vertex.node.Add(nodeBinding[nodes[b3]]);
                                        }
                                    }

                                    vertex.weight.Add(input.ReadSingle());
                                    if (skinningMode > 0)
                                    {
                                        vertex.weight.Add(input.ReadSingle());
                                        vertex.weight.Add(input.ReadSingle());
                                        vertex.weight.Add(input.ReadSingle());
                                    }
                                }

                                MeshUtils.calculateBounds(model, vertex);
                                obj.vertices.Add(vertex);

                                data.Seek(position, SeekOrigin.Begin);
                            }
                            break;
                        default: hasData = false; break;
                    }

                    data.Seek(startOffset + sectionLength, SeekOrigin.Begin);
                }

                model.mesh.Add(obj);
            }

            data.Close();
        }

        /// <summary>
        ///     Creates a table to bind the nodes with the skeleton.
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the current bone (root bone when it's not a recursive call)</param>
        /// <param name="nodeBinding">List with the table to bind the nodes to the skeleton</param>
        private static void buildNodeBinding(List<RenderBase.OBone> skeleton, int index, ref List<int> nodeBinding)
        {
            nodeBinding.Add(index);

            for (int i = 0; i < skeleton.Count; i++)
            {
                if (skeleton[i].parentId == index) buildNodeBinding(skeleton, i, ref nodeBinding);
            }
        }
    }
}
