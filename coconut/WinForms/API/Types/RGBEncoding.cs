using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoconutSharp.WinForms.API.Types
{
    using API.Designer;

    [Description("(A)RGB color encoding")]
    [Editor(typeof(RGBEncodingTypeEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(RGBEncodingTypeConverter))]
    [DesignerSerializer(typeof(RGBEncodingSerializer), typeof(CodeDomSerializer))]   
    public class RGBEncoding
    {
        public static RGBEncoding
            RGB  = new RGBEncoding("RGB" ,123),
            BGR  = new RGBEncoding("BGR" , 321),
            RGBA = new RGBEncoding("RGBA", 1234),
            ARGB = new RGBEncoding("ARGB", 4123),
            BGRA = new RGBEncoding("BGRA", 3214),
            ABGR = new RGBEncoding("ABGR", 4321);

        public RGBEncoding(string name, int value)
        {
            this.Name = name;
            this.Value = value;            
        }

        public RGBEncoding(RGBEncoding enc)
        {
            this.Name = enc.Name;
            this.Value = enc.Value;
        }

        public RGBEncoding(string name)
        {
            foreach(RGBEncoding enc in list)
            {
                if (enc.Name == name)
                {
                    Name = enc.Name;
                    Value = enc.Value;
                    return;
                }
            }
            Name = RGB.Name;
            Value = RGB.Value;            
        }    

        public string Name { get; private set; }
        protected internal int Value { get; private set; }

        public bool IsAlpha => Name.Length == 4; 

        public static implicit operator int (RGBEncoding enc) => enc.Value;

        public static List<RGBEncoding> list = GetList();
        private static List<RGBEncoding> GetList()
        {            
            return typeof(RGBEncoding)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(RGBEncoding))
                .ToList()
                .ConvertAll(new Converter<FieldInfo, RGBEncoding>                
                    (f => (RGBEncoding)f.GetValue(null)));
        }        
        public string FullName { get => "CoconutSharp.WinForms.API.Types.RGBEncoding." + Name; }

        /*public override string ToString()
        {
            return this.Name;
        }*/
    }
}
