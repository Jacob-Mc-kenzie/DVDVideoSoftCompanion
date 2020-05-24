namespace DataManagement
{
    /// <summary>
    /// A simple Binary Tree Structure.
    /// </summary>
    public class DVDTree
    {
        public Movie Value;
        public DVDTree Left, Right;

        /// <summary>
        /// Generate a tree with all null values.
        /// </summary>
        public DVDTree()
        {
            Value = null;
            Left = Right = null;
        }
        /// <summary>
        /// Generate a tree with a root value.
        /// </summary>
        /// <param name="value"></param>
        public DVDTree(Movie value)
        {
            Value = value;
            Left = Right = null;
        }
        /// <summary>
        /// Set the Left branch of the current tree node.
        /// </summary>
        /// <param name="branch">The node to add</param>
        public void AddLeft(DVDTree branch)
        {
            Left = branch;
        }
        /// <summary>
        /// Set the Right branch of the current tree node.
        /// </summary>
        /// <param name="branch">The node to add</param>
        public void AddRight(DVDTree branch)
        {
            Right = branch;
        }
    }
}
