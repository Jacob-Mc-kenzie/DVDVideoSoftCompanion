using CompactGraphics;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using DataManagement;
namespace DVDSoftConpanion
{
    class AddDVDMenu : Menu
    {
        private Textbox cursor;
        private TextEntry title;
        private TextEntry starring;
        private TextEntry director;
        private TextEntry duration;
        private TextEntry genre;
        private TextEntry classification;
        private TextEntry quantity;
        private TextEntry[] textEntries;
        private int tabIndex;
        private Rect baseCursorBounds;
        public AddDVDMenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            tabIndex = 0;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 30, g.height / 2, g.height / 2 + 20);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 -= 1;
            rect.y2 += 1;
            onPage.Add(new Textbox("Add DVD", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("Title", Lt));
            Lt.y1++;
            title = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            cursor = new Textbox("~", ConsoleColor.DarkYellow, Lt, Widget.DrawPoint.TopLeft);
            onPage.Add(cursor);
            baseCursorBounds = cursor.Bounds;
            onPage.Add(title);
            Lt.y1++;
            onPage.Add(new Textbox("Starring (commar seperated)", Lt));
            Lt.y1++;
            starring = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(starring);
            Lt.y1++;
            onPage.Add(new Textbox("Director", Lt));
            Lt.y1++;
            director = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(director);
            Lt.y1++;
            onPage.Add(new Textbox("Duration (hh:mm:ss)", Lt));
            Lt.y1++;
            duration = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(duration);
            Lt.y1++;
            onPage.Add(new Textbox("Genre", Lt));
            Lt.y1++;
            genre = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(genre);
            Lt.y1++;
            onPage.Add(new Textbox("Classification", Lt));
            Lt.y1++;
            classification = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(classification);
            Lt.y1++;
            onPage.Add(new Textbox("Quantity", Lt));
            Lt.y1++;
            quantity = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(quantity);
            Lt.y1 += 2;
            onPage.Add(new Textbox("RETURN) Add", ConsoleColor.Cyan, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1++;
            onPage.Add(new Textbox("ESC) Cancel", ConsoleColor.Gray, Lt, Widget.DrawPoint.TopLeft));
            textEntries = new TextEntry[] { title, starring, director, duration, genre, classification, quantity };
        }
        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame();
            switch (keyinfo.Key)
            {
                case ConsoleKey.Enter:
                    if (ValidateData())
                        status = 4;
                    break;
                case ConsoleKey.Tab:
                case ConsoleKey.DownArrow:
                case ConsoleKey.PageDown:
                    tabIndex = (tabIndex + 1) % textEntries.Length;
                    Rect bounds = baseCursorBounds;
                    bounds.y1 = baseCursorBounds.y1 + (2 * tabIndex);
                    cursor.ReSize(bounds);
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.PageUp:
                    tabIndex = (tabIndex - 1) % textEntries.Length;
                    Rect bound = baseCursorBounds;
                    bound.y1 = baseCursorBounds.y1 + (2 * tabIndex);
                    cursor.ReSize(bound);
                    break;
                case ConsoleKey.Escape:
                    status = 4;
                    break;
                default:
                    textEntries[tabIndex].Draw(g, keyinfo);
                    break;
            }
        }
        internal bool ValidateData()
        {
            bool success = true;
            if(!Regex.IsMatch(title.Text,@"[A-Za-z0-9 \.]+"))
            {
                title.Flash(ConsoleColor.Red);
                success = false;
            }
            if (!Regex.IsMatch(starring.Text, @"[A-Z][a-z\. A-Z,]+"))
            {
                starring.Flash(ConsoleColor.Red);
                success = false;
            }
            if(!Regex.IsMatch(director.Text, @"[A-Z][a-z\. A-Z,]+"))
            {
                director.Flash(ConsoleColor.Red);
                success = false;

            }
            if(!Regex.IsMatch(duration.Text, @"[0-9]+:[0-9]+:[0-9]+"))
            {
                duration.Flash(ConsoleColor.Red);
                success = false;
            }
            if(genre.Text.FriendlyGenreName() == MovieGenre.Error)
            {
                genre.Flash(ConsoleColor.Red);
                success = false;
            }
            if(classification.Text.FriendlyClassName() == MovieClass.Error)
            {
                classification.Flash(ConsoleColor.Red);
                success = false;
            }
            if(!int.TryParse(quantity.Text, out int _))
            {
                quantity.Flash(ConsoleColor.Red);
                success = false;
            }
            if (success)
            {
                string[] s = duration.Text.Split(':');
                int[] dur = new int[] { int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]) };
                Program.movieCollection.AddDVD(new Movie(title.Text, starring.Text.Split(','), director.Text, dur, genre.Text.FriendlyGenreName(), classification.Text.FriendlyClassName(), int.Parse(quantity.Text)));
            }
                
            return success;
        }
    }
}
