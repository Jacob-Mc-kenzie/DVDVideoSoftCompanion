using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompactGraphics;

namespace DVDSoftConpanion
{
    /// <summary>
    /// This and the other menus without user input are just 
    /// some static widgets that check to navigate to their relative menus.
    /// </summary>
    class Staffmenu : Menu
    {
        /// <summary>
        /// Creates the Staff menu
        /// </summary>
        /// <param name="g"></param>
        public Staffmenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.Width / 2, (g.Width / 2) + 25, g.Height / 2, g.Height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Staff Menu", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("1) Add a new movie DVD", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("2) Remove a movie DVD", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("3) Register a new Member", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("4) Find a registered", Lt));
            Lt.y1++;
            Lt.x1 += 3;
            onPage.Add(new Textbox("members phone number", Lt));
            Lt.x1 -= 3;
            Lt.y1+= 1;
            onPage.Add(new Textbox("0) Return", Lt));
        }
        /// <summary>
        /// Navigaes if given the apropriate number key.
        /// </summary>
        /// <param name="keyinfo">the user input</param>
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
