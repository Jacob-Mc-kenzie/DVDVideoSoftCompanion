using System;

namespace DataManagement
{
    public enum MovieGenre { Drama, Adventure, Family, Action, SciFi, Comedy, Animated, Thriller, Other, Error }
    public enum MovieClass { Genral, ParentalGuidance, Mature, MatureAcc, Error }

    public class Movie
    {
        private int quanitiy;
        public int Quantity { get { return quanitiy; } private set { } }

        private int timesBorrowed;
        public int TimesBorrowed { get { return timesBorrowed; } private set { } }
        private string title;
        public string Title { get { return title;  } private set { } }

        private string[] starring;
        public string[] Starring { get { return starring; } private set { } }

        private string director;
        public string Director { get { return director; } private set { } }


        private int[] duration;
        /// <summary>
        /// Formatted in [hh, mm, ss]
        /// </summary>
        public int[] Duration { get { return duration; } private set { } }

        private MovieGenre genre;
        public MovieGenre Genre { get { return genre; } private set { } }
        
        private MovieClass classification;
        public MovieClass Classification { get { return classification; } private set { } }

        public Movie(string title, string[] starring, string director, int[] duration, MovieGenre genre, MovieClass classification, int quantity)
        {
            this.quanitiy = quantity;
            this.title = title;
            this.starring = starring;
            this.director = director;
            this.duration = duration;
            this.genre = genre;
            this.classification = classification;
            timesBorrowed = 0;
        }

        public Movie Borrow()
        {
            if(quanitiy > 0)
            {
                quanitiy--;
                timesBorrowed++;
                return this;
            }
            else
            {
                throw new IndexOutOfRangeException("There are none of this movie avalible for borrowing.");
            }
        }
        public void Return()
        {
            quanitiy++;
        }

    }
}
