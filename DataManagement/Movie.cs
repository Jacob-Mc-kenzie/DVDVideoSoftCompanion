using System;

namespace DataManagement
{
    /// <summary>
    /// An enum to catorgrise the movie genre
    /// </summary>
    public enum MovieGenre { Drama, Adventure, Family, Action, SciFi, Comedy, Animated, Thriller, Other, Error }
    /// <summary>
    /// An enum to catogrise the Classification
    /// </summary>
    public enum MovieClass { Genral, ParentalGuidance, Mature, MatureAcc, Error }
    /// <summary>
    /// A simple class to hold movie info.
    /// </summary>
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
        /// <summary>
        /// Creates a movie with the given paramaters.
        /// </summary>
        /// <param name="title">The films title</param>
        /// <param name="starring">Who stars in it</param>
        /// <param name="director">The director</param>
        /// <param name="duration">The duration of the movie formatted as {hh,mm,ss} </param>
        /// <param name="genre">The Genre</param>
        /// <param name="classification">The Classification</param>
        /// <param name="quantity">The number of the film in stock</param>
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
        /// <summary>
        /// Borrows the film, by reducing it's quantity and returning its self.
        /// </summary>
        /// <returns>this</returns>
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
                return null;
            }
        }
        /// <summary>
        /// increments the quantity.
        /// </summary>
        public void Return()
        {
            quanitiy++;
        }
        /// <summary>
        /// Sets the quantity.
        /// </summary>
        /// <param name="quantity"></param>
        public void Add(int quantity)
        {
            this.quanitiy = quantity;
        }

    }
}
