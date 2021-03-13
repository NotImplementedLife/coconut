using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Windows.Forms;

namespace CoconutSharp.WinForms.API.Designer
{
    using API.Types;

    public class RGBEncodingTypeConverter : TypeConverter
    {
        Dictionary<string, RGBEncoding> values;

        public Type ContainerType { get; private set; }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)        
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string FullName = (string)value;
            MessageBox.Show(FullName);

            if (FullName == "(none)")
                return null;

            initValues(context);
            

            if (values.ContainsKey(FullName))
                return values[FullName];

            return value;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
                return "(none)";

            initValues(context);

            foreach (string key in values.Keys)
                if (key != "(none)")
                    if (values[key].FullName == ((RGBEncoding)value).FullName)
                        return key;
           
            return "error";
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            initValues(context);   
            return new StandardValuesCollection(values.Values);
        }

        void initValues(ITypeDescriptorContext context)
        {  
            
            if (context == null) return;

            var tvalues = new Dictionary<string, RGBEncoding>();
            tvalues.Add("(none)",null);
            
            ITypeDiscoveryService Service = (ITypeDiscoveryService)context.GetService(typeof(ITypeDiscoveryService));
            
            if (Service == null)
                return;

            foreach(RGBEncoding enc in RGBEncoding.list)
            {
                tvalues.Add(enc.Name,enc);               
            }           
            if (tvalues.Count > 1 || values == null)
                values = tvalues;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }
    }
}
