using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Storage
    {
        private static Storage instance;
        public float Angle { get; set; }
        public float ActualCannonAngle { get; set; }
        public Vector2 Target { get; set; }

        private Storage() {}

        public static Storage Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Storage();
                }
                return instance;
            }
        }
    }
}
