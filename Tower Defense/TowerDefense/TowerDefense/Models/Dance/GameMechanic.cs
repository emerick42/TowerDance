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
        bool _hasNewFlashMessage;
        string _flashMessage;
        int _combo;

        public GameMechanic(MusicSheet musicSheet)
        {
            _combo = 0;
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

        public bool hasNewFlashMessage()
        {
            return _hasNewFlashMessage;
        }

        public string getFlashMessage()
        {
            return _flashMessage;
        }

        public int getCombo()
        {
            return _combo;
        }

        private void validNote(int type)
        {
            float diff = 0.05f;
            float timePlayed = (float)_timePlayed.TotalSeconds;
            _combo = 0;
            foreach (Note n in _notes)
            {
                if (n.getPosition() < timePlayed - 3 * diff)
                {
                    if (!n.isValid())
                        _combo = 0;
                    else
                        _combo++;
                }
                if (n.getType() == type && !n.isValid())
                {
                    float position = n.getPosition();
                    if (position >= timePlayed - diff && position <= timePlayed + diff)
                    {
                        n.validate(Grade.Fantastic);
                        _flashMessage = "FANTASTIC";
                        _hasNewFlashMessage = true;
                    }
                    else if (position >= timePlayed - 2 * diff && position <= timePlayed + 2 * diff)
                    {
                        n.validate(Grade.Good);
                        _flashMessage = "GOOD";
                        _hasNewFlashMessage = true;
                    }
                    else if (position >= timePlayed - 3 * diff && position <= timePlayed + 3 * diff)
                    {
                        n.validate(Grade.Bad);
                        _flashMessage = "BAD";
                        _hasNewFlashMessage = true;
                    }
                }
            }
        }
    }
}
