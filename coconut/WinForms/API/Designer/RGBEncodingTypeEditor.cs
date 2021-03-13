using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;

namespace CoconutSharp.WinForms.API.Designer
{
    using API.Types;
    public class RGBEncodingTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {           
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {            
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            ListBox lb = new ListBox
            {
                SelectionMode = SelectionMode.One
            };           
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;
                       
            lb.DisplayMember = "Name";
            var list = RGBEncoding.list;
            foreach(RGBEncoding enc in list)
            {
                int index = lb.Items.Add(enc);
                if(enc.Equals(value))
                {
                    lb.SelectedIndex = index;
                }
            }                                           
            _editorService.DropDownControl(lb);

            if (lb.SelectedItem == null)
                return value;

            return lb.SelectedItem;
        }

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {           
            _editorService.CloseDropDown();
        }      
    }
}
