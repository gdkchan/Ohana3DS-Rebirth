using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OModelWindow : ODockWindow
    {
        RenderEngine renderer;

        RenderBase.OVector2 initialRotation, initialMovement;
        RenderBase.OVector2 finalMovement;
        bool clicked;

        public OModelWindow()
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
            if (!Screen.Focused) Screen.Select();
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
                        renderer.translation.X = (initialMovement.x - MousePosition.X) + finalMovement.x;
                        renderer.translation.Y = (initialMovement.y - MousePosition.Y) + finalMovement.y;
                        break;
                }
            }
        }

        private void Screen_MouseDown(object sender, MouseEventArgs e)
        {
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
                switch (e.Button)
                {
                    case MouseButtons.Right: finalMovement = new RenderBase.OVector2(finalMovement.x + (initialMovement.x - MousePosition.X), finalMovement.y + (initialMovement.y - MousePosition.Y)); break;
                }
                clicked = false;
            }
        }

        private void Screen_MouseWheel(object sender, MouseEventArgs e)
        {
            if (renderer != null && e.Delta > 0) renderer.zoom += 1.0f; else renderer.zoom -= 1.0f;
        }
    }
}
