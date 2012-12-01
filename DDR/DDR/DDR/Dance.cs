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
            if (_currentSong == null)
            {
                _currentSongIndex++;
                if (_currentSongIndex < _songs.Count)
                {
                    _currentSong = _songs[_currentSongIndex];
                    playSong(_currentSong.directory + "/" + _currentSong.music);
                }
            }
            if (_currentSong != null)
            {
                /* The song is playing */
            }
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
            MediaPlayer.Play(song);
        }
    }
}
