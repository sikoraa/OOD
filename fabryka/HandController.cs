using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualReality
{
    enum HandednessType
    {
        Left, Right
    }

    abstract class HandController
    {
        protected HandednessType Handedness;

        public HandController(HandednessType Handedness)
        {
            this.Handedness = Handedness;
        }

        abstract public void Grab();

        abstract public bool IsHeld();

        abstract public void Vibrate();
    }

    class RealHandController : HandController
    {
        string type;
        string name;
        int minVibrate, maxVibrate;
        Random rng;
        

        public RealHandController(string s, HandednessType h, int min, int max, string name_) : base(h)
        {
            type = s;
            rng = new Random();
            minVibrate = min;
            maxVibrate = max;
            name = name_;
        }

        public override void Grab()
        {
            string text = Handedness.ToString() + " " + name + " grabbing object";
            Console.WriteLine(text);
        }
        public override bool IsHeld()
        {
            if (type == "vive")
                throw new NotImplementedException();
            int holding = rng.Next(2);
            bool holding_ = holding > 0 ? true : false;
            string text;
            text = Handedness.ToString() + " " + name + " is "; ;
            if (!holding_)
            {
                text += "not ";
            }
            text += "held";
            Console.WriteLine(text);
            
            return holding_;
        }

        public override void Vibrate()
        {
            int p = rng.Next(minVibrate, maxVibrate);
            string text = Handedness.ToString() + " " + name + " vibrating for " + p.ToString() + "[s]";
            Console.WriteLine(text);
            Thread.Sleep(p * 1000);
            //throw new NotImplementedException();
        }
    }
}
