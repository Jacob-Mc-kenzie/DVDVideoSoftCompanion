namespace CompactGraphics
{

    public abstract class Widget
    {
        public enum DrawPoint { TopLeft, Top, TopRight, Left, Center, Right, BottomLeft, Bottom, BottomRight }
        public enum State { Up, Down, Selected, None };
        protected State state;
        protected System.ConsoleColor forColor;
        public DrawPoint Pin { get; internal set; }
        public Rect Bounds { get; internal set; }
        protected Rect baseBounds;
        internal TFrame rendered;
        public abstract void SetColor(System.ConsoleColor color);
        public abstract void Draw(Graphics g);
        public abstract void Draw(Graphics g, System.ConsoleKeyInfo keyInfo);
        public abstract void PinTo(DrawPoint point);
        public abstract void ReSize(Rect rect);
    }
}
