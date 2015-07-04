using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OModelWindow : ODockWindow
    {
        RenderEngine renderer;

        bool clicked;
        RenderBase.OVector2 initialRotation, initialMovement;
        RenderBase.OVector2 finalMovement;

        public OModelWindow()
        {
            initialRotation = new RenderBase.OVector2();
            initialMovement = new RenderBase.OVector2();
            finalMovement = new RenderBase.OVector2();

            InitializeComponent();
            ModelMenu.Renderer = new GUI.OMenuStrip();
        }

        private void Screen_Resize(object sender, EventArgs e)
        {
            if (renderer != null) renderer.resize(Screen.Width, Screen.Height);
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            renderer.initialize(Screen.Handle, Screen.Width, Screen.Height);
            renderer.render();
        }

        public override void dispose()
        {
            renderer.dispose();

            base.dispose();
        }

        private void Screen_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                if (renderer != null)
                {
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            float rY = (float)(((e.X - initialRotation.x) / Screen.Width) * Math.PI);
                            float rX = (float)(((e.Y - initialRotation.y) / Screen.Height) * Math.PI);
                            renderer.setRotation(rY, rX);
                            initialRotation = new RenderBase.OVector2(e.X, e.Y);
                            break;
                        case MouseButtons.Middle:
                            renderer.translation.X = (initialMovement.x - MousePosition.X) + finalMovement.x;
                            renderer.translation.Y = (initialMovement.y - MousePosition.Y) + finalMovement.y;
                            break;
                    }
                }
            }
        }

        private void Screen_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Screen.Focused) Screen.Select();
            clicked = true;
            switch (e.Button)
            {
                case MouseButtons.Left: initialRotation = new RenderBase.OVector2(e.X, e.Y); break;
                case MouseButtons.Middle: initialMovement = new RenderBase.OVector2(MousePosition.X, MousePosition.Y); break;
                case MouseButtons.Right: ModelMenu.Show(Cursor.Position); break;
            }
        }

        private void Screen_MouseUp(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                clicked = false;
                switch (e.Button)
                {
                    case MouseButtons.Middle: finalMovement = new RenderBase.OVector2(finalMovement.x + (initialMovement.x - MousePosition.X), finalMovement.y + (initialMovement.y - MousePosition.Y)); break;
                }
            }
        }

        private void Screen_MouseWheel(object sender, MouseEventArgs e)
        {
            if (renderer != null && e.Delta > 0) renderer.zoom += 1.0f; else renderer.zoom -= 1.0f;
        }

        private void MnuImport_Click(object sender, EventArgs e)
        {
            Object importedData = FileImporter.import(FileImporter.importFileType.model);
            if (importedData != null) renderer.model.model.AddRange((List<RenderBase.OModel>)importedData);
        }

        private void MnuClear_Click(object sender, EventArgs e)
        {
            renderer.model.model.Clear();
        }
    }
}
