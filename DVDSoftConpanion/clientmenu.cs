using System;
using System.Collections.Generic;
using CompactGraphics;
using DataManagement;

namespace DVDSoftConpanion
{
    /// <summary>
    /// This and the other menus without user input are just 
    /// some static widgets that check to navigate to their relative menus.
    /// </summary>
    class Clientmenu : Menu
    {
        /// <summary>
        /// Creates a Clent Menu
        /// </summary>
        /// <param name="g">The screen to draw to</param>
        public Clientmenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.Width / 2, (g.Width / 2) + 25, g.Height / 2, g.Height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Client Menu", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("1) Display All DVDs", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("2) Borrow DVD", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("3) Return DVD", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("4) Show Loaned", Lt));
            Lt.y1++;
            onPage.Add(new Textbox("5) Display top 10", Lt));
            Lt.y1 += 1;
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
                    Program.CurrentMember.Update(-1);
                    break;
                case ConsoleKey.D1:
                    status = 10;
                    break;
                case ConsoleKey.D2:
                    status = 11;
                    break;
                case ConsoleKey.D3:
                    status = 12;
                    break;
                case ConsoleKey.D4:
                    status = 13;
                    break;
                case ConsoleKey.D5:
                    status = 14;
                    break;
                case ConsoleKey.D0:
                    status = -1;
                    Program.CurrentMember.Update(-1);
                    break;
                default:
                    break;
            }
        }
    }
}
