using System.CodeDom;
using System.ComponentModel.Design.Serialization;

namespace CoconutSharp.WinForms.API.Designer
{
    using API.Types;
    class RGBEncodingSerializer : CodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {                     
            return new CodeVariableReferenceExpression((value as RGBEncoding).FullName);
        }               
    }
}
