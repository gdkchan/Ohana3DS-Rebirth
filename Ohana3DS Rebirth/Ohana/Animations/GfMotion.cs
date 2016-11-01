using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ohana3DS_Rebirth.Ohana.Animations
{
    class GfMotion
    {
        public static List<RenderBase.OSkeletalAnimation> load(Stream data)
        {
            List<RenderBase.OSkeletalAnimation> output = new List<RenderBase.OSkeletalAnimation>();

            BinaryReader input = new BinaryReader(data);

            uint animCount = input.ReadUInt32();

            for (int anm = 0; anm < animCount; anm++)
            {
                data.Seek(4 + anm * 4, SeekOrigin.Begin);

                uint animAddr = input.ReadUInt32();
                if (animAddr == 0) continue;

                RenderBase.OSkeletalAnimation anim = new RenderBase.OSkeletalAnimation();
                anim.name = "anim_" + anm;
                anim.frameSize = 1;

                data.Seek(animAddr + 4, SeekOrigin.Begin);

                uint unkFlags = input.ReadUInt32();
                uint unkCount = input.ReadUInt32();
                data.Seek(0x24 + unkCount * 0xc, SeekOrigin.Current);

                uint boneNamesCount = input.ReadUInt32();
                uint boneNamesLength = input.ReadUInt32();

                long boneNamesStart = data.Position;

                string[] boneNames = new string[boneNamesCount];

                for (int b = 0; b < boneNamesCount; b++)
                {
                    boneNames[b] = IOUtils.readStringWithLength(input, input.ReadByte());
                }

                data.Seek(boneNamesStart + boneNamesLength, SeekOrigin.Begin);

                byte bbone = 0;

                for (int b = 0; b < boneNames.Length; b++)
                {
                    uint flags = input.ReadUInt32();
                    uint frameLength = input.ReadUInt32();
                    long frameStart = data.Position;

                    RenderBase.OSkeletalAnimationBone bone = new RenderBase.OSkeletalAnimationBone();

                    bone.name = boneNames[b];
                    bone.isAxisAngle = flags >> 31 == 0;

                    for (int axis = 0; axis < 9; axis++)
                    {
                        bool axisExists = ((flags >> (2 + axis * 3)) & 1) != 0;
                        bool axisConst = ((flags >> (axis * 3)) & 3) == 3;

                        bool mul2 = axis > 2 && axis < 6 && (flags >> 31) == 0;

                        if (axisConst) addFrame(bone, mul2, axis, input.ReadSingle());
                        if (!axisExists) continue;

                        uint keyFramesCount = input.ReadUInt32();

                        byte[] keyFrames = new byte[keyFramesCount];

                        for (int n = 0; n < keyFramesCount; n++) { keyFrames[n] = input.ReadByte(); if (keyFrames[n] > bbone) bbone = keyFrames[n]; }
                        while ((data.Position & 3) != 0) input.ReadByte();

                        float valueScale = input.ReadSingle();
                        float valueOffset = input.ReadSingle();
                        float slopeScale = input.ReadSingle();
                        float slopeOffset = input.ReadSingle();

                        for (int i = 0; i < keyFramesCount; i++)
                        {
                            ushort qvalue = input.ReadUInt16();
                            ushort qslope = input.ReadUInt16();

                            float value = valueOffset + (qvalue / (float)0xffff) * valueScale;
                            float slope = slopeOffset + (qslope / (float)0xffff) * slopeScale;

                            addFrame(bone, mul2, axis, value, keyFrames[i], slope);
                        }
                    }

                    anim.bone.Add(bone);
                }

                foreach (var b in anim.bone)
                {
                    b.scaleX.interpolation = RenderBase.OInterpolationMode.hermite;
                    b.scaleY.interpolation = RenderBase.OInterpolationMode.hermite;
                    b.scaleZ.interpolation = RenderBase.OInterpolationMode.hermite;

                    b.rotationX.interpolation = RenderBase.OInterpolationMode.hermite;
                    b.rotationY.interpolation = RenderBase.OInterpolationMode.hermite;
                    b.rotationZ.interpolation = RenderBase.OInterpolationMode.hermite;

                    b.translationX.interpolation = RenderBase.OInterpolationMode.hermite;
                    b.translationY.interpolation = RenderBase.OInterpolationMode.hermite;
                    b.translationZ.interpolation = RenderBase.OInterpolationMode.hermite;

                    b.scaleX.exists = b.scaleX.keyFrames.Count > 0;
                    b.scaleY.exists = b.scaleY.keyFrames.Count > 0;
                    b.scaleZ.exists = b.scaleZ.keyFrames.Count > 0;

                    b.rotationX.exists = b.rotationX.keyFrames.Count > 0;
                    b.rotationY.exists = b.rotationY.keyFrames.Count > 0;
                    b.rotationZ.exists = b.rotationZ.keyFrames.Count > 0;

                    b.translationX.exists = b.translationX.keyFrames.Count > 0;
                    b.translationY.exists = b.translationY.keyFrames.Count > 0;
                    b.translationZ.exists = b.translationZ.keyFrames.Count > 0;
                }

                if (bbone > 0) anim.frameSize = bbone;

                output.Add(anim);
            }

            data.Close();

            return output;
        }

        private static void addFrame(
            RenderBase.OSkeletalAnimationBone bone,
            bool mul2,
            int axis, 
            float val, 
            float frame = 0, 
            float slope = 0)
        {
            RenderBase.OAnimationKeyFrame frm = new RenderBase.OAnimationKeyFrame();

            if (mul2) val *= 2;

            frm.frame = frame;
            frm.inSlope = slope;
            frm.outSlope = slope;
            frm.value = val;

            switch (axis)
            {
                case 0: bone.scaleX.keyFrames.Add(frm); break;
                case 1: bone.scaleY.keyFrames.Add(frm); break;
                case 2: bone.scaleZ.keyFrames.Add(frm); break;

                case 3: bone.rotationX.keyFrames.Add(frm); break;
                case 4: bone.rotationY.keyFrames.Add(frm); break;
                case 5: bone.rotationZ.keyFrames.Add(frm); break;

                case 6: bone.translationX.keyFrames.Add(frm); break;
                case 7: bone.translationY.keyFrames.Add(frm); break;
                case 8: bone.translationZ.keyFrames.Add(frm); break;
            }
        }
    }
}
