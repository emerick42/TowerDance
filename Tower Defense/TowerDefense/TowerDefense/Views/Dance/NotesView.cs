using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.Dance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace TowerDance.Views.Dance
{
    class NotesView
    {
        SpriteBatch _spriteBatch;
        List<Texture2D> _spriteTextures = new List<Texture2D>();
        GraphicsDevice _graphicsDevice;

        public NotesView()
        {
            
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_0"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_1"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_2"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_3"));
        }

        public void draw(List<Note> notes, TimeSpan timePlayed)
        {
            float spaceMultiplicatorBetweenArrows = 2.0f;
            Vector2 pos;
            _spriteBatch.Begin();
            Color c = Color.Red;
            foreach (Note n in notes)
            {
                float pixelsBetweenTwoNotes = 100.0f * (n.getSpeed() / 60.0f);
                pos = new Vector2(100.0f * n.getType(), pixelsBetweenTwoNotes * spaceMultiplicatorBetweenArrows * (n.getPosition() - (float)timePlayed.TotalSeconds));
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

        public void playSong(string songFileName)
        {
            Uri songPath = new Uri(songFileName, UriKind.Relative);
            Microsoft.Xna.Framework.Media.Song song = Microsoft.Xna.Framework.Media.Song.FromUri("song", songPath);
            MediaPlayer.Play(song);
        }

        public void resumeSong()
        {
            MediaPlayer.Resume();
        }

        public void pauseSong()
        {
            MediaPlayer.Pause();
        }
    }
}
