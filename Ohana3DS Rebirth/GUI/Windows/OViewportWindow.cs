using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OViewportWindow : ODockWindow
    {
        RenderEngine renderer;

        bool clicked;
        RenderBase.OVector2 initialRotation, initialMovement;
        RenderBase.OVector2 finalMovement;

        public OViewportWindow()
        {
            initialRotation = new RenderBase.OVector2();
            initialMovement = new RenderBase.OVector2();
            finalMovement = new RenderBase.OVector2();

            InitializeComponent();
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
                        case MouseButtons.Right:
                            renderer.setTranslation(
                                (initialMovement.x - MousePosition.X) + finalMovement.x, 
                                (initialMovement.y - MousePosition.Y) + finalMovement.y);
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
                case MouseButtons.Right: initialMovement = new RenderBase.OVector2(MousePosition.X, MousePosition.Y); break;
            }
        }

        private void Screen_MouseUp(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                clicked = false;
                switch (e.Button)
                {
                    case MouseButtons.Right: finalMovement = new RenderBase.OVector2(finalMovement.x + (initialMovement.x - MousePosition.X), finalMovement.y + (initialMovement.y - MousePosition.Y)); break;
                }
            }
        }

        private void Screen_MouseWheel(object sender, MouseEventArgs e)
        {
            if (renderer != null && e.Delta > 0) renderer.setZoom(renderer.Zoom + 1.0f); else renderer.setZoom(renderer.Zoom - 1.0f);
        }
    }
}
