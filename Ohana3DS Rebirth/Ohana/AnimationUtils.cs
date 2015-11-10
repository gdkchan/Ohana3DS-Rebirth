using System.Collections.Generic;

namespace Ohana3DS_Rebirth.Ohana
{
    class AnimationUtils
    {
        /// <summary>
        ///     Gets the smaller and closest frame to the given Key Frame.
        /// </summary>
        /// <param name="keyFrames">List with the Key Frames</param>
        /// <param name="frame">The frame number used as reference</param>
        /// <returns></returns>
        public static RenderBase.OInterpolationFloat getSmallerPoint(List<RenderBase.OInterpolationFloat> keyFrames, float frame)
        {
            if (keyFrames == null || keyFrames.Count == 0) return null;
            RenderBase.OInterpolationFloat value = keyFrames[0];
            foreach (RenderBase.OInterpolationFloat key in keyFrames)
            {
                if (key.frame >= value.frame && key.frame <= frame) value = key;
            }

            return value;
        }

        /// <summary>
        ///     Gets the larger and closest frame to the given Key Frame.
        /// </summary>
        /// <param name="keyFrames">List with the Key Frames</param>
        /// <param name="frame">The frame number used as reference</param>
        /// <returns></returns>
        public static RenderBase.OInterpolationFloat getLargerPoint(List<RenderBase.OInterpolationFloat> keyFrames, float frame)
        {
            if (keyFrames == null || keyFrames.Count == 0) return null;
            RenderBase.OInterpolationFloat value = keyFrames[keyFrames.Count - 1];
            foreach (RenderBase.OInterpolationFloat key in keyFrames)
            {
                if (key.frame <= value.frame && key.frame >= frame) value = key;
            }

            return value;
        }

        /// <summary>
        ///     Gets the smaller point between two Key Frames.
        ///     It doesn't actually interpolates anything, just returns the closest value.
        /// </summary>
        /// <param name="keyFrames">The list with all available Key Frames (Linear format)</param>
        /// <param name="frame">The frame number that should be returned</param>
        /// <returns>The closest smaller frame value</returns>
        public static float interpolateStep(List<RenderBase.OInterpolationFloat> keyFrames, float frame)
        {
            return getSmallerPoint(keyFrames, frame).value;
        }

        /// <summary>
        ///     Interpolates a point between two Key Frames on a given Frame using Linear Interpolation.
        /// </summary>
        /// <param name="keyFrames">The list with all available Key Frames (Linear format)</param>
        /// <param name="frame">The frame number that should be interpolated</param>
        /// <returns>The interpolated frame value</returns>
        public static float interpolateLinear(List<RenderBase.OInterpolationFloat> keyFrames, float frame)
        {
            RenderBase.OInterpolationFloat a = getSmallerPoint(keyFrames, frame);
            RenderBase.OInterpolationFloat b = getLargerPoint(keyFrames, frame);
            if (a.frame == b.frame) return a.value;

            float mu = (frame - a.frame) / (b.frame - a.frame);
            return (a.value * (1 - mu) + b.value * mu);
        }

        /// <summary>
        ///     Interpolates a point between two vectors using Linear Interpolation.
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <param name="mu">Value between 0-1 of the interpolation amount</param>
        /// <returns></returns>
        public static RenderBase.OVector3 interpolateLinear(RenderBase.OVector3 a, RenderBase.OVector3 b, float mu)
        {
            RenderBase.OVector3 output = new RenderBase.OVector3();

            output.x = interpolateLinear(a.x, b.x, mu);
            output.y = interpolateLinear(a.y, b.y, mu);
            output.z = interpolateLinear(a.z, b.z, mu);

            return output;
        }

        /// <summary>
        ///     Interpolates a point between two vectors using Linear Interpolation.
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <param name="mu">Value between 0-1 of the interpolation amount</param>
        /// <returns></returns>
        public static RenderBase.OVector4 interpolateLinear(RenderBase.OVector4 a, RenderBase.OVector4 b, float mu)
        {
            RenderBase.OVector4 output = new RenderBase.OVector4();

            output.x = interpolateLinear(a.x, b.x, mu);
            output.y = interpolateLinear(a.y, b.y, mu);
            output.z = interpolateLinear(a.z, b.z, mu);
            output.w = interpolateLinear(a.w, b.w, mu);

            return output;
        }

        /// <summary>
        ///     Interpolates a point between two points using Linear Interpolation.
        /// </summary>
        /// <param name="a">First point</param>
        /// <param name="b">Second point</param>
        /// <param name="mu">Value between 0-1 of the interpolation amount</param>
        /// <returns></returns>
        public static float interpolateLinear(float a, float b, float mu)
        {
            return (a * (1 - mu) + b * mu);
        }

        /// <summary>
        ///     Interpolates a point between two Key Frames on a given Frame using Hermite Interpolation.
        /// </summary>
        /// <param name="keyFrames">The list with all available Key Frames (Hermite format)</param>
        /// <param name="frame">The frame number that should be interpolated</param>
        /// <returns>The interpolated frame value</returns>
        public static float interpolateHermite(List<RenderBase.OInterpolationFloat> keyFrames, float frame)
        {
            RenderBase.OInterpolationFloat a = getSmallerPoint(keyFrames, frame);
            RenderBase.OInterpolationFloat b = getLargerPoint(keyFrames, frame);
            if (a.frame == b.frame) return a.value;

            float outSlope = a.outSlope;
            float inSlope = b.inSlope;
            float distance = frame - a.frame;
            float invDuration = 1f / (b.frame - a.frame);
            float t = distance * invDuration;
            float t1 = t - 1f;
            return (a.value + ((((a.value - b.value) * ((2f * t) - 3f)) * t) * t)) + ((distance * t1) * ((t1 * outSlope) + (t * inSlope)));
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
                case RenderBase.OInterpolationMode.step: return interpolateStep(sourceFrame.keyFrames, frame);
                case RenderBase.OInterpolationMode.linear: return interpolateLinear(sourceFrame.keyFrames, frame);
                case RenderBase.OInterpolationMode.hermite: return interpolateHermite(sourceFrame.keyFrames, frame);
                default: return 0; //Shouldn't happen
            }
        }
    }
}
