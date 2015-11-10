using System.Collections.Generic;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    public class GenericContainer
    {
        public enum OCompression
        {
            none = 0,
            rle = 1,
            lzss = 2,
            lz77 = 4,
            huffman = 8,
            deflate = 0x10,
            zlib = 0x20,
            blz = 0x40,
            yaz0 = 0x80
        }

        public struct OContainerEntry
        {
            public string name;
            public long compression;
            public byte[] data;
        }

        public class OContainer
        {
            public string name;
            public string fileIdentifier;
            public List<OContainerEntry> content;

            public OContainer()
            {
                content = new List<OContainerEntry>();
            }
        }
    }
}
