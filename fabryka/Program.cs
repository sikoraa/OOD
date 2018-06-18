using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualReality
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var code = Console.ReadLine();
                if (code == "exit")
                    return;
                (HeadMountedDisplay HMD, HandController LeftHandController, HandController RightHandController, Tracker LeftFootTracker, Tracker RightFootTracker) parts = VRFactory.GetParts(code);
                VRPlayer player = BuildVRPlayer(parts.HMD, parts.LeftHandController, parts.RightHandController, parts.LeftFootTracker, parts.RightFootTracker);
                TestVRPlayer(player);
            }
        }

        static VRPlayer BuildVRPlayer(HeadMountedDisplay HMD, 
            HandController LeftHandController, HandController RightHandController, 
            Tracker LeftFootTracker, Tracker RightFootTracker)
        {
            return new VRPlayer(HMD, 
                LeftHandController, RightHandController, 
                LeftFootTracker, RightFootTracker);
        }

        static void TestVRPlayer(VRPlayer VRPlayer)
        {
            VRPlayer.HMD.Render();

            VRPlayer.LeftHandController.Grab();
            VRPlayer.RightHandController.Grab();

            try
            {
                VRPlayer.LeftHandController.IsHeld();
                VRPlayer.RightHandController.IsHeld();
            }
            catch(NotImplementedException e)
            {
                Console.WriteLine("IsHeld() not implemented");
            }
            VRPlayer.LeftHandController.Vibrate();
            VRPlayer.RightHandController.Vibrate();

            try
            {
                VRPlayer.LeftFootTracker.StartTracking();
                VRPlayer.RightFootTracker.StartTracking();
            }
            catch(NotImplementedException e)
            {
                Console.WriteLine("StartTracking() not implemented");
            }
        }
    }
}
