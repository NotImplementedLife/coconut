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
    using API.Types;
    internal partial class ColorGrid : UserControl
    {
        public ColorGrid()
        {
            InitializeComponent();
            UpdateEnabled = false;           
            CellSize = 10;
            CellsCount = new Size(10, 10);            
            UpdateEnabled = true;
            Frame.Top = (Height - Frame.Height) / 2;
            Frame.Left = (Width - Frame.Width) / 2;
        }

        private ColorMatrix _Cells;
        [Category("Grid")]
        public ColorMatrix Cells
        {
            get =>_Cells;
            set
            {
                _Cells = value;
                GridUpdate(false);
            }
        }

        private Size _CellsCount;
        [Category("Grid")]
        public Size CellsCount
        {
            get => _CellsCount;
            set
            {
                _CellsCount = value;
                GridUpdate();
            }
        }

        private int _CellSize;
        [Category("Grid")]
        public int CellSize
        {
            get => _CellSize;
            set
            {
                _CellSize = value;
                GridUpdate();
            }
        }

        [Category("Grid")]
        public int ActualCellSize {get; private set;}

        private bool _UpdateEnabled;

        [Browsable(false)]
        public bool UpdateEnabled
        {
            get => _UpdateEnabled;
            set
            {
                _UpdateEnabled = value;
                GridUpdate();
            }
        }
        private void GridUpdate(bool alterateCells=true)
        {
            if (alterateCells)
            {
                if (_Cells == null) Cells = new Color[_CellsCount.Width, _CellsCount.Height];
                                
                if (_CellsCount != new Size(_Cells.Rows, _Cells.Columns))
                {
                    Color[,] cells = new Color[_CellsCount.Width, _CellsCount.Height];
                    var w = Math.Min(_Cells.Rows, _CellsCount.Width);
                    var h = Math.Min(_Cells.Columns, _CellsCount.Height);                   
                    for (int i = 0; i < w; i++)
                        for (int j = 0; j < h; j++)
                            cells[i, j] = _Cells[i, j];
                    _Cells = cells;
                }                          
               
            }
            else
            {              
                CellsCount = new Size(_Cells.Rows,_Cells.Columns);
            }

            if (!UpdateEnabled) return;
            ActualCellSize = _CellSize;
            var frameSize = new Size(ActualCellSize * _CellsCount.Width + 1, ActualCellSize * _CellsCount.Height + 1);
            if (frameSize.Width > Width || frameSize.Height > Height)
            {
                float f = Math.Min((float)Width / frameSize.Width, (float)Height / frameSize.Height);                
                ActualCellSize = (int)(f * ActualCellSize);
            }
            Frame.Size = new Size(ActualCellSize * _CellsCount.Width + 1, ActualCellSize * _CellsCount.Height + 1);
            TransparencyGrid.SquareSize = ActualCellSize * .5f;
            Canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {           
            Bitmap colors = new Bitmap(Width, Height);
            var gcolors = Graphics.FromImage(colors);

            for (int i = 0; i < CellsCount.Width; i++)
                for (int j = 0; j < CellsCount.Height; j++)
                {
                    gcolors.FillRectangle(new SolidBrush(Cells[i, j]), 
                        new Rectangle(ActualCellSize * i, ActualCellSize * j, ActualCellSize, ActualCellSize));
                }


            Bitmap grid = new Bitmap(Width, Height);
            var ggrid = Graphics.FromImage(grid);
            for (int i = 0; i <= CellsCount.Width; i++)
                ggrid.DrawLine(Pens.Black, new Point(i * ActualCellSize, 0), new Point(i * ActualCellSize, Height));
            for (int j = 0; j <= CellsCount.Height; j++)
                ggrid.DrawLine(Pens.Black, new Point(0, j * ActualCellSize), new Point(Width, j * ActualCellSize));

            e.Graphics.DrawImageUnscaled(colors, Point.Empty);
            e.Graphics.DrawImageUnscaled(grid, Point.Empty);
        }

        private void ColorGrid_Resize(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void Canvas_Resize(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void ArrangeControls()
        {
            if (!CanResize) return;
            CanResize = false;
            GridUpdate();
            Frame.Top = (Height - Frame.Height) / 2;
            Frame.Left = (Width - Frame.Width) / 2;
            CanResize = true;
        }

        private bool CanResize = true;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
