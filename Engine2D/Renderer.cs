using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine2D
{
    public class Renderer
    {
        public float ScreenWidth;
        public float ScreenHeight;
        public void RenderCollisionBox(SpriteBatch sb, CollisionBox cb, Texture2D texture, bool disable3d, bool enablezscaling)
        {
            Vector3 centerclose = new Vector3(cb.Center.X, cb.Center.Y, cb.Center.Z - cb.Depth / 2.0f);
            Vector3 centerfar = new Vector3(cb.Center.X, cb.Center.Y, cb.Center.Z + cb.Depth / 2.0f);
            if (disable3d && enablezscaling)
            {
                //need to offset by half object width and height
                int scaledwidth = (int)(cb.Width * (100.0f - cb.Center.Z) / 100.0f);
                int scaledheight = (int)(cb.Height * (100.0f - cb.Center.Z) / 100.0f);
                sb.Draw(texture, new Rectangle((int)(centerclose.X + ScreenWidth / 2.0f - scaledwidth / 2.0f), (int)(centerclose.Y + ScreenHeight / 2.0f - scaledheight / 2.0f), scaledwidth, scaledheight), Color.White);
            }
            else if(disable3d == true)
            {
                //need to offset by half object width and height
                sb.Draw(texture, new Rectangle((int)(centerclose.X + ScreenWidth / 2.0f - cb.Width / 2.0f), (int)(centerclose.Y + ScreenHeight / 2.0f - cb.Height / 2.0f), (int)cb.Width, (int)cb.Height), Color.White);
            }
            else
            {
                sb.Draw(texture, new Rectangle((int)(centerclose.X + GetHorizontalOffset(centerclose) + ScreenWidth / 2.0f), (int)(ScreenHeight / 2.0f) - (int)(centerclose.Y + GetVerticalOffset(centerclose)), (int)cb.Width, (int)cb.Height), Color.White);
            }
        }

        //these offsets need to depend on the x value
        private float GetHorizontalOffset(Vector3 position)
        {
            //if z is + & x is +, offset is negative
            if (position.Z > 0 && position.X > 0)
                return -position.Z / 10.0f;
            if(position.Z > 0 && position.X < 0)
            {
                return position.Z / 10.0f;
            }
            if(position.Z < 0 && position.X > 0)
            {
                return -position.Z / 10.0f;
            }
            if (position.Z < 0 && position.X < 0)
            {
                return position.Z / 10.0f;
            }
            return 0.0f;
        }

        private float GetVerticalOffset(Vector3 position)
        {
            //if z is + & x is +, offset is negative
            return position.Z / 4.0f;
        }
    }
}
