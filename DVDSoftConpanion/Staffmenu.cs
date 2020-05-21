using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompactGraphics;

namespace DVDSoftConpanion
{
    class Staffmenu : Menu
    {
        public Staffmenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 20, g.height / 2, g.height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Staff Login", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("1) Add DVD", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("2) Remove DVD", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("3) Register Member", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("4) Find Contact Number", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("0) Return", Lt));
        }

        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame(keyinfo);
            switch (keyinfo.Key)
            {
                case ConsoleKey.Escape:
                    status = -1;
                    break;
                case ConsoleKey.D1:
                    status = 5;
                    break;
                case ConsoleKey.D2:
                    status = 6;
                    break;
                case ConsoleKey.D3:
                    status = 7;
                    break;
                case ConsoleKey.D4:
                    status = 8;
                    break;
                case ConsoleKey.D0:
                    status = -1;
                    break;
                default:
                    break;
            }
        }
    }
}
