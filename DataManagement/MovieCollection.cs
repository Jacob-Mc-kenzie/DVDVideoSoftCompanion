using System;
namespace DataManagement
{
    public class MovieCollection
    {
        DVDTree StoredDvd;
        public MovieCollection()
        {

        }
        public MovieCollection(Movie toAdd)
        {
            StoredDvd = new DVDTree(toAdd);
        }
        /// <summary>
        /// Adds a dvd to the database, this adds a new movie object if none with that name exists, or adds to the quantity otherwise.
        /// </summary>
        /// <param name="movie">the film to add</param>
        public void AddDVD(Movie movie)
        {
            Movie exists = DoesExits(movie.Title);
            if(exists != null)
            {
                exists.Return();
            }
            else if (StoredDvd == null)
            {
                StoredDvd = new DVDTree(movie);
            }
            else
            {
                
                //todo: insert movie to the binary tree, https://www.csharpstar.com/csharp-program-to-implement-binary-search-tree/ perhaps
            }
        }
        /// <summary>
        /// Removes a film from the public database, will be marked as deleted and moved to 'deleted items'
        /// </summary>
        /// <param name="movie">the movie object to remove</param>
        public void RemoveMovie(Movie movie)
        {
            //todo: check if film exists, if so, either remove from the tree and put in a list of deleted items, or, mark as deleted and leave in the tree.
        }
        /// <summary>
        /// Removes a film from the public database, will be marked as deleted and moved to 'deleted items'
        /// </summary>
        /// <param name="title">the title of the film to remove</param>
        public void RemoveMovie(string title)
        {
            //""
        }
        private Movie DoesExits(string title)
        {
            if (StoredDvd == null)
                return null;
            DVDTree root = StoredDvd;
            Movie foundFilm = null;
            do
            {
                if (root.Value.Title == title)
                {
                    foundFilm = root.Value;

                }
                else
                {
                    if (String.Compare(root.Value.Title, title) < 0) // https://docs.microsoft.com/en-us/dotnet/csharp/how-to/compare-strings
                    {
                        if (root.Right != null)
                        {
                            root = root.Right;
                        }
                        else
                            break;
                    }
                    else
                    {
                        if (root.Left != null)
                        {
                            root = root.Left;
                        }
                        else
                            break;
                    }
                }
            } while (foundFilm != null && (root.Left != null || root.Right != null));

            return foundFilm;
        }
        /// <summary>
        /// Searches for and returns the movie with the given title
        /// </summary>
        /// <param name="title">the film to search for</param>
        /// <returns>the film, if found</returns>
        public Movie BorrowMovie(string title)
        {
            Movie foundFilm = DoesExits(title);

            return foundFilm?.Borrow();
        }
        public void ReturnMovie(Movie movie)
        {
            movie.Return();
        }
        public Movie [] TopTen()
        {
            return new Movie[] { };
        }
    }
}
