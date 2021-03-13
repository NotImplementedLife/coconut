using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoconutSharp.WinForms.API.Types
{
    public class RGBConverter
    {        
        public static Color Convert(string cl,RGBEncoding enc)
        {
            int k = 0;
            List<byte> O=new List<byte>();               

            for(int i=0;i<cl.Length;i++)
            {
                if (k >= 8)
                    throw new ArgumentSizeException($"{ cl } is too long to be a valid RGB color.");
                if (
                    ('0' <= cl[i] && cl[i] <= '9') ||
                    ('a' <= cl[i] && cl[i] <= 'f') ||
                    ('A' <= cl[i] && cl[i] <= 'F'))
                {
                    if (k % 2 == 0) O.Add(0);
                    byte d = (char.IsDigit(cl[i])) ?
                        (byte)(cl[i] - '0') :
                        (byte)(char.ToUpper(cl[i]) - 'A' + 10);
                    O[k >> 1] <<= 4;
                    O[k >> 1] += d;
                    if (((i & 1) == 0) && (i == cl.Length - 1)) O[k >> 1] <<= 4;
                    k++;
                }
                else throw new ArgumentException($"{cl} is not a valid {enc} color.");
            }

            int e = (int)enc;
            List<byte> L=new List<byte>();            
            while(e>0)
            {
                L.Add((byte)(e%10));
                e /= 10;                
            }
            L.Reverse();
            byte[] l = L.ToArray();               
            byte[] o = O.ToArray();

            foreach(var i in l) Console.Write(i+" ");
            Console.WriteLine();
            foreach (var i in o) Console.Write(i+" ");
            Console.WriteLine();
            if (l.Length == 3 && o.Length == 4)
                throw new RGBAToRGBInvalidConversionException($"{cl} is too long to be a valid {enc.ToString()} color.");

            byte r = 0, g = 0, b = 0, a = 255;
            for (int i = 0; i < Math.Min(o.Length,l.Length); i++)
            {               
                switch (l[i])
                {
                    case 1: r = o[i]; break;
                    case 2: g = o[i]; break;
                    case 3: b = o[i]; break;
                    case 4: a = o[i]; break;
                }
            }            
            return Color.FromArgb(a,r,g,b);
        }
        public static string Convert(string cl,RGBEncoding oldEnc,RGBEncoding newEnc)
        {
            Color color = Convert(cl, oldEnc);
            string name = color.Name;
            string res = "";

            int e = (int)newEnc;
            List<byte> L = new List<byte>();
            while (e > 0)
            {
                L.Add((byte)(e % 10));
                e /= 10;
            }
            L.Reverse();
            byte[] l = L.ToArray();           
            for(int i=0;i<l.Length;i++)
                switch(l[i])
                {
                    case 1:res = res + color.R.ToString(); break;
                    case 2:res = res + color.G.ToString(); break;
                    case 3:res = res + color.B.ToString(); break;
                    case 4:res = res + color.A.ToString(); break;
                }
            return res;
        }        

        public static string Convert(Color Color,RGBEncoding enc)
        {
            string name = Color.Name;
            string[] s = new string[4]
                {
                    Color.R.ToString("X2"),
                    Color.G.ToString("X2"),
                    Color.B.ToString("X2"),
                    Color.A.ToString("X2")

                };
            int e = (int)enc;
            List<byte> L = new List<byte>();
            while (e > 0)
            {
                L.Add((byte)(e % 10));
                e /= 10;
            }
            L.Reverse();
            byte[] l = L.ToArray();
            string result = "";
            for(int i=0;i<l.Length;i++)
            {
                result += s[l[i]-1];
            }
            return result;
        }
    }

    public class ArgumentSizeException : ArgumentException
    {
        public ArgumentSizeException(string msg) : base(msg) { }
    }

    public class RGBAToRGBInvalidConversionException : ArgumentException
    {
        public RGBAToRGBInvalidConversionException(string msg) : base(msg) { }
    }
}
