using System;
using System.Collections.Generic;
namespace CompactGraphics
{
    public class Textbox : Widget
    {
        List<string> lines;
        ConsoleColor forcolor;
        
        public Textbox(string text, Rect r)
        {
            this.baseBounds = r;
            this.bounds = r;
            lines = text.Wrap(Math.Abs(r.x2 - r.x1));
            forcolor = ConsoleColor.White;
            this.pin = DrawPoint.TopLeft;
        }

        public Textbox(string text,ConsoleColor forcolor, Rect r, DrawPoint p) :this(text,r)
        {
            this.forcolor = forcolor;
            this.pin = p;
            bounds = baseBounds.OffsetPin(p);
        }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if(bounds.y1 + i <= bounds.y2)
                {
                    g.Draw(lines[i], forcolor, bounds.x1, bounds.y1 + i);
                }
            }

        }
        public override void reSize(Rect rect)
        {
            this.baseBounds = rect;
            this.bounds = rect.OffsetPin(this.pin);
        }
        public override void PinTo(DrawPoint point)
        {
            this.pin = point;
            this.bounds = baseBounds.OffsetPin(pin);
        }

        public override void Draw(Graphics g, ConsoleKeyInfo keyInfo)
        {
            Draw(g);
            //ToDo
        }
    }

}
