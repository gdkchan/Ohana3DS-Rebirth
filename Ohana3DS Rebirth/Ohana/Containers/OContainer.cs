using System.Collections.Generic;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    public class OContainer
    {
        public struct fileEntry
        {
            public string name;
            public byte[] data;

            public bool loadFromDisk;
            public uint fileOffset;
            public uint fileLength;
        }

        public Stream data;
        public List<fileEntry> content;

        public OContainer()
        {
            content = new List<fileEntry>();
        }
    }
}
