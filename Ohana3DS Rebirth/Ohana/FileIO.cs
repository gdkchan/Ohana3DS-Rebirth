using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Ohana3DS_Rebirth.GUI.Forms;
using Ohana3DS_Rebirth.Ohana.Models;
using Ohana3DS_Rebirth.Ohana.Models.GenericFormats;
using Ohana3DS_Rebirth.Ohana.Models.PocketMonsters;
using Ohana3DS_Rebirth.Ohana.Textures.PocketMonsters;
using Ohana3DS_Rebirth.Ohana.Animations.PocketMonsters;
using Ohana3DS_Rebirth.Ohana.Textures;
using Ohana3DS_Rebirth.Ohana.Compressions;
using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth.Ohana
{
    public class FileIO
    {
        [Flags]
        public enum formatType : uint
        {
            unsupported = 0,
            compression = 1 << 0,
            container = 1 << 1,
            image = 1 << 2,
            model = 1 << 3,
            texture = 1 << 4,
            anims = 1 << 5,
            all = 0xffffffff
        }

        public struct file
        {
            public object data;
            public formatType type;
        }

        public static file load(string fileName)
        {
            switch (Path.GetExtension(fileName).ToLower())
            {
                case ".mbn": return new file { data = MBN.load(fileName), type = formatType.model };
                case ".xml": return new file { data = NLP.load(fileName), type = formatType.model };
                default: return load(new FileStream(fileName, FileMode.Open));
            }
        }

        public static file load(Stream data)
        {
            //Too small
            if (data.Length < 0x10)
            {
                data.Close();
                return new file { type = formatType.unsupported };
            }

            BinaryReader input = new BinaryReader(data);
            uint magic, length;

            switch (peek(input))
            {
                case 0x00010000: return new file { data = GfModel.load(data), type = formatType.model };
                case 0x15041213: return new file { data = GfTexture.load(data), type = formatType.image };
                case 0x15122117:
                    RenderBase.OModelGroup mdls = new RenderBase.OModelGroup();
                    mdls.model.Add(GfModel.loadModel(data));
                    return new file { data = mdls, type = formatType.model };
            }

            switch (getMagic(input, 5))
            {
                case "MODEL": return new file { data = DQVIIPack.load(data), type = formatType.container };
            }

            switch (getMagic(input, 4))
            {
                case "CGFX": return new file { data = CGFX.load(data), type = formatType.model };
                case "CRAG": return new file { data = GARC.load(data), type = formatType.container };
                case "darc": return new file { data = DARC.load(data), type = formatType.container };
                case "FPT0": return new file { data = FPT0.load(data), type = formatType.container };
                case "IECP":
                    magic = input.ReadUInt32();
                    length = input.ReadUInt32();
                    return load(new MemoryStream(LZSS.decompress(data, length)));
                case "NLK2":
                    data.Seek(0x80, SeekOrigin.Begin);
                    return new file
                    {
                        data = CGFX.load(data),
                        type = formatType.model
                    };
                case "SARC": return new file { data = SARC.load(data), type = formatType.container };
                case "SMES": return new file { data = NLP.loadMesh(data), type = formatType.model };
                case "Yaz0":
                    magic = input.ReadUInt32();
                    length = IOUtils.endianSwap(input.ReadUInt32());
                    data.Seek(8, SeekOrigin.Current);
                    return load(new MemoryStream(Yaz0.decompress(data, length)));
                case "zmdl": return new file { data = ZMDL.load(data), type = formatType.model };
                case "ztex": return new file { data = ZTEX.load(data), type = formatType.texture };
            }

            //Check if is a BCLIM or BFLIM file (header on the end)
            if (data.Length > 0x28)
            {
                data.Seek(-0x28, SeekOrigin.End);
                string clim = IOUtils.readStringWithLength(input, 4);
                if (clim == "CLIM" || clim == "FLIM") return new file { data = BCLIM.load(data), type = formatType.image };
            }

            switch (getMagic(input, 3))
            {
                case "BCH":
                    byte[] buffer = new byte[data.Length];
                    input.Read(buffer, 0, buffer.Length);
                    data.Close();
                    return new file
                    {
                        data = BCH.load(new MemoryStream(buffer)),
                        type = formatType.model
                    };
                case "DMP": return new file { data = DMP.load(data), type = formatType.image };
            }

            string magic2b = getMagic(input, 2);

            switch (magic2b)
            {
                case "AD": return new file { data = AD.load(data), type = formatType.model };
                case "BM": return new file { data = MM.load(data), type = formatType.model };
                case "BS": return new file { data = BS.load(data), type = formatType.anims };
                case "CM": return new file { data = CM.load(data), type = formatType.model };
                case "CP": return new file { data = CP.load(data), type = formatType.model };
                case "GR": return new file { data = GR.load(data), type = formatType.model };
                case "MM": return new file { data = MM.load(data), type = formatType.model };
                case "PC": return new file { data = PC.load(data), type = formatType.model };
                case "PT": return new file { data = PT.load(data), type = formatType.texture };
            }

            if ((magic2b[0] >= 'A' && magic2b[0] <= 'Z') &&
                (magic2b[1] >= 'A' && magic2b[1] <= 'Z'))
            {
                return new file { data = PkmnContainer.load(data), type = formatType.container };
            }

            //Compressions
            data.Seek(0, SeekOrigin.Begin);
            uint cmp = input.ReadUInt32();
            if ((cmp & 0xff) == 0x13) cmp = input.ReadUInt32();
            switch (cmp & 0xff)
            {
                case 0x11: return load(new MemoryStream(LZSS_Ninty.decompress(data, cmp >> 8)));
                case 0x90:
                    byte[] buffer = BLZ.decompress(data);
                    byte[] newData = new byte[buffer.Length - 1];
                    Buffer.BlockCopy(buffer, 1, newData, 0, newData.Length);
                    return load(new MemoryStream(newData));
            }

            data.Close();
            return new file { type = formatType.unsupported };
        }

        public static string getExtension(byte[] data, int startIndex = 0)
        {
            if (data.Length > 3 + startIndex)
            {
                switch (getMagic(data, 4, startIndex))
                {
                    case "CGFX": return ".bcres";
                }
            }

            if (data.Length > 2 + startIndex)
            {
                switch (getMagic(data, 3, startIndex))
                {
                    case "BCH": return ".bch";
                }
            }

            if (data.Length > 1 + startIndex)
            {
                switch (getMagic(data, 2, startIndex))
                {
                    case "AD": return ".ad";
                    case "BG": return ".bg";
                    case "BM": return ".bm";
                    case "BS": return ".bs";
                    case "CM": return ".cm";
                    case "GR": return ".gr";
                    case "MM": return ".mm";
                    case "PB": return ".pb";
                    case "PC": return ".pc";
                    case "PF": return ".pf";
                    case "PK": return ".pk";
                    case "PO": return ".po";
                    case "PT": return ".pt";
                    case "TM": return ".tm";
                }
            }

            return ".bin";
        }

        private static uint peek(BinaryReader input)
        {
            uint value = input.ReadUInt32();
            input.BaseStream.Seek(-4, SeekOrigin.Current);
            return value;
        }

        private static string getMagic(BinaryReader input, uint length)
        {
            string magic = IOUtils.readString(input, 0, length);
            input.BaseStream.Seek(0, SeekOrigin.Begin);
            return magic;
        }

        public static string getMagic(byte[] data, int length, int startIndex = 0)
        {
            return Encoding.ASCII.GetString(data, startIndex, length);
        }

        public enum fileType
        {
            none,
            model,
            texture,
            skeletalAnimation,
            materialAnimation,
            visibilityAnimation
        }

        /// <summary>
        ///     Imports a file of the given type.
        ///     Returns data relative to the chosen type.
        /// </summary>
        /// <param name="type">The type of the data</param>
        /// <returns></returns>
        public static object import(fileType type)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Multiselect = true;

                switch (type)
                {
                    case fileType.model:
                        openDlg.Title = "Import models";
                        openDlg.Filter = "All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            List<RenderBase.OModel> output = new List<RenderBase.OModel>();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                output.AddRange(((RenderBase.OModelGroup)load(fileName).data).model);
                            }
                            return output;
                        }
                        break;
                    case fileType.texture:
                        openDlg.Title = "Import textures";
                        openDlg.Filter = "All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            List<RenderBase.OTexture> output = new List<RenderBase.OTexture>();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                file file = load(fileName);
                                switch (file.type)
                                {
                                    case formatType.model: output.AddRange(((RenderBase.OModelGroup)file.data).texture); break;
                                    case formatType.texture: output.AddRange((List<RenderBase.OTexture>)file.data); break;
                                }
                            }
                            return output;
                        }
                        break;
                    case fileType.skeletalAnimation:
                        openDlg.Title = "Import skeletal animations";
                        openDlg.Filter = "All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            RenderBase.OAnimationListBase output = new RenderBase.OAnimationListBase();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                output.list.AddRange(((RenderBase.OModelGroup)load(fileName).data).skeletalAnimation.list);
                            }
                            return output;
                        }
                        break;
                    case fileType.materialAnimation:
                        openDlg.Title = "Import material animations";
                        openDlg.Filter = "All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            RenderBase.OAnimationListBase output = new RenderBase.OAnimationListBase();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                output.list.AddRange(((RenderBase.OModelGroup)load(fileName).data).materialAnimation.list);
                            }
                            return output;
                        }
                        break;
                    case fileType.visibilityAnimation:
                        openDlg.Title = "Import visibility animations";
                        openDlg.Filter = "All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            RenderBase.OAnimationListBase output = new RenderBase.OAnimationListBase();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                output.list.AddRange(((RenderBase.OModelGroup)load(fileName).data).visibilityAnimation.list);
                            }
                            return output;
                        }
                        break;
                }
            }

            return null;
        }

        /// <summary>
        ///     Exports a file of a given type.
        ///     Formats available to export will depend on the type of the data.
        /// </summary>
        /// <param name="type">Type of the data to be exported</param>
        /// <param name="data">The data</param>
        /// <param name="arguments">Optional arguments to be used by the exporter</param>
        public static void export(fileType type, object data, params int[] arguments)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                switch (type)
                {
                    case fileType.model:
                        OModelExportForm exportMdl = new OModelExportForm((RenderBase.OModelGroup)data, arguments[0]);
                        exportMdl.Show();
                        break;
                    case fileType.texture:
                        OTextureExportForm exportTex = new OTextureExportForm((RenderBase.OModelGroup)data, arguments[0]);
                        exportTex.Show();
                        break;
                    case fileType.skeletalAnimation:
                        saveDlg.Title = "Export Skeletal Animation";
                        saveDlg.Filter = "Source Model|*.smd";
                        if (saveDlg.ShowDialog() == DialogResult.OK)
                        {
                            switch (saveDlg.FilterIndex)
                            {
                                case 1:
                                    SMD.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0], arguments[1]);
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
}
