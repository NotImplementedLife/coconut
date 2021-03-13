using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoconutSharp.WinForms.API.InternalControls
{
    internal partial class TransparentPanel : UserControl
    {
        public TransparentPanel()
        {
            InitializeComponent();
        }

        const int WS_EX_TRANSPARENT = 0x20;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {            
        }

        protected override void OnPaint(PaintEventArgs e)
        {            
            base.OnPaint(e);
        }
    }
}
