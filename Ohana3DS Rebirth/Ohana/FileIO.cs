using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Ohana3DS_Rebirth.GUI.Forms;
using Ohana3DS_Rebirth.Ohana.Models;
using Ohana3DS_Rebirth.Ohana.Models.GenericFormats;
using Ohana3DS_Rebirth.Ohana.Textures;
using Ohana3DS_Rebirth.Ohana.Compressions;
using Ohana3DS_Rebirth.Ohana.Containers;

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

        /// <summary>
        ///     Identifies the format of a file based on Signature.
        /// </summary>
        /// <param name="fileName">The Full Path to the file</param>
        /// <returns></returns>
        public static fileFormat identify(string fileName)
        {
            Stream data = new FileStream(fileName, FileMode.Open);
            fileFormat format = identify(data);
            data.Close();
            return format;
        }

        /// <summary>
        ///     Identifies the format of a file based on Signature.
        ///     Note that the Stream will REMAIN OPEN and at position 0, so it can be used afterwards.
        /// </summary>
        /// <param name="data">The Stream with the data</param>
        /// <returns></returns>
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
                    decompressedData = BLZ.decompress(data);
                    content = new byte[decompressedData.Length - 1];
                    Buffer.BlockCopy(decompressedData, 1, content, 0, content.Length);
                    data = new MemoryStream(content);
                    format = identify(data);
                    break;
                case fileFormat.cmpIECP: //Stella Glow
                    uint magic = input.ReadUInt32();
                    length = input.ReadUInt32();
                    decompressedData = LZSS.decompress(data, length);
                    data = new MemoryStream(decompressedData);
                    format = identify(data);
                    break;
                case fileFormat.cmpLZSS:
                case fileFormat.cmpLZSSHeader:
                    if (format == fileFormat.cmpLZSSHeader) input.ReadUInt32();
                    length = input.ReadUInt32() >> 8;
                    decompressedData = LZSS_Ninty.decompress(data, length);
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
                openDlg.Multiselect = true;

                switch (type)
                {
                    case fileType.model:
                        openDlg.Title = "Import Model";
                        openDlg.Filter = "All supported files|*.bch;*.bcmdl;*.bcres;*.cx;*.lz;*.cmp;*.zmdl;*.smes;*.smd";
                        openDlg.Filter += "|Binary CTR H3D|*.bch";
                        openDlg.Filter += "|Binary CTR Model|*.bcmdl";
                        openDlg.Filter += "|Binary CTR Resource|*.bcres";
                        openDlg.Filter += "|Compressed file|*.cx;*.lz;*.cmp";
                        openDlg.Filter += "|Fantasy Life Model|*.zmdl";
                        openDlg.Filter += "|New Love Plus Mesh|*.smes";
                        openDlg.Filter += "|StudioMdl Model|*.smd";
                        openDlg.Filter += "|All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            List<RenderBase.OModel> output = new List<RenderBase.OModel>();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                try
                                {
                                    switch (Path.GetExtension(fileName).ToLower())
                                    {
                                        case ".smd": output.AddRange(SMD.import(fileName).model); break;
                                        default:
                                            byte[] buffer = File.ReadAllBytes(fileName);
                                            Stream data = new MemoryStream(buffer);
                                            fileFormat format = identify(fileName);
                                            if (isCompressed(format)) decompress(ref data, ref format);
                                            switch (format)
                                            {
                                                case fileFormat.flZMdl: output.AddRange(ZMDL.load(data).model); break;
                                                case fileFormat.nlpSMes: output.AddRange(NLP.loadMesh(data).model); break;
                                                case fileFormat.nw4cCGfx: output.AddRange(CGFX.load(data).model); break;
                                                case fileFormat.nw4cH3D: output.AddRange(BCH.load((MemoryStream)data).model); break;
                                                case fileFormat.pkmnContainer: 
                                                    GenericContainer.OContainer container = PkmnContainer.load(data);
                                                    switch (container.fileIdentifier)
                                                    {
                                                        case "PC": //Pokémon model
                                                            output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).model);
                                                            break;
                                                        case "MM": //Pokémon Overworld model
                                                            output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).model);
                                                            break;
                                                        case "GR": //Pokémon Map model
                                                            output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).model);
                                                            break;
														case "PT": //Pokémon texture
															output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).model);
															break;
														case "PK": //Pokemon Visibility/Skeletal animations
															output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).model);
															break;
														case "PB": //Pokémon material animations
															output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).model);
															break;
														case "AD": //???
															output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).model);
															break;
                                                    }
                                                    break;
                                                default: data.Close(); break;
                                            }
                                            break;
                                    }
                                }
                                catch
                                {
                                    Debug.WriteLine("FileIO: Unable to import model file \"" + fileName + "\"!");
                                }
                            }
                            return output;
                        }
                        break;
                    case fileType.texture:
                        openDlg.Title = "Import Texture";
                        openDlg.Filter = "All supported files|*.bch;*.bcmdl;*.bcres;*.bctex;*.cx;*.lz;*.cmp;*.ztex";
                        openDlg.Filter += "|Binary CTR H3D|*.bch";
                        openDlg.Filter += "|Binary CTR Model|*.bcmdl";
                        openDlg.Filter += "|Binary CTR Resource|*.bcres";
                        openDlg.Filter += "|Binary CTR Texture|*.bctex";
                        openDlg.Filter += "|Compressed file|*.cx;*.lz;*.cmp";
                        openDlg.Filter += "|Fantasy Life Texture|*.ztex";
                        openDlg.Filter += "|All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            List<RenderBase.OTexture> output = new List<RenderBase.OTexture>();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                byte[] buffer = File.ReadAllBytes(fileName);
                                Stream data = new MemoryStream(buffer);
                                fileFormat format = identify(fileName);
                                if (isCompressed(format)) decompress(ref data, ref format);
                                switch (format)
                                {
                                    case fileFormat.flZTex: output.AddRange(ZTEX.load(data)); break;
                                    case fileFormat.nw4cCGfx: output.AddRange(CGFX.load(data).texture); break;
                                    case fileFormat.nw4cH3D: output.AddRange(BCH.load((MemoryStream)data).texture); break;
                                    case fileFormat.pkmnContainer: 
                                        GenericContainer.OContainer container = PkmnContainer.load(data);
                                        switch (container.fileIdentifier)
                                        {
                                            case "PC": //Pokémon model
                                                output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).texture);
                                                break;
                                            case "MM": //Pokémon Overworld model
                                                output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).texture);
                                                break;
                                            case "GR": //Pokémon Map model
                                                output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).texture);
                                                break;
                                            case "PT": //Pokémon texture
                                                output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).texture);
                                                break;
                                            case "PK": //Pokemon Visibility/Skeletal animations
                                                output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).texture);
                                                break;
                                            case "PB": //Pokémon material animations
                                                output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).texture);
                                                break;
                                            case "AD": //???
                                                output.AddRange(BCH.load(new MemoryStream(container.content[0].data)).texture);
                                                break;
                                        }
                                        break;
                                    default: data.Close(); break;
                                }
                            }
                            return output;
                        }
                        break;
                    case fileType.skeletalAnimation:
                        openDlg.Title = "Import Skeletal Animation";
                        openDlg.Filter = "All supported files|*.bch;*.bcres;*.bcskla;*.smd";
                        openDlg.Filter += "|Binary CTR H3D|*.bch";
                        openDlg.Filter += "|Binary CTR Resource|*.bcres";
                        openDlg.Filter += "|Binary CTR Skeletal Animation|*.bcskla";
                        openDlg.Filter += "|StudioMdl Model|*.smd";
                        openDlg.Filter += "|All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            RenderBase.OAnimationListBase output = new RenderBase.OAnimationListBase();
                            foreach (string fileName in openDlg.FileNames)
                            {
                                switch (Path.GetExtension(fileName).ToLower())
                                {
                                    case ".smd": output.list.AddRange(SMD.import(fileName).skeletalAnimation.list); break;
                                    default:
                                        byte[] buffer = File.ReadAllBytes(fileName);
                                        Stream data = new MemoryStream(buffer);
                                        fileFormat format = identify(fileName);
                                        if (isCompressed(format)) decompress(ref data, ref format);
                                        switch (format)
                                        {
                                            case fileFormat.nw4cCGfx: output.list.AddRange(CGFX.load(data).skeletalAnimation.list); break;
                                            case fileFormat.nw4cH3D: output.list.AddRange(BCH.load((MemoryStream)data).skeletalAnimation.list); break;
                                            default: data.Close(); break;
                                        }
                                        break;
                                }
                            }
                            return output;
                        }
                        break;
                    case fileType.materialAnimation:
                        openDlg.Title = "Import Material Animation";
                        openDlg.Filter = "All supported files|*.bch;";
                        openDlg.Filter += "|Binary CTR H3D|*.bch";
                        openDlg.Filter += "|All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            RenderBase.OAnimationListBase output = new RenderBase.OAnimationListBase();
                            foreach (string fileName in openDlg.FileNames)
                            {

                                byte[] buffer = File.ReadAllBytes(fileName);
                                Stream data = new MemoryStream(buffer);
                                fileFormat format = identify(fileName);
                                if (isCompressed(format)) decompress(ref data, ref format);
                                switch (format)
                                {
                                    case fileFormat.nw4cCGfx: output.list.AddRange(CGFX.load(data).materialAnimation.list); break;
                                    case fileFormat.nw4cH3D: output.list.AddRange(BCH.load((MemoryStream)data).materialAnimation.list); break;
                                    default: data.Close(); break;
                                }
                            }
                            return output;
                        }
                        break;
                    case fileType.visibilityAnimation:
                        openDlg.Title = "Import Visibility Animation";
                        openDlg.Filter = "All supported files|*.bch;";
                        openDlg.Filter += "|Binary CTR H3D|*.bch";
                        openDlg.Filter += "|All files|*.*";

                        if (openDlg.ShowDialog() == DialogResult.OK)
                        {
                            RenderBase.OAnimationListBase output = new RenderBase.OAnimationListBase();
                            foreach (string fileName in openDlg.FileNames)
                            {

                                byte[] buffer = File.ReadAllBytes(fileName);
                                Stream data = new MemoryStream(buffer);
                                fileFormat format = identify(fileName);
                                if (isCompressed(format)) decompress(ref data, ref format);
                                switch (format)
                                {
                                    case fileFormat.nw4cCGfx: output.list.AddRange(CGFX.load(data).visibilityAnimation.list); break;
                                    case fileFormat.nw4cH3D: output.list.AddRange(BCH.load((MemoryStream)data).visibilityAnimation.list); break;
                                    default: data.Close(); break;
                                }
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
