using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine2D
{
    public class Particle
    {
        public bool CanCollide = false;
        public Vector3 Velocity;
        public Texture2D Texture;
        public int TTL;
    }
}
