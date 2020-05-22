﻿using System;
using System.Collections.Generic;
namespace CompactGraphics
{
    public class Textbox : Widget
    {
        List<string> lines;
        private System.ConsoleColor flashColor;
        private System.Timers.Timer eventDelay;

        public Textbox(string text, Rect r)
        {
            eventDelay = new System.Timers.Timer(200);
            this.baseBounds = r;
            this.Bounds = r;
            lines = text.Wrap(Math.Abs(r.x2 - r.x1));
            forColor = ConsoleColor.White;
            this.Pin = DrawPoint.TopLeft;
        }

        public Textbox(string text,ConsoleColor forcolor, Rect r, DrawPoint p) :this(text,r)
        {
            this.forColor = forcolor;
            this.Pin = p;
            Bounds = baseBounds.OffsetPin(p);
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

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if(Bounds.y1 + i <= Bounds.y2)
                {
                    g.Draw(lines[i], forColor, Bounds.x1, Bounds.y1 + i);
                }
            }

        }
        public void SetText(string text)
        {
            lines = text.Wrap(Math.Abs(baseBounds.x2 - baseBounds.x1));
        }
        public override void ReSize(Rect rect)
        {
            this.baseBounds = rect;
            this.Bounds = rect.OffsetPin(this.Pin);
        }
        public override void PinTo(DrawPoint point)
        {
            this.Pin = point;
            this.Bounds = baseBounds.OffsetPin(Pin);
        }

        public override void Draw(Graphics g, ConsoleKeyInfo keyInfo)
        {
            Draw(g);
            //ToDo
        }

        public override void SetColor(ConsoleColor color)
        {
            this.forColor = color;
        }
    }

}