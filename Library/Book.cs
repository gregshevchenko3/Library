using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Book : StorageInfo
    {
        public Book(string title, Ganres ganre, Langs lang, DateTime publish, string pathToFile, string paper = "A5 148 x 210mm",
            string FontFamily = "Arial", Single emSize = 16) : base(title, ganre, lang, publish, pathToFile, paper, FontFamily, emSize)
        {

        }
    }
}
