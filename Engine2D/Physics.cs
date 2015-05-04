using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine2D
{
    public class Physics
    {
        public float GravitationalFieldStrength = 9.81f;
        public float AirDampeningFactor = 0.02f;//from 0 to 1

        public static void Update(GameObject go)
        {
            go.RigidBody.Box.Center += go.RigidBody.Velocity;
        }
    }
}
