using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ohana3DS_Rebirth.Ohana
{
    class AnimationHelper
    {
        private static float interpolateStep(List<RenderBase.OLinearFloat> keyFrames, float frame)
        {
            float minFrame = 0;
            float a = 0;

            foreach (RenderBase.OLinearFloat key in keyFrames)
            {
                if (key.frame >= minFrame && key.frame <= frame)
                {
                    minFrame = key.frame;
                    a = key.value;
                }

            }

            return a;
        }

        /// <summary>
        ///     Interpolates a point between two Key Frames on a given Frame using Linear Interpolation.
        /// </summary>
        /// <param name="keyFrames">The list with all available Key Frames (Linear format)</param>
        /// <param name="frame">The frame number that should be interpolated</param>
        /// <returns>The interpolated frame value</returns>
        public static float interpolate(List<RenderBase.OLinearFloat> keyFrames, float frame)
        {
            float minFrame = 0;
            float maxFrame = float.MaxValue;

            float a = 0;
            float b = 0;

            foreach (RenderBase.OLinearFloat key in keyFrames)
            {
                if (key.frame >= minFrame && key.frame <= frame)
                {
                    minFrame = key.frame;
                    a = key.value;
                }

                if (key.frame <= maxFrame && key.frame >= frame)
                {
                    maxFrame = key.frame;
                    b = key.value;
                }
            }
            if (minFrame == maxFrame) return a;

            float mu = (frame - minFrame) / (maxFrame - minFrame);
            return (a * (1 - mu) + b * mu);
        }

        private struct hermite
        {
            public float value;
            public float inSlope;
            public float outSlope;
        }

        /// <summary>
        ///     Interpolates a point between two Key Frames on a given Frame using Hermite Interpolation.
        /// </summary>
        /// <param name="keyFrames">The list with all available Key Frames (Hermite format)</param>
        /// <param name="frame">The frame number that should be interpolated</param>
        /// <returns>The interpolated frame value</returns>
        public static float interpolate(List<RenderBase.OHermiteFloat> keyFrames, float frame)
        {
            float minFrame = 0;
            float maxFrame = float.MaxValue;

            hermite a = new hermite();
            hermite b = new hermite();

            foreach (RenderBase.OHermiteFloat key in keyFrames)
            {
                if (key.frame >= minFrame && key.frame <= frame)
                {
                    minFrame = key.frame;
                    a.value = key.value;
                    a.inSlope = key.inSlope;
                    a.outSlope = key.outSlope;
                }

                if (key.frame <= maxFrame && key.frame >= frame)
                {
                    maxFrame = key.frame;
                    b.value = key.value;
                    b.inSlope = key.inSlope;
                    b.outSlope = key.outSlope;
                }
            }
            if (minFrame == maxFrame) return a.value;

            float mu = (frame - minFrame) / (maxFrame - minFrame);
            float mu2 = mu * mu;
            float mu3 = mu2 * mu;
            float m0 = a.outSlope / 2;
            m0 += (b.value - a.value) / 2;
            float m1 = (b.value - a.value) / 2;
            m1 += b.inSlope / 2;
            float a0 = 2 * mu3 - 3 * mu2 + 1;
            float a1 = mu3 - 2 * mu2 + mu;
            float a2 = mu3 - mu2;
            float a3 = -2 * mu3 + 3 * mu2;

            return (a0 * a.value + a1 * m0 + a2 * m1 + a3 * b.value);
        }

        /// <summary>
        ///     Interpolates a Key Frame from a list of Key Frames.
        /// </summary>
        /// <param name="sourceFrame">The list of key frames</param>
        /// <param name="frame">The frame that should be returned or interpolated from the list</param>
        /// <returns></returns>
        public static float getKey(RenderBase.OAnimationKeyFrame sourceFrame, float frame)
        {
            switch (sourceFrame.interpolation)
            {
                case RenderBase.OInterpolationMode.step: return interpolateStep(sourceFrame.linearFrame, frame);
                case RenderBase.OInterpolationMode.linear: return interpolate(sourceFrame.linearFrame, frame);
                case RenderBase.OInterpolationMode.hermite: return interpolate(sourceFrame.hermiteFrame, frame);
                default: return 0; //Shouldn't happen
            }
        }

        /// <summary>
        ///     Converts global Frame number to segment Frame number.
        /// </summary>
        /// <param name="sourceFrame">The list of key frames</param>
        /// <param name="frame">The frame that should be verified</param>
        /// <returns></returns>
        public static float getFrameNumber(RenderBase.OAnimationKeyFrame sourceFrame, float frame)
        {
            //TODO
            if (frame < sourceFrame.startFrame)
            {
                switch (sourceFrame.preRepeat)
                {
                    case RenderBase.ORepeatMethod.none: return sourceFrame.startFrame;
                }
            }

            if (frame > sourceFrame.endFrame)
            {
                switch (sourceFrame.postRepeat)
                {
                    case RenderBase.ORepeatMethod.none: return sourceFrame.endFrame;
                }
            }

            return frame;
        }
    }
}
