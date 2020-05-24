using CompactGraphics;
using DataManagement;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace DVDSoftConpanion
{
    class PhoneLookupMenu : Menu
    {
        //keep track of the user input, and error message box
        private TextEntry Name;
        private Textbox response;
        /// <summary>
        /// Create a new Phone lookup menu.
        /// </summary>
        /// <param name="g">the screen to draw to</param>
        public PhoneLookupMenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 25, g.height / 2, g.height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Lookup Member Phone Number", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("Username / firstname surname", Lt));//limitation wont work with middle names or people with more than two registerd names.
            Lt.y1++;
            Name = new TextEntry(Lt.x1 + 1, Lt.y1, 20);
            onPage.Add(Name);
            onPage.Add(new Textbox("~", ConsoleColor.DarkYellow, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1+= 2;
            response = new Textbox("", Lt);
            onPage.Add(response);
            Lt.y1 += 4;
            onPage.Add(new Textbox("RETURN) Search", ConsoleColor.Cyan, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1++;
            onPage.Add(new Textbox("ESC) Return", ConsoleColor.Gray, Lt, Widget.DrawPoint.TopLeft));
        }
        /// <summary>
        /// Step the frame and evaluate input.
        /// </summary>
        /// <param name="keyinfo">The input</param>
        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame();
            switch (keyinfo.Key)
            {
                case ConsoleKey.Enter:
                    ValidateInput();
                    break;
                case ConsoleKey.Escape:
                    status = 4;
                    break;
                default:
                    Name.Draw(g, keyinfo);
                    break;
            }
        }
        /// <summary>
        /// Validate The given input.
        /// </summary>
        /// <returns>The status of the validation</returns>
        private int ValidateInput()
        {
            if(Regex.IsMatch(Name.Text,@"[A-Za-z 0-9]+"))
            {
                Member m = Program.members.GetMember(Name.Text, out string message);
                if(message != "Success")
                {
                    response.SetText(message);
                    response.SetColor(ConsoleColor.Red);
                    return 0;
                }
                else
                {
                    response.SetText("Ph: "+m.GetPhoneNumber());
                    response.SetColor(ConsoleColor.Green);
                }
            }
            else
            {
                Name.Flash(ConsoleColor.Red);
            }
            return 1;
        }
    }
}
