using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Book : Document
    {

        public Book(string title, Ganres ganre, Langs lang, DateTime publish, string pathToFile, Font font = null, PageSettings pg_settings = null) 
            : base(title, ganre, lang, publish, pathToFile, font, pg_settings)
        {

        }
    }
}
