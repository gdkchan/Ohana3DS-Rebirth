using System;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana
{
    class FileIdentifier
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
    }
}
