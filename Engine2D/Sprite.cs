using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine2D
{
    public class Sprite
    {
        public Texture2D Texture;
        public Texture2D NormalMap;
        private Effect shader;
        private RenderTarget2D target;
        private RenderTarget2D normaltarget;
        private RenderTarget2D complete;
        private GraphicsDevice _gd;
        private int _width;
        private int _height;

        public Sprite(Texture2D texture, Texture2D normals, int width, int height, Effect lightshader, GraphicsDevice gd)
        {
            Texture = texture;
            NormalMap = normals;
            shader = lightshader;
            target = new RenderTarget2D(
                gd,
                width,
                height);
            normaltarget = new RenderTarget2D(
                gd,
                width,
                height);
            complete = new RenderTarget2D(
                gd,
                width,
                height);
            _gd = gd;
            _width = width;
            _height = height;
        }

        public void Draw(SpriteBatch sb, Light l)
        {
            _gd.SetRenderTarget(target);
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp,  null, null);
            sb.Draw(Texture, Vector2.Zero, new Rectangle(0, 0, _width, _height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            sb.End();

            _gd.SetRenderTarget(normaltarget);
            _gd.Clear(new Color(127, 127, 255));


            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, null, null);
            sb.Draw(NormalMap, Vector2.Zero, new Rectangle(0, 0, _width, _height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            sb.End();

            _gd.SetRenderTarget(null);

            Vector4 lightColor = new Vector4(1.0f, 1.0f, 1f, 0);

            shader.Parameters["xNormalMap"].SetValue((Texture2D)normaltarget);
            //shader.Parameters["xSpecularMap"].SetValue((Texture2D)specularRenderTarget);
            shader.Parameters["xLightPosition"].SetValue(l.Position);
            shader.Parameters["xLightColor"].SetValue(lightColor);
            shader.Parameters["xWidth"].SetValue(_width);
            shader.Parameters["xHeight"].SetValue(_height);

            sb.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.LinearClamp, null, null, shader);
            sb.Draw((Texture2D)target, new Rectangle(0, 0, _width, _height), Color.White);
            sb.End();
        }
    }
}
