namespace CompactGraphics
{

    public abstract class Widget
    {
        /// <summary>
        /// The point to align to on widgets.
        /// </summary>
        public enum DrawPoint { TopLeft, Top, TopRight, Left, Center, Right, BottomLeft, Bottom, BottomRight }
        /// <summary>
        /// Unused, intended for mouse interaction.
        /// </summary>
        public enum State { Up, Down, Selected, None };
        protected State state;
        /// <summary>
        /// The forground color of a widget.
        /// </summary>
        protected System.ConsoleColor forColor;
        /// <summary>
        /// The actual allignment of the widget.
        /// </summary>
        public DrawPoint Pin { get; internal set; }
        /// <summary>
        /// The current location of the widget bounds.
        /// </summary>
        public Rect Bounds { get; internal set; }
        /// <summary>
        /// The initilised bounds, un shifted by pinning.
        /// </summary>
        protected Rect baseBounds;
        /// <summary>
        /// Un-used, intended to speed up draw times of infrequently updated widgets.
        /// </summary>
        internal TFrame rendered;
        /// <summary>
        /// Update the forground color of a widget.
        /// </summary>
        /// <param name="color">The color to change to</param>
        public abstract void SetColor(System.ConsoleColor color);
        /// <summary>
        /// Draws the widget to the current frame of the Graphics object
        /// </summary>
        /// <param name="g">The Graphics to draw to</param>
        public abstract void Draw(Graphics g);
        /// <summary>
        /// Draws the widget to the current frame, after evaluating updating input.
        /// </summary>
        /// <param name="g">The Graphics to draw to</param>
        /// <param name="keyInfo">The User input to be handled.</param>
        public abstract void Draw(Graphics g, System.ConsoleKeyInfo keyInfo);
        /// <summary>
        /// Using the initilised bounds, re-align the widget.
        /// </summary>
        /// <param name="point">The direction to allign to</param>
        public abstract void PinTo(DrawPoint point);
        /// <summary>
        /// Modify the Initilised bounds, new bounds will be calculated based on the curent Pin
        /// </summary>
        /// <param name="rect">The new bounds</param>
        public abstract void ReSize(Rect rect);
    }
}
