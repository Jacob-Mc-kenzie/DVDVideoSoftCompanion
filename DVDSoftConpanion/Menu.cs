using CompactGraphics;
using System;
using System.Collections.Generic;

namespace DVDSoftConpanion
{
    public class Menu
    {
        protected Graphics g;
        protected List<Widget> onPage;
        public int Status { get { return status; } }
        protected int status;
        public virtual void StepFrame()
        {
            foreach (Widget widget in onPage)
            {
                widget.Draw(g);
            }
        }
        public virtual void StepFrame(ConsoleKeyInfo keyinfo)
        {
            foreach (Widget widget in onPage)
            {
                widget.Draw(g, keyinfo);
            }
        }
    }
}
