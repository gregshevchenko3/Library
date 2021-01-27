using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text.RegularExpressions;

namespace Library
{
    enum Ganres
    {
        LOVE_STORY,
        DETECTIVE,
        HOROR,
        SCI_FI
    }
    enum Langs
    {
        uk_UA,
        ru_RU,
        en_US,
    }
    public delegate void StorageUpdatedEvent(int pageCount);
    abstract class StorageInfo : IStorage
    {
        public Ganres Ganre { get; set; }
        public Langs Lang { get; set; }
        private DateTime _dateOfPublished;
        private string _title;
        private Font _font;
        private PaperSize _paper;
        private StorageReader _storageReader;
        private List<Page> _pages;
        public event StorageUpdatedEvent StorageUpdatedEvent;

        public DateTime DateOfPublished
        {
            protected set
            {
                if (value >= DateTime.Now)
                    throw new ArgumentException("Date is future");
                _dateOfPublished = value;
            }
            get
            {
                return _dateOfPublished;
            }
        }
        public int Width
        {
            get
            {
                return _paper.Width;
            }
        }
        public int Height
        {
            get
            {
                return _paper.Height;
            }
        }
        public Font Font
        {
            get
            {
                return _font;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
        }
        public int PagesCount
        {
            get
            {
                return _pages.Count;
            }
        }
        public StorageInfo(string title, Ganres ganre, Langs lang, DateTime publish, string pathToFile, string paper = "A5 148 x 210mm", 
            string FontFamily = "Arial", Single emSize = 16)
        {
            _title = title;
            Ganre = ganre;
            Lang = lang;
            DateOfPublished = publish;
            _pages = new List<Page>();
            _paper = PaperStandards.GetSizeByName(paper);
            _font = new Font(FontFamily, emSize);
            _storageReader = new StorageReader(pathToFile);
            _storageReader.eventHandler += read_line;
            _storageReader.read_line();
        }
        public void read_line(object sender, StorageReader.NextLineReadArgs args)
        {
            Page page;
            if (_pages.Count == 0) {
                page = new Page(this, _pages.Count + 1);
                _pages.Add(page);
            }
            else
                page = _pages.Last();
            string line = args.Line;
            do
            {
                line = page.Write(line);
                if (!string.IsNullOrEmpty(line))
                {
                    _pages[_pages.Count - 1] = page;
                    page = new Page(this, _pages.Count + 1);
                    _pages.Add(page);
                    if (this.StorageUpdatedEvent != null)
                    {
                        Task.Factory.StartNew(() => StorageUpdatedEvent(_pages.Count));
                    }
                }
            } 
            while (!string.IsNullOrEmpty(line));
        }
        public void show(int page = 0)
        {
            string str = _pages[page].Text;
            str = Regex.Replace(str, @"^", "\t\t\t", RegexOptions.Multiline);
            Console.WriteLine(str); 
        }
        
    }
}
