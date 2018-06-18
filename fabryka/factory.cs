using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualReality
{
    static class VRFactory
    {
        static Dictionary<string, Factory> factories;
        static VRFactory()
        {
            factories = new Dictionary<string, Factory>();
            factories.Add("vive", new ViveFactory());
            factories.Add("oculus", new OculusFactory());
            factories.Add("mix1", new mix1Factory());
            factories.Add("mix2", new mix2Factory());

        }
        public static (HeadMountedDisplay, HandController, HandController, Tracker , Tracker) GetParts(string s)
        {
            HeadMountedDisplay HMD = null;
            HandController LeftHandController = null;
            HandController RightHandController = null;
            Tracker LeftFootTracker = null;
            Tracker RightFootTracker = null;
            if (!factories.ContainsKey(s)) throw new ArgumentException();
            Factory f;
            factories.TryGetValue(s, out f);
            HMD = f.getHeadMountedDisplay();
            LeftHandController = f.getHandController("left");
            RightHandController = f.getHandController("right");
            LeftFootTracker = f.getTracker();
            RightFootTracker = f.getTracker();


            return (HMD, LeftHandController, RightHandController, LeftFootTracker, RightFootTracker);
        }
    }

    abstract class Factory
    {
        public abstract HeadMountedDisplay getHeadMountedDisplay();
        public abstract HandController getHandController(string s);
        public abstract Tracker getTracker();
    }

    class ViveFactory : Factory
    {
        public override HeadMountedDisplay getHeadMountedDisplay()
        {
            return new RealHeadMountedDispaly("vive", "HTC Vive", 90);
        }
        public override HandController getHandController(string s)
        {
            if (s == "left")
                return new RealHandController("vive", HandednessType.Left, 2, 4, "HTC Vive");
            else if (s == "right")
                return new RealHandController("vive", HandednessType.Right, 2, 4, "HTC Vive");
            return null;                
        }
        public override Tracker getTracker()
        {
            return new RealTracker("vive", "HTC Vive Tracker");
        }
    }

    class OculusFactory : Factory
    {
        public override HeadMountedDisplay getHeadMountedDisplay()
        {
            return new RealHeadMountedDispaly("oculus", "Oculus", 120);
        }
        public override HandController getHandController(string s)
        {
            if (s == "left")
                return new RealHandController("oculus", HandednessType.Left, 1, 5, "Oculus Touch");
            else if (s == "right")
                return new RealHandController("oculus", HandednessType.Right, 1, 5, "Oculus Touch");
            return null;
        }
        public override Tracker getTracker()
        {
            return new RealTracker("oculus", "Oculus Tracker");
        }
    }

    class mix1Factory : Factory
    {
        public override HeadMountedDisplay getHeadMountedDisplay()
        {
            return new RealHeadMountedDispaly("vive", "HTC Vive", 90);
        }
        public override HandController getHandController(string s)
        {
            if (s == "left")
                return new RealHandController("vive", HandednessType.Left, 2, 4, "HTC Vive");
            else if (s == "right")
                return new RealHandController("vive", HandednessType.Right, 2, 4, "HTC Vive");
            return null;
        }
        public override Tracker getTracker()
        {
            return new RealTracker("oculus", "Oculus Tracker");
        }
    }

    class mix2Factory : Factory
    {
        public override HeadMountedDisplay getHeadMountedDisplay()
        {
            return new RealHeadMountedDispaly("oculus", "Oculus", 120);
        }
        public override HandController getHandController(string s)
        {
            if (s == "left")
                return new RealHandController("oculus", HandednessType.Left, 1, 5, "Oculus Touch");
            else if (s == "right")
                return new RealHandController("vive", HandednessType.Right, 2, 4, "HTC Vive");
            return null;
        }
        public override Tracker getTracker()
        {
            return new RealTracker("oculus", "Oculus Tracker");
        }
    }


}
