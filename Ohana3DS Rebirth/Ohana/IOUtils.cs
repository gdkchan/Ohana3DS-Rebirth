using System.Text;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana
{
    class IOUtils
    {
        /// <summary>
        ///     Read an ASCII String from a given Reader at a given address.
        ///     Note that the text MUST end with a Null Terminator (0x0).
        ///     It doesn't advances the position after reading.
        /// </summary>
        /// <param name="input">The Reader of the File Stream</param>
        /// <param name="address">Address where the text begins</param>
        /// <returns></returns>
        public static string readString(BinaryReader input, uint address, bool advancePosition = false)
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
            if (!advancePosition) input.BaseStream.Seek(originalPosition, SeekOrigin.Begin);
            return Encoding.ASCII.GetString(bytes.ToArray());
        }

        /// <summary>
        ///     Read an ASCII String from a given Reader at a given address with given size.
        ///     It will also stop reading if a Null Terminator (0x0) is found.
        ///     It WILL advance the position until the count is reached, or a 0x0 is found.
        /// </summary>
        /// <param name="input">The Reader of the File Stream</param>
        /// <param name="address">Address where the text begins</param>
        /// <param name="count">Number of bytes that the text have</param>
        /// <returns></returns>
        public static string readString(BinaryReader input, uint address, uint count)
        {
            input.BaseStream.Seek(address, SeekOrigin.Begin);
            MemoryStream bytes = new MemoryStream();
            for (int i = 0; i < count; i++)
            {
                byte b = input.ReadByte();
                if (b == 0) break;
                bytes.WriteByte(b);
            }
            return Encoding.ASCII.GetString(bytes.ToArray());
        }

        /// <summary>
        ///     Sign extends the value, so it will keep the sign flag.
        /// </summary>
        /// <param name="value">The value that should be sign-extended</param>
        /// <param name="bits">Number of bits that the value have</param>
        /// <returns></returns>
        public static int signExtend(uint value, int bits)
        {
            int output = (int)value;
            bool sign = (value & (1 << (bits - 1))) > 0;
            if (sign) output -= (1 << bits);
            return output;
        }

        /// <summary>
        ///     Sign extends the value, so it will keep the sign flag.
        /// </summary>
        /// <param name="value">The value that should be sign-extended</param>
        /// <param name="bits">Number of bits that the value have</param>
        /// <returns></returns>
        public static int signExtend(int value, int bits)
        {
            bool sign = (value & (1 << (bits - 1))) > 0;
            if (sign) value -= (1 << bits);
            return value;
        }
    }
}
