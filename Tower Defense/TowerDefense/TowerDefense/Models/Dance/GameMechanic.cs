using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDance.Models.Dance
{
    class GameMechanic
    {
        Song _song;
        MusicSheet _musicSheet;
        TimeSpan _timePlayed;
        TimeSpan _timeBeforePlay;
        List<Note> _notes = new List<Note>();
        bool _hasMusicStarted;
        bool _needToPlaySong;

        public GameMechanic(MusicSheet musicSheet)
        {
            _musicSheet = musicSheet;
            _timePlayed = new TimeSpan();
            _hasMusicStarted = false;
            _needToPlaySong = false;
            _notes = _musicSheet.getNotes();
            _song = _musicSheet.getSong();
            /* Load the musicSheets in note list */
            if (_song.offset >= 0)
            {
                _timePlayed = new TimeSpan(0, 0, 0, 0, -500);
                _timeBeforePlay = new TimeSpan(0, 0, 0, 0, (int)(_song.offset * 1000));
            }
            else
            {
                _timePlayed = new TimeSpan(0, 0, 0, 0, (int)(_song.offset * 1000) - 150);
                _timeBeforePlay = new TimeSpan();
            }

        }

        public void update(GameTime gameTime)
        {
            /* Main loop */
            _timePlayed += gameTime.ElapsedGameTime;
            if (_hasMusicStarted == false)
            {
                /* Begin to play the song if needed */
                _timeBeforePlay -= gameTime.ElapsedGameTime;
                if (_timeBeforePlay.TotalMilliseconds <= 0)
                    _needToPlaySong = true;
            }
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Left))
                validNote(0);
            if (keyState.IsKeyDown(Keys.Down))
                validNote(1);
            if (keyState.IsKeyDown(Keys.Up))
                validNote(2);
            if (keyState.IsKeyDown(Keys.Right))
                validNote(3);
        }

        public bool isFinished()
        {
            if (_timePlayed.TotalSeconds >= _notes[_notes.Count - 1].getPosition() + 2.0f)
                return true;
            return false;
        }

        public List<Note> getNotes()
        {
            return _notes;
        }

        public TimeSpan getTimePlayed()
        {
            return _timePlayed;
        }

        public string getSongFileName()
        {
            return _song.directory + "/" + _song.music;
        }

        public bool needToPlaySong()
        {
            if (_needToPlaySong)
            {
                _needToPlaySong = false;
                return true;
            }
            return false;
        }

        public void setMusicStarted(bool hasMusicStarted)
        {
            _hasMusicStarted = hasMusicStarted;
        }

        private void validNote(int type)
        {
            float diff = 0.1f;
            float timePlayed = (float)_timePlayed.TotalSeconds;
            foreach (Note n in _notes)
            {
                if (n.getType() == type)
                {
                    float position = n.getPosition();
                    if (position >= timePlayed - diff && position <= timePlayed + diff)
                        n.validate();
                }
            }
        }
    }
}
