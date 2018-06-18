using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualReality
{
    abstract class Tracker
    {
        public abstract void StartTracking();
    }

    class RealTracker : Tracker
    {
        string type;
        string name;

        public RealTracker(string type_, string name_)
        {
            type = type_;
            name = name_;
        }

        public override void StartTracking()
        {
            if (type == "oculus")
                throw new NotImplementedException();
            string text = name + " start tracking";
            Console.WriteLine(text);
        }
    }
}
