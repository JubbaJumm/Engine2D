using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine2D
{
    public class ParticleSystem
    {
        private Random r;
        private Particle _particle;

        public ParticleSystem(Particle p)
        {
            _particle = p;
            r = new Random();
        }

        //if maxX = 5, local velocity can be anything between -5 and +5 in the x direction
        public Vector3 GenerateRandomVelocity(float maxxdeviation, float maxydeviation, float maxzdeviation)
        {
            float x = (((float)r.NextDouble() * 2.0f) - 1.0f) * maxxdeviation;
            float y = (((float)r.NextDouble() * 2.0f) - 1.0f) * maxydeviation;
            float z = (((float)r.NextDouble() * 2.0f) - 1.0f) * maxzdeviation;
            return new Vector3(x, y, z);
        }

        public Vector3 GenerateRandomVelocity(float minx, float maxx, float miny, float maxy, float minz, float maxz)
        {
            //if maxx = 2.0f, its impossible to reach 2.0f but it can get to 1.999f
            float x = r.Next((int)(minx * 1000.0f), (int)((maxx * 1000.0f) + 1.0f)) / 1000.0f;
            float y = r.Next((int)(miny * 1000.0f), (int)((maxy * 1000.0f) + 1.0f)) / 1000.0f;
            float z = r.Next((int)(minz * 1000.0f), (int)((maxz * 1000.0f) + 1.0f)) / 1000.0f;
            return new Vector3(x, y, z);
        }

        public float GenerateRandomTTL(float minttl, float maxttl)
        {
            return r.Next((int)(minttl * 1000.0f), (int)((maxttl * 1000.0f) + 1.0f)) / 1000.0f;
        }
    }
}
