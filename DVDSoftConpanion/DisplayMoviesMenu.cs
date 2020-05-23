using System;
using CompactGraphics;
using DataManagement;
using System.Collections.Generic;
namespace DVDSoftConpanion
{
    class DisplayMoviesMenu : Menu
    {
        private Movie[] movies;
        private int displayStyle;
        private Textbox response;
        private Rect Start;
        public DisplayMoviesMenu(Graphics g)
        {
            onPage = new List<Widget>();
            movies = Program.movieCollection.ListAll();
            this.g = g;
            status = 0;
            displayStyle = 1;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 130, g.height / 2, g.height / 2 + 50);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Registered DVDs", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            onPage.Add(new Textbox("DVDS", ConsoleColor.Yellow, new Rect(rect.x1 + ((rect.x2 - rect.x1) / 2), rect.x2, rect.y1 + 1, rect.y1 + 4), Widget.DrawPoint.Top));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            Start = Lt;
            DrawMovies();
        }


        private void DrawMovies()
        {
            //TODO: Dynamicly place the movies to the text can wrap
            Rect L = Start;
            L.y2 = L.y2 - 4;
            List<Textbox> displayed = new List<Textbox>();
            if(displayStyle == 1)
            {
                L.x1++;
                displayed.Add(new Textbox("Title", L, ConsoleColor.Yellow));
                L.x1 += 32;
                displayed.Add(new Textbox("Staring", L, ConsoleColor.Yellow));
                L.x1 += 15;
                displayed.Add(new Textbox("Director", L, ConsoleColor.Yellow));
                L.x1 += 20;
                displayed.Add(new Textbox("Duration (hh:mm:ss)", L, ConsoleColor.Yellow));
                L.x1 += 20;
                displayed.Add(new Textbox("Genre", L, ConsoleColor.Yellow));
                L.x1 += 10;
                displayed.Add(new Textbox("Classification",L, ConsoleColor.Yellow));
                L.x1 += 20;
                displayed.Add(new Textbox("Quantity", L, ConsoleColor.Yellow));
                L.x1 += 8;
                L.x1 -= 126;
                foreach (Movie movie in movies)
                {
                    L.y1++;
                    L.x1++;
                    displayed.Add(new Textbox(movie.Title, L));
                    L.x1 += 32;
                    string starring = movie.Starring[0];
                    for (int i = 1; i < movie.Starring.Length; i++)
                        starring += ", " + movie.Starring[i];
                    displayed.Add(new Textbox(starring, L));
                    L.x1 += 15;
                    displayed.Add(new Textbox(movie.Director, L));
                    L.x1 += 20;
                    displayed.Add(new Textbox($"{movie.Duration[0]}:{movie.Duration[1]}:{movie.Duration[2]}", L));
                    L.x1 += 20;
                    displayed.Add(new Textbox(movie.Genre.FriendlyGenreName(), L));
                    L.x1 += 10;
                    displayed.Add(new Textbox(movie.Classification.FriendlyClassName()[1], L));
                    L.x1 += 20;
                    displayed.Add(new Textbox(movie.Quantity.ToString(), L));
                    L.x1 += 8;
                    L.x1 -= 126;
                    
                }


                
            }
            onPage.InsertRange(3,displayed);
            L.y1 = L.y2+1;
            L.y2 = L.y2 + 3;
            onPage.Add(new Textbox("ESC) Return", L));
        }

        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame();
            switch (keyinfo.Key)
            {
                case ConsoleKey.Escape:
                    status = 9;
                    break;
                default:
                    break;
            }
        }

    }
}
