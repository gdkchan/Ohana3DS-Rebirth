using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Ohana3DS_Rebirth.Ohana.ModelFormats;
using Ohana3DS_Rebirth.Ohana.ModelFormats.GenericFormats;
using Ohana3DS_Rebirth.Ohana.TextureFormats;

namespace Ohana3DS_Rebirth.Ohana
{
    public class FileIO
    {
        public enum fileFormat
        {
            Unsupported,
            H3D,
            DMPTexture,
            PkmnContainer,
            BLZCompressed,
            LZSSCompressed,
            LZSSHeaderCompressed,
            IECPCompressed,
            DQVIIPack,
            FPT0,
            NLK2,
            CGFX,
            zmdl,
            ztex
        }

        public static fileFormat identify(string fileName)
        {
            Stream data = new FileStream(fileName, FileMode.Open);
            fileFormat format = identify(data);
            data.Close();
            data.Dispose();
            return format;
        }

        public static fileFormat identify(Stream data)
        {
            BinaryReader input = new BinaryReader(data);

            byte compression = input.ReadByte();
            string magic2b = IOUtils.readString(input, 0, 2);
            string magic3b = IOUtils.readString(input, 0, 3);
            string magic4b = IOUtils.readString(input, 0, 4);
            string magic5b = IOUtils.readString(input, 0, 5);
            data.Seek(0, SeekOrigin.Begin);

            switch (magic5b)
            {
                case "MODEL": return fileFormat.DQVIIPack;
            }

            switch (magic4b)
            {
                case "CGFX": return fileFormat.CGFX;
                case "zmdl": return fileFormat.zmdl;
                case "ztex": return fileFormat.ztex;
                case "IECP": return fileFormat.IECPCompressed;
                case "FPT0": return fileFormat.FPT0;
                case "NLK2": //Forbidden Magna models
                    data.Seek(0x80, SeekOrigin.Begin);
                    string magic = IOUtils.readString(input, 0, 4);
                    data.Seek(0, SeekOrigin.Begin);
                    return fileFormat.NLK2;

            }

            switch (magic3b)
            {
                case "BCH": return fileFormat.H3D;
                case "DMP": return fileFormat.DMPTexture;
            }

            switch (magic2b)
            {
                case "PC":
                case "GR":
                case "MM":
                    return fileFormat.PkmnContainer;
            }

            //Unfortunately compression only have one byte for identification.
            //So, it may have a lot of false positives.
            switch (compression)
            {
                case 0x11: return fileFormat.LZSSCompressed;
                case 0x13: return fileFormat.LZSSHeaderCompressed;
                case 0x90: return fileFormat.BLZCompressed;
            }

            return fileFormat.Unsupported;
        }

