using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ohana3DS_Rebirth.Ohana
{
    class CompressionManager
    {
        /// <summary>
        ///     Decompress a file and automaticaly updates the format of the compressed file.
        /// </summary>
        /// <param name="data">Stream with the file to be decompressed</param>
        /// <param name="format">Format of the compression</param>
        /// <returns></returns>
        public static void decompress(ref Stream data, ref FileIdentifier.fileFormat format)
        {
            BinaryReader input = new BinaryReader(data);
            byte[] decompressedData, content;
            uint length;

            switch (format)
            {
                case FileIdentifier.fileFormat.BLZCompressed:
                    decompressedData = Ohana.Compressions.BLZ.decompress(data);
                    content = new byte[decompressedData.Length - 1];
                    Buffer.BlockCopy(decompressedData, 1, content, 0, content.Length);
                    data = new MemoryStream(content);
                    format = FileIdentifier.identify(data);
                    break;
                case FileIdentifier.fileFormat.LZSSCompressed:
                case FileIdentifier.fileFormat.LZSSHeaderCompressed:
                    if (format == FileIdentifier.fileFormat.LZSSHeaderCompressed) input.ReadUInt32();
                    length = input.ReadUInt32() >> 8;
                    decompressedData = Ohana.Compressions.LZSS_Ninty.decompress(data, length);
                    data = new MemoryStream(decompressedData);
                    format = FileIdentifier.identify(data);
                    break;
                case FileIdentifier.fileFormat.IECPCompressed: //Stella glow
                    input.ReadUInt32(); //Magic
                    length = input.ReadUInt32();
                    decompressedData = Ohana.Compressions.LZSS.decompress(data, length);
                    data = new MemoryStream(decompressedData);
                    format = FileIdentifier.identify(data);
                    break;
            }
        }
    }
}
