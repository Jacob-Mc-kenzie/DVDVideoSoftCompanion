using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace CompactGraphics
{
    /// <summary>
    /// A datastructure containing the three screen buffers in a given frame.
    /// </summary>
    internal struct TFrame
    {
        public char[][] image;
        public ConsoleColor[][] background;
        public ConsoleColor[][] forground;

        public TFrame(int w, int h)
        {
            image = new char[h + 1][];
            background = new ConsoleColor[h + 1][];
            forground = new ConsoleColor[h + 1][];
            for (int i = 0; i < h; i++)
            {
                image[i] = new char[w + 1];
                forground[i] = new ConsoleColor[w + 1];
                background[i] = new ConsoleColor[w + 1];

                for (int j = 0; j <= w; j++)
                {
                    image[i][j] = ' ';
                    forground[i][j] = ConsoleColor.White;
                    background[i][j] = ConsoleColor.Black;
                }
            }
        }

        public TFrame(char[][] image, ConsoleColor[][] background, ConsoleColor[][] forground)
        {
            this.image = image;
            this.background = background;
            this.forground = forground;
        }
    }
    /// <summary>
    /// A compacted console visual libary of mine with GUI centered features.
    /// draws insperation from from https://stackoverflow.com/questions/19568127/hide-scrollbars-in-console-without-flickering 
    /// </summary>
    public class Graphics
    {
        /// 
        /// To add, carosell like barrier X for stratup, bound text box, text entry, lists, radio buttons, password entry.
        /// 
        ///
        private int framedelay = 3;
        private int maxQueueLength;
        private TFrame currentFrame;
        private Queue<TFrame> frameQueue;
        public int width { get; private set; }
        public int height { get; private set; }
        private bool keepgoing = true;
        private int frame_Counter = 0;
        private int fps = 0;
        private SafeFileHandle f;
        CharInfo[] buf;
        SmallRect rect;
        Thread updateThread;
        Thread updateFpsThread;
        /// <summary>
        /// The current number of frames processed in a second, 
        /// may not be accurate as no new frames are drawn if there are none in the queue.
        /// </summary>
        public int Fps
        {
            get { return fps; }
            set { }
        }
        /// <summary>
        /// Delay the main thread by this much if you want to prevent frame drops.
        /// </summary>
        public int FrameTime
        {
            get { return (fps > 0 ?  (900 /fps) : 0) + 1; }
            set { }
        }
        /// <summary>
        /// The current langth of the frame queue, aka how many frames behind are we, usually 20~50
        /// </summary>
        public int QueueLength
        {
            get { return frameQueue.Count; }
            set { }
        }
        private int last_frame_count = 0;

        public int FrameCap
        {
            get { return 1000 / (framedelay + 1); }
            set { framedelay = (value > 0 ? 1000/(value + 1)  : 1); }
        }

        #region native imports
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern SafeFileHandle CreateFile(
           string fileName,
           [MarshalAs(UnmanagedType.U4)] uint fileAccess,
           [MarshalAs(UnmanagedType.U4)] uint fileShare,
           IntPtr securityAttributes,
           [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
           [MarshalAs(UnmanagedType.U4)] int flags,
           IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }
        #endregion
        /// <summary>
        /// Initilises the graphics object.
        /// </summary>
        /// <param name="w">width of the screen</param>
        /// <param name="h">height of the screen</param>
        public Graphics(int w, int h)
        {
            Console.SetWindowSize(w, h);
            Console.CursorVisible = false;
            currentFrame = new TFrame(w, h);
            frameQueue = new Queue<TFrame>();
            buf = new CharInfo[w * h];
            rect = new SmallRect() { Left = 0, Top = 0, Right = (short)w, Bottom = (short)h };
            this.f = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            height = h;
            width = w;
            maxQueueLength = 20;

            ThreadStart fpsth = new ThreadStart(updateFps);
            updateFpsThread = new Thread(fpsth);
            updateFpsThread.IsBackground = true;
            updateFpsThread.Start();

            ThreadStart thref = new ThreadStart(FrameUpdater);
            updateThread = new Thread(thref);
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        public Graphics(int w, int h, int queueLength) :this(w,h)
        {
            maxQueueLength = queueLength;
        }

        private void updateFps()
        {
            while (keepgoing)
            {
                fps = (frame_Counter - last_frame_count);
                last_frame_count = frame_Counter;
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Updates the console window by comparing the last frame to the current one,
        /// if they differ modify those bits of the screen only.
        /// </summary>
        private void update(TFrame screen)
        {
            if (!f.IsInvalid)
            {
                int y, x;
                for (int i = 0; i < buf.Length; ++i)
                {
                    y = (int)i / width;
                    x = i % width;
                    buf[i].Attributes = (short)((int)screen.forground[y][x] | (((int)screen.background[y][x]) << 4));
                    buf[i].Char.UnicodeChar = screen.image[y][x];
                }
                bool b = WriteConsoleOutput(this.f, buf,
                          new Coord() { X = (short)width, Y = (short)height },
                          new Coord() { X = 0, Y = 0 },
                          ref rect);
                frame_Counter++;
            }
            else
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Console output handel is invalid");
            }
        }

        /// <summary>
        ///  Runs on a seperate thread and draws the desired frames. also removes the drawn frames from the queue.
        /// </summary>
        internal void FrameUpdater()
        {
            while (keepgoing)
            {
                if (frameQueue.Count >= 1)
                    update(frameQueue.Dequeue());
                Thread.Sleep(framedelay);
                
            }
        }
        /// <summary>
        /// For debuging only. forces the next frame to be drawn on the main thread.
        /// </summary>
        public void ForceDraw()
        {
            if (frameQueue.Count >= 1)
                update(frameQueue.Dequeue());
        }


        /// <summary>
        /// Queues up the current frame.
        /// </summary>
        public void pushFrame()
        {
            //while(frameQueue.Count > maxQueueLength)
            //{
            //    if(frameQueue.Count < maxQueueLength)
            //    {
            //        break;
            //    }
            //}
            if(frameQueue.Count < maxQueueLength)
            {
                frameQueue.Enqueue(currentFrame);
                currentFrame = new TFrame(width,height);
            }
            Thread.Sleep(FrameTime);
        }

        /// <summary>
        /// Stop trying to draw frames.
        /// </summary>
        public void quit()
        {
            keepgoing = false;
        }


        /// <summary>
        /// Add a character to the current frame.
        /// </summary>
        /// <param name="c"> the character to be drawn</param>
        /// <param name="color"> the color to draw the character</param>
        /// <param name="x">the x position from the left</param>
        /// <param name="y">the y position from the top</param>
        public void Draw(char c, ConsoleColor color, int x, int y)
        {
            if (x < width && y < height && x >= 0 && y >= 0)
            {
                currentFrame.image[y][x] = c;
                currentFrame.forground[y][x] = color;
            }
        }



        /// <summary>
        /// Add a string to the current frame.
        /// </summary>
        /// <param name="st">the string to draw</param>
        /// <param name="color">the color to draw the string</param>
        /// <param name="x">the x position from the left</param>
        /// <param name="y">the y position from the top</param>
        public void Draw(string st, ConsoleColor color, int x, int y)
        {
            if (y <= height)
            {
                for (int i = 0; i < st.Length; i++)
                {
                    if (x + i <= width && x + i >= 0)
                    {
                        currentFrame.image[y][x + i] = st[i];
                        currentFrame.forground[y][x + i] = color;
                    }
                }
            }

        }

        /// <summary>
        /// change the backgroud color at the specified point.
        /// </summary>
        /// <param name="c"> the character to be drawn</param>
        /// <param name="color"> the color to draw the character</param>
        /// <param name="x">the x position from the left</param>
        /// <param name="y">the y position from the top</param>
        public void DrawBackground(ConsoleColor color, int x, int y)
        {
            if (x <= width && y <= height && x >= 0 && y >= 0)
            {
                currentFrame.background[y][x] = color;
            }
        }

        /// <summary>
        /// Add a line with given ange and magnitiude to the frame.
        /// </summary>
        /// <param name="c">the character to draw</param>
        /// <param name="color">the color to draw the characters</param>
        /// <param name="x_init">the intial x coordinate to draw from</param>
        /// <param name="y_init">the initial y cooradinate to draw from</param>
        /// <param name="angle">the ange counter-clockwise from east to draw at</param>
        /// <param name="magnitude">the distance to draw the line</param>
        public void DrawLine(char c, ConsoleColor color, int x_init, int y_init, double angle, int magnitude)
        {
            Draw(c,color, x_init, y_init);
            int y_final, x_final;
            for (int i = 1; i <= magnitude; i++)
            {
                x_final = (int)(i * Math.Cos(angle * (Math.PI / 180.0))) + x_init;
                y_final = (int)(i * Math.Sin(angle * (Math.PI / 180.0))) + y_init;
                Draw(c,color, x_final, y_final);
            }
        }

        /// <summary>
        /// Add a line between two given points to the screen.
        /// </summary>
        /// <param name="c">the character to draw</param>
        /// <param name="color">the color to draw the characters</param>
        /// <param name="x_init">the intial x coordinate to draw from</param>
        /// <param name="y_init">the initial y cooradinate to draw from</param>
        /// <param name="x_final">the final x coordinate to draw to</param>
        /// <param name="y_final">the final y coordinate to draw to</param>
        public void DrawLine(char c, ConsoleColor color, int x_init, int y_init, int x_final, int y_final)
        {
            int x = x_final - x_init;
            int y = y_final - y_init;
            int magnitude = (int)Math.Sqrt(((Math.Pow(x, 2)) + (Math.Pow(y, 2))));
            double angle = Math.Atan((y / x));

            Draw(c,color, x_init, y_init);
            int y_fin, x_fin;
            for (int i = 1; i <= magnitude; i++)
            {
                x_fin = (int)(i * Math.Cos(angle * (Math.PI / 180.0))) + x_init;
                y_fin = (int)(i * Math.Sin(angle * (Math.PI / 180.0))) + y_init;
                Draw(c,color, x_fin, y_fin);
            }
        }

        #region polygon rotation from https://www.experts-exchange.com/questions/23316788/Rotate-a-2D-polygon-in-C.html
        public struct Point
        {
            public int X, Y;
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        /// <summary>
        /// Rotates a polygon formend by a array of points and returns the transformed shape.
        /// </summary>
        /// <param name="polygon">an array of points representing the points of the polygon.</param>
        /// <param name="centroid">the center of rotation</param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public Point[] rotatePolygon(Point[] polygon, Point centroid, double angle)
        {
            for (int i = 0; i < polygon.Length; i++)
            {
                polygon[i] = rotatePoint(polygon[i], centroid, angle);
            }

            return polygon;
        }

        public Point rotatePoint(Point point, Point centroid, double angle)
        {
            int x = centroid.X + (int)((point.X - centroid.X) * Math.Cos(angle * (Math.PI / 180.0)) - (point.Y - centroid.Y) * Math.Sin(angle * (Math.PI / 180.0)));

            int y = centroid.Y + (int)((point.X - centroid.X) * Math.Sin(angle * (Math.PI / 180.0)) + (point.Y - centroid.Y) * Math.Cos(angle * (Math.PI / 180.0)));

            return new Point(x, y);
        }
        #endregion
    }
}
