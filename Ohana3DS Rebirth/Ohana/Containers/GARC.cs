using System;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    class GARC
    {
        /// <summary>
        ///     Reads a GARC archive.
        /// </summary>
        /// <param name="fileName">The File Name where the data is located</param>
        /// <returns>The container data</returns>
        public static OContainer load(string fileName)
        {
            return load(new FileStream(fileName, FileMode.Open));
        }

        /// <summary>
        ///     Reads a GARC archive.
        /// </summary>
        /// <param name="data">Stream of the data</param>
        /// <returns>The container data</returns>
        public static OContainer load(Stream data)
        {
            OContainer output = new OContainer();
            BinaryReader input = new BinaryReader(data);

            output.data = data;

            string garcMagic = IOUtils.readStringWithLength(input, 4);
            uint garcHeaderLength = input.ReadUInt32();
            ushort endian = input.ReadUInt16();
            input.ReadUInt16(); //0x400
            uint sectionCount = input.ReadUInt32();
            uint dataOffset = input.ReadUInt32();
            uint decompressedLength = input.ReadUInt32();
            uint compressedLength = input.ReadUInt32();

            //File Allocation Table Offsets
            string fatoMagic = IOUtils.readStringWithLength(input, 4);
            uint fatoHeaderLength = input.ReadUInt32();
            ushort fatoEntries = input.ReadUInt16();
            input.ReadUInt16(); //0xffff = Padding?
            data.Seek(fatoEntries * 4, SeekOrigin.Current); //We don't need this

            string fatbMagic = IOUtils.readStringWithLength(input, 4);
            uint fatbHeaderLength = input.ReadUInt32();
            uint entries = input.ReadUInt32();

            long baseOffset = data.Position;

            for (int i = 0; i < entries; i++)
            {
                data.Seek(baseOffset + i * 0x10, SeekOrigin.Begin);

                uint flags = input.ReadUInt32();
                uint startOffset = input.ReadUInt32();
                uint endOffset = input.ReadUInt32();
                uint length = input.ReadUInt32();

                data.Seek(startOffset + dataOffset, SeekOrigin.Begin);
                byte[] buffer = new byte[Math.Min(0x10, length)];
                data.Read(buffer, 0, buffer.Length);
                bool isCompressed = buffer.Length > 0 ? buffer[0] == 0x11 : false;
                string extension = IOUtils.getExtensionFromMagic(buffer, isCompressed ? 5 : 0);

                //Make sure we don't grab garbage within the extension
                switch (extension.Substring(0, 3))
                {
                    case ".ad": extension = ".ad"; break;
                    case ".gr": extension = ".gr"; break;
                    case ".mm": extension = ".mm"; break;
                    case ".pb": extension = ".pb"; break;
                    case ".pc": extension = ".pc"; break;
                    case ".pk": extension = ".pk"; break;
                    case ".po": extension = ".po"; break;
                    case ".pt": extension = ".pt"; break;
                }
                string name = string.Format("file_{0:D5}{1}", i, extension);

                OContainer.fileEntry entry = new OContainer.fileEntry();
                entry.name = name;
                entry.loadFromDisk = true;
                entry.fileOffset = startOffset + dataOffset;
                entry.fileLength = length;
                output.content.Add(entry);
            }

            return output;
        }
    }
}
