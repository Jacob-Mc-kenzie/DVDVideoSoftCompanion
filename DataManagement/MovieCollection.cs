using System;
using System.Collections.Generic;
using System.Linq;
namespace DataManagement
{
    public class MovieCollection
    {
        /// <summary>
        /// The binary tree to store DVDs in
        /// </summary>
        DVDTree StoredDvd;
        /// <summary>
        /// Create a new empty collection of DVDs
        /// </summary>
        public MovieCollection()
        {
            StoredDvd = new DVDTree();
        }
        /// <summary>
        /// Create a new collection of DVDs
        /// </summary>
        /// <param name="toAdd"></param>
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
            //First have we already got a copy of this film?
            Movie exists = GetifExists(movie.Title);
            //if we do, then just add to it's quantity.
            if(exists != null)
            {
                exists.Add(movie.Quantity);
            }
            //otherwise if we don't have anything in the collection yet, then just initilise it.
            else if (StoredDvd == null)
            {
                StoredDvd = new DVDTree(movie);
            }
            //Otherwise insert it into the tree.
            else
            {
                //Start from the root.
                DVDTree point = StoredDvd;
                DVDTree temp;
                DVDTree toAdd = new DVDTree(movie);
                //Wile we still have tree to traverse.
                while (true)
                {
                    temp = point;
                    //Search down the tree to the point to add.
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
            }
        }
        /// <summary>
        /// Recursivly remove and shift elements in the tree.
        /// </summary>
        /// <param name="R">The current point in the tree</param>
        /// <param name="M">The movie to remove</param>
        /// <returns></returns>
        private DVDTree RemoveRecursivly(DVDTree R, Movie M)
        {
            //If empty we must be done
            if (R == null)
                return R;
            //compare the value to remove and the current node to see which path to take.
            int ordinalComparison = String.Compare(M.Title, R.Value.Title, StringComparison.Ordinal);
            //if the desired is smaller, keep going left.
            if (ordinalComparison < 0)
            {
                R.Left = RemoveRecursivly(R.Left, M);
            }
            //If it's greater keep going right.
            else if (ordinalComparison > 0)
            {
                R.Right = RemoveRecursivly(R.Right, M);
            }
            //if it's just right then we need to start dealing with any dependant children.
            else
            {
                if (R.Left == null) // no more children good.
                {
                    return R.Right;
                }
                else if (R.Right == null) // ""
                {
                    return R.Left;
                }
                else //there must be two children left, darn.
                {
                    R.Value = FindMin(R.Right);// get the smallest child.
                    R.Right = RemoveRecursivly(R.Right, R.Value);//shift it up.
                }
            }
            return R;
        }
        /// <summary>
        /// Get the minimum child.
        /// </summary>
        /// <param name="r">the node to search from.</param>
        /// <returns></returns>
        private Movie FindMin(DVDTree r)
        {
            Movie min = r.Value;
            //just keep trudging left till we get what we want
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
            Movie movie = GetifExists(title);
            if(movie != null)
            {
                return RemoveMovie(movie);
            }
            return "Movie Could not be found";
        }
        /// <summary>
        /// Do a binary search for a film, and return it if found.
        /// </summary>
        /// <param name="title">The name of the film to search for</param>
        /// <returns>The movie if it exists</returns>
        public Movie GetifExists(string title)
        {
            //If can't exist if we don't have anything.
            if (StoredDvd == null)
                return null;
            DVDTree root = StoredDvd;
            Movie foundFilm = null;
            //while we haven't found the film
            do
            {
                //if the current Node is our movie, good!
                if (root.Value.Title == title)
                {
                    foundFilm = root.Value;
                }
                else
                {
                    //Check to see which direction to travel.
                    if (String.Compare(root.Value.Title, title, StringComparison.Ordinal) < 0) // https://docs.microsoft.com/en-us/dotnet/csharp/how-to/compare-strings
                    {
                        //if we can keep looking good, otherwise break
                        if (root.Right != null)
                        {
                            root = root.Right;
                        }
                        else
                            break;
                    }
                    else
                    {
                        //if we can keep looking good, otherwise break
                        if (root.Left != null)
                        {
                            root = root.Left;
                        }
                        else
                            break;
                    }
                }
            } while (foundFilm == null);
            //return whatever the result was
            return foundFilm;
        }
        /// <summary>
        /// Searches for and returns the movie with the given title
        /// </summary>
        /// <param name="title">the film to search for</param>
        /// <param name="message">A message explaining the result</param>
        /// <returns>the film, if found</returns>
        public Movie BorrowMovie(string title, out string message)
        {
            //look for the film
            Movie foundFilm = GetifExists(title);
            if(foundFilm == null)
            {
                message = "Film Could not be found";
                return null;
            }
            //ensure it's in stock
            else if (foundFilm.Quantity > 0)
            {
                message = "Success";
                return foundFilm;
            }
            message = "Film not in stock";
            return null;
        }
        /// <summary>
        /// Just calls Movie.Return()
        /// </summary>
        /// <param name="movie">The film to return</param>
        public void ReturnMovie(Movie movie)
        {
            movie.Return();
        }
        /// <summary>
        /// Traverses the tree inorder, and returns the resulting array.
        /// </summary>
        /// <returns>An array of all the curernt films in Lexographic order</returns>
        public Movie [] ListAll()
        {
            List<Movie> movies = new List<Movie>();
            //The recursive helper for the traversal.
            void GetRecursivlyInOrder(DVDTree n)
            {
                if(n == null)
                    return;
                //ensure that the leftmost gets added first, followed by the root, then the root to the rightmost.
                GetRecursivlyInOrder(n.Left);
                movies.Add(n.Value);
                GetRecursivlyInOrder(n.Right);
            }
            GetRecursivlyInOrder(StoredDvd);
            return movies.ToArray();
        }
        /// <summary>
        /// Gets the size of the tree by doing a traversal.
        /// </summary>
        /// <returns>The size of the tree</returns>
        public int GetLength()
        {
            int movies = 0;
            void GetRecursivlyInOrder(DVDTree n)
            {
                if (n == null)
                    return;
                //adds inorder, but it dosen't matter.
                GetRecursivlyInOrder(n.Left);
                movies++;
                GetRecursivlyInOrder(n.Right);
            }
            GetRecursivlyInOrder(StoredDvd);
            return movies;
        }
        /// <summary>
        /// Gets the Top ten most Borrowed films.
        /// </summary>
        /// <returns>An array of those ten</returns>
        public Movie [] TopTen()
        {
            MovieCollection d = new MovieCollection();
            //First get all the movies. and add them racked by popularity to a new tree
            foreach (Movie movie in this.ListAll())
            {
                //the first movie initilises the tree.
                if (d.StoredDvd == null)
                {
                    d.StoredDvd = new DVDTree(movie);
                }
                else
                {
                    //starting from the root
                    DVDTree point = d.StoredDvd;
                    DVDTree temp;
                    DVDTree toAdd = new DVDTree(movie);
                    //While we haven't reached the add point.
                    while (true)
                    {
                        temp = point;
                        //Search down the tree to the point to add.
                        if (point.Value.TimesBorrowed < toAdd.Value.TimesBorrowed)
                        {
                            point = point.Left;
                            if (point == null) // if we have reached a leaf, add.
                            {
                                temp.Left = toAdd;
                                break;
                            }
                        }
                        else
                        {
                            point = point.Right;
                            if (point == null)// if we have reached a leaf, add.
                            {
                                temp.Right = toAdd;
                                break;
                            }
                        }
                    }
                }
            }
            //Convert this tree back to an array, and grab the first 10 elements. return those as an array
            return d.ListAll().Take(10).ToArray();
        }
    }
}
