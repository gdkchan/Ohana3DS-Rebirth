//BCH Importer/Exporter
//Note: Still need to figure out a LOT of stuff, and a bunch of things before the bare-bones model can be rendered
//Note to myself: Remember to stop using Managed DirectX structures and make a class with general structures that will be converted on DirectX structs later.
//^ This way the lib can be easily switched to another one, like for example, SharpDX or some OpenGL wrapper.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            public uint objectsHeaderPointerOffset;
            public uint objectsHeaderPointerEntries;
            public uint objectsShadowNameOffset;
            public uint objectsShadowOffset;
            public uint objectsShadowEntries;
            public uint materialsOffset;
            public uint materialsEntries;
            public uint texturesPointerTableOffset;
            public uint texturesPointerTableEntries;
            public uint Unknow_2_Name_Offset;
            public uint texturesPointerTable2Offset;
            public uint texturesPointerTable2Entries;
        }
        private struct BCH_Object_Shadow
        {
            public UInt32 Unknow_3;
            public UInt32 Unknow_4;
            public RenderBase.OVector3 Unknow_1_1;
            public RenderBase.OVector3 Unknow_1_2;
            public RenderBase.OVector3 Unknow_2_1;
            public RenderBase.OVector3 Unknow_2_2;
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
            public String modelName;
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
            public RenderBase.OVector3 boundingBoxVector;
            public uint nodesHeaderOffset;

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

        public static RenderBase.OModel load(string fileName)
        {
            FileStream data = new FileStream(fileName, FileMode.Open);
            BinaryReader input = new BinaryReader(data);

            RenderBase.OModel model = new RenderBase.OModel();

            //Header primário
            bchHeader header = new bchHeader();
            header.magic = IOUtils.Read_String(input, 0);
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

            //Header secundário
            data.Seek(header.mainHeaderOffset, SeekOrigin.Begin);
            bchContentHeader contentHeader = new bchContentHeader();
            contentHeader.objectsHeaderPointerOffset = input.ReadUInt32();
            contentHeader.objectsHeaderPointerEntries = input.ReadUInt32();
            contentHeader.objectsShadowNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.objectsShadowOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.objectsShadowEntries = input.ReadUInt32();
            contentHeader.materialsOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.materialsEntries = input.ReadUInt32();
            input.ReadUInt32(); //??? (0x0) ^ Provavelmente relacionado ao material
            input.ReadUInt32(); //Unused? Nota: olhar depois
            contentHeader.texturesPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.texturesPointerTableEntries = input.ReadUInt32();
            contentHeader.Unknow_2_Name_Offset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.texturesPointerTable2Offset = input.ReadUInt32() + header.mainHeaderOffset;
            contentHeader.texturesPointerTable2Entries = input.ReadUInt32();
            input.ReadUInt32(); //Nome vazio
            //(O resto não é usado, apenas aponta para um campo vazio de 0x0)

            if (contentHeader.objectsHeaderPointerOffset != 0)
            {
                data.Seek(contentHeader.objectsHeaderPointerOffset + header.mainHeaderOffset, SeekOrigin.Begin);
                UInt32 objectsHeaderOffset = input.ReadUInt32() + header.mainHeaderOffset;

                //Sombra dos objetos?
                List<BCH_Object_Shadow> Objects_Shadow = new List<BCH_Object_Shadow>();
                for (int Index = 0; Index < contentHeader.objectsShadowEntries; Index++)
                {
                    BCH_Object_Shadow Object_Shadow;

                    data.Seek(contentHeader.objectsShadowOffset + (Index * 4), SeekOrigin.Begin);
                    UInt32 dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    data.Seek(dataOffset, SeekOrigin.Begin);

                    Object_Shadow.Unknow_3 = input.ReadUInt32();
                    Object_Shadow.Unknow_4 = input.ReadUInt32(); //Alterar isso deixa os personagens pretos!!!!

                    Object_Shadow.Unknow_1_1 = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    Object_Shadow.Unknow_1_2 = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    Object_Shadow.Unknow_2_1 = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle()); //Flag 0x10?
                    Object_Shadow.Unknow_2_2 = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
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

                //Header dos objetos
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
                MessageBox.Show(data.Position.ToString("X8"));
                objectsHeader.verticesTableEntries = input.ReadUInt32();
                data.Seek(0x28, SeekOrigin.Current);
                objectsHeader.skeletonOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.skeletonEntries = input.ReadUInt32();
                objectsHeader.skeletonNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                input.ReadUInt32(); //Aponta para 0xFFFF e mais nada
                input.ReadUInt32(); //Count? (ex: 0x10)
                objectsHeader.modelName = IOUtils.Read_String(input, header.stringTableOffset + input.ReadUInt32());
                input.ReadUInt32(); //Count? (ex: 0x10) sempre igual ao Count acima ao que parece, 0x1 tbm
                //0xFFFF - parece que cada bit definido é uma face na tabela abaixo
                uint offset = input.ReadUInt32(); //Aponta para uma estrutura de comandos localizada logo após o 0xFFFF acima - acredito ter relação com as faces
                input.ReadUInt32(); //0x0
                input.ReadUInt32(); //Dar uma olhada depois

                //Header dos vértices
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
                    objectEntry.boundingBoxVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    input.ReadUInt32(); //ex: 0x278 fixo
                    input.ReadUInt32(); //ex: 0x0 fixo
                    objectEntry.nodesHeaderOffset = input.ReadUInt32() + header.mainHeaderOffset; //Bounding Box Name?

                    objects.Add(objectEntry);
                }

                for (int index = 0; index < objects.Count; index++)
                {
                    data.Seek(objects[index].verticesHeaderOffset, SeekOrigin.Begin);
                    uint vertexFlags;
                    uint vertexDataOffset;
                    byte vertexEntryLength;
                    for (int entry = 0; entry < objects[index].verticesHeaderEntries; entry++)
                    {
                        uint code = input.ReadUInt32();
                        switch (code)
                        {
                            case codeVerticeHeaderData:
                                vertexFlags = input.ReadUInt32();
                                input.ReadUInt32(); //TODO: Figure out what all this data is
                                vertexDataOffset = input.ReadUInt32();
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

                    for (int i = 0; i < objects[index].facesHeaderEntries; i++)
                    {
                        uint baseOffset = objects[index].facesHeaderOffset + ((uint)i * 0x34);
                        data.Seek(baseOffset, SeekOrigin.Begin);
                        for (int j = 0; j < 0x2c; j++)
                        {
                            ushort a = input.ReadUInt16();
                            ushort b = input.ReadUInt16();
                            ushort c = input.ReadUInt16();

                            if (a == 0 && b == 0 && c == 0) break;
                        }

                        data.Seek(baseOffset + 0x2c, SeekOrigin.Begin);
                        uint faceHeaderOffset = input.ReadUInt32() + header.descriptionOffset;
                        uint faceHeaderEntries = input.ReadUInt32();

                        uint flags = input.ReadUInt32();
                        uint faceDataOffset;
                        uint faceDataLength;
                        for (int entry = 0; entry < faceHeaderEntries; entry++)
                        {
                            uint code = input.ReadUInt32();
                            switch (code)
                            {
                                case codeFaceDataOffset: faceDataOffset = input.ReadUInt32() + header.dataOffset; entry++; break;
                                case codeFaceDataLength: faceDataLength = input.ReadUInt32(); entry++; break;
                                case codeBlockEnd: entry = (int)faceHeaderEntries; break;
                                default: input.ReadUInt32(); entry++; break;
                            }
                        }
                    }
                }
            }

            return model;
        }
    }
}
