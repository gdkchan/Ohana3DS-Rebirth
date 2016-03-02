using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.Models;
using Ohana3DS_Rebirth.Ohana.Models.GenericFormats;

namespace Ohana3DS_Rebirth.Tools
{
    public partial class OSm4shModelCreator : OForm
    {
        FrmMain parentForm;

        public OSm4shModelCreator(FrmMain parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void OTextureExportForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter: create(); Close(); break;
                case Keys.Escape: Close(); break;
            }
        }

        private void BtnOpenInputModel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "Supported formats|*.obj;*.smd";
                if (openDlg.ShowDialog() == DialogResult.OK) TxtInModel.Text = openDlg.FileName;
            }
        }

        private void BtnSaveOutputModel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Filter = "Sm4sh model|*.mbn";
                if (saveDlg.ShowDialog() == DialogResult.OK) TxtOutModel.Text = saveDlg.FileName;
            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (create()) MessageBox.Show("Done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool create()
        {
            if (!File.Exists(TxtInModel.Text)) return false;

            RenderBase.OModelGroup models;
            switch (Path.GetExtension(TxtInModel.Text).ToLower())
            {
                case ".obj": models = OBJ.import(TxtInModel.Text); break;
                case ".smd": models = SMD.import(TxtInModel.Text); break;
                default:
                    MessageBox.Show("Unsupported model format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
            }

            RenderBase.OModel model = models.model[0];

            using (MemoryStream output = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(output);

                writer.Write((ushort)5);
                writer.Write((ushort)0xffff);
                writer.Write((uint)4);
                writer.Write((uint)0);
                writer.Write(model.mesh.Count);

                List<mesh> meshes = new List<mesh>();
                foreach (RenderBase.OMesh mesh in model.mesh) meshes.Add(createMesh(mesh));

                foreach (mesh m in meshes)
                {
                    //Face
                    writer.Write((uint)1); //Faces count
                    writer.Write(m.descriptor.nodes.Count);
                    foreach (uint n in m.descriptor.nodes) writer.Write(n);
                    writer.Write(m.indexBuffer.Length / 2);

                    //Vertex
                    writer.Write(m.descriptor.attributes.Count);
                    foreach (attributeDescriptor att in m.descriptor.attributes)
                    {
                        writer.Write(att.type);
                        writer.Write(att.format);
                        writer.Write(att.scale);
                    }
                    writer.Write(m.vertexBuffer.Length);
                }

                align(output);
                foreach (mesh m in meshes)
                {
                    writer.Write(m.vertexBuffer);
                    align(output);

                    writer.Write(m.indexBuffer);
                    align(output);
                }

                File.WriteAllBytes(TxtOutModel.Text, output.ToArray());
            }

            return true;
        }

        private void align(Stream data)
        {
            while ((data.Position & 0x1f) != 0) data.WriteByte(0xff);
        }

        private class attributeDescriptor
        {
            public uint type;
            public uint format;
            public float scale;

            public attributeDescriptor(uint _type, uint _format, float _scale)
            {
                type = _type;
                format = _format;
                scale = _scale;
            }
        }

        private struct meshDescriptor
        {
            public List<uint> nodes;
            public List<attributeDescriptor> attributes;
        }

        private struct mesh
        {
            public meshDescriptor descriptor;
            public byte[] vertexBuffer;
            public byte[] indexBuffer;
        }

        private mesh createMesh(RenderBase.OMesh input)
        {
            mesh output;

            output.descriptor.nodes = new List<uint>();
            output.descriptor.attributes = new List<attributeDescriptor>();

            output.descriptor.attributes.Add(new attributeDescriptor(0, 0, 1f)); //Position
            if (input.hasNormal) output.descriptor.attributes.Add(new attributeDescriptor(1, 0, 1f));
            if (input.texUVCount > 0) output.descriptor.attributes.Add(new attributeDescriptor(3, 0, 1f));
            if (input.hasNode) output.descriptor.attributes.Add(new attributeDescriptor(5, 1, 1f));
            if (input.hasWeight) output.descriptor.attributes.Add(new attributeDescriptor(6, 1, 0.00392156862f));

            MeshUtils.optimizedMesh optimized = MeshUtils.optimizeMesh(input);
            using (MemoryStream vertexStream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(vertexStream);

                foreach (RenderBase.OVertex vtx in optimized.vertices)
                {
                    writer.Write(vtx.position.x);
                    writer.Write(vtx.position.y);
                    writer.Write(vtx.position.z);

                    if (optimized.hasNormal)
                    {
                        writer.Write(vtx.normal.x);
                        writer.Write(vtx.normal.y);
                        writer.Write(vtx.normal.z);
                    }

                    if (optimized.texUVCount > 0)
                    {
                        writer.Write(vtx.texture0.x);
                        writer.Write(vtx.texture0.y);
                    }

                    if (optimized.hasNode)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (i < vtx.node.Count)
                            {
                                int nodeIndex = output.descriptor.nodes.IndexOf((uint)vtx.node[i]);
                                if (nodeIndex == -1)
                                {
                                    writer.Write((byte)output.descriptor.nodes.Count);
                                    output.descriptor.nodes.Add((uint)vtx.node[i]);
                                }
                                else
                                    writer.Write((byte)nodeIndex);
                            }
                            else
                                writer.Write((byte)0);
                        }
                    }

                    if (optimized.hasWeight)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (i < vtx.weight.Count)
                                writer.Write((byte)(vtx.weight[i] * byte.MaxValue));
                            else
                                writer.Write((byte)0);
                        }
                    }
                }

                output.vertexBuffer = vertexStream.ToArray();
            }

            using (MemoryStream indexStream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(indexStream);
                foreach (uint index in optimized.indices) writer.Write((ushort)index);
                output.indexBuffer = indexStream.ToArray();
            }

            return output;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
