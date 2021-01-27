using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    public delegate void KeyPressedDelegate(ConsoleKeyInfo key);
    class Program
    {
        private static event KeyPressedDelegate _keyPressed;
        private static int _currentPage = -1;
        private static bool _exit = false;
        private static Book _bk = null;
        static void Main(string[] args)
        {
            _keyPressed += Program.OnKeyPressed;
            _bk = new Book("Якийсь там title", Ganres.DETECTIVE, Langs.ru_RU, DateTime.Now, @"book_01.txt", "A5");
            _bk.StorageUpdatedEvent += page_count =>
            {
                if(_currentPage < 0)
                {
                    _currentPage = 0;
                    Console.Clear();
                    _bk.show(_currentPage);
                }
            };
            while (!_exit)
            {
                if (Console.KeyAvailable)
                {
                    _keyPressed(Console.ReadKey(true));
                }
            }

        }
        public static void OnKeyPressed(ConsoleKeyInfo key) {
            switch(key.Key)
            {
                case ConsoleKey.Escape:
                    _exit = true;
                    break;
                case ConsoleKey.LeftArrow:
                    if (_currentPage > 0) _currentPage--;
                    Console.Clear();
                    _bk.show(_currentPage);
                    break;
                case ConsoleKey.RightArrow:
                    if (_currentPage < _bk.PagesCount) _currentPage++;
                    Console.Clear();
                    _bk.show(_currentPage);
                    break;
            }
        }
    }
}
