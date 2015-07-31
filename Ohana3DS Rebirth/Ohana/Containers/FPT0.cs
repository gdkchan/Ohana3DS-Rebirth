//Dragon Quest VII FPT0 Container parser made by gdkchan for Ohana3DS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    public class FPT0
    {
        private struct sectionEntry
        {
            public string name;
            public uint offset;
            public uint length;
        }

        /// <summary>
        ///     Reads the FPT0 containers from Dragon Quest VII.
        /// </summary>
        /// <param name="fileName">The File Name where the data is located</param>
        /// <returns></returns>
        public static GenericContainer.OContainer load(string fileName)
        {
            return load(new FileStream(fileName, FileMode.Open));
        }

        /// <summary>
        ///     Reads FPT0 containers from Dragon Quest VII.
        /// </summary>
        /// <param name="data">Stream with container data</param>
        /// <returns></returns>
        public static GenericContainer.OContainer load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);
            GenericContainer.OContainer output = new GenericContainer.OContainer();

            output.fileIdentifier = IOUtils.readString(input, 0, 4); //FPT0 magic
            input.ReadUInt32();
            uint entries = input.ReadUInt32();
            uint baseAddress = 0x10 + (entries * 0x20) + 0x80;
            input.ReadUInt32();

            List<sectionEntry> files = new List<sectionEntry>();
            for (int i = 0; i < entries; i++)
            {
                sectionEntry entry = new sectionEntry();

                entry.name = IOUtils.readString(input, (uint)(0x10 + (i * 0x20)));
                data.Seek(0x20 + (i * 0x20), SeekOrigin.Begin);
                input.ReadUInt32(); //Memory address?
                entry.offset = input.ReadUInt32() + baseAddress;
                entry.length = input.ReadUInt32();
                input.ReadUInt32(); //Padding?

                files.Add(entry);
            }

            foreach (sectionEntry file in files)
            {
                GenericContainer.OContainerEntry entry = new GenericContainer.OContainerEntry();

                data.Seek(file.offset, SeekOrigin.Begin);
                byte[] buffer = new byte[file.length];
                input.Read(buffer, 0, buffer.Length);
                entry.data = buffer;
                entry.name = file.name;

                output.content.Add(entry);
            }

            data.Close();
            data.Dispose();

            return output;
        }
    }
}
