using System.IO;

namespace Ohana3DS_Rebirth.Ohana.Containers
{
    public class PkmnContainer
    {
        /// <summary>
        ///     Reads a generic Pokémon container from a File.
        ///     Those containers are the ones that starts with "GR", "MM", "AD" and so on...
        /// </summary>
        /// <param name="fileName">The File Name where the data is located</param>
        /// <returns></returns>
        public static OContainer load(string fileName)
        {
            return load(new FileStream(fileName, FileMode.Open));
        }

        /// <summary>
        ///     Reads a generic Pokémon container from a Stream.
        ///     Those containers are the ones that starts with "GR", "MM", "AD" and so on...
        /// </summary>
        /// <param name="data">Stream with container data</param>
        /// <returns></returns>
        public static OContainer load(Stream data)
        {
            BinaryReader input = new BinaryReader(data);
            OContainer output = null;

            string magic = IOUtils.readString(input, 0, 2); //Magic
            ushort sectionCount = input.ReadUInt16();
            output = new OContainer();

            for (int i = 0; i < sectionCount; i++)
            {
                OContainer.fileEntry entry = new OContainer.fileEntry();

                data.Seek(4 + (i * 4), SeekOrigin.Begin);
                uint startOffset = input.ReadUInt32();
                uint endOffset = input.ReadUInt32();
                uint length = endOffset - startOffset;

                data.Seek(startOffset, SeekOrigin.Begin);
                byte[] buffer = new byte[length];
                input.Read(buffer, 0, (int)length);

                bool isCompressed = buffer.Length > 0 ? buffer[0] == 0x11 : false;
                string extension = FileIO.getExtension(buffer, isCompressed ? 5 : 0);
                string name = string.Format("file_{0:D5}{1}", i, extension);

                entry.data = buffer;
                entry.name = name;

                output.content.Add(entry);
            }

            data.Close();

            return output;
        }
    }
}
