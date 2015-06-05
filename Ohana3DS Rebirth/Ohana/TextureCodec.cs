using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Ohana3DS_Rebirth.Ohana
{
    class TextureCodec
    {
        public enum textureFormat
        {
            rgba8,
            rgb8,
            rgba5551,
            rgb565,
            rgba4,
            la8,
            hilo8,
            l8,
            a8,
            la4,
            l4,
            a4,
            etc1,
            etc1a4
        }

        private static int[] tileOrder = { 0, 1, 8, 9, 2, 3, 10, 11, 16, 17, 24, 25, 18, 19, 26, 27, 4, 5, 12, 13, 6, 7, 14, 15, 20, 21, 28, 29, 22, 23, 30, 31, 32, 33, 40, 41, 34, 35, 42, 43, 48, 49, 56, 57, 50, 51, 58, 59, 36, 37, 44, 45, 38, 39, 46, 47, 52, 53, 60, 61, 54, 55, 62, 63 };
        private static int[,] etc1LUT = { { 2, 8, -2, -8 }, { 5, 17, -5, -17 }, { 9, 29, -9, -29 }, { 13, 42, -13, -42 }, { 18, 60, -18, -60 }, { 24, 80, -24, -80 }, { 33, 106, -33, -106 }, { 47, 183, -47, -183 } };

        #region "Decode"
        public static Bitmap decode(byte[] data, int width, int height, textureFormat format)
        {
            byte[] output = new byte[width * height * 4];
            long dataOffset = 0;

            switch (format)
            {
                case textureFormat.rgba8:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + ((tY * 8 + y) * width)) * 4;

                                Buffer.BlockCopy(data, (int)dataOffset + 1, output, (int)outputOffset, 3);
                                output[outputOffset + 3] = data[dataOffset];

                                dataOffset += 4;
                            }
                        }
                    }
                    break;

                case textureFormat.rgb8:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                Buffer.BlockCopy(data, (int)dataOffset + 1, output, (int)outputOffset, 3);
                                output[outputOffset + 3] = 0xff;

                                dataOffset += 3;
                            }
                        }
                    }
                    break;

                case textureFormat.rgba5551:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                ushort pixelData = (ushort)(data[dataOffset] | ((ushort)data[dataOffset + 1] << 8));

                                byte r = (byte)(((pixelData >> 1) & 0x1f) << 3);
                                byte g = (byte)(((pixelData >> 6) & 0x1f) << 3);
                                byte b = (byte)(((pixelData >> 11) & 0x1f) << 3);
                                byte a = (byte)((pixelData & 1) * 0xff);

                                output[outputOffset] = (byte)(r | (r >> 5));
                                output[outputOffset + 1] = (byte)(g | (g >> 5));
                                output[outputOffset + 2] = (byte)(b | (b >> 5));
                                output[outputOffset + 3] = a;

                                dataOffset += 2;
                            }
                        }
                    }
                    break;

                case textureFormat.rgb565:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                ushort pixelData = (ushort)(data[dataOffset] | ((ushort)data[dataOffset + 1] << 8));

                                byte r = (byte)((pixelData & 0x1f) << 3);
                                byte g = (byte)(((pixelData >> 5) & 0x3f) << 2);
                                byte b = (byte)(((pixelData >> 11) & 0x1f) << 3);

                                output[outputOffset] = (byte)(r | (r >> 5));
                                output[outputOffset + 1] = (byte)(g | (g >> 6));
                                output[outputOffset + 2] = (byte)(b | (b >> 5));
                                output[outputOffset + 3] = 0xff;

                                dataOffset += 2;
                            }
                        }
                    }
                    break;

                case textureFormat.rgba4:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                ushort pixelData = (ushort)(data[dataOffset] | ((ushort)data[dataOffset + 1] << 8));

                                byte r = (byte)((pixelData >> 4) & 0xf);
                                byte g = (byte)((pixelData >> 8) & 0xf);
                                byte b = (byte)((pixelData >> 12) & 0xf);
                                byte a = (byte)(pixelData & 0xf);

                                output[outputOffset] = (byte)(r | (r << 4));
                                output[outputOffset + 1] = (byte)(g | (g << 4));
                                output[outputOffset + 2] = (byte)(b | (b << 4));
                                output[outputOffset + 3] = (byte)(a | (a << 4));

                                dataOffset += 2;
                            }
                        }
                    }
                    break;

                case textureFormat.la8:
                case textureFormat.hilo8:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                output[outputOffset] = data[dataOffset];
                                output[outputOffset + 1] = data[dataOffset];
                                output[outputOffset + 2] = data[dataOffset];
                                output[outputOffset + 3] = data[dataOffset + 1];

                                dataOffset += 2;
                            }
                        }
                    }
                    break;

                case textureFormat.l8:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                output[outputOffset] = data[dataOffset];
                                output[outputOffset + 1] = data[dataOffset];
                                output[outputOffset + 2] = data[dataOffset];
                                output[outputOffset + 3] = 0xff;

                                dataOffset++;
                            }
                        }
                    }
                    break;

                case textureFormat.a8:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                output[outputOffset] = 0xff;
                                output[outputOffset + 1] = 0xff;
                                output[outputOffset + 2] = 0xff;
                                output[outputOffset + 3] = data[dataOffset];

                                dataOffset++;
                            }
                        }
                    }
                    break;

                case textureFormat.la4:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                output[outputOffset] = (byte)(data[dataOffset] >> 4);
                                output[outputOffset + 1] = (byte)(data[dataOffset] >> 4);
                                output[outputOffset + 2] = (byte)(data[dataOffset] >> 4);
                                output[outputOffset + 3] = (byte)(data[dataOffset] & 0xf);

                                dataOffset++;
                            }
                        }
                    }
                    break;

                case textureFormat.l4:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 8;

                                output[outputOffset] = (byte)(data[dataOffset / 2] >> 4);
                                output[outputOffset + 1] = (byte)(data[dataOffset / 2] >> 4);
                                output[outputOffset + 2] = (byte)(data[dataOffset / 2] >> 4);
                                output[outputOffset + 3] = 0xff;

                                output[outputOffset + 4] = (byte)(data[dataOffset / 2] & 0xf);
                                output[outputOffset + 5] = (byte)(data[dataOffset / 2] & 0xf);
                                output[outputOffset + 6] = (byte)(data[dataOffset / 2] & 0xf);
                                output[outputOffset + 7] = 0xff;

                                dataOffset++;
                            }
                        }
                    }
                    break;

                case textureFormat.a4:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 8;

                                output[outputOffset] = 0xff;
                                output[outputOffset + 1] = 0xff;
                                output[outputOffset + 2] = 0xff;
                                output[outputOffset + 3] = (byte)(data[dataOffset / 2] >> 4);

                                output[outputOffset + 4] = 0xff;
                                output[outputOffset + 5] = 0xff;
                                output[outputOffset + 6] = 0xff;
                                output[outputOffset + 7] = (byte)(data[dataOffset / 2] & 0xf);

                                dataOffset++;
                            }
                        }
                    }
                    break;

                case textureFormat.etc1:
                case textureFormat.etc1a4:
                    byte[] decodedData = etc1Decode(data, width, height, format == textureFormat.etc1a4);
                    int[] etc1Order = etc1Scramble(width, height);

                    int i = 0;
                    for (int tY = 0; tY < height / 4; tY++) {
	                    for (int tX = 0; tX < width / 4; tX++) {
                            int TX = etc1Order[i] % (width / 4);
                            int TY = (etc1Order[i] - TX) / (width / 4);
		                    for (int y = 0; y < 4; y++) {
			                    for (int x = 0; x < 4; x++) {
				                    dataOffset = ((TX * 4) + x + (((TY * 4) + y) * width)) * 4;
                                    long outputOffset = ((tX * 4) + x + (((tY * 4 + y)) * width)) * 4;

                                    Buffer.BlockCopy(decodedData, (int)dataOffset, output, (int)outputOffset, 4);
			                    }
		                    }
		                    i += 1;
	                    }
                    }

                    break;
            }

            return getBitmap(output.ToArray(), width, height);
        }

        private static Bitmap getBitmap(byte[] array, int width, int height)
        {
            Bitmap img = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(array, 0, imgData.Scan0, array.Length);
            img.UnlockBits(imgData);
            return img;
        }

        #region "ETC1"
        private static byte[] etc1Decode(byte[] input, int width, int height, bool alpha)
        {
            byte[] output = new byte[(width * height * 4)];
            long offset = 0;

            for (int y = 0; y < height / 4; y++)
            {
                for (int x = 0; x < width / 4; x++)
                {
                    byte[] colorBlock = new byte[8];
                    byte[] alphaBlock = new byte[8];
                    if (alpha)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            colorBlock[7 - i] = input[offset + 8 + i];
                            alphaBlock[i] = input[offset + i];
                        }
                        offset += 16;
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            colorBlock[7 - i] = input[offset + i];
                            alphaBlock[i] = 0xff;
                        }
                        offset += 8;
                    }

                    colorBlock = etc1DecodeBlock(colorBlock);

                    bool toggle = false;
                    long alphaOffset = 0;
                    for (int tX = 0; tX < 4; tX++)
                    {
                        for (int tY = 0; tY < 4; tY++)
                        {
                            int outputOffset = (x * 4 + tX + ((y * 4 + tY) * width)) * 4;
                            int blockOffset = (tX + (tY * 4)) * 4;
                            Buffer.BlockCopy(colorBlock, blockOffset, output, outputOffset, 3);

                            byte a = 0;
                            if (toggle)
                            {
                                a = (byte)((alphaBlock[alphaOffset] & 0xf0) >> 4);
                                alphaOffset++;
                            }
                            else
                            {
                                a = (byte)(alphaBlock[alphaOffset] & 0xf);
                            }
                            toggle = !toggle;
                            output[outputOffset + 3] = (byte)((a << 4) | a);
                        }
                    }
                }
            }

            return output;
        }

        private static byte[] etc1DecodeBlock(byte[] data)
        {
            uint blockTop = BitConverter.ToUInt32(data, 0);
            uint blockBottom = BitConverter.ToUInt32(data, 4);

            bool flip = (blockTop & 0x1000000) != 0;
            bool difference = (blockTop & 0x2000000) != 0;

            uint r1 = 0;
            uint g1 = 0;
            uint b1 = 0;

            uint r2 = 0;
            uint g2 = 0;
            uint b2 = 0;

            if (difference)
            {
                r1 = blockTop & 0xf8;
                g1 = (blockTop & 0xf800) >> 8;
                b1 = (blockTop & 0xf80000) >> 16;

                r2 = (uint)((sbyte)(r1 >> 3) + ((sbyte)((blockTop & 7) << 5) >> 5));
                g2 = (uint)((sbyte)(g1 >> 3) + ((sbyte)((blockTop & 0x700) >> 3) >> 5));
                b2 = (uint)((sbyte)(b1 >> 3) + ((sbyte)((blockTop & 0x70000) >> 11) >> 5));

                r1 = r1 + (r1 >> 5);
                g1 = g1 + (g1 >> 5);
                b1 = b1 + (b1 >> 5);

                r2 = (r2 << 3) + (r2 >> 2);
                g2 = (g2 << 3) + (g2 >> 2);
                b2 = (b2 << 3) + (b2 >> 2);
            }
            else
            {
                r1 = blockTop & 0xf0;
                r1 = r1 + (r1 >> 4);
                g1 = (blockTop & 0xf000) >> 8;
                g1 = g1 + (g1 >> 4);
                b1 = (blockTop & 0xf00000) >> 16;
                b1 = b1 + (b1 >> 4);

                r2 = (blockTop & 0xf) << 4;
                r2 = r2 + (r2 >> 4);
                g2 = (blockTop & 0xf00) >> 4;
                g2 = g2 + (g2 >> 4);
                b2 = (blockTop & 0xf0000) >> 12;
                b2 = b2 + (b2 >> 4);
            }

            uint table1 = (blockTop >> 29) & 7;
            uint table2 = (blockTop >> 26) & 7;

            byte[] output = new byte[(4 * 4 * 4)];
            if (!flip)
            {
                for (int y = 0; y <= 3; y++)
                {
                    for (int x = 0; x <= 1; x++)
                    {
                        Color color1 = getETC1Pixel(r1, g1, b1, x, y, blockBottom, table1);
                        Color color2 = getETC1Pixel(r2, g2, b2, x + 2, y, blockBottom, table2);

                        int offset1 = (y * 4 + x) * 4;
                        output[offset1] = color1.B;
                        output[offset1 + 1] = color1.G;
                        output[offset1 + 2] = color1.R;

                        int offset2 = (y * 4 + x + 2) * 4;
                        output[offset2] = color2.B;
                        output[offset2 + 1] = color2.G;
                        output[offset2 + 2] = color2.R;
                    }
                }
            }
            else
            {
                for (int y = 0; y <= 1; y++)
                {
                    for (int x = 0; x <= 3; x++)
                    {
                        Color color1 = getETC1Pixel(r1, g1, b1, x, y, blockBottom, table1);
                        Color color2 = getETC1Pixel(r2, g2, b2, x, y + 2, blockBottom, table2);

                        int offset1 = (y * 4 + x) * 4;
                        output[offset1] = color1.B;
                        output[offset1 + 1] = color1.G;
                        output[offset1 + 2] = color1.R;

                        int offset2 = ((y + 2) * 4 + x) * 4;
                        output[offset2] = color2.B;
                        output[offset2 + 1] = color2.G;
                        output[offset2 + 2] = color2.R;
                    }
                }
            }

            return output;
        }

        private static Color getETC1Pixel(uint r, uint g, uint b, int x, int y, uint block, uint table)
        {
            int index = x * 4 + y;
            int pixel = 0;
            uint MSB = block << 1;

            if (index < 8)
            {
                pixel = etc1LUT[table, ((block >> (index + 24)) & 1) + ((MSB >> (index + 8)) & 2)];
            }
            else
            {
                pixel = etc1LUT[table, ((block >> (index + 8)) & 1) + ((MSB >> (index - 8)) & 2)];
            }

            r = clamp((int)(r + pixel));
            g = clamp((int)(g + pixel));
            b = clamp((int)(b + pixel));

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        private static byte clamp(int value)
        {
            if (value > 0xff) return 0xff;
            else if (value < 0) return 0;
            else return (byte)(value & 0xff);
        }

        private static int[] etc1Scramble(int width, int height)
        {
            //Maybe theres a better way to do this?
            int[] tileScramble = new int[((width / 4) * (height / 4))];
            int baseAccumulator = 0;
            int lineAccumulator = 0;
            int baseNumber = 0;
            int lineNumber = 0;

            for (int tile = 0; tile < tileScramble.Length; tile++)
            {
                if ((tile % (width / 4) == 0) & tile > 0)
                {
                    if (lineAccumulator < 1)
                    {
                        lineAccumulator += 1;
                        lineNumber += 2;
                        baseNumber = lineNumber;
                    }
                    else
                    {
                        lineAccumulator = 0;
                        baseNumber -= 2;
                        lineNumber = baseNumber;
                    }
                }

                tileScramble[tile] = baseNumber;

                if (baseAccumulator < 1)
                {
                    baseAccumulator += 1;
                    baseNumber += 1;
                }
                else
                {
                    baseAccumulator = 0;
                    baseNumber += 3;
                }
            }

            return tileScramble;
        }
        #endregion

        #endregion

    }
}
