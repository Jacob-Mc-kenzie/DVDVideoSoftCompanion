﻿using CompactGraphics;
using DataManagement;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DVDSoftConpanion
{
    class RegisterClientMenu : Menu
    {
        private Textbox cursor;
        private TextEntry FistName;
        private TextEntry SurName;
        private TextEntry Address;
        private TextEntry Phone;
        private TextEntry Password;
        private Textbox response;

        private TextEntry[] textEntries;
        private int tabIndex;
        private Rect baseCursorBounds;
        public RegisterClientMenu(Graphics g)
        {
            onPage = new List<Widget>();
            this.g = g;
            status = 0;
            tabIndex = 0;
            Rect r = new Rect(g.width / 2, (g.width / 2) + 30, g.height / 2, g.height / 2 + 20);
            Rect rect = r.OffsetPin(Widget.DrawPoint.Center);
            onPage.Add(new Frame('=', ConsoleColor.Yellow, ConsoleColor.Black, r, Widget.DrawPoint.Center));
            rect.y1 = rect.y1 - 1;
            rect.y2 = rect.y1 + 1;
            onPage.Add(new Textbox("Add Member", ConsoleColor.Green, rect, Widget.DrawPoint.TopLeft));
            Rect Lt = r.OffsetPin(Widget.DrawPoint.Center);
            Lt.x1++;
            Lt.y1 += 2;
            onPage.Add(new Textbox("FirstName", Lt));
            Lt.y1++;
            FistName = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            cursor = new Textbox("~", ConsoleColor.DarkYellow, Lt, Widget.DrawPoint.TopLeft);
            onPage.Add(cursor);
            baseCursorBounds = cursor.Bounds;
            onPage.Add(FistName);
            Lt.y1++;
            onPage.Add(new Textbox("Surname", Lt));
            Lt.y1++;
            SurName = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(SurName);
            Lt.y1++;
            onPage.Add(new Textbox("Address", Lt));
            Lt.y1++;
            Address = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(Address);
            Lt.y1++;
            onPage.Add(new Textbox("PhoneNumber", Lt));
            Lt.y1++;
            Phone = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(Phone);
            Lt.y1++;
            onPage.Add(new Textbox("Password", Lt));
            Lt.y1++;
            Password = new TextEntry(Lt.x1 + 1, Lt.y1, 25);
            onPage.Add(Password);
            Lt.y1 += 2;
            response = new Textbox("", Lt);
            onPage.Add(response);
            Lt.y1 += 3;
            onPage.Add(new Textbox("RETURN) Add", ConsoleColor.Cyan, Lt, Widget.DrawPoint.TopLeft));
            Lt.y1++;
            onPage.Add(new Textbox("ESC) Cancel", ConsoleColor.Gray, Lt, Widget.DrawPoint.TopLeft));
            textEntries = new TextEntry[] { FistName, SurName, Address, Phone, Password};
        }

        public override void StepFrame(ConsoleKeyInfo keyinfo)
        {
            base.StepFrame();
            switch (keyinfo.Key)
            {
                case ConsoleKey.Enter:
                    int i = ValidateData();
                    if (i == 5)
                        status = 4;
                    else if (i != 6)
                        textEntries[i].Flash(ConsoleColor.Red);
                    break;
                case ConsoleKey.Tab:
                case ConsoleKey.DownArrow:
                case ConsoleKey.PageDown:
                    tabIndex = (tabIndex + 1) % textEntries.Length;
                    Rect bounds = baseCursorBounds;
                    bounds.y1 = baseCursorBounds.y1 + (2 * tabIndex);
                    cursor.ReSize(bounds);
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.PageUp:
                    tabIndex = (tabIndex - 1) % textEntries.Length;
                    Rect bound = baseCursorBounds;
                    bound.y1 = baseCursorBounds.y1 + (2 * tabIndex);
                    cursor.ReSize(bound);
                    break;
                case ConsoleKey.Escape:
                    status = 4;
                    break;
                default:
                    textEntries[tabIndex].Draw(g, keyinfo);
                    break;
            }
        }

        public int ValidateData()
        {
            if (!Regex.IsMatch(FistName.Text, @"[A-Z][a-z]+"))
            {
                return 0;
            }
            if (!Regex.IsMatch(SurName.Text, @"[A-Z][a-z]+"))
            {
                return 1;
            }
            if(!Regex.IsMatch(Address.Text,@"\A[A-Z]?[0-9]+[A-Z]? \d*[A-Za-z ]+"))
            {
                return 2;
            }
            if (!Regex.IsMatch(Phone.Text, @"\+?[0-9]{8,}"))
            {
                return 3;
            }
            if (!Regex.IsMatch(Password.Text, @"\A[0-9]{4}$"))
            {
                return 4;
            }
            Program.members.RegisterMember(new Member(FistName.Text, SurName.Text, Phone.Text, Password.Text, Address.Text), out string message);
            if(message != "Success")
            {
                response.SetText(message);
                response.SetColor(ConsoleColor.Red);
                return 6;
            }
            return 5;
        }
    }
}