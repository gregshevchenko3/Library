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
        private static int _ReadPagesCount = 0;
        private static bool _exit = false;
        private static Book _bk = null;
        static void Main(string[] args)
        {
            _keyPressed += Program.OnKeyPressed;
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            PageSettings pageSettings = new PageSettings();
            pageSettings.PaperSize = PaperStandards.GetSizeByName("A4");
            pageSettings.Landscape = false;
            pageSettings.Margins.Left = PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
            int short_margin = PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
            pageSettings.Margins.Top = pageSettings.Margins.Bottom = pageSettings.Margins.Right = short_margin;

            _bk = new Book("Якийсь там title", Ganres.DETECTIVE, Langs.ru_RU, DateTime.Now, @"book_01.txt", new Font("Arial", 14), pageSettings);
            _bk.StorageUpdatedEvent += page_count =>
            {
                if (_currentPage == -1)
                {
                    _currentPage = 0;
                    Console.Clear();
                    _bk.show(_currentPage);
                }
                _ReadPagesCount = page_count;
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
                    if (_currentPage > 0)
                    {
                        _currentPage--;
                        Console.Clear();
                        _bk.show(_currentPage);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (_currentPage != -1 && _currentPage < _ReadPagesCount)
                    {
                        _currentPage++;
                        Console.Clear();
                        _bk.show(_currentPage);
                    }
                    break;
                case ConsoleKey.F2:
                    if (_currentPage != -1)
                    {
                        _bk.ToBMPFile(_currentPage, $"{_bk.DocumentName}-page-{_currentPage + 1}");
                    }
                    break;
            }
        }
    }
}

//string row = "   Планета Ареана. Империя Ларгот. Столица Парн. Территория Академии магии Империи Ларгот. Полигон под зданием библиотеки и хранилища артефактов. Ранний вечер. Некоторое время спустя. ";
//string _content = "";

//SizeF size;
//Graphics gfx = Graphics.FromImage(new Bitmap(1, 1));

//Regex re = new Regex(" ");
//string[] words = re.Split(row);
//string tmp = "";
//int i = 0, first_word = 0, last_word = 0;
//while (first_word < words.Length - 1)
//{
//    size = new SizeF();
//    i = first_word;
//    //while (size.Width <= _bk.Width)
//    //{
//    //    if (i == words.Length)
//    //    {
//    //        last_word = i;
//    //        break;
//    //    }
//    //    tmp += ((words[i].Length == 0) ? " " : (words[i] + " "));
//    //    size = gfx.MeasureString(tmp, _bk.Font);
//    //    last_word = i++;
//    //}
//    tmp = "";
//    for (i = first_word; i < last_word; i++)
//    {
//        tmp += ((words[i].Length == 0) ? " " : (words[i] + " "));
//    }
//    size = gfx.MeasureString(tmp, _bk.Font);
//    tmp = "";
//    int num_spaces = (int)((_bk.Width - size.Width) / gfx.MeasureString(" ", _bk.Font).Width);
//    if (num_spaces > 50)
//        num_spaces = 0;
//    int space_count = num_spaces / (last_word - first_word - 1);
//    space_count = (space_count == 0) ? 1 : space_count;
//    for (i = first_word; i < last_word; i++)
//    {
//        if (words[i].Length == 0)
//        {
//            space_count = num_spaces / (last_word - first_word - 1);
//            space_count = (space_count == 0) ? 1 : space_count;
//            tmp += " ";
//        }
//        else if (num_spaces > 0)
//        {
//            tmp += words[i];
//            for (int k = 0; k < space_count; k++) tmp += " ";
//            num_spaces -= space_count;
//        }
//        else
//            tmp += words[i] + " ";
//    }
//    Console.WriteLine(tmp);
//    tmp = "";
//    first_word = last_word;
//}