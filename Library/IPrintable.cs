using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    interface IPrintable
    {
        int Width { get; }
        int Height { get; }
        Font Font { get;  }
    }
}
