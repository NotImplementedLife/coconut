using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace CoconutSharp.WinForms.Controls
{
    [DefaultEvent("ValueChanged")]
    public partial class ColorWheel : UserControl
    {
        private const int PSize=360;       
        private const float PCenter = PSize * 0.5f;
        private const float PCenterSquared = PSize * PSize * 0.25f;
        private const float PLength = 20;
        private const float PRadius = PCenter - 2*PLength;        
        private const float PRadiusSquared = PRadius * PRadius;
        private const float PMedium = PCenter - PLength;
        private const float PMediumSquared = PMedium * PMedium;
        private static readonly PointF Center = new PointF(PCenter,PCenter);
        private const double _rad = Math.PI / 180;
        private const double _invrad = 180d / Math.PI;

        private Bitmap MainPalette=new Bitmap(PSize,PSize);
        private Bitmap BlackPalette = new Bitmap(PSize, PSize);
        private Bitmap AlphaBackground = new Bitmap(PSize, PSize);
        private Bitmap AlphaPalette = new Bitmap(PSize, PSize);
        public ColorWheel()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            Frame, new object[] { true });

            float[] r = { 0, -4.25f, 0, 0, 4.25f, 0 };
            float[] g = { 4.25f, 0, 0, -4.25f, 0, 0 };
            float[] b = { 0, 0, 4.25f, 0, 0, -4.25f };

            float R = 255, G = 0, B = 0;
            for (int k = 0; k < 6; k++)
            {
                for (int i = 0, p = k * 60; i < 60; i++)
                {
                    int q = p + i;
                    R += r[k];
                    G += g[k];
                    B += b[k];
                    aR[q] = (byte)R; aG[q] = (byte)G; aB[q] = (byte)B;
                }
            }

            AdjustFrame();                       
            CreateMainPalette();
            ColorFromMainPalette = ChooseMain(0, 1);         
            CreateAlphaBackground();           

        }

        private Color _ColorFromMainPalette;
        private Color ColorFromMainPalette
        {
            get=>_ColorFromMainPalette;
            set
            {
                _ColorFromMainPalette = value;
                CreateBlackPalette();
                ColorFromBlackPalette = ChooseBlack(359);                              
            }
        }

        private Color _ColorFromBlackPalette;
        private Color ColorFromBlackPalette
        {
            get => _ColorFromBlackPalette;
            set
            {
                _ColorFromBlackPalette = value;
                CreateAlphaPalette();                
                internalValueChanging = true;
                Value = value;               
                Frame.Invalidate();
            }
        }

        private Color _Value;
        public Color Value
        {
            get =>_Value;
            set
            {
                if(internalValueChanging)
                {
                    _Value = value;
                    internalValueChanging = false;
                    var changingEvent = new HandledEventArgs(false);
                    ValueChanging?.Invoke(this, changingEvent);
                    if (changingEvent.Handled) return;
                    ValueChanged?.Invoke(this, new EventArgs());
                    return;
                }               
                ColorFromMainPalette = Color.FromArgb(value.R, value.G, value.B);                
            }
        }

        private void AdjustFrame()
        {
            Frame.Width = Frame.Height = Math.Min(Width,Height);
            Frame.Left = (Width - Frame.Width) / 2;
            Frame.Top = (Height - Frame.Height) / 2;
        }

        byte[] aR = new byte[360];
        byte[] aG = new byte[360];
        byte[] aB = new byte[360];

        private void CreateBlackPalette()
        {
            var Color = ColorFromMainPalette;
            var gr = Graphics.FromImage(BlackPalette);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.Clear(Color.Transparent);
            float
                _360 = 1f / 360,
                _r = _360 * Color.R,
                _g = _360 * Color.G,
                _b = _360 * Color.B;          
            for(int i=0;i<360;i++)
            {
                var r = _r * i;
                var g = _g * i;
                var b = _b * i;
                var P1 = new PointF(
                        PCenter + PRadius * (float)Math.Cos((i) * _rad),
                        PCenter + PRadius * (float)Math.Sin((i) * _rad)
                    );
                var P2 = new PointF(
                       PCenter + (PRadius+PLength) * (float)Math.Cos((i) * _rad),
                       PCenter + (PRadius+PLength) * (float)Math.Sin((i) * _rad)
                   );
                gr.DrawLine(new Pen(Color.FromArgb((byte)r,(byte)g,(byte)b),3.5f),P1,P2);
            }
        }

        private void CreateMainPalette()
        {
            PointF[] pts = new PointF[6];
            const double a = Math.PI / 3;
            for (int i = 0; i < 6; i++)
            {
                pts[i].X = 2*PLength+(float)(PRadius * (1 + Math.Cos(i*a)));
                pts[i].Y = 2*PLength+(float)(PRadius * (1 + Math.Sin(i*a)));
            }
            using (var g = Graphics.FromImage(MainPalette))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {                    
                    path.AddLines(pts);
                    path.Flatten();
                    using (var path_brush = new PathGradientBrush(path))
                    {                       
                        path_brush.CenterColor = Color.White;
                        path_brush.SurroundColors = new Color[]
                        {
                        Color.Red,Color.Yellow,Color.Lime,
                        Color.Cyan,Color.Blue,Color.Magenta
                        };                       
                        g.FillPath(path_brush,path);
                    }
                }
            }           
        }
        
        private void CreateAlphaBackground()
        {
            var Color = ColorFromBlackPalette;
            var gr = Graphics.FromImage(AlphaBackground);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.Clear(Color.Transparent);
            var pattern = new Bitmap(20,20);
            using (var gp = Graphics.FromImage(pattern))
            {
                gp.FillRectangle(Brushes.White,new Rectangle(0,0,10,10));
                gp.FillRectangle(Brushes.LightGray,new Rectangle(10,0,10,10));
                gp.FillRectangle(Brushes.LightGray,new Rectangle(0,10,10,10));
                gp.FillRectangle(Brushes.White,new Rectangle(10,10,10,10));
            }
            var tBrush = new TextureBrush(pattern);
            var path = new GraphicsPath();
            path.AddEllipse(new RectangleF(PLength,PLength,2*PMedium,2*PMedium));
            gr.FillEllipse(tBrush,new Rectangle(0,0,PSize,PSize));            
            gr.Clip = new Region(path);
            gr.Clear(Color.Transparent);           
        }

        private void CreateAlphaPalette()
        {
            var Color = ColorFromBlackPalette;
            var gr = Graphics.FromImage(AlphaPalette);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.Clear(Color.Transparent);
            float _360 = 255f / 360;           
            for (int i = 0; i < 360; i++)
            {
                float a = _360 * i;               
                var P1 = new PointF(
                        PCenter - PSize/2 * (float)Math.Cos((i) * _rad),
                        PCenter - PSize/2 * (float)Math.Sin((i) * _rad)
                    );
                var P2 = new PointF(
                       PCenter - PMedium * (float)Math.Cos((i) * _rad),
                       PCenter - PMedium * (float)Math.Sin((i) * _rad)
                   );
                gr.DrawLine(new Pen(Color.FromArgb((byte)a,Color), 4f), P1, P2);                
            }            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AdjustFrame();
        }
        private void ColorPicker_Resize(object sender, EventArgs e)
        {            
            AdjustFrame();
            Invalidate(true);
        }

        private void Frame_Paint(object sender, PaintEventArgs e)
        {
            //var background = new Bitmap(PSize,PSize);
            //DrawToBitmap(background, Frame.ClientRectangle);
            var bitmap = new Bitmap(PSize, PSize);
            using (var g = Graphics.FromImage(bitmap))
            {                
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //g.DrawImageUnscaled(background,Point.Empty);
                g.DrawImageUnscaled(MainPalette, Point.Empty);
                g.DrawImageUnscaled(BlackPalette, Point.Empty);
                g.DrawImageUnscaled(AlphaBackground, Point.Empty);
                g.DrawImageUnscaled(AlphaPalette, Point.Empty);
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawImage(bitmap,new Rectangle(0,0,Frame.Width,Frame.Height));
        }

        private bool clicked = false;
        private int area = -1;
        private void Frame_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
        }

        private void Frame_MouseMove(object sender, MouseEventArgs e)
        {
            if (!clicked) return;           
            Point pos = Frame.PointToClient(Cursor.Position);
            double f = 360d / Frame.Width;
            System.Windows.Point
                wpos = new System.Windows.Point(pos.X*f, pos.Y*f),
                wcen = new System.Windows.Point(Center.X, Center.Y);
            var vec = wpos - wcen;
            double dist = vec.LengthSquared;
            if (dist < PRadiusSquared && new int[]{-1,0}.Contains(area)) 
            {
                area = 0;                
                var d = InvSqrt((float)dist);
                double x = vec.X, y = vec.Y;
                double a = Math.Acos(Math.Abs(x * d)) * _invrad;               
                if (x <= 0 && y <= 0) a = 180 - a;
                if (x < 0 && y > 0) a = 180 + a;
                if (x >= 0 && y > 0) a = 360-a;
                a = 360 - a;
                d = (float)Math.Sqrt(dist)/PRadius;
                ColorFromMainPalette = ChooseMain(a,d);                                            
            }
            else if(dist<PMediumSquared && new int[] { -1,1}.Contains(area))
            {
                area = 1;                
                var d = InvSqrt((float)dist);
                double x = vec.X, y = vec.Y;
                double a = Math.Acos(Math.Abs(x * d)) * _invrad;
                if (x <= 0 && y <= 0) a = 180 - a;
                if (x < 0 && y > 0) a = 180 + a;
                if (x >= 0 && y > 0) a = 360 - a;
                a = 360 - a;
                ColorFromBlackPalette = ChooseBlack(a);                
            }
            else if(dist<PCenterSquared && new int[] { -1, 2 }.Contains(area))
            {
                area = 2;             
                var d = InvSqrt((float)dist);
                double x = vec.X, y = vec.Y;
                double a = Math.Acos(Math.Abs(x * d)) * _invrad;
                if (x <= 0 && y <= 0) a = 180 - a;
                if (x < 0 && y > 0) a = 180 + a;
                if (x >= 0 && y > 0) a = 360 - a;
                a = 360 - a;
                internalValueChanging = true;
                Value = ChooseAlpha(a);               
            }                        
        }

        private void Frame_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
            area = -1;
        }

        private Color ChooseMain(double a,double d)
        {
            var r = aR[(int)a] * d + 255 * (1 - d);
            var g = aG[(int)a] * d + 255 * (1 - d);
            var b = aB[(int)a] * d + 255 * (1 - d);           
            return Color.FromArgb((byte)r, (byte)g, (byte)b);
        }

        private Color ChooseBlack(double a)
        {          
            a /= 360;           
            var r = ColorFromMainPalette.R * a;
            var g = ColorFromMainPalette.G * a;
            var b = ColorFromMainPalette.B * a;
            return Color.FromArgb((byte)r,(byte)g,(byte)b);
        }            
        
        private Color ChooseAlpha(double a)
        {
            a -= 180;
            if (a < 0) a += 360;
            a /= 360;           
            return Color.FromArgb((byte)(a*255),ColorFromBlackPalette);
        }

        private float InvSqrt(float x)
        {
            float xhalf = 0.5f * x;
            int i = BitConverter.ToInt32(BitConverter.GetBytes(x), 0);
            i = 0x5f3759df - (i >> 1);
            x = BitConverter.ToSingle(BitConverter.GetBytes(i), 0);
            x = x * (1.5f - xhalf * x * x);
            return x;
        }

        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                if(BackgroundImage!=null)
                    Frame.BackColor = value;
            }
        }

        public new Image BackgroundImage
        {
            get => base.BackgroundImage;
            set
            {
                base.BackgroundImage = value;
                Frame.BackColor = Color.Transparent;
            }
        }

        private void ColorWheel_DockChanged(object sender, EventArgs e)
        {           
            AdjustFrame();
            Invalidate(true);           
        }

        private bool internalValueChanging = false;

        public delegate void OnValueChanging(object sender, HandledEventArgs e);
        public event OnValueChanging ValueChanging;

        public delegate void OnValueChanged(object sender, EventArgs e);
        public event OnValueChanged ValueChanged;        
    }
}
