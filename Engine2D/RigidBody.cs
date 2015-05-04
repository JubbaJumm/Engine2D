using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine2D
{
    public class RigidBody
    {
        public CollisionBox Box;
        public Vector3 Velocity;
        public Vector3 Acceleration;
        public float Mass;
    }
}
