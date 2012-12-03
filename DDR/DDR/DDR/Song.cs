using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;

namespace DDR
{
    class Song
    {
        string _url;
        string _format;
        Boolean _ready = false;
        Boolean _valid = false;
        
        /* Song properties */
        public string title;
        public string artist;
        public string music;
        public float offset;
        public float sampleStart;
        public float sampleLength;
        public List<BeatValue> bpms = new List<BeatValue>();
        public List<BeatValue> stops = new List<BeatValue>();
        public List<MusicSheet> musicSheets = new List<MusicSheet>();
        public string directory;

        public Song(string url, string format, string directory)
        {
            _url = url;
            _format = format;
            this.directory = directory;
            try
            {
                System.IO.Stream stream = TitleContainer.OpenStream(_url);
                System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                IParser parser = null;
                if (format == "sm")
                    parser = new SMParser(reader, this);
                else if (format == "dwi")
                    parser = new DWIParser(reader, this);
                if (parser != null && parser.parse())
                    _valid = true;
                _ready = true;
                stream.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
            }
        }

        public bool isReady()
        {
            return _ready;
        }

        public bool isValid()
        {
            return _valid;
        }

        public override string ToString()
        {
            return title;
        }
    }
}
