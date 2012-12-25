using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Reflection;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DDR
{
    class Dance : IGameable
    {
        List<Song> _songs = new List<Song>();
        int _currentSongIndex = -1;
        Song _currentSong = null;
        TimeSpan _timePlayed;
        TimeSpan _timeBeforePlay;
        bool _hasMusicStarted;
        MusicSheet _currentMusicSheet = null;
        List<Note> _notes = new List<Note>();
        /* About the draw */
        ContentManager _contentManager;
        SpriteBatch _spriteBatch;
        List<Texture2D> _spriteTextures = new List<Texture2D>();

        public Dance(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void initialize()
        {
        }

        public void loadContent(GraphicsDevice graphicsDevice)
        {
            /* loads about the draw */
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _spriteTextures.Add(_contentManager.Load<Texture2D>("ddr_arrow_sprite_0"));
            _spriteTextures.Add(_contentManager.Load<Texture2D>("ddr_arrow_sprite_1"));
            _spriteTextures.Add(_contentManager.Load<Texture2D>("ddr_arrow_sprite_2"));
            _spriteTextures.Add(_contentManager.Load<Texture2D>("ddr_arrow_sprite_3"));
            /* load all the songs */
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
                    /* Load the musicSheets in note list */
                    loadNotes();
                    if (_currentSong.offset >= 0)
                    {
                        _timePlayed = new TimeSpan(0, 0, 0, 0, -500);
                        _timeBeforePlay = new TimeSpan(0, 0, 0, 0, (int)(_currentSong.offset * 1000));
                    }
                    else
                    {
                        _timePlayed = new TimeSpan(0, 0, 0, 0, (int)(_currentSong.offset * 1000) - 500);
                        _timeBeforePlay = new TimeSpan();
                    }
                }
            }
            else
            {
                /* Main loop */
                _timePlayed += gameTime.ElapsedGameTime;
                if (_hasMusicStarted == false)
                {
                    /* Begin to play the song if needed */
                    _timeBeforePlay -= gameTime.ElapsedGameTime;
                    if (_timeBeforePlay.TotalMilliseconds <= 0)
                        playSong(_currentSong.directory + "/" + _currentSong.music);
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
        }

        public void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float spaceMultiplicatorBetweenArrows = 2.0f;
            Vector2 pos;
            _spriteBatch.Begin();
            Color c = Color.Red;
            foreach (Note n in _notes)
            {
                float pixelsBetweenTwoNotes = 100.0f * (n.getSpeed() / 60.0f);
                pos = new Vector2(100.0f * n.getType(), pixelsBetweenTwoNotes * spaceMultiplicatorBetweenArrows * (n.getPosition() - (float)_timePlayed.TotalSeconds));
                c = Color.Indigo;
                switch (n.getTempo())
                {
                    case 0:
                        c = Color.Red;
                        break;
                    case 1:
                        c = Color.Blue;
                        break;
                    case 2:
                        c = Color.Yellow;
                        break;
                    case 3:
                        c = Color.Green;
                        break;
                }
                if (n.isValid())
                    c = Color.White;
                _spriteBatch.Draw(_spriteTextures[n.getType()], pos, c);
            }
            _spriteBatch.End();
        }

        private void playSong(string path)
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

        private void loadNotes()
        {
            int currentTempo = 0;
            int currentBeat = -1;
            int currentMeasure = 0;
            int currentBPMIndex = -1;
            float currentTime = 0.0f;
            _notes.Clear();
            if (_currentMusicSheet.notesType == "dance-single")
            {
                /* We iterate on each measure */
                while (currentMeasure < _currentMusicSheet.measures.Count())
                {
                    string measure = _currentMusicSheet.measures[currentMeasure];
                    int nbNote = measure.Length / 4;
                    int i = -1;
                    /* We iterate on each note */
                    currentTempo = 0;
                    while (++i < nbNote)
                    {
                        if (i % (nbNote / 4) == 0)
                            currentBeat++;
                        currentTempo = 4; /* Purple note */
                        if (nbNote >= 32 && i % (nbNote / 16) == nbNote / 32)
                            currentTempo = 3; /* Yellow note */
                        else if (nbNote >= 16 && i % (nbNote / 8) == nbNote / 16)
                            currentTempo = 2; /* Yellow note */
                        else if (nbNote >= 8 && i % (nbNote / 4) == nbNote / 8)
                            currentTempo = 1; /* Blue note */
                        else if (nbNote >= 4)
                            currentTempo = 0; /* Red note */
                        if (currentBPMIndex + 1 < _currentSong.bpms.Count && _currentSong.bpms[currentBPMIndex + 1].getBeat() == currentBeat)
                            currentBPMIndex++;
                        string note = measure.Substring(i * 4, 4);
                        int j = -1;
                        while (++j < note.Length)
                        {
                            if (note[j] == '1' || note[j] == '2')
                                _notes.Add(new Note(currentTime, _currentSong.bpms[currentBPMIndex].getValue(), j, currentTempo));
                            /* We look for the last note with this type and set the positionStop to currentTime */
                            if (note[j] == '3')
                                searchForLastNoteOfTypeAndSetPositionStop(j, currentTime);
                        }
                        currentTime += (1.0f / (_currentSong.bpms[currentBPMIndex].getValue() / 60.0f)) / (nbNote / 4);
                    }
                    currentMeasure++;
                }
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
