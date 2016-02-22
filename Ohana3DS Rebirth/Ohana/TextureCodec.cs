using System;
using System.Linq;
using System.Drawing;

namespace Ohana3DS_Rebirth.Ohana
{
    class TextureCodec
    {
        private static int[] tileOrder = { 0, 1, 8, 9, 2, 3, 10, 11, 16, 17, 24, 25, 18, 19, 26, 27, 4, 5, 12, 13, 6, 7, 14, 15, 20, 21, 28, 29, 22, 23, 30, 31, 32, 33, 40, 41, 34, 35, 42, 43, 48, 49, 56, 57, 50, 51, 58, 59, 36, 37, 44, 45, 38, 39, 46, 47, 52, 53, 60, 61, 54, 55, 62, 63 };
        private static int[,] etc1LUT = { { 2, 8, -2, -8 }, { 5, 17, -5, -17 }, { 9, 29, -9, -29 }, { 13, 42, -13, -42 }, { 18, 60, -18, -60 }, { 24, 80, -24, -80 }, { 33, 106, -33, -106 }, { 47, 183, -47, -183 } };

        /// <summary>
        ///     Decodes a PICA200 Texture.
        /// </summary>
        /// <param name="data">Buffer with the Texture</param>
        /// <param name="width">Width of the Texture</param>
        /// <param name="height">Height of the Texture</param>
        /// <param name="format">Pixel Format of the Texture</param>
        /// <returns></returns>
        public static Bitmap decode(byte[] data, int width, int height, RenderBase.OTextureFormat format)
        {
            byte[] output = new byte[width * height * 4];
            long dataOffset = 0;
            bool toggle = false;

            switch (format)
            {
                case RenderBase.OTextureFormat.rgba8:
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

                case RenderBase.OTextureFormat.rgb8:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                Buffer.BlockCopy(data, (int)dataOffset, output, (int)outputOffset, 3);
                                output[outputOffset + 3] = 0xff;

                                dataOffset += 3;
                            }
                        }
                    }
                    break;

                case RenderBase.OTextureFormat.rgba5551:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                ushort pixelData = (ushort)(data[dataOffset] | (data[dataOffset + 1] << 8));

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

                case RenderBase.OTextureFormat.rgb565:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                ushort pixelData = (ushort)(data[dataOffset] | (data[dataOffset + 1] << 8));

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

                case RenderBase.OTextureFormat.rgba4:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                ushort pixelData = (ushort)(data[dataOffset] | (data[dataOffset + 1] << 8));

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

                case RenderBase.OTextureFormat.la8:
                case RenderBase.OTextureFormat.hilo8:
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

                case RenderBase.OTextureFormat.l8:
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

                case RenderBase.OTextureFormat.a8:
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

                case RenderBase.OTextureFormat.la4:
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

