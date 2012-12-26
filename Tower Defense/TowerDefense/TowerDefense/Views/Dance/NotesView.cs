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
    class NotesView : IView
    {
        SpriteBatch _spriteBatch;
        List<Texture2D> _spriteTextures = new List<Texture2D>();
        GraphicsDevice _graphicsDevice;
        string _flashMessage = "";
        SpriteFont _flashMessageFont;
        List<Note> _notes;
        TimeSpan _timePlayed;
        int _combo;

        public NotesView(List<Note> notes, TimeSpan timePlayed)
        {
            _notes = notes;
            _timePlayed = timePlayed;
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_0"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_1"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_2"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_3"));
            _flashMessageFont = contentManager.Load<SpriteFont>("DanceFlashFont");
        }

        public void draw()
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
            drawFlashMessage();
            drawCombo();
            _spriteBatch.End();
        }

        public void setFlashMessage(string flashMessage)
        {
            _flashMessage = flashMessage;
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

        public void setTimePlayed(TimeSpan timePlayed)
        {
            _timePlayed = timePlayed;
        }

        public void setCombo(int combo)
        {
            _combo = combo;
        }

        private void drawFlashMessage()
        {
            Vector2 _msgSize = _flashMessageFont.MeasureString(_flashMessage);
            _spriteBatch.DrawString(_flashMessageFont, _flashMessage, new Vector2(), Color.Red);
        }

        private void drawCombo()
        {
            if (_combo > 1)
            {
                _spriteBatch.DrawString(_flashMessageFont, _combo.ToString() + " combos", new Vector2(0, 200), Color.Red);
            }
        }
    }
}
