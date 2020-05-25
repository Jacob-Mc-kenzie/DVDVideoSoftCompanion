using CompactGraphics;
using DataManagement;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DVDSoftConpanion
{
    class BorrowDVDMenu : Menu
    {
        //keep track of the user input, and error message box
        private TextEntry Title;
        private Textbox response;
        /// <summary>
        /// Create a new DVD borrowing menu
        /// </summary>
        /// <param name="g">the screen to draw to</param>
        public BorrowDVDMenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.Width / 2, (g.Width / 2) + 25, g.Height / 2, g.Height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Borrow DVD", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("DVD title", Lt));//limitation wont work with middle names or people with more than two registerd names.
            Lt.y1++;
            Title = new TextEntry(Lt.x1 + 1, Lt.y1, 20);
            onPage.Add(Title);
            onPage.Add(new Textbox("~", ConsoleColor.DarkYellow, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1 += 2;
            response = new Textbox("", Lt);
            onPage.Add(response);
            Lt.y1 += 4;
            onPage.Add(new Textbox("RETURN) Borrow", ConsoleColor.Cyan, Lt, Widget.DrawPoint.TopLeft));
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
                    switch (ValidateInput())
                    {
                        case 0:
                        case 2:
                            break;
                        case 1:
                            status = 9;
                            break;
                        default:
                            break;
                    }
                    break;
                case ConsoleKey.Escape:
                    status = 9;
                    break;
                default:
                    Title.Draw(g, keyinfo);
                    break;
            }
        }
        /// <summary>
        /// attempts to borrow the dvd given by the user input.
        /// </summary>
        /// <returns>if it worked</returns>
        private int ValidateInput()
        {
            //first check if the user can even borrow.
            if(Program.members.registeredMembers[Program.CurrentMember.Value].borrowed.GetLength() > 9)
            {
                response.SetText("Borrow Limit reached");
                response.SetColor(ConsoleColor.Red);
                return 0;
            }
            //then check if the given text looks like a move title.
            if (Regex.IsMatch(Title.Text, @"[A-Za-z0-9 \.]+"))
            {
                //check to see if we have the movie.
                Movie m = Program.movieCollection.BorrowMovie(Title.Text, out string message);
                //if we failed for some reason say why.
                if (message != "Success")
                {
                    response.SetText(message);
                    response.SetColor(ConsoleColor.Red);
                    return 0;
                }
                //otherwise if the user hasn't already borrowed a copy of that film, borrow it.
                else if (Program.members.registeredMembers[Program.CurrentMember.Value].borrowed.GetifExists(Title.Text) == null)
                {
                    Program.members.registeredMembers[Program.CurrentMember.Value].Borrow(m);
                    return 1;
                }
                else
                {
                    response.SetText("You've already borrowed that film");
                    response.SetColor(ConsoleColor.Red);
                    return 0;
                }
            }
            else
            {
                Title.Flash(ConsoleColor.Red);
                return 0;
            }
            
        }
    }
}
