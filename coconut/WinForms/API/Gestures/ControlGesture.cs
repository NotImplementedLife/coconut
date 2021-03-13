using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoconutSharp.WinForms.API.Gestures
{
    public class ControlGesture : Gesture
    {
        public ControlGesture() : this(null) { }
        public ControlGesture(Control Target)
        {
            this.Target = Target;
        }

        public override void Start()
        {
            if (Target == null) return;
            base.Start();            
            oldCursor = Target.Cursor;
            Target.Cursor = Cursor;
        }        
        public override void Stop()
        {
            base.Stop();
            if (Target == null) return;
            Target.Cursor = oldCursor;            
        }

        public Control Target { get; protected internal set; } = null;
        protected Cursor oldCursor = Cursors.Default;
        public Cursor Cursor { get; set; } = Cursors.Default;
    }
}
