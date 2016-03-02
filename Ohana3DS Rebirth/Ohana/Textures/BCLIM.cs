using System;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Textures
{
    class BCLIM
    {
        /// <summary>
        ///     Loads a Binary Citra Layout Image from a Stream.
        /// </summary>
        /// <param name="data">The Stream with the data</param>
        /// <returns>The image as a texture</returns>
        public static RenderBase.OTexture load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);
            data.Seek(-0x28, SeekOrigin.End);

            //Note: Stella Glow uses Little Endian BFLIMs, so please don't check the magic ;)
            string climMagic = IOUtils.readStringWithLength(input, 4);
            ushort endian = input.ReadUInt16();
            uint climHeaderLength = input.ReadUInt32();
            input.ReadUInt16();
            uint fileLength = input.ReadUInt32();
            input.ReadUInt32();

            string imagMagic = IOUtils.readStringWithLength(input, 4);
            uint imagHeaderLength = input.ReadUInt32();
            ushort width = input.ReadUInt16();
            ushort height = input.ReadUInt16();
            uint format = input.ReadUInt32();
            uint length = input.ReadUInt32();
            if (climMagic == "FLIM") format = (format >> 16) & 0xf;

            data.Seek(-(length + 0x28), SeekOrigin.End);
            byte[] buffer = new byte[length];
            data.Read(buffer, 0, buffer.Length);
            data.Close();

            int pow2Width = (int)(Math.Pow(2, Math.Ceiling(Math.Log(width) / Math.Log(2))));
            int pow2Height = (int)(Math.Pow(2, Math.Ceiling(Math.Log(height) / Math.Log(2))));

            Bitmap bmp = null;
            switch (format)
            {
                case 0: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.l8); break;
                case 1: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.a8); break;
                case 2: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.la4); break;
                case 3: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.la8); break;
                case 4: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.hilo8); break;
                case 5: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.rgb565); break;
                case 6: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.rgb8); break;
                case 7: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.rgba5551); break;
                case 8: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.rgba4); break;
                case 9: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.rgba8); break;
                case 0xa: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.etc1); break;
                case 0xb: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.etc1a4); break;
                case 0xc: bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.l4); break;
            }

            Bitmap newBmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(newBmp);
            gfx.DrawImage(bmp, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            gfx.Dispose();

            return new RenderBase.OTexture(newBmp, "texture");
        }
    }
}
