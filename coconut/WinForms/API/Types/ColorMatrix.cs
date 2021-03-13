using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.ComponentModel.Design.Serialization;

namespace CoconutSharp.WinForms.API.Types
{
    using API.Designer;
    [Editor(typeof(ColorMatrixTypeEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DesignerSerializer(typeof(ColorMatrixSerializer), typeof(CodeDomSerializer))]
    public class ColorMatrix
    {
        public ColorMatrix() : this(null) {}
        public ColorMatrix(Color[,] data) { this.data = data; }         
        
        public ColorMatrix(int rows,int cols) : this(new Color[rows, cols]) { }       
        public Color this[int i,int j]
        {
            get
            {
                try
                {
                    return data[i, j];
                }
                catch (IndexOutOfRangeException e)
                {
                    if (!DefaultValueOnException) throw e;
                    return Color.Transparent;
                }
            }
            set 
            {
                try
                {
                    data[i, j] = value;
                }
                catch (IndexOutOfRangeException e)
                {
                    if (!DefaultValueOnException) throw e;
                }
            }
        }
        
        public ColorMatrix Set(int i,int j,Color Color)
        {
            this[i, j] = Color;
            return this;
        }

        public bool DefaultValueOnException = false;
        public int Rows { get => data == null ? 0 : data.GetLength(0); }
        public int Columns { get => data == null ? 0 : data.GetLength(1); }

        public static explicit operator Color[,] (ColorMatrix cm) => cm.data;
        public static implicit operator ColorMatrix(Color[,] c) => new ColorMatrix(c);

        Color[,] data;
    }
}
