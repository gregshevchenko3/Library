using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
    
    abstract class Document : PrintDocument, IPrintable
    {
        public Ganres Ganre { get; set; }
        public Langs Lang { get; set; }
        private DateTime _dateOfPublished;
        private Font _font;
        private StorageReader _storageReader;
        private List<PageContent> _pages;
        public event StorageUpdatedEvent StorageUpdatedEvent;
        /*
         * System.Drawing.Printing.PrintDocument.DocumentName is used instead of this property
         * 
         * //private string _title; 
         */
        /*
         * System.Drawing.Printing.PrintDocument.DefaultPageSettings is used instead of this property
         * 
         * //private PaperSize _paper;
         */

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
                return DefaultPageSettings.Bounds.Width - DefaultPageSettings.Margins.Left - DefaultPageSettings.Margins.Right;
            }
        }
        public int Height
        {
            get
            {
                return DefaultPageSettings.Bounds.Height - DefaultPageSettings.Margins.Top - DefaultPageSettings.Margins.Bottom;
            }
        }
        public Font Font
        {
            get
            {
                return _font;
            }
        }
        public int PagesCount
        {
            get
            {
                return _pages.Count;
            }
        }
        public Document(string title, Ganres ganre, Langs lang, DateTime publish, string pathToFile, Font font = null, PageSettings pg_settings = null)
        {
            DocumentName = title;
            Ganre = ganre;
            Lang = lang;
            DateOfPublished = publish;
            _pages = new List<PageContent>();
            if (pg_settings != null)
                DefaultPageSettings = pg_settings;
            else
            {
                DefaultPageSettings.PaperSize = PaperStandards.GetSizeByName("A5");
                DefaultPageSettings.Landscape = false;
                DefaultPageSettings.Margins.Left = PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
                int short_margin = PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
                DefaultPageSettings.Margins.Top = DefaultPageSettings.Margins.Bottom = DefaultPageSettings.Margins.Right = short_margin;
            }
            if (font != null)
                _font = font;
            else
            {
                _font = new Font("Arial", 14);
            }
            _storageReader = new StorageReader(pathToFile);
            _storageReader.eventHandler += OnReadLine;
            _storageReader.read_line();
        }
        public void OnReadLine(object sender, StorageReader.NextLineReadArgs args)
        {
            PageContent page;
            if (_pages.Count == 0)
            {
                page = new PageContent(this);
                _pages.Add(page);
            }
            else
                page = _pages.Last();
            if (_pages[_pages.Count - 1].Ready)
            {
                this.StorageUpdatedEvent(_pages.Count);
                page = new PageContent(this);
                _pages.Add(page);
            }
            if (!_pages[_pages.Count - 1].Ready)
            {
                string line = args.Line;
               while (!string.IsNullOrEmpty(line)) { 
                    line = page.Write(line);
                    _pages[_pages.Count - 1] = page;
                    if (!string.IsNullOrEmpty(line))
                    {
                        this.StorageUpdatedEvent(_pages.Count);
                        page = new PageContent(this);
                        _pages.Add(page);
                    }
               }
            }
        }
        public void show(int page = 0)
        {
            string str = _pages[page].Show();
            str = Regex.Replace(str, @"^", "\t\t\t", RegexOptions.Multiline);
            Console.WriteLine(str);
        }
        public void ToBMPFile(int page = 0, string filename = null)
        {
            using (Bitmap bmp = new Bitmap(DefaultPageSettings.PaperSize.Width, DefaultPageSettings.PaperSize.Height))
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    Brush br = Brushes.Black;
                    Pen pen = new Pen(br, 1);

                    gfx.FillRectangle(Brushes.White, 
                        new RectangleF(new PointF(0, 0), new SizeF(DefaultPageSettings.PaperSize.Width, DefaultPageSettings.PaperSize.Height)));
                    // границі сторінки
                    gfx.DrawRectangle(pen, new Rectangle(1, 1, DefaultPageSettings.Bounds.Width - 4, DefaultPageSettings.Bounds.Height - 4));
                    // поля на сторінці
                    Rectangle layout = new Rectangle(DefaultPageSettings.Margins.Left, DefaultPageSettings.Margins.Top, Width, Height);
                    gfx.DrawRectangle(pen, layout);
                    // Текст на сторінці
                    _pages[page].Draw(gfx, br, layout);
                    
                }
                bmp.Save(Environment.CurrentDirectory + $"\\{filename}.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }
    }
}
