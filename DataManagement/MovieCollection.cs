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
                exists.Add(movie.Quantity);
            }
            else if (StoredDvd == null)
            {
                StoredDvd = new DVDTree(movie);
            }
            else
            {
                DVDTree point = StoredDvd;
                DVDTree temp;
                DVDTree toAdd = new DVDTree(movie);
                while (true)
                {
                    temp = point;
                    if(String.Compare(toAdd.Value.Title, point.Value.Title, StringComparison.Ordinal) < 0)
                    {
                        point = point.Left;
                        if(point == null)
                        {
                            temp.Left = toAdd;
                            break;
                        }
                    }
                    else
                    {
                        point = point.Right;
                        if(point == null)
                        {
                            temp.Right = toAdd;
                            break;
                        }
                    }
                }
                //todo: insert movie to the binary tree, https://www.csharpstar.com/csharp-program-to-implement-binary-search-tree/ perhaps
            }
        }
        private DVDTree RemoveRecursivly(DVDTree R, Movie M)
        {
            if (R == null)
                return R;
            int ordinalComparison = String.Compare(M.Title, R.Value.Title, StringComparison.Ordinal);
            if (ordinalComparison < 0)
            {
                R.Left = RemoveRecursivly(R.Left, M);
            }
            else if (ordinalComparison > 0)
            {
                R.Right = RemoveRecursivly(R.Right, M);
            }
            else
            {
                if (R.Left == null)
                {
                    return R.Right;
                }
                else if (R.Right == null)
                {
                    return R.Left;
                }
                else
                {
                    R.Value = FindMin(R.Right);
                    R.Right = RemoveRecursivly(R.Right, R.Value);
                }
            }
            return R;
        }
        private Movie FindMin(DVDTree r)
        {
            Movie min = r.Value;
            while (r.Left != null)
            {
                min = r.Left.Value;
                r = r.Left;
            }
            return min;
        }
        /// <summary>
        /// Removes a film from the public database.
        /// </summary>
        /// <param name="movie">the movie object to remove</param>
        public string RemoveMovie(Movie movie)
        {
            StoredDvd = RemoveRecursivly(StoredDvd, movie);
            return "Success";
        }
        /// <summary>
        /// Removes a film from the public database.
        /// </summary>
        /// <param name="title">the title of the film to remove</param>
        public string RemoveMovie(string title)
        {
            Movie movie = DoesExits(title);
            if(movie != null)
            {
                return RemoveMovie(movie);
            }
            return "Movie Could not be found";
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
                    if (String.Compare(root.Value.Title, title, StringComparison.Ordinal) < 0) // https://docs.microsoft.com/en-us/dotnet/csharp/how-to/compare-strings
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
            } while (foundFilm == null);

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
