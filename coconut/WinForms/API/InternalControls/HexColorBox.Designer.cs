using System.Drawing;

namespace CoconutSharp.WinForms.API.InternalControls
{
    using API.Types;
    partial class HexColorBox
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
            this.SuspendLayout();
            // 
            // RGBATextBoxcs
            //            
            this.Name = "RGBATextBox";
            this.Size = new System.Drawing.Size(133, 60);
            base.Multiline = false;
            this.RGBEncoding = RGBEncoding.ARGB;
            this.Value = Color.Transparent;
            this.ResumeLayout(false);

        }

        #endregion
    }
}
