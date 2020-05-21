namespace CompactGraphics
{
    public class TextEntry : Widget
    {
        private string text;
        private System.ConsoleColor forColor;
        private System.ConsoleColor flashColor;
        public string Text { get { return text; } }
        private System.Timers.Timer eventDelay;
        /// <summary>
        /// Creates a Text entry widget, single line with scrolling overflow.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="maxlenght"></param>
        public TextEntry(int x, int y, int maxlenght)
        {
            eventDelay = new System.Timers.Timer(200);
            this.text = "";
            this.baseBounds = new Rect(x, x + maxlenght, y, y + 1);
            this.pin = DrawPoint.TopLeft;
            this.bounds = new Rect(x, x + maxlenght, y, y + 1);
            this.forColor = System.ConsoleColor.White;
        }
        public TextEntry(int x, int y, int maxlenght, System.ConsoleColor forground, string placeholder) : this(x,y,maxlenght)
        {
            this.forColor = forground;
            this.text = placeholder;
        }
        /// <summary>
        /// Change the forgroud color of the specified widget for 200 ms
        /// </summary>
        /// <param name="color">the color to flash to</param>
        public void Flash(System.ConsoleColor color)
        {
            flashColor = forColor;
            forColor = color;
            eventDelay.Enabled = true;
            eventDelay.Elapsed += Delayed_Flash;
            eventDelay.AutoReset = false;
        }
        /// <summary>
        /// Callback for the event timer.
        /// </summary>
        private void Delayed_Flash(object sender, System.Timers.ElapsedEventArgs e)
        {
            forColor = flashColor;
        }
        /// <summary>
        /// Draws the text entry feild to the screen, draws in reverse from right to left, to enable easier text overflow.
        /// </summary>
        /// <param name="g">the graphics object to draw to</param>
        public override void Draw(Graphics g)
        {
            int diffrence = (bounds.x2 - bounds.x1);
            int iDif;
            for (int i = bounds.x2; i >= bounds.x1; i--)
            {
                iDif = i - bounds.x1;
                // if the length of the text is greater than the width of the text box offset the 0 point by the overlap.
                if(text.Length > diffrence)
                {
                    g.Draw(text[iDif+(text.Length - diffrence-1)], forColor, i, bounds.y1);
                }
                else
                {
                    //otherwise draw either the text of the unflled character.
                    if(iDif < text.Length)
                    {
                        g.Draw(text[iDif], forColor, i, bounds.y1);
                    }
                    else
                    {
                      g.Draw('_', forColor, i, bounds.y1);
                    }
                }
                
            }
        }
        /// <summary>
        /// Update the text entry with a key input then draw to the screen buffer.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="keyInfo"></param>
        public override void Draw(Graphics g, System.ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key != System.ConsoleKey.Backspace)
            {
                text += keyInfo.KeyChar;
            }
            else if (keyInfo.Key == System.ConsoleKey.Backspace)
            {
                if(text.Length > 0)
                    text = text.Remove(text.Length - 1);
            }
            Draw(g);
        }

        public override void reSize(Rect rect)
        {
            this.baseBounds = rect;
            this.bounds = rect.OffsetPin(this.pin);
        }
        public override void PinTo(DrawPoint point)
        {
            this.pin = point;
            this.bounds = baseBounds.OffsetPin(pin);
        }
    }
}
