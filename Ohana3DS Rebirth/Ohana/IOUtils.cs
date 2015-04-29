using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static string Read_String(BinaryReader Input, UInt32 Address)
        {
            long originalPosition = Input.BaseStream.Position;
            Input.BaseStream.Seek(Address, SeekOrigin.Begin);
            MemoryStream Bytes = new MemoryStream();
            for (;;)
            {

                byte b = (byte)Input.ReadByte();
                if (b == 0) break;
                Bytes.WriteByte(b);
            }
            Input.BaseStream.Seek(originalPosition, SeekOrigin.Begin);
            return Encoding.ASCII.GetString(Bytes.ToArray());
        }
    }
}
