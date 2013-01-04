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
        List<Note> _notes = new List<Note>();
        bool _hasMusicStarted;
        bool _needToPlaySong;
        bool _hasNewFlashMessage;
        string _flashMessage;
        int _combo;
        int _lastValidatedNoteId;

        public GameMechanic(MusicSheet musicSheet)
        {
            _combo = 0;
            _musicSheet = musicSheet;
            _timePlayed = new TimeSpan();
            _hasNewFlashMessage = false;
            _hasMusicStarted = false;
            _needToPlaySong = true;
            _notes = _musicSheet.getNotes();
            _song = _musicSheet.getSong();
            _timePlayed = new TimeSpan(0, 0, 0, 0, (int)(_song.offset * 1000) - 500);
            _lastValidatedNoteId = -1;
        }

        public void update(GameTime gameTime)
        {
            if (_hasMusicStarted)
                _timePlayed += gameTime.ElapsedGameTime;
            failNotes();
            recalculateCombo();
        }

        public bool isFinished()
        {
            if (_notes.Count <= 0)
                return true;
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

        public bool hasMusicStarted()
        {
            return _hasMusicStarted;
        }

        public bool hasNewFlashMessage()
        {
            if (_hasNewFlashMessage)
            {
                _hasNewFlashMessage = false;
                return true;
            }
            return false;
        }

        public string getFlashMessage()
        {
            return _flashMessage;
        }

        public void setFlashMessage(string flashMessage)
        {
            _flashMessage = flashMessage;
            _hasNewFlashMessage = true;
        }

        public int getCombo()
        {
            return _combo;
        }

        public void syncWithSong(TimeSpan songPosition)
        {
            _timePlayed += songPosition;
            _hasMusicStarted = true;
        }

        public void tryToValid(int type)
        {
            Note n;
            Grade g;
            int i = _lastValidatedNoteId;
            int j = 0;
            while (i + j > 0)
            {
                if (_notes[i + j - 1].getPosition() == _notes[_lastValidatedNoteId].getPosition())
                    j--;
                else
                    break;
            }
            if (j < 0)
                j--;
            i += j;
            while (++i < _notes.Count)
            {
                n = _notes[i];
                g = retrieveGrade(n);
                if (g == Grade.NotPlayed)
                    break;
                if (n.getType() == type && !n.isValid() && g >= Grade.Bad)
                    validNote(n, i, g);
            }
            failNotes();
            recalculateCombo();
        }

        public void autoValid()
        {
            Note n;
            Grade g;
            int i = _lastValidatedNoteId;
            int j = 0;
            while (i + j > 0)
            {
                if (_notes[i + j - 1].getPosition() == _notes[_lastValidatedNoteId].getPosition())
                    j--;
                else
                    break;
            }
            if (j < 0)
                j--;
            i += j;
            while (++i < _notes.Count)
            {
                n = _notes[i];
                g = retrieveGrade(n);
                if (g == Grade.NotPlayed)
                    break;
                if (!n.isValid() && g >= Grade.Fantastic)
                    validNote(n, i, g);
            }
            failNotes();
            recalculateCombo();
        }

        private void validNote(Note note, int id, Grade grade)
        {
            note.validate(grade);
            string flashMessage = "";
            switch (grade)
            {
                case Grade.Fantastic:
                    flashMessage = "FANTASTIC";
                    break;
                case Grade.Good:
                    flashMessage = "GOOD";
                    break;
                case Grade.Bad:
                    flashMessage = "BAD";
                    break;
                case Grade.Fail:
                    flashMessage = "";
                    break;
            }
            setFlashMessage(flashMessage);
            _lastValidatedNoteId = id;
        }

        private void failNotes()
        {
            Grade g;
            int i = _lastValidatedNoteId;
            int j = 0;
            while (i + j > 0)
            {
                if (_notes[i + j - 1].getPosition() == _notes[_lastValidatedNoteId].getPosition())
                    j--;
                else
                    break;
            }
            if (j < 0)
                j--;
            i += j;
            while (++i < _notes.Count)
            {
                g = retrieveGrade(_notes[i]);
                if (g == Grade.Fail && !_notes[i].isValid())
                    validNote(_notes[i], i, g);
                if (g == Grade.NotPlayed)
                    break;
            }
        }

        private void recalculateCombo()
        {
            _combo = 0;
            int i = _lastValidatedNoteId;
            while (i >= 0 && i < _notes.Count - 1)
            {
                if (_notes[i + 1].getPosition() == _notes[_lastValidatedNoteId].getPosition())
                    i++;
                else
                    break;
            }
            while (i >= 0)
            {
                Note n = _notes[i];
                if (n.isValid())
                    _combo++;
                else if (n.getPosition() != _notes[_lastValidatedNoteId].getPosition())
                    break;
                i--;
            }
        }

        private Grade retrieveGrade(Note note)
        {
            float diff = 0.05f;
            float timePlayed = (float)_timePlayed.TotalSeconds;
            float position = note.getPosition();
            if (position >= timePlayed - diff && position <= timePlayed + diff)
                return Grade.Fantastic;
            else if (position >= timePlayed - 2 * diff && position <= timePlayed + 2 * diff)
                return Grade.Good;
            else if (position >= timePlayed - 3 * diff && position <= timePlayed + 3 * diff)
                return Grade.Bad;
            else if (position > timePlayed + 3 * diff)
                return Grade.NotPlayed;
            return Grade.Fail;
        }
    }
}
