using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDR
{
    class DWIParser : IParser
    {
        string _stream;
        Song _song;

        public DWIParser(System.IO.StreamReader reader, Song song)
        {
            _stream = reader.ReadToEnd();
            _song = song;
        }

        public bool parse()
        {
            return false;
        }
    }
}
