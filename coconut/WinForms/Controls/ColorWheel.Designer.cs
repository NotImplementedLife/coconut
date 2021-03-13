namespace CoconutSharp.WinForms.Controls
{
    partial class ColorWheel
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
            this.Frame = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Frame
            // 
            this.Frame.Location = new System.Drawing.Point(0, 0);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(150, 150);
            this.Frame.TabIndex = 0;
            this.Frame.Paint += new System.Windows.Forms.PaintEventHandler(this.Frame_Paint);
            this.Frame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frame_MouseDown);
            this.Frame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frame_MouseMove);
            this.Frame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frame_MouseUp);
            // 
            // ColorWheel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Frame);
            this.Name = "ColorWheel";
            this.DockChanged += new System.EventHandler(this.ColorWheel_DockChanged);
            this.Resize += new System.EventHandler(this.ColorPicker_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Frame;
    }
}
