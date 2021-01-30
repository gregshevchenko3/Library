using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class PageContent
    {
        private string _content;
        private IPrintable _storage;
        private float _height = 0;
        public bool Ready { get {
                return _height <= _storage.Font.Height;
            } 
        }
        public string Text
        {
            get { return _content; }
        }
        /** 
         *  Виводе сторінку в консоль з врахуванням формату сторінки
         */
        public string Show()
        {
            string content = _content;
            string result = "";

            SizeF size = new SizeF();
            Graphics gfx = Graphics.FromImage(new Bitmap(1, 1));

            StringFormat format = new StringFormat();
            format.Trimming = StringTrimming.Word;
            int iCharacterFitted, iLinesFilled;

            size = gfx.MeasureString(_content, _storage.Font, new SizeF((float)_storage.Width, (float)_storage.Height), format, out iCharacterFitted, out iLinesFilled);
            if(iLinesFilled == 0)
                Console.WriteLine(new SizeF((float)_storage.Width, (float)_height));
            int charPerLine = iCharacterFitted / iLinesFilled, tmpCharPerLine;

            while (content.Length > charPerLine)
            {
                if (content[0] == ' ' || content[0] == '\n')
                    content = content.Substring(0);
                tmpCharPerLine = charPerLine;
                charPerLine = content.IndexOf('\n');

                if (charPerLine <= 0 || charPerLine > tmpCharPerLine)
                    charPerLine = tmpCharPerLine;
                else
                    charPerLine++;

                string line = content.Substring(0, charPerLine);
                result += line;
                if (charPerLine == tmpCharPerLine)
                    result += "\n";
               
                content = content.Substring(charPerLine);
                charPerLine = tmpCharPerLine;
            }
            if (content.Length > 0)
            {
                result += $"{content}\n";
            }
            return result;
        }
        /** 
         *  Метод, що ініціалізує сторінку з файлу.
         *  Визивається в обробнику події (EventHandler<>)  Document::OnReadLine(...)
         */
        public string Write(string row)
        {
            SizeF size = new SizeF();
            if (string.IsNullOrEmpty(row)) 
                return "";
            Graphics gfx = Graphics.FromImage(new Bitmap(1, 1));
            StringFormat format = new StringFormat();
            format.Trimming = StringTrimming.Word;
            int iCharacterFitted, iLinesFilled;
            size = gfx.MeasureString(row, _storage.Font, new SizeF((float)_storage.Width, (float)_height), format, out iCharacterFitted, out iLinesFilled);
            _content += $"{row.Substring(0, iCharacterFitted)}\n";
            _height -= size.Height;
            string ret = row.Substring(iCharacterFitted);
            return ret;
        }
        /**
         * Малює сторінку в BMP. Визивається методом Document::ToBMPFile()
         */
        public void Draw(Graphics gfx, Brush brush, Rectangle layoutRectangle)
        {
            SizeF size = new SizeF();
            StringFormat format = new StringFormat();
            format.Trimming = StringTrimming.Word;
            int iCharacterFitted, iLinesFilled;
            size = gfx.MeasureString(_content, _storage.Font, new SizeF((float)_storage.Width, (float)_storage.Height), format, out iCharacterFitted, out iLinesFilled);
            gfx.DrawString(_content, _storage.Font, brush, layoutRectangle, format);
        }
        public PageContent(IPrintable storage)
        {
            _storage = storage;
            _content = "";
            _height = _storage.Height;
        }
    }
}
