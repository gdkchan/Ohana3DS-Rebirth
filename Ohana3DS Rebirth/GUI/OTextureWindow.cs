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
    public partial class OTextureWindow : ODockWindow
    {
        public OTextureWindow()
        {
            InitializeComponent();
        }

        public void initialize(RenderBase.OModelGroup model)
        {
            TextureList.addColumn(new OList.columnHeader(128, "#"));
            TextureList.addColumn(new OList.columnHeader(128, "Name"));
            foreach (RenderBase.OTexture texture in model.texture)
            {
                OList.listItemGroup item = new OList.listItemGroup();
                item.columns.Add(new OList.listItem(null, texture.texture));
                item.columns.Add(new OList.listItem(texture.name));
                TextureList.addItem(item);
            }
        }
    }
}
