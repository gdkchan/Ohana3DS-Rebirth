//Ohana3DS 3D Rendering Engine by gdkchan
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Ohana3DS_Rebirth.Ohana
{
    public class RenderEngine
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
        private const bool showGrid = true;


        private struct animationControl
        {
            public float animationStep;
            public int currentAnimation;
            public float frame;
            public bool animate;
            public bool paused;
        }
        animationControl ctrlSA, ctrlMA;

        private struct animationCacheEntry
        {
            public float frame;
            public int modelIndex, objectIndex;
            public RenderBase.CustomVertex[] buffer;
        }
        List<animationCacheEntry> animationCache = new List<animationCacheEntry>();

        /// <summary>
        ///     Initialize the renderer at a given target.
        /// </summary>
        /// <param name="handle">Memory pointer to the control rendering buffer</param>
        /// <param name="width">Render width</param>
        /// <param name="height">Render height</param>
        public void initialize(IntPtr handle, int width, int height)
        {
            pParams = new PresentParameters
            {
                BackBufferCount = 1,
                BackBufferFormat = Manager.Adapters[0].CurrentDisplayMode.Format,
                BackBufferWidth = width,
                BackBufferHeight = height,
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = DepthFormat.D24S8
            };

            try
            {
                device = new Device(0, DeviceType.Hardware, handle, CreateFlags.HardwareVertexProcessing, pParams);
            }
            catch
            {
                //Some crap GPUs only works with Software vertex processing
                device = new Device(0, DeviceType.Hardware, handle, CreateFlags.SoftwareVertexProcessing, pParams);
            }

            //Spare parts
            ctrlSA.currentAnimation = -1;
            ctrlMA.currentAnimation = -1;
            ctrlSA.animationStep = 0.25f;
            ctrlMA.animationStep = 0.25f;

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
        ///     Releases all resources used by DirectX.
        /// </summary>
        public void dispose()
        {
            keepRendering = false;
            
            foreach (CustomTexture texture in textures) texture.texture.Dispose();
            foreach (RenderBase.OTexture texture in model.texture) texture.texture.Dispose();
            textures.Clear();
            if (!useLegacyTexturing) fragmentShader.Dispose();
            device.Dispose();
            model.model.Clear();
            model.texture.Clear();
            model.lookUpTable.Clear();
            model.light.Clear();
            model.camera.Clear();
            model.fog.Clear();
            model.skeletalAnimation.Clear();
            animationCache.Clear();
            model = null;
        }

        /// <summary>
        ///     Forces all textures to be updated.
        ///     Call this if you add new textures to reflect changes.
        /// </summary>
        public void updateTextures()
        {
            textures.Clear();
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
        }

        public void render()
        {
            if (!useLegacyTexturing)
            {
                string compilationErros;
                fragmentShader = Effect.FromString(device, Properties.Resources.OFragmentShader, null, null, ShaderFlags.SkipOptimization, null, out compilationErros);
                if (compilationErros != "") MessageBox.Show("Failed to compile Fragment Shader!" + Environment.NewLine + compilationErros, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fragmentShader.Technique = "Combiner";
            }

            updateTextures();

            float minSize = Math.Min(Math.Min(model.minVector.x, model.minVector.y), model.minVector.z);
            float maxSize = Math.Max(Math.Max(model.maxVector.x, model.maxVector.y), model.maxVector.z);
            float scale = (10.0f / (maxSize - minSize)); //Try to adjust to screen
            if (maxSize - minSize == 0) scale = 1;

            #region "Grid buffer creation"
            //Creates buffer with grid that appears below the model
            CustomVertex.PositionColored[] gridBuffer = new CustomVertex.PositionColored[204];
            int bufferIndex = 0;
            for (float i = -50.0f; i <= 50.0f; i += 2.0f)
            {
                if (i == 0) continue;
                gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(-50.0f / scale, 0, i / scale, Color.White.ToArgb());
                gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(50.0f / scale, 0, i / scale, Color.White.ToArgb());
                gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(i / scale, 0, -50.0f / scale, Color.White.ToArgb());
                gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(i / scale, 0, 50.0f / scale, Color.White.ToArgb());
            }
            gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(0, 0, -50.0f / scale, Color.FromArgb(0xff, 0x7f, 0x7f).ToArgb());
            gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(0, 0, 50.0f / scale, Color.FromArgb(0xff, 0x7f, 0x7f).ToArgb());
            gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(-50.0f / scale, 0, 0, Color.FromArgb(0x7f, 0xff, 0x7f).ToArgb());
            gridBuffer[bufferIndex++] = new CustomVertex.PositionColored(50.0f / scale, 0, 0, Color.FromArgb(0x7f, 0xff, 0x7f).ToArgb());
            #endregion

            keepRendering = true;
            while (keepRendering)
            {
                device.Clear(ClearFlags.Stencil | ClearFlags.Target | ClearFlags.ZBuffer, 0x5f5f5f, 1.0f, 0);
                device.SetTexture(0, null);
                device.BeginScene();

                //View
                Matrix centerMatrix = Matrix.Translation(
                    -(model.minVector.x + model.maxVector.x) / 2,
                    -(model.minVector.y + model.maxVector.y) / 2,
                    -(model.minVector.z + model.maxVector.z) / 2);
                Matrix translationMatrix = Matrix.Translation(new Vector3((-translation.X / 50.0f) / scale, (translation.Y / 50.0f) / scale, zoom / scale));
                Matrix baseTransform = Matrix.Identity;
                baseTransform *= Matrix.RotationY(rotation.Y) * Matrix.RotationX(rotation.X);
                baseTransform *= centerMatrix * translationMatrix * Matrix.Scaling(-scale, scale, scale);

                //Grid
                #region "Grid Rendering"
                device.RenderState.AlphaTestEnable = false;
                device.RenderState.ZBufferEnable = true;
                device.RenderState.ZBufferWriteEnable = true;
                device.RenderState.AlphaBlendEnable = false;
                device.RenderState.StencilEnable = false;
                device.RenderState.CullMode = Cull.None;
                if (showGrid)
                {
                    device.Transform.World = baseTransform;
                    device.VertexFormat = CustomVertex.PositionColored.Format;
                    VertexBuffer lineBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), gridBuffer.Length, device, Usage.None, CustomVertex.PositionColored.Format, Pool.Managed);
                    lineBuffer.SetData(gridBuffer, 0, LockFlags.None);
                    device.SetStreamSource(0, lineBuffer, 0);
                    device.DrawPrimitives(PrimitiveType.LineList, 0, gridBuffer.Length / 2);
                    lineBuffer.Dispose();
                }
                #endregion

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

                int modelIndex = 0;
                foreach (RenderBase.OModel mdl in model.model)
                {
                    device.Transform.World = getMatrix(mdl.transform) * baseTransform;

                    #region "Animation"
                    bool foundModelEntry = false;
                    foreach (animationCacheEntry cacheEntry in animationCache)
                    {
                        if (cacheEntry.modelIndex == modelIndex && cacheEntry.frame == ctrlSA.frame)
                        {
                            foundModelEntry = true;
                            break;
                        }
                    }

                    Matrix[] animationSkeletonTransform = new Matrix[mdl.skeleton.Count];
                    if (ctrlSA.animate && !foundModelEntry && ctrlSA.currentAnimation > -1)
                    {
                        Matrix[] skeletonTransform = new Matrix[mdl.skeleton.Count];

                        for (int index = 0; index < mdl.skeleton.Count; index++)
                        {
                            skeletonTransform[index] = Matrix.Identity;
                            transformSkeleton(mdl.skeleton, index, ref skeletonTransform[index]);
                        }

                        List<RenderBase.OSkeletalAnimationBone> bone = model.skeletalAnimation[ctrlSA.currentAnimation].bone;
                        List<RenderBase.OBone> frameAnimationSkeleton = new List<RenderBase.OBone>();
                        for (int index = 0; index < mdl.skeleton.Count; index++)
                        {
                            RenderBase.OBone newBone = new RenderBase.OBone();
                            newBone.parentId = mdl.skeleton[index].parentId;
                            newBone.rotation = new RenderBase.OVector3(mdl.skeleton[index].rotation);
                            newBone.translation = new RenderBase.OVector3(mdl.skeleton[index].translation);
                            foreach (RenderBase.OSkeletalAnimationBone b in bone)
                            {
                                if (b.name == mdl.skeleton[index].name)
                                {
                                    if (b.rotationX.exists) newBone.rotation.x = getKey(b.rotationX, getFrameNumber(b.rotationX, ctrlSA.frame));
                                    if (b.rotationY.exists) newBone.rotation.y = getKey(b.rotationY, getFrameNumber(b.rotationY, ctrlSA.frame));
                                    if (b.rotationZ.exists) newBone.rotation.z = getKey(b.rotationZ, getFrameNumber(b.rotationZ, ctrlSA.frame));
                                    if (b.translationX.exists) newBone.translation.x = getKey(b.translationX, getFrameNumber(b.translationX, ctrlSA.frame));
                                    if (b.translationY.exists) newBone.translation.y = getKey(b.translationY, getFrameNumber(b.translationY, ctrlSA.frame));
                                    if (b.translationZ.exists) newBone.translation.z = getKey(b.translationZ, getFrameNumber(b.translationZ, ctrlSA.frame));

                                    break;
                                }
                            }

                            frameAnimationSkeleton.Add(newBone);
                        }

                        for (int index = 0; index < mdl.skeleton.Count; index++)
                        {
                            animationSkeletonTransform[index] = Matrix.Identity;
                            transformSkeleton(frameAnimationSkeleton, index, ref animationSkeletonTransform[index]);
                            animationSkeletonTransform[index] = Matrix.Invert(skeletonTransform[index]) * animationSkeletonTransform[index];
                        }
                    }
                    #endregion

                    int objectIndex = 0;
                    foreach (RenderBase.OModelObject obj in mdl.modelObject)
                    {
                        RenderBase.OMaterial material = mdl.material[obj.materialId];

                        #region "Material Animation"
                        int[] textureId = {-1, -1, -1};
                        Color blendColor = material.fragmentOperation.blend.blendColor;
                        Color[] borderColor = new Color[3];
                        borderColor[0] = material.textureMapper[0].borderColor;
                        borderColor[1] = material.textureMapper[1].borderColor;
                        borderColor[2] = material.textureMapper[2].borderColor;
                        if (ctrlMA.animate && ctrlMA.currentAnimation > -1)
                        {
                            foreach (RenderBase.OMaterialAnimationData data in model.materialAnimation[ctrlMA.currentAnimation].data)
                            {
                                if (data.name == material.name)
                                {
                                    switch (data.type)
                                    {
                                        case RenderBase.OMaterialAnimationType.textureMapper0: textureId[0] = (int)getMaterialAnimationFloat(data); break;
                                        case RenderBase.OMaterialAnimationType.textureMapper1: textureId[1] = (int)getMaterialAnimationFloat(data); break;
                                        case RenderBase.OMaterialAnimationType.textureMapper2: textureId[2] = (int)getMaterialAnimationFloat(data); break;
                                        case RenderBase.OMaterialAnimationType.borderColorMapper0: borderColor[0] = getMaterialAnimationColor(data); break;
                                        case RenderBase.OMaterialAnimationType.borderColorMapper1: borderColor[1] = getMaterialAnimationColor(data); break;
                                        case RenderBase.OMaterialAnimationType.borderColorMapper2: borderColor[2] = getMaterialAnimationColor(data); break;
                                        case RenderBase.OMaterialAnimationType.blendColor: blendColor = getMaterialAnimationColor(data); break;
                                    }
                                }
                            }
                        }
                        #endregion

                        if (!useLegacyTexturing)
                        {
                            #region "Shader combiner parameters"
                            RenderBase.OMaterialColor materialColor = new RenderBase.OMaterialColor();
                            materialColor = material.materialColor;

                            if (ctrlMA.animate && ctrlMA.currentAnimation > -1)
                            {
                                foreach (RenderBase.OMaterialAnimationData data in model.materialAnimation[ctrlMA.currentAnimation].data)
                                {
                                    if (data.name == material.name)
                                    {
                                        switch (data.type)
                                        {
                                            case RenderBase.OMaterialAnimationType.constant0: materialColor.constant0 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.constant1: materialColor.constant1 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.constant2: materialColor.constant2 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.constant3: materialColor.constant3 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.constant4: materialColor.constant4 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.constant5: materialColor.constant5 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.diffuse: materialColor.diffuse = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.specular0: materialColor.specular0 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.specular1: materialColor.specular1 = getMaterialAnimationColor(data); break;
                                            case RenderBase.OMaterialAnimationType.ambient: materialColor.ambient = getMaterialAnimationColor(data); break;
                                        }
                                    }
                                }
                            }

                            fragmentShader.SetValue("hasTextures", textures.Count > 0);
                            fragmentShader.SetValue("uvCount", obj.texUVCount);

                            fragmentShader.SetValue("isD0Enabled", material.fragmentShader.lighting.isDistribution0Enabled);
                            fragmentShader.SetValue("isD1Enabled", material.fragmentShader.lighting.isDistribution1Enabled);
                            fragmentShader.SetValue("isG0Enabled", material.fragmentShader.lighting.isGeometryFactor0Enabled);
                            fragmentShader.SetValue("isG1Enabled", material.fragmentShader.lighting.isGeometryFactor1Enabled);
                            fragmentShader.SetValue("isREnabled", material.fragmentShader.lighting.isReflectionEnabled);

                            fragmentShader.SetValue("bumpIndex", (int)material.fragmentShader.bump.texture);
                            fragmentShader.SetValue("bumpMode", (int)material.fragmentShader.bump.mode);

                            fragmentShader.SetValue("mEmissive", getColor(materialColor.emission));
                            fragmentShader.SetValue("mAmbient", getColor(materialColor.ambient));
                            fragmentShader.SetValue("mDiffuse", getColor(materialColor.diffuse));
                            fragmentShader.SetValue("mSpecular", getColor(materialColor.specular0));

                            for (int i = 0; i < 6; i++)
                            {
                                RenderBase.OTextureCombiner textureCombiner = material.fragmentShader.textureCombiner[i];

                                fragmentShader.SetValue(String.Format("combiners[{0}].colorCombine", i), (int)textureCombiner.combineRgb);
                                fragmentShader.SetValue(String.Format("combiners[{0}].alphaCombine", i), (int)textureCombiner.combineAlpha);

                                fragmentShader.SetValue(String.Format("combiners[{0}].colorScale", i), (float)textureCombiner.rgbScale);
                                fragmentShader.SetValue(String.Format("combiners[{0}].alphaScale", i), (float)textureCombiner.alphaScale);

                                for (int j = 0; j < 3; j++)
                                {
                                    fragmentShader.SetValue(String.Format("combiners[{0}].colorArg[{1}]", i, j), (int)textureCombiner.rgbSource[j]);
                                    fragmentShader.SetValue(String.Format("combiners[{0}].colorOp[{1}]", i, j), (int)textureCombiner.rgbOperand[j]);
                                    fragmentShader.SetValue(String.Format("combiners[{0}].alphaArg[{1}]", i, j), (int)textureCombiner.alphaSource[j]);
                                    fragmentShader.SetValue(String.Format("combiners[{0}].alphaOp[{1}]", i, j), (int)textureCombiner.alphaOperand[j]);
                                }

                                Color constantColor = Color.White;
                                switch (textureCombiner.constantColor)
                                {
                                    case RenderBase.OConstantColor.ambient: constantColor = materialColor.ambient; break;
                                    case RenderBase.OConstantColor.constant0: constantColor = materialColor.constant0; break;
                                    case RenderBase.OConstantColor.constant1: constantColor = materialColor.constant1; break;
                                    case RenderBase.OConstantColor.constant2: constantColor = materialColor.constant2; break;
                                    case RenderBase.OConstantColor.constant3: constantColor = materialColor.constant3; break;
                                    case RenderBase.OConstantColor.constant4: constantColor = materialColor.constant4; break;
                                    case RenderBase.OConstantColor.constant5: constantColor = materialColor.constant5; break;
                                    case RenderBase.OConstantColor.diffuse: constantColor = materialColor.diffuse; break;
                                    case RenderBase.OConstantColor.emission: constantColor = materialColor.emission; break;
                                    case RenderBase.OConstantColor.specular0: constantColor = materialColor.specular0; break;
                                    case RenderBase.OConstantColor.specular1: constantColor = materialColor.specular1; break;
                                }

                                fragmentShader.SetValue(String.Format("combiners[{0}].constant", i), new Vector4(
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
                            }

                            #region "Material Animation"
                            if (textureId[0] > -1) fragmentShader.SetValue("texture0", textures[textureId[0]].texture);
                            if (textureId[1] > -1) fragmentShader.SetValue("texture1", textures[textureId[1]].texture);
                            if (textureId[2] > -1) fragmentShader.SetValue("texture2", textures[textureId[2]].texture);
                            #endregion

                            #endregion
                        }
                        else
                        {
                            device.SetTexture(0, null);

                            //Texture
                            foreach (CustomTexture texture in textures)
                            {
                                if (texture.name == material.name0) device.SetTexture(0, texture.texture);
                                else if (texture.name == material.name1) device.SetTexture(0, texture.texture);
                                else if (texture.name == material.name2) device.SetTexture(0, texture.texture);
                            }
                        }

                        #region "Texture Filtering/Addressing Setup"
                        //Filtering
                        for (int s = 0; s < 3; s++)
                        {
                            if (!useLegacyTexturing)
                            {
                                RenderBase.OTextureCoordinator coordinator = material.textureCoordinator[s];

                                Vector2 translate = new Vector2(coordinator.translateU, coordinator.translateV);
                                Vector2 scaling = new Vector2(coordinator.scaleU, coordinator.scaleV);
                                if (scaling == Vector2.Empty) scaling = new Vector2(1, 1);
                                float rotate = coordinator.rotate;
                                #region "Material Animation"
                                if (ctrlMA.animate && ctrlMA.currentAnimation > -1)
                                {
                                    foreach (RenderBase.OMaterialAnimationData data in model.materialAnimation[ctrlMA.currentAnimation].data)
                                    {
                                        if (data.name == material.name)
                                        {
                                            switch (data.type)
                                            {
                                                case RenderBase.OMaterialAnimationType.translateCoordinator0: if (s == 0) translate = getMaterialAnimationVector2(data); break; //Translation
                                                case RenderBase.OMaterialAnimationType.translateCoordinator1: if (s == 1) translate = getMaterialAnimationVector2(data); break;
                                                case RenderBase.OMaterialAnimationType.translateCoordinator2: if (s == 2) translate = getMaterialAnimationVector2(data); break;
                                                case RenderBase.OMaterialAnimationType.scaleCoordinator0: if (s == 0) scaling = getMaterialAnimationVector2(data); break; //Scaling
                                                case RenderBase.OMaterialAnimationType.scaleCoordinator1: if (s == 1) scaling = getMaterialAnimationVector2(data); break;
                                                case RenderBase.OMaterialAnimationType.scaleCoordinator2: if (s == 2) scaling = getMaterialAnimationVector2(data); break;
                                                case RenderBase.OMaterialAnimationType.rotateCoordinator0: if (s == 0) rotate = getMaterialAnimationFloat(data); break; //Rotation
                                                case RenderBase.OMaterialAnimationType.rotateCoordinator1: if (s == 1) rotate = getMaterialAnimationFloat(data); break;
                                                case RenderBase.OMaterialAnimationType.rotateCoordinator2: if (s == 2) rotate = getMaterialAnimationFloat(data); break;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                fragmentShader.SetValue(String.Format("uvTranslate[{0}]", s), new Vector4(translate.X, translate.Y, 0, 0));
                                fragmentShader.SetValue(String.Format("uvScale[{0}]", s), new Vector4(scaling.X, scaling.Y, 0, 0));
                                fragmentShader.SetValue(String.Format("uvTransform[{0}]", s), Matrix.RotationZ(rotate));
                            }

                            device.SetSamplerState(s, SamplerStageStates.MinFilter, (int)TextureFilter.Linear);
                            switch (material.textureMapper[s].magFilter)
                            {
                                case RenderBase.OTextureMagFilter.nearest: device.SetSamplerState(s, SamplerStageStates.MagFilter, (int)TextureFilter.None); break;
                                case RenderBase.OTextureMagFilter.linear: device.SetSamplerState(s, SamplerStageStates.MagFilter, (int)TextureFilter.Linear); break;
                            }

                            //Addressing
                            device.SetSamplerState(s, SamplerStageStates.BorderColor, borderColor[s].ToArgb());
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
                        #endregion

                        #region "Culling/Alpha/Depth/Stencil testing/blending stuff Setup"
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
                        device.RenderState.AlphaBlendEnable = blend.mode == RenderBase.OBlendMode.blend;
                        device.RenderState.SeparateAlphaBlendEnabled = true;
                        device.RenderState.SourceBlend = getBlend(blend.rgbFunctionSource);
                        device.RenderState.DestinationBlend = getBlend(blend.rgbFunctionDestination);
                        device.RenderState.BlendOperation = getBlendOperation(blend.rgbBlendEquation);
                        device.RenderState.AlphaSourceBlend = getBlend(blend.alphaFunctionSource);
                        device.RenderState.AlphaDestinationBlend = getBlend(blend.alphaFunctionDestination);
                        device.RenderState.AlphaBlendOperation = getBlendOperation(blend.alphaBlendEquation);
                        device.RenderState.BlendFactorColor = blendColor.ToArgb();

                        //Stencil testing
                        RenderBase.OStencilOperation stencil = material.fragmentOperation.stencil;
                        device.RenderState.StencilEnable = stencil.isTestEnabled;
                        device.RenderState.StencilFunction = getCompare(stencil.testFunction);
                        device.RenderState.ReferenceStencil = (int)stencil.testReference;
                        device.RenderState.StencilWriteMask = (int)stencil.testMask;
                        device.RenderState.StencilFail = getStencilOperation(stencil.failOperation);
                        device.RenderState.StencilZBufferFail = getStencilOperation(stencil.zFailOperation);
                        device.RenderState.StencilPass = getStencilOperation(stencil.passOperation);
                        #endregion

                        #region "Rendering"
                        //Vertex rendering
                        VertexFormats vertexFormat = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture3 | VertexFormats.Diffuse;
                        device.VertexFormat = vertexFormat;
                        VertexBuffer vertexBuffer;

                        if (!useLegacyTexturing) fragmentShader.BeginPass(0);
                        if (ctrlSA.animate && ctrlSA.currentAnimation > -1)
                        {
                            animationCacheEntry entry = new animationCacheEntry();

                            #region "Animation smart caching"
                            bool foundEntry = false;
                            if (foundModelEntry)
                            {
                                foreach (animationCacheEntry cacheEntry in animationCache)
                                {
                                    if (cacheEntry.modelIndex == modelIndex && cacheEntry.objectIndex == objectIndex && cacheEntry.frame == ctrlSA.frame)
                                    {
                                        foundEntry = true;
                                        entry = cacheEntry;
                                        break;
                                    }
                                }
                            }

                            if (!foundEntry)
                            {
                                entry.frame = ctrlSA.frame;
                                entry.modelIndex = modelIndex;
                                entry.objectIndex = objectIndex;
                                entry.buffer = new RenderBase.CustomVertex[obj.renderBuffer.Length];

                                for (int vertex = 0; vertex < obj.renderBuffer.Length; vertex++)
                                {
                                    entry.buffer[vertex] = obj.renderBuffer[vertex];
                                    RenderBase.OVertex input = obj.obj[vertex];
                                    Vector3 position = new Vector3(input.position.x, input.position.y, input.position.z);
                                    Vector4 p = new Vector4();

                                    int weightIndex = 0;
                                    foreach (int boneIndex in input.node)
                                    {
                                        float weight = 1;
                                        if (weightIndex < input.weight.Count) weight = input.weight[weightIndex++];
                                        p += Vector3.Transform(position, animationSkeletonTransform[boneIndex]) * weight;
                                    }

                                    entry.buffer[vertex].x = p.X;
                                    entry.buffer[vertex].y = p.Y;
                                    entry.buffer[vertex].z = p.Z;
                                }

                                animationCache.Add(entry);
                            }
                            #endregion

                            vertexBuffer = new VertexBuffer(typeof(RenderBase.CustomVertex), entry.buffer.Length, device, Usage.None, vertexFormat, Pool.Managed);
                            vertexBuffer.SetData(entry.buffer, 0, LockFlags.None);
                            device.SetStreamSource(0, vertexBuffer, 0);

                            device.DrawPrimitives(PrimitiveType.TriangleList, 0, entry.buffer.Length / 3);
                        }
                        else
                        {
                            vertexBuffer = new VertexBuffer(typeof(RenderBase.CustomVertex), obj.renderBuffer.Length, device, Usage.None, vertexFormat, Pool.Managed);
                            vertexBuffer.SetData(obj.renderBuffer, 0, LockFlags.None);
                            device.SetStreamSource(0, vertexBuffer, 0);

                            device.DrawPrimitives(PrimitiveType.TriangleList, 0, obj.renderBuffer.Length / 3);
                        }

                        vertexBuffer.Dispose();
                        if (!useLegacyTexturing) fragmentShader.EndPass();
                        #endregion

                        objectIndex++;
                    }

                    modelIndex++;
                }
                if (!useLegacyTexturing) fragmentShader.End();

                device.EndScene();
                device.Present();

                if (!ctrlSA.paused && ctrlSA.animate && ctrlSA.currentAnimation > -1) ctrlSA.frame = (ctrlSA.frame + ctrlSA.animationStep) % (int)model.skeletalAnimation[ctrlSA.currentAnimation].frameSize;
                if (!ctrlMA.paused && ctrlMA.animate && ctrlMA.currentAnimation > -1) ctrlMA.frame = (ctrlMA.frame + ctrlMA.animationStep) % (int)model.materialAnimation[ctrlMA.currentAnimation].frameSize;

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
            return (float)(value % (Math.PI * 2));
        }

        /// <summary>
        ///     Gets a MDX Matrix from a RenderBase Matrix.
        /// </summary>
        /// <param name="mtx">RenderBase matrix</param>
        /// <returns></returns>
        private Matrix getMatrix(RenderBase.OMatrix mtx)
        {
            Matrix output = Matrix.Identity;

            output.M11 = mtx.M11;
            output.M12 = mtx.M12;
            output.M13 = mtx.M13;
            output.M14 = mtx.M14;

            output.M21 = mtx.M21;
            output.M22 = mtx.M22;
            output.M23 = mtx.M23;
            output.M24 = mtx.M24;

            output.M31 = mtx.M31;
            output.M32 = mtx.M32;
            output.M33 = mtx.M33;
            output.M34 = mtx.M34;

            return output;
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
        private float interpolate(List<RenderBase.OHermiteFloat> keyFrames, float frame)
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
        private float getKey(RenderBase.OAnimationFrame sourceFrame, float frame)
        {
            switch (sourceFrame.interpolation)
            {
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
        private float getFrameNumber(RenderBase.OAnimationFrame sourceFrame, float frame)
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
        ///     It will have no effect if the index is invalid.
        /// </summary>
        /// <param name="animationIndex">The index where the animation is located</param>
        public bool loadSkeletalAnimation(int animationIndex)
        {
            if (animationIndex < -1 || animationIndex >= model.skeletalAnimation.Count) return false;
            animationCache.Clear();
            ctrlSA.currentAnimation = animationIndex;
            ctrlSA.frame = 0;
            return true;
        }

        /// <summary>
        ///     Changes the current animation frame.
        ///     It will have no effect if the frame is outside of the animation range.
        /// </summary>
        /// <param name="targetFrame">The new frame index</param>
        public void setSkeletalAnimationFrame(int targetFrame)
        {
            if (targetFrame < 0 || targetFrame > model.skeletalAnimation[ctrlSA.currentAnimation].frameSize) return;
            ctrlSA.frame = targetFrame;
        }

        /// <summary>
        ///     Pauses the animation.
        /// </summary>
        public void pauseSkeletalAnimation()
        {
            ctrlSA.paused = true;
        }

        /// <summary>
        ///     Stops the animation, and render the still model.
        /// </summary>
        public void stopSkeletalAnimation()
        {
            ctrlSA.animate = false;
            ctrlSA.frame = 0;
        }

        /// <summary>
        ///     Play the animation.
        ///     It will have no effect if no animations are loaded.
        /// </summary>
        public void playSkeletalAnimation()
        {
            if (ctrlSA.currentAnimation == -1) return;
            ctrlSA.paused = false;
            ctrlSA.animate = true;
        }

        /// <summary>
        ///     Get the current animation index.
        ///     It will return -1 if no animations have been loaded yet.
        /// </summary>
        public int currentSkeletalAnimationIndex
        {
            get { return ctrlSA.currentAnimation; }
        }

        //
        //
        //

        private Color getMaterialAnimationColor(RenderBase.OMaterialAnimationData data)
        {
            float r = getKey(data.d0, ctrlMA.frame);
            float g = getKey(data.d1, ctrlMA.frame);
            float b = getKey(data.d2, ctrlMA.frame);
            float a = getKey(data.d3, ctrlMA.frame);

            byte R = (byte)(r * 0xff);
            byte G = (byte)(g * 0xff);
            byte B = (byte)(b * 0xff);
            byte A = (byte)(a * 0xff);

            return Color.FromArgb(A, R, G, B);
        }

        private Vector2 getMaterialAnimationVector2(RenderBase.OMaterialAnimationData data)
        {
            float x = getKey(data.d0, ctrlMA.frame);
            float y = getKey(data.d1, ctrlMA.frame);

            return new Vector2(x, y);
        }

        private float getMaterialAnimationFloat(RenderBase.OMaterialAnimationData data)
        {
            return getKey(data.d0, ctrlMA.frame);
        }

        /// <summary>
        ///     Load the animation at the given index on the model Material Animation.
        ///     It will have no effect if the index is invalid.
        /// </summary>
        /// <param name="animationIndex">The index where the animation is located</param>
        public bool loadMaterialAnimation(int animationIndex)
        {
            if (animationIndex < -1 || animationIndex >= model.materialAnimation.Count) return false;
            ctrlMA.currentAnimation = animationIndex;
            ctrlMA.frame = 0;
            return true;
        }

        /// <summary>
        ///     Changes the current animation frame.
        ///     It will have no effect if the frame is outside of the animation range.
        /// </summary>
        /// <param name="targetFrame">The new frame index</param>
        public void setMaterialAnimationFrame(int targetFrame)
        {
            if (targetFrame < 0 || targetFrame > model.materialAnimation[ctrlMA.currentAnimation].frameSize) return;
            ctrlMA.frame = targetFrame;
        }

        /// <summary>
        ///     Pauses the animation.
        /// </summary>
        public void pauseMaterialAnimation()
        {
            ctrlMA.paused = true;
        }

        /// <summary>
        ///     Stops the animation, and render the still model.
        /// </summary>
        public void stopMaterialAnimation()
        {
            ctrlMA.animate = false;
            ctrlMA.frame = 0;
        }

        /// <summary>
        ///     Play the animation.
        ///     It will have no effect if no animations are loaded.
        /// </summary>
        public void playMaterialAnimation()
        {
            if (ctrlMA.currentAnimation == -1) return;
            ctrlMA.paused = false;
            ctrlMA.animate = true;
        }

        /// <summary>
        ///     Get the current animation index.
        ///     It will return -1 if no animations have been loaded yet.
        /// </summary>
        public int currentMaterialAnimationIndex
        {
            get { return ctrlMA.currentAnimation; }
        }
        #endregion

    }
}
