using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace Ohana3DS_Rebirth.Ohana.Textures
{
    class ZTEX
    {
        private struct textureEntry
        {
            public string name;
            public int width, height;
            public uint offset;
            public uint length;
            public byte format;
        }

        public static List<RenderBase.OTexture> load(string fileName)
        {
            return load(new MemoryStream(File.ReadAllBytes(fileName)));
        }

        public static List<RenderBase.OTexture> load(Stream data)
        {
            List<RenderBase.OTexture> textures = new List<RenderBase.OTexture>();

            BinaryReader input = new BinaryReader(data);

            string ztexMagic = IOUtils.readString(input, 0, 4);
            ushort textureCount = input.ReadUInt16();
            input.ReadUInt16();
            input.ReadUInt32();

            List<textureEntry> entries = new List<textureEntry>();
            for (int i = 0; i < textureCount; i++)
            {
                textureEntry entry = new textureEntry();

                entry.name = IOUtils.readString(input, (uint)(0xc + (i * 0x58)));
                data.Seek(0xc + (i * 0x58) + 0x40, SeekOrigin.Begin);

                input.ReadUInt32();
                entry.offset = input.ReadUInt32();
                input.ReadUInt32();
                entry.length = input.ReadUInt32();
                entry.width = input.ReadUInt16();
                entry.height = input.ReadUInt16();
                input.ReadByte();
                entry.format = input.ReadByte();
                input.ReadUInt16();

                entries.Add(entry);
            }

            foreach (textureEntry entry in entries)
            {
                data.Seek(entry.offset, SeekOrigin.Begin);
                byte[] buffer = new byte[entry.length];
                data.Read(buffer, 0, buffer.Length);

                Bitmap bmp = null;
                switch (entry.format)
                {
                    case 1: bmp = TextureCodec.decode(buffer, entry.width, entry.height, RenderBase.OTextureFormat.rgb565); break;
                    case 5: bmp = TextureCodec.decode(buffer, entry.width, entry.height, RenderBase.OTextureFormat.rgba4); break;
                    case 9: bmp = TextureCodec.decode(buffer, entry.width, entry.height, RenderBase.OTextureFormat.rgba8); break;
                    case 0x18: bmp = TextureCodec.decode(buffer, entry.width, entry.height, RenderBase.OTextureFormat.etc1); break;
                    case 0x19: bmp = TextureCodec.decode(buffer, entry.width, entry.height, RenderBase.OTextureFormat.etc1a4); break;
                }
                
                textures.Add(new RenderBase.OTexture(bmp, entry.name));
            }

            data.Close();

            return textures;
        }
    }
}
