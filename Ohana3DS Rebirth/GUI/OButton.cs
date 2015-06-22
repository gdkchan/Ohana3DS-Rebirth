using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OButton : Button
    {
        public OButton()
        {
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Black;
            InitializeComponent();
        }
    }
}
