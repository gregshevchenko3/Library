using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Library
{
    static class PaperStandarts
    {
        private readonly static Dictionary<string, Size> _paperFormats =
                new Dictionary<string, Size>
                {
                    {"A_0", new Size(1189, 841) },
                    {"A_1", new Size(841, 594 ) },
                    {"A_2", new Size(594, 420) },
                    {"A_3", new Size(420, 297) },
                    {"A_4", new Size(297, 210) },
                    {"A_5", new Size(210, 148) },
                    {"A_6", new Size(148, 105) },
                };
        public static Size getSize(string paperFormat)
        {
            if (_paperFormats.ContainsKey(paperFormat))
            {
                return _paperFormats[paperFormat]; //return Size
            }
            else
            {
                throw new ArgumentException("Неподдерживаемый формат");
            }
        }
        public static KeyValuePair<string, Size> getPaperFormat(string paperFormat)
        {
            if (_paperFormats.ContainsKey(paperFormat))
            {
                return new KeyValuePair<string, Size>(paperFormat, _paperFormats[paperFormat]);
            }
            else
            {
                throw new ArgumentException("Неподдерживаемый формат");
            }
        }
        public static Dictionary<string, Size> allFormats
        {
            get
            {
                return _paperFormats;
            }
        }
    }
}
