using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace DDR
{
    class Dance : IGameable
    {
        ContentManager _contentManager;
        List<Song> _songs = new List<Song>();

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
                FileInfo[] files = directory.GetFiles("*.sm");
                if (files.Length > 0)
                    _songs.Add(new Song("Songs/" + directory.Name + "/" + files[0].Name, "sm"));
                else
                {
                    files = directory.GetFiles("*.dwi");
                    if (files.Length > 0)
                        _songs.Add(new Song("Songs/" + directory.Name + "/" + files[0].Name, "dwi"));
                }
            }
        }

        public void unloadContent()
        {
        }

        public void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
