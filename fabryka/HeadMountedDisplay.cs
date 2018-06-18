using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualReality
{
    abstract class HeadMountedDisplay
    {
        public abstract void Render();
    }

    class RealHeadMountedDispaly : HeadMountedDisplay
    {
        string type;
        string name;
        int refreshRate;

        public RealHeadMountedDispaly(string type_, string name_, int refreshRate_)
        {
            type = type_;
            name = name_;
            refreshRate = refreshRate_;
        }
        public override void Render()
        {
            string text = name + " render refresh rate: " + refreshRate.ToString() + "[Hz]";
            Console.WriteLine(text);
        }
    }
}
