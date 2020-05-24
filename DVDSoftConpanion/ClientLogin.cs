using CompactGraphics;
using System;
using System.Collections.Generic;
using DataManagement;

namespace DVDSoftConpanion
{
    class ClientLogin : Menu
    {
        //The things to keep track of.
        private int active;
        private Textbox cursorIcon;
        private TextEntry username;
        private TextEntry password;
        /// <summary>
        /// Creates a simple login.
        /// </summary>
        /// <param name="g">The screen to draw to</param>
        public ClientLogin(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            this.active = 1;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 20, g.height / 2, g.height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Client Login", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("Username:", Lt));
            Lt.y1++;
            username = new TextEntry(Lt.x1 + 1, Lt.y1, 14);
            onPage.Add(username);
            cursorIcon = new Textbox("~", ConsoleColor.DarkYellow, Lt, Widget.DrawPoint.TopLeft);
            onPage.Add(cursorIcon);
            Lt.y1++;
            onPage.Add(new Textbox("Password:", Lt));
            Lt.y1++;
            password = new TextEntry(Lt.x1 + 1, Lt.y1, 14);
            onPage.Add(password);
            Lt.y1 += 2;
            onPage.Add(new Textbox("RETURN) Login", ConsoleColor.Cyan, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1++;
            onPage.Add(new Textbox("ESC) Cancel", ConsoleColor.Gray, Lt, Widget.DrawPoint.TopLeft));

        }
        /// <summary>
        /// Handles switching between username and password. also navigation
        /// </summary>
        /// <param name="keyinfo">The user input</param>
        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame();
            switch (keyinfo.Key)
            {
                case ConsoleKey.Tab:
                case ConsoleKey.DownArrow:
                case ConsoleKey.PageDown:
                case ConsoleKey.UpArrow:
                case ConsoleKey.PageUp:
                    active = (active == 1 ? 2 : 1);
                    Rect bounds = cursorIcon.Bounds;
                    bounds.y1 = (active == 1 ? bounds.y1 - 2 : bounds.y1 + 2);
                    cursorIcon.ReSize(bounds);
                    break;
                case ConsoleKey.Enter:
                    if (Login())
                        status = 9;
                    break;
                case ConsoleKey.Escape:
                    status = -1;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    break;
                default:
                    (active == 1 ? username : password).Draw(g, keyinfo);
                    break;
            }
        }
        /// <summary>
        /// Try to varify the login user.
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            //see if they exist
            Member m = Program.members.GetMember(username.Text, out string message, out int index);
            if ( message == "Success" && m.Login(password.Text))
            {
                Program.CurrentMember.Update(index);
                return true;
            }
            //if they can't login for some reason, flash red
            username.Flash(ConsoleColor.Red);
            password.Flash(ConsoleColor.Red);
            return false;

        }
    }
}
