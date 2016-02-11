using System.Collections.Generic;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    public class OContainer
    {
        public struct fileEntry
        {
            public string name;
            public byte[] data;
        }

        public string internalFormat;
        public List<fileEntry> content;

        public OContainer()
        {
            content = new List<fileEntry>();
        }
    }
}
