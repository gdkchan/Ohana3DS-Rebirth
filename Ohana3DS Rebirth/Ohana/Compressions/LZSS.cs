using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Compressions
{
    class LZSS
    {
        public static byte[] decompress(Stream data, uint decodedLength)
        {
            byte[] input = new byte[data.Length - data.Position];
            data.Read(input, 0, input.Length);
            data.Close();
            long inputOffset = 0;

            byte[] output = new byte[decodedLength];
            byte[] dictionary = new byte[4096];

            long outputOffset = 0;
            long dictionaryOffset = 4078;

            ushort mask = 0x80;
            byte header = 0;

            while (outputOffset < decodedLength)
            {
                if ((mask <<= 1) == 0x100)
                {
                    header = input[inputOffset++];
                    mask = 1;
                }

                if ((header & mask) > 0)
                {
                    if (outputOffset == output.Length) break;
                    output[outputOffset++] = input[inputOffset];
                    dictionary[dictionaryOffset] = input[inputOffset++];
                    dictionaryOffset = (dictionaryOffset + 1) & 0xfff;
                }
                else
                {
                    ushort value = (ushort)(input[inputOffset++] | (input[inputOffset++] << 8));
                    int length = ((value >> 8) & 0xf) + 3;
                    int position = ((value & 0xf000) >> 4) | (value & 0xff);

                    while (length > 0)
                    {
                        dictionary[dictionaryOffset] = dictionary[position];
                        output[outputOffset++] = dictionary[dictionaryOffset];
                        dictionaryOffset = (dictionaryOffset + 1) & 0xfff;
                        position = (position + 1) & 0xfff;
                        length--;
                    }
                }
            }

            return output;
        }
    }
}
