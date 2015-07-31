using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth
{
    public partial class OContainerForm : OForm
    {
        GenericContainer.OContainer container;

        public OContainerForm()
        {
            InitializeComponent();
        }

        public void launch(GenericContainer.OContainer _container)
        {
            container = _container;
            foreach (GenericContainer.OContainerEntry entry in container.content) FileList.addItem(entry.name);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (FileList.SelectedIndex == -1) return;
            ((FrmMain)Owner).open(new MemoryStream(container.content[FileList.SelectedIndex].data), container.content[FileList.SelectedIndex].name);
        }
    }
}
