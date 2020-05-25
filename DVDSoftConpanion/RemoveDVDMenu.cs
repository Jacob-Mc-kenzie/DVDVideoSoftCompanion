using CompactGraphics;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DataManagement;

namespace DVDSoftConpanion
{
    class RemoveDVDMenu : Menu
    {
        //keep track of the user input, and error message box
        private TextEntry title;
        private Textbox response;
        /// <summary>
        /// creates a DVD removal menu.
        /// </summary>
        /// <param name="g">the screen to draw to</param>
        public RemoveDVDMenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 25, g.height / 2, g.height / 2 + 15);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Remove DVD Menu", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("DVD title", Lt));
            Lt.y1++;
            title = new TextEntry(Lt.x1 + 1, Lt.y1, 20);
            onPage.Add(title);
            onPage.Add(new Textbox("~", ConsoleColor.DarkYellow, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1+= 2;
            response = new Textbox("", Lt);
            onPage.Add(response);
            Lt.y1 += 4;
            onPage.Add(new Textbox("RETURN) Remove", ConsoleColor.Cyan, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1++;
            onPage.Add(new Textbox("ESC) Cancel", ConsoleColor.Gray, Lt, Widget.DrawPoint.TopLeft));
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
                case ConsoleKey.Escape:
                    status = 4;
                    break;
                case ConsoleKey.Enter:
                    switch (ValidateInput())
                    {
                        case 0:
                            title.Flash(ConsoleColor.Red);
                            response.SetText("Unable to find film");
                            response.SetColor(ConsoleColor.Red);
                            break;
                        case 1:
                            title.Flash(ConsoleColor.Red);
                            break;
                        case 2:
                            status = 4;
                            break;
                        default:
                            response.SetText("An unknown error occured");
                            response.SetColor(ConsoleColor.Red);
                            break;
                    }
                    break;
                default:
                    title.Draw(g, keyinfo);
                    break;
            }
        }
        /// <summary>
        /// Validate The given input.
        /// </summary>
        /// <returns>The status of the validation</returns>
        private int ValidateInput()
        {
            if (!Regex.IsMatch(title.Text, @"[A-Za-z0-9 \.]+"))
            {
                return 0;
            }
            else
            {
                string resp = Program.movieCollection.RemoveMovie(title.Text);
                if(resp != "Success")
                {
                    response.SetText(resp);
                    response.SetColor(ConsoleColor.Red);
                    return 1;
                }
                response.SetText(resp);
                response.SetColor(ConsoleColor.Green);
                return 2;
            }
        }
    }
}
