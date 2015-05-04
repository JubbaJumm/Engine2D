using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine2D
{
    public class FrameCounter
    {
        public int FrameCount = 0;
        public float TimeElapsed = 0.0f;

        public void NewFrame(float elapsedtime)
        {
            FrameCount++;
            TimeElapsed = elapsedtime;
        }

        public float GetLastFrameTime(float elapsedtime)
        {
            return elapsedtime - TimeElapsed;
        }
    }
}
