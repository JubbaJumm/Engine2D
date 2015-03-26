using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine2D
{
    public class Math2
    {
        public static float DistanceBetween(Vector3 one, Vector3 two)
        {
            return (float)Math.Sqrt(Math.Pow(two.X - one.X, 2.0f) + Math.Pow(two.Y - one.Y, 2.0f) + Math.Pow(two.Z - one.Z, 2.0f));
        }        

        public static uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }

        public static Color UIntToColor(uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromNonPremultiplied(r, g, b, a);
        }

        public Vector3 GetVector(Vector3 v1, Vector3 v2)
        {
            return v2 - v1;
        }
    }
}
