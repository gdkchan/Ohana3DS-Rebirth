//BCH Importer/Exporter
//Note: Still need to figure out a LOT of stuff, and a bunch of things before the bare-bones model can be rendered
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Ohana3DS_Rebirth.Ohana
{
    class BCH
    {
        private struct bchHeader
        {
            public string magic;
            public byte backwardCompatibility;
            public byte forwardCompatibility;
            public ushort version;

            public uint mainHeaderOffset;
            public uint stringTableOffset;
            public uint descriptionOffset;
            public uint dataOffset;
            public uint dataExtendedOffset;
            public uint relocatableTableOffset;

            public uint mainHeaderLength;
            public uint stringTableLength;
            public uint descriptionLength;
            public uint dataLength;
            public uint dataExtendedLength;
            public uint relocatableTableLength;
            public uint uninitializedDataSectionLength;
            public uint uninitializedDescriptionSectionLength;

            public ushort flags;
            public ushort addressCount;
        }

        private struct bchContentHeader
        {
            public uint modelsPointerTableOffset;
            public uint modelsPointerTableEntries;
            public uint modelsNameOffset;
            public uint materialsPointerTableOffset;
            public uint materialsPointerTableEntries;
            public uint materialsNameOffset;
            public uint shadersPointerTableOffset;
            public uint shadersPointerTableEntries;
            public uint shadersNameOffset;
            public uint texturesPointerTableOffset;
            public uint texturesPointerTableEntries;
            public uint texturesNameOffset;
            public uint texturesPointerTable2Offset;
            public uint texturesPointerTable2Entries;
            public uint texturesName2Offset;
        }

        private struct bchObjectsHeader
        {
            public byte flags;
            public byte skeletonScalingType;
            public ushort shadowMaterialEntries;
            public RenderBase.OMatrix worldTransform;

            public uint texturesTableOffset;
            public uint texturesTableEntries;
            public uint objectsNameOffset;
            public uint verticesTableOffset;
            public uint verticesTableEntries;
            public uint skeletonOffset;
            public uint skeletonEntries;
            public uint skeletonNameOffset;
            public uint facesNameFlagsOffset;
            public uint facesNameFlagsBits;
            public String modelName;
            public uint facesNameEntries;
            public uint facesNameOffset;
            public uint boundingBoxAndMeasuresPointerOffset;
        }

        private struct bchObjectEntry
        {
            public ushort materialId;
            public ushort renderPriority;
            public uint verticesHeaderOffset;
            public uint verticesHeaderEntries;
            public uint facesHeaderOffset;
            public uint facesHeaderEntries;
            public uint verticesHeader2Offset;
            public uint verticesHeader2Entries;
            public RenderBase.OVector3 centerVector;
            public uint flagsOffset;
            public uint boundingBoxOffset;

            //Dados das sub-tabelas
        }

        const uint codeMaterialUnknow1 = 0x000f01d0;
        const uint codeMaterialUnknow2 = 0x000f01d1;
        const uint codeMaterialConstantColor = 0x000f01d2;

        const uint codeTextureCombineOperand = 0x804f00c0;
        const uint codeTextureCombineUnknow1 = 0x804f00c8;
        const uint codeTextureCombineUnknow2 = 0x804f00d0;
        const uint codeTextureCombineUnknow3 = 0x804f00d8;
        const uint codeTextureCombineUnknow4 = 0x804f00f0;
        const uint codeTextureCombineUnknow5 = 0x804f00f8;
        //TODO: Lots of codes missing here!

        const uint codeTextureDataOffset = 0x000f0082;
        const uint codeTextureDataOffsetExtension = 0x00040084;
        const uint codeTextureFormatId = 0x000f0085;
        const uint codeTextureUnknow1 = 0x000f008e;

        const uint codeFaceUnknow1 = 0x000f02b0;
        const uint codeFaceDataOffset = 0x000f025f;
        const uint codeFaceDataLength = 0x000f0227;
        const uint codeFaceUnknow2 = 0x000f0228;
        const uint codeFaceUnknow3 = 0x00010245;
        const uint codeFaceUnknow4 = 0x000f022f;
        const uint codeFaceUnknow5 = 0x000f0231;
        const uint codeFaceUnknow6 = 0x0008025e;

        const uint codeVerticeVectorsPerEntry = 0x000b02b9;
        const uint codeVerticeVectorOrder = 0x00010242;
        const uint codeVerticeUnknow1 = 0x000f02bb;
        const uint codeVerticeUnknow2 = 0x000f02bc;
        const uint codeVerticeHeaderData = 0x805f0200;
        const uint codeVerticePositionOffset = 0x804f02c0;
        const uint codeVerticeUnknow3 = 0x000f02c0;
        const uint codeVerticeScale = 0x007f02c1;

        const uint codeBlockEnd = 0x000f023d;

        const byte quantizationByte = 0;
        const byte quantizationUByte = 1;
        const byte quantizationShort = 2;
        const byte quantizationFloat = 3;

        const byte vectorTypeNone = 0;
        const byte vectorTypeVector2 = 1;
        const byte vectorTypeVector3 = 2;
        const byte vectorTypeVector4 = 3;

        #region "Import"
        public static RenderBase.OModelGroup load(string fileName)
        {
            FileStream data = new FileStream(fileName, FileMode.Open);
            BinaryReader input = new BinaryReader(data);

            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            //Primary header
            bchHeader header = new bchHeader();
            header.magic = IOUtils.readString(input, 0);
            data.Seek(4, SeekOrigin.Current);
            header.backwardCompatibility = input.ReadByte();
            header.forwardCompatibility = input.ReadByte();
            header.version = input.ReadUInt16();

            header.mainHeaderOffset = input.ReadUInt32();
            header.stringTableOffset = input.ReadUInt32();
            header.descriptionOffset = input.ReadUInt32();
            header.dataOffset = input.ReadUInt32();
            header.dataExtendedOffset = input.ReadUInt32();
            header.relocatableTableOffset = input.ReadUInt32();

            header.mainHeaderLength = input.ReadUInt32();
            header.stringTableLength = input.ReadUInt32();
            header.descriptionLength = input.ReadUInt32();
            header.dataLength = input.ReadUInt32();
            header.dataExtendedLength = input.ReadUInt32();
            header.relocatableTableLength = input.ReadUInt32();
            header.uninitializedDataSectionLength = input.ReadUInt32();
            header.uninitializedDescriptionSectionLength = input.ReadUInt32();

            header.flags = input.ReadUInt16();
            header.addressCount = input.ReadUInt16();

            //Content header
            data.Seek(header.mainHeaderOffset, SeekOrigin.Begin);
            bchContentHeader contentHeader = new bchContentHeader();
            contentHeader.modelsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.modelsPointerTableEntries = input.ReadUInt32();
            contentHeader.modelsNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.materialsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.materialsPointerTableEntries = input.ReadUInt32();
            contentHeader.materialsNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.shadersPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.shadersPointerTableEntries = input.ReadUInt32();
            contentHeader.shadersNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.texturesPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.texturesPointerTableEntries = input.ReadUInt32();
            contentHeader.texturesNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.texturesPointerTable2Offset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.texturesPointerTable2Entries = input.ReadUInt32();
            contentHeader.texturesName2Offset = input.ReadUInt32() + header.mainHeaderOffset;
            //Note: 15 entries total, all have the same pattern: Table Offset/Table Entries/Name Offset

            //Materials
            for (int index = 0; index < contentHeader.materialsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.materialsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                UInt32 dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                uint blendFlags = input.ReadUInt32();
                uint unknow = input.ReadUInt32();

                data.Seek(0x50, SeekOrigin.Current);
                RenderBase.OMaterial material = new RenderBase.OMaterial();
                material.emission = RenderBase.getMaterialColor(input);
                material.ambient = RenderBase.getMaterialColor(input);
                material.diffuse = RenderBase.getMaterialColor(input);
                material.specular0 = RenderBase.getMaterialColor(input);
                material.specular1 = RenderBase.getMaterialColor(input);
                material.constant0 = RenderBase.getMaterialColor(input);
                material.constant1 = RenderBase.getMaterialColor(input);
                material.constant2 = RenderBase.getMaterialColor(input);
                material.constant3 = RenderBase.getMaterialColor(input);
                material.constant4 = RenderBase.getMaterialColor(input);
                material.constant5 = RenderBase.getMaterialColor(input);

                models.addMaterial(material);
            }

            //Shaders
            for (int index = 0; index < contentHeader.shadersPointerTableEntries; index++)
            {
                data.Seek(contentHeader.shadersPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                uint shaderDataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint shaderDataLength = input.ReadUInt32();
            }

            //Textures
            for (int index = 0; index < contentHeader.texturesPointerTableEntries; index++)
            {
                data.Seek(contentHeader.texturesPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                uint textureHeaderOffset = input.ReadUInt32() + header.descriptionOffset;
                uint textureHeaderEntries = input.ReadUInt32();
                data.Seek(0x14, SeekOrigin.Current);
                String textureName = IOUtils.readString(input, input.ReadUInt32() + header.stringTableOffset);

                data.Seek(textureHeaderOffset, SeekOrigin.Begin);
                ushort textureHeight = input.ReadUInt16();
                ushort textureWidth = input.ReadUInt16();
                uint textureDataOffset = 0;
                uint textureFormatId = 0;
                for (int entry = 0; entry < textureHeaderEntries; entry++)
                {
                    uint code = input.ReadUInt32();
                    uint value = input.ReadUInt32();
                    entry++;

                    switch (code)
                    {
                        case codeTextureDataOffset: case codeTextureDataOffsetExtension: textureDataOffset = value + header.dataOffset; break;
                        case codeTextureFormatId: textureFormatId = value; break;
                        case codeBlockEnd: entry = (int)textureHeaderEntries; break;
                        default: input.ReadUInt32(); entry++; break;
                    }
                }

                Bitmap texture = null;
                #region "Texture decoding"
                data.Seek(textureDataOffset, SeekOrigin.Begin);
                switch (textureFormatId)
                {
                    case 0:
                        byte[] bufferRgba8 = new byte[textureWidth * textureHeight * 4];
                        input.Read(bufferRgba8, 0, bufferRgba8.Length);
                        texture = TextureCodec.decode(bufferRgba8, textureWidth, textureHeight, TextureCodec.textureFormat.rgba8);
                        break;

                    case 1:
                        byte[] bufferRgb8 = new byte[textureWidth * textureHeight * 3];
                        input.Read(bufferRgb8, 0, bufferRgb8.Length);
                        texture = TextureCodec.decode(bufferRgb8, textureWidth, textureHeight, TextureCodec.textureFormat.rgb8);
                        break;

                    case 2:
                        byte[] bufferRgba5551 = new byte[textureWidth * textureHeight * 2];
                        input.Read(bufferRgba5551, 0, bufferRgba5551.Length);
                        texture = TextureCodec.decode(bufferRgba5551, textureWidth, textureHeight, TextureCodec.textureFormat.rgba5551);
                        break;

                    case 3:
                        byte[] bufferRgb565 = new byte[textureWidth * textureHeight * 2];
                        input.Read(bufferRgb565, 0, bufferRgb565.Length);
                        texture = TextureCodec.decode(bufferRgb565, textureWidth, textureHeight, TextureCodec.textureFormat.rgb565);
                        break;

                    case 4:
                        byte[] bufferRgba4 = new byte[textureWidth * textureHeight * 2];
                        input.Read(bufferRgba4, 0, bufferRgba4.Length);
                        texture = TextureCodec.decode(bufferRgba4, textureWidth, textureHeight, TextureCodec.textureFormat.rgba4);
                        break;

                    case 5:
                        byte[] bufferLa8 = new byte[textureWidth * textureHeight * 2];
                        input.Read(bufferLa8, 0, bufferLa8.Length);
                        texture = TextureCodec.decode(bufferLa8, textureWidth, textureHeight, TextureCodec.textureFormat.la8);
                        break;

                    case 6:
                        byte[] bufferHilo8 = new byte[textureWidth * textureHeight * 2];
                        input.Read(bufferHilo8, 0, bufferHilo8.Length);
                        texture = TextureCodec.decode(bufferHilo8, textureWidth, textureHeight, TextureCodec.textureFormat.hilo8);
                        break;

                    case 7:
                        byte[] bufferL8 = new byte[textureWidth * textureHeight];
                        input.Read(bufferL8, 0, bufferL8.Length);
                        texture = TextureCodec.decode(bufferL8, textureWidth, textureHeight, TextureCodec.textureFormat.l8);
                        break;

                    case 8:
                        byte[] bufferA8 = new byte[textureWidth * textureHeight];
                        input.Read(bufferA8, 0, bufferA8.Length);
                        texture = TextureCodec.decode(bufferA8, textureWidth, textureHeight, TextureCodec.textureFormat.a8);
                        break;

                    case 9:
                        byte[] bufferLA4 = new byte[textureWidth * textureHeight];
                        input.Read(bufferLA4, 0, bufferLA4.Length);
                        texture = TextureCodec.decode(bufferLA4, textureWidth, textureHeight, TextureCodec.textureFormat.la4);
                        break;

                    case 0xa:
                        byte[] bufferL4 = new byte[(textureWidth * textureHeight) / 2];
                        input.Read(bufferL4, 0, bufferL4.Length);
                        texture = TextureCodec.decode(bufferL4, textureWidth, textureHeight, TextureCodec.textureFormat.l4);
                        break;

                    case 0xb:
                        byte[] bufferA4 = new byte[(textureWidth * textureHeight) / 2];
                        input.Read(bufferA4, 0, bufferA4.Length);
                        texture = TextureCodec.decode(bufferA4, textureWidth, textureHeight, TextureCodec.textureFormat.a4);
                        break;

                    case 0xc:
                        byte[] bufferEtc1 = new byte[(textureWidth * textureHeight) / 2];
                        input.Read(bufferEtc1, 0, bufferEtc1.Length);
                        texture = TextureCodec.decode(bufferEtc1, textureWidth, textureHeight, TextureCodec.textureFormat.etc1);
                        break;

                    case 0xd:
                        byte[] bufferEtc1a4 = new byte[textureWidth * textureHeight];
                        input.Read(bufferEtc1a4, 0, bufferEtc1a4.Length);
                        texture = TextureCodec.decode(bufferEtc1a4, textureWidth, textureHeight, TextureCodec.textureFormat.etc1a4);
                        break;
                }
                #endregion

                models.addTexture(new RenderBase.OTexture(texture, textureName));
            }

            for (int modelIndex = 0; modelIndex < contentHeader.modelsPointerTableEntries; modelIndex++)
            {
                RenderBase.OModel model = new RenderBase.OModel();

                data.Seek(contentHeader.modelsPointerTableOffset + (modelIndex * 4), SeekOrigin.Begin);
                UInt32 objectsHeaderOffset = input.ReadUInt32() + header.mainHeaderOffset;

                //Objects header
                data.Seek(objectsHeaderOffset, SeekOrigin.Begin);
                bchObjectsHeader objectsHeader;
                objectsHeader.flags = input.ReadByte();
                objectsHeader.skeletonScalingType = input.ReadByte();
                objectsHeader.shadowMaterialEntries = input.ReadUInt16();

                objectsHeader.worldTransform = new RenderBase.OMatrix();
                objectsHeader.worldTransform.M11 = input.ReadSingle();
                objectsHeader.worldTransform.M12 = input.ReadSingle();
                objectsHeader.worldTransform.M13 = input.ReadSingle();
                objectsHeader.worldTransform.M14 = input.ReadSingle();

                objectsHeader.worldTransform.M21 = input.ReadSingle();
                objectsHeader.worldTransform.M22 = input.ReadSingle();
                objectsHeader.worldTransform.M23 = input.ReadSingle();
                objectsHeader.worldTransform.M24 = input.ReadSingle();

                objectsHeader.worldTransform.M31 = input.ReadSingle();
                objectsHeader.worldTransform.M32 = input.ReadSingle();
                objectsHeader.worldTransform.M33 = input.ReadSingle();
                objectsHeader.worldTransform.M34 = input.ReadSingle();

                objectsHeader.texturesTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.texturesTableEntries = input.ReadUInt32();
                objectsHeader.objectsNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.verticesTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.verticesTableEntries = input.ReadUInt32();
                data.Seek(0x28, SeekOrigin.Current);
                objectsHeader.skeletonOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.skeletonEntries = input.ReadUInt32();
                objectsHeader.skeletonNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.facesNameFlagsOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.facesNameFlagsBits = input.ReadUInt32();
                objectsHeader.modelName = IOUtils.readString(input, input.ReadUInt32() + header.stringTableOffset);
                objectsHeader.facesNameEntries = input.ReadUInt32();
                objectsHeader.facesNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                input.ReadUInt32(); //0x0
                objectsHeader.boundingBoxAndMeasuresPointerOffset = input.ReadUInt32();

                //Texture names table
                for (int index = 0; index < objectsHeader.texturesTableEntries; index++)
                {
                    data.Seek(objectsHeader.texturesTableOffset + (index * 0x2c), SeekOrigin.Begin);
                    RenderBase.OTextureParameter parameter = new RenderBase.OTextureParameter();

                    uint textureParametersOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    input.ReadUInt32(); //TODO
                    input.ReadUInt32();
                    input.ReadUInt32();
                    input.ReadUInt32();
                    input.ReadUInt32();
                    uint textureCoordinatorOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    uint nameOffset0 = input.ReadUInt32();
                    uint nameOffset1 = input.ReadUInt32();
                    uint nameOffset2 = input.ReadUInt32();
                    if (nameOffset0 != 0) parameter.name0 = IOUtils.readString(input, nameOffset0 + header.stringTableOffset);
                    if (nameOffset1 != 0) parameter.name1 = IOUtils.readString(input, nameOffset1 + header.stringTableOffset);
                    if (nameOffset2 != 0) parameter.name2 = IOUtils.readString(input, nameOffset2 + header.stringTableOffset);

                    //Parameters
                    data.Seek(textureParametersOffset, SeekOrigin.Begin);
                    input.ReadUInt32();
                    input.ReadUInt32();
                    parameter.sourceCoordinate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    uint projectionAndCamera = input.ReadUInt32();
                    switch ((projectionAndCamera >> 16) & 0xff)
                    {
                        case 0: parameter.projection = RenderBase.OTextureProjection.uvMap; break;
                        case 1: parameter.projection = RenderBase.OTextureProjection.cameraCubeMap; break;
                        case 2: parameter.projection = RenderBase.OTextureProjection.cameraSphereMap; break;
                        case 3: parameter.projection = RenderBase.OTextureProjection.projectionMap; break;
                    }
                    parameter.referenceCamera = projectionAndCamera >> 24;
                    parameter.scaleU = input.ReadSingle();
                    parameter.scaleV = input.ReadSingle();
                    parameter.rotate = input.ReadSingle();
                    parameter.translateU = input.ReadSingle();
                    parameter.translateV = input.ReadSingle();

                    data.Seek(0x80, SeekOrigin.Current);
                    for (int entry = 0; entry < 6; entry++)
                    {
                        uint code = input.ReadUInt32();
                        uint value = input.ReadUInt32();
                        entry++;

                        switch (code)
                        {
                            case codeMaterialConstantColor: parameter.constantColorIndex = value; entry++; break;
                            default: input.ReadUInt32(); entry++; break;
                        }
                    }
                    input.ReadUInt32();
                    uint textureBlendingParametersOffset = input.ReadUInt32() + header.descriptionOffset;
                    uint textureBlendingParametersEntries = input.ReadUInt32();

                    data.Seek(textureBlendingParametersOffset, SeekOrigin.Begin);
                    byte source = input.ReadByte();
                    parameter.rgbSource.Add(getCombineSource((byte)(source & 0xf)));
                    parameter.rgbSource.Add(getCombineSource((byte)(source >> 4)));
                    parameter.rgbSource.Add(getCombineSource((byte)(input.ReadByte() & 0xf)));
                    source = input.ReadByte();
                    parameter.alphaSource.Add(getCombineSource((byte)(source & 0xf)));
                    parameter.alphaSource.Add(getCombineSource((byte)(source >> 4)));
                    parameter.alphaSource.Add(getCombineSource((byte)(input.ReadByte() & 0xf)));

                    for (int entry = 0; entry < textureBlendingParametersEntries; entry++)
                    {
                        uint code = input.ReadUInt32();
                        uint value = input.ReadUInt32();
                        entry++;

                        switch (code)
                        {
                            case codeTextureCombineOperand:
                                byte operand = (byte)(value >> 24);
                                parameter.rgbOperand.Add(getCombineOperandColor((byte)(operand & 0xf)));
                                parameter.rgbOperand.Add(getCombineOperandColor((byte)(operand >> 4)));
                                parameter.rgbOperand.Add(getCombineOperandColor((byte)((value >> 16) & 0xf)));
                                operand = (byte)(value >> 8);
                                parameter.alphaOperand.Add(getCombineOperandAlpha((byte)(operand & 0xf)));
                                parameter.alphaOperand.Add(getCombineOperandAlpha((byte)(operand >> 4)));
                                parameter.alphaOperand.Add(getCombineOperandAlpha((byte)(value & 0xf)));

                                parameter.combineRgb = getCombine(input.ReadUInt16());
                                parameter.combineAlpha = getCombine(input.ReadUInt16());
                                input.ReadUInt32();
                                parameter.rgbScale = (ushort)(input.ReadUInt16() + 1);
                                parameter.alphaScale = (ushort)(input.ReadUInt16() + 1);

                                entry += 4;
                    
                                break;
                            case codeBlockEnd: entry = (int)textureBlendingParametersEntries; break;
                        }
                    }

                    //Coordinator
                    data.Seek(textureCoordinatorOffset, SeekOrigin.Begin);
                    parameter.coordinator.Add(getCoordinator(input));
                    parameter.coordinator.Add(getCoordinator(input));
                    parameter.coordinator.Add(getCoordinator(input));

                    model.addTextureParameter(parameter);
                }

                //Skeleton
                data.Seek(objectsHeader.skeletonOffset, SeekOrigin.Begin);
                for (int index = 0; index < objectsHeader.skeletonEntries; index++)
                {
                    RenderBase.OBone bone = new RenderBase.OBone();

                    uint boneFlags = input.ReadUInt32();
                    bone.parentId = input.ReadInt16();
                    ushort boneSpacer = input.ReadUInt16();
                    bone.scale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    bone.rotation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    bone.translation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    
                    RenderBase.OMatrix boneMatrix = new RenderBase.OMatrix();
                    boneMatrix.M11 = input.ReadSingle();
                    boneMatrix.M12 = input.ReadSingle();
                    boneMatrix.M13 = input.ReadSingle();
                    boneMatrix.M14 = input.ReadSingle();

                    boneMatrix.M21 = input.ReadSingle();
                    boneMatrix.M22 = input.ReadSingle();
                    boneMatrix.M23 = input.ReadSingle();
                    boneMatrix.M24 = input.ReadSingle();

                    boneMatrix.M31 = input.ReadSingle();
                    boneMatrix.M32 = input.ReadSingle();
                    boneMatrix.M33 = input.ReadSingle();
                    boneMatrix.M34 = input.ReadSingle();

                    bone.name = IOUtils.readString(input, input.ReadUInt32() + header.stringTableOffset);
                    input.ReadUInt32(); //TODO: Figure out

                    model.addBone(bone);
                }

                //Bounding box
                RenderBase.OVector3 minimumVector = new RenderBase.OVector3();
                RenderBase.OVector3 maximumVector = new RenderBase.OVector3();
                float heightCentimeters = 0;
                float heightAdjustCentimeters = 0;
                if (objectsHeader.boundingBoxAndMeasuresPointerOffset != 0)
                {
                    data.Seek(objectsHeader.boundingBoxAndMeasuresPointerOffset + header.mainHeaderOffset, SeekOrigin.Begin);
                    uint measuresHeaderOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    uint measuresHeaderEntries = input.ReadUInt32();

                    uint boundingBoxOffset = 0;
                    for (int index = 0; index < measuresHeaderEntries; index++)
                    {
                        data.Seek(measuresHeaderOffset + (index * 0xc), SeekOrigin.Begin);
                        uint nameOffset = input.ReadUInt32() + header.stringTableOffset;
                        uint flags = input.ReadUInt32();
                        uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;

                        switch (index)
                        {
                            case 0: boundingBoxOffset = dataOffset; break;
                            case 1: data.Seek(dataOffset, SeekOrigin.Begin); heightCentimeters = input.ReadSingle(); break;
                            case 2: data.Seek(dataOffset, SeekOrigin.Begin); heightAdjustCentimeters = input.ReadSingle(); break;
                        }
                    }

                    if (measuresHeaderEntries > 0)
                    {
                        data.Seek(boundingBoxOffset, SeekOrigin.Begin);
                        minimumVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        maximumVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    }
                }

                //Vertices header
                data.Seek(objectsHeader.verticesTableOffset, SeekOrigin.Begin);
                List<bchObjectEntry> objects = new List<bchObjectEntry>();

                for (int index = 0; index < objectsHeader.verticesTableEntries; index++)
                {
                    bchObjectEntry objectEntry = new bchObjectEntry();
                    objectEntry.materialId = input.ReadUInt16();
                    input.ReadUInt16();
                    objectEntry.renderPriority = input.ReadUInt16();
                    input.ReadUInt16();
                    objectEntry.verticesHeaderOffset = input.ReadUInt32() + header.descriptionOffset;
                    objectEntry.verticesHeaderEntries = input.ReadUInt32();
                    objectEntry.facesHeaderOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    objectEntry.facesHeaderEntries = input.ReadUInt32();
                    objectEntry.verticesHeader2Offset = input.ReadUInt32() + header.descriptionOffset;
                    objectEntry.verticesHeader2Entries = input.ReadUInt32();
                    objectEntry.centerVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    objectEntry.flagsOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    input.ReadUInt32(); //ex: 0x0 fixo
                    objectEntry.boundingBoxOffset = input.ReadUInt32() + header.mainHeaderOffset;

                    objects.Add(objectEntry);
                }

                for (int index = 0; index < objects.Count; index++)
                {
                    RenderBase.OModelObject obj = new RenderBase.OModelObject();
                    obj.materialId = objects[index].materialId;

                    //Vertices
                    data.Seek(objects[index].verticesHeaderOffset, SeekOrigin.Begin);
                    uint vertexDataPointerOffset = 0;
                    uint vertexVectorsPerEntry = 0;
                    uint vertexVectorOrder = 0;
                    uint vertexFlags = 0;
                    uint vertexDataOffset = 0;
                    byte vertexEntryLength = 0;
                    RenderBase.OVector3 positionOffset = new RenderBase.OVector3();
                    float objectScale;
                    uint vertexVectorFlags = input.ReadUInt32();
                    for (int entry = 0; entry < objects[index].verticesHeaderEntries; entry++)
                    {
                        uint code = input.ReadUInt32();
                        uint value = input.ReadUInt32();
                        entry++;

                        switch (code)
                        {
                            case codeVerticeVectorsPerEntry: vertexVectorsPerEntry = value; break;
                            case codeVerticeVectorOrder: vertexVectorOrder = value; break;
                            case codeVerticeHeaderData:
                                vertexFlags = value;
                                input.ReadUInt32(); //TODO: Figure out what all this data is
                                vertexDataPointerOffset = (uint)data.Position - header.descriptionOffset;
                                vertexDataOffset = input.ReadUInt32();
                                input.ReadUInt32();
                                vertexEntryLength = (byte)((input.ReadUInt32() >> 16) & 0xff);
                                input.ReadUInt32();
                                entry += 5;
                                break;
                            case codeVerticePositionOffset:
                                positionOffset.z = input.ReadSingle();
                                positionOffset.y = input.ReadSingle();
                                positionOffset.x = input.ReadSingle();
                                entry += 3;
                                break;
                            case codeVerticeScale:
                                input.ReadUInt32();
                                objectScale = input.ReadSingle();
                                entry += 2;
                                break;
                            case codeBlockEnd: entry = (int)objects[index].verticesHeaderEntries; break;
                        }
                    }

                    bool dbgVertexDataOffsetCheck = false;
                    data.Seek(header.relocatableTableOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.relocatableTableLength / 4; i++)
                    {
                        uint value = input.ReadUInt32();
                        uint offset = (value & 0xffffff) * 4;
                        byte flags = (byte)(value >> 24);

                        if (offset == vertexDataPointerOffset)
                        {
                            dbgVertexDataOffsetCheck = true;
                            switch (flags)
                            {
                                case 0x4c: vertexDataOffset += header.dataOffset; break;
                                case 0x56: vertexDataOffset += header.dataExtendedOffset; break;
                                default: throw new Exception("BCH: Unknow Vertex Data Location! STOP!");
                            }
                            break;
                        }
                    }
                    if (!dbgVertexDataOffsetCheck) throw new Exception("BCH: Vertex Data Offset pointer not found on Relocatable Table! STOP!");

                    List<RenderBase.CustomVertex> vertexBuffer = new List<RenderBase.CustomVertex>();
                    
                    //Faces
                    List<ushort> nodeList = new List<ushort>();
                    for (int i = 0; i < objects[index].facesHeaderEntries; i++)
                    {
                        uint baseOffset = objects[index].facesHeaderOffset + ((uint)i * 0x34);
                        data.Seek(baseOffset, SeekOrigin.Begin);
                        ushort nodeIdFlags = input.ReadUInt16();
                        ushort nodeIdEntries = input.ReadUInt16();
                        for (int j = 0; j < nodeIdEntries; j++)
                        {
                            nodeList.Add(input.ReadUInt16());
                        }

                        data.Seek(baseOffset + 0x2c, SeekOrigin.Begin);
                        uint faceHeaderOffset = input.ReadUInt32() + header.descriptionOffset;
                        uint faceHeaderEntries = input.ReadUInt32();

                        data.Seek(faceHeaderOffset, SeekOrigin.Begin);
                        uint faceDataPointerOffset = 0;
                        uint faceDataOffset = 0;
                        uint faceDataEntries = 0;
                        input.ReadUInt32(); //TODO: Figure out
                        for (int entry = 0; entry < faceHeaderEntries; entry++)
                        {
                            uint code = input.ReadUInt32();
                            uint value = input.ReadUInt32();
                            entry++;

                            switch (code)
                            {
                                case codeFaceDataOffset:
                                    faceDataPointerOffset = (uint)(data.Position - 4) - header.descriptionOffset;
                                    faceDataOffset = value;
                                    break;
                                case codeFaceDataLength: faceDataEntries = value; break;
                                case codeBlockEnd: entry = (int)faceHeaderEntries; break;
                            }
                        }

                        bool dbgFaceDataOffsetCheck = false;
                        byte faceDataFormat = 0;
                        data.Seek(header.relocatableTableOffset, SeekOrigin.Begin);
                        for (int j = 0; j < header.relocatableTableLength / 4; j++)
                        {
                            uint value = input.ReadUInt32();
                            uint offset = (value & 0xffffff) * 4;
                            byte flags = (byte)(value >> 24);

                            if (offset == faceDataPointerOffset)
                            {
                                dbgFaceDataOffsetCheck = true;
                                faceDataFormat = flags;
                                switch (flags)
                                {
                                    case 0x4e: case 0x50: faceDataOffset += header.dataOffset; break;
                                    case 0x58: case 0x5a: faceDataOffset += header.dataExtendedOffset; break;
                                    default: throw new Exception("BCH: Unknow Face Data Location/Format! STOP!");
                                }
                                break;
                            }
                        }
                        if (!dbgFaceDataOffsetCheck) throw new Exception("BCH: Face Data Offset pointer not found on Relocatable Table! STOP!");

                        #region "Vertex types/quantizations"
                        uint positionVectorQuantization = 0;
                        uint normalVectorQuantization = 0;
                        uint tangentVectorQuantization = 0;
                        uint binormalVectorQuantization = 0; //Is this right?
                        uint texture0VectorQuantization = 0;
                        uint texture1VectorQuantization = 0;
                        uint texture2VectorQuantization = 0;
                        uint boneIndexQuantization = 0;
                        uint boneWeightQuantization = 0;

                        uint positionVectorType = 0;
                        uint normalVectorType = 0;
                        uint tangentVectorType = 0;
                        uint binormalVectorType = 0;
                        uint texture0VectorType = 0;
                        uint texture1VectorType = 0;
                        uint texture2VectorType = 0;
                        uint boneIndexType = 0;
                        uint boneWeightType = 0;

                        for (int nibble = 0; nibble <= vertexVectorsPerEntry; nibble++)
                        {
                            byte value = (byte)((vertexVectorOrder >> (nibble * 4)) & 0xf);
                            byte vectorFlags = (byte)((vertexFlags >> (nibble * 4)) & 0xf);

                            switch (value)
                            {
                                case 0:
                                    positionVectorType = (uint)(vectorFlags >> 2);
                                    positionVectorQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 1:
                                    normalVectorType = (uint)(vectorFlags >> 2);
                                    normalVectorQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 2:
                                    tangentVectorType = (uint)(vectorFlags >> 2);
                                    tangentVectorQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 3:
                                    binormalVectorType = (uint)(vectorFlags >> 2);
                                    binormalVectorQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 4:
                                    texture0VectorType = (uint)(vectorFlags >> 2);
                                    texture0VectorQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 5:
                                    texture1VectorType = (uint)(vectorFlags >> 2);
                                    texture1VectorQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 6:
                                    texture2VectorType = (uint)(vectorFlags >> 2);
                                    texture2VectorQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 7:
                                    boneIndexType = (uint)(vectorFlags >> 2);
                                    boneIndexQuantization = (uint)(vectorFlags & 3);
                                    break;
                                case 8:
                                    boneWeightType = (uint)(vectorFlags >> 2);
                                    boneWeightQuantization = (uint)(vectorFlags & 3);
                                    break;
                            }
                        }
                        #endregion

                        data.Seek(faceDataOffset, SeekOrigin.Begin);
                        for (int faceIndex = 0; faceIndex < faceDataEntries / 3; faceIndex++)
                        {
                            ushort[] indices = new ushort[3];

                            switch (faceDataFormat)
                            {
                                case 0x4e:
                                case 0x58:
                                    indices[0] = input.ReadUInt16();
                                    indices[1] = input.ReadUInt16();
                                    indices[2] = input.ReadUInt16();
                                    break;
                                case 0x50:
                                case 0x5a:
                                    indices[0] = input.ReadByte();
                                    indices[1] = input.ReadByte();
                                    indices[2] = input.ReadByte();
                                    break;
                                default: throw new Exception("BCH: Unknow Face Data Format! STOP!");
                            }

                            long position = data.Position;
                            for (int j = 0; j < 3; j++)
                            {
                                data.Seek(vertexDataOffset + (indices[j] * vertexEntryLength), SeekOrigin.Begin);
                                RenderBase.OVertex vertex = new RenderBase.OVertex();

                                //Position
                                switch (positionVectorQuantization) //Note: Byte Vector3 seems to be aligned to 4 bytes, but Im not sure... hm...
                                {
                                    case quantizationByte: vertex.position = new RenderBase.OVector3((sbyte)input.ReadByte(), (sbyte)input.ReadByte(), (sbyte)input.ReadByte()); input.ReadByte(); break;
                                    case quantizationUByte: vertex.position = new RenderBase.OVector3(input.ReadByte(), input.ReadByte(), input.ReadByte()); input.ReadByte(); break;
                                    case quantizationShort: vertex.position = new RenderBase.OVector3(input.ReadInt16(), input.ReadInt16(), input.ReadInt16()); break;
                                    case quantizationFloat: vertex.position = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle()); break;
                                }

                                //Normal
                                if (normalVectorType > 0)
                                {
                                    switch (normalVectorQuantization)
                                    {
                                        case quantizationByte: vertex.normal = new RenderBase.OVector3((sbyte)input.ReadByte(), (sbyte)input.ReadByte(), (sbyte)input.ReadByte()); input.ReadByte(); break;
                                        case quantizationUByte: vertex.normal = new RenderBase.OVector3(input.ReadByte(), input.ReadByte(), input.ReadByte()); input.ReadByte(); break;
                                        case quantizationShort: vertex.normal = new RenderBase.OVector3(input.ReadInt16(), input.ReadInt16(), input.ReadInt16()); break;
                                        case quantizationFloat: vertex.normal = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle()); break;
                                    }
                                }

                                //Texture U/V
                                if (texture0VectorType > 0)
                                {
                                    switch (texture0VectorQuantization)
                                    {
                                        case quantizationByte: vertex.texture = new RenderBase.OVector2((sbyte)input.ReadByte() / sbyte.MaxValue, (sbyte)input.ReadByte() / sbyte.MaxValue); input.ReadByte(); break;
                                        case quantizationUByte: vertex.texture = new RenderBase.OVector2(input.ReadByte() / byte.MaxValue, input.ReadByte() / byte.MaxValue); input.ReadByte(); break;
                                        case quantizationShort: vertex.texture = new RenderBase.OVector2(input.ReadInt16() / short.MaxValue, input.ReadInt16() / short.MaxValue); break;
                                        case quantizationFloat: vertex.texture = new RenderBase.OVector2(input.ReadSingle(), input.ReadSingle()); break;
                                    }
                                }

                                vertex.diffuseColor = 0xffffffff;

                                if (vertex.position.y < models.minimumY) models.minimumY = vertex.position.y;
                                if (vertex.position.y > models.maximumY) models.maximumY = vertex.position.y;

                                obj.addVertex(vertex);
                                vertexBuffer.Add(RenderBase.convertVertex(vertex));
                            }

                            data.Seek(position, SeekOrigin.Begin);
                        }
                    }

                    obj.renderBuffer = vertexBuffer.ToArray();
                    model.addObject(obj);
                }

                models.addModel(model);
            }

            input.Close();
            return models;
        }

        private static RenderBase.OCombine getCombine(ushort value)
        {
            switch (value)
            {
                case 1: return RenderBase.OCombine.modulate;
                case 2: return RenderBase.OCombine.add;
                case 3: return RenderBase.OCombine.addSigned;
                case 4: return RenderBase.OCombine.interpolate;
                case 5: return RenderBase.OCombine.subtract;
                case 6: return RenderBase.OCombine.dot3Rgb;
                case 7: return RenderBase.OCombine.dot3Rgba;
                case 8: return RenderBase.OCombine.multiplyAdd;
                case 9: return RenderBase.OCombine.addMultiply;
                default: return RenderBase.OCombine.none;
            }
        }

        private static RenderBase.OCombineSource getCombineSource(byte value)
        {
            switch (value)
            {
                case 0: return RenderBase.OCombineSource.primaryColor;
                case 1: return RenderBase.OCombineSource.fragmentPrimaryColor;
                case 2: return RenderBase.OCombineSource.fragmentSecondaryColor;
                case 3: return RenderBase.OCombineSource.texture0;
                case 4: return RenderBase.OCombineSource.texture1;
                case 5: return RenderBase.OCombineSource.texture2;
                case 6: return RenderBase.OCombineSource.texture3;
                case 0xd: return RenderBase.OCombineSource.previousBuffer;
                case 0xe: return RenderBase.OCombineSource.constant;
                case 0xf: return RenderBase.OCombineSource.previous;
                default: return RenderBase.OCombineSource.none;
            }
        }

        private static RenderBase.OCombineOperand getCombineOperandColor(byte value)
        {
            switch (value)
            {
                case 0: return RenderBase.OCombineOperand.color;
                case 1: return RenderBase.OCombineOperand.oneMinusColor;
                case 4: return RenderBase.OCombineOperand.red;
                case 5: return RenderBase.OCombineOperand.oneMinusRed;
                case 8: return RenderBase.OCombineOperand.green;
                case 9: return RenderBase.OCombineOperand.oneMinusGreen;
                case 0xc: return RenderBase.OCombineOperand.blue;
                case 0xd: return RenderBase.OCombineOperand.oneMinusBlue;
                default: return RenderBase.OCombineOperand.none;
            }
        }

        private static RenderBase.OCombineOperand getCombineOperandAlpha(byte value)
        {
            switch (value)
            {
                case 0: return RenderBase.OCombineOperand.alpha;
                case 1: return RenderBase.OCombineOperand.oneMinusAlpha;
                default: return RenderBase.OCombineOperand.none;
            }
        }

        private static RenderBase.OCoordinator getCoordinator(BinaryReader input)
        {
            RenderBase.OCoordinator output = new RenderBase.OCoordinator();
            uint wrapAndMagFilter = input.ReadUInt32();
            uint levelOfDetailAndMinFilter = input.ReadUInt32();

            switch ((wrapAndMagFilter >> 8) & 0xff)
            {
                case 0: output.wrapU = RenderBase.OTextureWrap.clampToEdge; break;
                case 1: output.wrapU = RenderBase.OTextureWrap.clampToBorder; break;
                case 2: output.wrapU = RenderBase.OTextureWrap.repeat; break;
                case 3: output.wrapU = RenderBase.OTextureWrap.mirroredRepeat; break;
            }
            switch ((wrapAndMagFilter >> 16) & 0xff)
            {
                case 0: output.wrapV = RenderBase.OTextureWrap.clampToEdge; break;
                case 1: output.wrapV = RenderBase.OTextureWrap.clampToBorder; break;
                case 2: output.wrapV = RenderBase.OTextureWrap.repeat; break;
                case 3: output.wrapV = RenderBase.OTextureWrap.mirroredRepeat; break;
            }
            switch (wrapAndMagFilter >> 24)
            {
                case 0: output.magFilter = RenderBase.OTextureFilter.nearest; break;
                case 1: output.magFilter = RenderBase.OTextureFilter.linear; break;
            }

            switch (levelOfDetailAndMinFilter & 0xff)
            {
                case 1: output.minFilter = RenderBase.OTextureFilter.nearestMipmapNearest; break;
                case 2: output.minFilter = RenderBase.OTextureFilter.nearestMipmapLinear; break;
                case 4: output.minFilter = RenderBase.OTextureFilter.linearMipmapNearest; break;
                case 5: output.minFilter = RenderBase.OTextureFilter.linearMipmapLinear; break;
            }
            output.minLOD = (levelOfDetailAndMinFilter >> 8) & 0xff; //max 232
            output.LODBias = input.ReadSingle();
            output.borderColor = RenderBase.getMaterialColor(input);

            return output;
        }

        #endregion

    }
}
