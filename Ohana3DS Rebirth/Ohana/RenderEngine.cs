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

        struct combiner
        {
            public int colorCombine;
            public int alphaCombine;

            public int colorArg0, colorArg1, colorArg2;
            public int colorOp0, colorOp1, colorOp2;
            public int alphaArg0, alphaArg1, alphaArg2;
            public int alphaOp0, alphaOp1, alphaOp2;

            public float constantR, constantG, constantB, constantA;
        };

        struct light
        {
            public float posX, posY, posZ, posW;
            public float aR, aG, aB, aA;
            public float dR, dG, dB, dA;
            public float sR, sG, sB, sA;
        };

        private bool useLegacyTexturing = false; //Set to True to disable Fragment Shader

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
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, (float)pParams.BackBufferWidth / pParams.BackBufferHeight, 0.1f, 500.0f);
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
            }

            keepRendering = true;
            while (keepRendering)
            {
                blit();
                Application.DoEvents();
            }
        }

        private void blit()
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

                light light0;
                light0.posX = 0.0f;
                light0.posY = -10.0f;
                light0.posZ = -10.0f;
                light0.posW = 0.0f;

                light0.aR = 0.1f;
                light0.aG = 0.1f;
                light0.aB = 0.1f;
                light0.aA = 1.0f;

                light0.dR = 1.0f;
                light0.dG = 1.0f;
                light0.dB = 1.0f;
                light0.dA = 1.0f;

                light0.sR = 1.0f;
                light0.sG = 1.0f;
                light0.sB = 1.0f;
                light0.sA = 1.0f;

                unsafe
                {
                    fixed (void* pointer = new byte[Marshal.SizeOf(light0)])
                    {
                        var managedPointer = new IntPtr(pointer);
                        Marshal.StructureToPtr(light0, managedPointer, true);

                        fragmentShader.SetValue("lights[0]", pointer, Marshal.SizeOf(light0));
                    }
                }
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

                        fragmentShader.SetValue("bumpIndex", (int)material.fragmentShader.bump.bumpTexture);
                        fragmentShader.SetValue("bumpMode", (int)material.fragmentShader.bump.bumpMode);

                        fragmentShader.SetValue("mEmissive", getColor(material.materialColor.emission));
                        fragmentShader.SetValue("mAmbient", getColor(material.materialColor.ambient));
                        fragmentShader.SetValue("mDiffuse", getColor(material.materialColor.diffuse));
                        fragmentShader.SetValue("mSpecular", getColor(material.materialColor.specular0));

                        for (int i = 0; i < 6; i++)
                        {
                            RenderBase.OTextureCombiner textureCombiner = material.fragmentShader.textureCombiner[i];
                            combiner shaderCombiner;

                            shaderCombiner.colorCombine = (int)textureCombiner.combineRgb;
                            shaderCombiner.alphaCombine = (int)textureCombiner.combineAlpha;

                            shaderCombiner.colorArg0 = (int)textureCombiner.rgbSource[0];
                            shaderCombiner.colorArg1 = (int)textureCombiner.rgbSource[1];
                            shaderCombiner.colorArg2 = (int)textureCombiner.rgbSource[2];

                            shaderCombiner.alphaArg0 = (int)textureCombiner.alphaSource[0];
                            shaderCombiner.alphaArg1 = (int)textureCombiner.alphaSource[1];
                            shaderCombiner.alphaArg2 = (int)textureCombiner.alphaSource[2];

                            shaderCombiner.colorOp0 = (int)textureCombiner.rgbOperand[0];
                            shaderCombiner.colorOp1 = (int)textureCombiner.rgbOperand[1];
                            shaderCombiner.colorOp2 = (int)textureCombiner.rgbOperand[2];

                            shaderCombiner.alphaOp0 = (int)textureCombiner.alphaOperand[0];
                            shaderCombiner.alphaOp1 = (int)textureCombiner.alphaOperand[1];
                            shaderCombiner.alphaOp2 = (int)textureCombiner.alphaOperand[2];

                            System.Drawing.Color constantColor = new System.Drawing.Color();
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

                            shaderCombiner.constantR = (float)constantColor.R / 0xff;
                            shaderCombiner.constantG = (float)constantColor.G / 0xff;
                            shaderCombiner.constantB = (float)constantColor.B / 0xff;
                            shaderCombiner.constantA = (float)constantColor.A / 0xff;

                            unsafe
                            {
                                fixed (void* pointer = new byte[Marshal.SizeOf(shaderCombiner)])
                                {
                                    var managedPointer = new IntPtr(pointer);
                                    Marshal.StructureToPtr(shaderCombiner, managedPointer, true);

                                    fragmentShader.SetValue(String.Format("combiners[{0}]", i.ToString()), pointer, Marshal.SizeOf(shaderCombiner));
                                }
                            }
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
                        vertexBuffer.SetData(obj.renderBuffer, 0, LockFlags.None);
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

    }
}
