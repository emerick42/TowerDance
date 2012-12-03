using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Reflection;
using Microsoft.Xna.Framework.Media;

namespace DDR
{
    class Dance : IGameable
    {
        ContentManager _contentManager;
        List<Song> _songs = new List<Song>();
        int _currentSongIndex = -1;
        Song _currentSong = null;
        TimeSpan _timePlayed;
        TimeSpan _timeBeforePlay;
        bool _hasMusicStarted;
        MusicSheet _currentMusicSheet = null;
        List<Note> _notes = new List<Note>();

        public Dance(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void initialize()
        {
        }

        public void loadContent()
        {
            DirectoryInfo dir = new DirectoryInfo("Songs");
            if (!dir.Exists)
                return;
            DirectoryInfo[] directories = dir.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                Song s = null;
                FileInfo[] files = directory.GetFiles("*.sm");
                if (files.Length > 0)
                    s = new Song("Songs/" + directory.Name + "/" + files[0].Name, "sm", "Songs/" + directory.Name);
                else
                {
                    files = directory.GetFiles("*.dwi");
                    if (files.Length > 0)
                        s = new Song("Songs/" + directory.Name + "/" + files[0].Name, "dwi", "Songs/" + directory.Name);
                }
                if (s.isReady() && s.isValid())
                    _songs.Add(s);
            }
        }

        public void unloadContent()
        {
        }

        public void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            /* We need to play a song */
            if (_currentSong == null || _currentMusicSheet == null)
            {
                _currentSongIndex++;
                if (_currentSongIndex < _songs.Count)
                {
                    _hasMusicStarted = false;
                    _currentSong = _songs[_currentSongIndex];
                    _currentMusicSheet = _currentSong.musicSheets[0];
                    if (_currentSong.offset >= 0)
                    {
                        _timeBeforePlay = new TimeSpan(0, 0, 0, 0, (int)(_currentSong.offset * 1000));
                        _timePlayed = new TimeSpan();
                    }
                    else
                    {
                        _timePlayed = new TimeSpan(0, 0, 0, 0, (int)(_currentSong.offset * 1000));
                        _timeBeforePlay = new TimeSpan();
                    }
                }
            }
            else
            {
                _timePlayed += gameTime.ElapsedGameTime;
                if (_hasMusicStarted == false)
                {
                    /* Begin to play the song if needed */
                    _timeBeforePlay -= gameTime.ElapsedGameTime;
                    if (_timeBeforePlay.TotalMilliseconds <= 0)
                        playSong(_currentSong.directory + "/" + _currentSong.music);
                }
                updateMusicSheet();
            }
        }

        public void updateMusicSheet()
        {
            _notes.Clear();
            float beat = getBeatAt(_timePlayed);
            float measure = getMeasureAt(beat);
            /* We create notes for the n-1, n, n+1 and n+2 measure */
            int n = (int)Math.Ceiling(measure);
            for (int i = - 1; i < 2; i++)
            {
                /* If the measure exists, we can add its notes */
                if (i + n >= 0 && i + n < _currentMusicSheet.measures.Count)
                {
                    int nbBeats = _currentMusicSheet.measures[i + n].Length / 4;
                    while (nbBeats >= 0)
                    {
                        
                    }
                }
            }
        }
        private float getBeatAt(TimeSpan timePlayed)
        {
            int i = 0;
            float beatPlayed = 0;
            TimeSpan timeLeft = timePlayed;
            List<BeatValue> bpms = _currentSong.bpms;
            /* Doesn't manage the stops for the moment */
            List<BeatValue> stops = _currentSong.stops;
            while (i < bpms.Count)
            {
                /* No other bpm */
                if (i + 1 >= bpms.Count)
                {
                    beatPlayed += (float)timeLeft.TotalMinutes * bpms[i].getValue();
                    break;
                }
                /* Time limited */
                else
                {
                    float maxBeat = bpms[i + 1].getBeat();
                    float beat = (float)timeLeft.TotalMinutes * bpms[i].getValue();
                    /* It fits in the BPM session */
                    if (beat < maxBeat)
                    {
                        beatPlayed += beat;
                        break;
                    }
                    /* Too many time for this BPM session */
                    else
                    {
                        beatPlayed += maxBeat;
                        timeLeft -= new TimeSpan(0, (int)(maxBeat / bpms[i].getValue()), 0);
                    }
                }
                i++;
            }
            return beatPlayed;
        }
        private float getMeasureAt(float beat)
        {
            return beat / 4.0f;
        }

        public void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public void playSong(string path)
        {
            /*var ctor = typeof(Microsoft.Xna.Framework.Media.Song).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null,
                new[] { typeof(string), typeof(string), typeof(int) }, null);
            Microsoft.Xna.Framework.Media.Song song = (Microsoft.Xna.Framework.Media.Song)ctor.Invoke(new object[] { "song", path, 0 });
             */
            Uri songPath = new Uri(path, UriKind.Relative);
            Microsoft.Xna.Framework.Media.Song song = Microsoft.Xna.Framework.Media.Song.FromUri("song", songPath);
            _hasMusicStarted = true;
            MediaPlayer.Play(song);
        }
    }
}
