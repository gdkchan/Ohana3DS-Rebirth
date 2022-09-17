using System;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Textures
{
    class _3DST
    {
        /// <summary>
        ///     Loads a 3DST Nintendo Anime Channel image.
        /// </summary>
        /// <param name="data">The 3DST image data</param>
        /// <returns>The image as a texture</returns>
        public static RenderBase.OTexture load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);

            // A 3DST image contains the word 'texture' at the beggining of the file, followed by 9 null bytes
            string _3dstMagic = IOUtils.readStringWithLength(input, 7);
            input.ReadUInt32();
            input.ReadUInt32();
            input.ReadByte();

            // Next, we have the width and height and finally, the image format
            ushort width = input.ReadUInt16();
            ushort height = input.ReadUInt16();
            ushort format = input.ReadUInt16();

            // The image data starts at byte 0x80
            data.Seek(0x80, SeekOrigin.Begin);
            byte[] buffer = new byte[data.Length - 0x80];
            data.Read(buffer, 0, buffer.Length);
            data.Close();

            // Note: 3DST images are flipped upside-down.
            // This is intended as the console flips it back and shows it correctly
            Bitmap bmp = null;
            switch (format)
            {
                case 0:
                    bmp = TextureCodec.decode(buffer, width, height, RenderBase.OTextureFormat.rgba8);
                    break;
                case 1:
                    bmp = TextureCodec.decode(buffer, width, height, RenderBase.OTextureFormat.rgb8);
                    break;
                case 2:
                    bmp = TextureCodec.decode(buffer, width, height, RenderBase.OTextureFormat.a8);
                    break;
                case 4:
                    bmp = TextureCodec.decode(buffer, width, height, RenderBase.OTextureFormat.etc1);
                    break;
                case 5:
                    bmp = TextureCodec.decode(buffer, width, height, RenderBase.OTextureFormat.rgba5551);
                    break;
                case 6:
                    bmp = TextureCodec.decode(buffer, width, height, RenderBase.OTextureFormat.rgb565);
                    break;
                case 7:
                    bmp = TextureCodec.decode(buffer, width, height, RenderBase.OTextureFormat.rgba4);
                    break;
            }

            Bitmap newBmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(newBmp);
            gfx.DrawImage(bmp, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            gfx.Dispose();

            return new RenderBase.OTexture(newBmp, "Texture");
        }
    }
}
