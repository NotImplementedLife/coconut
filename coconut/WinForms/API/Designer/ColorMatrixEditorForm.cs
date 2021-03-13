using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoconutSharp.WinForms.API.Designer
{
    using Types;
    using InternalControls;
    // not ready for use yet | unstable 
    public partial class ColorMatrixEditorForm : Form
    {
        public ColorMatrixEditorForm()
        {
            InitializeComponent();
            ColorInput.Value = Color.Red;
            createTable();
        }

        private ColorMatrix _Value;
        public ColorMatrix Value
        {
            get => _Value;
            set
            {                
                _Value = value;
                update = false;
                numericUpDown1.Value = _Value.Rows;
                numericUpDown2.Value = _Value.Columns;
                update = true;
                createTable(true);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {                        
            var value = new ColorMatrix(rows,cols);
            foreach(Control control in Table.Controls)
            {
                var p = (Point)control.Tag;
                value[p.X, p.Y] = control.BackColor;
            }
            Value = value;            
            DialogResult = DialogResult.OK;
            Close();
        }      
       
        private void ColorWheel_ValueChanging(object sender, HandledEventArgs e)
        {
                
        }

        private void ColorInput_ValueChanging(object sender, HandledEventArgs e)
        {
                     
        }

        private bool wheelChanging = false;

        private bool inputChanging = false;

        private void ColorWheel_ValueChanged(object sender, EventArgs e)
        {
            if (inputChanging) return;
            wheelChanging = true;
            ColorInput.Value = ColorWheel.Value;
            wheelChanging = false;
        }

        private void ColorInput_ValueChanged(object sender, EventArgs e)
        {
            if (wheelChanging) return;
            inputChanging = true;
            ColorWheel.Value = ColorInput.Value;
            inputChanging = false;
        }

        private int rows=1, cols=1;
        private bool update = true;

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {            
            cols = (int)numericUpDown2.Value;
            if(update) createTable();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            rows = (int)numericUpDown1.Value;
            if(update) createTable();
        }

        private void createTable(bool fromValue=false)
        {
            var cm = fromValue ? _Value : getFromTable();
            Table.SuspendLayout();
            Table.Controls.Clear();
            for(int i=0;i<rows;i++)
            {
                for(int j=0;j<cols;j++)
                {
                    var cell = new TransparencyEffectGrid();
                    cell.Tag = new Point(i, j);
                    cell.BackColor = Color.Transparent;
                    cell.Size = new Size(20, 20);
                    cell.Location = new Point(21*i,21*j);
                    cell.BorderStyle = BorderStyle.FixedSingle;                    
                    if (i < cm.Rows && j < cm.Columns)
                        cell.BackColor = cm[i, j];                    
                    cell.Click += delegate (object o, EventArgs e)
                      {
                          ((Control)o).BackColor = ColorInput.Value;
                      };   
                    Table.Controls.Add(cell);
                }
            }
            Table.ResumeLayout(true);
        }

        private void ColorWheel_Load(object sender, EventArgs e)
        {

        }

        private ColorMatrix getFromTable()
        {
            var cm = new ColorMatrix(rows,cols);
            cm.DefaultValueOnException = true;           
            foreach(Control c in Table.Controls)
            {
                var p = (Point)c.Tag;                
                cm[p.X, p.Y] = c.BackColor;
            }            
            return cm;
        }
    }
}
