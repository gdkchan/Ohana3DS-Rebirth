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
        public static string Read_String(Stream Data, UInt32 Address)
        {
            Data.Seek(Address, SeekOrigin.Begin);
            var Bytes = new List<byte>();
            for (;;)
            {

                byte b = (byte)Data.ReadByte();
                if (b == 0) break;
                Bytes.Add(b);
            }
            return Encoding.ASCII.GetString(Bytes.ToArray());
        }

        public static string Read_String(BinaryReader Input, UInt32 Address)
        {
            Input.BaseStream.Seek(Address, SeekOrigin.Begin);
            var Bytes = new List<byte>();
            for (; ; )
            {

                byte b = (byte)Input.ReadByte();
                if (b == 0) break;
                Bytes.Add(b);
            }
            return Encoding.ASCII.GetString(Bytes.ToArray());
        }
    }
}
