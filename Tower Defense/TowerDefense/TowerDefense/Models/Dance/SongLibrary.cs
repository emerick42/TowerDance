using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TowerDance.Models.Dance
{
    class SongLibrary
    {
        public List<Song> songs = new List<Song>();

        public SongLibrary()
        {

        }

        public void initialize()
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
                    songs.Add(s);
            }

        }
    }
}
