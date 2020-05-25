﻿using CompactGraphics;
using DataManagement;
using System;
using System.Collections.Generic;

namespace DVDSoftConpanion
{
    class DisplayTopTenMenu : Menu
    {
        private int displayStyle;
        private Rect Start;
        /// <summary>
        /// Displays The top ten movies.
        /// </summary>
        /// <param name="g">The screen to draw to</param>
        public DisplayTopTenMenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            displayStyle = 1;
            Rect r = new Rect(g.Width / 2, (g.Width / 2) + 132, g.Height / 2, g.Height / 2 + 50);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Top Ten", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
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
            L.y2 = L.y2 - 4;
            List<Textbox> displayed = new List<Textbox>();
            if (displayStyle == 1)
            {
                //generate the headders
                L.x1++;
                displayed.Add(new Textbox("Title", L, ConsoleColor.Yellow));
                L.x1 += 32;
                displayed.Add(new Textbox("Staring", L, ConsoleColor.Yellow));
                L.x1 += 15;
                displayed.Add(new Textbox("Director", L, ConsoleColor.Yellow));
                L.x1 += 19;
                displayed.Add(new Textbox("Duration (hh:mm:ss)", L, ConsoleColor.Yellow));
                L.x1 += 20;
                displayed.Add(new Textbox("Genre", L, ConsoleColor.Yellow));
                L.x1 += 9;
                displayed.Add(new Textbox("Classification", L, ConsoleColor.Yellow));
                L.x1 += 15;
                displayed.Add(new Textbox("Quantity", L, ConsoleColor.Yellow));
                L.x1 += 9;
                displayed.Add(new Textbox("Popularity", L, ConsoleColor.Yellow));
                L.x1 += 11;
                L.x1 -= 131;
                //place the movies
                foreach (Movie movie in Program.movieCollection.TopTen())
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
                    L.x1 += 19;
                    displayed.Add(new Textbox($"{movie.Duration[0]}:{movie.Duration[1]}:{movie.Duration[2]}", L));
                    L.x1 += 20;
                    displayed.Add(new Textbox(movie.Genre.FriendlyGenreName(), L));
                    L.x1 += 9;
                    displayed.Add(new Textbox(movie.Classification.FriendlyClassName()[1], L));
                    L.x1 += 15;
                    displayed.Add(new Textbox(movie.Quantity.ToString(), L));
                    L.x1 += 9;
                    displayed.Add(new Textbox(movie.TimesBorrowed.ToString(), L));
                    L.x1 += 11;
                    L.x1 -= 131;
                }
            }
            onPage.InsertRange(3, displayed);
            L.y1 = L.y2 + 1;
            L.y2 = L.y2 + 3;
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
                default:
                    break;
            }
        }
    }
}
