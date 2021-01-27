using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Library
{
    static class PaperStandards
    {
        private static System.Drawing.Printing.PaperSize[] _paperFormats = null;
        public static System.Drawing.Printing.PaperSize[] PaperFormats
        {
            get
            {
                if(_paperFormats == null)
                {
                    System.Drawing.Printing.PrintDocument nobody_doc = new System.Drawing.Printing.PrintDocument();
                    _paperFormats = new System.Drawing.Printing.PaperSize[nobody_doc.PrinterSettings.PaperSizes.Count];
                    nobody_doc.PrinterSettings.PaperSizes.CopyTo(_paperFormats, 0);
                }
                return _paperFormats;
            }
        }
        public static System.Drawing.Printing.PaperSize GetSizeByName(string name)
        {
            return Array.Find(PaperFormats, item => {
                Regex match = new Regex($"^{name}[^\\.]*");
                return match.IsMatch(item.PaperName);
            });
        }

    }
}

