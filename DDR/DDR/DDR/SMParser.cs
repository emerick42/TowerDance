using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DDR
{
    class SMParser : IParser
    {
        string _stream;
        Song _song;

        public SMParser(System.IO.StreamReader reader, Song song)
        {
            _stream = reader.ReadToEnd();
            _song = song;
        }

        public bool parse()
        {
            string pattern = "(//[^\n]*\n)";
            _stream = Regex.Replace(_stream, pattern, String.Empty);
            pattern = "(#(?<title>[^:]+):(?<value>[^;]*);[\r\n \t]*)";
            foreach (Match match in Regex.Matches(_stream, pattern, RegexOptions.ExplicitCapture))
            {
                Group title = match.Groups["title"];
                Group value = match.Groups["value"];
                if (title != null && value != null)
                {
                    switch (title.ToString())
                    {
                        case "TITLE":
                            _song.title = value.ToString();
                            break;
                        case "ARTIST":
                            _song.artist = value.ToString();
                            break;
                        case "MUSIC":
                            _song.music = value.ToString();
                            break;
                        case "OFFSET":
                            _song.offset = float.Parse(value.ToString());
                            break;
                        case "SAMPLESTART":
                            _song.sampleStart = float.Parse(value.ToString());
                            break;
                        case "SAMPLELENGTH":
                            _song.sampleLength = float.Parse(value.ToString());
                            break;
                        case "BPMS":
                            parseBPMS(value.ToString());
                            break;
                        case "STOPS":
                            parseStops(value.ToString());
                            break;
                        case "NOTES":
                            parseNotes(value.ToString());
                            break;
                    }
                }
            }
            if (_song.music != "" && _song.musicSheets.Count >= 1)
                return true;
            return false;
        }

        public void parseBPMS(string bpms)
        {
            bpms = Regex.Replace(bpms, "[\n\r \t]*", String.Empty);
            _song.bpms.Clear();
            string pattern = "(?<begin>[-+]?[0-9]*\\.?[0-9]*)=(?<bpm>[-+]?[0-9]*\\.?[0-9]*)";
            foreach (Match match in Regex.Matches(bpms, pattern, RegexOptions.ExplicitCapture))
            {
                Group begin = match.Groups["begin"];
                Group bpm = match.Groups["bpm"];
                if (begin != null && bpm != null)
                {
                    _song.bpms.Add(new TimeValue(float.Parse(begin.ToString()), float.Parse(bpm.ToString())));
                }
            }
        }

        public void parseStops(string stops)
        {
            stops = Regex.Replace(stops, "[\n\r \t]*", String.Empty);
            _song.stops.Clear();
            string pattern = "(?<begin>[-+]?[0-9]*\\.?[0-9]*)=(?<bpm>[-+]?[0-9]*\\.?[0-9]*)";
            foreach (Match match in Regex.Matches(stops, pattern, RegexOptions.ExplicitCapture))
            {
                Group begin = match.Groups["begin"];
                Group bpm = match.Groups["bpm"];
                if (begin != null && bpm != null)
                {
                    _song.stops.Add(new TimeValue(float.Parse(begin.ToString()), float.Parse(bpm.ToString())));
                }
            }
        }
        
        public void parseNotes(string notes)
        {
            notes = Regex.Replace(notes, "[\n\r \t]*", String.Empty);
            string pattern = "(?<notesType>[^:]*):([^:]*):([^:]*):(?<difficultyMeter>[^:]*):([^:]*):(?<musicSheet>[^;]*)";
            foreach (Match match in Regex.Matches(notes, pattern, RegexOptions.ExplicitCapture))
            {
                Group notesType = match.Groups["notesType"];
                Group difficultyMeter = match.Groups["difficultyMeter"];
                Group musicSheet = match.Groups["musicSheet"];
                if (notesType != null && difficultyMeter != null && musicSheet != null)
                {
                    MusicSheet ms = new MusicSheet();
                    ms.notesType = notesType.ToString();
                    ms.difficultyMeter = int.Parse(difficultyMeter.ToString());
                    string musicSheetPattern = "(?<measure>[0-9])*[,]*";
                    foreach (Match match2 in Regex.Matches(musicSheet.ToString(), musicSheetPattern, RegexOptions.ExplicitCapture))
                    {
                        Group measure = match2.Groups["measure"];
                        if (measure != null)
                        {
                            ms.measures.Add(measure.ToString());
                        }
                    }
                    _song.musicSheets.Add(ms);
                }
            }
        }
    }
}
