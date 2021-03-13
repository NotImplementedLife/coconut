using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoconutSharp.WinForms.API.Designer
{
    using API.Types;
    class ColorMatrixSerializer : CodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {            
            var cm = value as ColorMatrix;            
            string expr = $"new {cm.ToString()} ({cm.Rows},{cm.Columns})";            
            for(int i=0;i<cm.Rows;i++)
                for(int j=0;j<cm.Columns;j++)
                {
                    expr += $"\n\t\t\t\t.Set({i},{j}," +
                        $"System.Drawing.Color.FromArgb({cm[i, j].A},{cm[i,j].R},{cm[i,j].G},{cm[i,j].B}))";
                }
            return new CodeVariableReferenceExpression(expr);
        }
    }
}