                case RenderBase.OTextureFormat.l4:
                    for (int tY = 0; tY < height / 8; tY++)
                    {
                        for (int tX = 0; tX < width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long outputOffset = ((tX * 8) + x + (((tY * 8 + y)) * width)) * 4;

                                byte c = toggle ? (byte)((data[dataOffset++] & 0xf0) >> 4) : (byte)(data[dataOffset] & 0xf);
                                toggle = !toggle;
                                c = (byte)((c << 4) | c);
                                output[outputOffset] = c;
                                output[outputOffset + 1] = c;
                                output[outputOffset + 2] = c;
                                output[outputOffset + 3] = 0xff;
                            }
                        }
                    }
                    break;

                case RenderBase.OTextureFormat.a4:
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
                                byte a = toggle ? (byte)((data[dataOffset++] & 0xf0) >> 4) : (byte)(data[dataOffset] & 0xf);
                                toggle = !toggle;
                                output[outputOffset + 3] = (byte)((a << 4) | a);
                            }
                        }
                    }
                    break;

                case RenderBase.OTextureFormat.etc1:
                case RenderBase.OTextureFormat.etc1a4:
                    byte[] decodedData = etc1Decode(data, width, height, format == RenderBase.OTextureFormat.etc1a4);
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

            return TextureUtils.getBitmap(output.ToArray(), width, height);
        }

        /// <summary>
        ///     Encodes a PICA200 Texture.
        /// </summary>
        /// <param name="img">Input image to be encoded</param>
        /// <param name="format">Pixel Format of the Texture</param>
        /// <returns></returns>
        public static byte[] encode(Bitmap img, RenderBase.OTextureFormat format)
        {
            byte[] data = TextureUtils.getArray(img);
            byte[] output = new byte[data.Length];

            uint outputOffset = 0;
            switch (format)
            {
                case RenderBase.OTextureFormat.rgba8:
                    for (int tY = 0; tY < img.Height / 8; tY++)
                    {
                        for (int tX = 0; tX < img.Width / 8; tX++)
                        {
                            for (int pixel = 0; pixel < 64; pixel++)
                            {
                                int x = tileOrder[pixel] % 8;
                                int y = (tileOrder[pixel] - x) / 8;
                                long dataOffset = ((tX * 8) + x + ((tY * 8 + y) * img.Width)) * 4;

                                Buffer.BlockCopy(data, (int)dataOffset, output, (int)outputOffset + 1, 3);
                                output[outputOffset] = data[dataOffset + 3];

                                outputOffset += 4;
                            }
                        }
                    }
                    break;

                default: throw new NotImplementedException();
            }

            return output;
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

                            byte a = toggle ? (byte)((alphaBlock[alphaOffset++] & 0xf0) >> 4) : (byte)(alphaBlock[alphaOffset] & 0xf);
                            output[outputOffset + 3] = (byte)((a << 4) | a);
                            toggle = !toggle;
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

            bool flip = (blockTop & 0x1000000) > 0;
            bool difference = (blockTop & 0x2000000) > 0;

            uint r1, g1, b1;
            uint r2, g2, b2;

            if (difference)
            {
                r1 = blockTop & 0xf8;
                g1 = (blockTop & 0xf800) >> 8;
                b1 = (blockTop & 0xf80000) >> 16;

                r2 = (uint)((sbyte)(r1 >> 3) + ((sbyte)((blockTop & 7) << 5) >> 5));
                g2 = (uint)((sbyte)(g1 >> 3) + ((sbyte)((blockTop & 0x700) >> 3) >> 5));
                b2 = (uint)((sbyte)(b1 >> 3) + ((sbyte)((blockTop & 0x70000) >> 11) >> 5));

                r1 |= r1 >> 5;
                g1 |= g1 >> 5;
                b1 |= b1 >> 5;

                r2 = (r2 << 3) | (r2 >> 2);
                g2 = (g2 << 3) | (g2 >> 2);
                b2 = (b2 << 3) | (b2 >> 2);
            }
            else
            {
                r1 = blockTop & 0xf0;
                g1 = (blockTop & 0xf000) >> 8;
                b1 = (blockTop & 0xf00000) >> 16;

                r2 = (blockTop & 0xf) << 4;
                g2 = (blockTop & 0xf00) >> 4;
                b2 = (blockTop & 0xf0000) >> 12;

                r1 |= r1 >> 4;
                g1 |= g1 >> 4;
                b1 |= b1 >> 4;

                r2 |= r2 >> 4;
                g2 |= g2 >> 4;
                b2 |= b2 >> 4;
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
                        Color color1 = etc1Pixel(r1, g1, b1, x, y, blockBottom, table1);
                        Color color2 = etc1Pixel(r2, g2, b2, x + 2, y, blockBottom, table2);

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
                        Color color1 = etc1Pixel(r1, g1, b1, x, y, blockBottom, table1);
                        Color color2 = etc1Pixel(r2, g2, b2, x, y + 2, blockBottom, table2);

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

        private static Color etc1Pixel(uint r, uint g, uint b, int x, int y, uint block, uint table)
        {
            int index = x * 4 + y;
            uint MSB = block << 1;

            int pixel = index < 8 
                ? etc1LUT[table, ((block >> (index + 24)) & 1) + ((MSB >> (index + 8)) & 2)] 
                : etc1LUT[table, ((block >> (index + 8)) & 1) + ((MSB >> (index - 8)) & 2)];

            r = saturate((int)(r + pixel));
            g = saturate((int)(g + pixel));
            b = saturate((int)(b + pixel));

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        private static byte saturate(int value)
        {
            if (value > 0xff) return 0xff;
            if (value < 0) return 0;
            return (byte)(value & 0xff);
        }

        private static int[] etc1Scramble(int width, int height)
        {
            //Maybe theres a better way to do this?
            int[] tileScramble = new int[((width / 4) * (height / 4))];
            int baseAccumulator = 0;
            int rowAccumulator = 0;
            int baseNumber = 0;
            int rowNumber = 0;

            for (int tile = 0; tile < tileScramble.Length; tile++)
            {
                if ((tile % (width / 4) == 0) && tile > 0)
                {
                    if (rowAccumulator < 1)
                    {
                        rowAccumulator += 1;
                        rowNumber += 2;
                        baseNumber = rowNumber;
                    }
                    else
                    {
                        rowAccumulator = 0;
                        baseNumber -= 2;
                        rowNumber = baseNumber;
                    }
                }

                tileScramble[tile] = baseNumber;

                if (baseAccumulator < 1)
                {
                    baseAccumulator++;
                    baseNumber++;
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
    }
}