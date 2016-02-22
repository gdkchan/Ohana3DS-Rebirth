using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Ohana3DS_Rebirth.GUI;
using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.Models.PICA200;

namespace Ohana3DS_Rebirth.Tools
{
    public partial class OBCHTextureReplacer : OForm
    {
        string currentFile;
        FrmMain parentForm;

        private struct loadedTexture
        {
            public bool modified;
            public uint gpuCommandsOffset;
            public uint gpuCommandsWordCount;
            public uint offset;
            public int length;
            public RenderBase.OTexture texture;
        }

        private class loadedBCH
        {
            public uint mainHeaderOffset;
            public uint gpuCommandsOffset;
            public uint dataOffset;
            public uint relocationTableOffset;
            public uint relocationTableLength;
            public List<loadedTexture> textures;

            public loadedBCH()
            {
                textures = new List<loadedTexture>();
            }
        }

        loadedBCH bch;

        public OBCHTextureReplacer(FrmMain parent)
        {
            InitializeComponent();
            parentForm = parent;
            TopMenu.Renderer = new OMenuStrip();
        }

        private void OBCHTextureReplacer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.O: open(); break;
                case Keys.S: if (bch != null) save(); break;
                case Keys.P: saveAndPreview(); break;
            }
        }

        private void MenuOpen_Click(object sender, EventArgs e)
        {
            open();
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            if (bch != null) save();
        }

        private void MenuSaveAndPreview_Click(object sender, EventArgs e)
        {
            saveAndPreview();
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void open()
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "All supported files|*.bch";

                if (openDlg.ShowDialog() == DialogResult.OK && File.Exists(openDlg.FileName))
                {
                    if (!open(openDlg.FileName))
                        MessageBox.Show(
                            "Unsupported file format!",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                }
            }
        }

        private void saveAndPreview()
        {
            if (bch != null)
            {
                save();
                parentForm.open(currentFile);
            }
        }

        private bool open(string fileName)
        {
            using (FileStream data = new FileStream(fileName, FileMode.Open))
            {
                BinaryReader input = new BinaryReader(data);

                string magic = IOUtils.readString(input, 0);
                if (magic != "BCH") return false;
                currentFile = fileName;
                data.Seek(4, SeekOrigin.Current);
                byte backwardCompatibility = input.ReadByte();
                byte forwardCompatibility = input.ReadByte();
                ushort version = input.ReadUInt16();

                uint mainHeaderOffset = input.ReadUInt32();
                uint stringTableOffset = input.ReadUInt32();
                uint gpuCommandsOffset = input.ReadUInt32();
                uint dataOffset = input.ReadUInt32();
                uint dataExtendedOffset = backwardCompatibility > 0x20 ? input.ReadUInt32() : 0;
                uint relocationTableOffset = input.ReadUInt32();

                uint mainHeaderLength = input.ReadUInt32();
                uint stringTableLength = input.ReadUInt32();
                uint gpuCommandsLength = input.ReadUInt32();
                uint dataLength = input.ReadUInt32();
                uint dataExtendedLength = backwardCompatibility > 0x20 ? input.ReadUInt32() : 0;
                uint relocationTableLength = input.ReadUInt32();

                data.Seek(mainHeaderOffset + 0x24, SeekOrigin.Begin);
                uint texturesPointerTableOffset = input.ReadUInt32() + mainHeaderOffset;
                uint texturesPointerTableEntries = input.ReadUInt32();

                bch = new loadedBCH();

                //Textures
                for (int index = 0; index < texturesPointerTableEntries; index++)
                {
                    data.Seek(texturesPointerTableOffset + (index * 4), SeekOrigin.Begin);
                    data.Seek(input.ReadUInt32() + mainHeaderOffset, SeekOrigin.Begin);

                    loadedTexture tex;
                    tex.modified = false;
                    tex.gpuCommandsOffset = input.ReadUInt32() + gpuCommandsOffset;
                    tex.gpuCommandsWordCount = input.ReadUInt32();
                    data.Seek(0x14, SeekOrigin.Current);
                    uint textureNameOffset = input.ReadUInt32();
                    string textureName = IOUtils.readString(input, textureNameOffset + stringTableOffset);

                    data.Seek(tex.gpuCommandsOffset, SeekOrigin.Begin);
                    PICACommandReader textureCommands = new PICACommandReader(data, tex.gpuCommandsWordCount);

                    tex.offset = textureCommands.getTexUnit0Address() + dataOffset;
                    RenderBase.OTextureFormat fmt = textureCommands.getTexUnit0Format();
                    Size textureSize = textureCommands.getTexUnit0Size();
                    switch (fmt)
                    {
                        case RenderBase.OTextureFormat.rgba8: tex.length = (textureSize.Width * textureSize.Height) * 4; break;
                        case RenderBase.OTextureFormat.rgb8: tex.length = (textureSize.Width * textureSize.Height) * 3; break;
                        case RenderBase.OTextureFormat.rgba5551: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.rgb565: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.rgba4: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.la8: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.hilo8: tex.length = (textureSize.Width * textureSize.Height) * 2; break;
                        case RenderBase.OTextureFormat.l8: tex.length = textureSize.Width * textureSize.Height; break;
                        case RenderBase.OTextureFormat.a8: tex.length = textureSize.Width * textureSize.Height; break;
                        case RenderBase.OTextureFormat.la4: tex.length = textureSize.Width * textureSize.Height; break;
                        case RenderBase.OTextureFormat.l4: tex.length = (textureSize.Width * textureSize.Height) >> 1; break;
                        case RenderBase.OTextureFormat.a4: tex.length = (textureSize.Width * textureSize.Height) >> 1; break;
                        case RenderBase.OTextureFormat.etc1: tex.length = (textureSize.Width * textureSize.Height) >> 1; break;
                        case RenderBase.OTextureFormat.etc1a4: tex.length = textureSize.Width * textureSize.Height; break;
                        default: throw new Exception("OBCHTextureReplacer: Invalid texture format on BCH!");
                    }

                    while ((tex.length & 0x7f) > 0) tex.length++;

                    data.Seek(tex.offset, SeekOrigin.Begin);
                    byte[] buffer = new byte[textureSize.Width * textureSize.Height * 4];
                    input.Read(buffer, 0, buffer.Length);
                    Bitmap texture = TextureCodec.decode(
                        buffer,
                        textureSize.Width,
                        textureSize.Height,
                        textureCommands.getTexUnit0Format());

                    tex.texture = new RenderBase.OTexture(texture, textureName);

                    bch.textures.Add(tex);
                }

                bch.mainHeaderOffset = mainHeaderOffset;
                bch.gpuCommandsOffset = gpuCommandsOffset;
                bch.dataOffset = dataOffset;
                bch.relocationTableOffset = relocationTableOffset;
                bch.relocationTableLength = relocationTableLength;
            }

            updateTexturesList();
            return true;
        }

        private void updateTexturesList()
        {
            TextureList.flush();
            PicPreview.Image = null;
            foreach (loadedTexture tex in bch.textures) TextureList.addItem(tex.texture.name);
            TextureList.Refresh();
        }

        private void TextureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;

            PicPreview.Image = bch.textures[TextureList.SelectedIndex].texture.texture;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;

            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Filter = "Image|*.png";
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    bch.textures[TextureList.SelectedIndex].texture.texture.Save(saveDlg.FileName);
                }
            }
        }

        private void BtnExportAll_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;

            using (FolderBrowserDialog browserDlg = new FolderBrowserDialog())
            {
                if (browserDlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (loadedTexture tex in bch.textures)
                    {
                        string outFile = Path.Combine(browserDlg.SelectedPath, tex.texture.name);
                        tex.texture.texture.Save(outFile + ".png");
                    }
                }
            }
        }

        private void BtnReplace_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;

            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "Image|*.png";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    loadedTexture tex = bch.textures[TextureList.SelectedIndex];
                    bch.textures.RemoveAt(TextureList.SelectedIndex);
                    Bitmap newTexture = new Bitmap(openDlg.FileName);
                    tex.texture.texture = newTexture;
                    tex.modified = true;
                    bch.textures.Insert(TextureList.SelectedIndex, tex);
                    PicPreview.Image = newTexture;
                }
            }
        }

        private void BtnReplaceAll_Click(object sender, EventArgs e)
        {
            if (bch == null) return;

            using (FolderBrowserDialog browserDlg = new FolderBrowserDialog())
            {
                if (browserDlg.ShowDialog() == DialogResult.OK)
                {
                    string[] files = Directory.GetFiles(browserDlg.SelectedPath);
                    for (int i = 0; i < bch.textures.Count; i++)
                    {
                        loadedTexture tex = bch.textures[i];

                        foreach (string file in files)
                        {
                            string name = Path.GetFileNameWithoutExtension(file);

                            if (string.Compare(name, tex.texture.name, StringComparison.InvariantCultureIgnoreCase) == 0)
                            {
                                loadedTexture newTex = tex;
                                bch.textures.RemoveAt(i);
                                Bitmap newTexture = new Bitmap(file);
                                tex.texture.texture = newTexture;
                                tex.modified = true;
                                bch.textures.Insert(i, tex);
                                if (TextureList.SelectedIndex == i) PicPreview.Image = newTexture;
                            }
                        }
                    }
                }
            }
        }

        private void save()
        {
            using (FileStream data = new FileStream(currentFile, FileMode.Open))
            {
                BinaryReader input = new BinaryReader(data);
                BinaryWriter output = new BinaryWriter(data);

                for (int i = 0; i < bch.textures.Count; i++)
                {
                    loadedTexture tex = bch.textures[i];

                    if (tex.modified)
                    {
                        byte[] buffer = align(TextureCodec.encode(tex.texture.texture, RenderBase.OTextureFormat.rgba8));
                        int diff = buffer.Length - tex.length;

                        replaceData(data, tex.offset, tex.length, buffer);

                        //Update offsets of next textures
                        tex.length = buffer.Length;
                        tex.modified = false;
                        updateTexture(i, tex);
                        for (int j = i; j < bch.textures.Count; j++)
                        {
                            loadedTexture next = bch.textures[j];
                            next.offset = (uint)(next.offset + diff);
                            updateTexture(j, next);
                        }

                        //Update all addresses poiting after the replaced data
                        bch.relocationTableOffset = (uint)(bch.relocationTableOffset + diff);
                        for (int index = 0; index < bch.relocationTableLength; index += 4)
                        {
                            data.Seek(bch.relocationTableOffset + index, SeekOrigin.Begin);
                            uint value = input.ReadUInt32();
                            uint offset = value & 0x1ffffff;
                            byte flags = (byte)(value >> 25);

                            if ((flags & 0x20) > 0 || flags == 7 || flags == 0xc)
                            {
                                if ((flags & 0x20) > 0)
                                    data.Seek((offset * 4) + bch.gpuCommandsOffset, SeekOrigin.Begin);
                                else
                                    data.Seek((offset * 4) + bch.mainHeaderOffset, SeekOrigin.Begin);

                                uint address = input.ReadUInt32();
                                if (address + bch.dataOffset > tex.offset)
                                {
                                    address = (uint)(address + diff);
                                    data.Seek(-4, SeekOrigin.Current);
                                    output.Write(address);
                                }
                            }
                        }

                        //Update texture format
                        data.Seek(tex.gpuCommandsOffset, SeekOrigin.Begin);
                        for (int index = 0; index < tex.gpuCommandsWordCount * 3; index++)
                        {
                            uint command = input.ReadUInt32();

                            switch (command)
                            {
                                case 0xf008e:
                                case 0xf0096:
                                case 0xf009e:
                                    data.Seek(-8, SeekOrigin.Current);
                                    output.Write((uint)0); //Set texture format to 0 = RGBA8888
                                    data.Seek(4, SeekOrigin.Current);
                                    break;
                                case 0xf0082:
                                case 0xf0092:
                                case 0xf009a:
                                    data.Seek(-8, SeekOrigin.Current);
                                    uint size = (uint)((tex.texture.texture.Width << 16) | tex.texture.texture.Height);
                                    output.Write(size); //Set new texture size
                                    data.Seek(4, SeekOrigin.Current);
                                    break;
                            }
                        }

                        //Patch up BCH header for new offsets and lengths
                        data.Seek(4, SeekOrigin.Begin);
                        byte backwardCompatibility = input.ReadByte();
                        byte forwardCompatibility = input.ReadByte();

                        //Update Data Extended and Relocation Table offsets
                        data.Seek(18, SeekOrigin.Current);
                        if (backwardCompatibility > 0x20) updateAddress(data, input, output, diff);
                        updateAddress(data, input, output, diff);

                        //Update data length
                        data.Seek(12, SeekOrigin.Current);
                        updateAddress(data, input, output, diff);
                    }
                }
            }

            MessageBox.Show("Done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private byte[] align(byte[] input)
        {
            int length = input.Length;
            while ((length & 0x7f) > 0) length++;
            byte[] output = new byte[length];
            Buffer.BlockCopy(input, 0, output, 0, input.Length);
            return output;
        }

        private void replaceData(Stream data, uint offset, int length, byte[] newData)
        {
            data.Seek(offset + length, SeekOrigin.Begin);
            byte[] after = new byte[data.Length - data.Position];
            data.Read(after, 0, after.Length);
            data.SetLength(offset);
            data.Seek(offset, SeekOrigin.Begin);
            data.Write(newData, 0, newData.Length);
            data.Write(after, 0, after.Length);
        }

        private void updateAddress(Stream data, BinaryReader input, BinaryWriter output, int diff)
        {
            uint offset = input.ReadUInt32();
            offset = (uint)(offset + diff);
            data.Seek(-4, SeekOrigin.Current);
            output.Write(offset);
        }

        private void updateTexture(int index, loadedTexture newTex)
        {
            bch.textures.RemoveAt(index);
            bch.textures.Insert(index, newTex);
        }
    }
}