        /// <summary>
        ///     Returns true if the format is a compression format, and false otherwise.
        /// </summary>
        /// <param name="format">The format of the file</param>
        /// <returns></returns>
        public static bool isCompressed(fileFormat format)
        {
            switch (format)
            {
                case fileFormat.BLZCompressed:
                case fileFormat.LZSSCompressed:
                case fileFormat.LZSSHeaderCompressed:
                case fileFormat.IECPCompressed:
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Decompress a file and automaticaly updates the format of the compressed file.
        /// </summary>
        /// <param name="data">Stream with the file to be decompressed</param>
        /// <param name="format">Format of the compression</param>
        /// <returns></returns>
        public static void decompress(ref Stream data, ref fileFormat format)
        {
            BinaryReader input = new BinaryReader(data);
            byte[] decompressedData, content;
            uint length;

            switch (format)
            {
                case fileFormat.BLZCompressed:
                    decompressedData = Compressions.BLZ.decompress(data);
                    content = new byte[decompressedData.Length - 1];
                    Buffer.BlockCopy(decompressedData, 1, content, 0, content.Length);
                    data = new MemoryStream(content);
                    format = identify(data);
                    break;
                case fileFormat.LZSSCompressed:
                case fileFormat.LZSSHeaderCompressed:
                    if (format == fileFormat.LZSSHeaderCompressed) input.ReadUInt32();
                    length = input.ReadUInt32() >> 8;
                    decompressedData = Compressions.LZSS_Ninty.decompress(data, length);
                    data = new MemoryStream(decompressedData);
                    format = identify(data);
                    break;
                case fileFormat.IECPCompressed: //Stella glow
                    input.ReadUInt32(); //Magic
                    length = input.ReadUInt32();
                    decompressedData = Compressions.LZSS.decompress(data, length);
                    data = new MemoryStream(decompressedData);
                    format = identify(data);
                    break;
            }
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
        public static Object import(fileType type)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                switch (type)
                {
                    case fileType.model:
                        openDlg.Title = "Import Model";
                        openDlg.Filter = "Binary CTR H3D|*.bch|Binary CTR Model|*.bcres;*.bcmdl|Source Model|*.smd";
                        openDlg.Multiselect = true;

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            List<RenderBase.OModel> output = new List<RenderBase.OModel>();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                try
                                {
                                    switch (openDlg.FilterIndex)
                                    {
                                        case 1: output.AddRange(BCH.load(fileName).model); break;
                                        case 2: output.AddRange(CGFX.load(fileName).model); break;
                                        case 3: output.AddRange(SMD.import(fileName).model); break;
                                    }
                                }
                                catch
                                {
                                    Debug.WriteLine("FileIO: Unable to import model file " + fileName + "!");
                                }
                            }
                            return output;
                        }
                        break;
                    case fileType.texture:
                        openDlg.Title = "Import Texture";
                        openDlg.Filter = "Binary CTR H3D|*.bch|Binary CTR Texture|*.bcres;*.bctex;*.bcmdl|Fantasy Life Texture|*.tex";
                        openDlg.Multiselect = true;

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            List<RenderBase.OTexture> output = new List<RenderBase.OTexture>();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                switch (openDlg.FilterIndex)
                                {
                                    case 1: output.AddRange(BCH.load(fileName).texture); break;
                                    case 2: output.AddRange(CGFX.load(fileName).texture); break;
                                    case 3: output.AddRange(ZTEX.load(fileName)); break;
                                }
                            }
                            return output;
                        }
                        break;
                    case fileType.skeletalAnimation:
                        openDlg.Title = "Import Skeletal Animation";
                        openDlg.Filter = "Binary CTR H3D|*.bch|Binary CTR Skeletal Animation|*.bcskla|Source Model|*.smd";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            switch (openDlg.FilterIndex)
                            {
                                case 1: return BCH.load(openDlg.FileName).skeletalAnimation;
                                case 2: return CGFX.load(openDlg.FileName).skeletalAnimation;
                                case 3: return SMD.import(openDlg.FileName).skeletalAnimation;
                                default: return null;
                            }
                        }
                        break;
                    case fileType.materialAnimation:
                        openDlg.Title = "Import Material Animation";
                        openDlg.Filter = "Binary CTR H3D|*.bch";

                        if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).materialAnimation;
                        break;
                    case fileType.visibilityAnimation:
                        openDlg.Title = "Import Visibility Animation";
                        openDlg.Filter = "Binary CTR H3D|*.bch";

                        if (openDlg.ShowDialog() == DialogResult.OK) return BCH.load(openDlg.FileName).visibilityAnimation;
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
        public static void export(fileType type, Object data, List<int> arguments = null)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                switch (type)
                {
                    case fileType.model:
                        saveDlg.Title = "Export Model";
                        saveDlg.Filter = "Source Model|*.smd";
                        saveDlg.Filter += "|Collada Model|*.dae";
                        saveDlg.Filter += "|Wavefront OBJ|*.obj";
                        saveDlg.Filter += "|CTR Model|*.cmdl";
                        if (saveDlg.ShowDialog() == DialogResult.OK)
                        {
                            switch (saveDlg.FilterIndex)
                            {
                                case 1:
                                    SMD.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0]);
                                    break;
                                case 2:
                                    DAE.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0]);
                                    break;
                                case 3:
                                    OBJ.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0]);
                                    break;
                                case 4:
                                    CMDL.export((RenderBase.OModelGroup)data, saveDlg.FileName, arguments[0]);
                                    break;
                            }
                        }
                        break;
                    case fileType.texture:
                        RenderBase.OModelGroup model = (RenderBase.OModelGroup)data;
                        saveDlg.Title = "Export Texture";
                        saveDlg.FileName = "dummy";

                        if (arguments[0] > -1)
                        {
                            saveDlg.Filter = "Selected texture|*.png|All textures|*.png";

                            if (saveDlg.ShowDialog() == DialogResult.OK)
                            {
                                switch (saveDlg.FilterIndex)
                                {
                                    case 1: model.texture[arguments[0]].texture.Save(saveDlg.FileName); break;
                                    case 2:
                                        foreach (RenderBase.OTexture texture in model.texture)
                                        {
                                            texture.texture.Save(Path.Combine(Path.GetDirectoryName(saveDlg.FileName), texture.name + ".png"));
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            saveDlg.Filter = "All textures|*.png";

                            if (saveDlg.ShowDialog() == DialogResult.OK)
                            {
                                foreach (RenderBase.OTexture texture in model.texture)
                                {
                                    texture.texture.Save(Path.Combine(Path.GetDirectoryName(saveDlg.FileName), texture.name + ".png"));
                                }
                            }
                        }

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
