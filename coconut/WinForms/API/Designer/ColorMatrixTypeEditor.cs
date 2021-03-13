using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CoconutSharp.WinForms.API.Designer
{
    using API.Types;
    class ColorMatrixTypeEditor:UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            ColorMatrix cm = value as ColorMatrix;
            if (svc != null && cm != null)
            {
                using (ColorMatrixEditorForm form = new ColorMatrixEditorForm())
                {
                    form.Value = cm;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        cm = form.Value;
                    }
                }
            }
            return cm; 
        }
    }
}
