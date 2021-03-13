using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoconutSharp
{    
    public static class AssemblyInfo
    {
        public static readonly string Version =
            Assembly.GetExecutingAssembly().GetName().Version.ToString();     
    }

}
