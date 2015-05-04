using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine2D
{
    public enum CollisionSide
    {
        Top,
        Bottom,
        Right,
        Left,
        FarBack,
        Close,
        Other
    }

    public struct CollisionSize
    {
        public float XMax;
        public float XMin;
        public float YMax;
        public float YMin;
        public float ZMax;
        public float ZMin;

        public static CollisionSize BoxToSize(CollisionBox box)
        {
            CollisionSize cs = new CollisionSize();
            cs.XMax = box.Center.X + box.Width / 2.0f;
            cs.XMin = box.Center.X - box.Width / 2.0f;
            cs.YMax = box.Center.Y + box.Height / 2.0f;
            cs.YMin = box.Center.Y - box.Height / 2.0f;
            cs.ZMax = box.Center.Z + box.Depth / 2.0f;
            cs.ZMin = box.Center.Z - box.Depth / 2.0f;
            return cs;
        }
    }

    public class CollisionBox
    {
        private float _width;
        private float _halfwidth;
        public float Width
        {
            get { return _width; }
            set { _width = value; _halfwidth = value / 2.0f; }
        }

        private float _height;
        private float _halfheight;
        public float Height
        {
            get { return _height; }
            set { _height = value; _halfheight = value / 2.0f; }
        }

        private float _depth;
        private float _halfdepth;
        public float Depth
        {
            get { return _depth; }
            set { _depth = value; _halfdepth = value / 2.0f; }
        }
        public Vector3 Center;
        private CollisionSize _cs;

        public bool CheckPointCollision(Vector3 point)
        {
            bool xcollided = (point.X <= _halfwidth + Center.X && point.X >= Center.X - _halfwidth) ? true : false;
            bool ycollided = (point.Y <= _halfheight + Center.Y && point.Y >= Center.Y - _halfheight) ? true : false;
            bool zcollided = (point.Z <= _halfdepth + Center.Z && point.Z >= Center.Z - _halfdepth) ? true : false;
            return xcollided && ycollided && zcollided;
        }
        /*
         *       Y+|   /Z+
         *         |  /
         *         | /
         *         |/
         *X- ------|--------X+
         *        /|
         *       / |
         *   Z- /  |Y-
        */ 

        public CollisionSide CheckCollisionSide(Vector3 point)
        {
            if(point.Y == Center.Y + _halfheight)
            {
                return CollisionSide.Top;
            }
            if (point.Y == Center.Y - _halfheight)
            {
                return CollisionSide.Bottom;
            }
            if (point.Z < Center.Z + _halfdepth && point.Z > Center.Z - _halfdepth)
            {
                if (point.X > Center.X)
                {
                    return CollisionSide.Right;
                }
                if (point.X <= Center.X)
                {
                    return CollisionSide.Left;
                }
            }
            if(point.Z == Center.Z + _halfdepth)
            {
                return CollisionSide.FarBack;
            }
            if(point.Z == Center.Z - _halfdepth)
            {
                return CollisionSide.Close;
            }
            return CollisionSide.Left;
        }

        public void ReadyForCollisionChecks()
        {
            _cs = CollisionSize.BoxToSize(this);
        }

        //returns collision side of the parent object, not other
        public CollisionSide CheckCollisionWithOther(CollisionBox other, out bool collision)
        {
            collision = false;
            //right of this, left of other
            if(Center.X < other.Center.X &&_cs.XMax >= other._cs.XMin && !(_cs.YMin >= other._cs.YMax || _cs.YMax <= other._cs.YMin) && !(_cs.ZMin >= other._cs.ZMax || _cs.ZMax <= other._cs.ZMin))
            {
                collision = true;
                return CollisionSide.Right;
            }
            //left
            if (Center.X > other.Center.X && _cs.XMin <= other._cs.XMax && !(_cs.YMin >= other._cs.YMax || _cs.YMax <= other._cs.YMin) && !(_cs.ZMin >= other._cs.ZMax || _cs.ZMax <= other._cs.ZMin))
            {
                collision = true;
                return CollisionSide.Left;
            }
            //top // this check should be bottom of object as ymin clips with ymax
            if(Center.Y > other.Center.Y && _cs.YMin <= other._cs.YMax && !(_cs.XMin >= other._cs.XMax || _cs.XMax <= other._cs.XMin) && !(_cs.ZMin >= other._cs.ZMax || _cs.ZMax <= other._cs.ZMin))
            {
                collision = true;
                return CollisionSide.Top;
            }
            //this checks for top but for some reason it returns bottom
            if (Center.Y < other.Center.Y && _cs.YMax >= other._cs.YMin && !(_cs.XMin >= other._cs.XMax || _cs.XMax <= other._cs.XMin) && !(_cs.ZMin >= other._cs.ZMax || _cs.ZMax <= other._cs.ZMin))
            {
                collision = true;
                return CollisionSide.Bottom;
            }

            if (Center.Z > other.Center.Z && _cs.ZMin <= other._cs.ZMax && !(_cs.XMin >= other._cs.XMax || _cs.XMax <= other._cs.XMin) && !(_cs.YMin >= other._cs.YMax || _cs.YMax <= other._cs.YMin))
            {
                collision = true;
                return CollisionSide.Close;
            }
            if (Center.Z < other.Center.Z && _cs.ZMax >= other._cs.ZMin && !(_cs.XMin >= other._cs.XMax || _cs.XMax <= other._cs.XMin) && !(_cs.YMin >= other._cs.YMax || _cs.YMax <= other._cs.YMin))
            {
                collision = true;
                return CollisionSide.Close;
            }
            return CollisionSide.Other;
        }
    }
}
