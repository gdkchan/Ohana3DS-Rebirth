using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ohana3DS_Rebirth.Ohana.PICA200
{
    class PICACommand
    {
        public const ushort texUnitsConfig = 0x80;
        public const ushort texUnit0BorderColor = 0x81;
        public const ushort texUnit0Size = 0x82;
        public const ushort texUnit0Param = 0x83;
        public const ushort texUnit0Address = 0x85;
        public const ushort texUnit0Type = 0x8e;
        public const ushort texUnit1BorderColor = 0x91;
        public const ushort texUnit1Size = 0x92;
        public const ushort texUnit1Param = 0x93;
        public const ushort texUnit1Address = 0x95;
        public const ushort texUnit1Type = 0x96;
        public const ushort texUnit2BorderColor = 0x99;
        public const ushort texUnit2Size = 0x9a;
        public const ushort texUnit2Param = 0x9b;
        public const ushort texUnit2Address = 0x9d;
        public const ushort texUnit2Type = 0x9e;
        public const ushort tevStage0Config0 = 0xc0;
        public const ushort tevStage0Config1 = 0xc1;
        public const ushort tevStage0Config2 = 0xc2;
        public const ushort tevStage0Config3 = 0xc3;
        public const ushort tevStage0Config4 = 0xc4;
        public const ushort tevStage1Config0 = 0xc8;
        public const ushort tevStage1Config1 = 0xc9;
        public const ushort tevStage1Config2 = 0xca;
        public const ushort tevStage1Config3 = 0xcb;
        public const ushort tevStage1Config4 = 0xcc;
        public const ushort tevStage2Config0 = 0xd0;
        public const ushort tevStage2Config1 = 0xd1;
        public const ushort tevStage2Config2 = 0xd2;
        public const ushort tevStage2Config3 = 0xd3;
        public const ushort tevStage2Config4 = 0xd4;
        public const ushort tevStage3Config0 = 0xd8;
        public const ushort tevStage3Config1 = 0xd9;
        public const ushort tevStage3Config2 = 0xda;
        public const ushort tevStage3Config3 = 0xdb;
        public const ushort tevStage3Config4 = 0xdc;
        public const ushort tevStage4Config0 = 0xf0;
        public const ushort tevStage4Config1 = 0xf1;
        public const ushort tevStage4Config2 = 0xf2;
        public const ushort tevStage4Config3 = 0xf3;
        public const ushort tevStage4Config4 = 0xf4;
        public const ushort tevStage5Config0 = 0xf8;
        public const ushort tevStage5Config1 = 0xf9;
        public const ushort tevStage5Config2 = 0xfa;
        public const ushort tevStage5Config3 = 0xfb;
        public const ushort tevStage5Config4 = 0xfc;
        public const ushort fragmentBufferColor = 0xfd;
        public const ushort colorOutputConfig = 0x100;
        public const ushort blendConfig = 0x101;
        public const ushort colorLogicOperationConfig = 0x102;
        public const ushort blendColor = 0x103;
        public const ushort alphaTestConfig = 0x104;
        public const ushort stencilTestConfig = 0x105;
        public const ushort stencilOperationConfig = 0x106;
        public const ushort depthTestConfig = 0x107;
        public const ushort rasterizationConfig = 0x108;
        public const ushort fragmentShaderLookUpTableConfig = 0x1c5;
        public const ushort fragmentShaderLookUpTableData = 0x1c8;
        public const ushort reflectanceSamplerInput = 0x1d1;
        public const ushort reflectanceSamplerScale = 0x1d2;
        public const ushort vertexShaderAttributesBufferAddress = 0x200;
        public const ushort vertexShaderAttributesBufferFormatLow = 0x201;
        public const ushort vertexShaderAttributesBufferFormatHigh = 0x202;
        public const ushort vertexShaderAttributesBuffer0Address = 0x203;
        public const ushort vertexShaderAttributesBuffer0Permutation = 0x204;
        public const ushort vertexShaderAttributesBuffer0Stride = 0x205;
        public const ushort vertexShaderAttributesBuffer1Address = 0x206;
        public const ushort vertexShaderAttributesBuffer1Permutation = 0x207;
        public const ushort vertexShaderAttributesBuffer1Stride = 0x208;
        public const ushort vertexShaderAttributesBuffer2Address = 0x209;
        public const ushort vertexShaderAttributesBuffer2Permutation = 0x20a;
        public const ushort vertexShaderAttributesBuffer2Stride = 0x20b;
        public const ushort vertexShaderAttributesBuffer3Address = 0x20c;
        public const ushort vertexShaderAttributesBuffer3Permutation = 0x20d;
        public const ushort vertexShaderAttributesBuffer3Stride = 0x20e;
        public const ushort vertexShaderAttributesBuffer4Address = 0x20f;
        public const ushort vertexShaderAttributesBuffer4Permutation = 0x210;
        public const ushort vertexShaderAttributesBuffer4Stride = 0x211;
        public const ushort vertexShaderAttributesBuffer5Address = 0x212;
        public const ushort vertexShaderAttributesBuffer5Permutation = 0x213;
        public const ushort vertexShaderAttributesBuffer5Stride = 0x214;
        public const ushort vertexShaderAttributesBuffer6Address = 0x215;
        public const ushort vertexShaderAttributesBuffer6Permutation = 0x216;
        public const ushort vertexShaderAttributesBuffer6Stride = 0x217;
        public const ushort vertexShaderAttributesBuffer7Address = 0x218;
        public const ushort vertexShaderAttributesBuffer7Permutation = 0x219;
        public const ushort vertexShaderAttributesBuffer7Stride = 0x21a;
        public const ushort vertexShaderAttributesBuffer8Address = 0x21b;
        public const ushort vertexShaderAttributesBuffer8Permutation = 0x21c;
        public const ushort vertexShaderAttributesBuffer8Stride = 0x21d;
        public const ushort vertexShaderAttributesBuffer9Address = 0x21e;
        public const ushort vertexShaderAttributesBuffer9Permutation = 0x21f;
        public const ushort vertexShaderAttributesBuffer9Stride = 0x220;
        public const ushort vertexShaderAttributesBuffer10Address = 0x221;
        public const ushort vertexShaderAttributesBuffer10Permutation = 0x222;
        public const ushort vertexShaderAttributesBuffer10Stride = 0x223;
        public const ushort vertexShaderAttributesBuffer11Address = 0x224;
        public const ushort vertexShaderAttributesBuffer11Permutation = 0x225;
        public const ushort vertexShaderAttributesBuffer11Stride = 0x226;
        public const ushort indexBufferConfig = 0x227;
        public const ushort indexBufferTotalVertices = 0x228;
        public const ushort blockEnd = 0x23d;
        public const ushort vertexShaderTotalAttributes = 0x242;
        public const ushort vertexShaderBooleanUniforms = 0x2b0;
        public const ushort vertexShaderInputBufferConfig = 0x2b9;
        public const ushort vertexShaderEntryPoint = 0x2ba;
        public const ushort vertexShaderAttributesPermutationLow = 0x2bb;
        public const ushort vertexShaderAttributesPermutationHigh = 0x2bc;
        public const ushort vertexShaderOutmapMask = 0x2bd;
        public const ushort vertexShaderCodeTransferEnd = 0x2bf;
        public const ushort vertexShaderFloatUniformConfig = 0x2c0;
        public const ushort vertexShaderFloatUniformData = 0x2c1;

        public enum vshAttribute
        {
            position = 0,
            normal = 1,
            tangent = 2,
            color = 3,
            textureCoordinate0 = 4,
            textureCoordinate1 = 5,
            textureCoordinate2 = 6,
            boneIndex = 7,
            boneWeight = 8,
            userAttribute0 = 9,
            userAttribute1 = 0xa,
            userAttribute2 = 0xb,
            userAttribute3 = 0xc,
            userAttribute4 = 0xd,
            userAttribute5 = 0xe,
            userAttribute6 = 0xf,
            userAttribute7 = 0x10,
            userAttribute8 = 0x11,
            userAttribute9 = 0x12,
            userAttribute10 = 0x13,
            userAttribute11 = 0x14,
            interleave = 0x15,
            quantity = 0x16
        }

        public enum attributeFormatType
        {
            signedByte = 0,
            unsignedByte = 1,
            signedShort = 2,
            single = 3
        }

        public struct attributeFormat
        {
            public attributeFormatType type;
            public uint attributeLength;
        }

        public enum indexBufferFormat
        {
            unsignedByte = 0,
            unsignedShort = 1
        }

        public class fragmentSamplerInput
        {
            public RenderBase.OFragmentSamplerInput r, g, b;
            public RenderBase.OFragmentSamplerInput d0, d1;
            public RenderBase.OFragmentSamplerInput fresnel;

            public fragmentSamplerInput()
            {
                r = new RenderBase.OFragmentSamplerInput();
                g = new RenderBase.OFragmentSamplerInput();
                b = new RenderBase.OFragmentSamplerInput();
                d0 = new RenderBase.OFragmentSamplerInput();
                d1 = new RenderBase.OFragmentSamplerInput();
                fresnel = new RenderBase.OFragmentSamplerInput();
            }
        }

        public class fragmentSamplerScale
        {
            public RenderBase.OFragmentSamplerScale r, g, b;
            public RenderBase.OFragmentSamplerScale d0, d1;
            public RenderBase.OFragmentSamplerScale fresnel;

            public fragmentSamplerScale()
            {
                r = new RenderBase.OFragmentSamplerScale();
                g = new RenderBase.OFragmentSamplerScale();
                b = new RenderBase.OFragmentSamplerScale();
                d0 = new RenderBase.OFragmentSamplerScale();
                d1 = new RenderBase.OFragmentSamplerScale();
                fresnel = new RenderBase.OFragmentSamplerScale();
            }
        }
    }
}
