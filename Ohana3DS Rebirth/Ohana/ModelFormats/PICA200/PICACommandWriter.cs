using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace Ohana3DS_Rebirth.Ohana.ModelFormats.PICA200
{
    class PICACommandWriter : IDisposable
    {
        bool disposed = false;
        BinaryWriter writer;

        public PICACommandWriter(Stream data)
        {
            writer = new BinaryWriter(data);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) writer.Close();
            disposed = true;
        }

        public void Close()
        {
            Dispose(true);
        }

        public Stream BaseStream
        {
            get
            {
                return writer.BaseStream;
            }
        }

        /// <summary>
        ///     Sets a new command with a single UInt parameter.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameter">Parameter of the command</param>
        /// <param name="mask">Mask used when updating the register value</param>
        public void setCommand(ushort commandId, uint parameter, byte mask = 0xf)
        {
            writer.Write(parameter);
            writer.Write((uint)(commandId | (mask << 16)));
        }

        /// <summary>
        ///     Sets a new command with a single Float parameter.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameter">Parameter of the command</param>
        public void setCommand(ushort commandId, float parameter)
        {
            writer.Write(parameter);
            writer.Write((uint)(commandId | (0xf << 16)));
        }

        /// <summary>
        ///     Sets a new command with a single Color parameter.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameter">Parameter of the command</param>
        public void setCommand(ushort commandId, Color parameter)
        {
            uint rgba;
            rgba = (uint)parameter.A << 24;
            rgba |= (uint)parameter.B << 16;
            rgba |= (uint)parameter.G << 8;
            rgba |= parameter.R;
            writer.Write(rgba);
            writer.Write((uint)(commandId | (0xf << 16)));
        }

        /// <summary>
        ///     Sets a new command with multiple UInt parameters.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameters">Parameters of the command</param>
        /// <param name="mask">Mask used when updating the register value</param>
        public void setCommand(ushort commandId, List<uint> parameters, byte mask = 0xf)
        {
            if (parameters.Count == 0) return;
            writer.Write(parameters[0]);
            if (parameters.Count > 1)
            {
                uint extraWords = (uint)(((parameters.Count - 1) & 0x7ff) << 20);
                writer.Write((uint)((commandId | (uint)(mask << 16)) | extraWords));
                for (int p = 1; p < parameters.Count; p++) writer.Write(parameters[p]);
            }
            else
            {
                writer.Write((uint)(commandId | (mask << 16)));
            }
            align(7);
        }

        /// <summary>
        ///     Sets a new command with multiple Float parameters.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameters">Parameters of the command</param>
        /// <param name="mask">Mask used when updating the register value</param>
        public void setCommand(ushort commandId, List<float> parameters)
        {
            if (parameters.Count == 0) return;
            writer.Write(parameters[0]);
            if (parameters.Count > 1)
            {
                uint extraWords = (uint)(((parameters.Count - 1) & 0x7ff) << 20);
                writer.Write((uint)((commandId | (uint)(0xf << 16)) | extraWords));
                for (int p = 1; p < parameters.Count; p++) writer.Write(parameters[p]);
            }
            else
            {
                writer.Write((uint)(commandId | (0xf << 16)));
            }
            align(7);
        }

        /// <summary>
        ///     Sets a new command with multiple UInt parameters.
        ///     It uses consecutive mode, which means that the command ID will be incremented after each parameter.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameters">Parameters of the command</param>
        /// <param name="mask">Mask used when updating the register value</param>
        public void setCommandConsecutive(ushort commandId, List<uint> parameters, byte mask = 0xf)
        {
            if (parameters.Count == 0) return;
            writer.Write(parameters[0]);
            if (parameters.Count > 1)
            {
                uint extraWords = (uint)(((parameters.Count - 1) & 0x7ff) << 20);
                writer.Write((uint)((commandId | (uint)(mask << 16)) | extraWords) | 0x80000000);
                for (int p = 1; p < parameters.Count; p++) writer.Write(parameters[p]);
            }
            else
            {
                writer.Write((uint)(commandId | (mask << 16)) | 0x80000000);
            }
            align(7);
        }

        /// <summary>
        ///     Sets a new command with multiple Float parameters.
        ///     It uses consecutive mode, which means that the command ID will be incremented after each parameter.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameters">Parameters of the command</param>
        public void setCommandConsecutive(ushort commandId, List<float> parameters)
        {
            if (parameters.Count == 0) return;
            writer.Write(parameters[0]);
            if (parameters.Count > 1)
            {
                uint extraWords = (uint)(((parameters.Count - 1) & 0x7ff) << 20);
                writer.Write((uint)((commandId | (uint)(0xf << 16)) | extraWords) | 0x80000000);
                for (int p = 1; p < parameters.Count; p++) writer.Write(parameters[p]);
            }
            else
            {
                writer.Write((uint)(commandId | (0xf << 16)) | 0x80000000);
            }
            align(7);
        }

        /// <summary>
        ///     Sets a new command with one UInt parameter and multiple Float parameters.
        ///     It uses consecutive mode, which means that the command ID will be incremented after each parameter.
        /// </summary>
        /// <param name="commandId">ID of the command</param>
        /// <param name="parameter">UInt parameter of the command</param>
        /// <param name="parameters">Extra Float parameters of the command</param>
        public void setCommandConsecutive(ushort commandId, uint parameter, List<float> parameters)
        {
            writer.Write(parameter);
            if (parameters.Count > 0)
            {
                uint extraWords = (uint)((parameters.Count & 0x7ff) << 20);
                writer.Write((uint)((commandId | (uint)(0xf << 16)) | extraWords) | 0x80000000);
                for (int p = 1; p < parameters.Count; p++) writer.Write(parameters[p]);
            }
            else
            {
                writer.Write((uint)(commandId | (0xf << 16)) | 0x80000000);
            }
            align(7);
        }

        /// <summary>
        ///     Adds padding 0x0 bytes on the data until all address bits of the mask equals 0.
        /// </summary>
        /// <param name="mask">The mask</param>
        private void align(uint mask)
        {
            while ((writer.BaseStream.Position & mask) != 0) writer.Write((byte)0);
        }

        /// <summary>
        ///     Adds padding 0x0 bytes on the data until all address bits of the mask equals the specified value.
        /// </summary>
        /// <param name="mask">The mask</param>
        /// <param name="maskedValue">Value that masked bits should equal to stop padding</param>
        private void align(uint mask, uint maskedValue)
        {
            while ((writer.BaseStream.Position & mask) != maskedValue) writer.Write((byte)0);
        }
    }
}
