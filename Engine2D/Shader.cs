using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine2D
{
    public enum ShaderType
    {
        Diffuse
    }

    public class Shader
    {
        public ShaderType Type;
        //draw texture, then draw shader texture ontop (shader will be an alpha texture)

        public uint[] ApplyShader(uint[] texturedata)
        {
            return null;
        }
    }
}
