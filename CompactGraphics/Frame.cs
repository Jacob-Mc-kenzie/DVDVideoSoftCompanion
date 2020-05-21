using System;
namespace CompactGraphics
{
    public class Frame : Widget
    {
        char borderchar;
        ConsoleColor forcolor;
        ConsoleColor backcolor;
        public Frame(char c, Rect r)
        {
            this.borderchar = c;
            this.baseBounds = r;
            forcolor = ConsoleColor.White;
            backcolor = ConsoleColor.Black;
            pin = DrawPoint.TopLeft;
            this.bounds = r;
        }
        public Frame(char c,ConsoleColor forcolor, ConsoleColor backcolor, Rect r, DrawPoint p) :this(c,r)
        {
            this.forcolor = forcolor;
            this.backcolor = backcolor;
            pin = p;
            this.bounds = r.OffsetPin(this.pin);
        }

        public override void Draw(Graphics g)
        {
            for (int i = bounds.x1; i <= bounds.x2; i++)
            {
                for (int j = bounds.y1; j <= bounds.y2; j++)
                {
                    if(i == bounds.x1 || i == bounds.x2 || j == bounds.y1 || j == bounds.y2)
                    {
                        g.Draw(borderchar, forcolor, i, j);
                    }
                    g.DrawBackground(backcolor, i, j);
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
