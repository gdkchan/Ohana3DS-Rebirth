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

using Ohana3DS_Rebirth.Ohana.PICA200;

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
            public uint gpuCommandsOffset;
            public uint dataOffset;
            public uint dataExtendedOffset;
            public uint relocationTableOffset;

            public uint mainHeaderLength;
            public uint stringTableLength;
            public uint gpuCommandsLength;
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

        private struct bchModelHeader
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
            public string modelName;
            public uint objectsNodeNameEntries;
            public uint objectsNodeNameOffset;
            public uint metaDataPointerOffset;
        }

        private struct bchObjectEntry
        {
            public ushort materialId;
            public bool isSilhouette;
            public ushort nodeId;
            public ushort renderPriority;
            public uint vshAttributesBufferCommandsOffset;
            public uint vshAttributesBufferCommandsWordCount;
            public uint facesHeaderOffset;
            public uint facesHeaderEntries;
            public uint vshExtraAttributesBufferCommandsOffset;
            public uint vshExtraAttributesBufferCommandsWordCount;
            public RenderBase.OVector3 centerVector;
            public uint flagsOffset;
            public uint boundingBoxOffset;
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
        /// <param name="data">Memory Stream of the BCH file. The Stream will not be usable after</param>
        /// <returns></returns>
        public static RenderBase.OModelGroup load(MemoryStream data)
        {
            BinaryReader input = new BinaryReader(data);
            BinaryWriter writer = new BinaryWriter(data);

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
            header.gpuCommandsOffset = input.ReadUInt32();
            header.dataOffset = input.ReadUInt32();
            if (header.backwardCompatibility > 0x20) header.dataExtendedOffset = input.ReadUInt32();
            header.relocationTableOffset = input.ReadUInt32();

            header.mainHeaderLength = input.ReadUInt32();
            header.stringTableLength = input.ReadUInt32();
            header.gpuCommandsLength = input.ReadUInt32();
            header.dataLength = input.ReadUInt32();
            if (header.backwardCompatibility > 0x20) header.dataExtendedLength = input.ReadUInt32();
            header.relocationTableLength = input.ReadUInt32();
            header.uninitializedDataSectionLength = input.ReadUInt32();
            header.uninitializedDescriptionSectionLength = input.ReadUInt32();

            header.flags = input.ReadUInt16();
            header.addressCount = input.ReadUInt16();

            //Transform relative offsets to absolute offsets, also add extra bits if necessary.
            //The game does this on RAM after the BCH is loaded, so offsets to data is absolute and points to VRAM.
            for (uint o = header.relocationTableOffset; o < header.relocationTableOffset + header.relocationTableLength; o += 4)
            {
                data.Seek(o, SeekOrigin.Begin);
                uint value = input.ReadUInt32();
                uint offset = value & 0xffffff;
                byte flags = (byte)(value >> 24);

                switch (flags)
                {
                    case 0:
                        data.Seek((offset * 4) + header.mainHeaderOffset, SeekOrigin.Begin);
                        writer.Write(peek(input) + header.mainHeaderOffset);
                        break;
                    case 2:
                        data.Seek(offset + header.mainHeaderOffset, SeekOrigin.Begin);
                        writer.Write(peek(input) + header.stringTableOffset);
                        break;
                    case 4:
                        data.Seek((offset * 4) + header.mainHeaderOffset, SeekOrigin.Begin);
                        writer.Write(peek(input) + header.gpuCommandsOffset);
                        break;
                }

                //The moron that designed the format used different flags on different versions, instead of keeping compatibility.
                switch (header.backwardCompatibility)
                {
                    case 7: //Pokémon X/Y
                        switch (flags)
                        {
                            case 0x48: //Texture
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(peek(input) + header.dataOffset);
                                break;
                            case 0x4e: //Index 16 bits mode
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(((peek(input) + header.dataOffset) & 0x7fffffff) | 0x80000000);
                                break;
                            case 0x50: //Index 8 bits mode
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write((peek(input) + header.dataOffset) & 0x7fffffff);
                                break;
                            case 0x4c: //Vertex
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(peek(input) + header.dataOffset);
                                break;
                        }
                        break;
                    case 0x20: //Senran Kagura (a few models only)
                        switch (flags)
                        {
                            case 0x4a: //Texture
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(peek(input) + header.dataOffset);
                                break;
                            case 0x50: //Index 16 bits mode
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(((peek(input) + header.dataOffset) & 0x7fffffff) | 0x80000000);
                                break;
                            case 0x52: //Index 8 bits mode
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write((peek(input) + header.dataOffset) & 0x7fffffff);
                                break;
                            case 0x4e: //Vertex
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(peek(input) + header.dataOffset);
                                break;
                        }
                        break;
                    case 0x21:
                    case 0x22:
                    case 0x23: //Codename STEAM/ORAS/Atelier Rorona/all newer games...
                        switch (flags)
                        {
                            case 0x4a: //Texture
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(peek(input) + header.dataOffset);
                                break;
                            case 0x4e: //Index 16 bits mode relative to Data Offset
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(((peek(input) + header.dataOffset) & 0x7fffffff) | 0x80000000);
                                break;
                            case 0x58: //Index 16 bits mode relative to Data Extended Offset
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(((peek(input) + header.dataExtendedOffset) & 0x7fffffff) | 0x80000000);
                                break;
                            case 0x50: //Index 8 bits mode relative to Data Offset
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write((peek(input) + header.dataOffset) & 0x7fffffff);
                                break;
                            case 0x5a: //Index 8 bits mode relative to Data Extended Offset
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write((peek(input) + header.dataExtendedOffset) & 0x7fffffff);
                                break;
                            case 0x4c: //Vertex relative to Data Offset
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(peek(input) + header.dataOffset);
                                break;
                            case 0x56: //Vertex relative to Data Extended Offset
                                data.Seek((offset * 4) + header.gpuCommandsOffset, SeekOrigin.Begin);
                                writer.Write(peek(input) + header.dataExtendedOffset);
                                break;
                        }
                        break;
                    default: throw new Exception(String.Format("BCH: Unknow BCH version r{0} / Compatibility: {1}, can't relocate offsets! STOP!", header.version, header.backwardCompatibility));
                }
            }

            //Content header
            data.Seek(header.mainHeaderOffset, SeekOrigin.Begin);
            bchContentHeader contentHeader = new bchContentHeader
            {
                modelsPointerTableOffset = input.ReadUInt32(),
                modelsPointerTableEntries = input.ReadUInt32(),
                modelsNameOffset = input.ReadUInt32(),
                materialsPointerTableOffset = input.ReadUInt32(),
                materialsPointerTableEntries = input.ReadUInt32(),
                materialsNameOffset = input.ReadUInt32(),
                shadersPointerTableOffset = input.ReadUInt32(),
                shadersPointerTableEntries = input.ReadUInt32(),
                shadersNameOffset = input.ReadUInt32(),
                texturesPointerTableOffset = input.ReadUInt32(),
                texturesPointerTableEntries = input.ReadUInt32(),
                texturesNameOffset = input.ReadUInt32(),
                materialsLUTPointerTableOffset = input.ReadUInt32(),
                materialsLUTPointerTableEntries = input.ReadUInt32(),
                materialsLUTNameOffset = input.ReadUInt32(),
                lightsPointerTableOffset = input.ReadUInt32(),
                lightsPointerTableEntries = input.ReadUInt32(),
                lightsNameOffset = input.ReadUInt32(),
                camerasPointerTableOffset = input.ReadUInt32(),
                camerasPointerTableEntries = input.ReadUInt32(),
                camerasNameOffset = input.ReadUInt32(),
                fogsPointerTableOffset = input.ReadUInt32(),
                fogsPointerTableEntries = input.ReadUInt32(),
                fogsNameOffset = input.ReadUInt32(),
                skeletalAnimationsPointerTableOffset = input.ReadUInt32(),
                skeletalAnimationsPointerTableEntries = input.ReadUInt32(),
                skeletalAnimationsNameOffset = input.ReadUInt32(),
                materialAnimationsPointerTableOffset = input.ReadUInt32(),
                materialAnimationsPointerTableEntries = input.ReadUInt32(),
                materialAnimationsNameOffset = input.ReadUInt32(),
                visibilityAnimationsPointerTableOffset = input.ReadUInt32(),
                visibilityAnimationsPointerTableEntries = input.ReadUInt32(),
                visibilityAnimationsNameOffset = input.ReadUInt32(),
                lightAnimationsPointerTableOffset = input.ReadUInt32(),
                lightAnimationsPointerTableEntries = input.ReadUInt32(),
                lightAnimationsNameOffset = input.ReadUInt32(),
                cameraAnimationsPointerTableOffset = input.ReadUInt32(),
                cameraAnimationsPointerTableEntries = input.ReadUInt32(),
                cameraAnimationsNameOffset = input.ReadUInt32(),
                fogAnimationsPointerTableOffset = input.ReadUInt32(),
                fogAnimationsPointerTableEntries = input.ReadUInt32(),
                fogAnimationsNameOffset = input.ReadUInt32(),
                scenePointerTableOffset = input.ReadUInt32(),
                scenePointerTableEntries = input.ReadUInt32(),
                sceneNameOffset = input.ReadUInt32()
            };

            //Shaders
            for (int index = 0; index < contentHeader.shadersPointerTableEntries; index++)
            {
                data.Seek(contentHeader.shadersPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                uint shaderDataOffset = input.ReadUInt32();
                uint shaderDataLength = input.ReadUInt32();
            }

            //Textures
            for (int index = 0; index < contentHeader.texturesPointerTableEntries; index++)
            {
                data.Seek(contentHeader.texturesPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                uint textureCommandsOffset = input.ReadUInt32();
                uint textureCommandsWordCount = input.ReadUInt32();
                data.Seek(0x14, SeekOrigin.Current);
                string textureName = readString(input);

                data.Seek(textureCommandsOffset, SeekOrigin.Begin);
                PICACommandReader textureCommands = new PICACommandReader(data, textureCommandsWordCount);

                //Note: It have textures for the 3 texture units.
                //The other texture units are used with textureCoordinate1 and 2.
                Bitmap texture = null;
                data.Seek(textureCommands.getTexUnit0Address(), SeekOrigin.Begin);
                Size textureSize = textureCommands.getTexUnit0Size();
                byte[] buffer = new byte[textureSize.Width * textureSize.Height * 4];
                input.Read(buffer, 0, buffer.Length);
                texture = TextureCodec.decode(buffer, textureSize.Width, textureSize.Height, textureCommands.getTexUnit0Format());

                models.addTexture(new RenderBase.OTexture(texture, textureName));
            }

            //LookUp Tables
            for (int index = 0; index < contentHeader.materialsLUTPointerTableEntries; index++)
            {
                data.Seek(contentHeader.materialsLUTPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                input.ReadUInt32();
                uint samplersCount = input.ReadUInt32();
                string name = readString(input);

                RenderBase.OLookUpTable table = new RenderBase.OLookUpTable();
                table.name = name;
                for (int i = 0; i < samplersCount; i++)
                {
                    RenderBase.OLookUpTableSampler sampler = new RenderBase.OLookUpTableSampler();

                    input.ReadUInt32();
                    uint tableOffset = input.ReadUInt32();
                    uint tableWordCount = input.ReadUInt32();
                    sampler.name = readString(input);

                    long dataPosition = data.Position;
                    data.Seek(tableOffset, SeekOrigin.Begin);
                    PICACommandReader lutCommands = new PICACommandReader(data, tableWordCount);
                    sampler.table = lutCommands.getFSHLookUpTable();
                    table.sampler.Add(sampler);

                    data.Seek(dataPosition, SeekOrigin.Begin);
                }

                models.addLUT(table);
            }

            //Lights
            for (int index = 0; index < contentHeader.lightsPointerTableEntries; index++)
            {
                data.Seek(contentHeader.lightsPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OLight light = new RenderBase.OLight();
                light.name = readString(input);
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

                        light.distanceSampler.tableName = readString(input);
                        light.distanceSampler.samplerName = readString(input);

                        light.angleSampler.tableName = readString(input);
                        light.angleSampler.samplerName = readString(input);
                        break;
                }

                models.addLight(light);
            }

            //Cameras
            for (int index = 0; index < contentHeader.camerasPointerTableEntries; index++)
            {
                data.Seek(contentHeader.camerasPointerTableOffset + (index * 4), SeekOrigin.Begin);
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OCamera camera = new RenderBase.OCamera();
                camera.name = readString(input);
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
                uint viewOffset = input.ReadUInt32();
                uint projectionOffset = input.ReadUInt32();

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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OFog fog = new RenderBase.OFog();
                fog.name = readString(input);
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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OSkeletalAnimation skeletalAnimation = new RenderBase.OSkeletalAnimation();

                skeletalAnimation.name = readString(input);
                uint animationFlags = input.ReadUInt32();
                skeletalAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                skeletalAnimation.frameSize = input.ReadSingle();
                uint boneTableOffset = input.ReadUInt32();
                uint boneTableEntries = input.ReadUInt32();

                for (int i = 0; i < boneTableEntries; i++)
                {
                    data.Seek(boneTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32();

                    RenderBase.OSkeletalAnimationBone bone = new RenderBase.OSkeletalAnimationBone();

                    data.Seek(offset, SeekOrigin.Begin);
                    bone.name = readString(input);
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
                                        uint frameOffset = input.ReadUInt32();
                                        data.Seek(frameOffset, SeekOrigin.Begin);
                                        frame = getAnimationKeyFrame(input);
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
                            uint rotationOffset = input.ReadUInt32();
                            uint translationOffset = input.ReadUInt32();

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
                                    uint rotationDataOffset = input.ReadUInt32();
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
                                    uint translationDataOffset = input.ReadUInt32();
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
                            uint matrixOffset = input.ReadUInt32();
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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OMaterialAnimation materialAnimation = new RenderBase.OMaterialAnimation();

                materialAnimation.name = readString(input);
                uint animationFlags = input.ReadUInt32();
                materialAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                materialAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32();
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                uint textureNameTableOffset = input.ReadUInt32();
                uint textureNameTableEntries = input.ReadUInt32();

                data.Seek(textureNameTableOffset, SeekOrigin.Begin);
                for (int i = 0; i < textureNameTableEntries; i++)
                {
                    string name = readString(input);
                    materialAnimation.textureName.Add(name);
                }

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32();

                    RenderBase.OMaterialAnimationData animationData = new RenderBase.OMaterialAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input);
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
                                uint frameOffset = input.ReadUInt32();
                                data.Seek(frameOffset, SeekOrigin.Begin);
                                frame = getAnimationKeyFrame(input);
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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OVisibilityAnimation visibilityAnimation = new RenderBase.OVisibilityAnimation();

                visibilityAnimation.name = readString(input);
                uint animationFlags = input.ReadUInt32();
                visibilityAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                visibilityAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32();
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                input.ReadUInt32();

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32();

                    RenderBase.OVisibilityAnimationData animationData = new RenderBase.OVisibilityAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input);
                    uint animationTypeFlags = input.ReadUInt32();
                    uint flags = input.ReadUInt32();

                    RenderBase.OSegmentType segmentType = (RenderBase.OSegmentType)((animationTypeFlags >> 16) & 0xf);
                    if (segmentType == RenderBase.OSegmentType.boolean)
                    {
                        RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();
                        if (segmentType == RenderBase.OSegmentType.boolean) frame = getAnimationKeyFrameBool(input);
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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OLightAnimation lightAnimation = new RenderBase.OLightAnimation();

                lightAnimation.name = readString(input);
                uint animationFlags = input.ReadUInt32();
                lightAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                lightAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32();
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                uint typeFlags = input.ReadUInt32();
                lightAnimation.lightType = (RenderBase.OLightType)((typeFlags & 3) - 1);
                lightAnimation.lightUse = (RenderBase.OLightUse)((typeFlags >> 2) & 3);

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32();

                    RenderBase.OLightAnimationData animationData = new RenderBase.OLightAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input);
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
                                frame = getAnimationKeyFrameBool(input);
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
                                    uint frameOffset = input.ReadUInt32();
                                    data.Seek(frameOffset, SeekOrigin.Begin);
                                    frame = getAnimationKeyFrame(input);
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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OCameraAnimation cameraAnimation = new RenderBase.OCameraAnimation();

                cameraAnimation.name = readString(input);
                uint animationFlags = input.ReadUInt32();
                cameraAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                cameraAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32();
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                uint modeFlags = input.ReadUInt32();
                cameraAnimation.viewMode = (RenderBase.OCameraView)(modeFlags & 0xf);
                cameraAnimation.projectionMode = (RenderBase.OCameraProjection)((modeFlags >> 8) & 0xf);

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32();

                    RenderBase.OCameraAnimationData animationData = new RenderBase.OCameraAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input);
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
                                uint frameOffset = input.ReadUInt32();
                                data.Seek(frameOffset, SeekOrigin.Begin);
                                frame = getAnimationKeyFrame(input);
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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OFogAnimation fogAnimation = new RenderBase.OFogAnimation();

                fogAnimation.name = readString(input);
                uint animationFlags = input.ReadUInt32();
                fogAnimation.loopMode = (RenderBase.OLoopMode)(animationFlags & 1);
                fogAnimation.frameSize = input.ReadSingle();
                uint dataTableOffset = input.ReadUInt32();
                uint dataTableEntries = input.ReadUInt32();
                input.ReadUInt32();
                input.ReadUInt32();

                for (int i = 0; i < dataTableEntries; i++)
                {
                    data.Seek(dataTableOffset + (i * 4), SeekOrigin.Begin);
                    uint offset = input.ReadUInt32();

                    RenderBase.OFogAnimationData animationData = new RenderBase.OFogAnimationData();

                    data.Seek(offset, SeekOrigin.Begin);
                    animationData.name = readString(input);
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
                                uint frameOffset = input.ReadUInt32();
                                data.Seek(frameOffset, SeekOrigin.Begin);
                                frame = getAnimationKeyFrame(input);
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
                uint dataOffset = input.ReadUInt32();
                data.Seek(dataOffset, SeekOrigin.Begin);

                RenderBase.OScene scene = new RenderBase.OScene();
                scene.name = readString(input);

                uint cameraReferenceOffset = input.ReadUInt32();
                uint cameraReferenceEntries = input.ReadUInt32();
                uint lightReferenceOffset = input.ReadUInt32();
                uint lightReferenceEntries = input.ReadUInt32();
                uint fogReferenceOffset = input.ReadUInt32();
                uint fogReferenceEntries = input.ReadUInt32();

                data.Seek(cameraReferenceOffset, SeekOrigin.Begin);
                for (int i = 0; i < cameraReferenceEntries; i++)
                {
                    RenderBase.OSceneReference reference = new RenderBase.OSceneReference();
                    reference.slotIndex = input.ReadUInt32();
                    reference.name = readString(input);
                    scene.cameras.Add(reference);
                }

                data.Seek(lightReferenceOffset, SeekOrigin.Begin);
                for (int i = 0; i < lightReferenceEntries; i++)
                {
                    RenderBase.OSceneReference reference = new RenderBase.OSceneReference();
                    reference.slotIndex = input.ReadUInt32();
                    reference.name = readString(input);
                    scene.lights.Add(reference);
                }

                data.Seek(fogReferenceOffset, SeekOrigin.Begin);
                for (int i = 0; i < fogReferenceEntries; i++)
                {
                    RenderBase.OSceneReference reference = new RenderBase.OSceneReference();
                    reference.slotIndex = input.ReadUInt32();
                    reference.name = readString(input);
                    scene.fogs.Add(reference);
                }
            }

            //Model
            for (int modelIndex = 0; modelIndex < contentHeader.modelsPointerTableEntries; modelIndex++)
            {
                RenderBase.OModel model = new RenderBase.OModel();

                data.Seek(contentHeader.modelsPointerTableOffset + (modelIndex * 4), SeekOrigin.Begin);
                uint objectsHeaderOffset = input.ReadUInt32();

                //Objects header
                data.Seek(objectsHeaderOffset, SeekOrigin.Begin);
                bchModelHeader modelHeader;
                modelHeader.flags = input.ReadByte();
                modelHeader.skeletonScalingType = input.ReadByte();
                modelHeader.silhouetteMaterialEntries = input.ReadUInt16();

                modelHeader.worldTransform = new RenderBase.OMatrix();
                modelHeader.worldTransform.M11 = input.ReadSingle();
                modelHeader.worldTransform.M21 = input.ReadSingle();
                modelHeader.worldTransform.M31 = input.ReadSingle();
                modelHeader.worldTransform.M41 = input.ReadSingle();

                modelHeader.worldTransform.M12 = input.ReadSingle();
                modelHeader.worldTransform.M22 = input.ReadSingle();
                modelHeader.worldTransform.M32 = input.ReadSingle();
                modelHeader.worldTransform.M42 = input.ReadSingle();

                modelHeader.worldTransform.M13 = input.ReadSingle();
                modelHeader.worldTransform.M23 = input.ReadSingle();
                modelHeader.worldTransform.M33 = input.ReadSingle();
                modelHeader.worldTransform.M43 = input.ReadSingle();

                modelHeader.materialsTableOffset = input.ReadUInt32();
                modelHeader.materialsTableEntries = input.ReadUInt32();
                modelHeader.materialsNameOffset = input.ReadUInt32();
                modelHeader.verticesTableOffset = input.ReadUInt32();
                modelHeader.verticesTableEntries = input.ReadUInt32();
                data.Seek(0x28, SeekOrigin.Current);
                modelHeader.skeletonOffset = input.ReadUInt32();
                modelHeader.skeletonEntries = input.ReadUInt32();
                modelHeader.skeletonNameOffset = input.ReadUInt32();
                modelHeader.objectsNodeVisibilityOffset = input.ReadUInt32();
                modelHeader.objectsNodeCount = input.ReadUInt32();
                modelHeader.modelName = readString(input);
                modelHeader.objectsNodeNameEntries = input.ReadUInt32();
                modelHeader.objectsNodeNameOffset = input.ReadUInt32();
                input.ReadUInt32(); //0x0
                modelHeader.metaDataPointerOffset = input.ReadUInt32();

                model.transform = modelHeader.worldTransform;
                model.name = modelHeader.modelName;

                string[] objectName = new string[modelHeader.objectsNodeNameEntries];
                data.Seek(modelHeader.objectsNodeNameOffset + 0xc, SeekOrigin.Begin);
                for (int i = 0; i < modelHeader.objectsNodeNameEntries; i++)
                {
                    input.ReadUInt32();
                    input.ReadUInt32();
                    objectName[i] = readString(input);
                }

                //Materials
                for (int index = 0; index < modelHeader.materialsTableEntries; index++)
                {
                    //Nota: As versões mais antigas tinham o Coordinator já no header do material.
                    //As versões mais recentes tem uma seção reservada só pra ele, por isso possuem tamanho do header menor.
                    if (header.backwardCompatibility < 0x21)
                        data.Seek(modelHeader.materialsTableOffset + (index * 0x58), SeekOrigin.Begin);
                    else
                        data.Seek(modelHeader.materialsTableOffset + (index * 0x2c), SeekOrigin.Begin);

                    RenderBase.OMaterial material = new RenderBase.OMaterial();

                    uint materialParametersOffset = input.ReadUInt32();
                    input.ReadUInt32(); //TODO
                    input.ReadUInt32();
                    input.ReadUInt32();
                    uint textureCommandsOffset = input.ReadUInt32();
                    uint textureCommandsWordCount = input.ReadUInt32();

                    uint materialCoordinatorOffset = 0;
                    if (header.backwardCompatibility < 0x21)
                    {
                        materialCoordinatorOffset = (uint)data.Position;
                        data.Seek(0x30, SeekOrigin.Current);
                    }
                    else
                    {
                        materialCoordinatorOffset = input.ReadUInt32();
                    }

                    material.name0 = readString(input);
                    material.name1 = readString(input);
                    material.name2 = readString(input);
                    material.name = readString(input);

                    //Parameters
                    //Same pointer of Materials section. Why?
                    if (materialParametersOffset != 0)
                    {
                        data.Seek(materialParametersOffset, SeekOrigin.Begin);
                        uint hash = input.ReadUInt32();

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

                            material.textureCoordinator[i] = coordinator;
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

                        //Some Fragment Lighting related commands... This seems a bit out of place.
                        long position = data.Position;
                        PICACommandReader fshLightingCommands = new PICACommandReader(data, 6, true);

                        PICACommand.fragmentSamplerInput sInput = fshLightingCommands.getReflectanceSamplerInput();
                        material.fragmentShader.lighting.reflectanceRSampler.input = sInput.r;
                        material.fragmentShader.lighting.reflectanceGSampler.input = sInput.g;
                        material.fragmentShader.lighting.reflectanceBSampler.input = sInput.b;
                        material.fragmentShader.lighting.distribution0Sampler.input = sInput.d0;
                        material.fragmentShader.lighting.distribution1Sampler.input = sInput.d1;
                        material.fragmentShader.lighting.fresnelSampler.input = sInput.fresnel;

                        PICACommand.fragmentSamplerScale sScale = fshLightingCommands.getReflectanceSamplerScale();
                        material.fragmentShader.lighting.reflectanceRSampler.scale = sScale.r;
                        material.fragmentShader.lighting.reflectanceGSampler.scale = sScale.g;
                        material.fragmentShader.lighting.reflectanceBSampler.scale = sScale.b;
                        material.fragmentShader.lighting.distribution0Sampler.scale = sScale.d0;
                        material.fragmentShader.lighting.distribution1Sampler.scale = sScale.d1;
                        material.fragmentShader.lighting.fresnelSampler.scale = sScale.fresnel;
                        data.Seek(position + (4 * 6), SeekOrigin.Begin); //Just to be sure ;)

                        RenderBase.OConstantColor[] constantList = new RenderBase.OConstantColor[6];
                        uint constantColor = input.ReadUInt32();
                        for (int i = 0; i < 6; i++) constantList[i] = (RenderBase.OConstantColor)((constantColor >> (i * 4)) & 0xf);
                        material.rasterization.polygonOffsetUnit = input.ReadSingle();
                        uint fshCommandsOffset = input.ReadUInt32();
                        uint fshCommandsWordCount = input.ReadUInt32();
                        input.ReadUInt32();

                        material.fragmentShader.lighting.distribution0Sampler.tableName = readString(input);
                        material.fragmentShader.lighting.distribution1Sampler.tableName = readString(input);
                        material.fragmentShader.lighting.fresnelSampler.tableName = readString(input);
                        material.fragmentShader.lighting.reflectanceRSampler.tableName = readString(input);
                        material.fragmentShader.lighting.reflectanceGSampler.tableName = readString(input);
                        material.fragmentShader.lighting.reflectanceBSampler.tableName = readString(input);

                        material.fragmentShader.lighting.distribution0Sampler.samplerName = readString(input);
                        material.fragmentShader.lighting.distribution1Sampler.samplerName = readString(input);
                        material.fragmentShader.lighting.fresnelSampler.samplerName = readString(input);
                        material.fragmentShader.lighting.reflectanceRSampler.samplerName = readString(input);
                        material.fragmentShader.lighting.reflectanceGSampler.samplerName = readString(input);
                        material.fragmentShader.lighting.reflectanceBSampler.samplerName = readString(input);

                        //Fragment Shader commands
                        data.Seek(fshCommandsOffset, SeekOrigin.Begin);
                        PICACommandReader fshCommands = new PICACommandReader(data, fshCommandsWordCount);
                        for (byte stage = 0; stage < 6; stage++) material.fragmentShader.textureCombiner[stage] = fshCommands.getTevStage(stage);
                        material.fragmentShader.bufferColor = fshCommands.getFragmentBufferColor();
                        material.fragmentOperation.blend = fshCommands.getBlendOperation();
                        material.fragmentOperation.blend.logicalOperation = fshCommands.getColorLogicOperation();
                        material.fragmentShader.alphaTest = fshCommands.getAlphaTest();
                        material.fragmentOperation.stencil = fshCommands.getStencilTest();
                        material.fragmentOperation.depth = fshCommands.getDepthTest();
                        material.rasterization.cullMode = fshCommands.getCullMode();

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

                            material.textureMapper[i] = mapper;
                        }
                    }

                    model.addMaterial(material);
                }

                //Skeleton
                data.Seek(modelHeader.skeletonOffset, SeekOrigin.Begin);
                for (int index = 0; index < modelHeader.skeletonEntries; index++)
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

                    bone.name = readString(input);
                    input.ReadUInt32(); //TODO: Figure out

                    model.addBone(bone);
                }

                List<RenderBase.OMatrix> skeletonTransform = new List<RenderBase.OMatrix>();
                for (int index = 0; index < modelHeader.skeletonEntries; index++)
                {
                    RenderBase.OMatrix transform = new RenderBase.OMatrix();
                    transformSkeleton(model.skeleton, index, ref transform);
                    skeletonTransform.Add(transform);
                }

                data.Seek(modelHeader.objectsNodeVisibilityOffset, SeekOrigin.Begin);
                uint nodeVisibility = input.ReadUInt32();

                //Vertices header
                data.Seek(modelHeader.verticesTableOffset, SeekOrigin.Begin);
                List<bchObjectEntry> objects = new List<bchObjectEntry>();

                for (int index = 0; index < modelHeader.verticesTableEntries; index++)
                {
                    bchObjectEntry objectEntry = new bchObjectEntry();
                    objectEntry.materialId = input.ReadUInt16();
                    ushort flags = input.ReadUInt16();
                    objectEntry.isSilhouette = (flags & 1) > 0;
                    objectEntry.nodeId = input.ReadUInt16();
                    objectEntry.renderPriority = input.ReadUInt16();
                    objectEntry.vshAttributesBufferCommandsOffset = input.ReadUInt32(); //Buffer 0
                    objectEntry.vshAttributesBufferCommandsWordCount = input.ReadUInt32();
                    objectEntry.facesHeaderOffset = input.ReadUInt32();
                    objectEntry.facesHeaderEntries = input.ReadUInt32();
                    objectEntry.vshExtraAttributesBufferCommandsOffset = input.ReadUInt32(); //Buffers 1-11
                    objectEntry.vshExtraAttributesBufferCommandsWordCount = input.ReadUInt32();
                    objectEntry.centerVector = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    objectEntry.flagsOffset = input.ReadUInt32();
                    input.ReadUInt32(); //ex: 0x0 fixo
                    objectEntry.boundingBoxOffset = input.ReadUInt32();

                    objects.Add(objectEntry);
                }

                for (int objIndex = 0; objIndex < objects.Count; objIndex++)
                {
                    if (objects[objIndex].isSilhouette) continue; //TODO: Figure out for what "Silhouette" is used.

                    RenderBase.OModelObject obj = new RenderBase.OModelObject();
                    obj.materialId = objects[objIndex].materialId;
                    obj.renderPriority = objects[objIndex].renderPriority;
                    if (objects[objIndex].nodeId < objectName.Length) obj.name = objectName[objects[objIndex].nodeId]; else obj.name = "mesh" + objIndex.ToString();
                    obj.isVisible = (nodeVisibility & (1 << objects[objIndex].nodeId)) > 0;

                    //Vertices
                    data.Seek(objects[objIndex].vshAttributesBufferCommandsOffset, SeekOrigin.Begin);
                    PICACommandReader vshCommands = new PICACommandReader(data, objects[objIndex].vshAttributesBufferCommandsWordCount);

                    Stack<float> vshAttributesUniformReg6 = vshCommands.getVSHFloatUniformData(6);
                    Stack<float> vshAttributesUniformReg7 = vshCommands.getVSHFloatUniformData(7);
                    RenderBase.OVector4 positionOffset = new RenderBase.OVector4(vshAttributesUniformReg6.Pop(),
                        vshAttributesUniformReg6.Pop(),
                        vshAttributesUniformReg6.Pop(),
                        vshAttributesUniformReg6.Pop());
                    float texture0Scale = vshAttributesUniformReg7.Pop();
                    float texture1Scale = vshAttributesUniformReg7.Pop();
                    float texture2Scale = vshAttributesUniformReg7.Pop();
                    float boneWeightScale = vshAttributesUniformReg7.Pop();
                    float positionScale = vshAttributesUniformReg7.Pop();
                    float normalScale = vshAttributesUniformReg7.Pop();
                    float tangentScale = vshAttributesUniformReg7.Pop();
                    float colorScale = vshAttributesUniformReg7.Pop();

                    List<RenderBase.CustomVertex> vshAttributesBuffer = new List<RenderBase.CustomVertex>();

                    //Faces
                    for (uint f = 0; f < objects[objIndex].facesHeaderEntries; f++)
                    {
                        uint baseOffset = objects[objIndex].facesHeaderOffset + (f * 0x34);
                        data.Seek(baseOffset, SeekOrigin.Begin);
                        RenderBase.OSkinningMode skinningMode = (RenderBase.OSkinningMode)input.ReadUInt16();
                        ushort nodeIdEntries = input.ReadUInt16();
                        List<ushort> nodeList = new List<ushort>();
                        for (int n = 0; n < nodeIdEntries; n++) nodeList.Add(input.ReadUInt16());

                        data.Seek(baseOffset + 0x2c, SeekOrigin.Begin);
                        uint faceHeaderOffset = input.ReadUInt32();
                        uint faceHeaderWordCount = input.ReadUInt32();

                        data.Seek(faceHeaderOffset, SeekOrigin.Begin);
                        PICACommandReader idxCommands = new PICACommandReader(data, faceHeaderWordCount);
                        uint idxBufferOffset = idxCommands.getIndexBufferAddress();
                        PICACommand.indexBufferFormat idxBufferFormat = idxCommands.getIndexBufferFormat();
                        uint idxBufferTotalVertices = idxCommands.getIndexBufferTotalVertices();

                        //Carregamento de dados relacionados ao Vertex Shader
                        uint vshAttributesBufferOffset = vshCommands.getVSHAttributesBufferAddress(0);
                        uint vshAttributesBufferStride = vshCommands.getVSHAttributesBufferStride(0);
                        uint vshTotalAttributes = vshCommands.getVSHTotalAttributes(0);
                        PICACommand.vshAttribute[] vshMainAttributesBufferPermutation = vshCommands.getVSHAttributesBufferPermutation();
                        uint[] vshAttributesBufferPermutation = vshCommands.getVSHAttributesBufferPermutation(0);
                        PICACommand.attributeFormat[] vshAttributesBufferFormat = vshCommands.getVSHAttributesBufferFormat();

                        for (int attribute = 0; attribute < vshTotalAttributes; attribute++)
                        {
                            PICACommand.attributeFormat format = vshAttributesBufferFormat[vshAttributesBufferPermutation[attribute]];

                            switch (vshMainAttributesBufferPermutation[vshAttributesBufferPermutation[attribute]])
                            {
                                case PICACommand.vshAttribute.normal: obj.hasNormal = true; break;
                                case PICACommand.vshAttribute.tangent: obj.hasTangent = true; break;
                                case PICACommand.vshAttribute.color: obj.hasColor = true; break;
                                case PICACommand.vshAttribute.textureCoordinate0: obj.texUVCount = Math.Max(obj.texUVCount, 1); break;
                                case PICACommand.vshAttribute.textureCoordinate1: obj.texUVCount = Math.Max(obj.texUVCount, 2); break;
                                case PICACommand.vshAttribute.textureCoordinate2: obj.texUVCount = Math.Max(obj.texUVCount, 3); break;
                                case PICACommand.vshAttribute.boneIndex: obj.hasNode = true; break;
                                case PICACommand.vshAttribute.boneWeight: obj.hasWeight = true; break;
                            }
                        }

                        data.Seek(idxBufferOffset, SeekOrigin.Begin);
                        for (int faceIndex = 0; faceIndex < idxBufferTotalVertices; faceIndex++)
                        {
                            ushort index = 0;

                            switch (idxBufferFormat)
                            {
                                case PICACommand.indexBufferFormat.unsignedShort: index = input.ReadUInt16(); break;
                                case PICACommand.indexBufferFormat.unsignedByte: index = input.ReadByte(); break;
                            }

                            long dataPosition = data.Position;
                            long vertexOffset = vshAttributesBufferOffset + (index * vshAttributesBufferStride);
                            data.Seek(vertexOffset, SeekOrigin.Begin);

                            RenderBase.OVertex vertex = new RenderBase.OVertex();
                            vertex.diffuseColor = 0xffffffff;
                            for (int attribute = 0; attribute < vshTotalAttributes; attribute++)
                            {
                                PICACommand.attributeFormat format = vshAttributesBufferFormat[vshAttributesBufferPermutation[attribute]];
                                RenderBase.OVector4 vector = getVector(input, format);

                                switch (vshMainAttributesBufferPermutation[vshAttributesBufferPermutation[attribute]])
                                {
                                    case PICACommand.vshAttribute.position:
                                        float x = (vector.x * positionScale) + positionOffset.x;
                                        float y = (vector.y * positionScale) + positionOffset.y;
                                        float z = (vector.z * positionScale) + positionOffset.z;
                                        vertex.position = new RenderBase.OVector3(x, y, z);
                                        break;
                                    case PICACommand.vshAttribute.normal:
                                        vertex.normal = new RenderBase.OVector3(vector.x * normalScale, vector.y * normalScale, vector.z * normalScale);
                                        break;
                                    case PICACommand.vshAttribute.tangent:
                                        vertex.tangent = new RenderBase.OVector3(vector.x * tangentScale, vector.y * tangentScale, vector.z * tangentScale);
                                        break;
                                    case PICACommand.vshAttribute.color:
                                        uint r = (uint)((vector.x * colorScale) * 0xff);
                                        uint g = (uint)((vector.y * colorScale) * 0xff);
                                        uint b = (uint)((vector.z * colorScale) * 0xff);
                                        uint a = (uint)((vector.w * colorScale) * 0xff);
                                        vertex.diffuseColor = b | (g << 8) | (r << 16) | (a << 24);
                                        break;
                                    case PICACommand.vshAttribute.textureCoordinate0:
                                        vertex.texture0 = new RenderBase.OVector2(vector.x * texture0Scale, vector.y * texture0Scale);
                                        break;
                                    case PICACommand.vshAttribute.textureCoordinate1:
                                        vertex.texture1 = new RenderBase.OVector2(vector.x * texture1Scale, vector.y * texture1Scale);
                                        break;
                                    case PICACommand.vshAttribute.textureCoordinate2:
                                        vertex.texture2 = new RenderBase.OVector2(vector.x * texture2Scale, vector.y * texture2Scale);
                                        break;
                                    case PICACommand.vshAttribute.boneIndex:
                                        vertex.addNode(nodeList[(int)vector.x]);
                                        if (format.attributeLength > 0) vertex.addNode(nodeList[(int)vector.y]);
                                        if (format.attributeLength > 1) vertex.addNode(nodeList[(int)vector.z]);
                                        if (format.attributeLength > 2) vertex.addNode(nodeList[(int)vector.w]);
                                        break;
                                    case PICACommand.vshAttribute.boneWeight:
                                        vertex.addWeight(vector.x * boneWeightScale);
                                        if (format.attributeLength > 0) vertex.addWeight(vector.y * boneWeightScale);
                                        if (format.attributeLength > 1) vertex.addWeight(vector.z * boneWeightScale);
                                        if (format.attributeLength > 2) vertex.addWeight(vector.w * boneWeightScale);
                                        break;
                                }
                            }

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
                            vshAttributesBuffer.Add(RenderBase.convertVertex(vertex));

                            data.Seek(dataPosition, SeekOrigin.Begin);
                        }
                    }

                    //Bounding box
                    if (objects[objIndex].boundingBoxOffset != 0)
                    {
                        data.Seek(objects[objIndex].boundingBoxOffset, SeekOrigin.Begin);
                        uint bBoxDataOffset = input.ReadUInt32();
                        uint bBoxEntries = input.ReadUInt32();
                        uint bBoxNameOffset = input.ReadUInt32();

                        for (int index = 0; index < bBoxEntries; index++)
                        {
                            data.Seek(bBoxDataOffset + (index * 0xc), SeekOrigin.Begin);

                            RenderBase.OOrientedBoundingBox bBox = new RenderBase.OOrientedBoundingBox();

                            bBox.name = readString(input);
                            uint flags = input.ReadUInt32();
                            uint dataOffset = input.ReadUInt32();

                            data.Seek(dataOffset, SeekOrigin.Begin);
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

                            obj.addBoundingBox(bBox);
                        }
                    }
    
                    obj.renderBuffer = vshAttributesBuffer.ToArray();
                    model.addObject(obj);
                }

                if (modelHeader.metaDataPointerOffset != 0)
                {
                    data.Seek(modelHeader.metaDataPointerOffset, SeekOrigin.Begin);
                    uint metaDataOffset = input.ReadUInt32();
                    uint metaDataEntries = input.ReadUInt32();
                    uint metaDataNameOffset = input.ReadUInt32();

                    for (int index = 0; index < metaDataEntries; index++)
                    {
                        data.Seek(metaDataOffset + (index * 0xc), SeekOrigin.Begin);

                        RenderBase.OMetaData metaData = new RenderBase.OMetaData();

                        metaData.name = readString(input);
                        metaData.type = (RenderBase.OMetaDataValueType)input.ReadUInt16();
                        ushort flags = input.ReadUInt16();
                        uint dataOffset = input.ReadUInt32();

                        data.Seek(dataOffset, SeekOrigin.Begin);
                        switch (metaData.type)
                        {
                            case RenderBase.OMetaDataValueType.integer: metaData.value = input.ReadInt32(); break;
                            case RenderBase.OMetaDataValueType.single: metaData.value = input.ReadSingle(); break;
                            case RenderBase.OMetaDataValueType.utf16String:
                            case RenderBase.OMetaDataValueType.utf8String:
                                data.Seek(input.ReadUInt32(), SeekOrigin.Begin);
                                MemoryStream strStream = new MemoryStream();
                                byte strChar = input.ReadByte();
                                byte oldChar = 0xff;
                                while ((metaData.type == RenderBase.OMetaDataValueType.utf8String && strChar != 0) || !(oldChar == 0 && strChar == 0))
                                {
                                    oldChar = strChar;
                                    strStream.WriteByte(strChar);
                                    strChar = input.ReadByte();
                                }

                                if (metaData.type == RenderBase.OMetaDataValueType.utf16String)
                                    metaData.value = Encoding.Unicode.GetString(strStream.ToArray());
                                else
                                    metaData.value = Encoding.UTF8.GetString(strStream.ToArray());

                                strStream.Close();
                                break;
                        }
                    }
                }

                for (int index = 0; index < modelHeader.skeletonEntries; index++)
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
        ///     Reads a UInt without advancing the position on the Stream.
        /// </summary>
        /// <param name="input">The BinaryReader of the stream</param>
        /// <returns></returns>
        private static uint peek(BinaryReader input)
        {
            uint value = input.ReadUInt32();
            input.BaseStream.Seek(-4, SeekOrigin.Current);
            return value;
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
        private static RenderBase.OVector4 getVector(BinaryReader input, PICACommand.attributeFormat format)
        {
            RenderBase.OVector4 output = new RenderBase.OVector4();

            switch (format.type)
            {
                case PICACommand.attributeFormatType.signedByte:
                    output.x = (sbyte)input.ReadByte();
                    if (format.attributeLength > 0) output.y = (sbyte)input.ReadByte();
                    if (format.attributeLength > 1) output.z = (sbyte)input.ReadByte();
                    if (format.attributeLength > 2) output.w = (sbyte)input.ReadByte();
                    break;
                case PICACommand.attributeFormatType.unsignedByte:
                    output.x = input.ReadByte();
                    if (format.attributeLength > 0) output.y = input.ReadByte();
                    if (format.attributeLength > 1) output.z = input.ReadByte();
                    if (format.attributeLength > 2) output.w = input.ReadByte();
                    break;
                case PICACommand.attributeFormatType.signedShort:
                    output.x = input.ReadInt16();
                    if (format.attributeLength > 0) output.y = input.ReadInt16();
                    if (format.attributeLength > 1) output.z = input.ReadInt16();
                    if (format.attributeLength > 2) output.w = input.ReadInt16();
                    break;
                case PICACommand.attributeFormatType.single:
                    output.x = input.ReadSingle();
                    if (format.attributeLength > 0) output.y = input.ReadSingle();
                    if (format.attributeLength > 1) output.z = input.ReadSingle();
                    if (format.attributeLength > 2) output.w = input.ReadSingle();
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
        ///     Reads a string if offset is different of 0.
        ///     Position advances 4 bytes only.
        /// </summary>
        /// <param name="input">BCH reader</param>
        /// <returns></returns>
        private static string readString(BinaryReader input)
        {
            uint offset = input.ReadUInt32();
            if (offset != 0) return IOUtils.readString(input, offset);
            return null;
        }

        /// <summary>
        ///     Gets an Animation Key frame from the BCH file.
        ///     The Reader position must be set to the beggining of the Key Frame Data.
        /// </summary>
        /// <param name="input">The BCH file Reader</param>
        /// <param name="header">The BCH file header</param>
        /// <returns></returns>
        private static RenderBase.OAnimationKeyFrame getAnimationKeyFrame(BinaryReader input)
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
            uint maxValueEncoded = input.ReadUInt32();
            float minValue = input.ReadSingle();
            float frameScale = input.ReadSingle();
            float valueScale = input.ReadSingle(); //Probably wrong

            switch (entryFormat)
            {
                case 1:
                case 7:
                    maxValue = getCustomFloat(maxValueEncoded, 107);
                    break;
                case 2:
                case 4:
                    maxValue = getCustomFloat(maxValueEncoded, 111);
                    break;
                case 5: maxValue = getCustomFloat(maxValueEncoded, 115); break;
            }
            maxValue = minValue + maxValue;

            float interpolation;
            uint value;
            uint rawDataOffset = input.ReadUInt32();
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
        private static RenderBase.OAnimationKeyFrame getAnimationKeyFrameBool(BinaryReader input)
        {
            RenderBase.OAnimationKeyFrame frame = new RenderBase.OAnimationKeyFrame();

            frame.exists = true;
            frame.startFrame = 0;

            frame.defaultValue = ((input.ReadUInt32() >> 24) & 1) > 0;
            input.ReadUInt32();
            uint offset = input.ReadUInt32();
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

        #region "Export"
        private class faceData
        {
            public byte[] buffer;
            public byte[] nodes;

            public faceData(BinaryWriter indices, List<ushort> nodeList)
            {
                buffer = ((MemoryStream)indices.BaseStream).ToArray();
                indices.BaseStream.SetLength(0);
                byte[] nodesBuffer = new byte[nodeList.Count * 2];
                long offset = 0;
                foreach (ushort node in nodeList)
                {
                    nodesBuffer[offset++] = (byte)(node & 0xff);
                    nodesBuffer[offset++] = (byte)(node >> 8);
                }
                nodeList.Clear();
                nodes = nodesBuffer;
            }

            public faceData()
            {
            }
        }

        private class vertexData
        {
            public List<faceData> faces;
            public byte[] buffer;

            public vertexData()
            {
                faces = new List<faceData>();
            }
        }

        public static byte[] save(RenderBase.OModelGroup model)
        {
            BinaryWriter mainHeader = new BinaryWriter(new MemoryStream());
            PICACommandWriter mainHeaderCommands = new PICACommandWriter(mainHeader.BaseStream);
            BinaryWriter stringTable = new BinaryWriter(new MemoryStream());
            PICACommandWriter gpuCommands = new PICACommandWriter(new MemoryStream());
            BinaryWriter data = new BinaryWriter(new MemoryStream());
            BinaryWriter dataExtended = new BinaryWriter(new MemoryStream());
            BinaryWriter relocationTable = new BinaryWriter(new MemoryStream());

            uint offset1 = 0;
            uint offset2 = 12 * 15;

            //Models
            mainHeader.Write(0);
            mainHeader.Write(model.model.Count);
            writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
            mainHeader.Write(offset2);
            mainHeader.BaseStream.Seek(offset2, SeekOrigin.Begin);
            mainHeader.Write(0);
            mainHeader.Write(0);
            mainHeader.Write(0); //Name thingy
            offset2 += 12;
            mainHeader.BaseStream.Seek(offset1, SeekOrigin.Begin);
            writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
            mainHeader.Write(offset2);
            offset1 += 12;
            offset2 += (uint)model.model.Count * 4;

            //Placeholder until I write remaining sections
            for (int i = 0; i < 14; i++)
            {
                mainHeader.BaseStream.Seek(offset1, SeekOrigin.Begin);
                mainHeader.Write(0);
                mainHeader.Write(0);
                writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
                mainHeader.Write(offset2);
                mainHeader.BaseStream.Seek(offset2, SeekOrigin.Begin);
                mainHeader.Write(0);
                mainHeader.Write(0);
                mainHeader.Write(0); //Name thingy
                offset2 += 12;
                offset1 += 12;
            }

            List<uint> modelsPointerTable = new List<uint>();
            foreach (RenderBase.OModel mdl in model.model)
            {
                //Header do modelo
                modelsPointerTable.Add((uint)mainHeader.BaseStream.Position);
                mainHeader.Write(0);

                mainHeader.Write(mdl.transform.M11);
                mainHeader.Write(mdl.transform.M21);
                mainHeader.Write(mdl.transform.M31);
                mainHeader.Write(mdl.transform.M41);

                mainHeader.Write(mdl.transform.M12);
                mainHeader.Write(mdl.transform.M22);
                mainHeader.Write(mdl.transform.M32);
                mainHeader.Write(mdl.transform.M42);

                mainHeader.Write(mdl.transform.M13);
                mainHeader.Write(mdl.transform.M23);
                mainHeader.Write(mdl.transform.M33);
                mainHeader.Write(mdl.transform.M43);

                long modelHeaderPosition = mainHeader.BaseStream.Position;
                mainHeader.BaseStream.Seek(0x68, SeekOrigin.Current);
                mainHeader.Write(0);

                /*
                 * Materials
                 */
                uint materialsTableOffset = (uint)mainHeader.BaseStream.Position;
                foreach (RenderBase.OMaterial material in mdl.material)
                {
                    uint position = (uint)mainHeader.BaseStream.Position;
                    writeRelocatableOffset(position, 0, relocationTable);
                    mainHeader.Write(position + 0x70);
                    mainHeader.Write(0);
                    mainHeader.Write(0);
                    mainHeader.Write(0);
                    mainHeader.Write(0); //Texture header offset
                    mainHeader.Write(0); //Texture header entries
                    writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
                    mainHeader.Write(position + 0x118); //Coordinator offset

                    writeString(material.name0, mainHeader, stringTable, relocationTable);
                    writeString(material.name1, mainHeader, stringTable, relocationTable);
                    writeString(material.name2, mainHeader, stringTable, relocationTable);
                    writeString(material.name, mainHeader, stringTable, relocationTable);

                    mainHeader.Seek((int)(position + 0x70), SeekOrigin.Begin);
                    mainHeader.Write(0);

                    //Material flags
                    ushort materialFlags = 0;
                    if (material.isFragmentLightEnabled) materialFlags = 1;
                    if (material.isVertexLightEnabled) materialFlags |= 2;
                    if (material.isHemiSphereLightEnabled) materialFlags |= 4;
                    if (material.isHemiSphereOcclusionEnabled) materialFlags |= 8;
                    if (material.isFogEnabled) materialFlags |= 0x10;
                    if (material.rasterization.isPolygonOffsetEnabled) materialFlags |= 0x20;
                    mainHeader.Write(materialFlags);

                    //Fragment Shader flags
                    ushort fragmentFlags = 0;
                    if (material.fragmentShader.bump.isBumpRenormalize) fragmentFlags = 1;
                    if (material.fragmentShader.lighting.isClampHighLight) fragmentFlags |= 2;
                    if (material.fragmentShader.lighting.isDistribution0Enabled) fragmentFlags |= 4;
                    if (material.fragmentShader.lighting.isDistribution1Enabled) fragmentFlags |= 8;
                    if (material.fragmentShader.lighting.isGeometryFactor0Enabled) fragmentFlags |= 0x10;
                    if (material.fragmentShader.lighting.isGeometryFactor1Enabled) fragmentFlags |= 0x20;
                    if (material.fragmentShader.lighting.isReflectionEnabled) fragmentFlags |= 0x40;
                    fragmentFlags |= (ushort)(((uint)material.fragmentOperation.blend.mode & 3) << 10);
                    mainHeader.Write(fragmentFlags);

                    mainHeader.Write(0);

                    //Coordinator
                    for (int texture = 0; texture < 3; texture++)
                    {
                        RenderBase.OTextureCoordinator coordinator = material.textureCoordinator[texture];
                        uint projectionAndCamera = ((uint)coordinator.projection << 16);
                        projectionAndCamera |= coordinator.referenceCamera << 24;
                        mainHeader.Write(projectionAndCamera);
                        mainHeader.Write(coordinator.scaleU);
                        mainHeader.Write(coordinator.scaleV);
                        mainHeader.Write(coordinator.rotate);
                        mainHeader.Write(coordinator.translateU);
                        mainHeader.Write(coordinator.translateV);
                    }

                    mainHeader.Write((ushort)material.lightSetIndex);
                    mainHeader.Write((ushort)material.fogIndex);

                    //Material colors
                    setColor(material.materialColor.emission, mainHeader);
                    setColor(material.materialColor.ambient, mainHeader);
                    setColor(material.materialColor.diffuse, mainHeader);
                    setColor(material.materialColor.specular0, mainHeader);
                    setColor(material.materialColor.specular1, mainHeader);
                    setColor(material.materialColor.constant0, mainHeader);
                    setColor(material.materialColor.constant1, mainHeader);
                    setColor(material.materialColor.constant2, mainHeader);
                    setColor(material.materialColor.constant3, mainHeader);
                    setColor(material.materialColor.constant4, mainHeader);
                    setColor(material.materialColor.constant5, mainHeader);
                    setColor(material.fragmentOperation.blend.blendColor, mainHeader);
                    mainHeader.Write(material.materialColor.colorScale);

                    mainHeader.Write(0);
                    mainHeader.Write(0);
                    mainHeader.Write(0);
                    mainHeader.Write(0);
                    mainHeader.Write(0);
                    mainHeader.Write(0);

                    uint fragmentData;
                    fragmentData = ((uint)material.fragmentShader.bump.texture & 0xff) << 24;
                    fragmentData |= ((uint)material.fragmentShader.bump.mode & 0xff) << 16;
                    fragmentData |= ((uint)material.fragmentShader.lighting.fresnelConfig & 0xff) << 8;
                    fragmentData |= (uint)material.fragmentShader.layerConfig & 0xff;
                    mainHeader.Write(fragmentData);

                    mainHeaderCommands.setCommand(0x1d0, 0x2222222);

                    //LookUp tables
                    RenderBase.OFragmentLighting lighting = material.fragmentShader.lighting;

                    //Input
                    uint reflectanceInput;
                    reflectanceInput = ((uint)lighting.reflectanceRSampler.input & 0xf) << 24;
                    reflectanceInput |= ((uint)lighting.reflectanceGSampler.input & 0xf) << 20;
                    reflectanceInput |= ((uint)lighting.reflectanceBSampler.input & 0xf) << 16;
                    reflectanceInput |= (uint)lighting.distribution0Sampler.input & 0xf;
                    reflectanceInput |= ((uint)lighting.distribution1Sampler.input & 0xf) << 4;
                    reflectanceInput |= ((uint)lighting.fresnelSampler.input & 0xf) << 12;
                    mainHeaderCommands.setCommand(PICACommand.reflectanceSamplerInput, reflectanceInput);

                    //Scale
                    uint reflectanceScale;
                    reflectanceScale = ((uint)lighting.reflectanceRSampler.scale & 0xf) << 24;
                    reflectanceScale |= ((uint)lighting.reflectanceGSampler.scale & 0xf) << 20;
                    reflectanceScale |= ((uint)lighting.reflectanceBSampler.scale & 0xf) << 16;
                    reflectanceScale |= (uint)lighting.distribution0Sampler.scale & 0xf;
                    reflectanceScale |= ((uint)lighting.distribution1Sampler.scale & 0xf) << 4;
                    reflectanceScale |= ((uint)lighting.fresnelSampler.scale & 0xf) << 12;
                    mainHeaderCommands.setCommand(PICACommand.reflectanceSamplerInput, reflectanceScale);

                    uint constantColor = 0;
                    for (int tevStage = 0; tevStage < 6; tevStage++)
                    {
                        constantColor |= ((uint)material.fragmentShader.textureCombiner[tevStage].constantColor & 0xf) << (tevStage * 4);
                    }
                    mainHeader.Write(constantColor);

                    mainHeader.Write(material.rasterization.polygonOffsetUnit);
                    writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 4, relocationTable);
                    mainHeader.Write((uint)gpuCommands.BaseStream.Position);
                    mainHeader.Write((uint)0x38); //Commands Word Count
                    mainHeader.Write(0);

                    writeString(lighting.distribution0Sampler.tableName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.distribution1Sampler.tableName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.fresnelSampler.tableName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.reflectanceRSampler.tableName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.reflectanceGSampler.tableName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.reflectanceBSampler.tableName, mainHeader, stringTable, relocationTable);

                    writeString(lighting.distribution0Sampler.samplerName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.distribution1Sampler.samplerName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.fresnelSampler.samplerName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.reflectanceRSampler.samplerName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.reflectanceGSampler.samplerName, mainHeader, stringTable, relocationTable);
                    writeString(lighting.reflectanceBSampler.samplerName, mainHeader, stringTable, relocationTable);

                    gpuCommands.setFSHCommands(material.fragmentShader, material.fragmentOperation, material.rasterization);

                    for (int texture = 0; texture < 3; texture++)
                    {
                        RenderBase.OTextureMapper mapper = material.textureMapper[texture];
                        uint wrapAndMagFilter;
                        uint levelOfDetailAndMinFilter;
                        wrapAndMagFilter = ((uint)mapper.wrapU & 0xff) << 8;
                        wrapAndMagFilter |= ((uint)mapper.wrapV & 0xff) << 16;
                        wrapAndMagFilter |= ((uint)mapper.magFilter & 0xff) << 24;
                        levelOfDetailAndMinFilter = (uint)mapper.minFilter & 0xff;
                        levelOfDetailAndMinFilter |= (uint)(mapper.minLOD & 0xff) << 8;
                        mainHeader.Write(wrapAndMagFilter);
                        mainHeader.Write(levelOfDetailAndMinFilter);
                        mainHeader.Write(mapper.LODBias);
                        setColor(mapper.borderColor, mainHeader);
                    }
                }

                /*
                 * Meshes 
                 */
                int objIndex = 0;
                uint meshNodeVisibility = 0;
                uint verticesTableOffset = (uint)mainHeader.BaseStream.Position;
                List<uint> faceCounts = new List<uint>();
                BinaryWriter mainHeaderFace = new BinaryWriter(new MemoryStream());
                foreach (RenderBase.OModelObject modelObject in mdl.modelObject)
                {
                    /*
                     * Criação de objetos
                     */
                    List<vertexData> vertexList = new List<vertexData>();
                    List<faceData> indexList = new List<faceData>();
                    BinaryWriter vertices = new BinaryWriter(new MemoryStream());
                    BinaryWriter indices = new BinaryWriter(new MemoryStream());
                    List<ushort> nodeList = new List<ushort>();

                    uint index = 0;
                    uint totalIndex = 0;
                    RenderBase.OVector3 minVector = new RenderBase.OVector3();
                    RenderBase.OVector3 maxVector = new RenderBase.OVector3();

                    RenderBase.OVertex[] oldVertex = new RenderBase.OVertex[2];

                    foreach (RenderBase.OVertex vertex in modelObject.obj)
                    {
                        bool repeatedVertex = false;

                        uint currentIndex = totalIndex;
                        if (totalIndex > 1)
                        {
                            if (vertex.Equals(oldVertex[1]))
                            {
                                currentIndex = totalIndex - 2;
                                repeatedVertex = true;
                            }
                            else if (vertex.Equals(oldVertex[1]))
                            {
                                currentIndex = totalIndex - 1;
                                repeatedVertex = true;
                            }
                        }
                        oldVertex[1] = oldVertex[0];
                        oldVertex[0] = vertex;

                        if (!repeatedVertex)
                        {
                            //Escreve vertices no buffer
                            vertices.Write(vertex.position.x);
                            vertices.Write(vertex.position.y);
                            vertices.Write(vertex.position.z);

                            if (modelObject.hasNormal)
                            {
                                vertices.Write(vertex.normal.x);
                                vertices.Write(vertex.normal.y);
                                vertices.Write(vertex.normal.z);
                            }

                            if (modelObject.hasTangent)
                            {
                                vertices.Write(vertex.tangent.x);
                                vertices.Write(vertex.tangent.y);
                                vertices.Write(vertex.tangent.z);
                            }

                            if (modelObject.hasColor)
                            {
                                vertices.Write((byte)((vertex.diffuseColor >> 16) & 0xff));
                                vertices.Write((byte)((vertex.diffuseColor >> 8) & 0xff));
                                vertices.Write((byte)(vertex.diffuseColor & 0xff));
                                vertices.Write((byte)(vertex.diffuseColor >> 24));
                            }

                            if (modelObject.texUVCount > 0) { vertices.Write(vertex.texture0.x); vertices.Write(vertex.texture0.y); }
                            if (modelObject.texUVCount > 1) { vertices.Write(vertex.texture1.x); vertices.Write(vertex.texture1.y); }
                            if (modelObject.texUVCount > 2) { vertices.Write(vertex.texture2.x); vertices.Write(vertex.texture2.y); }

                            if (modelObject.hasNode)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (i < vertex.node.Count)
                                    {
                                        if (!nodeList.Contains((ushort)vertex.node[i])) nodeList.Add((ushort)vertex.node[i]);
                                        vertices.Write((byte)nodeList.IndexOf((ushort)vertex.node[i]));
                                    }
                                    else
                                    {
                                        vertices.Write((byte)0);
                                    }
                                }
                            }

                            if (modelObject.hasWeight)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (i < vertex.weight.Count)
                                        vertices.Write((byte)(vertex.weight[i] * byte.MaxValue));
                                    else
                                        vertices.Write((byte)0);
                                }
                            }

                            totalIndex++;
                        }

                        if (modelObject.obj.Count <= byte.MaxValue)
                            indices.Write((byte)currentIndex);
                        else
                            indices.Write((ushort)currentIndex);

                        if (totalIndex - 1 >= ushort.MaxValue)
                        {
                            //Força criação de um novo objeto, já que o limite do indice é 65535
                            byte[] vBuffer = ((MemoryStream)vertices.BaseStream).ToArray();
                            vertexData vData = new vertexData();
                            vData.buffer = vBuffer;
                            indexList.Add(new faceData(indices, nodeList));
                            vData.faces.AddRange(indexList);
                            vertexList.Add(vData);
                            indexList.Clear();
                            vertices.Close();
                            vertices = new BinaryWriter(new MemoryStream());
                            totalIndex = 0;
                        }
                        else if (nodeList.Count >= 16)
                        {
                            if (index + 1 < modelObject.obj.Count)
                            {
                                int count = nodeList.Count;
                                for (int i = 0; i < modelObject.obj[(int)index + 1].node.Count; i++)
                                {
                                    if (!nodeList.Contains((ushort)modelObject.obj[(int)index + 1].node[i])) count++;
                                }

                                if (count >= 20) indexList.Add(new faceData(indices, nodeList)); //Força criação de uma nova Face, pois o máximo de nodes da nodeList é 20
                            }
                        }

                        if (vertex.position.x < minVector.x || index == 0) minVector.x = vertex.position.x;
                        if (vertex.position.y < minVector.y || index == 0) minVector.y = vertex.position.y;
                        if (vertex.position.z < minVector.z || index == 0) minVector.z = vertex.position.z;

                        if (vertex.position.x > maxVector.x) maxVector.x = vertex.position.x;
                        if (vertex.position.y > maxVector.y) maxVector.y = vertex.position.y;
                        if (vertex.position.z > maxVector.z) maxVector.z = vertex.position.z;

                        index++;
                    }

                    //Guarda o último objeto
                    byte[] vtxBuffer = ((MemoryStream)vertices.BaseStream).ToArray();
                    vertexData vtxData = new vertexData();
                    vtxData.buffer = vtxBuffer;
                    indexList.Add(new faceData(indices, nodeList));
                    indices.Close();
                    vtxData.faces.AddRange(indexList);
                    vertexList.Add(vtxData);
                    vertices.Close();

                    foreach (vertexData v in vertexList)
                    {
                        uint vshAttributesBufferAddress = (uint)data.BaseStream.Position;
                        data.Write(v.buffer);

                        uint vshAttributesBufferCommandsOffset = (uint)gpuCommands.BaseStream.Position;
                        gpuCommands.setVSHAttributesCommands(vshAttributesBufferAddress, new List<bool> {true, 
                            modelObject.hasNormal, 
                            modelObject.hasTangent, 
                            modelObject.hasColor,
                            modelObject.texUVCount > 0, 
                            modelObject.texUVCount > 1, 
                            modelObject.texUVCount > 2, 
                            modelObject.hasNode, 
                            modelObject.hasWeight});

                        writeRelocatableOffset(vshAttributesBufferCommandsOffset + 0x20, 0x5c, relocationTable);
                        writeRelocatableOffset(vshAttributesBufferCommandsOffset + 0x30, 0x4c, relocationTable);

                        //Header do objeto
                        mainHeader.Write(modelObject.materialId);
                        mainHeader.Write((ushort)0); //Flags
                        mainHeader.Write((ushort)objIndex); //Node ID
                        mainHeader.Write(modelObject.renderPriority);
                        writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 4, relocationTable);
                        mainHeader.Write(vshAttributesBufferCommandsOffset);
                        mainHeader.Write(0x24); //Commands Word Count
                        mainHeader.Write(0); //Faces Header offset (place holder)
                        mainHeader.Write(v.faces.Count);
                        mainHeader.Write(vshAttributesBufferCommandsOffset + 0xf0); //Extra Commands Offset
                        mainHeader.Write(0x1c); //Extra Commands Word Count
                        mainHeader.Write((minVector.x + maxVector.x) / 2f); //Center position
                        mainHeader.Write((minVector.y + maxVector.y) / 2f);
                        mainHeader.Write((minVector.z + maxVector.z) / 2f);
                        mainHeader.Write(0);
                        mainHeader.Write(0);
                        mainHeader.Write(0);

                        /*
                         * Faces 
                         */
                        uint faceCount = 0;
                        foreach (faceData f in v.faces)
                        {
                            uint indexBufferAddress = (uint)data.BaseStream.Position;
                            data.Write(f.buffer);

                            //Face Data Header
                            uint indexBufferCommandsOffset = (uint)gpuCommands.BaseStream.Position;
                            gpuCommands.setIndexCommands(indexBufferAddress, (uint)(f.buffer.Length / (modelObject.obj.Count > byte.MaxValue ? 2 : 1)), 0);
                            writeRelocatableOffset(indexBufferCommandsOffset + 0x10, (byte)(modelObject.obj.Count > byte.MaxValue ? 0x4e : 0x50), relocationTable);

                            //Face Header
                            mainHeaderFace.Write((ushort)RenderBase.OSkinningMode.smoothSkinning); //Skinning Mode
                            mainHeaderFace.Write((ushort)(f.nodes.Length / 2));
                            mainHeaderFace.Write(f.nodes);
                            for (int i = 0; i < 40 - f.nodes.Length; i++) mainHeaderFace.Write((byte)0); //Fill remaining space
                            mainHeaderFace.Write(indexBufferCommandsOffset);
                            mainHeaderFace.Write(0x18); //Commands Word Count
                            faceCount++;
                        }
                        faceCounts.Add(faceCount);
                        align(0xf, data);

                        uint vshExtraAttributesBufferCommandsOffset = (uint)gpuCommands.BaseStream.Position;
                        gpuCommands.setVSHExtraAttributesCommands();
                    }

                    if (modelObject.isVisible) meshNodeVisibility |= (uint)(1 << objIndex);
                    objIndex++;
                }

                uint faceHeaderOffset = (uint)mainHeader.BaseStream.Position;
                mainHeader.Write(((MemoryStream)mainHeaderFace.BaseStream).ToArray());
                mainHeaderFace.Close();

                for (int i = 0; i < objIndex; i++)
                {
                    mainHeader.Seek((int)(verticesTableOffset + (i * 0x38) + 0x10), SeekOrigin.Begin);
                    writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
                    mainHeader.Write(faceHeaderOffset);
                    for (int j = 0; j < faceCounts[i]; j++)
                    {
                        writeRelocatableOffset((uint)(faceHeaderOffset + 0x2c), 4, relocationTable);
                        faceHeaderOffset += 0x34;
                    }
                }

                long currentPosition = mainHeader.BaseStream.Position;
                mainHeader.BaseStream.Seek(modelHeaderPosition, SeekOrigin.Begin);
                writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
                mainHeader.Write(materialsTableOffset);
                mainHeader.Write((uint)mdl.material.Count);
                mainHeader.Write(0);
                writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
                mainHeader.Write(verticesTableOffset);
                mainHeader.Write((uint)mdl.modelObject.Count);
                for (int i = 0; i < 10; i++) mainHeader.Write(verticesTableOffset); //Layer related
                mainHeader.Write(0);
                mainHeader.Write(0);
                mainHeader.Write(0);
                writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
                mainHeader.Write((uint)mainHeader.BaseStream.Position + 0x1c); //Nesh Node Visibility
                mainHeader.Write((uint)mdl.modelObject.Count); //Mesh Node count
                writeString(mdl.name, mainHeader, stringTable, relocationTable);
                mainHeader.Write(0);
                mainHeader.Write(0);
                mainHeader.Write(0);
                mainHeader.Write(0);
                mainHeader.Write(meshNodeVisibility);

                mainHeader.BaseStream.Seek(currentPosition, SeekOrigin.Begin);
            }

            using (BinaryWriter output = new BinaryWriter(new MemoryStream()))
            {
                uint mainHeaderOffset = 0x44;

                output.Write(Encoding.ASCII.GetBytes("BCH"));
                output.Write((byte)0);
                output.Write((byte)0x21);
                output.Write((byte)0x21);
                output.Write((ushort)42921);

                output.Write(mainHeaderOffset);
                output.Write(0);
                output.Write(0);
                output.Write(0);
                output.Write(0);
                output.Write(0);

                output.Write(0);
                output.Write((uint)stringTable.BaseStream.Length);
                output.Write((uint)gpuCommands.BaseStream.Length);
                output.Write((uint)data.BaseStream.Length);
                output.Write((uint)dataExtended.BaseStream.Length);
                output.Write(0);
                output.Write(8);
                output.Write(0);

                output.Write((ushort)1);
                output.Write((ushort)2);

                output.BaseStream.Seek(mainHeaderOffset, SeekOrigin.Begin);

                BinaryReader reader = new BinaryReader(mainHeader.BaseStream);
                mainHeader.BaseStream.Seek(0, SeekOrigin.Begin);
                uint offset = reader.ReadUInt32();
                mainHeader.BaseStream.Seek(offset, SeekOrigin.Begin);
                foreach (uint o in modelsPointerTable)
                {
                    writeRelocatableOffset((uint)mainHeader.BaseStream.Position, 0, relocationTable);
                    mainHeader.Write(o);
                }

                output.Write(((MemoryStream)mainHeader.BaseStream).ToArray());
                uint stringTableOffset = (uint)output.BaseStream.Position;
                output.Write(((MemoryStream)stringTable.BaseStream).ToArray());
                align(0xf, output);
                uint descriptionOffset = (uint)output.BaseStream.Position;
                output.Write(((MemoryStream)gpuCommands.BaseStream).ToArray());
                align(0x7f, output);
                uint dataOffset = (uint)output.BaseStream.Position;
                output.Write(((MemoryStream)data.BaseStream).ToArray());
                align(0x7f, output);
                uint dataExtendedOffset = (uint)output.BaseStream.Position;
                output.Write(((MemoryStream)dataExtended.BaseStream).ToArray());
                align(0x7f, output);
                uint relocationTableOffset = (uint)output.BaseStream.Position;
                output.Write(((MemoryStream)relocationTable.BaseStream).ToArray());

                output.Seek(0xc, SeekOrigin.Begin);
                output.Write(stringTableOffset);
                output.Write(descriptionOffset);
                output.Write(dataOffset);
                output.Write(dataExtendedOffset);
                output.Write(relocationTableOffset);
                output.BaseStream.Seek(0x14, SeekOrigin.Current);
                output.Write((uint)relocationTable.BaseStream.Length);

                mainHeader.Close();
                stringTable.Close();
                gpuCommands.Dispose();
                data.Close();
                dataExtended.Close();
                relocationTable.Close();

                return ((MemoryStream)output.BaseStream).ToArray();
            }
        }

        private static void setColor(Color color, BinaryWriter output)
        {
            output.Write(color.R);
            output.Write(color.G);
            output.Write(color.B);
            output.Write(color.A);
        }

        private static void writeString(string text, BinaryWriter output)
        {
            output.Write(Encoding.ASCII.GetBytes(text));
            output.Write((byte)0);
        }

        private static void writeString(string text, BinaryWriter output, BinaryWriter stringTable, BinaryWriter relocationTable)
        {
            if (text != null)
            {
                relocationTable.Write((uint)(output.BaseStream.Position & 0xffffff) | 0x02000000);
                output.Write((uint)stringTable.BaseStream.Position);
                writeString(text, stringTable);
            }
            else
            {
                output.Write(0);
            }
        }

        private static void writeRelocatableOffset(uint offset, byte flags, BinaryWriter relocationTable, bool justIgnore = false)
        {
            uint value = ((offset / 4) & 0xffffff) | ((uint)flags << 24);
            relocationTable.Write(value);
        }

        private static void align(uint mask, BinaryWriter output)
        {
            while ((output.BaseStream.Position & mask) != 0) output.Write((byte)0);
        }

        #endregion

    }
}
