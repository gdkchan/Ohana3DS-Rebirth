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
        RenderBase.OVector2 finalRotation, finalMovement;

        public OModelWindow()
        {
            initialRotation = new RenderBase.OVector2();
            initialMovement = new RenderBase.OVector2();
            finalRotation = new RenderBase.OVector2();
            finalMovement = new RenderBase.OVector2();

            InitializeComponent();
        }

        public void initialize(RenderBase.OModelGroup model)
        {
            renderer = new RenderEngine();
            renderer.initialize(Screen.Handle, Screen.Width, Screen.Height);
            renderer.model = model;
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
            switch (e.Button)
            {
                case MouseButtons.Left:
                    renderer.rotation.X = (initialRotation.x - MousePosition.X) + finalRotation.x;
                    renderer.rotation.Y = (initialRotation.y - MousePosition.Y) + finalRotation.y;
                    break;
                case MouseButtons.Right:
                    renderer.translation.X = (initialMovement.x - MousePosition.X) + finalMovement.x;
                    renderer.translation.Y = (initialMovement.y - MousePosition.Y) + finalMovement.y;
                    break;
            }
        }

        private void Screen_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: initialRotation = new RenderBase.OVector2(MousePosition.X, MousePosition.Y); break;
                case MouseButtons.Right: initialMovement = new RenderBase.OVector2(MousePosition.X, MousePosition.Y); break;
            }
        }

        private void Screen_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: finalRotation = new RenderBase.OVector2(finalRotation.x + (initialRotation.x - MousePosition.X), finalRotation.y + (initialRotation.y - MousePosition.Y)); break;
                case MouseButtons.Right: finalMovement = new RenderBase.OVector2(finalMovement.x + (initialMovement.x - MousePosition.X), finalMovement.y + (initialMovement.y - MousePosition.Y)); break;
            }
        }

        private void Screen_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) renderer.zoom += 1.0f; else renderer.zoom -= 1.0f;
        }
    }
}
