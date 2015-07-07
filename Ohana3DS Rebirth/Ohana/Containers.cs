using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.Ohana
{
    class Containers
    {
        private struct MMHeader
        {
            public string magic;
            public uint unkown;
            public uint fileLength;

            public uint mainBCHOffset;
        }

        public static RenderBase.OModelGroup loadMM(string fileName)
        {
            FileStream data = new FileStream(fileName, FileMode.Open);
            BinaryReader input = new BinaryReader(data);

            //Primary header
            MMHeader header = new MMHeader();
            header.magic = IOUtils.readString(input, 0);
            header.unkown = input.ReadUInt16();
            data.Seek(4, SeekOrigin.Begin);
            header.mainBCHOffset = input.ReadUInt32();
            header.fileLength = input.ReadUInt32();
            byte[] array = new byte[header.fileLength - header.mainBCHOffset];
            input.BaseStream.Position = header.mainBCHOffset;
            input.Read(array, 0, (int)(header.fileLength - header.mainBCHOffset));
            input.Close();
            data.Close();
            return BCH.load(new MemoryStream(array));
        }
    }
}
