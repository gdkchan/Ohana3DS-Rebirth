using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Textures.PocketMonsters
{
    class GfTexture
    {
        /// <summary>
        ///     Loads a Game freak texture.
        /// </summary>
        /// <param name="data">The texture data</param>
        /// <returns>The image as a texture</returns>
        public static RenderBase.OTexture load(Stream data, bool keepOpen = false)
        {
            BinaryReader input = new BinaryReader(data);
            long descAddress = data.Position;

            data.Seek(8, SeekOrigin.Current);
            if (IOUtils.readStringWithLength(input, 7) != "texture") return null;

            data.Seek(descAddress + 0x18, SeekOrigin.Begin);
            int texLength = input.ReadInt32();

            data.Seek(descAddress + 0x28, SeekOrigin.Begin);
            string texName = IOUtils.readStringWithLength(input, 0x40);

            data.Seek(descAddress + 0x68, SeekOrigin.Begin);
            ushort width = input.ReadUInt16();
            ushort height = input.ReadUInt16();
            ushort texFormat = input.ReadUInt16();
            ushort texMipMaps = input.ReadUInt16();

            data.Seek(0x10, SeekOrigin.Current);
            byte[] texBuffer = input.ReadBytes(texLength);

            RenderBase.OTextureFormat fmt = RenderBase.OTextureFormat.dontCare;

            switch (texFormat)
            {
                case 0x2: fmt = RenderBase.OTextureFormat.rgb565; break;
                case 0x3: fmt = RenderBase.OTextureFormat.rgb8; break;
                case 0x4: fmt = RenderBase.OTextureFormat.rgba8; break;
                case 0x17: fmt = RenderBase.OTextureFormat.rgba5551; break;
                case 0x23: fmt = RenderBase.OTextureFormat.la8; break;
                case 0x24: fmt = RenderBase.OTextureFormat.hilo8; break;
                case 0x25: fmt = RenderBase.OTextureFormat.l8; break;
                case 0x26: fmt = RenderBase.OTextureFormat.a8; break;
                case 0x27: fmt = RenderBase.OTextureFormat.la4; break;
                case 0x28: fmt = RenderBase.OTextureFormat.l4; break;
                case 0x29: fmt = RenderBase.OTextureFormat.a4; break;
                case 0x2a: fmt = RenderBase.OTextureFormat.etc1; break;
                case 0x2b: fmt = RenderBase.OTextureFormat.etc1a4; break;

                default: Debug.WriteLine("Unk tex fmt " + texFormat.ToString("X4") + " @ " + texName); break;
            }

            Bitmap tex = TextureCodec.decode(texBuffer, width, height, fmt);

            if (!keepOpen) data.Close();

            return new RenderBase.OTexture(tex, texName);
        }
    }
}
