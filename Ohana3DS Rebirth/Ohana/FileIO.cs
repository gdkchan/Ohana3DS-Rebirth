using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Ohana3DS_Rebirth.GUI.Forms;
using Ohana3DS_Rebirth.Ohana.Models;
using Ohana3DS_Rebirth.Ohana.Models.GenericFormats;
using Ohana3DS_Rebirth.Ohana.Textures;

namespace Ohana3DS_Rebirth.Ohana
{
    public class FileIO
    {
        public enum fileFormat
        {
            cmpBLZ,
            cmpIECP,
            cmpLZSS,
            cmpLZSSHeader,
            dq7DMP,
            dq7FPT0,
            dq7Model,
            flZMdl,
            flZTex,
            fmNLK2,
            nlpSMes,
            nw4cCGfx,
            nw4cH3D,
            pkmnContainer,
            unsupported
        }

        public static fileFormat identify(string fileName)
        {
            Stream data = new FileStream(fileName, FileMode.Open);
            fileFormat format = identify(data);
            data.Close();
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
                case "MODEL": return fileFormat.dq7Model;
            }

            switch (magic4b)
            {
                case "IECP": return fileFormat.cmpIECP;
                case "FPT0": return fileFormat.dq7FPT0;
                case "zmdl": return fileFormat.flZMdl;
                case "ztex": return fileFormat.flZTex;
                case "NLK2": return fileFormat.fmNLK2;
                case "SMES": return fileFormat.nlpSMes;
                case "CGFX": return fileFormat.nw4cCGfx;
            }

            switch (magic3b)
            {
                case "DMP": return fileFormat.dq7DMP;
                case "BCH": return fileFormat.nw4cH3D;
            }

            switch (magic2b)
            {
                case "PC":
                case "PT":
                case "PK":
                case "PB":
                case "GR":
                case "MM":
                case "AD":
                    return fileFormat.pkmnContainer;
            }

            //Unfortunately compression only have one byte for identification.
            //So, it may have a lot of false positives.
            switch (compression)
            {
                case 0x90: return fileFormat.cmpBLZ;
                case 0x11: return fileFormat.cmpLZSS;
                case 0x13: return fileFormat.cmpLZSSHeader;
            }

            return fileFormat.unsupported;
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
                case fileFormat.cmpBLZ:
                case fileFormat.cmpIECP:
                case fileFormat.cmpLZSS:
                case fileFormat.cmpLZSSHeader:
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
                case fileFormat.cmpBLZ:
                    decompressedData = Compressions.BLZ.decompress(data);
                    content = new byte[decompressedData.Length - 1];
                    Buffer.BlockCopy(decompressedData, 1, content, 0, content.Length);
                    data = new MemoryStream(content);
                    format = identify(data);
                    break;
                case fileFormat.cmpIECP: //Stella Glow
                    uint magic = input.ReadUInt32();
                    length = input.ReadUInt32();
                    decompressedData = Compressions.LZSS.decompress(data, length);
                    data = new MemoryStream(decompressedData);
                    format = identify(data);
                    break;
                case fileFormat.cmpLZSS:
                case fileFormat.cmpLZSSHeader:
                    if (format == fileFormat.cmpLZSSHeader) input.ReadUInt32();
                    length = input.ReadUInt32() >> 8;
                    decompressedData = Compressions.LZSS_Ninty.decompress(data, length);
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
                        openDlg.Filter = "Binary CTR H3D|*.bch|Binary CTR Texture|*.bcres;*.bcmdl;*.bctex|Fantasy Life Texture|*.tex";
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
                        openDlg.Filter = "Binary CTR H3D|*.bch|Binary CTR Skeletal Animation|*.bcres;*.bcskla|Source Model|*.smd";
                        openDlg.Multiselect = true;

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            RenderBase.OAnimationListBase output = new RenderBase.OAnimationListBase();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                switch (openDlg.FilterIndex)
                                {
                                    case 1: output.list.AddRange(BCH.load(fileName).skeletalAnimation.list); break;
                                    case 2: output.list.AddRange(CGFX.load(fileName).skeletalAnimation.list); break;
                                    case 3: output.list.AddRange(SMD.import(fileName).skeletalAnimation.list); break;
                                }
                            }
                            return output;
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
