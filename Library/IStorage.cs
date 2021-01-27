using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    interface IStorage
    {
        int Width { get; }
        int Height { get; }
        Font Font { get;  }
    }
}
