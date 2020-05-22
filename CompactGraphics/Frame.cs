using System;
namespace CompactGraphics
{
    public class Frame : Widget
    {
        char borderchar;
        ConsoleColor backcolor;
        public Frame(char c, Rect r)
        {
            this.borderchar = c;
            this.baseBounds = r;
            forColor = ConsoleColor.White;
            backcolor = ConsoleColor.Black;
            Pin = DrawPoint.TopLeft;
            this.Bounds = r;
        }
        public Frame(char c,ConsoleColor forcolor, ConsoleColor backcolor, Rect r, DrawPoint p) :this(c,r)
        {
            this.forColor = forcolor;
            this.backcolor = backcolor;
            Pin = p;
            this.Bounds = r.OffsetPin(this.Pin);
        }

        public override void Draw(Graphics g)
        {
            for (int i = Bounds.x1; i <= Bounds.x2; i++)
            {
                for (int j = Bounds.y1; j <= Bounds.y2; j++)
                {
                    if(i == Bounds.x1 || i == Bounds.x2 || j == Bounds.y1 || j == Bounds.y2)
                    {
                        g.Draw(borderchar, forColor, i, j);
                    }
                    g.DrawBackground(backcolor, i, j);
                }
            }
        }

        public override void ReSize(Rect rect)
        {
            this.baseBounds = rect;
            this.Bounds = rect.OffsetPin(this.Pin);
        }
        public override void PinTo(DrawPoint point)
        {
            this.Pin = point;
            this.Bounds = baseBounds.OffsetPin(Pin);
        }

        public override void Draw(Graphics g, ConsoleKeyInfo keyInfo)
        {
            Draw(g);
            //ToDo
        }
        public override void SetColor(ConsoleColor color)
        {
            this.forColor = color;
        }
    }
}
