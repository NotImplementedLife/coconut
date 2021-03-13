using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CoconutSharp.WinForms.API.InternalControls
{
    using API.Types;
    using System.ComponentModel.Design;
    using System.Drawing.Design;

    internal partial class HexColorBox : TextBox
    {       
        public HexColorBox()
        {
            InitializeComponent();                    
            CheckTextAvailability();
        }

        #region private Members

        private void CheckTextAvailability()        
        {
            try
            {
                Value = RGBConverter.Convert(Text, RGBEncoding);
            }
            catch (ArgumentSizeException)
            {
                Text = Text.Substring(0, 8);
                SelectionStart = Text.Length;
                SelectionLength = 0;
            }
            catch (RGBAToRGBInvalidConversionException)
            {
                Text = Text.Substring(0, 6);
                SelectionStart = Text.Length;
                SelectionLength = 0;
            }
            catch (ArgumentException)
            {
                StringBuilder text = new StringBuilder(Text);
                for (var i = 0; i < text.Length; i++)
                {
                    var c = text[i];
                    if (('0' <= c && c <= '9') || ('A' <= c && c <= 'F') || ('a' <= c && c <= 'f'))
                        continue;
                    text[i] = '0';
                }
                Text = text.ToString();
            }
        }       


        private Color _Value;
        private RGBEncoding _RGBEncodingType;

        #endregion                        

        #region protected override Members
        protected override void OnKeyPress(KeyPressEventArgs e)       
        {            
            if (SelectionStart < TextLength && !Char.IsControl(e.KeyChar))
            {
                int CacheSelectionStart = SelectionStart;
                StringBuilder sb = new StringBuilder(Text); 
                sb[SelectionStart] = e.KeyChar; 
                Text = sb.ToString(); 
                SelectionStart = CacheSelectionStart + 1;
                CheckTextAvailability();
                e.Handled = true;
            }           
        }       

        protected override void OnTextChanged(EventArgs e)       
        {           
            CheckTextAvailability();           
        }    

        #endregion     

        #region Events
        public delegate void OnValueChanged(object sender, EventArgs e);
        public event OnValueChanged ValueChanged;
    
        public delegate void OnRGBEncodingTypeChanged(object sender, EventArgs e);
        public event OnRGBEncodingTypeChanged RGBEncodingTypeChanged;
        #endregion

        #region Properties

        [Browsable(true)]
        [Description("Color from input.")]
        public Color Value
        {
            get => _Value;
            set
            {
                bool changed = (_Value != value);
                _Value = value;
                Text = RGBConverter.Convert(Value, RGBEncoding);
                if (changed)
                    ValueChanged?.Invoke(this, new EventArgs());
            }
        }

        [Browsable(true)]
        [Editor(typeof(Designer.RGBEncodingTypeEditor), typeof(UITypeEditor))]                
        public RGBEncoding RGBEncoding
        {
            get => _RGBEncodingType;
            set
            {
                _RGBEncodingType = value;
                Value = Value;
                RGBEncodingTypeChanged?.Invoke(this, new EventArgs());
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override bool Multiline { get => base.Multiline; set { } }
        #endregion

        #region Designer Services

        private IDesignerHost designerHost;

        public override ISite Site
        {
            get { return base.Site; }
            set
            {
                base.Site = value;
                if (value == null)
                {                   
                    DetachDesignerServices();
                }
                else
                {
                    designerHost = (IDesignerHost)value.GetService(typeof(IDesignerHost));
                    if (designerHost != null)
                    {
                        if (designerHost.Loading)
                        {                          
                            designerHost.LoadComplete += DesignerHostLoaded;
                        }
                        else
                        {
                            if (designerHost.InTransaction)
                            {                                
                                designerHost.TransactionClosed += DesignerTransactionClosed;
                            }
                            else
                            {                                
                                ClearDesignerActionLists();
                            }
                        }
                    }
                }
            }
        }

        private void DesignerHostLoaded(object sender, EventArgs e)
        {
            designerHost.LoadComplete -= DesignerHostLoaded;
            ClearDesignerActionLists();
        }

        private void DesignerTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
        {
            designerHost.TransactionClosed -= DesignerTransactionClosed;
            ClearDesignerActionLists();
        }
        private void DetachDesignerServices()
        {
            if (designerHost != null)
            {
                designerHost.TransactionClosed -= DesignerTransactionClosed;
                designerHost.LoadComplete -= DesignerHostLoaded;
                designerHost = null;
            }
        }

        private void ClearDesignerActionLists()
        {
            ControlDesigner myDesigner = designerHost.GetDesigner(this) as ControlDesigner;
            myDesigner?.ActionLists.Clear();
        }

        #endregion


    }
}
