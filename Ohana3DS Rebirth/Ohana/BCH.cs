//BCH Importer/Exporter
//Note: Still need to figure out a LOT of stuff, and a bunch of things before the bare-bones model can be rendered
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

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
            public uint unusedDataSectionLength;
            public uint unusedDescriptionSectionLength;

            public ushort flags;
            public ushort addressCount;
        }

        private struct bchContentHeader
        {
            public uint modelsPointerTableOffset;
            public uint modelsPointerTableEntries;
            public uint modelsNameOffset;
            public uint silhouetteMaterialPointerTableOffset;
            public uint silhouetteMaterialPointerTableEntries;
            public uint silhouetteMaterialNameOffset;
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
            public uint textureId;
            public uint visibilityGroup;
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

        const uint codeVerticeUnknow1 = 0x000b02b9;
        const uint codeVerticeUnknow2 = 0x00010242; //Offset relativo ao data offset
        const uint codeVerticeUnknow3 = 0x000f02bb;
        const uint codeVerticeUnknow4 = 0x000f02bc;
        const uint codeVerticeHeaderData = 0x805f0200;
        const uint codeVerticeUnknow5 = 0x804f02c0;
        const uint codeVerticeUnknow6 = 0x000f02c0;
        const uint codeVerticeUnknow7 = 0x007f02c1;

        const uint codeBlockEnd = 0x000f023d;

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
            header.unusedDataSectionLength = input.ReadUInt32();
            header.unusedDescriptionSectionLength = input.ReadUInt32();

            header.flags = input.ReadUInt16();
            header.addressCount = input.ReadUInt16();

            //Content header
            data.Seek(header.mainHeaderOffset, SeekOrigin.Begin);
            bchContentHeader contentHeader = new bchContentHeader();
            contentHeader.modelsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.modelsPointerTableEntries = input.ReadUInt32();
            contentHeader.modelsNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.silhouetteMaterialPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.silhouetteMaterialPointerTableEntries = input.ReadUInt32();
            contentHeader.silhouetteMaterialNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
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

            //Silhouette Material
            for (int index = 0; index < contentHeader.silhouetteMaterialPointerTableEntries; index++)
            {
                data.Seek(contentHeader.silhouetteMaterialPointerTableOffset + (index * 4), SeekOrigin.Begin);
                UInt32 dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                //TODO: Figure out
                //Nota para gdkchan: Alterar o valor em 0x4 deixou parte do corpo do personagem preto.
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

                data.Seek(textureHeaderOffset, SeekOrigin.Begin);
                ushort textureHeight = input.ReadUInt16();
                ushort textureWidth = input.ReadUInt16();
                uint textureDataOffset;
                uint textureFormatId;
                for (int entry = 0; entry < textureHeaderEntries; entry++)
                {
                    uint code = input.ReadUInt32();
                    switch (code)
                    {
                        case codeTextureDataOffset:
                        case codeTextureDataOffsetExtension:
                            textureDataOffset = input.ReadUInt32() + header.dataOffset;
                            entry++;
                            break;
                        case codeTextureFormatId: textureFormatId = input.ReadUInt32(); entry++; break;
                        case codeBlockEnd: entry = (int)textureHeaderEntries; break;
                        default: input.ReadUInt32(); entry++; break;
                    }
                }
            }

            uint faceExtendedOffset = header.dataExtendedOffset;
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
                objectsHeader.boundingBoxAndMeasuresPointerOffset = input.ReadUInt32() + header.mainHeaderOffset;

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

                //Bounding Box
                data.Seek(objectsHeader.boundingBoxAndMeasuresPointerOffset, SeekOrigin.Begin);
                uint measuresHeaderOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint measuresHeaderEntries = input.ReadUInt32();

                uint boundingBoxOffset = 0;
                float heightCentimeters = 0;
                float heightAdjustCentimeters = 0;
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

                RenderBase.OVector3 minimumVector, maximumVector;
                if (measuresHeaderEntries > 0)
                {
                    data.Seek(boundingBoxOffset, SeekOrigin.Begin);
                    minimumVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    maximumVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                }

                //Vertices header
                data.Seek(objectsHeader.verticesTableOffset, SeekOrigin.Begin);
                List<bchObjectEntry> objects = new List<bchObjectEntry>();

                for (int index = 0; index < objectsHeader.verticesTableEntries; index++)
                {
                    bchObjectEntry objectEntry = new bchObjectEntry();
                    objectEntry.textureId = input.ReadUInt32();
                    objectEntry.visibilityGroup = input.ReadUInt32();
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

                    data.Seek(objects[index].verticesHeaderOffset, SeekOrigin.Begin);
                    uint vertexFlags = 0;
                    uint vertexDataOffset = 0;
                    byte vertexEntryLength = 0;
                    uint unknow = input.ReadUInt32();
                    for (int entry = 0; entry < objects[index].verticesHeaderEntries; entry++)
                    {
                        uint code = input.ReadUInt32();
                        switch (code)
                        {
                            case codeVerticeHeaderData:
                                vertexFlags = input.ReadUInt32();
                                input.ReadUInt32(); //TODO: Figure out what all this data is
                                vertexDataOffset = input.ReadUInt32() + header.dataOffset;
                                input.ReadUInt32();
                                input.ReadUInt16();
                                vertexEntryLength = input.ReadByte();
                                input.ReadByte();
                                input.ReadUInt32();
                                input.ReadUInt32();
                                entry += 6;
                                break;
                            case codeBlockEnd: entry = (int)objects[index].verticesHeaderEntries; break;
                            default: input.ReadUInt32(); entry++; break;
                        }
                    }

                    faceExtendedOffset += 8;
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
                        uint flags = input.ReadUInt32();
                        uint faceDataOffset = 0;
                        uint faceDataEntries = 0;
                        for (int entry = 0; entry < faceHeaderEntries; entry++)
                        {
                            uint code = input.ReadUInt32();
                            switch (code)
                            {
                                case codeFaceDataOffset: faceDataOffset = input.ReadUInt32() + header.dataOffset; entry++; break;
                                case codeFaceDataLength: faceDataEntries = input.ReadUInt32(); entry++; break;
                                case codeBlockEnd: entry = (int)faceHeaderEntries; break;
                                default: input.ReadUInt32(); entry++; break;
                            }
                        }

                        data.Seek(faceExtendedOffset, SeekOrigin.Begin);
                        uint faceExtendedData = input.ReadUInt32();
                        faceExtendedOffset += 4;
                        byte faceDataFormat = (byte)(faceExtendedData >> 24);

                        data.Seek(faceDataOffset, SeekOrigin.Begin);
                        for (int faceIndex = 0; faceIndex < faceDataEntries / 3; faceIndex++)
                        {
                            ushort[] indices = new ushort[3];

                            if (faceDataFormat == 0x4e)
                            {
                                indices[0] = input.ReadUInt16();
                                indices[1] = input.ReadUInt16();
                                indices[2] = input.ReadUInt16();
                            }
                            else if (faceDataFormat == 0x50)
                            {
                                indices[0] = input.ReadByte();
                                indices[1] = input.ReadByte();
                                indices[2] = input.ReadByte();
                            }

                            long position = data.Position;
                            for (int j = 0; j < 3; j++)
                            {
                                data.Seek(vertexDataOffset + (indices[j] * vertexEntryLength), SeekOrigin.Begin);
                                RenderBase.OVertex vertex = new RenderBase.OVertex();

                                //Position
                                if ((vertexFlags & 8) != 0)
                                {
                                    switch (((vertexFlags & 2) >> 1) | ((vertexFlags & 0x10) >> 3))
                                    {
                                        case 0: vertex.position = new RenderBase.OVector3(input.ReadByte(), input.ReadByte(), input.ReadByte()); break;
                                        case 1: vertex.position = new RenderBase.OVector3(input.ReadInt16(), input.ReadInt16(), input.ReadInt16()); break;
                                        case 3: vertex.position = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle()); break;
                                    }
                                }

                                //Normal
                                if ((vertexFlags & 0x80) != 0)
                                {
                                    switch ((vertexFlags & 1) | ((vertexFlags & 0x20) >> 4))
                                    {
                                        case 0: vertex.normal = new RenderBase.OVector3(input.ReadByte(), input.ReadByte(), input.ReadByte()); break;
                                        case 1: vertex.normal = new RenderBase.OVector3(input.ReadInt16(), input.ReadInt16(), input.ReadInt16()); break;
                                        case 3: vertex.normal = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle()); break;
                                    }
                                }

                                //Texture U/V
                                if ((vertexFlags & 0x400) != 0)
                                {
                                    switch ((vertexFlags & 0x300) >> 8)
                                    {
                                        case 0: vertex.texture = new RenderBase.OVector2(input.ReadByte(), input.ReadByte()); break;
                                        case 1: vertex.texture = new RenderBase.OVector2(input.ReadInt16(), input.ReadInt16()); break;
                                        case 3: vertex.texture = new RenderBase.OVector2(input.ReadSingle(), input.ReadSingle()); break;
                                    }
                                }

                                vertex.diffuseColor = 0xffffffff;

                                obj.addVertex(vertex);
                            }
                            data.Seek(position, SeekOrigin.Begin);
                        }

                        //MessageBox.Show(vertexFlags.ToString("X8") + " - " + vertexEntryLength.ToString() + " - " + nodeList.Count.ToString());
                    }

                    model.addObject(obj);
                }

                models.addModel(model);
            }

            input.Close();
            return models;
        }
    }
}
