using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoconutSharp.WinForms.API.Designer;
using CoconutSharp.WinForms.API.Types;

namespace CoconutSharp.WinForms.Controls
{    
   
    [Designer(typeof(ColorInputDesigner))]
    [DefaultEvent("ValueChanged")]
    public partial class ColorInput : UserControl
    {
        public ColorInput()
        {
            InitializeComponent();
            RField.TextChanged += FieldValueChanged;
            GField.TextChanged += FieldValueChanged;
            BField.TextChanged += FieldValueChanged;
            AField.TextChanged += FieldValueChanged;           
            GetColorFromRGBAFields();
            Value = Value;
            ColorDisplay.BackColor = Value;
        }
                      
        public RGBEncoding RGBEncoding
        {
            get => HexField.RGBEncoding;
            set => HexField.RGBEncoding=value;
        }

        private void GetColorFromRGBAFields(object sender=null)
        {
            var cl = Value;
            var el = sender as NumericUpDown;

            if (el == AField)
                Value = Color.FromArgb(HexField.RGBEncoding.IsAlpha ? (int)AField.Value : 255, cl);
            else if (el == RField)
                Value = Color.FromArgb(cl.A, (int)RField.Value, cl.G, cl.B);
            else if (el == GField)
                Value = Color.FromArgb(cl.A, cl.R, (int)GField.Value, cl.B);
            else if (el == BField)
                Value = Color.FromArgb(cl.A, cl.R, cl.G, (int)BField.Value);
            else
                Value = Color.FromArgb(HexField.RGBEncoding.IsAlpha ? (int)AField.Value : 255,
                    (int)RField.Value, (int)GField.Value, (int)BField.Value);
        }

        private bool fromFields = false;
        private bool updatingFields = false;
        private void FieldValueChanged(object sender,EventArgs e)
        {
            if (updatingFields) return;
            fromFields = true;
            GetColorFromRGBAFields(sender);
        }

        private void HexField_RGBEncodingTypeChanged(object sender, EventArgs e)
        {
            AField.Enabled = HexField.RGBEncoding.IsAlpha;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {                       
            fixedHeight = panel2.Height;
            minWidth = panel1.Width + panel2.Width;
            if (width < minWidth) width = minWidth;
            height = fixedHeight;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        [DefaultValue(typeof(Color),"Black")]
        public Color Value
        {
            get => HexField.Value;
            set 
            {
                var changingEvent = new HandledEventArgs(false);
                ValueChanging?.Invoke(this, changingEvent);              
                if (changingEvent.Handled) return;                
                HexField.Value = value;
                ValueChanged?.Invoke(this,new EventArgs());
            }
        }

        private void HexField_ValueChanged(object sender, EventArgs e)
        {            
            ColorDisplay.BackColor = Value;    
            if(fromFields)
            {
                fromFields = false;
                return;
            }            
            RField.Value = HexField.Value.R;
            GField.Value = HexField.Value.G;
            BField.Value = HexField.Value.B;           
            AField.Value = HexField.Value.A;            
        }

        private void ColorInput_FontChanged(object sender, EventArgs e)
        {           
            SetBoundsCore(Left,Top,Width,Height,BoundsSpecified.Size);
        }       
               
        private int fixedHeight;
        private int minWidth;

        public delegate void OnValueChanging(object sender, HandledEventArgs e);
        public event OnValueChanging ValueChanging;

        public delegate void OnValueChanged(object sender,EventArgs e);
        public event OnValueChanged ValueChanged;
    }    
}
