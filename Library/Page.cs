using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Page : INumered
    {
        private string _content;
        public int _number;
        private IStorage _storage;
        public int Number { 
            get { return _number; }
        }
        public string Text
        {
            get { return _content; }
        }
        public string Write(string row)
        {
            SizeF size = new SizeF();
            if (string.IsNullOrEmpty(row)) return "";
            Graphics gfx = Graphics.FromImage(new Bitmap(1, 1));
            size = gfx.MeasureString(row, _storage.Font);

            int charPerLine = (int)Math.Truncate(row.Length / (size.Width / _storage.Width));

            while (row.Length > charPerLine && size.Height < _storage.Height)
            {
                string line;
                do
                {
                    line = row.Substring(0, charPerLine);
                    size = gfx.MeasureString(line, _storage.Font);
                    if (size.Width > _storage.Width)
                        charPerLine--;
                }
                while (size.Width > _storage.Width);

                _content += row.Substring(0, charPerLine) + '\n';
                size = gfx.MeasureString(_content, _storage.Font);
                row = row.Substring(charPerLine);
            }
            if (size.Height >= _storage.Height)
                return row;
            _content += row + "\n";
            size = gfx.MeasureString(_content, _storage.Font);

            return "";
        }
        public Page(IStorage storage, int number)
        {
            _storage = storage;
            _number = number;
            _content = "";
        }
    }
}
