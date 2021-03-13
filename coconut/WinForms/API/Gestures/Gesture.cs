using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoconutSharp.WinForms.API.Gestures
{
    public abstract class Gesture
    {    
        public bool Active { get; private set; } = false;
        public virtual void Start()
        {
            Active = true;
        }
        public virtual void Ready(bool succeed = true) { }
        public virtual void Stop()
        {
            Active = false;
        }
    }
}
