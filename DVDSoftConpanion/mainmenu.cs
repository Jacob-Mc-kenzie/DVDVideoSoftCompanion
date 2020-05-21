using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompactGraphics;

namespace DVDSoftConpanion
{
    class Mainmenu : Menu
    {
        public Mainmenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 20, g.height / 2, g.height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);//new Rect(r.x1, r.x2 + 10, r.y1, r.y2).OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 -2;
            rect.y2 = rect.y1+2;
            onPage.Add(new Textbox("Welcome to the Community Library", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            onPage.Add(new Textbox("Main Menu", ConsoleColor.Yellow, new Rect(rect.x1 + ((rect.x2 - rect.x1) / 2), rect.x2, rect.y1+2, rect.y1+4), Widget.DrawPoint.Top));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("1) Staff Login", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("2) Member Login", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("0) Exit", Lt));
        }


        public override void StepFrame(ConsoleKeyInfo keyinfo) 
        {
            base.StepFrame(keyinfo);
            switch (keyinfo.Key)
            {
                case ConsoleKey.D1:
                    status = 1;
                    break;
                case ConsoleKey.D2:
                    status = 2;
                    break;
                case ConsoleKey.D0:
                    status = 3;
                    break;
                default:
                    break;
            }
        }

    }
}
