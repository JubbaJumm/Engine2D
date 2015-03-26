using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine2D
{
    public enum OffsetDirection
    {
        X,
        Y
    }
    public class Animator
    {
        public Texture2D AnimationSheet;
        public int SpriteWidth;
        public int SpriteHeight;
        public int SpriteSheetOffset;
        public OffsetDirection OffsetDirection = OffsetDirection.X;
        private int spritecount;

        public Animator(Texture2D spritesheet, int spritewidth, int spriteheight, int spriteoffset, OffsetDirection direction = Engine2D.OffsetDirection.X)
        {
            AnimationSheet = spritesheet;
            SpriteWidth = spritewidth;
            SpriteHeight = spriteheight;
            SpriteSheetOffset = spriteoffset;

            spritecount = (OffsetDirection == OffsetDirection.X) ? (int)(AnimationSheet.Width / SpriteSheetOffset) : (int)(AnimationSheet.Height / SpriteSheetOffset);
        }
    }
}
