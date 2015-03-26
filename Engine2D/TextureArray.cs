using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine2D
{
    public struct TextureArray
    {
        public uint[][] Array;
        public int Width;
        public int Height;

        public void ResetTexture()
        {
            for (int y = 0; y < Height; y++)
            {
                Array[y] = new uint[Width];
                for (int x = 0; x < Width; x++)
                {
                    Array[y][x] = 0x00000000;
                }
            }
        }

        public Texture2D ToTexture(GraphicsDevice gd)
        {
            Texture2D temp = new Texture2D(gd, Width, Height);
            temp.SetData(TextureArray.Uint2DArraytoSingle(Array, Width, Height));
            return temp;
        }

        public static TextureArray FromTexture(Texture2D texture, int width, int height)
        {
            TextureArray ta = new TextureArray();
            ta.Width = width;
            ta.Height = height;
            uint[] temp = new uint[width * height];
            texture.GetData<uint>(temp);
            ta.Array = new uint[height][];
            for (int y = 0; y < height; y++)
            {
                ta.Array[y] = new uint[width];
            }
            ta.Array = TextureArray.UintArrayto2D(temp, width, height);
            return ta;
        }









        public static uint[] Uint2DArraytoSingle(uint[][] array, int width, int height)
        {
            uint[] data = new uint[width * height];
            int counter = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    data[counter] = array[y][x];
                    counter++;
                }
            }
            return data;
        }

        public static uint[][] UintArrayto2D(uint[] array, int width, int height)
        {
            uint[][] data = new uint[height][];
            int counter = 0;
            for (int y = 0; y < height; y++)
            {
                data[y] = new uint[width];
                for (int x = 0; x < width; x++)
                {
                    data[y][x] = array[counter];
                    counter++;
                }
            }
            return data;
        }

        public static uint[][] AddTextures(uint[][] first, uint[][] second, int width, int height)
        {
            uint[][] result = new uint[height][];
            for (int y = 0; y < height; y++)
            {
                result[y] = new uint[width];
                for (int x = 0; x < width; x++)
                {
                    uint add = first[y][x] + second[y][x];
                    if (add > 0xFFFFFFFF)
                        add = 0xFFFFFFFF;
                    result[y][x] = add;
                }
            }
            return result;
        }

        public static uint[][] MultiplyTextures(uint[][] first, uint[][] second, int width, int height)
        {
            uint[][] result = new uint[height][];
            for (int y = 0; y < height; y++)
            {
                result[y] = new uint[width];
                for (int x = 0; x < width; x++)
                {
                    uint add = first[y][x] * second[y][x];
                    if (add > 0xFFFFFFFF)
                        add = 0xFFFFFFFF;
                    result[y][x] = add;
                }
            }
            return result;
        }
    }
}
