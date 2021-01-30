using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class StorageReader
    {

        public class NextLineReadArgs
        {
            public string Line { get; set; }
        }
        private StreamReader _reader;
        private string _prev_line = String.Empty;
        public EventHandler<NextLineReadArgs> eventHandler;

        public StorageReader(string filename)
        {
            if (File.Exists(filename))
            {
                if (Path.GetExtension(filename) == ".txt")
                {
                    _reader = new StreamReader(new FileStream(filename, FileMode.Open));
                }
                else
                {
                    throw new Exception("Incorrect File Extention");
                }

            }
            else
            {
                throw new FileNotFoundException("File not Found " + filename);
            }       
        }
        public async void read_line()
        {
            string line = await _reader.ReadLineAsync();
            int line_no = 0;
            eventHandler(this, new NextLineReadArgs { Line = line });
            while (!_reader.EndOfStream)
            {
                line = await _reader.ReadLineAsync();
                if (line == null) break;
               // Console.WriteLine(line_no++);
                eventHandler(this, new NextLineReadArgs { Line = line });
            }
            _reader.Close();
        }
    }
}
