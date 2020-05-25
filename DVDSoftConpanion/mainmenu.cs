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
    class Mainmenu : Menu
    {
        /// <summary>
        /// Creates the main menu
        /// </summary>
        /// <param name="g"></param>
        public Mainmenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.Width / 2, (g.Width / 2) + 20, g.Height / 2, g.Height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
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

        /// <summary>
        /// Navigaes if given the apropriate number key.
        /// </summary>
        /// <param name="keyinfo">the user input</param>
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
                case ConsoleKey.Escape:
                    status = 3;
                    break;
                default:
                    break;
            }
        }

    }
}
