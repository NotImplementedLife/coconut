using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoconutSharp.WinForms.API.Gestures
{
    public class MouseDragGesture : ControlGesture
    {
        public MouseDragGesture(Control Target) : base(Target) { }

        public override void Start()
        {
            base.Start();
            Target.MouseDown += MouseDown;
            Target.MouseMove += MouseMove;
            Target.MouseUp += MouseUp;
        }

        public override void Ready(bool succeed = true)
        {
            base.Ready(succeed);
        }

        public override void Stop()
        {
            base.Stop();
            Target.MouseDown -= MouseDown;
            Target.MouseMove -= MouseMove;
            Target.MouseUp -= MouseUp;
        }


        protected bool msLeft = false, msRight = false, msClick = true;
        public Point FirstPoint { get; protected internal set; }
        public Point SecondPoint { get; protected internal set; }
        protected virtual void MouseDown(object sender,MouseEventArgs e)
        {
            msLeft = (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left;
            msRight = (Control.MouseButtons & MouseButtons.Right) == MouseButtons.Right;
            msClick = msRight || msLeft;
            FirstPoint = SecondPoint = Target.PointToClient(Cursor.Position);
        }
        protected virtual void MouseMove(object sender,MouseEventArgs e)
        {
            if (!msClick) return;
            FirstPoint = SecondPoint;
            SecondPoint = Target.PointToClient(Cursor.Position);
        }
        protected virtual void MouseUp(object sender,MouseEventArgs e)
        {
            msLeft = (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left;
            msRight = (Control.MouseButtons & MouseButtons.Right) == MouseButtons.Right;
            msClick = msRight || msLeft;
        }       

    }
}
