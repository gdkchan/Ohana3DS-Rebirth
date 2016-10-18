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
            uint garcLength = input.ReadUInt32();
            ushort endian = input.ReadUInt16();
            ushort version = input.ReadUInt16(); //0x400
            uint sectionCount = input.ReadUInt32();
            uint dataOffset = input.ReadUInt32();
            uint decompressedLength = input.ReadUInt32();
            uint compressedLength = input.ReadUInt32();

            data.Seek(garcLength, SeekOrigin.Begin); //This is just the header "GARC" blk len, not the entire file

            //File Allocation Table Offsets
            long fatoPosition = data.Position;
            string fatoMagic = IOUtils.readStringWithLength(input, 4);
            uint fatoLength = input.ReadUInt32();
            ushort fatoEntries = input.ReadUInt16();
            input.ReadUInt16(); //0xffff = Padding?

            long fatbPosition = fatoPosition + fatoLength;

            for (int i = 0; i < fatoEntries; i++)
            {
                data.Seek(fatoPosition + 0xc + i * 4, SeekOrigin.Begin);
                data.Seek(input.ReadUInt32() + fatbPosition + 0xc, SeekOrigin.Begin);

                uint flags = input.ReadUInt32();

                string folder = string.Empty;

                if (flags != 1) folder = string.Format("folder_{0:D5}/", i);

                for (int bit = 0; bit < 32; bit++)
                {
                    if ((flags & (1 << bit)) > 0)
                    {
                        uint startOffset = input.ReadUInt32();
                        uint endOffset = input.ReadUInt32();
                        uint length = input.ReadUInt32();

                        long position = data.Position;

                        input.BaseStream.Seek(startOffset + dataOffset, SeekOrigin.Begin);

                        byte[] buffer = new byte[length];
                        input.Read(buffer, 0, buffer.Length);

                        bool isCompressed = buffer.Length > 0 ? buffer[0] == 0x11 : false;
                        string extension = FileIO.getExtension(buffer, isCompressed ? 5 : 0);
                        string name = folder + string.Format("file_{0:D5}{1}", flags == 1 ? i : bit, extension);

                        //And add the file to the container list
                        OContainer.fileEntry entry = new OContainer.fileEntry();
                        entry.name = name;
                        entry.loadFromDisk = true;
                        entry.fileOffset = startOffset + dataOffset;
                        entry.fileLength = length;
                        entry.doDecompression = isCompressed;
                        output.content.Add(entry);

                        input.BaseStream.Seek(position, SeekOrigin.Begin);
                    }
                }
            }

            return output;
        }
    }
}
