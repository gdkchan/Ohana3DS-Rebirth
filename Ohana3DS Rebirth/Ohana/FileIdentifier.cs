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
            MM
        }

        public static fileFormat identify(String fileName)
        {
            FileStream data = new FileStream(fileName, FileMode.Open);
            BinaryReader input = new BinaryReader(data);
            String magic = new string(input.ReadChars(2));
            if (magic.Equals("BC")) magic = "BCH"; //TODO: work on a better magic reader
            System.Windows.Forms.MessageBox.Show(magic);
            input.Close();
            data.Dispose();

            switch (magic)
            {
                case "BCH": return fileFormat.H3D;
                case "MM": return fileFormat.MM;
                default: return fileFormat.Unsupported;
            }
        }
    }
}
