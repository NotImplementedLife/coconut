namespace CoconutSharp.WinForms.Controls
{
    partial class ColorInput
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RField = new System.Windows.Forms.NumericUpDown();
            this.GField = new System.Windows.Forms.NumericUpDown();
            this.BField = new System.Windows.Forms.NumericUpDown();
            this.AField = new System.Windows.Forms.NumericUpDown();
            this.ColorDisplay = new CoconutSharp.WinForms.API.InternalControls.TransparencyEffectGrid();
            this.HexField = new CoconutSharp.WinForms.API.InternalControls.HexColorBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.RField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AField)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "R :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "G :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "B :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "A :";
            // 
            // RField
            // 
            this.RField.Location = new System.Drawing.Point(32, 3);
            this.RField.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RField.Name = "RField";
            this.RField.Size = new System.Drawing.Size(53, 20);
            this.RField.TabIndex = 4;
            // 
            // GField
            // 
            this.GField.Location = new System.Drawing.Point(32, 30);
            this.GField.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GField.Name = "GField";
            this.GField.Size = new System.Drawing.Size(53, 20);
            this.GField.TabIndex = 5;
            // 
            // BField
            // 
            this.BField.Location = new System.Drawing.Point(31, 59);
            this.BField.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BField.Name = "BField";
            this.BField.Size = new System.Drawing.Size(53, 20);
            this.BField.TabIndex = 6;
            // 
            // AField
            // 
            this.AField.Location = new System.Drawing.Point(32, 88);
            this.AField.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AField.Name = "AField";
            this.AField.Size = new System.Drawing.Size(53, 20);
            this.AField.TabIndex = 7;
            this.AField.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // ColorDisplay
            // 
            this.ColorDisplay.Location = new System.Drawing.Point(8, 3);
            this.ColorDisplay.Name = "ColorDisplay";
            this.ColorDisplay.Size = new System.Drawing.Size(100, 94);
            this.ColorDisplay.SquareSize = 10F;
            this.ColorDisplay.TabIndex = 9;
            // 
            // HexField
            // 
            this.HexField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HexField.Location = new System.Drawing.Point(8, 103);
            this.HexField.Name = "HexField";
            this.HexField.Size = new System.Drawing.Size(100, 20);
            this.HexField.TabIndex = 8;
            this.HexField.Text = "FF808080";
            this.HexField.Value = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.HexField.ValueChanged += new CoconutSharp.WinForms.API.InternalControls.HexColorBox.OnValueChanged(this.HexField_ValueChanged);
            this.HexField.RGBEncodingTypeChanged += new CoconutSharp.WinForms.API.InternalControls.HexColorBox.OnRGBEncodingTypeChanged(this.HexField_RGBEncodingTypeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ColorDisplay);
            this.panel1.Controls.Add(this.HexField);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(108, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 126);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.AField);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.BField);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.GField);
            this.panel2.Controls.Add(this.RField);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(102, 126);
            this.panel2.TabIndex = 11;
            // 
            // ColorInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ColorInput";
            this.Size = new System.Drawing.Size(219, 126);
            this.FontChanged += new System.EventHandler(this.ColorInput_FontChanged);
            ((System.ComponentModel.ISupportInitialize)(this.RField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AField)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown RField;
        private System.Windows.Forms.NumericUpDown GField;
        private System.Windows.Forms.NumericUpDown BField;
        private System.Windows.Forms.NumericUpDown AField;
        private API.InternalControls.HexColorBox rgbaTextBox1;
        private API.InternalControls.HexColorBox HexField;
        private API.InternalControls.TransparencyEffectGrid ColorDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
