using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana.PICA200;

namespace Ohana3DS_Rebirth.Ohana
{
    class CGFX
    {
        private struct cgfxHeader
        {
            public string magic;
            public ushort endian;
            public ushort length;
            public uint revision;
            public uint fileLength;
            public uint entries;
        }

        private class dataHeader
        {
            public string magic;
            public uint length;
            public List<dictEntry> models;
            public List<dictEntry> textures;
            public List<dictEntry> lookUpTables;
            public List<dictEntry> materials;
            public List<dictEntry> shaders;
            public List<dictEntry> cameras;
            public List<dictEntry> lights;
            public List<dictEntry> fogs;
            public List<dictEntry> scenes;
            public List<dictEntry> skeletalAnimations;
            public List<dictEntry> materialAnimations;
            public List<dictEntry> visibilityAnimations;
            public List<dictEntry> cameraAnimations;
            public List<dictEntry> lightAnimations;
            public List<dictEntry> emitters;
        }

        private struct dictEntry
        {
            public uint nameOffset;
            public uint dataOffset;
        }

        private struct cmdlHeader
        {
            public bool hasSkeleton;
            public string modelName;
            public uint userDataEntries;
            public uint userDataDictionaryOffset;
            public bool isBranchVisible;
            public uint childCount;
            public uint animationGroupEntries;
            public uint animationGroupDictionaryOffset;
            public RenderBase.OVector3 transformScale;
            public RenderBase.OVector3 transformRotate;
            public RenderBase.OVector3 transformTranslate;
            public RenderBase.OMatrix localMatrix;
            public RenderBase.OMatrix worldMatrix;
            public uint objectEntries;
            public uint objectPointerTableOffset;
            public uint materialEntries;
            public uint materialDictionaryOffset;
            public uint shapeEntries;
            public uint shapePointerTableOffset;
            public uint objectNodeVisibilityEntries;
            public uint objectNodeVisibilityDictionaryOffset;
            public bool isVisible;
            public bool isNonUniformScalable;
            public RenderBase.OModelCullingMode cullMode;
            public uint layerId;
            public uint skeletonOffset;
        }

        private struct cgfxShapeEntry
        {
            public string name;
            public uint userDataEntries;
            public uint userDataDictionaryOffset;
            public uint boundingBoxOffset;
            public RenderBase.OVector3 positionOffset;
            public uint facesGroupEntries;
            public uint facesGroupOffset;
            public uint vertexGroupEntries;
            public uint vertexGroupOffset;
            public uint blendShapeOffset;
        }

        private struct cgfxObjectEntry
        {
            public string name;
            public uint userDataEntries;
            public uint userDataDictionaryOffset;
            public uint shapeIndex;
            public uint materialId;
            public uint ownerModelOffset;
            public bool isVisible;
            public byte renderPriority;
            public ushort objectNodeVisibilityIndex;
            public ushort currentPrimitiveIndex;
        }

        private enum attributeFormatType
        {
            signedByte = 0,
            unsignedByte = 1,
            signedShort = 2,
            single = 6
        }

        private struct attributeFormat
        {
            public PICACommand.vshAttribute attribute;
            public attributeFormatType type;
            public uint attributeLength;
            public float scale;
            public uint offset;
            public bool isInterleaved;
        }

        private enum cgfxSkeletonScalingRule
        {
            standard = 0,
            maya = 1,
            softImage = 2
        }

