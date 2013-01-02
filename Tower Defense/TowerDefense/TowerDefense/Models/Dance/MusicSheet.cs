using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models.Dance
{
    class MusicSheet
    {
        public string notesType;
        public int difficultyMeter;
        public List<string> measures = new List<string>();
        private List<Note> _notes = new List<Note>();
        private Song _song;

        public MusicSheet(Song song)
        {
            _song = song;
        }

        public List<Note> getNotes()
        {
            loadNotes();
            return _notes;
        }

        private void loadNotes()
        {
            int currentTempo = 0;
            int currentBeat = -1;
            int currentMeasure = 0;
            int currentBPMIndex = -1;
            float currentTime = 0.0f;
            _notes.Clear();
            if (notesType == "dance-single")
            {
                /* We iterate on each measure */
                while (currentMeasure < measures.Count())
                {
                    string measure = measures[currentMeasure];
                    int nbNote = measure.Length / 4;
                    int i = -1;
                    /* We iterate on each note */
                    currentTempo = 0;
                    while (++i < nbNote)
                    {
                        if (i % (nbNote / 4) == 0)
                        {
                            currentBeat++;
                            foreach (BeatValue b in _song.stops)
                            {
                                if (b.getBeat() == currentBeat)
                                    currentTime += b.getValue();
                            }
                            foreach (BeatValue b in _song.bpms)
                            {
                                if (b.getBeat() == currentBeat && currentBPMIndex + 1 < _song.bpms.Count)
                                    currentBPMIndex++;
                            }
                        }
                        currentTempo = findCurrentTempo(i, nbNote);
                        string note = measure.Substring(i * 4, 4);
                        int j = -1;
                        while (++j < note.Length)
                        {
                            if (note[j] == '1' || note[j] == '2')
                                _notes.Add(new Note(currentTime, _song.bpms[currentBPMIndex].getValue(), j, currentTempo));
                            /* We look for the last note with this type and set the positionStop to currentTime */
                            if (note[j] == '3')
                                searchForLastNoteOfTypeAndSetPositionStop(j, currentTime);
                        }
                        currentTime += (1.0f / (_song.bpms[currentBPMIndex].getValue() / 60.0f)) / ((float)nbNote / 4.0f);
                    }
                    currentMeasure++;
                }
            }
        }

        private int findCurrentTempo(int i, int nbNote)
        {
            int currentTempo = 0;
            int ecart = 0;
            if (nbNote % 4 != 0)
                return 4;
            currentTempo = 0;
            ecart = nbNote / 4;
            i %= ecart;
            while (true)
            {
                if (i == 0)
                    return currentTempo;
                if (ecart % 2 == 0)
                {
                    ecart /= 2;
                    i %= ecart;
                    currentTempo++;
                }
                else
                    return 4;
            }
        }

        private void searchForLastNoteOfTypeAndSetPositionStop(int type, float positionStop)
        {
            int i = _notes.Count - 1;
            while (i >= 0)
            {
                if (_notes[i].getType() == type)
                {
                    _notes[i].setPositionStop(positionStop);
                    return;
                }
                i--;
            }
        }

        public Song getSong()
        {
            return _song;
        }
    }
}
