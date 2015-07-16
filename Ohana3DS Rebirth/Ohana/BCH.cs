//BCH Importer/Exporter made by gdkchan for Ohana3DS.
//Please add credits if you use in your project.
//It is about 92% complete, information here is not guaranteed to be accurate.

/*
 * BCH Version Chart
 * r38xxx - Pokémon X/Y
 * r41xxx - Some Senran Kagura models
 * r42xxx - Pokémon OR/AS, SSB3DS, Zelda ALBW, Senran Kagura
 * r43xxx - Codename S.T.E.A.M. (lastest revision at date of writing)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Ohana3DS_Rebirth.Ohana
{
    public class BCH
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
            public uint relocationTableOffset;

            public uint mainHeaderLength;
            public uint stringTableLength;
            public uint descriptionLength;
            public uint dataLength;
            public uint dataExtendedLength;
            public uint relocationTableLength;
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
            public uint materialsLUTPointerTableOffset;
            public uint materialsLUTPointerTableEntries;
            public uint materialsLUTNameOffset;
            public uint lightsPointerTableOffset;
            public uint lightsPointerTableEntries;
            public uint lightsNameOffset;
            public uint camerasPointerTableOffset;
            public uint camerasPointerTableEntries;
            public uint camerasNameOffset;
            public uint fogsPointerTableOffset;
            public uint fogsPointerTableEntries;
            public uint fogsNameOffset;
            public uint skeletalAnimationsPointerTableOffset;
            public uint skeletalAnimationsPointerTableEntries;
            public uint skeletalAnimationsNameOffset;
            public uint materialAnimationsPointerTableOffset;
            public uint materialAnimationsPointerTableEntries;
            public uint materialAnimationsNameOffset;
            public uint visibilityAnimationsPointerTableOffset;
            public uint visibilityAnimationsPointerTableEntries;
            public uint visibilityAnimationsNameOffset;
            public uint lightAnimationsPointerTableOffset;
            public uint lightAnimationsPointerTableEntries;
            public uint lightAnimationsNameOffset;
            public uint cameraAnimationsPointerTableOffset;
            public uint cameraAnimationsPointerTableEntries;
            public uint cameraAnimationsNameOffset;
            public uint fogAnimationsPointerTableOffset;
            public uint fogAnimationsPointerTableEntries;
            public uint fogAnimationsNameOffset;
            public uint scenePointerTableOffset;
            public uint scenePointerTableEntries;
            public uint sceneNameOffset;
        }

        private struct bchObjectsHeader
        {
            public byte flags;
            public byte skeletonScalingType;
            public ushort silhouetteMaterialEntries;
            public RenderBase.OMatrix worldTransform;

            public uint materialsTableOffset;
            public uint materialsTableEntries;
            public uint materialsNameOffset;
            public uint verticesTableOffset;
            public uint verticesTableEntries;
            public uint skeletonOffset;
            public uint skeletonEntries;
            public uint skeletonNameOffset;
            public uint objectsNodeVisibilityOffset;
            public uint objectsNodeCount;
            public String modelName;
            public uint objectsNodeNameEntries;
            public uint objectsNodeNameOffset;
            public uint boundingBoxAndMeasuresPointerOffset;
        }

        private struct bchObjectEntry
        {
            public ushort materialId;
            public bool isSilhouette;
            public ushort nodeId;
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
        }

        const uint codeMaterialReflectanceSamplerInput = 0x000f01d0;
        const uint codeMaterialReflectanceSamplerScale = 0x000f01d1;
        const uint codeMaterialConstantColor = 0x000f01d2;

        const uint codeMaterialUnknow1 = 0x000f02c0;
        const uint codeMaterialUnknow2 = 0x01ff02c1;
        const uint codeMaterialCombiner0 = 0x804f00c0;
        const uint codeMaterialCombiner1 = 0x804f00c8;
        const uint codeMaterialCombiner2 = 0x804f00d0;
        const uint codeMaterialCombiner3 = 0x804f00d8;
        const uint codeMaterialCombiner4 = 0x804f00f0;
        const uint codeMaterialCombiner5 = 0x804f00f8;
        const uint codeMaterialFragmentBufferColor = 0x000200e0;
        const uint codeMaterialUnknow3 = 0x000f00fd;
        const uint codeMaterialBlendFunction = 0x00030100;
        const uint codeMaterialLogicalOperation = 0x000f0101;
        const uint codeMaterialAlphaTest = 0x000f0102;
        const uint codeMaterialStencilOperationTests = 0x00030104;
        const uint codeMaterialStencilOperationOperations = 0x000f0105;
        const uint codeMaterialDepthOperation = 0x000f0106;
        const uint codeMaterialRasterization = 0x000f0107;
        const uint codeMaterialUnknow4 = 0x000f0040;
        const uint codeMaterialUnknow5 = 0x000f0111;
        const uint codeMaterialUnknow6 = 0x000f0110;
        const uint codeMaterialUnknow7 = 0x00010112;
        const uint codeMaterialUnknow8 = 0x00010113;
        const uint codeMaterialUnknow9 = 0x00010114;
        const uint codeMaterialUnknow10 = 0x00010115;
        const uint codeMaterialUnknow11 = 0x804f02c0;

        const uint codeTextureMipmaps = 0x000f0082;
        const uint codeTextureDataOffset = 0x00040084;
        const uint codeTextureFormatId = 0x000f0085;
        const uint codeTextureUnknow1 = 0x000f008e;

        const uint codeLookUpTable = 0x0fff01c8;

        const uint codeFaceUnknow1 = 0x000f02b0;
        const uint codeFaceDataOffset = 0x000f025f;
        const uint codeFaceDataLength = 0x000f0227;
        const uint codeFaceUnknow2 = 0x000f0228;
        const uint codeFaceUnknow3 = 0x00010245;
        const uint codeFaceUnknow4 = 0x000f022f;
        const uint codeFaceUnknow5 = 0x000f0231;
        const uint codeFaceUnknow6 = 0x0008025e;

        const uint codeVertexComponentTypeCount = 0x000b02b9;
        const uint codeVertexComponentType = 0x00010242;
        const uint codeVertexUnknow1 = 0x000f02bb;
        const uint codeVertexUnknow2 = 0x000f02bc;
        const uint codeVertexComponentHeader = 0x805f0200;
        const uint codeVertexUnknow3 = 0x803f0232;
        const uint codeVertexPositionOffset = 0x804f02c0;
        const uint codeVertexColorScale = 0x000f02c0;
        const uint codeVertexScale = 0x007f02c1;

        const uint codeBlockEnd = 0x000f023d;

        private enum vectorQuantization
        {
            qByte = 0,
            qUByte = 1,
            qShort = 2,
            qFloat = 3
        }

        private enum vectorType
        {
            vector1 = 0,
            vector2 = 1,
            vector3 = 2,
            vector4 = 3
        }

        private struct vectorData
        {
            public float scale;
            public vectorQuantization quantization;
            public vectorType type;
            public bool isPresent;
            public bool isUsed;
        }

        #region "Import"
        /// <summary>
        ///     Loads a BCH file.
        /// </summary>
        /// <param name="fileName">File Name of the BCH file</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(string fileName)
        {
            return load(new MemoryStream(File.ReadAllBytes(fileName)));
        }

        /// <summary>
        ///     Loads a BCH file.
        ///     Note that BCH must start at offset 0x0 (don't try using it for BCHs inside containers).
        /// </summary>
        /// <param name="data">Stream of the BCH file</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(Stream data)
        {
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
            if (header.backwardCompatibility > 0x20) header.dataExtendedOffset = input.ReadUInt32();
            header.relocationTableOffset = input.ReadUInt32();

            header.mainHeaderLength = input.ReadUInt32();
            header.stringTableLength = input.ReadUInt32();
            header.descriptionLength = input.ReadUInt32();
            header.dataLength = input.ReadUInt32();
            if (header.backwardCompatibility > 0x20) header.dataExtendedLength = input.ReadUInt32();
            header.relocationTableLength = input.ReadUInt32();
            header.uninitializedDataSectionLength = input.ReadUInt32();
            header.uninitializedDescriptionSectionLength = input.ReadUInt32();

            header.flags = input.ReadUInt16();
            header.addressCount = input.ReadUInt16();

            //Content header
            data.Seek(header.mainHeaderOffset, SeekOrigin.Begin);
            bchContentHeader contentHeader = new bchContentHeader
            {
                modelsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                modelsPointerTableEntries = input.ReadUInt32(),
                modelsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                materialsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                materialsPointerTableEntries = input.ReadUInt32(),
                materialsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                shadersPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                shadersPointerTableEntries = input.ReadUInt32(),
                shadersNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                texturesPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                texturesPointerTableEntries = input.ReadUInt32(),
                texturesNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                materialsLUTPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                materialsLUTPointerTableEntries = input.ReadUInt32(),
                materialsLUTNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                lightsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                lightsPointerTableEntries = input.ReadUInt32(),
                lightsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                camerasPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                camerasPointerTableEntries = input.ReadUInt32(),
                camerasNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                fogsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                fogsPointerTableEntries = input.ReadUInt32(),
                fogsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                skeletalAnimationsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                skeletalAnimationsPointerTableEntries = input.ReadUInt32(),
                skeletalAnimationsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                materialAnimationsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                materialAnimationsPointerTableEntries = input.ReadUInt32(),
                materialAnimationsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                visibilityAnimationsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                visibilityAnimationsPointerTableEntries = input.ReadUInt32(),
                visibilityAnimationsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                lightAnimationsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                lightAnimationsPointerTableEntries = input.ReadUInt32(),
                lightAnimationsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                cameraAnimationsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                cameraAnimationsPointerTableEntries = input.ReadUInt32(),
                cameraAnimationsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                fogAnimationsPointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                fogAnimationsPointerTableEntries = input.ReadUInt32(),
                fogAnimationsNameOffset = input.ReadUInt32() + header.mainHeaderOffset,
                scenePointerTableOffset = input.ReadUInt32() + header.mainHeaderOffset,
                scenePointerTableEntries = input.ReadUInt32(),
                sceneNameOffset = input.ReadUInt32() + header.mainHeaderOffset
            };

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
                string textureName = readString(input, header);

                data.Seek(textureHeaderOffset, SeekOrigin.Begin);
                ushort textureHeight = input.ReadUInt16();
                ushort textureWidth = input.ReadUInt16();
                uint textureMipmaps = 0;
                uint textureDataOffset = 0;
                uint textureFormatId = 0;
                for (int entry = 0; entry < textureHeaderEntries; entry++)
                {
                    uint code = input.ReadUInt32();

                    switch (code)
                    {
                        case codeTextureMipmaps: input.ReadUInt16(); textureMipmaps = input.ReadUInt16(); entry++; break;
                        case codeTextureDataOffset: textureDataOffset = input.ReadUInt32() + header.dataOffset; entry++; break;
                        case codeTextureFormatId: textureFormatId = input.ReadUInt32(); entry++; break;
                        case codeTextureUnknow1: input.ReadUInt32(); entry++; break;
                        case codeBlockEnd: entry = (int)textureHeaderEntries; break;
                    }
                }

                Bitmap texture = null;
                data.Seek(textureDataOffset, SeekOrigin.Begin);
                byte[] buffer = new byte[textureWidth * textureHeight * 4];
                input.Read(buffer, 0, buffer.Length);
                texture = TextureCodec.decode(buffer, textureWidth, textureHeight, (TextureCodec.OTextureFormat)textureFormatId);

                models.addTexture(new RenderBase.OTexture(texture, textureName));
            }

            //LookUp Tables
            for (int index = 0; index < contentHeader.materialsLUTPointerTableEntries; index++)
            {
                data.Seek(contentHeader.materialsLUTPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                input.ReadUInt32();
                uint samplersCount = input.ReadUInt32();
                string name = readString(input, header);

                RenderBase.OLookUpTable table = new RenderBase.OLookUpTable();
                table.name = name;
                for (int i = 0; i < samplersCount; i++)
                {
                    input.ReadUInt32();
                    uint tableOffset = input.ReadUInt32() + header.descriptionOffset;
                    uint tableEntries = input.ReadUInt32();
                    string tableName = readString(input, header);

                    long dataPosition = data.Position;
                    data.Seek(tableOffset, SeekOrigin.Begin);
                    for (int entry = 0; entry < tableEntries; entry++)
                    {
                        uint code = input.ReadUInt32();

                        switch (code)
                        {
                            case codeLookUpTable:
                                RenderBase.OLookUpTableSampler sampler = new RenderBase.OLookUpTableSampler();
                                sampler.name = tableName;
                                for (int t = 0; t < 256; t++) sampler.table[t] = input.ReadSingle();
                                table.sampler.Add(sampler);
                                break;
                            case codeBlockEnd: entry = (int)tableEntries; break;
                        }
                    }

                    data.Seek(dataPosition, SeekOrigin.Begin);
                }

                models.addLUT(table);
            }

            //Lights
            for (int index = 0; index < contentHeader.lightsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.lightsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OLight light = new RenderBase.OLight();
                light.name = readString(input, header);
                light.transformScale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                light.transformRotate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                light.transformTranslate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());

                uint lightFlags = input.ReadUInt32();
                switch (lightFlags & 0xf)
                {
                    case 1: light.lightUse = RenderBase.OLightUse.hemiSphere; break;
                    case 2: light.lightUse = RenderBase.OLightUse.ambient; break;
                    case 5:
                    case 6:
                    case 7:
                        light.lightUse = RenderBase.OLightUse.vertex;
                        switch (lightFlags & 0xf)
                        {
                            case 5: light.lightType = RenderBase.OLightType.directional; break;
                            case 6: light.lightType = RenderBase.OLightType.point; break;
                            case 7: light.lightType = RenderBase.OLightType.spot; break;
                            
                        }
                        break;
                    case 9:
                    case 0xa:
                    case 0xb:
                        light.lightUse = RenderBase.OLightUse.fragment;
                        switch (lightFlags & 0xf)
                        {
                            case 9: light.lightType = RenderBase.OLightType.directional; break;
                            case 0xa: light.lightType = RenderBase.OLightType.point; break;
                            case 0xb: light.lightType = RenderBase.OLightType.spot; break;
                        }
                        break;
                    default: Debug.WriteLine(String.Format("BCH: Warning - Unknow Light Flags {0}", lightFlags.ToString("X8"))); break;
                }
                light.isLightEnabled = (lightFlags & 0x100) > 0;
                light.isTwoSideDiffuse = (lightFlags & 0x10000) > 0;
                light.isDistanceAttenuationEnabled = (lightFlags & 0x20000) > 0;
                light.angleSampler.input = (RenderBase.OFragmentSamplerInput)((lightFlags >> 24) & 0xf);
                light.angleSampler.scale = (RenderBase.OFragmentSamplerScale)((lightFlags >> 28) & 0xf);

                input.ReadUInt32();
                switch (light.lightUse)
                {
                    case RenderBase.OLightUse.hemiSphere:
                        light.groundColor = getColorFloat(input);
                        light.skyColor = getColorFloat(input);
                        light.direction = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        light.lerpFactor = input.ReadSingle();
                        break;
                    case RenderBase.OLightUse.ambient: light.ambient = getColor(input); break;
                    case RenderBase.OLightUse.vertex:
                        light.ambient = getColorFloat(input);
                        light.diffuse = getColorFloat(input);
                        light.direction = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        light.distanceAttenuationConstant = input.ReadSingle();
                        light.distanceAttenuationLinear = input.ReadSingle();
                        light.distanceAttenuationQuadratic = input.ReadSingle();
                        light.spotExponent = input.ReadSingle();
                        light.spotCutoffAngle = input.ReadSingle();
                        break;
                    case RenderBase.OLightUse.fragment:
                        light.ambient = getColor(input);
                        light.diffuse = getColor(input);
                        light.specular0 = getColor(input);
                        light.specular1 = getColor(input);
                        light.direction = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        input.ReadUInt32();
                        input.ReadUInt32();
                        light.attenuationStart = input.ReadSingle();
                        light.attenuationEnd = input.ReadSingle();

                        light.distanceSampler.materialLUTName = readString(input, header);
                        light.distanceSampler.samplerName = readString(input, header);

                        light.angleSampler.materialLUTName = readString(input, header);
                        light.angleSampler.samplerName = readString(input, header);
                        break;
                }

                models.addLight(light);
            }

            //Cameras
            for (int index = 0; index < contentHeader.camerasPointerTableEntries; index++)
            {
                data.Seek(contentHeader.camerasPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OCamera camera = new RenderBase.OCamera();
                camera.name = readString(input, header);
                camera.transformScale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                camera.transformRotate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                camera.transformTranslate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());

                uint cameraFlags = input.ReadUInt32();
                camera.isInheritingTargetRotate = (cameraFlags & 0x10000) > 0;
                camera.isInheritingTargetTranslate = (cameraFlags & 0x20000) > 0;
                camera.isInheritingUpRotate = (cameraFlags & 0x40000) > 0;
                camera.view = (RenderBase.OCameraView)(cameraFlags & 0xf);
                camera.projection = (RenderBase.OCameraProjection)((cameraFlags >> 8) & 0xf);

                input.ReadSingle();
                uint viewOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint projectionOffset = input.ReadUInt32() + header.mainHeaderOffset;

                data.Seek(viewOffset, SeekOrigin.Begin);
                camera.target = new RenderBase.OVector3();
                camera.rotation = new RenderBase.OVector3();
                camera.upVector = new RenderBase.OVector3();
                RenderBase.OVector3 target = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                switch (camera.view)
                {
                    case RenderBase.OCameraView.aimTarget: camera.target = target; camera.twist = input.ReadSingle(); break;
                    case RenderBase.OCameraView.lookAtTarget: camera.target = target; camera.upVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle()); break;
                    case RenderBase.OCameraView.rotate: camera.rotation = target; break;
                }

                data.Seek(projectionOffset, SeekOrigin.Begin);
                camera.zNear = input.ReadSingle();
                camera.zFar = input.ReadSingle();
                camera.aspectRatio = input.ReadSingle();
                switch (camera.projection)
                {
                    case RenderBase.OCameraProjection.perspective: camera.fieldOfViewY = input.ReadSingle(); break;
                    case RenderBase.OCameraProjection.orthogonal: camera.height = input.ReadSingle(); break;
                }

                models.addCamera(camera);
            }

            //Fogs
            for (int index = 0; index < contentHeader.fogsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.fogsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OFog fog = new RenderBase.OFog();
                fog.name = readString(input, header);
                fog.transformScale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                fog.transformRotate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                fog.transformTranslate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());

                uint fogFlags = input.ReadUInt32();
                fog.fogUpdater = (RenderBase.OFogUpdater)(fogFlags & 0xf);
                fog.isZFlip = (fogFlags & 0x100) > 0;
                fog.isAttenuateDistance = (fogFlags & 0x200) > 0;

                fog.fogColor = getColor(input);

                fog.minFogDepth = input.ReadSingle();
                fog.maxFogDepth = input.ReadSingle();
                fog.fogDensity = input.ReadSingle();

                models.addFog(fog);
            }

            byte[] shiftTable;

            //Skeletal Animations
            shiftTable = new byte[] { 1, 2, 3, 5, 6, 7 };
            for (int index = 0; index < contentHeader.skeletalAnimationsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.skeletalAnimationsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OSkeletalAnimation skeletalAnimation = new RenderBase.OSkeletalAnimation();

                skeletalAnimation.name = readString(input, header);
                uint animationFlags = input.ReadUInt32();
                skeletalAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                skeletalAnimation.frameSize = input.ReadSingle();
                uint boneTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint boneTableEntries = input.ReadUInt32();

                for (int i = 0; i < boneTableEntries; i++)
                {
                    data.Seek(boneTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32() + header.mainHeaderOffset;

                    RenderBase.OSkeletalAnimationBone bone = new RenderBase.OSkeletalAnimationBone();

                    data.Seek(offset, SeekOrigin.Begin);
                    bone.name = readString(input, header);
                    uint animationTypeFlags = input.ReadUInt32();
                    uint flags = input.ReadUInt32();
                    input.ReadUInt32();

                    RenderBase.OSegmentType segmentType = (RenderBase.OSegmentType)((animationTypeFlags >> 16) & 0xf);

                    switch (segmentType)
                    {
                        case RenderBase.OSegmentType.transform:
                            bone.rotationExists = (flags & 0x300000) == 0;
                            bone.translationExists = (flags & 0xc00000) == 0;

                            for (int j = 0; j < 6; j++)
                            {
                                RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

                                data.Seek(offset + 0x18 + (j * 4), SeekOrigin.Begin);
                                bool inline = ((flags >> 8) & (1 << shiftTable[j])) > 0;
                                if ((j < 3 && bone.rotationExists) || (j > 2 && bone.translationExists))
                                {
                                    if (inline)
                                    {
                                        frame.interpolation = RenderBase.OInterpolationMode.linear;
                                        frame.keyFrames.Add(new RenderBase.OInterpolationFloat(input.ReadSingle(), 0.0f));
                                    }
                                    else
                                    {
                                        uint frameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                                        data.Seek(frameOffset, SeekOrigin.Begin);
                                        frame = getAnimationKeyFrame(input, header);
                                    }
                                }

                                switch (j)
                                {
                                    case 0: bone.rotationX = frame; break;
                                    case 1: bone.rotationY = frame; break;
                                    case 2: bone.rotationZ = frame; break;
                                    case 3: bone.translationX = frame; break;
                                    case 4: bone.translationY = frame; break;
                                    case 5: bone.translationZ = frame; break;
                                }
                            }
                            break;
                        case RenderBase.OSegmentType.transformQuaternion:
                            bone.isFrameFormat = true;

                            long originalPos = data.Position;
                            uint rotationOffset = input.ReadUInt32() + header.mainHeaderOffset;
                            uint translationOffset = input.ReadUInt32() + header.mainHeaderOffset;

                            if ((flags & 0x10) == 0)
                            {
                                bone.rotationQuaternion.exists = true;
                                data.Seek(rotationOffset, SeekOrigin.Begin);

                                if ((flags & 2) > 0)
                                {
                                    bone.rotationQuaternion.vector.Add(new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle()));
                                }
                                else
                                {
                                    bone.rotationQuaternion.startFrame = input.ReadSingle();
                                    bone.rotationQuaternion.endFrame = input.ReadSingle();
                                    uint rotationFlags = input.ReadUInt32();
                                    uint rotationDataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                                    uint rotationEntries = input.ReadUInt32();

                                    data.Seek(rotationDataOffset, SeekOrigin.Begin);
                                    for (int j = 0; j < rotationEntries; j++)
                                    {
                                        bone.rotationQuaternion.vector.Add(new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle()));
                                    }
                                }
                            }

                            if ((flags & 8) == 0)
                            {
                                bone.translation.exists = true;
                                data.Seek(translationOffset, SeekOrigin.Begin);

                                if ((flags & 1) > 0)
                                {
                                    bone.translation.vector.Add(new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), 0));
                                }
                                else
                                {
                                    bone.translation.startFrame = input.ReadSingle();
                                    bone.translation.endFrame = input.ReadSingle();
                                    uint translationFlags = input.ReadUInt32();
                                    uint translationDataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                                    uint translationEntries = input.ReadUInt32();

                                    data.Seek(translationDataOffset, SeekOrigin.Begin);
                                    for (int j = 0; j < translationEntries; j++)
                                    {
                                        bone.translation.vector.Add(new RenderBase.OVector4(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), 0));
                                    }
                                }
                            }

                            break;
                        case RenderBase.OSegmentType.transformMatrix:
                            bone.isFullBakedFormat = true;

                            input.ReadUInt32();
                            uint matrixOffset = input.ReadUInt32() + header.mainHeaderOffset;
                            uint entries = input.ReadUInt32();

                            data.Seek(matrixOffset, SeekOrigin.Begin);
                            for (int j = 0; j < entries; j++)
                            {
                                RenderBase.OMatrix transform = new RenderBase.OMatrix();
                                transform.M11 = input.ReadSingle();
                                transform.M21 = input.ReadSingle();
                                transform.M31 = input.ReadSingle();
                                transform.M41 = input.ReadSingle();

                                transform.M12 = input.ReadSingle();
                                transform.M22 = input.ReadSingle();
                                transform.M32 = input.ReadSingle();
                                transform.M42 = input.ReadSingle();

                                transform.M13 = input.ReadSingle();
                                transform.M23 = input.ReadSingle();
                                transform.M33 = input.ReadSingle();
                                transform.M43 = input.ReadSingle();

                                bone.transform.Add(transform);
                            }

                            break;
                        default: throw new Exception(String.Format("BCH: Unknow Segment Type {0} on Skeletal Animation bone {1}! STOP!", segmentType, bone.name));
                    }

                    skeletalAnimation.bone.Add(bone);
                }

                models.addSkeletalAnimaton(skeletalAnimation);
            }

            //Material Animations
            for (int index = 0; index < contentHeader.materialAnimationsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.materialAnimationsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OMaterialAnimation materialAnimation = new RenderBase.OMaterialAnimation();

                materialAnimation.name = readString(input, header);
                uint animationFlags = input.ReadUInt32();
                materialAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                materialAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                uint textureNameTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint textureNameTableEntries = input.ReadUInt32();

                data.Seek(textureNameTableOffset, SeekOrigin.Begin);
                for (int i = 0; i < textureNameTableEntries; i++)
                {
                    string name = readString(input, header);
                    materialAnimation.textureName.Add(name);
                }

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32() + header.mainHeaderOffset;

                    RenderBase.OMaterialAnimationData animationData = new RenderBase.OMaterialAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input, header);
                    uint animationTypeFlags = input.ReadUInt32();
                    uint flags = input.ReadUInt32();

                    animationData.type = (RenderBase.OMaterialAnimationType)(animationTypeFlags & 0xff);
                    RenderBase.OSegmentType segmentType = (RenderBase.OSegmentType)((animationTypeFlags >> 16) & 0xf);

                    int segmentCount = 0;
                    switch (segmentType)
                    {
                        case RenderBase.OSegmentType.rgbaColor: segmentCount = 4; break;
                        case RenderBase.OSegmentType.vector2: segmentCount = 2; break;
                        case RenderBase.OSegmentType.single: segmentCount = 1; break;
                        case RenderBase.OSegmentType.integer: segmentCount = 1; break;
                    }

                    for (int j = 0; j < segmentCount; j++)
                    {
                        RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

                        data.Seek(offset + 0xc + (j * 4), SeekOrigin.Begin);

                        frame.exists = ((flags >> 8) & (1 << j)) == 0;
                        bool inline = (flags & (1 << j)) > 0;

                        if (frame.exists)
                        {
                            if (inline)
                            {
                                frame.interpolation = RenderBase.OInterpolationMode.linear;
                                frame.keyFrames.Add(new RenderBase.OInterpolationFloat(input.ReadSingle(), 0.0f));
                            }
                            else
                            {
                                uint frameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                                data.Seek(frameOffset, SeekOrigin.Begin);
                                frame = getAnimationKeyFrame(input, header);
                            }
                        }

                        animationData.frameList.Add(frame);

                    }

                    materialAnimation.data.Add(animationData);
                }

                models.addMaterialAnimation(materialAnimation);
            }

            //Visibility Animations
            for (int index = 0; index < contentHeader.visibilityAnimationsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.visibilityAnimationsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OVisibilityAnimation visibilityAnimation = new RenderBase.OVisibilityAnimation();

                visibilityAnimation.name = readString(input, header);
                uint animationFlags = input.ReadUInt32();
                visibilityAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                visibilityAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                input.ReadUInt32();

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32() + header.mainHeaderOffset;

                    RenderBase.OVisibilityAnimationData animationData = new RenderBase.OVisibilityAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input, header);
                    uint animationTypeFlags = input.ReadUInt32();
                    uint flags = input.ReadUInt32();

                    RenderBase.OSegmentType segmentType = (RenderBase.OSegmentType)((animationTypeFlags >> 16) & 0xf);
                    if (segmentType == RenderBase.OSegmentType.boolean)
                    {
                        RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();
                        if (segmentType == RenderBase.OSegmentType.boolean) frame = getAnimationKeyFrameBool(input, header);
                        animationData.visibilityList = frame;
                    }

                    visibilityAnimation.data.Add(animationData);
                }

                models.addVisibilityAnimation(visibilityAnimation);
            }

            shiftTable = new byte[] { 6, 7, 8, 9, 10, 11, 13, 14, 15 };

            //Light Animations
            for (int index = 0; index < contentHeader.lightAnimationsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.lightAnimationsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OLightAnimation lightAnimation = new RenderBase.OLightAnimation();

                lightAnimation.name = readString(input, header);
                uint animationFlags = input.ReadUInt32();
                lightAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                lightAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                uint typeFlags = input.ReadUInt32();
                lightAnimation.lightType = (RenderBase.OLightType)((typeFlags & 3) - 1);
                lightAnimation.lightUse = (RenderBase.OLightUse)((typeFlags >> 2) & 3);

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32() + header.mainHeaderOffset;

                    RenderBase.OLightAnimationData animationData = new RenderBase.OLightAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input, header);
                    uint animationTypeFlags = input.ReadUInt32();
                    uint flags = input.ReadUInt32();

                    animationData.type = (RenderBase.OLightAnimationType)(animationTypeFlags & 0xff);
                    RenderBase.OSegmentType segmentType = (RenderBase.OSegmentType)((animationTypeFlags >> 16) & 0xf);

                    int segmentCount = 0;
                    switch (segmentType)
                    {
                        case RenderBase.OSegmentType.transform: segmentCount = 9; break;
                        case RenderBase.OSegmentType.rgbaColor: segmentCount = 4; break;
                        case RenderBase.OSegmentType.vector3: segmentCount = 3; break;
                        case RenderBase.OSegmentType.single: segmentCount = 1; break;
                        case RenderBase.OSegmentType.boolean: segmentCount = 1; break;
                    }

                    for (int j = 0; j < segmentCount; j++)
                    {
                        RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

                        data.Seek(offset + 0xc + (j * 4), SeekOrigin.Begin);

                        frame.exists = ((flags >> (segmentType == RenderBase.OSegmentType.transform ? 16 : 8)) & (1 << j)) == 0;

                        if (frame.exists)
                        {
                            if (segmentType == RenderBase.OSegmentType.boolean)
                            {
                                frame = getAnimationKeyFrameBool(input, header);
                            }
                            else
                            {
                                bool inline = (flags & (1 << (segmentType == RenderBase.OSegmentType.transform ? shiftTable[j] : j))) > 0;

                                if (inline)
                                {
                                    frame.interpolation = RenderBase.OInterpolationMode.linear;
                                    frame.keyFrames.Add(new RenderBase.OInterpolationFloat(input.ReadSingle(), 0.0f));
                                }
                                else
                                {
                                    uint frameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                                    data.Seek(frameOffset, SeekOrigin.Begin);
                                    frame = getAnimationKeyFrame(input, header);
                                }
                            }
                        }

                        animationData.frameList.Add(frame);
                    }

                    lightAnimation.data.Add(animationData);
                }

                models.addLightAnimation(lightAnimation);
            }

            //Camera Animations
            for (int index = 0; index < contentHeader.cameraAnimationsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.cameraAnimationsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OCameraAnimation cameraAnimation = new RenderBase.OCameraAnimation();

                cameraAnimation.name = readString(input, header);
                uint animationFlags = input.ReadUInt32();
                cameraAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                cameraAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                uint modeFlags = input.ReadUInt32();
                cameraAnimation.viewMode = (RenderBase.OCameraView)(modeFlags & 0xf);
                cameraAnimation.projectionMode = (RenderBase.OCameraProjection)((modeFlags >> 8) & 0xf);

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32() + header.mainHeaderOffset;

                    RenderBase.OCameraAnimationData animationData = new RenderBase.OCameraAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input, header);
                    uint animationTypeFlags = input.ReadUInt32();
                    uint flags = input.ReadUInt32();

                    animationData.type = (RenderBase.OCameraAnimationType)(animationTypeFlags & 0xff);
                    RenderBase.OSegmentType segmentType = (RenderBase.OSegmentType)((animationTypeFlags >> 16) & 0xf);

                    int segmentCount = 0;
                    switch (segmentType)
                    {
                        case RenderBase.OSegmentType.transform: segmentCount = 9; break;
                        case RenderBase.OSegmentType.vector3: segmentCount = 3; break;
                        case RenderBase.OSegmentType.single: segmentCount = 1; break;
                    }

                    for (int j = 0; j < segmentCount; j++)
                    {
                        RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

                        data.Seek(offset + 0xc + (j * 4), SeekOrigin.Begin);

                        frame.exists = ((flags >> (segmentType == RenderBase.OSegmentType.transform ? 16 : 8)) & (1 << j)) == 0;
                        bool inline = (flags & (1 << (segmentType == RenderBase.OSegmentType.transform ? shiftTable[j] : j))) > 0;

                        if (frame.exists)
                        {
                            if (inline)
                            {
                                frame.interpolation = RenderBase.OInterpolationMode.linear;
                                frame.keyFrames.Add(new RenderBase.OInterpolationFloat(input.ReadSingle(), 0.0f));
                            }
                            else
                            {
                                uint frameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                                data.Seek(frameOffset, SeekOrigin.Begin);
                                frame = getAnimationKeyFrame(input, header);
                            }
                        }

                        animationData.frameList.Add(frame);
                    }

                    cameraAnimation.data.Add(animationData);
                }

                models.addCameraAnimation(cameraAnimation);
            }

            //Fog Animations
            for (int index = 0; index < contentHeader.fogAnimationsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.fogAnimationsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OFogAnimation fogAnimation = new RenderBase.OFogAnimation();

                fogAnimation.name = readString(input, header);
                uint animationFlags = input.ReadUInt32();
                fogAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                fogAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                input.ReadUInt32();

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32() + header.mainHeaderOffset;

                    RenderBase.OFogAnimationData animationData = new RenderBase.OFogAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input, header);
                    uint animationTypeFlags = input.ReadUInt32();
                    uint flags = input.ReadUInt32();

                    RenderBase.OSegmentType segmentType = (RenderBase.OSegmentType)((animationTypeFlags >> 16) & 0xf);
                    int segmentCount = segmentType == RenderBase.OSegmentType.rgbaColor ? 4 : 0;

                    for (int j = 0; j < segmentCount; j++)
                    {
                        RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

                        data.Seek(offset + 0xc + (j * 4), SeekOrigin.Begin);

                        frame.exists = ((flags >> 8) & (1 << j)) == 0;

                        if (frame.exists)
                        {
                            bool inline = (flags & (1 << j)) > 0;

                            if (inline)
                            {
                                frame.interpolation = RenderBase.OInterpolationMode.linear;
                                frame.keyFrames.Add(new RenderBase.OInterpolationFloat(input.ReadSingle(), 0.0f));
                            }
                            else
                            {
                                uint frameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                                data.Seek(frameOffset, SeekOrigin.Begin);
                                frame = getAnimationKeyFrame(input, header);
                            }
                        }

                        animationData.colorList.Add(frame);
                    }

                    fogAnimation.data.Add(animationData);
                }

                models.addFogAnimation(fogAnimation);
            }

            //Scene Environment
            for (int index = 0; index < contentHeader.scenePointerTableEntries; index++)
            {
                data.Seek(contentHeader.scenePointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OScene scene = new RenderBase.OScene();
                scene.name = readString(input, header);

                uint cameraReferenceOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint cameraReferenceEntries = input.ReadUInt32();
                uint lightReferenceOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint lightReferenceEntries = input.ReadUInt32();
                uint fogReferenceOffset = input.ReadUInt32() + header.mainHeaderOffset;
                uint fogReferenceEntries = input.ReadUInt32();

                data.Seek(cameraReferenceOffset, SeekOrigin.Begin);
                for (int i = 0; i < cameraReferenceEntries; i++)
                {
                    RenderBase.OSceneReference reference = new RenderBase.OSceneReference();
                    reference.slotIndex = input.ReadUInt32();
                    reference.name = readString(input, header);
                    scene.cameras.Add(reference);
                }

                data.Seek(lightReferenceOffset, SeekOrigin.Begin);
                for (int i = 0; i < lightReferenceEntries; i++)
                {
                    RenderBase.OSceneReference reference = new RenderBase.OSceneReference();
                    reference.slotIndex = input.ReadUInt32();
                    reference.name = readString(input, header);
                    scene.lights.Add(reference);
                }

                data.Seek(fogReferenceOffset, SeekOrigin.Begin);
                for (int i = 0; i < fogReferenceEntries; i++)
                {
                    RenderBase.OSceneReference reference = new RenderBase.OSceneReference();
                    reference.slotIndex = input.ReadUInt32();
                    reference.name = readString(input, header);
                    scene.fogs.Add(reference);
                }
            }

            //Model
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
                objectsHeader.silhouetteMaterialEntries = input.ReadUInt16();

                objectsHeader.worldTransform = new RenderBase.OMatrix();
                objectsHeader.worldTransform.M11 = input.ReadSingle();
                objectsHeader.worldTransform.M21 = input.ReadSingle();
                objectsHeader.worldTransform.M31 = input.ReadSingle();
                objectsHeader.worldTransform.M41 = input.ReadSingle();

                objectsHeader.worldTransform.M12 = input.ReadSingle();
                objectsHeader.worldTransform.M22 = input.ReadSingle();
                objectsHeader.worldTransform.M32 = input.ReadSingle();
                objectsHeader.worldTransform.M42 = input.ReadSingle();

                objectsHeader.worldTransform.M13 = input.ReadSingle();
                objectsHeader.worldTransform.M23 = input.ReadSingle();
                objectsHeader.worldTransform.M33 = input.ReadSingle();
                objectsHeader.worldTransform.M43 = input.ReadSingle();

                objectsHeader.materialsTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.materialsTableEntries = input.ReadUInt32();
                objectsHeader.materialsNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.verticesTableOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.verticesTableEntries = input.ReadUInt32();
                data.Seek(0x28, SeekOrigin.Current);
                objectsHeader.skeletonOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.skeletonEntries = input.ReadUInt32();
                objectsHeader.skeletonNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.objectsNodeVisibilityOffset = input.ReadUInt32() + header.mainHeaderOffset;
                objectsHeader.objectsNodeCount = input.ReadUInt32();
                objectsHeader.modelName = readString(input, header);
                objectsHeader.objectsNodeNameEntries = input.ReadUInt32();
                objectsHeader.objectsNodeNameOffset = input.ReadUInt32() + header.mainHeaderOffset;
                input.ReadUInt32(); //0x0
                objectsHeader.boundingBoxAndMeasuresPointerOffset = input.ReadUInt32();

                model.transform = objectsHeader.worldTransform;
                model.name = objectsHeader.modelName;

                string[] objectName = new string[objectsHeader.objectsNodeNameEntries];
                data.Seek(objectsHeader.objectsNodeNameOffset + 0xc, SeekOrigin.Begin);
                for (int i = 0; i < objectsHeader.objectsNodeNameEntries; i++)
                {
                    input.ReadUInt32();
                    input.ReadUInt32();
                    objectName[i] = readString(input, header);
                }

                //Materials
                for (int index = 0; index < objectsHeader.materialsTableEntries; index++)
                {
                    //Nota: As versões mais antigas tinham o Coordinator já no header do material.
                    //As versões mais recentes tem uma seção reservada só pra ele, por isso possuem tamanho do header menor.
                    switch (header.backwardCompatibility)
                    {
                        case 0x20: data.Seek(objectsHeader.materialsTableOffset + (index * 0x58), SeekOrigin.Begin); break;
                        case 0x21: case 0x22: data.Seek(objectsHeader.materialsTableOffset + (index * 0x2c), SeekOrigin.Begin); break;
                        default: throw new Exception(String.Format("BCH: Unknow BCH version r{0}, can't parse Material texture header! STOP!", header.version.ToString()));
                    }

                    RenderBase.OMaterial material = new RenderBase.OMaterial();

                    uint materialParametersOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    input.ReadUInt32(); //TODO
                    input.ReadUInt32();
                    input.ReadUInt32();
                    uint textureHeaderOffset = input.ReadUInt32() + header.descriptionOffset;
                    uint textureHeaderEntries = input.ReadUInt32();

                    uint materialCoordinatorOffset = 0;
                    switch (header.backwardCompatibility)
                    {
                        case 0x20:
                            materialCoordinatorOffset = (uint)data.Position;
                            data.Seek(0x30, SeekOrigin.Current);
                            break;
                        case 0x21: case 0x22: materialCoordinatorOffset = input.ReadUInt32() + header.mainHeaderOffset; break;
                    }

                    material.name0 = readString(input, header);
                    material.name1 = readString(input, header);
                    material.name2 = readString(input, header);
                    material.name = readString(input, header);

                    //Parameters
                    //Same pointer of Materials section. Why?
                    data.Seek(materialParametersOffset, SeekOrigin.Begin);
                    input.ReadUInt32(); //Checksum?

                    ushort materialFlags = input.ReadUInt16();
                    material.isFragmentLightEnabled = (materialFlags & 1) > 0;
                    material.isVertexLightEnabled = (materialFlags & 2) > 0;
                    material.isHemiSphereLightEnabled = (materialFlags & 4) > 0;
                    material.isHemiSphereOcclusionEnabled = (materialFlags & 8) > 0;
                    material.isFogEnabled = (materialFlags & 0x10) > 0;
                    material.rasterization.isPolygonOffsetEnabled = (materialFlags & 0x20) > 0;

                    ushort fragmentFlags = input.ReadUInt16();
                    material.fragmentShader.bump.isBumpRenormalize = (fragmentFlags & 1) > 0;
                    material.fragmentShader.lighting.isClampHighLight = (fragmentFlags & 2) > 0;
                    material.fragmentShader.lighting.isDistribution0Enabled = (fragmentFlags & 4) > 0;
                    material.fragmentShader.lighting.isDistribution1Enabled = (fragmentFlags & 8) > 0;
                    material.fragmentShader.lighting.isGeometryFactor0Enabled = (fragmentFlags & 0x10) > 0;
                    material.fragmentShader.lighting.isGeometryFactor1Enabled = (fragmentFlags & 0x20) > 0;
                    material.fragmentShader.lighting.isReflectionEnabled = (fragmentFlags & 0x40) > 0;
                    material.fragmentOperation.blend.mode = (RenderBase.OBlendMode)((fragmentFlags >> 10) & 3);

                    input.ReadUInt32();
                    for (int i = 0; i < 3; i++)
                    {
                        RenderBase.OTextureCoordinator coordinator;
                        uint projectionAndCamera = input.ReadUInt32();
                        coordinator.projection = (RenderBase.OTextureProjection)((projectionAndCamera >> 16) & 0xff);
                        coordinator.referenceCamera = projectionAndCamera >> 24;
                        coordinator.scaleU = input.ReadSingle();
                        coordinator.scaleV = input.ReadSingle();
                        coordinator.rotate = input.ReadSingle();
                        coordinator.translateU = input.ReadSingle();
                        coordinator.translateV = input.ReadSingle();

                        material.textureCoordinator.Add(coordinator);
                    }

                    material.lightSetIndex = input.ReadUInt16();
                    material.fogIndex = input.ReadUInt16();

                    material.materialColor.emission = getColor(input);
                    material.materialColor.ambient = getColor(input);
                    material.materialColor.diffuse = getColor(input);
                    material.materialColor.specular0 = getColor(input);
                    material.materialColor.specular1 = getColor(input);
                    material.materialColor.constant0 = getColor(input);
                    material.materialColor.constant1 = getColor(input);
                    material.materialColor.constant2 = getColor(input);
                    material.materialColor.constant3 = getColor(input);
                    material.materialColor.constant4 = getColor(input);
                    material.materialColor.constant5 = getColor(input);
                    material.fragmentOperation.blend.blendColor = getColor(input);
                    material.materialColor.colorScale = input.ReadSingle();

                    input.ReadUInt32(); //TODO: Figure out
                    input.ReadUInt32();
                    input.ReadUInt32();
                    input.ReadUInt32();
                    input.ReadUInt32();
                    input.ReadUInt32();

                    uint fragmentData = input.ReadUInt32();
                    material.fragmentShader.bump.texture = (RenderBase.OBumpTexture)(fragmentData >> 24);
                    material.fragmentShader.bump.mode = (RenderBase.OBumpMode)((fragmentData >> 16) & 0xff);
                    material.fragmentShader.lighting.fresnelConfig = (RenderBase.OFresnelConfig)((fragmentData >> 8) & 0xff);
                    material.fragmentShader.layerConfig = fragmentData & 0xff;
                    input.ReadUInt32();

                    RenderBase.OConstantColor[] constantList = new RenderBase.OConstantColor[6];
                    for (int entry = 0; entry < 6; entry++)
                    {
                        uint code = input.ReadUInt32();
                        uint value;

                        switch (code)
                        {
                            case codeMaterialReflectanceSamplerInput:
                                value = input.ReadUInt32();
                                material.fragmentShader.lighting.reflectanceRSampler.input = (RenderBase.OFragmentSamplerInput)((value >> 24) & 0xf);
                                material.fragmentShader.lighting.reflectanceGSampler.input = (RenderBase.OFragmentSamplerInput)((value >> 20) & 0xf);
                                material.fragmentShader.lighting.reflectanceBSampler.input = (RenderBase.OFragmentSamplerInput)((value >> 16) & 0xf);
                                material.fragmentShader.lighting.distribution0Sampler.input = (RenderBase.OFragmentSamplerInput)(value & 0xf);
                                material.fragmentShader.lighting.distribution1Sampler.input = (RenderBase.OFragmentSamplerInput)((value >> 4) & 0xf);
                                material.fragmentShader.lighting.fresnelSampler.input = (RenderBase.OFragmentSamplerInput)((value >> 12) & 0xf);
                                entry++;
                                break;
                            case codeMaterialReflectanceSamplerScale:
                                value = input.ReadUInt32();
                                material.fragmentShader.lighting.reflectanceRSampler.scale = (RenderBase.OFragmentSamplerScale)((value >> 24) & 0xf);
                                material.fragmentShader.lighting.reflectanceGSampler.scale = (RenderBase.OFragmentSamplerScale)((value >> 20) & 0xf);
                                material.fragmentShader.lighting.reflectanceBSampler.scale = (RenderBase.OFragmentSamplerScale)((value >> 16) & 0xf);
                                material.fragmentShader.lighting.distribution0Sampler.scale = (RenderBase.OFragmentSamplerScale)(value & 0xf);
                                material.fragmentShader.lighting.distribution1Sampler.scale = (RenderBase.OFragmentSamplerScale)((value >> 4) & 0xf);
                                material.fragmentShader.lighting.fresnelSampler.scale = (RenderBase.OFragmentSamplerScale)((value >> 12) & 0xf);
                                entry++;
                                break;
                            case codeMaterialConstantColor:
                                value = input.ReadUInt32();
                                for (int i = 0; i < 6; i++) constantList[i] = (RenderBase.OConstantColor)((value >> (i * 4)) & 0xf);
                                entry++;
                                break;
                        }
                    }
                    material.rasterization.polygonOffsetUnit = input.ReadSingle();
                    uint textureCombinerOffset = input.ReadUInt32() + header.descriptionOffset;
                    uint textureCombinerEntries = input.ReadUInt32();
                    input.ReadUInt32();

                    material.fragmentShader.lighting.distribution0Sampler.materialLUTName = readString(input, header);
                    material.fragmentShader.lighting.distribution1Sampler.materialLUTName = readString(input, header);
                    material.fragmentShader.lighting.fresnelSampler.materialLUTName = readString(input, header);
                    material.fragmentShader.lighting.reflectanceRSampler.materialLUTName = readString(input, header);
                    material.fragmentShader.lighting.reflectanceGSampler.materialLUTName = readString(input, header);
                    material.fragmentShader.lighting.reflectanceBSampler.materialLUTName = readString(input, header);

                    material.fragmentShader.lighting.distribution0Sampler.samplerName = readString(input, header);
                    material.fragmentShader.lighting.distribution1Sampler.samplerName = readString(input, header);
                    material.fragmentShader.lighting.fresnelSampler.samplerName = readString(input, header);
                    material.fragmentShader.lighting.reflectanceRSampler.samplerName = readString(input, header);
                    material.fragmentShader.lighting.reflectanceGSampler.samplerName = readString(input, header);
                    material.fragmentShader.lighting.reflectanceBSampler.samplerName = readString(input, header);

                    data.Seek(textureCombinerOffset, SeekOrigin.Begin);
                    input.ReadUInt32();
                    for (int entry = 0; entry < textureCombinerEntries; entry++)
                    {
                        uint code = input.ReadUInt32();
                        uint value;

                        switch (code)
                        {
                            case codeMaterialUnknow1: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow2: data.Seek(0x80, SeekOrigin.Current); entry += 0x20; break;
                            case codeMaterialCombiner0:
                            case codeMaterialCombiner1:
                            case codeMaterialCombiner2:
                            case codeMaterialCombiner3:
                            case codeMaterialCombiner4:
                            case codeMaterialCombiner5:
                                RenderBase.OTextureCombiner combiner = new RenderBase.OTextureCombiner();
                                input.BaseStream.Seek(-8, SeekOrigin.Current);

                                //Source
                                uint source = input.ReadUInt32();

                                combiner.rgbSource.Add((RenderBase.OCombineSource)(source & 0xf));
                                combiner.rgbSource.Add((RenderBase.OCombineSource)((source >> 4) & 0xf));
                                combiner.rgbSource.Add((RenderBase.OCombineSource)((source >> 8) & 0xf));

                                combiner.alphaSource.Add((RenderBase.OCombineSource)((source >> 16) & 0xf));
                                combiner.alphaSource.Add((RenderBase.OCombineSource)((source >> 20) & 0xf));
                                combiner.alphaSource.Add((RenderBase.OCombineSource)((source >> 24) & 0xf));

                                input.BaseStream.Seek(4, SeekOrigin.Current);

                                //Operand
                                uint operand = input.ReadUInt32();

                                combiner.rgbOperand.Add((RenderBase.OCombineOperandRgb)(operand & 0xf));
                                combiner.rgbOperand.Add((RenderBase.OCombineOperandRgb)((operand >> 4) & 0xf));
                                combiner.rgbOperand.Add((RenderBase.OCombineOperandRgb)((operand >> 8) & 0xf));

                                combiner.alphaOperand.Add((RenderBase.OCombineOperandAlpha)((operand >> 12) & 0xf));
                                combiner.alphaOperand.Add((RenderBase.OCombineOperandAlpha)((operand >> 16) & 0xf));
                                combiner.alphaOperand.Add((RenderBase.OCombineOperandAlpha)((operand >> 20) & 0xf));

                                //Operator
                                combiner.combineRgb = (RenderBase.OCombineOperator)input.ReadUInt16();
                                combiner.combineAlpha = (RenderBase.OCombineOperator)input.ReadUInt16();

                                //Scale
                                input.ReadUInt32();
                                combiner.rgbScale = (ushort)(input.ReadUInt16() + 1);
                                combiner.alphaScale = (ushort)(input.ReadUInt16() + 1);

                                switch (code)
                                {
                                    case codeMaterialCombiner0: combiner.constantColor = constantList[0]; break;
                                    case codeMaterialCombiner1: combiner.constantColor = constantList[1]; break;
                                    case codeMaterialCombiner2: combiner.constantColor = constantList[2]; break;
                                    case codeMaterialCombiner3: combiner.constantColor = constantList[3]; break;
                                    case codeMaterialCombiner4: combiner.constantColor = constantList[4]; break;
                                    case codeMaterialCombiner5: combiner.constantColor = constantList[5]; break;
                                }

                                material.fragmentShader.textureCombiner.Add(combiner);
                                entry += 4;
                                break;
                            case codeMaterialFragmentBufferColor:
                                material.fragmentShader.bufferColor = getColor(input);
                                entry++;
                                break;
                            case codeMaterialUnknow3: input.ReadUInt32(); entry++; break;
                            case codeMaterialDepthOperation:
                                value = input.ReadUInt32();
                                material.fragmentOperation.depth.isTestEnabled = (value & 1) > 0;
                                material.fragmentOperation.depth.testFunction = (RenderBase.OTestFunction)((value >> 4) & 0xf);
                                material.fragmentOperation.depth.isMaskEnabled = (value & 0x1000) > 0;
                                break;
                            case codeMaterialBlendFunction:
                                //Note: Only Separate Blend have unique Alpha functions.
                                value = input.ReadUInt32();
                                material.fragmentOperation.blend.rgbFunctionSource = (RenderBase.OBlendFunction)((value >> 16) & 0xf);
                                material.fragmentOperation.blend.rgbFunctionDestination = (RenderBase.OBlendFunction)((value >> 20) & 0xf);
                                material.fragmentOperation.blend.alphaFunctionSource = (RenderBase.OBlendFunction)((value >> 24) & 0xf);
                                material.fragmentOperation.blend.alphaFunctionDestination = (RenderBase.OBlendFunction)((value >> 28) & 0xf);
                                material.fragmentOperation.blend.rgbBlendEquation = (RenderBase.OBlendEquation)(value & 0xff);
                                material.fragmentOperation.blend.alphaBlendEquation = (RenderBase.OBlendEquation)((value >> 8) & 0xff);
                                entry++;
                                break;
                            case codeMaterialLogicalOperation:
                            case codeMaterialAlphaTest:
                                value = input.ReadUInt32();
                                if (code == codeMaterialLogicalOperation && material.fragmentOperation.blend.mode == RenderBase.OBlendMode.logical)
                                {
                                    material.fragmentOperation.blend.logicalOperation = (RenderBase.OLogicalOperation)(input.ReadUInt32() & 0xf);
                                }
                                else
                                {
                                    //When Logical Operation is enabled, the Alpha Test code changes it seems...
                                    material.fragmentShader.alphaTest.isTestEnabled = (value & 1) > 0;
                                    material.fragmentShader.alphaTest.testFunction = (RenderBase.OTestFunction)((value >> 4) & 0xf);
                                    material.fragmentShader.alphaTest.testReference = ((value >> 8) & 0xff);
                                }
                                entry++;
                                break;
                            case codeMaterialStencilOperationTests:
                                value = input.ReadUInt32();
                                material.fragmentOperation.stencil.isTestEnabled = (value & 1) > 0;
                                material.fragmentOperation.stencil.testFunction = (RenderBase.OTestFunction)((value >> 4) & 0xf);
                                material.fragmentOperation.stencil.testReference = (value >> 16) & 0xff;
                                material.fragmentOperation.stencil.testMask = (value >> 24);
                                entry++;
                                break;
                            case codeMaterialStencilOperationOperations:
                                value = input.ReadUInt32();
                                material.fragmentOperation.stencil.failOperation = (RenderBase.OStencilOp)(value & 0xf);
                                material.fragmentOperation.stencil.zFailOperation = (RenderBase.OStencilOp)((value >> 4) & 0xf);
                                material.fragmentOperation.stencil.passOperation = (RenderBase.OStencilOp)((value >> 8) & 0xf);
                                entry++;
                                break;
                            case codeMaterialRasterization:
                                value = input.ReadUInt32();
                                material.rasterization.cullMode = (RenderBase.OCullMode)(value & 0xf);
                                entry++;
                                break;
                            case codeMaterialUnknow4: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow5: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow6: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow7: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow8: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow9: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow10: input.ReadUInt32(); entry++; break;
                            case codeMaterialUnknow11: data.Seek(0x10, SeekOrigin.Current); entry += 4; break;
                            case codeBlockEnd: entry = (int)textureCombinerEntries; break;
                        }
                    }

                    //Coordinator
                    data.Seek(materialCoordinatorOffset, SeekOrigin.Begin);
                    for (int i = 0; i < 3; i++)
                    {
                        RenderBase.OTextureMapper mapper;
                        uint wrapAndMagFilter = input.ReadUInt32();
                        uint levelOfDetailAndMinFilter = input.ReadUInt32();
                        mapper.wrapU = (RenderBase.OTextureWrap)((wrapAndMagFilter >> 8) & 0xff);
                        mapper.wrapV = (RenderBase.OTextureWrap)((wrapAndMagFilter >> 16) & 0xff);
                        mapper.magFilter = (RenderBase.OTextureMagFilter)(wrapAndMagFilter >> 24);
                        mapper.minFilter = (RenderBase.OTextureMinFilter)(levelOfDetailAndMinFilter & 0xff);
                        mapper.minLOD = (levelOfDetailAndMinFilter >> 8) & 0xff; //max 232
                        mapper.LODBias = input.ReadSingle();
                        mapper.borderColor = getColor(input);

                        material.textureMapper.Add(mapper);
                    }

                    model.addMaterial(material);
                }

                //Skeleton
                data.Seek(objectsHeader.skeletonOffset, SeekOrigin.Begin);
                for (int index = 0; index < objectsHeader.skeletonEntries; index++)
                {
                    RenderBase.OBone bone = new RenderBase.OBone();

                    uint boneFlags = input.ReadUInt32();
                    bone.billboardMode = (RenderBase.OBillboardMode)((boneFlags >> 16) & 0xf);
                    bone.isSegmentScaleCompensate = (boneFlags & 0x00400000) > 0;
                    bone.parentId = input.ReadInt16();
                    ushort boneSpacer = input.ReadUInt16();
                    bone.scale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    bone.rotation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    bone.translation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    bone.absoluteScale = new RenderBase.OVector3(bone.scale);

                    RenderBase.OMatrix boneMatrix = new RenderBase.OMatrix();
                    boneMatrix.M11 = input.ReadSingle();
                    boneMatrix.M21 = input.ReadSingle();
                    boneMatrix.M31 = input.ReadSingle();
                    boneMatrix.M41 = input.ReadSingle();

                    boneMatrix.M12 = input.ReadSingle();
                    boneMatrix.M22 = input.ReadSingle();
                    boneMatrix.M32 = input.ReadSingle();
                    boneMatrix.M42 = input.ReadSingle();

                    boneMatrix.M13 = input.ReadSingle();
                    boneMatrix.M23 = input.ReadSingle();
                    boneMatrix.M33 = input.ReadSingle();
                    boneMatrix.M43 = input.ReadSingle();

                    bone.name = readString(input, header);
                    input.ReadUInt32(); //TODO: Figure out

                    model.addBone(bone);
                }

                List<RenderBase.OMatrix> skeletonTransform = new List<RenderBase.OMatrix>();
                for (int index = 0; index < objectsHeader.skeletonEntries; index++)
                {
                    RenderBase.OMatrix transform = new RenderBase.OMatrix();
                    transformSkeleton(model.skeleton, index, ref transform);
                    skeletonTransform.Add(transform);
                }

                //Bounding box
                List<RenderBase.OOrientedBoundingBox> orientedBBox = new List<RenderBase.OOrientedBoundingBox>();
                if (objectsHeader.boundingBoxAndMeasuresPointerOffset != 0)
                {
                    data.Seek(objectsHeader.boundingBoxAndMeasuresPointerOffset + header.mainHeaderOffset, SeekOrigin.Begin);
                    uint measuresHeaderOffset = input.ReadUInt32() + header.mainHeaderOffset;
                    uint measuresHeaderEntries = input.ReadUInt32();

                    for (int index = 0; index < measuresHeaderEntries; index++)
                    {
                        data.Seek(measuresHeaderOffset + (index * 0xc), SeekOrigin.Begin);
                        uint nameOffset = input.ReadUInt32() + header.stringTableOffset;
                        uint flags = input.ReadUInt32();
                        uint dataOffset = input.ReadUInt32() + header.mainHeaderOffset;

                        RenderBase.OOrientedBoundingBox bBox = new RenderBase.OOrientedBoundingBox();
                        bBox.centerPosition = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());

                        bBox.orientationMatrix.M11 = input.ReadSingle();
                        bBox.orientationMatrix.M21 = input.ReadSingle();
                        bBox.orientationMatrix.M31 = input.ReadSingle();

                        bBox.orientationMatrix.M12 = input.ReadSingle();
                        bBox.orientationMatrix.M22 = input.ReadSingle();
                        bBox.orientationMatrix.M32 = input.ReadSingle();

                        bBox.orientationMatrix.M13 = input.ReadSingle();
                        bBox.orientationMatrix.M23 = input.ReadSingle();
                        bBox.orientationMatrix.M33 = input.ReadSingle();

                        bBox.size = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());

                        orientedBBox.Add(bBox);
                    }
                }

                data.Seek(objectsHeader.objectsNodeVisibilityOffset, SeekOrigin.Begin);
                uint nodeVisibility = input.ReadUInt32();

                //Vertices header
                data.Seek(objectsHeader.verticesTableOffset, SeekOrigin.Begin);
                List<bchObjectEntry> objects = new List<bchObjectEntry>();

                for (int index = 0; index < objectsHeader.verticesTableEntries; index++)
                {
                    bchObjectEntry objectEntry = new bchObjectEntry();
                    objectEntry.materialId = input.ReadUInt16();
                    ushort flags = input.ReadUInt16();
                    objectEntry.isSilhouette = (flags & 1) > 0;
                    objectEntry.nodeId = input.ReadUInt16();
                    objectEntry.renderPriority = input.ReadUInt16();
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
                    if (objects[index].isSilhouette) continue; //TODO: Figure out for what "Silhouette" is used.

                    RenderBase.OModelObject obj = new RenderBase.OModelObject();
                    obj.materialId = objects[index].materialId;
                    obj.renderPriority = objects[index].renderPriority;
                    if (objects[index].nodeId < objectName.Length) obj.name = objectName[objects[index].nodeId]; else obj.name = "mesh" + index.ToString();
                    obj.isVisible = (nodeVisibility & (1 << objects[index].nodeId)) > 0;

                    //Vertices
                    data.Seek(objects[index].verticesHeaderOffset, SeekOrigin.Begin);

                    RenderBase.OVector3 positionOffset = new RenderBase.OVector3();
                    uint vertexDataPointerOffset = 0;
                    uint vertexComponentTypeCount = 0;
                    uint vertexComponentType = 0;
                    uint vertexComponentIndex = 0;
                    uint vertexComponentFlags = 0;
                    uint vertexFlags = 0;
                    uint vertexDataOffset = 0;
                    byte vertexStride = 0;
                    byte vertexComponentCount = 0;

                    vectorData position = new vectorData();
                    vectorData normal = new vectorData();
                    vectorData tangent = new vectorData();
                    vectorData color = new vectorData();
                    vectorData texture0 = new vectorData(); //Texture
                    vectorData texture1 = new vectorData();
                    vectorData texture2 = new vectorData();
                    vectorData boneIndex = new vectorData(); //Rigging
                    vectorData boneWeight = new vectorData();

                    uint vertexVectorFlags = input.ReadUInt32();
                    if (vertexVectorFlags == 0xffffffff) break; //No vertex data
                    for (int entry = 0; entry < objects[index].verticesHeaderEntries; entry++)
                    {
                        uint code = input.ReadUInt32();

                        switch (code)
                        {
                            case codeVertexComponentTypeCount: vertexComponentTypeCount = input.ReadUInt32(); entry++; break;
                            case codeVertexComponentType: vertexComponentType = input.ReadUInt32(); entry++; break;
                            case codeVertexUnknow1: input.ReadUInt32(); entry++; break;
                            case codeVertexUnknow2: input.ReadUInt32(); entry++; break;
                            case codeVertexComponentHeader:
                                vertexFlags = input.ReadUInt32();
                                vertexComponentFlags = input.ReadUInt32();
                                vertexDataPointerOffset = (uint)data.Position - header.descriptionOffset;
                                vertexDataOffset = input.ReadUInt32();
                                vertexComponentIndex = input.ReadUInt32();
                                uint value = input.ReadUInt32();
                                vertexStride = (byte)((value >> 16) & 0xff);
                                vertexComponentCount = (byte)(value >> 28);
                                input.ReadUInt32(); //TODO
                                input.ReadUInt32();
                                entry += 7;
                                break;
                            case codeVertexUnknow3:
                                input.ReadUInt32();
                                input.ReadUInt32();
                                input.ReadUInt32();
                                input.ReadUInt32();
                                input.ReadUInt32();
                                entry += 5;
                                break;
                            case codeVertexPositionOffset:
                                input.ReadSingle();
                                positionOffset.z = input.ReadSingle();
                                positionOffset.y = input.ReadSingle();
                                positionOffset.x = input.ReadSingle();
                                input.ReadSingle();
                                entry += 5;
                                break;
                            case codeVertexColorScale: color.scale = input.ReadSingle(); entry++; break;
                            case codeVertexScale:
                                tangent.scale = input.ReadSingle();
                                normal.scale = input.ReadSingle();
                                position.scale = input.ReadSingle();
                                boneWeight.scale = input.ReadSingle();
                                texture2.scale = input.ReadSingle();
                                texture1.scale = input.ReadSingle();
                                texture0.scale = input.ReadSingle();
                                input.ReadSingle(); //Custom scales
                                input.ReadSingle();
                                input.ReadSingle();
                                entry += 10;
                                break;
                            case codeBlockEnd: entry = (int)objects[index].verticesHeaderEntries; break;
                        }
                    }

                    bool dbgVertexDataOffsetCheck = false;
                    data.Seek(header.relocationTableOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.relocationTableLength / 4; i++)
                    {
                        uint value = input.ReadUInt32();
                        uint offset = (value & 0xffffff) * 4;
                        byte flags = (byte)(value >> 24);

                        if (offset == vertexDataPointerOffset)
                        {
                            switch (header.backwardCompatibility)
                            {
                                case 0x20: vertexDataOffset += header.dataOffset; break;
                                case 0x21: case 0x22:
                                    switch (flags)
                                    {
                                        case 0x4c: vertexDataOffset += header.dataOffset; dbgVertexDataOffsetCheck = true; break;
                                        case 0x56: vertexDataOffset += header.dataExtendedOffset; dbgVertexDataOffsetCheck = true; break;
                                    }
                                    break;
                                default: throw new Exception(String.Format("BCH: Unknow BCH version r{0}, can't parse Vertex Data Location! STOP!", header.version.ToString()));
                            }
                            break;
                        }
                    }
                    if (!dbgVertexDataOffsetCheck) throw new Exception("BCH: Vertex Data Offset pointer not found on Relocation Table! STOP!");

                    List<RenderBase.CustomVertex> vertexBuffer = new List<RenderBase.CustomVertex>();

                    //Faces
                    for (int i = 0; i < objects[index].facesHeaderEntries; i++)
                    {
                        uint baseOffset = objects[index].facesHeaderOffset + ((uint)i * 0x34);
                        data.Seek(baseOffset, SeekOrigin.Begin);
                        RenderBase.OSkinningMode skinningMode = (RenderBase.OSkinningMode)input.ReadUInt16();
                        ushort nodeIdEntries = input.ReadUInt16();
                        List<ushort> nodeList = new List<ushort>();
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

                            switch (code)
                            {
                                case codeFaceUnknow1: input.ReadUInt32(); entry++; break;
                                case codeFaceDataOffset:
                                    faceDataPointerOffset = (uint)data.Position - header.descriptionOffset;
                                    faceDataOffset = input.ReadUInt32();
                                    entry++;
                                    break;
                                case codeFaceDataLength: faceDataEntries = input.ReadUInt32(); entry++; break;
                                case codeFaceUnknow2: input.ReadUInt32(); entry++; break;
                                case codeFaceUnknow3: input.ReadUInt32(); entry++; break;
                                case codeFaceUnknow4: input.ReadUInt32(); entry++; break;
                                case codeFaceUnknow5: input.ReadUInt32(); entry++; break;
                                case codeFaceUnknow6: input.ReadUInt32(); entry++; break;
                                case codeBlockEnd: entry = (int)faceHeaderEntries; break;
                            }
                        }

                        bool dbgFaceDataOffsetCheck = false;
                        byte faceDataFormat = 0;
                        data.Seek(header.relocationTableOffset, SeekOrigin.Begin);
                        for (int j = 0; j < header.relocationTableLength / 4; j++)
                        {
                            uint value = input.ReadUInt32();
                            uint offset = (value & 0xffffff) * 4;
                            byte flags = (byte)(value >> 24);

                            if (offset == faceDataPointerOffset)
                            {
                                switch (header.backwardCompatibility)
                                {
                                    case 0x20: faceDataOffset += header.dataOffset; break;
                                    case 0x21: case 0x22:
                                        switch (flags)
                                        {
                                            case 0x4e: case 0x50:
                                                faceDataOffset += header.dataOffset;
                                                faceDataFormat = flags;
                                                dbgFaceDataOffsetCheck = true;
                                                break;
                                            case 0x58: case 0x5a:
                                                faceDataOffset += header.dataExtendedOffset;
                                                faceDataFormat = flags;
                                                dbgFaceDataOffsetCheck = true;
                                                break;
                                        }
                                        break;
                                    default: throw new Exception(String.Format("BCH: Unknow BCH version r{0}, can't parse Face Data Location! STOP!", header.version.ToString()));
                                }

                                break;
                            }
                        }
                        if (!dbgFaceDataOffsetCheck) throw new Exception("BCH: Face Data Offset pointer not found on Relocation Table! STOP!");

                        #region "Vertex types/quantizations"
                        int uvCount = 0;

                        for (int nibble = 0; nibble < vertexComponentTypeCount + 1; nibble++)
                        {
                            byte value = (byte)((vertexComponentType >> (nibble * 4)) & 0xf);
                            byte vectorFlags = (byte)((vertexFlags >> (nibble * 4)) & 0xf);
                            vectorType type = (vectorType)(vectorFlags >> 2);
                            vectorQuantization quantization = (vectorQuantization)(vectorFlags & 3);

                            //Check if the current index exists on the uint with index nibbles.
                            //It it doesnt, then it will be necessary to add default values (at least for weights and bone index)
                            bool present = false;
                            for (int n2 = 0; n2 < vertexComponentCount; n2++)
                            {
                                if (((vertexComponentIndex >> (n2 * 4)) & 0xf) == nibble)
                                {
                                    present = true;
                                    break;
                                }
                            }

                            switch (value)
                            {
                                case 0: position.type = type; position.quantization = quantization; break;
                                case 1: normal.type = type; normal.quantization = quantization; normal.isPresent = present; normal.isUsed = true; break;
                                case 2: tangent.type = type; tangent.quantization = quantization; break;
                                case 3: color.type = type; color.quantization = quantization; break;
                                case 4: texture0.type = type; texture0.quantization = quantization; uvCount++; break;
                                case 5: texture1.type = type; texture1.quantization = quantization; uvCount++; break;
                                case 6: texture2.type = type; texture2.quantization = quantization; uvCount++; break;
                                case 7: boneIndex.type = type; boneIndex.quantization = quantization; boneIndex.isPresent = present; boneIndex.isUsed = true; break;
                                case 8: boneWeight.type = type; boneWeight.quantization = quantization; boneWeight.isPresent = present; boneWeight.isUsed = true; break;
                            }
                        }

                        obj.texUVCount = uvCount;
                        #endregion

                        obj.hasNormal = normal.isPresent && normal.isUsed;

                        data.Seek(faceDataOffset, SeekOrigin.Begin);
                        for (int faceIndex = 0; faceIndex < faceDataEntries; faceIndex++)
                        {
                            ushort indice;

                            switch (header.backwardCompatibility)
                            {
                                case 0x20:
                                    switch (faceDataFormat)
                                    {
                                        case 0x50: indice = input.ReadUInt16(); break;
                                        case 0x52: indice = input.ReadByte(); break;
                                        default: throw new Exception("BCH: Unknow Face Data Format! STOP!");
                                    }
                                    break;
                                case 0x21: case 0x22:
                                    switch (faceDataFormat)
                                    {
                                        case 0x4e: case 0x58: indice = input.ReadUInt16(); break;
                                        case 0x50: case 0x5a: indice = input.ReadByte(); break;
                                        default: throw new Exception("BCH: Unknow Face Data Format! STOP!");
                                    }
                                    break;
                                default: throw new Exception(String.Format("BCH: Unknow BCH version r{0}, can't parse Face Data Format! STOP!", header.version.ToString()));
                            }

                            long dataPosition = data.Position;
                            long vertexOffset = vertexDataOffset + (indice * vertexStride);
                            data.Seek(vertexOffset, SeekOrigin.Begin);
                            
                            RenderBase.OVertex vertex = new RenderBase.OVertex();
                            vertex.diffuseColor = 0xffffffff;

                            for (int component = 0; component < vertexComponentCount; component++)
                            {
                                int componentIndex = (int)((vertexComponentIndex >> (component * 4)) & 0xf);
                                byte value = (byte)((vertexComponentType >> (componentIndex * 4)) & 0xf);

                                switch (value)
                                {
                                    case 0: //Position
                                        RenderBase.OVector4 positionVector = getVector(input, position.quantization, position.type);
                                        float x = (positionVector.x * position.scale) + positionOffset.x;
                                        float y = (positionVector.y * position.scale) + positionOffset.y;
                                        float z = (positionVector.z * position.scale) + positionOffset.z;
                                        vertex.position = new RenderBase.OVector3(x, y, z);
                                        break;
                                    case 1: //Normal
                                        RenderBase.OVector4 normalVector = getVector(input, normal.quantization, normal.type);
                                        vertex.normal = new RenderBase.OVector3(normalVector.x * normal.scale, normalVector.y * normal.scale, normalVector.z * normal.scale);
                                        break;
                                    case 2: //Tangent
                                        RenderBase.OVector4 tangentVector = getVector(input, tangent.quantization, tangent.type);
                                        vertex.tangent = new RenderBase.OVector3(tangentVector.x * tangent.scale, tangentVector.y * tangent.scale, tangentVector.z * tangent.scale);
                                        break;
                                    case 3: //Color
                                        RenderBase.OVector4 colorRgba = getVector(input, color.quantization, color.type);
                                        uint r = (uint)((colorRgba.x * color.scale) * 0xff);
                                        uint g = (uint)((colorRgba.y * color.scale) * 0xff);
                                        uint b = (uint)((colorRgba.z * color.scale) * 0xff);
                                        uint a = (uint)((colorRgba.w * color.scale) * 0xff);
                                        vertex.diffuseColor = b | (g << 8) | (r << 16) | (a << 24);
                                        break;
                                    case 4: //Texture 0
                                        RenderBase.OVector4 tex0Vector = getVector(input, texture0.quantization, texture0.type);
                                        vertex.texture0 = new RenderBase.OVector2(tex0Vector.x * texture0.scale, tex0Vector.y * texture0.scale);
                                        break;
                                    case 5: //Texture 1
                                        RenderBase.OVector4 tex1Vector = getVector(input, texture1.quantization, texture1.type);
                                        vertex.texture1 = new RenderBase.OVector2(tex1Vector.x * texture1.scale, tex1Vector.y * texture1.scale);
                                        break;
                                    case 6: //Texture 2
                                        RenderBase.OVector4 tex2Vector = getVector(input, texture2.quantization, texture2.type);
                                        vertex.texture2 = new RenderBase.OVector2(tex2Vector.x * texture2.scale, tex2Vector.y * texture2.scale);
                                        break;
                                    case 7: //Bone Index
                                        RenderBase.OVector4 bone = getVector(input, boneIndex.quantization, boneIndex.type);
                                        vertex.addNode(nodeList[(int)bone.x]);
                                        if (boneIndex.type > vectorType.vector1) vertex.addNode(nodeList[(int)bone.y]);
                                        if (boneIndex.type > vectorType.vector2) vertex.addNode(nodeList[(int)bone.z]);
                                        if (boneIndex.type > vectorType.vector3) vertex.addNode(nodeList[(int)bone.w]);
                                        break;
                                    case 8: //Bone Weight
                                        RenderBase.OVector4 weight = getVector(input, boneWeight.quantization, boneWeight.type);
                                        vertex.addWeight(weight.x * boneWeight.scale);
                                        if (boneWeight.type > vectorType.vector1) vertex.addWeight(weight.y * boneWeight.scale);
                                        if (boneWeight.type > vectorType.vector2) vertex.addWeight(weight.z * boneWeight.scale);
                                        if (boneWeight.type > vectorType.vector3) vertex.addWeight(weight.w * boneWeight.scale);
                                        break;
                                }
                            }

                            if (!boneIndex.isPresent && boneIndex.isUsed) vertex.addNode(nodeList[0]);
                            if (!boneWeight.isPresent && boneWeight.isUsed) vertex.addWeight(1);

                            if ((skinningMode == RenderBase.OSkinningMode.rigidSkinning || skinningMode == RenderBase.OSkinningMode.none) && vertex.node.Count > 0)
                            {
                                //Note: Rigid skinning can have only one bone per vertex
                                //Note2: Vertex with Rigid skinning seems to be always have meshes centered, so is necessary to make them follow the skeleton
                                vertex.position = RenderBase.OVector3.transform(vertex.position, skeletonTransform[vertex.node[0]]);
                            }

                            //Like a Bounding Box, used to calculate the proportions of the mesh on the Viewport
                            if (vertex.position.x < models.minVector.x) models.minVector.x = vertex.position.x;
                            else if (vertex.position.x > models.maxVector.x) models.maxVector.x = vertex.position.x;
                            else if (vertex.position.y < models.minVector.y) models.minVector.y = vertex.position.y;
                            else if (vertex.position.y > models.maxVector.y) models.maxVector.y = vertex.position.y;
                            else if (vertex.position.z < models.minVector.z) models.minVector.z = vertex.position.z;
                            else if (vertex.position.z > models.maxVector.z) models.maxVector.z = vertex.position.z;

                            obj.addVertex(vertex);
                            vertexBuffer.Add(RenderBase.convertVertex(vertex));

                            data.Seek(dataPosition, SeekOrigin.Begin);
                        }
                    }

                    obj.renderBuffer = vertexBuffer.ToArray();
                    model.addObject(obj);
                }

                for (int index = 0; index < objectsHeader.skeletonEntries; index++)
                {
                    scaleSkeleton(model.skeleton, index, index);
                }

                models.addModel(model);
            }

            data.Close();
            data.Dispose();

            return models;
        }

        /// <summary>
        ///     Scales all child bones of the current bone.
        /// </summary>
        /// <param name="skeleton">The complete skeleton</param>
        /// <param name="index">Index of the child bone</param>
        /// <param name="scale">Index of the parent bone</param>
        private static void scaleSkeleton(List<RenderBase.OBone> skeleton, int index, int parentIndex)
        {
            if (index != parentIndex)
            {
                skeleton[parentIndex].absoluteScale.x *= skeleton[index].scale.x;
                skeleton[parentIndex].absoluteScale.y *= skeleton[index].scale.y;
                skeleton[parentIndex].absoluteScale.z *= skeleton[index].scale.z;

                skeleton[parentIndex].translation.x *= skeleton[index].scale.x;
                skeleton[parentIndex].translation.y *= skeleton[index].scale.y;
                skeleton[parentIndex].translation.z *= skeleton[index].scale.z;
            }

            if (skeleton[index].parentId > -1) scaleSkeleton(skeleton, skeleton[index].parentId, parentIndex);
        }

        /// <summary>
        ///     Transforms a Skeleton from relative to absolute positions.
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the bone to convert</param>
        /// <param name="target">Target matrix to save bone transformation</param>
        private static void transformSkeleton(List<RenderBase.OBone> skeleton, int index, ref RenderBase.OMatrix target)
        {
            target *= RenderBase.OMatrix.scale(skeleton[index].scale);
            target *= RenderBase.OMatrix.rotateX(skeleton[index].rotation.x);
            target *= RenderBase.OMatrix.rotateY(skeleton[index].rotation.y);
            target *= RenderBase.OMatrix.rotateZ(skeleton[index].rotation.z);
            target *= RenderBase.OMatrix.translate(skeleton[index].translation);
            if (skeleton[index].parentId > -1) transformSkeleton(skeleton, skeleton[index].parentId, ref target);
        }

        /// <summary>
        ///     Gets a Vector4 from Data.
        ///     Number of used elements of the Vector4 will depend on the vector type.
        /// </summary>
        /// <param name="input">BCH reader</param>
        /// <param name="quantization">Quantization used on the vector (ubyte, byte, short or float)</param>
        /// <param name="type">Size of the vector (vector1, 2, 3 or 4)</param>
        /// <returns></returns>
        private static RenderBase.OVector4 getVector(BinaryReader input, vectorQuantization quantization, vectorType type)
        {
            RenderBase.OVector4 output = new RenderBase.OVector4();

            switch (quantization)
            {
                case vectorQuantization.qByte:
                    output.x = (sbyte)input.ReadByte();
                    if (type > vectorType.vector1) output.y = (sbyte)input.ReadByte();
                    if (type > vectorType.vector2) output.z = (sbyte)input.ReadByte();
                    if (type > vectorType.vector3) output.w = (sbyte)input.ReadByte();
                    break;
                case vectorQuantization.qUByte:
                    output.x = input.ReadByte();
                    if (type > vectorType.vector1) output.y = input.ReadByte();
                    if (type > vectorType.vector2) output.z = input.ReadByte();
                    if (type > vectorType.vector3) output.w = input.ReadByte();
                    break;
                case vectorQuantization.qShort:
                    output.x = input.ReadInt16();
                    if (type > vectorType.vector1) output.y = input.ReadInt16();
                    if (type > vectorType.vector2) output.z = input.ReadInt16();
                    if (type > vectorType.vector3) output.w = input.ReadInt16();
                    break;
                case vectorQuantization.qFloat:
                    output.x = input.ReadSingle();
                    if (type > vectorType.vector1) output.y = input.ReadSingle();
                    if (type > vectorType.vector2) output.z = input.ReadSingle();
                    if (type > vectorType.vector3) output.w = input.ReadSingle();
                    break;
            }

            return output;
        }

        /// <summary>
        ///     Reads a Color from the Data.
        /// </summary>
        /// <param name="input">BCH reader</param>
        /// <returns></returns>
        private static Color getColor(BinaryReader input)
        {
            byte r = (byte)input.ReadByte();
            byte g = (byte)input.ReadByte();
            byte b = (byte)input.ReadByte();
            byte a = (byte)input.ReadByte();

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        ///     Reads a Color stored in Float format from the Data.
        /// </summary>
        /// <param name="input">BCH reader</param>
        /// <returns></returns>
        private static Color getColorFloat(BinaryReader input)
        {
            byte r = (byte)(input.ReadSingle() * 0xff);
            byte g = (byte)(input.ReadSingle() * 0xff);
            byte b = (byte)(input.ReadSingle() * 0xff);
            byte a = (byte)(input.ReadSingle() * 0xff);

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        ///     Search a string on the Relocation table.
        ///     If it is present, read the string from the Data and return it.
        ///     If it doesn't exist on the Relocation table, return an empty string.
        ///     Position advances 4 bytes only.
        /// </summary>
        /// <param name="input">BCH reader</param>
        /// <param name="header">BCH header</param>
        /// <returns></returns>
        private static string readString(BinaryReader input, bchHeader header)
        {
            long dataPosition = input.BaseStream.Position;
            uint stringOffset = input.ReadUInt32();
            input.BaseStream.Seek(header.relocationTableOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.relocationTableLength / 4; i++)
            {
                uint value = input.ReadUInt32();
                uint offset = value & 0xffffff;
                byte flags = (byte)(value >> 24);

                if (flags == 2 && offset == (dataPosition - header.mainHeaderOffset))
                {
                    input.BaseStream.Seek(dataPosition + 4, SeekOrigin.Begin);
                    return IOUtils.readString(input, stringOffset + header.stringTableOffset);
                }
            }
            input.BaseStream.Seek(dataPosition + 4, SeekOrigin.Begin);
            return null;
        }

        /// <summary>
        ///     Gets an Animation Key frame from the BCH file.
        ///     The Reader position must be set to the beggining of the Key Frame Data.
        /// </summary>
        /// <param name="input">The BCH file Reader</param>
        /// <param name="header">The BCH file header</param>
        /// <returns></returns>
        private static RenderBase.OAnimationKeyFrame getAnimationKeyFrame(BinaryReader input, bchHeader header)
        {
            RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

            frame.exists = true;
            frame.startFrame = input.ReadSingle();
            frame.endFrame = input.ReadSingle();

            uint frameFlags = input.ReadUInt32();
            frame.preRepeat = (RenderBase.ORepeatMethod)(frameFlags & 0xf);
            frame.postRepeat = (RenderBase.ORepeatMethod)((frameFlags >> 8) & 0xf);

            frameFlags = input.ReadUInt32();
            frame.interpolation = (RenderBase.OInterpolationMode)(frameFlags & 0xf);
            uint entryFormat = (frameFlags >> 8) & 0xff;
            uint entries = frameFlags >> 16;

            float maxValue = 0;
            uint rawMaxValue = input.ReadUInt32();
            float minValue = input.ReadSingle();
            float frameScale = input.ReadSingle();
            float valueScale = input.ReadSingle(); //Probably wrong

            switch (entryFormat)
            {
                case 1: case 7: maxValue = getCustomFloat(rawMaxValue, 107); break;
                case 2: case 4: maxValue = getCustomFloat(rawMaxValue, 111); break;
                case 5: maxValue = getCustomFloat(rawMaxValue, 115); break;
            }
            maxValue = minValue + maxValue;

            float interpolation;
            uint value;
            uint rawDataOffset = input.ReadUInt32() + header.mainHeaderOffset;
            input.BaseStream.Seek(rawDataOffset, SeekOrigin.Begin);
            for (int k = 0; k < entries; k++)
            {
                RenderBase.OInterpolationFloat keyFrame = new RenderBase.OInterpolationFloat();

                switch (frame.interpolation)
                {
                    case RenderBase.OInterpolationMode.step:
                    case RenderBase.OInterpolationMode.linear:
                        switch (entryFormat)
                        {
                            case 6:
                                keyFrame.frame = input.ReadSingle();
                                keyFrame.value = input.ReadSingle();
                                break;
                            case 7:
                                value = input.ReadUInt32();
                                interpolation = (float)(value >> 12) / 0x100000;
                                keyFrame.frame = frame.startFrame + (float)(value & 0xfff);
                                keyFrame.value = (minValue * (1 - interpolation) + maxValue * interpolation);
                                break;
                            default:
                                Debug.WriteLine(String.Format("[BCH] Animation: Unsupported quantization format {0} on Linear...", entryFormat));
                                frame.exists = false;
                                break;
                        }

                        break;
                    case RenderBase.OInterpolationMode.hermite:
                        switch (entryFormat)
                        {
                            case 0:
                                keyFrame.frame = input.ReadSingle();
                                keyFrame.value = input.ReadSingle();
                                keyFrame.inSlope = input.ReadSingle();
                                keyFrame.outSlope = input.ReadSingle();
                                break;
                            case 1:
                                value = input.ReadUInt32();
                                keyFrame.frame = frame.startFrame + (float)(value & 0xfff);
                                interpolation = (float)(value >> 12) / 0x100000;
                                keyFrame.value = (minValue * (1 - interpolation) + maxValue * interpolation);
                                keyFrame.inSlope = (float)input.ReadInt16() / 256;
                                keyFrame.outSlope = (float)input.ReadInt16() / 256;
                                break;
                            case 2:
                                value = input.ReadUInt32();
                                uint inSlopeLow = value >> 24;
                                keyFrame.frame = frame.startFrame + (float)(value & 0xff);
                                interpolation = (float)((value >> 8) & 0xffff) / 0x10000;
                                keyFrame.value = (minValue * (1 - interpolation) + maxValue * interpolation);
                                value = input.ReadUInt16();
                                keyFrame.inSlope = get12bValue((int)(((value & 0xf) << 8) | inSlopeLow));
                                keyFrame.outSlope = get12bValue((int)((value >> 4) & 0xfff));
                                break;
                            case 3:
                                keyFrame.frame = input.ReadSingle();
                                keyFrame.value = input.ReadSingle();
                                keyFrame.inSlope = input.ReadSingle();
                                keyFrame.outSlope = keyFrame.inSlope;
                                break;
                            case 4:
                                //TODO: Implement support for different inSlope and outSlope
                                //Note: they are added with another frame with same number, but the inSlope is actually the outSlope
                                keyFrame.frame = (float)input.ReadInt16() / 32;
                                interpolation = (float)input.ReadUInt16() / 0x10000;
                                keyFrame.value = (minValue * (1 - interpolation) + maxValue * interpolation);
                                keyFrame.inSlope = (float)input.ReadInt16() / 256;
                                keyFrame.outSlope = keyFrame.inSlope;
                                break;
                            case 5:
                                value = input.ReadUInt32();
                                keyFrame.frame = frame.startFrame + ((float)(value & 0xff) * frameScale);
                                interpolation = (float)((value >> 8) & 0xfff) / 0x1000;
                                keyFrame.value = (minValue * (1 - interpolation) + maxValue * interpolation);
                                keyFrame.inSlope = get12bValue((int)(value >> 20));
                                keyFrame.outSlope = keyFrame.inSlope;
                                break;
                            default:
                                Debug.WriteLine(String.Format("[BCH] Animation: Unsupported quantization format {0} on Hermite...", entryFormat));
                                frame.exists = false;
                                break;
                        }

                        break;
                }

                frame.keyFrames.Add(keyFrame);
            }

            return frame;
        }

        /// <summary>
        ///     Gets an Animation Key frame from the BCH file.
        ///     The Reader position must be set to the beggining of the Key Frame Data.
        ///     This function should be ONLY used with Bool values, since they're stored in a different way.
        /// </summary>
        /// <param name="input">The BCH file Reader</param>
        /// <param name="header">The BCH file header</param>
        /// <returns></returns>
        private static RenderBase.OAnimationKeyFrame getAnimationKeyFrameBool(BinaryReader input, bchHeader header)
        {
            RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

            frame.exists = true;
            frame.startFrame = 0;

            frame.defaultValue = ((input.ReadUInt32() >> 24) & 1) > 0;
            input.ReadUInt32();
            uint offset = input.ReadUInt32() + header.mainHeaderOffset;
            uint entries = input.ReadUInt32();
            frame.endFrame = entries - 1;

            input.BaseStream.Seek(offset, SeekOrigin.Begin);
            uint mask = 1;
            byte value = input.ReadByte();
            for (int i = 0; i < entries; i++)
            {
                frame.keyFrames.Add(new RenderBase.OInterpolationFloat((value & mask) > 0, i));
                if ((mask <<= 1) > 0x80)
                {
                    value = input.ReadByte();
                    mask = 1;
                }
            }

            return frame;
        }

        /// <summary>
        ///     Gets a custom Float value found on BCH animations.
        /// </summary>
        /// <param name="value">Raw value of the floating point value</param>
        /// <param name="exponentBias">The bias used on the exponent</param>
        /// <returns></returns>
        private static float getCustomFloat(uint value, int exponentBias)
        {
            float mantissa = 1.0f;
            float m = 0.5f;

            uint rawMantissa = value & 0x7fffff;
            int rawExponent = (int)(value >> 23) - exponentBias;

            for (int bit = 22; bit >= 0; bit--)
            {
                if ((rawMantissa & (1 << bit)) > 0) mantissa += m;
                m *= 0.5f;
            }

            return (float)(Math.Pow(2, rawExponent) * mantissa);
        }

        /// <summary>
        ///     Gets the custom quantized Floating point value found on BCH animations.
        /// </summary>
        /// <param name="value">Raw value</param>
        /// <returns></returns>
        private static float get12bValue(int value)
        {
            if ((value & 0x800) > 0) value -= 0x1000;
            return (float)value / 32;
        }

        #endregion

    }
}
