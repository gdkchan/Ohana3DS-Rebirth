//Ohana3DS 3D Rendering Engine by gdkchan
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Ohana3DS_Rebirth.Ohana
{
    class RenderEngine
    {
        private Effect fragmentShader;
        private PresentParameters pParams;
        private Device device;

        public RenderBase.OModelGroup model;
        private struct CustomTexture
        {
            public string name;
            public Texture texture;
        }
        private List<CustomTexture> textures = new List<CustomTexture>();

        public Vector2 translation;
        private Vector2 rotation;
        public float zoom;

        private bool keepRendering;

        private bool useLegacyTexturing = false; //Set to True to disable Fragment Shader

        float animationStep = 0.5f;
        int currentAnimation;
        int frame = 0;
        bool animate = false;

        /// <summary>
        ///     Initialize the renderer at a given target.
        /// </summary>
        /// <param name="handle">Memory pointer to the control rendering buffer</param>
        /// <param name="width">Render width</param>
        /// <param name="height">Render height</param>
        public void initialize(System.IntPtr handle, int width, int height)
        {
            pParams = new PresentParameters();
            pParams.BackBufferCount = 1;
            pParams.BackBufferFormat = Manager.Adapters[0].CurrentDisplayMode.Format;
            pParams.BackBufferWidth = width;
            pParams.BackBufferHeight = height;
            pParams.Windowed = true;
            pParams.SwapEffect = SwapEffect.Discard;
            pParams.EnableAutoDepthStencil = true;
            pParams.AutoDepthStencilFormat = DepthFormat.D24S8;

            try
            {
                device = new Device(0, DeviceType.Hardware, handle, CreateFlags.HardwareVertexProcessing, pParams);
            }
            catch
            {
                //Some crap GPUs only works with Software vertex processing
                device = new Device(0, DeviceType.Hardware, handle, CreateFlags.SoftwareVertexProcessing, pParams);
            }

            setupViewPort();
        }

        /// <summary>
        ///     Resizes the Back Buffer.
        /// </summary>
        /// <param name="width">New width</param>
        /// <param name="height">New height</param>
        public void resize(int width, int height)
        {
            pParams.BackBufferWidth = width;
            pParams.BackBufferHeight = height;
            device.Reset(pParams);
            setupViewPort();
        }

        private void setupViewPort()
        {
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, (float)pParams.BackBufferWidth / pParams.BackBufferHeight, 0.1f, 100.0f);
            device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, 20.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));

            device.RenderState.Lighting = false;
        }

        /// <summary>
        ///     Release all resources used by DirectX.
        ///     You MUST call this before closing the window the render target belongs.
        ///     Otherwise you will end up with memory leaks.
        /// </summary>
        public void dispose()
        {
            keepRendering = false;
            foreach (CustomTexture texture in textures) texture.texture.Dispose();
            foreach (RenderBase.OTexture texture in model.texture) texture.texture.Dispose();
            if (!useLegacyTexturing) fragmentShader.Dispose();
            device.Dispose();
        }

        public void render()
        {
            if (!useLegacyTexturing)
            {
                string compilationErros = null;
                fragmentShader = Effect.FromString(device, Ohana3DS_Rebirth.Properties.Resources.OFragmentShader, null, null, ShaderFlags.SkipOptimization, null, out compilationErros);
                if (compilationErros != "") MessageBox.Show("Failed to compile Fragment Shader!" + Environment.NewLine + compilationErros, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fragmentShader.Technique = "Combiner";
            }

            foreach (RenderBase.OTexture texture in model.texture)
            {
                CustomTexture tex;
                tex.name = texture.name;
                Bitmap bmp = new Bitmap(texture.texture);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                tex.texture = new Texture(device, bmp, Usage.None, Pool.Managed);
                textures.Add(tex);
                bmp.Dispose();
            }

            //loadAnimation(3);
            //playAnimation();
            keepRendering = true;
            while (keepRendering)
            {
                device.Clear(ClearFlags.Stencil | ClearFlags.Target | ClearFlags.ZBuffer, 0x5f5f5f, 1.0f, 0);
                device.BeginScene();

                float minSize = Math.Min(Math.Min(model.minVector.x, model.minVector.y), model.minVector.z);
                float maxSize = Math.Max(Math.Max(model.maxVector.x, model.maxVector.y), model.maxVector.z);
                float scale = (10.0f / (maxSize - minSize)); //Try to adjust to screen
                Matrix centerMatrix = Matrix.Translation(
                    -(model.minVector.x + model.maxVector.x) / 2,
                    -(model.minVector.y + model.maxVector.y) / 2,
                    -(model.minVector.z + model.maxVector.z) / 2);
                Matrix translationMatrix = Matrix.Translation(new Vector3((-translation.X / 50.0f) / scale, (translation.Y / 50.0f) / scale, zoom / scale));
                device.Transform.World = centerMatrix * Matrix.RotationY(rotation.Y) * Matrix.RotationX(rotation.X) * translationMatrix * Matrix.Scaling(-scale, scale, scale);

                if (!useLegacyTexturing)
                {
                    fragmentShader.Begin(0);

                    #region "Shader Setup"
                    fragmentShader.SetValue("world", device.Transform.World);
                    fragmentShader.SetValue("view", device.Transform.View);
                    fragmentShader.SetValue("projection", device.Transform.Projection);

                    fragmentShader.SetValue("lights[0].pos", new Vector4(0.0f, -10.0f, -10.0f, 0.0f));
                    fragmentShader.SetValue("lights[0].ambient", new Vector4(0.1f, 0.1f, 0.1f, 1.0f));
                    fragmentShader.SetValue("lights[0].diffuse", new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                    fragmentShader.SetValue("lights[0].specular", new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                    fragmentShader.SetValue("numLights", 1);
                    #endregion
                }

                foreach (RenderBase.OModel mdl in model.model)
                {
                    foreach (RenderBase.OModelObject obj in mdl.modelObject)
                    {
                        RenderBase.OMaterial material = mdl.material[obj.materialId];

                        if (!useLegacyTexturing)
                        {
                            #region "Shader combiner parameters"
                            fragmentShader.SetValue("hasTextures", textures.Count > 0);
                            fragmentShader.SetValue("uvCount", obj.texUVCount);

                            fragmentShader.SetValue("isD0Enabled", material.fragmentShader.lighting.isDistribution0Enabled);
                            fragmentShader.SetValue("isD1Enabled", material.fragmentShader.lighting.isDistribution1Enabled);
                            fragmentShader.SetValue("isG0Enabled", material.fragmentShader.lighting.isGeometryFactor0Enabled);
                            fragmentShader.SetValue("isG1Enabled", material.fragmentShader.lighting.isGeometryFactor1Enabled);
                            fragmentShader.SetValue("isREnabled", material.fragmentShader.lighting.isReflectionEnabled);

                            fragmentShader.SetValue("bumpIndex", (int)material.fragmentShader.bump.texture);
                            fragmentShader.SetValue("bumpMode", (int)material.fragmentShader.bump.mode);

                            fragmentShader.SetValue("mEmissive", getColor(material.materialColor.emission));
                            fragmentShader.SetValue("mAmbient", getColor(material.materialColor.ambient));
                            fragmentShader.SetValue("mDiffuse", getColor(material.materialColor.diffuse));
                            fragmentShader.SetValue("mSpecular", getColor(material.materialColor.specular0));

                            for (int i = 0; i < 6; i++)
                            {
                                RenderBase.OTextureCombiner textureCombiner = material.fragmentShader.textureCombiner[i];

                                fragmentShader.SetValue(String.Format("combiners[{0}].colorCombine", i.ToString()), (int)textureCombiner.combineRgb);
                                fragmentShader.SetValue(String.Format("combiners[{0}].alphaCombine", i.ToString()), (int)textureCombiner.combineAlpha);

                                fragmentShader.SetValue(String.Format("combiners[{0}].colorScale", i.ToString()), (float)textureCombiner.rgbScale);
                                fragmentShader.SetValue(String.Format("combiners[{0}].alphaScale", i.ToString()), (float)textureCombiner.alphaScale);

                                for (int j = 0; j < 3; j++)
                                {
                                    fragmentShader.SetValue(String.Format("combiners[{0}].colorArg[{1}]", i.ToString(), j.ToString()), (int)textureCombiner.rgbSource[j]);
                                    fragmentShader.SetValue(String.Format("combiners[{0}].colorOp[{1}]", i.ToString(), j.ToString()), (int)textureCombiner.rgbOperand[j]);
                                    fragmentShader.SetValue(String.Format("combiners[{0}].alphaArg[{1}]", i.ToString(), j.ToString()), (int)textureCombiner.alphaSource[j]);
                                    fragmentShader.SetValue(String.Format("combiners[{0}].alphaOp[{1}]", i.ToString(), j.ToString()), (int)textureCombiner.alphaOperand[j]);
                                }

                                Color constantColor = Color.White;
                                switch (textureCombiner.constantColor)
                                {
                                    case RenderBase.OConstantColor.ambient: constantColor = material.materialColor.ambient; break;
                                    case RenderBase.OConstantColor.constant0: constantColor = material.materialColor.constant0; break;
                                    case RenderBase.OConstantColor.constant1: constantColor = material.materialColor.constant1; break;
                                    case RenderBase.OConstantColor.constant2: constantColor = material.materialColor.constant2; break;
                                    case RenderBase.OConstantColor.constant3: constantColor = material.materialColor.constant3; break;
                                    case RenderBase.OConstantColor.constant4: constantColor = material.materialColor.constant4; break;
                                    case RenderBase.OConstantColor.constant5: constantColor = material.materialColor.constant5; break;
                                    case RenderBase.OConstantColor.diffuse: constantColor = material.materialColor.diffuse; break;
                                    case RenderBase.OConstantColor.emission: constantColor = material.materialColor.emission; break;
                                    case RenderBase.OConstantColor.specular0: constantColor = material.materialColor.specular0; break;
                                    case RenderBase.OConstantColor.specular1: constantColor = material.materialColor.specular1; break;
                                }

                                fragmentShader.SetValue(String.Format("combiners[{0}].constant", i.ToString()), new Vector4(
                                    (float)constantColor.R / 0xff,
                                    (float)constantColor.G / 0xff,
                                    (float)constantColor.B / 0xff,
                                    (float)constantColor.A / 0xff));
                            }

                            foreach (CustomTexture texture in textures)
                            {
                                if (texture.name == material.name0) fragmentShader.SetValue("texture0", texture.texture);
                                else if (texture.name == material.name1) fragmentShader.SetValue("texture1", texture.texture);
                                else if (texture.name == material.name2) fragmentShader.SetValue("texture2", texture.texture);
                                else if (texture.name == material.name3) fragmentShader.SetValue("texture3", texture.texture);
                            }
                            #endregion
                        }
                        else
                        {
                            //Texture
                            foreach (CustomTexture texture in textures)
                            {
                                if (texture.name == material.name0) device.SetTexture(0, texture.texture);
                                else if (texture.name == material.name1) device.SetTexture(0, texture.texture);
                                else if (texture.name == material.name2) device.SetTexture(0, texture.texture);
                            }
                        }

                        //Filtering
                        for (int s = 0; s < 3; s++)
                        {
                            device.SetSamplerState(s, SamplerStageStates.MinFilter, (int)TextureFilter.Linear);
                            switch (material.textureMapper[s].magFilter)
                            {
                                case RenderBase.OTextureMagFilter.nearest: device.SetSamplerState(s, SamplerStageStates.MagFilter, (int)TextureFilter.None); break;
                                case RenderBase.OTextureMagFilter.linear: device.SetSamplerState(s, SamplerStageStates.MagFilter, (int)TextureFilter.Linear); break;
                            }

                            //Addressing
                            device.SetSamplerState(s, SamplerStageStates.BorderColor, material.textureMapper[s].borderColor.ToArgb());
                            switch (material.textureMapper[s].wrapU)
                            {
                                case RenderBase.OTextureWrap.repeat: device.SetSamplerState(s, SamplerStageStates.AddressU, (int)TextureAddress.Wrap); break;
                                case RenderBase.OTextureWrap.mirroredRepeat: device.SetSamplerState(s, SamplerStageStates.AddressU, (int)TextureAddress.Mirror); break;
                                case RenderBase.OTextureWrap.clampToEdge: device.SetSamplerState(s, SamplerStageStates.AddressU, (int)TextureAddress.Clamp); break;
                                case RenderBase.OTextureWrap.clampToBorder: device.SetSamplerState(s, SamplerStageStates.AddressU, (int)TextureAddress.Border); break;
                            }
                            switch (material.textureMapper[s].wrapV)
                            {
                                case RenderBase.OTextureWrap.repeat: device.SetSamplerState(s, SamplerStageStates.AddressV, (int)TextureAddress.Wrap); break;
                                case RenderBase.OTextureWrap.mirroredRepeat: device.SetSamplerState(s, SamplerStageStates.AddressV, (int)TextureAddress.Mirror); break;
                                case RenderBase.OTextureWrap.clampToEdge: device.SetSamplerState(s, SamplerStageStates.AddressV, (int)TextureAddress.Clamp); break;
                                case RenderBase.OTextureWrap.clampToBorder: device.SetSamplerState(s, SamplerStageStates.AddressV, (int)TextureAddress.Border); break;
                            }
                        }

                        //Culling
                        switch (material.rasterization.cullMode)
                        {
                            case RenderBase.OCullMode.backFace: device.RenderState.CullMode = Cull.Clockwise; break;
                            case RenderBase.OCullMode.frontFace: device.RenderState.CullMode = Cull.CounterClockwise; break;
                            case RenderBase.OCullMode.never: device.RenderState.CullMode = Cull.None; break;
                        }

                        //Alpha testing
                        RenderBase.OAlphaTest alpha = material.fragmentShader.alphaTest;
                        device.RenderState.AlphaTestEnable = alpha.isTestEnabled;
                        device.RenderState.AlphaFunction = getCompare(alpha.testFunction);
                        device.RenderState.ReferenceAlpha = (int)alpha.testReference;

                        //Depth testing
                        RenderBase.ODepthOperation depth = material.fragmentOperation.depth;
                        device.RenderState.ZBufferEnable = depth.isTestEnabled;
                        device.RenderState.ZBufferFunction = getCompare(depth.testFunction);
                        device.RenderState.ZBufferWriteEnable = depth.isMaskEnabled;

                        //Alpha blending
                        RenderBase.OBlendOperation blend = material.fragmentOperation.blend;
                        device.RenderState.AlphaBlendEnable = blend.blendMode == RenderBase.OBlendMode.blend;
                        device.RenderState.SeparateAlphaBlendEnabled = true;
                        device.RenderState.SourceBlend = getBlend(blend.rgbFunctionSource);
                        device.RenderState.DestinationBlend = getBlend(blend.rgbFunctionDestination);
                        device.RenderState.BlendOperation = getBlendOperation(blend.rgbBlendEquation);
                        device.RenderState.AlphaSourceBlend = getBlend(blend.alphaFunctionSource);
                        device.RenderState.AlphaDestinationBlend = getBlend(blend.alphaFunctionDestination);
                        device.RenderState.AlphaBlendOperation = getBlendOperation(blend.alphaBlendEquation);
                        device.RenderState.BlendFactorColor = blend.blendColor.ToArgb();

                        //Stencil testing
                        RenderBase.OStencilOperation stencil = material.fragmentOperation.stencil;
                        device.RenderState.StencilEnable = stencil.isTestEnabled;
                        device.RenderState.StencilFunction = getCompare(stencil.testFunction);
                        device.RenderState.ReferenceStencil = (int)stencil.testReference;
                        device.RenderState.StencilWriteMask = (int)stencil.testMask;
                        device.RenderState.StencilFail = getStencilOperation(stencil.failOperation);
                        device.RenderState.StencilZBufferFail = getStencilOperation(stencil.zFailOperation);
                        device.RenderState.StencilPass = getStencilOperation(stencil.passOperation);

                        //Vertex rendering
                        if (!useLegacyTexturing) fragmentShader.BeginPass(0);
                        if (obj.renderBuffer.Length > 0)
                        {
                            VertexFormats vertexFormat = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture3 | VertexFormats.Diffuse;
                            device.VertexFormat = vertexFormat;
                            VertexBuffer vertexBuffer = new VertexBuffer(typeof(RenderBase.CustomVertex), obj.renderBuffer.Length, device, Usage.None, vertexFormat, Pool.Managed);
                            if (animate)
                            {
                                vertexBuffer.SetData(obj.animatedRenderBuffer[frame], 0, LockFlags.None);
                            }
                            else
                            {
                                vertexBuffer.SetData(obj.renderBuffer, 0, LockFlags.None);
                            }
                            device.SetStreamSource(0, vertexBuffer, 0);

                            device.DrawPrimitives(PrimitiveType.TriangleList, 0, obj.renderBuffer.Length / 3);
                            vertexBuffer.Dispose();
                        }
                        if (!useLegacyTexturing) fragmentShader.EndPass();
                    }
                }
                if (!useLegacyTexturing) fragmentShader.End();

                device.EndScene();
                device.Present();

                if (animate) frame = (frame + 1) % (int)((model.skeletalAnimation[currentAnimation].frameSize / animationStep) + 1);

                Application.DoEvents();
            }
        }

        /// <summary>
        ///     Set X/Y angles to rotate the Mesh.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        public void setRotation(float y, float x)
        {
            rotation.X = wrap(rotation.X + x);
            rotation.Y = wrap(rotation.Y + y);
        }

        /// <summary>
        ///     Wrap a Rotation Angle between 0 and 2*PI.
        /// </summary>
        /// <param name="value">The angle in PI radians</param>
        /// <returns></returns>
        private float wrap(float value)
        {
            if (value < 0.0f) return (float)((Math.PI * 2) + value);
            else return (float)(value % (Math.PI * 2));
        }

        #region "Materials helper functions"
        private Compare getCompare(RenderBase.OTestFunction function)
        {
            switch (function)
            {
                case RenderBase.OTestFunction.always: return Compare.Always;
                case RenderBase.OTestFunction.equal: return Compare.Equal;
                case RenderBase.OTestFunction.greater: return Compare.Greater;
                case RenderBase.OTestFunction.greaterOrEqual: return Compare.GreaterEqual;
                case RenderBase.OTestFunction.less: return Compare.Less;
                case RenderBase.OTestFunction.lessOrEqual: return Compare.LessEqual;
                case RenderBase.OTestFunction.never: return Compare.Never;
                case RenderBase.OTestFunction.notEqual: return Compare.NotEqual;
            }

            return 0;
        }

        private Blend getBlend(RenderBase.OBlendFunction function)
        {
            switch (function)
            {
                case RenderBase.OBlendFunction.constantAlpha: return Blend.BothSourceAlpha;
                case RenderBase.OBlendFunction.constantColor: return Blend.BlendFactor;
                case RenderBase.OBlendFunction.destinationAlpha: return Blend.DestinationAlpha;
                case RenderBase.OBlendFunction.destinationColor: return Blend.DestinationColor;
                case RenderBase.OBlendFunction.one: return Blend.One;
                case RenderBase.OBlendFunction.oneMinusConstantAlpha: return Blend.BothInvSourceAlpha;
                case RenderBase.OBlendFunction.oneMinusConstantColor: return Blend.InvBlendFactor;
                case RenderBase.OBlendFunction.oneMinusDestinationAlpha: return Blend.InvDestinationAlpha;
                case RenderBase.OBlendFunction.oneMinusDestinationColor: return Blend.InvDestinationColor;
                case RenderBase.OBlendFunction.oneMinusSourceAlpha: return Blend.InvSourceAlpha;
                case RenderBase.OBlendFunction.oneMinusSourceColor: return Blend.InvSourceColor;
                case RenderBase.OBlendFunction.sourceAlpha: return Blend.SourceAlpha;
                case RenderBase.OBlendFunction.sourceAlphaSaturate: return Blend.SourceAlphaSat;
                case RenderBase.OBlendFunction.sourceColor: return Blend.SourceColor;
                case RenderBase.OBlendFunction.zero: return Blend.Zero;
            }

            return 0;
        }

        private BlendOperation getBlendOperation(RenderBase.OBlendEquation equation)
        {
            switch (equation)
            {
                case RenderBase.OBlendEquation.add: return BlendOperation.Add;
                case RenderBase.OBlendEquation.max: return BlendOperation.Max;
                case RenderBase.OBlendEquation.min: return BlendOperation.Min;
                case RenderBase.OBlendEquation.reverseSubtract: return BlendOperation.RevSubtract;
                case RenderBase.OBlendEquation.subtract: return BlendOperation.Subtract;
            }

            return 0;
        }

        private StencilOperation getStencilOperation(RenderBase.OStencilOp operation)
        {
            switch (operation)
            {
                case RenderBase.OStencilOp.decrease: return StencilOperation.Decrement;
                case RenderBase.OStencilOp.decreaseWrap: return StencilOperation.DecrementSaturation;
                case RenderBase.OStencilOp.increase: return StencilOperation.Increment;
                case RenderBase.OStencilOp.increaseWrap: return StencilOperation.IncrementSaturation;
                case RenderBase.OStencilOp.keep: return StencilOperation.Keep;
                case RenderBase.OStencilOp.replace: return StencilOperation.Replace;
                case RenderBase.OStencilOp.zero: return StencilOperation.Zero;
            }

            return 0;
        }

        private Vector4 getColor(Color input)
        {
            return new Vector4((float)input.R / 0xff, (float)input.G / 0xff, (float)input.B / 0xff, (float)input.A / 0xff);
        }
        #endregion

        #region "Animation"
        /// <summary>
        ///     Interpolates a point between two Key Frames on a given Frame using Linear Interpolation.
        /// </summary>
        /// <param name="keyFrames">The list with all available Key Frames (Linear format)</param>
        /// <param name="frame">The frame number that should be interpolated</param>
        /// <returns>The interpolated frame value</returns>
        private float interpolate(List<RenderBase.OLinearFloat> keyFrames, float frame)
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

            float mu = (minFrame != maxFrame) ? (frame - minFrame) / (maxFrame - minFrame) : 0;
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
        private float interpolate(List<RenderBase.OHermiteFloat> keyFrames, float frame)
        {
            //No idea if this is right
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

            float y1 = a.value;
            float y2 = b.value;

            float mu, mu2, mu3;
            float a0, a1, a2, a3;

            mu = (minFrame != maxFrame) ? (frame - minFrame) / (maxFrame - minFrame) : 0;
            mu2 = mu * mu;
            mu3 = mu2 * mu;
            a0 = 2 * mu3 - 3 * mu2 + 1;
            a1 = mu3 - 2 * mu2 + mu;
            a2 = mu3 - mu2;
            a3 = -2 * mu3 + 3 * mu2;

            return (a0 * y1 + a1 * a.inSlope + a2 * b.inSlope + a3 * y2);
        }

        private void preAnimate(int animationIndex)
        {
            currentAnimation = animationIndex;
            List<RenderBase.OSkeletalAnimationBone> bone = model.skeletalAnimation[animationIndex].bone;

            foreach (RenderBase.OModel mdl in model.model)
            {
                List<RenderBase.OBone> skeleton = mdl.skeleton;
                Matrix[] skeletonTransform = new Matrix[skeleton.Count];
                for (int index = 0; index < skeleton.Count; index++)
                {
                    skeletonTransform[index] = Matrix.Identity;
                    transformSkeleton(skeleton, index, ref skeletonTransform[index]);
                }

                List<Matrix[]> animationSkeleton = new List<Matrix[]>();
                for (float frame = 0; frame <= model.skeletalAnimation[animationIndex].frameSize; frame += animationStep)
                {
                    RenderBase.OBone[] frameAnimationSkeleton = new RenderBase.OBone[skeleton.Count];

                    for (int b2 = 0; b2 < skeleton.Count; b2++)
                    {
                        frameAnimationSkeleton[b2] = new RenderBase.OBone();
                        frameAnimationSkeleton[b2].parentId = skeleton[b2].parentId;
                        frameAnimationSkeleton[b2].rotation = new RenderBase.OVector3(skeleton[b2].rotation);
                        frameAnimationSkeleton[b2].translation = new RenderBase.OVector3(skeleton[b2].translation);
                        foreach (RenderBase.OSkeletalAnimationBone b in bone)
                        {
                            if (b.name == skeleton[b2].name)
                            {
                                if (frameCheck(b.rotationX, frame)) frameAnimationSkeleton[b2].rotation.x = getKey(b.rotationX, frame);
                                if (frameCheck(b.rotationY, frame)) frameAnimationSkeleton[b2].rotation.y = getKey(b.rotationY, frame);
                                if (frameCheck(b.rotationZ, frame)) frameAnimationSkeleton[b2].rotation.z = getKey(b.rotationZ, frame);
                                if (frameCheck(b.translationX, frame)) frameAnimationSkeleton[b2].translation.x = getKey(b.translationX, frame);
                                if (frameCheck(b.translationY, frame)) frameAnimationSkeleton[b2].translation.y = getKey(b.translationY, frame);
                                if (frameCheck(b.translationZ, frame)) frameAnimationSkeleton[b2].translation.z = getKey(b.translationZ, frame);
                            }
                        }
                    }

                    Matrix[] animationSkeletonTransform = new Matrix[frameAnimationSkeleton.Length];
                    for (int index = 0; index < frameAnimationSkeleton.Length; index++)
                    {
                        animationSkeletonTransform[index] = Matrix.Identity;
                        transformSkeleton(frameAnimationSkeleton.ToList(), index, ref animationSkeletonTransform[index]);
                        animationSkeletonTransform[index] = Matrix.Invert(skeletonTransform[index]) * animationSkeletonTransform[index];
                    }

                    animationSkeleton.Add(animationSkeletonTransform);
                }

                //
                //
                //

                foreach (RenderBase.OModelObject obj in mdl.modelObject)
                {
                    obj.animatedRenderBuffer.Clear();

                    for (int frame = 0; frame < animationSkeleton.Count; frame++)
                    {
                        RenderBase.CustomVertex[] buffer = new RenderBase.CustomVertex[obj.renderBuffer.Length];

                        for (int i = 0; i < buffer.Length; i++)
                        {
                            buffer[i] = obj.renderBuffer[i];
                            RenderBase.OVertex input = obj.obj[i];
                            Vector3 position = new Vector3(input.position.x, input.position.y, input.position.z);

                            Vector4 p = new Vector4();
                            
                            int weightIndex = 0;
                            foreach (int boneIndex in input.node)
                            {
                                float weight = 1.0f;
                                if (weightIndex < input.weight.Count) weight = input.weight[weightIndex++];
                                p += Vector3.Transform(position, animationSkeleton[frame][boneIndex]) * weight;
                            }
                            if (input.node.Count == 0) p = new Vector4(position.X, position.Y, position.Z, 0);

                            buffer[i].x = p.X;
                            buffer[i].y = p.Y;
                            buffer[i].z = p.Z;
                        }

                        obj.animatedRenderBuffer.Add(buffer);
                    }
                }
            }
        }

        /// <summary>
        ///     Interpolates a Key Frame from a list of Key Frames.
        /// </summary>
        /// <param name="sourceFrame">The list of key frames</param>
        /// <param name="frame">The frame that should be returned or interpolated from the list</param>
        /// <returns></returns>
        private float getKey(RenderBase.OSkeletalAnimationFrame sourceFrame, float frame)
        {
            switch (sourceFrame.interpolation)
            {
                case RenderBase.OInterpolationMode.linear: return interpolate(sourceFrame.linearFrame, frame);
                case RenderBase.OInterpolationMode.hermite: return interpolate(sourceFrame.hermiteFrame, frame);
                default: return 0; //Shouldn't happen
            }
        }

        /// <summary>
        ///     Check if a segment actually exists and if it is inside the frame window.
        /// </summary>
        /// <param name="sourceFrame">The list of key frames</param>
        /// <param name="frame">The frame that should be verified</param>
        /// <returns></returns>
        private bool frameCheck(RenderBase.OSkeletalAnimationFrame sourceFrame, float frame)
        {
            return (sourceFrame.exists && frame >= sourceFrame.startFrame && frame <= sourceFrame.endFrame);
        }

        /// <summary>
        ///     Transforms a Skeleton from relative to absolute positions.
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="index">Index of the bone to convert</param>
        /// <param name="target">Target matrix to save bone transformation</param>
        private void transformSkeleton(List<RenderBase.OBone> skeleton, int index, ref Matrix target)
        {
            target *= Matrix.RotationX(skeleton[index].rotation.x);
            target *= Matrix.RotationY(skeleton[index].rotation.y);
            target *= Matrix.RotationZ(skeleton[index].rotation.z);
            target *= Matrix.Translation(skeleton[index].translation.x, skeleton[index].translation.y, skeleton[index].translation.z);
            if (skeleton[index].parentId > -1) transformSkeleton(skeleton, skeleton[index].parentId, ref target);
        }

        /// <summary>
        ///     Load the animation at the given index on the model Skeletal Animation.
        /// </summary>
        /// <param name="animationIndex">The index where the animation is located</param>
        /// <param name="step">The step speed to load the animation. Smaller steps have very high RAM usage</param>
        public void loadAnimation(int animationIndex, float step = 0.5f)
        {
            animationStep = step;
            frame = 0;
            preAnimate(animationIndex);
        }

        /// <summary>
        ///     Changes the current animation frame.
        ///     It will have no effect if the frame is outside of the animation range.
        /// </summary>
        /// <param name="targetFrame">The new frame index</param>
        public void setAnimationFrame(int targetFrame)
        {
            if (targetFrame > (model.skeletalAnimation[currentAnimation].frameSize / animationStep)) return;
            frame = targetFrame;
        }

        /// <summary>
        ///     Pauses the animation.
        /// </summary>
        public void pauseAnimation()
        {
            animate = false;
        }

        /// <summary>
        ///     Stops the animation, going back to the beggining.
        /// </summary>
        public void stopAnimation()
        {
            animate = false;
            frame = 0;
        }

        /// <summary>
        ///     Play the animation.
        /// </summary>
        public void playAnimation()
        {
            animate = true;
        }

        /// <summary>
        ///     List with all loaded animations.
        /// </summary>
        public List<RenderBase.OSkeletalAnimation> animations
        {
            get { return model.skeletalAnimation; }
        }
        #endregion

    }
}
