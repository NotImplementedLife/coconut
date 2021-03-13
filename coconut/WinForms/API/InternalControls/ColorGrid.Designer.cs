namespace CoconutSharp.WinForms.API.InternalControls
{
    partial class ColorGrid
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
            this.TransparencyGrid = new CoconutSharp.WinForms.API.InternalControls.TransparencyEffectGrid();
            this.Canvas = new CoconutSharp.WinForms.API.InternalControls.TransparentPanel();
            this.Frame.SuspendLayout();
            this.SuspendLayout();
            // 
            // Frame
            // 
            this.Frame.Controls.Add(this.Canvas);
            this.Frame.Controls.Add(this.TransparencyGrid);
            this.Frame.Location = new System.Drawing.Point(90, 66);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(131, 116);
            this.Frame.TabIndex = 2;
            // 
            // TransparencyGrid
            // 
            this.TransparencyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransparencyGrid.Location = new System.Drawing.Point(0, 0);
            this.TransparencyGrid.Name = "TransparencyGrid";
            this.TransparencyGrid.Size = new System.Drawing.Size(131, 116);
            this.TransparencyGrid.SquareSize = 10F;
            this.TransparencyGrid.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(131, 116);
            this.Canvas.TabIndex = 1;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.Resize += new System.EventHandler(this.Canvas_Resize);
            // 
            // ColorGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Frame);
            this.Name = "ColorGrid";
            this.Size = new System.Drawing.Size(309, 260);
            this.Resize += new System.EventHandler(this.ColorGrid_Resize);
            this.Frame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TransparencyEffectGrid TransparencyGrid;
        private TransparentPanel Canvas;
        private System.Windows.Forms.Panel Frame;
    }
}
