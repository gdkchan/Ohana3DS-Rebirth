//Dragon Quest VII Container parser made by gdkchan for Ohana3DS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    public class DQVIIPack
    {
        private struct sectionEntry
        {
            public uint offset;
            public uint length;
        }

        /// <summary>
        ///     Reads the PACK containers from Dragon Quest VII.
        /// </summary>
        /// <param name="fileName">The File Name where the data is located</param>
        /// <returns></returns>
        public static GenericContainer.OContainer load(string fileName)
        {
            return load(new FileStream(fileName, FileMode.Open));
        }

        /// <summary>
        ///     Reads PACK containers from Dragon Quest VII.
        /// </summary>
        /// <param name="data">Stream with container data</param>
        /// <returns></returns>
        public static GenericContainer.OContainer load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);
            GenericContainer.OContainer output = new GenericContainer.OContainer();

            output.fileIdentifier = "DQVII_container";
            string modelMagic = IOUtils.readString(input, 0);
            if (modelMagic != "MODEL") throw new Exception("DQVIIPack: Invalid or corrupted DQVII Pack!");
            data.Seek(0x10, SeekOrigin.Begin);
            uint addressCount = input.ReadUInt32();
            uint baseAddress = input.ReadUInt32();
            uint addressSectionLength = input.ReadUInt32();
            input.ReadUInt32(); //Padding

            List<sectionEntry> sections = new List<sectionEntry>();
            for (int i = 0; i < addressCount; i++)
            {
                sectionEntry entry = new sectionEntry();

                entry.offset = input.ReadUInt32() + baseAddress;
                entry.length = input.ReadUInt32();

                sections.Add(entry);
            }

            data.Seek(sections[1].offset + 0x10, SeekOrigin.Begin);
            addressCount = input.ReadUInt32();
            baseAddress = sections[1].offset + input.ReadUInt32();
            addressSectionLength = input.ReadUInt32();
            for (int i = 0; i < addressCount; i++)
            {
                GenericContainer.OContainerEntry entry = new GenericContainer.OContainerEntry();

                data.Seek(sections[1].offset + 0x20 + (i * 8), SeekOrigin.Begin);
                //It have a FldData header before the CGFX file, that seems to point inside the CGFX file
                uint address = input.ReadUInt32() + baseAddress + 0x80;
                uint length = input.ReadUInt32() - 0x80;

                data.Seek(address, SeekOrigin.Begin);
                byte[] buffer = new byte[length];
                input.Read(buffer, 0, buffer.Length);
                entry.data = buffer;
                entry.name = String.Format("cgfx_{0}", i);

                output.content.Add(entry);
            }

            data.Close();
            data.Dispose();

            return output;
        }
    }
}
