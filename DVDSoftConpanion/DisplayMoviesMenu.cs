using System;
using CompactGraphics;
using DataManagement;
using System.Collections.Generic;
namespace DVDSoftConpanion
{
    class DisplayMoviesMenu : Menu
    {
        //Keep track of whats inportant.
        private Movie[] movies;
        private Textbox [][] Pages;
        private int displayStyle;
        private int pageIndex = 0;
        private int DTVstartIndex;
        Rect Start;
        /// <summary>
        /// Dispaly all the rigistered movies.
        /// </summary>
        /// <param name="g">The screen to draw to</param>
        public DisplayMoviesMenu(Graphics g)
        {
            onPage = new List<Widget>();
            movies = Program.movieCollection.ListAll();
            this.g = g;
            status = 0;
            displayStyle = 1;
            Rect r = new Rect(g.Width / 2, (g.Width / 2) + 130, g.Height / 2, g.Height / 2 + 50);
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

        /// <summary>
        /// Generates the widget list for drawing the table of movies.
        /// </summary>
        private void DrawMovies()
        {
            
            
            //TODO: Dynamicly place the movies to the text can wrap
            Rect L = Start;
            L.y2 -= 4;
            const int fitonscreen = 44;
            int index = 0;
            int xinit = L.x2;
            List<Textbox> displayed = new List<Textbox>();
            List<Textbox[]> pages = new List<Textbox[]>();
            if(displayStyle == 1)
            {
                //generate the headders
                L.x1++;
                L.x2  = L.x1 + 32;
                onPage.Add(new Textbox("Title", L, ConsoleColor.Yellow));
                L.x1 += 32;
                L.x2 = L.x1 + 15;
                onPage.Add(new Textbox("Staring", L, ConsoleColor.Yellow));
                L.x1 += 15;
                L.x2 = L.x1 + 20;
                onPage.Add(new Textbox("Director", L, ConsoleColor.Yellow));
                L.x1 += 20;
                L.x2 = L.x1 + 20;
                onPage.Add(new Textbox("Duration (hh:mm:ss)", L, ConsoleColor.Yellow));
                L.x1 += 20;
                L.x2 = L.x1 + 10;
                onPage.Add(new Textbox("Genre", L, ConsoleColor.Yellow));
                L.x1 += 10;
                L.x2 = L.x1 + 20;
                onPage.Add(new Textbox("Classification",L, ConsoleColor.Yellow));
                L.x1 += 20;
                L.x2 = L.x1 + 8;
                onPage.Add(new Textbox("Quantity", L, ConsoleColor.Yellow));
                L.x1 += 8;
                L.x2 = xinit;
                L.x1 -= 126;
                
                DTVstartIndex = onPage.Count-1;
                //place the movies
                foreach (Movie movie in movies)
                {
                    if(fitonscreen == index)
                    {
                        pages.Add(displayed.ToArray());
                        index = 0;
                        displayed.Clear();
                        L.y1 -= 44;
                    }
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
                    index++;
                } 
            }
            if(displayed.Count > 0)
                pages.Add(displayed.ToArray());
            Start = L;
            if (pages.Count < 2)
                onPage.InsertRange(10, displayed);
            else
            {
                onPage.InsertRange(10, pages[pageIndex]);
            }

            this.Pages = pages.ToArray();
            if(this.Pages.Length > 0)
            {
                if(pageIndex < this.Pages.Length - 1)
                {
                    onPage.Add(new Textbox("->) Next", new Rect(L.x1+120,L.x1+130,L.y2+1,L.y2+3)));
                }
                if(pageIndex > 0)
                {
                    onPage.Add(new Textbox("<-) Previous", new Rect(L.x1,L.x2,L.y2+1,L.y2+3)));
                }
            }
            L.y1 = L.y2 + 2;
            L.y2 = L.y2 + 4;
            
            //remember to pop the anvigation at the bottom, unfortunatly more than 48 movies causes them to underflow.
            onPage.Add(new Textbox("ESC) Return", L));
        }

        private void ChangePage()
        {
            Rect L = Start;
            onPage.RemoveRange(DTVstartIndex, onPage.Count - DTVstartIndex);
            onPage.AddRange(Pages[pageIndex]);
            if (this.Pages.Length > 0)
            {
                if (pageIndex < this.Pages.Length - 1)
                {
                    onPage.Add(new Textbox("->) Next", new Rect(L.x1 + 120, L.x1 + 130, L.y2 + 1, L.y2 + 3)));
                }
                if (pageIndex > 0)
                {
                    onPage.Add(new Textbox("<-) Previous", new Rect(L.x1, L.x2, L.y2 + 1, L.y2 + 3)));
                }
            }
            L.y1 = L.y2 + 2;
            L.y2 = L.y2 + 4;

            //remember to pop the anvigation at the bottom, unfortunatly more than 48 movies causes them to underflow.
            onPage.Add(new Textbox("ESC) Return", L));
        }


        /// <summary>
        /// Handle the escape key
        /// </summary>
        /// <param name="keyinfo">Key</param>
        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame();
            switch (keyinfo.Key)
            {
                case ConsoleKey.Escape:
                    status = 9;
                    break;
                case ConsoleKey.RightArrow:
                    if (Pages.Length > 0)
                    {
                        if(pageIndex < Pages.Length -1)
                        {
                            pageIndex++;
                            ChangePage();

                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (Pages.Length > 0)
                    {
                        if (pageIndex > 0)
                        {
                            pageIndex--;
                            ChangePage();

                        }
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
