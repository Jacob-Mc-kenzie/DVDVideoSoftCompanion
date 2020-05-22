using CompactGraphics;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DataManagement;

namespace DVDSoftConpanion
{
    class RemoveDVDMenu : Menu
    {
        private TextEntry title;
        private Textbox response;
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

        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame();
            switch (keyinfo.Key)
            {
                case ConsoleKey.Escape:
                    status = 4;
                    break;
                case ConsoleKey.Enter:
                    response.SetText("Test string");
                    switch (ValidateInput())
                    {
                        case 0:
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
