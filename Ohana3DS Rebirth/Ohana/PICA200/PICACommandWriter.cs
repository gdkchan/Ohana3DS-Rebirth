using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Ohana3DS_Rebirth.Ohana.PICA200
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

        /// <summary>
        ///     Sets basic data needed to render a Vertex Stream on the PICA200.
        ///     Note that it set pretty standard commands, so be sure to follow them when storing the vertices.
        /// </summary>
        /// <param name="address">Relative address where the vertex data is located</param>
        /// <param name="totalAttributes">Total number of attributes per vertex</param>
        /// <param name="usedAttributes">Bool list that tells if an attribute is present or not (order: pos, norm, tan, col, tex0, tex1, tex2, node, weight, ...)</param>
        public void setVSHAttributesCommands(uint address, List<bool> usedAttributes)
        {
            uint stride = 0;
            ulong attBufferPermutation = 0;
            ulong attBuffer0Permutation = 0;
            uint attBufferFormat = 0;
            uint totalAttributes = 0;
            int j = 0;
            for (int i = 0; i < usedAttributes.Count; i++)
            {
                if (usedAttributes[i])
                {
                    int k = j * 4;
                    attBufferPermutation |= (uint)(i << k);
                    attBuffer0Permutation |= (uint)(j << k);
                    j++;

                    //Set the quantization of the attribute data
                    if (i == 3 || i > 6)
                        attBufferFormat |= (uint)PICACommand.attributeFormatType.unsignedByte << k;
                    else
                        attBufferFormat |= (uint)PICACommand.attributeFormatType.single << k;

                    //Set the number of elements on the attribute
                    if (i == 3 || i > 6)
                    { attBufferFormat |= (uint)(3 << (k + 2)); stride += 4; } //Vector 4
                    else if (i < 3)
                    { attBufferFormat |= (uint)(2 << (k + 2)); stride += 12; } //Vector 3
                    else
                    { attBufferFormat |= (uint)(1 << (k + 2)); stride += 8; } //Vector 2

                    totalAttributes++;
                }
            }

            setCommand(PICACommand.vertexShaderInputBufferConfig, 0xa0000000 | (totalAttributes - 1), 0xb);
            setCommand(PICACommand.vertexShaderTotalAttributes, totalAttributes - 1, 1);
            setCommand(PICACommand.vertexShaderAttributesPermutationLow, (uint)(attBufferPermutation & 0xffffffff));
            setCommand(PICACommand.vertexShaderAttributesPermutationHigh, (uint)(attBufferPermutation >> 32));
            setCommandConsecutive(PICACommand.vertexShaderAttributesBufferAddress,
                new List<uint> {0,
                    (uint)(attBufferFormat & 0xffffffff),
                    (uint)(attBufferFormat >> 32),
                    address,
                    (uint)(attBuffer0Permutation & 0xffffffff),
                    (uint)(((attBuffer0Permutation >> 32) & 0xffff) | (stride << 16) | (totalAttributes << 28))});
            setCommandConsecutive(PICACommand.vertexShaderFloatUniformConfig, 0x80000006, new List<float> { 0, 0, 0, 0 }); //Position attribute displacement
            setCommand(PICACommand.vertexShaderFloatUniformConfig, 0x80000007);
            float b = 1f / byte.MaxValue; //For attributes stored as byte, transform from 0-255 to 0-1 when multiplied
            setCommand(PICACommand.vertexShaderFloatUniformData, new List<float> { b, 1f, 1f, 1f, b, 1f, 1f, 1f }); //Attributes scale
            align(0xf, 8);
            setCommand(PICACommand.blockEnd, 1);
        }

        /// <summary>
        ///     Sets commands for extra Attributes Buffer.
        ///     This method will set everything to zero, and is better suited if you only use the first Attributes Buffer.
        /// </summary>
        public void setVSHExtraAttributesCommands()
        {
            setCommandConsecutive(PICACommand.vertexShaderAttributesBuffer0Address, new List<uint> { 0, 0, 0 });
            for (int i = 0; i < 11; i++) setCommand((ushort)(PICACommand.vertexShaderAttributesBuffer1Stride + (i * 3)), 0);
            align(0xf, 8);
            setCommand(PICACommand.blockEnd, 1);
        }

        /// <summary>
        ///     Sets basic commands to setup a simple Index Stream.
        ///     The format of the buffer (byte or short) is specified during the relocation process.
        ///     Otherwise the default format will be byte.
        /// </summary>
        /// <param name="address">Relative address where Index Stream data is located</param>
        /// <param name="count">Number of vertices indexed by the stream</param>
        /// <param name="booleanUniforms">Boolean uniforms used on Vertex Shader</param>
        public void setIndexCommands(uint address, uint count, ushort booleanUniforms)
        {
            setCommand(PICACommand.vertexShaderBooleanUniforms, (uint)0x7fff0000 | booleanUniforms);
            setCommand(0x25f, 1);
            setCommand(PICACommand.indexBufferConfig, address & 0x7fffffff); //Note: It also have the format on the most heavy bit
            setCommand(PICACommand.indexBufferTotalVertices, count);
            setCommand(0x245, 0, 1);
            setCommand(0x22f, 1);
            setCommand(0x245, 0, 1);
            setCommand(0x231, 0);
            setCommand(0x25e, 0, 8);
            setCommand(0x25e, 0, 8);
            align(0xf, 8);
            setCommand(PICACommand.blockEnd, 1);
        }

        /// <summary>
        ///     Set general commands used on Fragment Shader, like TevStages and such.
        /// </summary>
        /// <param name="fragmentShader">Fragment shader parameters</param>
        /// <param name="fragmentOperation">Fragment operation parameters</param>
        /// <param name="rasterization">Rasterization parameters</param>
        public void setFSHCommands(RenderBase.OFragmentShader fragmentShader, RenderBase.OFragmentOperation fragmentOperation, RenderBase.ORasterization rasterization)
        {
            //Combiner
            for (int stage = 0; stage < 6; stage++)
            {
                RenderBase.OTextureCombiner combiner = fragmentShader.textureCombiner[stage];

                ushort baseCommand = 0;
                switch (stage)
                {
                    case 0: baseCommand = PICACommand.tevStage0Source; break;
                    case 1: baseCommand = PICACommand.tevStage1Source; break;
                    case 2: baseCommand = PICACommand.tevStage2Source; break;
                    case 3: baseCommand = PICACommand.tevStage3Source; break;
                    case 4: baseCommand = PICACommand.tevStage4Source; break;
                    case 5: baseCommand = PICACommand.tevStage5Source; break;
                    default: throw new Exception("PICACommandWriter: Invalid TevStage number!");
                }

                uint source;
                source = (uint)combiner.rgbSource[0] & 0xf; //Color
                source |= ((uint)combiner.rgbSource[1] & 0xf) << 4;
                source |= ((uint)combiner.rgbSource[2] & 0xf) << 8;

                source |= ((uint)combiner.alphaSource[0] & 0xf) << 16; //Alpha
                source |= ((uint)combiner.alphaSource[1] & 0xf) << 20;
                source |= ((uint)combiner.alphaSource[2] & 0xf) << 24;

                uint operand;
                operand = (uint)combiner.rgbOperand[0] & 0xf; //Color
                operand |= ((uint)combiner.rgbOperand[1] & 0xf) << 4;
                operand |= ((uint)combiner.rgbOperand[2] & 0xf) << 8;

                operand |= ((uint)combiner.alphaOperand[0] & 0xf) << 12; //Alpha
                operand |= ((uint)combiner.alphaOperand[1] & 0xf) << 16;
                operand |= ((uint)combiner.alphaOperand[2] & 0xf) << 20;

                uint combine = (uint)(((ushort)combiner.combineAlpha << 16) | (ushort)combiner.combineRgb);
                uint scale = (uint)((((int)combiner.alphaScale - 1) << 16) | ((int)combiner.rgbScale - 1));

                setCommandConsecutive(baseCommand, new List<uint> { source, operand, combine, 0, scale });
            }

            //Fragment Buffer color
            setCommand(PICACommand.fragmentBufferColor, fragmentShader.bufferColor);

            //ZBuffer depth testing
            uint depthTest = 0;
            if (fragmentOperation.depth.isTestEnabled) depthTest = 1;
            depthTest |= ((uint)fragmentOperation.depth.testFunction & 0xf) << 4;
            if (fragmentOperation.depth.isMaskEnabled) depthTest |= 0x1000;
            setCommand(PICACommand.depthTestConfig, depthTest);

            //Alpha blending
            uint blend;
            blend = ((uint)fragmentOperation.blend.rgbFunctionSource & 0xf) << 16;
            blend |= ((uint)fragmentOperation.blend.rgbFunctionDestination & 0xf) << 20;
            blend |= ((uint)fragmentOperation.blend.alphaFunctionSource & 0xf) << 24;
            blend |= ((uint)fragmentOperation.blend.alphaFunctionDestination & 0xf) << 28;
            blend |= (uint)fragmentOperation.blend.rgbBlendEquation & 0xff;
            blend |= ((uint)fragmentOperation.blend.alphaBlendEquation & 0xff) << 8;
            setCommand(PICACommand.blendConfig, blend);

            //Logical operations
            uint logicalOperation;
            logicalOperation = (uint)fragmentOperation.blend.logicalOperation & 0xf;
            setCommand(PICACommand.colorLogicOperationConfig, logicalOperation);

            //Alpha test
            uint alphaTest = 0;
            if (fragmentShader.alphaTest.isTestEnabled) alphaTest = 1;
            alphaTest |= ((uint)fragmentShader.alphaTest.testFunction & 0xf) << 4;
            alphaTest |= ((uint)fragmentShader.alphaTest.testReference & 0xff) << 8;
            setCommand(PICACommand.alphaTestConfig, alphaTest);

            //Stencil test
            uint stencilTest = 0;
            if (fragmentOperation.stencil.isTestEnabled) stencilTest = 1;
            stencilTest |= ((uint)fragmentOperation.stencil.testFunction & 0xf) << 4;
            stencilTest |= ((uint)fragmentOperation.stencil.testReference & 0xff) << 16;
            stencilTest |= ((uint)fragmentOperation.stencil.testMask & 0xff) << 24;
            setCommand(PICACommand.stencilTestConfig, stencilTest);

            //Stencil operation
            uint stencilOperation;
            stencilOperation = (uint)fragmentOperation.stencil.failOperation & 0xf;
            stencilOperation |= ((uint)fragmentOperation.stencil.zFailOperation & 0xf) << 4;
            stencilOperation |= ((uint)fragmentOperation.stencil.passOperation & 0xf) << 8;
            setCommand(PICACommand.stencilOperationConfig, stencilOperation);

            //Rasterization
            setCommand(PICACommand.cullModeConfig, (uint)rasterization.cullMode & 0xf);

            align(0xf, 8);
            setCommand(PICACommand.blockEnd, 1);
        }
    }
}
