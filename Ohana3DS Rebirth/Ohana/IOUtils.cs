using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana
{
    class IOUtils
    {
        /// <summary>
        ///     Read an ASCII String from a given Reader at a given address.
        ///     Note that the text MUST end with a Null Terminator (0x0).
        /// </summary>
        /// <param name="Input">The Reader of the file Stream</param>
        /// <param name="Address">Address where the text begins</param>
        /// <returns></returns>
        public static string readString(BinaryReader input, uint address)
        {
            long originalPosition = input.BaseStream.Position;
            input.BaseStream.Seek(address, SeekOrigin.Begin);
            MemoryStream bytes = new MemoryStream();
            for (;;)
            {
                byte b = input.ReadByte();
                if (b == 0) break;
                bytes.WriteByte(b);
            }
            input.BaseStream.Seek(originalPosition, SeekOrigin.Begin);
            return Encoding.ASCII.GetString(bytes.ToArray());
        }
    }
}
