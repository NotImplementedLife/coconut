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
    internal partial class TransparencyEffectGrid : UserControl
    {
        public TransparencyEffectGrid()
        {
            InitializeComponent();
            SquareSize = 10;
            BackColor = Color.Transparent;            
        }

        private float _SquareSize;
        public float SquareSize
        {
            get => _SquareSize;
            set
            {
                _SquareSize = value;
                CreateGrid();                
            }
        }

        private Bitmap _Grid;                       
        private void CreateGrid(bool invalidate=true)
        {
            _Grid = new Bitmap(Width, Height);
            var g = Graphics.FromImage(_Grid);
            bool k = false;
            for (float i = 0; i < Width + _SquareSize; i += _SquareSize)
            {
                g.FillRectangle(k?Brushes.White:Brushes.LightGray, new RectangleF(i, 0, _SquareSize, _SquareSize));
                g.FillRectangle(k?Brushes.LightGray:Brushes.White, new RectangleF(i, _SquareSize, _SquareSize, _SquareSize));
                k = !k;
            }

            for (float j = 0; j < Height + 2 * _SquareSize; j += 2 * _SquareSize)
                g.DrawImage(_Grid,new RectangleF(0,j,Width,2*_SquareSize),new RectangleF(0,0,Width,2*_SquareSize),GraphicsUnit.Pixel);
            g.FillRectangle(new SolidBrush(BackColor),this.ClientRectangle);
            if(invalidate)
                Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {            
            e.Graphics.DrawImage(_Grid, Point.Empty);           

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CreateGrid();
        }

        [DefaultValue(typeof(Color),"Transparent")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                CreateGrid();
            }
        }
    }
}
