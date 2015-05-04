using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Engine2D
{
    public class TextureGeneration
    {
        public static Texture2D Generate(GraphicsDevice gd, Shader shader, int width, int height)
        {
            uint[] texturedata = new uint[width * height];
            texturedata = shader.ApplyShader(texturedata);
            Texture2D newtexture = new Texture2D(gd, width, height);
            newtexture.SetData<uint>(texturedata);
            return newtexture;
        }
    }
}
