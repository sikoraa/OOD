using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualReality
{
    class VRPlayer
    {
        private HeadMountedDisplay hmd;
        private HandController leftHandController;
        private HandController rightHandController;
        private Tracker leftFootTracker;
        private Tracker rightFootTracker;

        public HeadMountedDisplay HMD { get { return hmd; } }
        public HandController LeftHandController { get { return leftHandController; } }
        public HandController RightHandController { get { return rightHandController; } }
        public Tracker LeftFootTracker { get { return leftFootTracker; } }
        public Tracker RightFootTracker { get { return rightFootTracker; } }

        public VRPlayer(HeadMountedDisplay HMD, 
            HandController LeftHandController, HandController RightHandController, 
            Tracker LeftFootTracker,
            Tracker RightFootTracker)
        {
            this.hmd = HMD;
            this.leftHandController = LeftHandController;
            this.rightHandController = RightHandController;
            this.leftFootTracker = LeftFootTracker;
            this.rightFootTracker = RightFootTracker;
        }
    }
}
