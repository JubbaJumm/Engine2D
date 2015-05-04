using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine2D
{
    public enum LightingFalloff
    {
        Linear,
        InverseSquare
    }

    public enum PlanePosition
    {
        Up,
        Down,
        Right,
        Left,
        Back,
        Front
    }

    public class Light
    {
        public Vector3 Position;
        public float Intensity = 0.6f;
        public Color Color = Color.White;
        public LightingFalloff FallOff = LightingFalloff.InverseSquare;
        //public int Rays = 500;
        //circular shape

        public Vector3[][] CalculateNormals(TextureArray normals)
        {
            Vector3[][] vnormals = new Vector3[normals.Height][];
            for (int y = 0; y < normals.Height; y++)
            {
                vnormals[y] = new Vector3[normals.Width];
                for (int x = 0; x < normals.Width; x++)
                {
                    Color c = Math2.UIntToColor(normals.Array[y][x]);
                    //all vectors start from the origin, (0, 0, 0) however that is not the center of the texture, that is the top left hand corner
                    Vector3 vec = new Vector3((c.R - 127.0f) / 127.0f, (c.G - 127.0f) / 127.0f, (c.B - 127.0f) / 127.0f);
                    vec.Normalize();
                    vnormals[y][x] = vec;
                }
            }
            return vnormals;
        }

        public TextureArray IlluminateTextureUsingNormalMap(TextureArray texture, Vector3[][] normals, float depth)
        {
            TextureArray afterlighting = texture;
            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    Color texturec = Math2.UIntToColor(texture.Array[y][x]);
                    //all vectors start from the origin, (0, 0, 0) however that is not the center of the texture, that is the top left hand corner
                    Vector3 vec = normals[y][x];
                    Vector3 center = new Vector3(texture.Width / 2, texture.Height / 2, depth);
                    Vector3 tolight = new Vector3((Position.X - center.X) - (x - center.X), (Position.Y - center.Y) - (y - center.Y), Position.Z - depth);
                    tolight.Normalize();
                    float intensity = (vec.X * tolight.X + vec.Y * tolight.Y + vec.Z * tolight.Z);
                    float tempi = (intensity + 1.0f) / 2.0f;
                    //Color newcolor = Color.FromNonPremultiplied((int)(intensity * texturec.R), (int)(intensity * texturec.G), (int)(intensity * texturec.B), 255);
                    Color newcolor = Color.FromNonPremultiplied((int)(tempi * texturec.R), (int)(tempi * texturec.G), (int)(tempi * texturec.B), 255);
                    //Color newcolor = Color.FromNonPremultiplied((int)(tempi * 255), (int)(tempi * 255), (int)(tempi * 255), 255);
                    afterlighting.Array[y][x] = Math2.ColorToUInt(newcolor);
                }
            }
            return afterlighting;
        }
    }

    public class Lighting
    {
        public static uint[][] ApplyLighting(CollisionBox plane, uint[][] texture, int width, int height, Light light, PlanePosition pp)
        {
            float x;
            float y;
            float z;
            if(pp == PlanePosition.Up || pp == PlanePosition.Down)
            {
                x = light.Position.X;
                y = plane.Center.Y;
                z = light.Position.Z;
                //need to add checks to see if the light can hit the object
                //maybe need to add a plane class with its own rasterizing system instead of using collisionbox, it will make it easier
            }
            else if(pp == PlanePosition.Right || pp == PlanePosition.Left)
            {
                x = plane.Center.X;
                y = light.Position.Y;
                z = light.Position.Z;
            }
            else if (pp == PlanePosition.Back || pp == PlanePosition.Front)
            {
                x = light.Position.X;
                y = light.Position.Y;
                z = plane.Center.Z;
            }
            return null;
            //might be easier to create a texture of the light effect then insert it at the light position on the plane instead of tracing each pixel of the plane
        }

        public static Func<float, float> DistanceToStrengthFunction(Light light)
        {
            if(light.FallOff == LightingFalloff.Linear)
            {
                return new Func<float, float>((float distance) => 
                {
                    float result = 1.0f / (light.Intensity * -100.0f * distance) + 1;
                    if (result < 0)
                    {
                        result = 0.0f;
                    }
                    return result;
                });
            }
            if(light.FallOff == LightingFalloff.InverseSquare)
            {
                return new Func<float, float>((float distance) =>
                {
                    return 1.0f / ((float)Math.Pow((1.0f / light.Intensity) * distance, 2.0f) + 1.0f);
                });
            }
            return null; // should never reach here
        }

        //width and height is for the 
        public static uint[][] DrawCircle(uint[][] texture, int x, int y, int radius, int width, int height, Color c)
        {
            for (int yc = -radius; yc <= radius; yc++)
            {
                for (int xc = -radius; xc <= radius; xc++)
                {
                    float distance = (float)Math.Sqrt(xc * xc + yc * yc);
                    if (xc * xc + yc * yc <= radius * radius * 0.8f)
                    {
                        int alpha = 255 - (int)Math.Floor(255 * distance / radius);
                        if (y + yc > height || y + yc <= 0 || x + xc > width || x + xc <= 0)
                        { }
                        else
                        {
                            texture[y + yc - 1][x + xc - 1] = Math2.ColorToUInt(Color.FromNonPremultiplied(c.R, c.G, c.B, alpha));
                        }
                        //catching exceptions is way too fucking slow, at 120 radius its like 2 seconds then at 140 radius its 6 seconds
                        //check if current y or x is out of range (width/height)
                        //texture[y + yc][x + xc] = 0x00000000;
                    }
                }
            }
            return texture;
        }
    }
}
