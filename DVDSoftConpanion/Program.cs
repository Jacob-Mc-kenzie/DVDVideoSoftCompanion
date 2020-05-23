using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompactGraphics;
using DataManagement;

namespace DVDSoftConpanion
{
    public class activeMember
    {
        int memberindex;
        public int Value { get { return memberindex; } }
        public activeMember(int index)
        {
            this.memberindex = index;
        }
        public void Update(int index)
        {
            this.memberindex = index;
        }
    }
    class Program
    {
            public static MemberCollection members = new MemberCollection();
            public static MovieCollection movieCollection = new MovieCollection();
            public static activeMember CurrentMember = new activeMember(-1);
        #region Circles
        public struct circle
        {
            public double k;
            public double h;
            public double r;
            public int max_r;
            public bool dir_r;
            public double counter;
            public ConsoleColor Color;
        };
        public static ConsoleColor[] consoleColors = { ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Magenta, ConsoleColor.DarkMagenta, ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.DarkYellow };
        public static circle[] GetCircles()
        {
            Random rng = new Random();
            circle[] circles = new circle[8];//rng.Next(5, 12)];

            for (int i = 0; i < circles.Length; i++)
            {
                circles[i].k = (double)(rng.Next(40));
                circles[i].h = (double)(rng.Next(40));
                circles[i].r = (double)(rng.Next(6)) + 2.5;
                circles[i].max_r = (rng.Next(20)) + 4;
                circles[i].counter = 0;
                circles[i].Color = consoleColors[rng.Next(consoleColors.Length)];
                circles[i].dir_r = false;
            }
            return circles;
        }
        public static void Draw_circles(circle[] circles, Graphics graphics, int width, int height)
        {
            double init_x = 0;
            Random rng = new Random();
            for (int i = 0; i < circles.Length; i++)
            {
                double k = circles[i].k;
                double h = circles[i].h;
                double r = circles[i].r;
                circles[i].counter += 0.2;
                //Console.ForegroundColor = circles[i].Color;

                init_x = h - (r + 0);
                //draw each x cordinant.
                for (double x = init_x; x < h + r; x += 0.4)
                {

                    double y_pos = k + Math.Sqrt((r * r) - ((x - h) * (x - h)));
                    double y_neg = k - Math.Sqrt((r * r) - ((x - h) * (x - h)));
                    graphics.Draw('o', circles[i].Color, (int)x, (int)y_pos);
                    graphics.Draw('o', circles[i].Color, (int)x, (int)y_neg);
                }
                //if its done a cycyle re generate.
                if (r < 2.0 && circles[i].dir_r == true)
                {
                    circles[i].k = (double)(rng.Next(height));
                    circles[i].h = (double)(rng.Next(width));
                    circles[i].r = (double)(rng.Next(6)) + 2.5;
                    circles[i].max_r = (rng.Next(10)) + 9;
                    circles[i].counter = 0;
                    circles[i].Color = consoleColors[rng.Next(consoleColors.Length)];
                    circles[i].dir_r = false;
                    continue;
                }
                else if (r > circles[i].max_r - 2)
                    circles[i].dir_r = true;
                //sine for smothing.
                circles[i].r = Math.Abs(Math.Sin(circles[i].counter)) * (double)circles[i].max_r;
            }

        }
        public static void Draw_circles(circle[] circles, Graphics graphics, int width, int height, int fill)
        {
            double init_x = 0;
            Random rng = new Random();
            for (int i = 0; i < circles.Length; i++)
            {
                double k = circles[i].k;
                double h = circles[i].h;
                double r = circles[i].r;
                circles[i].counter += 0.2;
                //Console.ForegroundColor = circles[i].Color;

                init_x = h - (r + 0);
                //draw each x cordinant.
                for (double x = init_x; x < h + r; x += 0.4)
                {

                    double y_pos = k + Math.Sqrt((r * r) - ((x - h) * (x - h)));
                    for (int j = (int)k; j < y_pos; j++)
                    {
                        graphics.Draw('o', circles[i].Color, (int)x, (int)j);
                    }
                    double y_neg = k - Math.Sqrt((r * r) - ((x - h) * (x - h)));
                    for (int z = (int)k; z > y_neg; z--)
                    {
                        graphics.Draw('o', circles[i].Color, (int)x, (int)z);
                    }
                    //graphics.Draw('o', circles[i].Color, (int)x, (int)y_pos);
                    //graphics.Draw('o', circles[i].Color, (int)x, (int)y_neg);
                }
                //if its done a cycyle re generate.
                if (r < 2.0 && circles[i].dir_r == true)
                {
                    circles[i].k = (double)(rng.Next(height));
                    circles[i].h = (double)(rng.Next(width));
                    circles[i].r = (double)(rng.Next(6)) + 2.5;
                    circles[i].max_r = (rng.Next(10)) + 9;
                    circles[i].counter = 0;
                    circles[i].Color = consoleColors[rng.Next(consoleColors.Length)];
                    circles[i].dir_r = false;
                    continue;
                }
                else if (r > circles[i].max_r - 2)
                    circles[i].dir_r = true;
                //sine for smothing.
                circles[i].r = Math.Abs(Math.Sin(circles[i].counter)) * (double)circles[i].max_r;
            }

        }
        public static void Drawbg_circles(circle[] circles, Graphics graphics, int width, int height, int fill)
        {
            double init_x = 0;
            Random rng = new Random();
            for (int i = 0; i < circles.Length; i++)
            {
                double k = circles[i].k;
                double h = circles[i].h;
                double r = circles[i].r;
                circles[i].counter += 0.2;
                //Console.ForegroundColor = circles[i].Color;

                init_x = h - (r + 0);
                //draw each x cordinant.
                for (double x = init_x; x < h + r; x += 0.4)
                {

                    double y_pos = k + Math.Sqrt((r * r) - ((x - h) * (x - h)));
                    for (int j = (int)k; j < y_pos; j++)
                    {
                        graphics.DrawBackground(circles[i].Color, (int)x, (int)j);
                    }
                    double y_neg = k - Math.Sqrt((r * r) - ((x - h) * (x - h)));
                    for (int z = (int)k; z > y_neg; z--)
                    {
                        graphics.DrawBackground(circles[i].Color, (int)x, (int)z);
                    }
                    //graphics.Draw('o', circles[i].Color, (int)x, (int)y_pos);
                    //graphics.Draw('o', circles[i].Color, (int)x, (int)y_neg);
                }
                //if its done a cycyle re generate.
                if (r < 2.0 && circles[i].dir_r == true)
                {
                    circles[i].k = (double)(rng.Next(height));
                    circles[i].h = (double)(rng.Next(width));
                    circles[i].r = (double)(rng.Next(6)) + 2.5;
                    circles[i].max_r = (rng.Next(10)) + 9;
                    circles[i].counter = 0;
                    circles[i].Color = consoleColors[rng.Next(consoleColors.Length)];
                    circles[i].dir_r = false;
                    continue;
                }
                else if (r > circles[i].max_r - 2)
                    circles[i].dir_r = true;
                //sine for smothing.
                circles[i].r = Math.Abs(Math.Sin(circles[i].counter)) * (double)circles[i].max_r;
            }

        }
        #endregion
        static void Main(string[] args)
        {
            Graphics g = new Graphics(140, 60);
            int stateTransitioner;
            bool keepLooping = true;
            Exception errorHolder = new Exception("Unknown");
            string t;
            members.RegisterMember(new Member("John", "Smith", "040220912", "1111", "23 applebe lane"), out t);
            members.RegisterMember(new Member("James", "Smith", "040220912", "1234", "23 applebe lane"), out t);
            members.RegisterMember(new Member("Johnny", "Craig", "040220912", "9999", "23 applebe lane"), out t);
            movieCollection.AddDVD(new Movie("Jonny Bravo vol 1", new string[] { "Johnny Bravo"}, "Alfred Hitchcock", new int[] { 10, 30, 45 }, MovieGenre.Action, MovieClass.ParentalGuidance, 99));
            movieCollection.AddDVD(new Movie("Jonny Bravo vol 2", new string[] { "Johnny Bravo"}, "Alfred Hitchcock", new int[] { 10, 50, 19 }, MovieGenre.Action, MovieClass.ParentalGuidance, 23));
            movieCollection.AddDVD(new Movie("Jonny Bravo vol 3", new string[] { "Johnny Bravo"}, "Alfred Hitchcock", new int[] { 10, 18, 25 }, MovieGenre.Action, MovieClass.ParentalGuidance, 9));
            movieCollection.AddDVD(new Movie("Jonny Bravo vol 4", new string[] { "Johnny Bravo"}, "Alfred Hitchcock", new int[] { 10, 23, 43 }, MovieGenre.Action, MovieClass.ParentalGuidance, 01));
            movieCollection.AddDVD(new Movie("The Muppets Take Manhattan", new string[] { "Kirmet"}, "Steven Speilberg", new int[] { 99, 00, 32 }, MovieGenre.Family, MovieClass.Genral, 236));
            movieCollection.AddDVD(new Movie("Jonny Bravo: The Movie", new string[] { "Johnny Bravo"}, "John Smith", new int[] { 90, 03, 01 }, MovieGenre.Action, MovieClass.MatureAcc, 999));

            preMenuInstructionsLoop(g);
            g.FrameCap = 200;
            stateTransitioner = MainMenuLoop(g);
            while (keepLooping)
            {
                switch (stateTransitioner)
                {
                    case -1:
                        stateTransitioner = MainMenuLoop(g);
                        break;
                    case 1:
                        stateTransitioner = StaffLoginLoop(g);
                        break;
                    case 2:
                        stateTransitioner = ClientLoginLoop(g);
                        break;
                    case 3:
                        g.quit();
                        return;
                    case 4:
                        stateTransitioner = StaffMenuLoop(g);
                        break;
                    case 5:
                        stateTransitioner = AddDVDMenuLoop(g);
                        break;
                    case 6:
                        stateTransitioner = RemoveDVDMenuLoop(g);
                        break;
                    case 7:
                        stateTransitioner = RegisterMemberMenuLoop(g);
                        break;
                    case 8:
                        stateTransitioner = LookupPhoneMenuLoop(g);
                        break;
                    case 9:
                        stateTransitioner = ClientMenuLoop(g);
                        break;
                    case 10:
                        stateTransitioner = DisplayAllMenuLoop(g);
                        break;
                    case 11:
                        stateTransitioner = BorrowDVDMenuLoop(g);
                        break;
                    case 12:
                        stateTransitioner = ReturnDVDMenuLoop(g);
                        break;
                    case 13:
                        stateTransitioner = ListBorrowedMenuLoop(g);
                        break;
                    case 14:
                        stateTransitioner = ListTopTenMenuLoop(g);
                        break;
                    default:
                        ErrorPageLoop(g, errorHolder);
                        keepLooping = false;
                        break;
                    

                }
            }
            g.quit();

        }
        static void preMenuInstructionsLoop(Graphics g)
        {
            string message = "This application uses advanced console formatting, please don't re-sise the window";
            string message2 = "Use the TAB RETURN & ESC keys to navigate feilds and menus";
            string message3 = "items denoted with ')' display the interaction key on the left e.g. '1)' requires the '1' key to interact";
            string message4 = "Press the RETRUN key to continue";
            int x = (g.width / 2);
            int y = (g.height / 2)-1;
            circle[] circles = GetCircles();
            g.FrameCap = 40;
            while (true)
            {
                Draw_circles(circles, g, g.width, g.height);
                g.Draw(message, ConsoleColor.Yellow, x - (message.Length / 2),y);
                g.Draw(message2, ConsoleColor.Yellow, x - (message2.Length / 2), y + 1);
                g.Draw(message3, ConsoleColor.Yellow, x - (message3.Length / 2), y + 2);
                g.Draw(message4, ConsoleColor.Green, x - (message4.Length / 2), y + 3);
                g.Draw($"Fps: {g.Fps}", ConsoleColor.White, 0, 0);
                g.Draw($"FrameTime: {g.FrameTime}", ConsoleColor.White, 0, 1);
                g.Draw($"QueueLenght: {g.QueueLength}", ConsoleColor.White, 0, 2);
                g.pushFrame();
                if (Console.KeyAvailable)
                {
                    if(Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            }
            

        }
        static int MainMenuLoop(Graphics g)
        {
            Mainmenu mm = new Mainmenu(g);
            return GenericMenu(g, mm);
        }
        static int StaffLoginLoop(Graphics g)
        {
            StaffLogin mm = new StaffLogin(g);
            return GenericMenu(g, mm);
        }
        static int ClientLoginLoop(Graphics g)
        {
            ClientLogin mm = new ClientLogin(g);
            return GenericMenu(g, mm);
        }
        static int StaffMenuLoop(Graphics g)
        {
            Staffmenu mm = new Staffmenu(g);
            return GenericMenu(g, mm);
        }
        static int ClientMenuLoop(Graphics g)
        {
            Clientmenu mm = new Clientmenu(g);
            return GenericMenu(g, mm);
        }
        static int AddDVDMenuLoop(Graphics g)
        {
            AddDVDMenu mm = new AddDVDMenu(g);
            return GenericMenu(g, mm);
        }
        static int RemoveDVDMenuLoop(Graphics g)
        {
            RemoveDVDMenu mm = new RemoveDVDMenu(g);
            return GenericMenu(g, mm);
        }
        static int RegisterMemberMenuLoop(Graphics g)
        {
            RegisterClientMenu mm = new RegisterClientMenu(g);
            return GenericMenu(g, mm);
        }
        static int LookupPhoneMenuLoop(Graphics g)
        {
            PhoneLookupMenu mm = new PhoneLookupMenu(g);
            return GenericMenu(g, mm);
        }
        static int DisplayAllMenuLoop(Graphics g)
        {
            DisplayMoviesMenu mm = new DisplayMoviesMenu(g);
            return GenericMenu(g, mm);
        }
        static int BorrowDVDMenuLoop(Graphics g)
        {
            BorrowDVDMenu mm = new BorrowDVDMenu(g);
            return GenericMenu(g, mm);
        }
        static int ReturnDVDMenuLoop(Graphics g)
        {
            ReturnDVDMenu mm = new ReturnDVDMenu(g);
            return GenericMenu(g, mm);
        }
        static int ListBorrowedMenuLoop(Graphics g)
        {
            BorrowedDVDMenu mm = new BorrowedDVDMenu(g);
            return GenericMenu(g, mm);
        }
        static int ListTopTenMenuLoop(Graphics g)
        {
            DisplayTopTenMenu mm = new DisplayTopTenMenu(g);
            return GenericMenu(g, mm);
        }
        static int ErrorPageLoop(Graphics g, Exception e)
        {
            return -1;
        }
        static int GenericMenu(Graphics g, Menu M)
        {
            while(M.Status == 0)
            {
                g.Draw($"Fps: {g.Fps}", ConsoleColor.White, 0, 0);
                g.Draw($"queuelenght: {g.QueueLength}", ConsoleColor.White, 0, 2);
                g.Draw($"frameTime: {g.FrameTime}", ConsoleColor.White, 0, 1);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey(false);
                    switch (info.Key)
                    {
                        case ConsoleKey.End:
                            ConsoleKeyInfo key = Console.ReadKey(false);
                            return int.Parse(key.KeyChar.ToString());
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.LeftWindows:
                        case ConsoleKey.RightWindows:
                            break;
                        default:
                            M.StepFrame(info);
                            break;

                    }
                }
                else
                {
                    M.StepFrame();
                }
                g.pushFrame();
            }
            return M.Status;
        }
    }
}
