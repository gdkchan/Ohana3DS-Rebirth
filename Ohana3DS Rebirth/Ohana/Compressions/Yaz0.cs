using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Compressions
{
    class Yaz0
    {
        public static byte[] decompress(Stream data, uint decodedLength)
        {
            byte[] input = new byte[data.Length - data.Position];
            data.Read(input, 0, input.Length);
            data.Close();
            long inputOffset = 0;

            byte[] output = new byte[decodedLength];
            long outputOffset = 0;

            ushort mask = 0;
            byte header = 0;

            while (outputOffset < decodedLength)
            {
                if ((mask >>= 1) == 0)
                {
                    header = input[inputOffset++];
                    mask = 0x80;
                }

                if ((header & mask) > 0)
                {
                    if (outputOffset == output.Length) break;
                    output[outputOffset++] = input[inputOffset++];
                }
                else
                {
                    byte byte1 = input[inputOffset++];
                    byte byte2 = input[inputOffset++];

                    int dist = ((byte1 & 0xF) << 8) | byte2;
                    int position = (int)outputOffset - (dist + 1);

                    int length = byte1 >> 4;
                    if (length == 0)
                        length = input[inputOffset++] + 0x12;
                    else
                        length += 2;

                    while (length-- > 0) output[outputOffset++] = output[position++];
                }
            }

            return output;
        }
    }
}
