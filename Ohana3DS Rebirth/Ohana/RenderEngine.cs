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
        private struct CustomVertex
        {
            public float x, y, z;
            public float nx, ny, nz;
            public uint color;
            public float u, v;
        }
        private List<CustomVertex> renderData;

        public Vector2 translation;
        public Vector2 rotation;
        public float zoom;

        private bool keepRendering;

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
            device.RenderState.CullMode = Cull.None;
            device.RenderState.ZBufferEnable = true;
            device.RenderState.AlphaBlendEnable = true;
            device.RenderState.SourceBlend = Blend.SourceAlpha;
            device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
            device.RenderState.BlendOperation = BlendOperation.Add;
            device.RenderState.AlphaFunction = Compare.GreaterEqual;
        }

        public void render()
        {
            fillRenderData();
            keepRendering = true;

            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, (float)(pParams.BackBufferWidth / pParams.BackBufferHeight), 0.1f, 500.0f);
            device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, 20.0f), new Vector3(0.0F, 0.0F, 0.0F), new Vector3(0.0f, 1.0f, 0.0f));

            while (keepRendering)
            {
                device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, 0x5f5f5f, 1.0f, 0);
                device.BeginScene();

                Matrix rotationMatrix = Matrix.RotationYawPitchRoll(-rotation.X / 200.0f, -rotation.Y / 200.0f, 0);
                Matrix translationMatrix = Matrix.Translation(new Vector3(-translation.X / 50.0f, translation.Y / 50.0f, zoom));
                device.Transform.World = rotationMatrix * translationMatrix * Matrix.Scaling(-1, 1, 1);

                Material material = new Material();
                material.Diffuse = Color.White;
                material.Ambient = Color.White;
                device.Material = material;

                VertexFormats vertexFormat = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture1 | VertexFormats.Diffuse;
                device.VertexFormat = vertexFormat;
                VertexBuffer vertexBuffer = new VertexBuffer(typeof(CustomVertex), renderData.Count, device, Usage.None, vertexFormat, Pool.Managed);
                vertexBuffer.SetData(renderData.ToArray(), 0, LockFlags.None);
                device.SetStreamSource(0, vertexBuffer, 0);

                device.DrawPrimitives(PrimitiveType.TriangleList, 0, renderData.Count / 3);
                vertexBuffer.Dispose();

                device.EndScene();
                device.Present();

                Application.DoEvents();
            }
        }

        private void fillRenderData()
        {
            renderData = new List<CustomVertex>();
            for (int i = 0; i < model.model.Count; i++)
            {
                for (int j = 0; j < model.model[i].modelObject.Count; j++)
                {
                    for (int k = 0; k < model.model[i].modelObject[j].obj.Count; k++)
                    {
                        CustomVertex vertex;

                        vertex.x = model.model[i].modelObject[j].obj[k].position.x;
                        vertex.y = model.model[i].modelObject[j].obj[k].position.y;
                        vertex.z = model.model[i].modelObject[j].obj[k].position.z;

                        vertex.nx = model.model[i].modelObject[j].obj[k].normal.x;
                        vertex.ny = model.model[i].modelObject[j].obj[k].normal.y;
                        vertex.nz = model.model[i].modelObject[j].obj[k].normal.z;

                        vertex.u = model.model[i].modelObject[j].obj[k].texture.x;
                        vertex.v = model.model[i].modelObject[j].obj[k].texture.y;

                        vertex.color = model.model[i].modelObject[j].obj[k].diffuseColor;

                        renderData.Add(vertex);
                    }
                }
            }
        }
    }
}
