//Dragon Quest VII Texture
using System;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Textures
{
    class DMP
    {
        public static RenderBase.OTexture load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);
            string dmpMagic = IOUtils.readString(input, 0, 3);
            if (dmpMagic != "DMP") throw new Exception("DMP: Invalid or corrupted file!");

            string format = IOUtils.readString(input, 4, 4);
            int width = input.ReadUInt16();
            int height = input.ReadUInt16();
            int pow2Width = input.ReadUInt16();
            int pow2Height = input.ReadUInt16();

            byte[] buffer = new byte[data.Length - 0x10];
            data.Read(buffer, 0, buffer.Length);
            data.Close();

            Bitmap bmp = null;
            switch (format)
            {
                case "8888": bmp = TextureCodec.decode(buffer, pow2Width, pow2Height, RenderBase.OTextureFormat.rgba8); break;
            }

            Bitmap newBmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(newBmp);
            gfx.DrawImage(bmp, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            gfx.Dispose();

            return new RenderBase.OTexture(newBmp, "texture");
        }
    }
}
