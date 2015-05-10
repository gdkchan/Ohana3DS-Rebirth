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
        public OModelWindow()
        {
            InitializeComponent();
        }

        public void initialize(RenderBase.OModelGroup model)
        {
            RenderEngine renderer = new RenderEngine();
            renderer.initialize(Screen.Handle, Screen.Width, Screen.Height);
            renderer.model = model;
            renderer.render();
        }
    }
}
