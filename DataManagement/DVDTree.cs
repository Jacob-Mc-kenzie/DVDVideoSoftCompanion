namespace DataManagement
{
    public class DVDTree
    {
        public Movie Value;
        public DVDTree Left, Right;

        public DVDTree()
        {
            Value = null;
            Left = null;
            Right = null;
        }

        public DVDTree(Movie value)
        {
            Value = value;
            Left = null;
            Right = null;
        }

        public DVDTree(Movie value, DVDTree left, DVDTree right)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public void AddLeft(DVDTree branch)
        {
            Left = branch;
        }
        public void AddRight(DVDTree branch)
        {
            Right = branch;
        }
    }
}