        public static RenderBase.OModelGroup load(MemoryStream data)
        {
            BinaryReader input = new BinaryReader(data);

            RenderBase.OModelGroup models = new RenderBase.OModelGroup();

            cgfxHeader header = new cgfxHeader();
            header.magic = IOUtils.readString(input, 0, 4);
            header.endian = input.ReadUInt16();
            header.length = input.ReadUInt16();
            header.revision = input.ReadUInt32();
            header.fileLength = input.ReadUInt32();
            header.entries = input.ReadUInt32();

            data.Seek(header.length, SeekOrigin.Begin);
            dataHeader dataHeader = new dataHeader();
            dataHeader.magic = IOUtils.readString(input, (uint)data.Position, 4);
            dataHeader.length = input.ReadUInt32();
            dataHeader.models = getDictionary(input);
            dataHeader.textures = getDictionary(input);
            dataHeader.lookUpTables = getDictionary(input);
            dataHeader.materials = getDictionary(input);
            dataHeader.shaders = getDictionary(input);
            dataHeader.cameras = getDictionary(input);
            dataHeader.lights = getDictionary(input);
            dataHeader.fogs = getDictionary(input);
            dataHeader.scenes = getDictionary(input);
            dataHeader.skeletalAnimations = getDictionary(input);
            dataHeader.materialAnimations = getDictionary(input);
            dataHeader.visibilityAnimations = getDictionary(input);
            dataHeader.cameraAnimations = getDictionary(input);
            dataHeader.lightAnimations = getDictionary(input);
            dataHeader.emitters = getDictionary(input);

            foreach (dictEntry modelEntry in dataHeader.models)
            {
                data.Seek(modelEntry.dataOffset, SeekOrigin.Begin);

                cmdlHeader cmdlHeader = new cmdlHeader();
                
                uint flags = input.ReadUInt32();
                cmdlHeader.hasSkeleton = (flags & 0x80) > 0;
                string cmdlMagic = IOUtils.readString(input, (uint)input.BaseStream.Position, 4);
                uint revision = input.ReadUInt32();
                cmdlHeader.modelName = IOUtils.readString(input, (uint)data.Position + input.ReadUInt32());
                cmdlHeader.userDataEntries = input.ReadUInt32();
                cmdlHeader.userDataDictionaryOffset = getRelativeOffset(input);
                input.ReadUInt32();
                flags = input.ReadUInt32();
                cmdlHeader.isBranchVisible = (flags & 1) > 0;
                cmdlHeader.childCount = input.ReadUInt32();
                input.ReadUInt32(); //Unused
                cmdlHeader.animationGroupEntries = input.ReadUInt32();
                cmdlHeader.animationGroupDictionaryOffset = getRelativeOffset(input);
                cmdlHeader.transformScale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                cmdlHeader.transformRotate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                cmdlHeader.transformTranslate = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                cmdlHeader.localMatrix = getMatrix(input);
                cmdlHeader.worldMatrix = getMatrix(input);
                cmdlHeader.objectEntries = input.ReadUInt32();
                cmdlHeader.objectPointerTableOffset = getRelativeOffset(input);
                cmdlHeader.materialEntries = input.ReadUInt32();
                cmdlHeader.materialDictionaryOffset = getRelativeOffset(input);
                cmdlHeader.shapeEntries = input.ReadUInt32();
                cmdlHeader.shapePointerTableOffset = getRelativeOffset(input);
                cmdlHeader.objectNodeVisibilityEntries = input.ReadUInt32();
                cmdlHeader.objectNodeVisibilityDictionaryOffset = getRelativeOffset(input);
                flags = input.ReadUInt32();
                cmdlHeader.isVisible = (flags & 1) > 0;
                cmdlHeader.isNonUniformScalable = (flags & 0x100) > 0;
                cmdlHeader.cullMode = (RenderBase.OModelCullingMode)input.ReadUInt32();
                cmdlHeader.layerId = input.ReadUInt32();
                if (cmdlHeader.hasSkeleton) cmdlHeader.skeletonOffset = getRelativeOffset(input);

                RenderBase.OModel model = new RenderBase.OModel();
                model.name = cmdlHeader.modelName;
                model.transform = cmdlHeader.worldMatrix;
                model.addMaterial(new RenderBase.OMaterial());

                //Skeleton
                bool isSkeletonTranslateAnimationEnabled;
                if (cmdlHeader.hasSkeleton)
                {
                    data.Seek(cmdlHeader.skeletonOffset, SeekOrigin.Begin);

                    flags = input.ReadUInt32();
                    string skeletonMagic = IOUtils.readString(input, (uint)input.BaseStream.Position, 4); //SOBJ
                    revision = input.ReadUInt32();
                    string name = IOUtils.readString(input, getRelativeOffset(input));
                    input.ReadUInt32();
                    input.ReadUInt32();
                    List<dictEntry> skeletonDictionary = getDictionary(input);
                    uint rootBoneOffset = getRelativeOffset(input);
                    cgfxSkeletonScalingRule scalingRule = (cgfxSkeletonScalingRule)input.ReadUInt32();
                    isSkeletonTranslateAnimationEnabled = (input.ReadUInt32() & 1) > 0;

                    foreach (dictEntry boneEntry in skeletonDictionary)
                    {
                        data.Seek(boneEntry.dataOffset, SeekOrigin.Begin);

                        RenderBase.OBone bone = new RenderBase.OBone();

                        bone.name = IOUtils.readString(input, getRelativeOffset(input));
                        uint boneFlags = input.ReadUInt32();
                        
                        bone.isSegmentScaleCompensate = (boneFlags & 0x20) > 0;

                        uint boneId = input.ReadUInt32();
                        bone.parentId = (short)input.ReadInt32();
                        int parentOffset = input.ReadInt32();
                        int childOffset = input.ReadInt32();
                        int previousSiblingOffset = input.ReadInt32();
                        int nextSiblingOffset = input.ReadInt32();
                        bone.scale = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        bone.rotation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        bone.translation = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                        bone.absoluteScale = new RenderBase.OVector3(bone.scale);
                        RenderBase.OMatrix localMatrix = getMatrix(input);
                        RenderBase.OMatrix worldMatrix = getMatrix(input);
                        RenderBase.OMatrix invBaseMatrix = getMatrix(input);
                        bone.billboardMode = (RenderBase.OBillboardMode)input.ReadInt32();
                        uint userDataEntries = input.ReadUInt32();
                        uint userDataOffset = getRelativeOffset(input);

                        model.addBone(bone);
                    }
                }

                //Shapes
                List<cgfxShapeEntry> shapeHeader = new List<cgfxShapeEntry>();
                for (int i = 0; i < cmdlHeader.shapeEntries; i++)
                {
                    data.Seek(cmdlHeader.shapePointerTableOffset + (i * 4), SeekOrigin.Begin);
                    data.Seek(getRelativeOffset(input), SeekOrigin.Begin);

                    cgfxShapeEntry shape = new cgfxShapeEntry();

                    flags = input.ReadUInt32();
                    string sobjMagic = IOUtils.readString(input, (uint)input.BaseStream.Position, 4);
                    revision = input.ReadUInt32();
                    shape.name = IOUtils.readString(input, getRelativeOffset(input));
                    shape.userDataEntries = input.ReadUInt32();
                    shape.userDataDictionaryOffset = getRelativeOffset(input);
                    flags = input.ReadUInt32();
                    shape.boundingBoxOffset = getRelativeOffset(input);
                    shape.positionOffset = new RenderBase.OVector3(input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
                    shape.facesGroupEntries = input.ReadUInt32();
                    shape.facesGroupOffset = getRelativeOffset(input);
                    input.ReadUInt32();
                    shape.vertexGroupEntries = input.ReadUInt32();
                    shape.vertexGroupOffset = getRelativeOffset(input);
                    shape.blendShapeOffset = getRelativeOffset(input);

                    shapeHeader.Add(shape);
                }

                List<RenderBase.OModelObject> shapes = new List<RenderBase.OModelObject>();
                foreach (cgfxShapeEntry shape in shapeHeader)
                {
                    RenderBase.OModelObject shp = new RenderBase.OModelObject();

                    data.Seek(shape.vertexGroupOffset, SeekOrigin.Begin);
                    data.Seek(getRelativeOffset(input), SeekOrigin.Begin);

                    input.ReadUInt32(); //Useless name offset
                    input.ReadUInt32(); //Useless User Data entries
                    input.ReadUInt32(); //Useless User Data offset
                    uint bufferObject = input.ReadUInt32();
                    uint locationFlag = input.ReadUInt32();
                    uint vshAttributesBufferLength = input.ReadUInt32();
                    uint vshAttributesBufferOffset = getRelativeOffset(input);
                    uint locationAddress = input.ReadUInt32();
                    uint memoryArea = input.ReadUInt32();
                    uint vshAttributesBufferStride = input.ReadUInt32();
                    uint vshAttributesBufferComponentsEntries = input.ReadUInt32();
                    uint vshAttributesBufferComponentsOffset = getRelativeOffset(input);

                    List<attributeFormat> vshAttributeFormats = new List<attributeFormat>();
                    for (int i = 0; i < vshAttributesBufferComponentsEntries; i++)
                    {
                        data.Seek(vshAttributesBufferComponentsOffset + (i * 4), SeekOrigin.Begin);
                        data.Seek(getRelativeOffset(input), SeekOrigin.Begin);

                        attributeFormat format = new attributeFormat();

                        input.ReadUInt32();
                        format.attribute = (PICACommand.vshAttribute)input.ReadUInt32();
                        format.isInterleaved = input.ReadUInt32() == 2;
                        bufferObject = input.ReadUInt32();
                        locationFlag = input.ReadUInt32();
                        uint attributesStreamLength = input.ReadUInt32();
                        uint attributesStreamOffset = getRelativeOffset(input);
                        locationAddress = input.ReadUInt32();
                        memoryArea = input.ReadUInt32();
                        format.type = (attributeFormatType)(input.ReadUInt32() & 0xf);
                        format.attributeLength = input.ReadUInt32();
                        format.scale = input.ReadSingle();
                        format.offset = input.ReadUInt32();

                        vshAttributeFormats.Add(format);
                    }

                    List<RenderBase.CustomVertex> vshAttributesBuffer = new List<RenderBase.CustomVertex>();

                    //Faces
                    for (int faceIndex = 0; faceIndex < shape.facesGroupEntries; faceIndex++)
                    {
                        data.Seek(shape.facesGroupOffset + (faceIndex * 4), SeekOrigin.Begin);
                        data.Seek(getRelativeOffset(input), SeekOrigin.Begin);

                        uint nodeListEntries = input.ReadUInt32();
                        uint nodeListOffset = getRelativeOffset(input);
                        RenderBase.OSkinningMode skinningMode = (RenderBase.OSkinningMode)input.ReadUInt32();
                        uint faceMainHeaderEntries = input.ReadUInt32();
                        uint faceMainHeaderOffset = getRelativeOffset(input);

                        //Bone nodes
                        List<uint> nodeList = new List<uint>();
                        data.Seek(nodeListOffset, SeekOrigin.Begin);
                        for (int i = 0; i < nodeListEntries; i++) nodeList.Add(input.ReadUInt32());

                        //Face-related stuff
                        data.Seek(faceMainHeaderOffset, SeekOrigin.Begin);
                        data.Seek(getRelativeOffset(input), SeekOrigin.Begin);

                        uint faceDescriptorEntries = input.ReadUInt32();
                        uint faceDescriptorOffset = getRelativeOffset(input);
                        input.ReadUInt32();
                        input.ReadUInt32();
                        input.ReadUInt32();

                        data.Seek(faceDescriptorOffset, SeekOrigin.Begin);
                        data.Seek(getRelativeOffset(input), SeekOrigin.Begin);

                        PICACommand.indexBufferFormat idxBufferFormat = (PICACommand.indexBufferFormat)((input.ReadUInt32() & 2) >> 1);
                        input.ReadUInt32();
                        uint idxBufferLength = input.ReadUInt32();
                        uint idxBufferOffset = getRelativeOffset(input);

                        data.Seek(idxBufferOffset, SeekOrigin.Begin);
                        for (int i = 0; i < idxBufferLength; i++)
                        {
                            ushort index = 0;

                            switch (idxBufferFormat)
                            {
                                case PICACommand.indexBufferFormat.unsignedShort: index = input.ReadUInt16(); i++; break;
                                case PICACommand.indexBufferFormat.unsignedByte: index = input.ReadByte(); break;
                            }

                            long dataPosition = data.Position;
                            long vertexOffset = vshAttributesBufferOffset + (index * vshAttributesBufferStride);

                            RenderBase.OVertex vertex = new RenderBase.OVertex();
                            vertex.diffuseColor = 0xffffffff;
                            
                            for (int attribute = 0; attribute < vshAttributeFormats.Count; attribute++)
                            {
                                attributeFormat format = vshAttributeFormats[attribute];
                                data.Seek(vertexOffset + format.offset, SeekOrigin.Begin);
                                RenderBase.OVector4 vector =  getVector(input, format);

                                switch (format.attribute)
                                {
                                    case PICACommand.vshAttribute.position:
                                        float x = (vector.x * format.scale) + shape.positionOffset.x;
                                        float y = (vector.y * format.scale) + shape.positionOffset.y;
                                        float z = (vector.z * format.scale) + shape.positionOffset.z;
                                        vertex.position = new RenderBase.OVector3(x, y, z);
                                        break;
                                    case PICACommand.vshAttribute.normal:
                                        vertex.normal = new RenderBase.OVector3(vector.x * format.scale, vector.y * format.scale, vector.z * format.scale);
                                        break;
                                    case PICACommand.vshAttribute.tangent:
                                        vertex.tangent = new RenderBase.OVector3(vector.x * format.scale, vector.y * format.scale, vector.z * format.scale);
                                        break;
                                    case PICACommand.vshAttribute.color:
                                        uint r = (uint)((vector.x * format.scale) * 0xff);
                                        uint g = (uint)((vector.y * format.scale) * 0xff);
                                        uint b = (uint)((vector.z * format.scale) * 0xff);
                                        uint a = (uint)((vector.w * format.scale) * 0xff);
                                        vertex.diffuseColor = b | (g << 8) | (r << 16) | (a << 24);
                                        break;
                                    case PICACommand.vshAttribute.textureCoordinate0:
                                        vertex.texture0 = new RenderBase.OVector2(vector.x * format.scale, vector.y * format.scale);
                                        break;
                                    case PICACommand.vshAttribute.textureCoordinate1:
                                        vertex.texture1 = new RenderBase.OVector2(vector.x * format.scale, vector.y * format.scale);
                                        break;
                                    case PICACommand.vshAttribute.textureCoordinate2:
                                        vertex.texture2 = new RenderBase.OVector2(vector.x * format.scale, vector.y * format.scale);
                                        break;
                                    case PICACommand.vshAttribute.boneIndex:
                                        int b0 = (int)vector.x;
                                        int b1 = (int)vector.y;
                                        int b2 = (int)vector.z;
                                        int b3 = (int)vector.w;

                                        if (b0 < nodeList.Count) vertex.addNode((int)nodeList[b0]);
                                        if (b1 < nodeList.Count && format.attributeLength > 0) vertex.addNode((int)nodeList[b1]);
                                        if (b2 < nodeList.Count && format.attributeLength > 1) vertex.addNode((int)nodeList[b2]);
                                        if (b3 < nodeList.Count && format.attributeLength > 2) vertex.addNode((int)nodeList[b3]);

                                        break;
                                    case PICACommand.vshAttribute.boneWeight:
                                        vertex.addWeight(vector.x * format.scale);
                                        if (format.attributeLength > 0) vertex.addWeight(vector.y * format.scale);
                                        if (format.attributeLength > 1) vertex.addWeight(vector.z * format.scale);
                                        if (format.attributeLength > 2) vertex.addWeight(vector.w * format.scale);
                                        break;
                                }
                            }

                            //Like a Bounding Box, used to calculate the proportions of the mesh on the Viewport
                            if (vertex.position.x < models.minVector.x) models.minVector.x = vertex.position.x;
                            else if (vertex.position.x > models.maxVector.x) models.maxVector.x = vertex.position.x;
                            else if (vertex.position.y < models.minVector.y) models.minVector.y = vertex.position.y;
                            else if (vertex.position.y > models.maxVector.y) models.maxVector.y = vertex.position.y;
                            else if (vertex.position.z < models.minVector.z) models.minVector.z = vertex.position.z;
                            else if (vertex.position.z > models.maxVector.z) models.maxVector.z = vertex.position.z;

                            shp.addVertex(vertex);
                            vshAttributesBuffer.Add(RenderBase.convertVertex(vertex));

                            data.Seek(dataPosition, SeekOrigin.Begin);
                        }
                    }

                    shp.renderBuffer = vshAttributesBuffer.ToArray();
                    shapes.Add(shp);
                }

                //Objects
                List<cgfxObjectEntry> objectHeader = new List<cgfxObjectEntry>();
                for (int i = 0; i < cmdlHeader.objectEntries; i++)
                {
                    data.Seek(cmdlHeader.objectPointerTableOffset + (i * 4), SeekOrigin.Begin);
                    data.Seek(getRelativeOffset(input), SeekOrigin.Begin);

                    cgfxObjectEntry obj = new cgfxObjectEntry();

                    flags = input.ReadUInt32();
                    string msobMagic = IOUtils.readString(input, (uint)input.BaseStream.Position, 4);
                    revision = input.ReadUInt32();
                    obj.name = IOUtils.readString(input, getRelativeOffset(input));
                    obj.userDataEntries = input.ReadUInt32();
                    obj.userDataDictionaryOffset = getRelativeOffset(input);
                    obj.shapeIndex = input.ReadUInt32();
                    obj.materialId = input.ReadUInt32();
                    obj.ownerModelOffset = getRelativeOffset(input);
                    obj.isVisible = (input.ReadByte() & 1) > 0;
                    obj.renderPriority = input.ReadByte();
                    obj.objectNodeVisibilityIndex = input.ReadUInt16();
                    obj.currentPrimitiveIndex = input.ReadUInt16();
                    //Theres still a bunch of stuff after this, but isn't really needed

                    objectHeader.Add(obj);
                }

                foreach (cgfxObjectEntry obj in objectHeader)
                {
                    RenderBase.OModelObject modelObject = shapes[(int)obj.shapeIndex];

                    modelObject.name = obj.name;
                    modelObject.isVisible = true;
                    modelObject.renderPriority = obj.renderPriority;

                    model.addObject(modelObject);
                }

                models.addModel(model);
            }

            return models;
        }

        /// <summary>
        ///     Reads a Dictionary section from the CGFX file.
        ///     The Stream is advanced exactly 8 bytes.
        /// </summary>
        /// <param name="input">BinaryReader of the CGFX file</param>
        /// <returns></returns>
        private static List<dictEntry> getDictionary(BinaryReader input)
        {
            List<dictEntry> output = new List<dictEntry>();

            uint entries = input.ReadUInt32();
            uint dictionaryOffset = getRelativeOffset(input);

            if (entries > 0)
            {
                long position = input.BaseStream.Position;

                input.BaseStream.Seek(dictionaryOffset, SeekOrigin.Begin);
                string dictMagic = IOUtils.readString(input, dictionaryOffset, 4);
                uint dictLength = input.ReadUInt32();
                uint dictEntries = input.ReadUInt32();
                int rootNodeReference = input.ReadInt32();
                ushort rootNodeLeft = input.ReadUInt16();
                ushort rootNodeRight = input.ReadUInt16();
                uint rootNodeNameOffset = input.ReadUInt32(); //Is this even used?
                uint rootNodeDataOffset = input.ReadUInt32();
                for (int i = 0; i < dictEntries; i++)
                {
                    dictEntry entry = new dictEntry();

                    int referenceBit = input.ReadInt32(); //Radix tree
                    ushort leftNode = input.ReadUInt16();
                    ushort rightNode = input.ReadUInt16();
                    entry.nameOffset = getRelativeOffset(input);
                    entry.dataOffset = getRelativeOffset(input);

                    output.Add(entry);
                }

                input.BaseStream.Seek(position, SeekOrigin.Begin);
            }

            return output;
        }

        /// <summary>
        ///     Gets a relative offset and automaticaly transforms into a absolute offset.
        ///     If the offset is zero (data doesn't exists), it will remain zero.
        /// </summary>
        /// <param name="input">BinaryReader of the CGFX file</param>
        /// <returns></returns>
        private static uint getRelativeOffset(BinaryReader input)
        {
            uint position = (uint)input.BaseStream.Position;
            uint offset = input.ReadUInt32();
            if (offset != 0) offset += position;
            return offset;
        }

        /// <summary>
        ///     Reads a 4x3 Matrix from the file.
        /// </summary>
        /// <param name="input">BinaryReader of the CGFX file</param>
        /// <returns></returns>
        private static RenderBase.OMatrix getMatrix(BinaryReader input)
        {
            RenderBase.OMatrix output = new RenderBase.OMatrix();

            output.M11 = input.ReadSingle();
            output.M21 = input.ReadSingle();
            output.M31 = input.ReadSingle();
            output.M41 = input.ReadSingle();

            output.M12 = input.ReadSingle();
            output.M22 = input.ReadSingle();
            output.M32 = input.ReadSingle();
            output.M42 = input.ReadSingle();

            output.M13 = input.ReadSingle();
            output.M23 = input.ReadSingle();
            output.M33 = input.ReadSingle();
            output.M43 = input.ReadSingle();

            return output;
        }

        /// <summary>
        ///     Gets a Vector4 from Data.
        ///     Number of used elements of the Vector4 will depend on the vector type.
        /// </summary>
        /// <param name="input">CGFX reader</param>
        /// <param name="format">Format of the buffer data</param>
        /// <returns></returns>
        private static RenderBase.OVector4 getVector(BinaryReader input, attributeFormat format)
        {
            RenderBase.OVector4 output = new RenderBase.OVector4();

            switch (format.type)
            {
                case attributeFormatType.signedByte:
                    output.x = (sbyte)input.ReadByte();
                    if (format.attributeLength > 0) output.y = (sbyte)input.ReadByte();
                    if (format.attributeLength > 1) output.z = (sbyte)input.ReadByte();
                    if (format.attributeLength > 2) output.w = (sbyte)input.ReadByte();
                    break;
                case attributeFormatType.unsignedByte:
                    output.x = input.ReadByte();
                    if (format.attributeLength > 0) output.y = input.ReadByte();
                    if (format.attributeLength > 1) output.z = input.ReadByte();
                    if (format.attributeLength > 2) output.w = input.ReadByte();
                    break;
                case attributeFormatType.signedShort:
                    output.x = input.ReadInt16();
                    if (format.attributeLength > 0) output.y = input.ReadInt16();
                    if (format.attributeLength > 1) output.z = input.ReadInt16();
                    if (format.attributeLength > 2) output.w = input.ReadInt16();
                    break;
                case attributeFormatType.single:
                    output.x = input.ReadSingle();
                    if (format.attributeLength > 0) output.y = input.ReadSingle();
                    if (format.attributeLength > 1) output.z = input.ReadSingle();
                    if (format.attributeLength > 2) output.w = input.ReadSingle();
                    break;
            }

            return output;
        }
    }
}
