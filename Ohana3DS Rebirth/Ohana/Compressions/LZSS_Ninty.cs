using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Compressions
{
    class LZSS_Ninty
    {
        public static byte[] decompress(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                BinaryReader input = new BinaryReader(ms);
                uint decodedLength = input.ReadUInt32() >> 8;
                return decompress(ms, decodedLength);
            }
        }

        public static byte[] decompress(Stream data, uint decodedLength)
        {
            byte[] input = new byte[data.Length - data.Position];
            data.Read(input, 0, input.Length);
            data.Close();
            long inputOffset = 0;
            byte[] output = new byte[decodedLength];
            long outputOffset = 0;

            byte mask = 0;
            byte header = 0;

            while (outputOffset < decodedLength)
            {
                if ((mask >>= 1) == 0)
                {
                    header = input[inputOffset++];
                    mask = 0x80;
                }

                if ((header & mask) == 0)
                {
                    output[outputOffset++] = input[inputOffset++];
                }
                else
                {
                    int byte1, byte2, byte3, byte4;
                    byte1 = input[inputOffset++];
                    int position, length;
                    switch (byte1 >> 4)
                    {
                        case 0:
                            byte2 = input[inputOffset++];
                            byte3 = input[inputOffset++];

                            position = ((byte2 & 0xf) << 8) | byte3;
                            length = (((byte1 & 0xf) << 4) | (byte2 >> 4)) + 0x11;
                            break;
                        case 1:
                            byte2 = input[inputOffset++];
                            byte3 = input[inputOffset++];
                            byte4 = input[inputOffset++];

                            position = ((byte3 & 0xf) << 8) | byte4;
                            length = (((byte1 & 0xf) << 12) | (byte2 << 4) | (byte3 >> 4)) + 0x111;
                            break;
                        default:
                            byte2 = input[inputOffset++];

                            position = ((byte1 & 0xf) << 8) | byte2;
                            length = (byte1 >> 4) + 1;
                            break;
                    }
                    position++;

                    while (length > 0)
                    {
                        output[outputOffset] = output[outputOffset - position];
                        outputOffset++;
                        length--;
                    }
                }
            }

            return output;
        }
    }
}
