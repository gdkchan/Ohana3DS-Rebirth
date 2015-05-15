using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Ohana3DS_Rebirth.Ohana
{
    class RenderEngine
    {
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
        public Vector2 rotation;
        public float zoom;

        private bool keepRendering;

        /// <summary>
        ///     Initialize the renderer at a given target.
        /// </summary>
        /// <param name="handler">Handler of the target</param>
        /// <param name="width">Render width</param>
        /// <param name="height">Render height</param>
        public void initialize(System.IntPtr handler, int width, int height)
        {
            pParams = new PresentParameters();
            pParams.BackBufferCount = 1;
            pParams.BackBufferFormat = Manager.Adapters[0].CurrentDisplayMode.Format;
            pParams.BackBufferWidth = width;
            pParams.BackBufferHeight = height;
            pParams.Windowed = true;
            pParams.SwapEffect = SwapEffect.Discard;
            pParams.EnableAutoDepthStencil = true;
            pParams.AutoDepthStencilFormat = DepthFormat.D16;

            try
            {
                device = new Device(0, DeviceType.Hardware, handler, CreateFlags.HardwareVertexProcessing, pParams);
            }
            catch
            {
                MessageBox.Show("Failed to initialize Direct3D with Hardware Acceleration!" + Environment.NewLine + "Now trying to use Software processing... Expect poor performance!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                device = new Device(0, DeviceType.Reference, handler, CreateFlags.HardwareVertexProcessing, pParams);
            }
            device.RenderState.Lighting = false;
            device.RenderState.CullMode = Cull.None;
            device.RenderState.ZBufferEnable = true;
            device.RenderState.AlphaBlendEnable = true;
            device.RenderState.SourceBlend = Blend.SourceAlpha;
            device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
            device.RenderState.BlendOperation = BlendOperation.Add;
            device.RenderState.AlphaFunction = Compare.GreaterEqual;
            device.SamplerState[0].MinFilter = TextureFilter.Linear;
            device.SamplerState[0].MagFilter = TextureFilter.Linear;
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
            device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, 20.0f), new Vector3(0.0F, 0.0F, 0.0F), new Vector3(0.0f, 1.0f, 0.0f));
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
            foreach (RenderBase.OTexture texture in model.texture)
            {
                CustomTexture tex;
                tex.name = texture.name;
                Bitmap bmp = new Bitmap(texture.texture);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                tex.texture = new Texture(device, bmp, Usage.None, Pool.Managed);
                textures.Add(tex);
            }

            setupViewPort();
            float scale = (10.0f / (model.maximumY - model.minimumY)); //Try to adjust to screen
            keepRendering = true;
            while (keepRendering)
            {
                device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, 0x5f5f5f, 1.0f, 0);
                device.BeginScene();

                Matrix rotationMatrix = Matrix.RotationYawPitchRoll(-rotation.X / 200.0f, -rotation.Y / 200.0f, 0);
                Matrix translationMatrix = Matrix.Translation(new Vector3((-translation.X / 50.0f) / scale, (translation.Y / 50.0f) / scale, zoom / scale));
                device.Transform.World = rotationMatrix * translationMatrix * Matrix.Scaling(-scale, scale, scale);

                Material material = new Material();
                material.Diffuse = Color.White;
                material.Ambient = Color.White;
                device.Material = material;

                foreach (RenderBase.OModel mdl in model.model)
                {
                    foreach (RenderBase.OModelObject obj in mdl.modelObject)
                    {
                        device.SetTexture(0, null);
                        if (obj.textureId < mdl.textureParameters.Count)
                        {
                            RenderBase.OTextureParameter parameter = mdl.textureParameters[obj.textureId];
                            foreach (CustomTexture texture in textures)
                            {
                                if (texture.name == parameter.name1) device.SetTexture(0, texture.texture);
                            }
                        }

                        VertexFormats vertexFormat = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture1 | VertexFormats.Diffuse;
                        device.VertexFormat = vertexFormat;
                        VertexBuffer vertexBuffer = new VertexBuffer(typeof(RenderBase.CustomVertex), obj.renderBuffer.Length, device, Usage.None, vertexFormat, Pool.Managed);
                        vertexBuffer.SetData(obj.renderBuffer, 0, LockFlags.None);
                        device.SetStreamSource(0, vertexBuffer, 0);

                        device.DrawPrimitives(PrimitiveType.TriangleList, 0, obj.renderBuffer.Length / 3);
                        vertexBuffer.Dispose();
                    }
                }

                device.EndScene();
                device.Present();

                Application.DoEvents();
            }
        }
    }
}
