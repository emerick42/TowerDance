using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.Dance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using TowerDance.Models;

namespace TowerDance.Views.Dance
{
    class NotesView : IView
    {
        WindowConfiguration _windowConfiguration;
        SpriteBatch _spriteBatch;
        List<Texture2D> _spriteTextures = new List<Texture2D>();
        GraphicsDevice _graphicsDevice;
        string _flashMessage = "";
        SpriteFont _flashMessageFont;
        List<Note> _notes;
        TimeSpan _timePlayed;
        TimeSpan _flashMessageCreation;
        int _combo;

        public NotesView(List<Note> notes, TimeSpan timePlayed)
        {
            _notes = notes;
            _timePlayed = timePlayed;
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _windowConfiguration = windowConfiguration;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_0"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_1"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_2"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_arrow_sprite_3"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_print_sprite_0"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_print_sprite_1"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_print_sprite_2"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("ddr_print_sprite_3"));
            _flashMessageFont = contentManager.Load<SpriteFont>("DanceFlashFont");
        }

        public void draw()
        {
            int gapX = (_windowConfiguration.width - 400) / 2;
            int gapY = 50;
            float spaceMultiplicatorBetweenArrows = 2.0f;
            Vector2 pos;
            _spriteBatch.Begin();
            drawNotePrints();
            Color c = Color.Red;
            foreach (Note n in _notes)
            {
                float pixelsBetweenTwoNotes = 100.0f * (n.getSpeed() / 60.0f);
                pos = new Vector2(gapX + 100.0f * n.getType(), gapY + pixelsBetweenTwoNotes * spaceMultiplicatorBetweenArrows * (n.getPosition() - (float)_timePlayed.TotalSeconds));
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
            drawTimePlayed();
            _spriteBatch.End();
        }

        public void setFlashMessage(string flashMessage)
        {
            _flashMessage = flashMessage;
            _flashMessageCreation = _timePlayed;
        }

        public void playSong(string songFileName)
        {
            Uri songPath = new Uri(songFileName, UriKind.Relative);
            Microsoft.Xna.Framework.Media.Song song = Microsoft.Xna.Framework.Media.Song.FromUri("song", songPath);
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume /= 2;
        }

        public void resumeSong()
        {
            MediaPlayer.Resume();
        }

        public void pauseSong()
        {
            MediaPlayer.Pause();
        }

        public void stopSong()
        {
            MediaPlayer.Stop();
        }

        public void setTimePlayed(TimeSpan timePlayed)
        {
            _timePlayed = timePlayed;
        }

        public void setCombo(int combo)
        {
            _combo = combo;
        }

        public bool hasSongStarted()
        {
            if (MediaPlayer.State == MediaState.Playing && MediaPlayer.PlayPosition > TimeSpan.Zero)
                return true;
            return false;
        }

        public TimeSpan getSongDiffPosition()
        {
            return MediaPlayer.PlayPosition;
        }

        private void drawFlashMessage()
        {
            if (_flashMessageCreation.TotalSeconds == 0.0d)
                return;
            float diffTime = (float)(_timePlayed.TotalSeconds - _flashMessageCreation.TotalSeconds);
            if (diffTime < 0.8f)
            {
                float scale = 1.0f;
                if (diffTime < 0.17f)
                    scale = diffTime / 0.17f;
                Vector2 _msgSize = _flashMessageFont.MeasureString(_flashMessage);
                int gapX = (_windowConfiguration.width - (int)(_msgSize.X * scale)) / 2;
                int gapY = 150 + (int)((1.0f - scale) * _msgSize.Y / 2);
                _spriteBatch.DrawString(_flashMessageFont, _flashMessage, new Vector2(gapX + 2, gapY + 2), Color.DarkRed, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                _spriteBatch.DrawString(_flashMessageFont, _flashMessage, new Vector2(gapX, gapY), Color.Red, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
            }
        }

        private void drawCombo()
        {
            if (_combo > 1)
            {
                Vector2 _msgSize = _flashMessageFont.MeasureString(_combo.ToString() + " combos");
                int gapX = (_windowConfiguration.width - (int)_msgSize.X) / 2;
                int gapY = 200;
                _spriteBatch.DrawString(_flashMessageFont, _combo.ToString() + " combos", new Vector2(gapX + 2, gapY + 2), Color.DarkRed);
                _spriteBatch.DrawString(_flashMessageFont, _combo.ToString() + " combos", new Vector2(gapX, gapY), Color.Red);
            }
        }

        private void drawTimePlayed()
        {
            TimeSpan timePlayed = new TimeSpan(_timePlayed.Days, _timePlayed.Hours, _timePlayed.Minutes, _timePlayed.Seconds, 0);
            string sTimePlayed = timePlayed.ToString();
            Vector2 _msgSize = _flashMessageFont.MeasureString(sTimePlayed);
            int gapX = (_windowConfiguration.width - (int)_msgSize.X) / 2;
            int gapY = 0;
            _spriteBatch.DrawString(_flashMessageFont, sTimePlayed, new Vector2(gapX, gapY), Color.White);
        }

        private void drawNotePrints()
        {
            int gapX = (_windowConfiguration.width - 400) / 2;
            int gapY = 50;
            _spriteBatch.Draw(_spriteTextures[4], new Vector2(gapX + 0, gapY + 0), Color.White);
            _spriteBatch.Draw(_spriteTextures[5], new Vector2(gapX + 100, gapY + 0), Color.White);
            _spriteBatch.Draw(_spriteTextures[6], new Vector2(gapX + 200, gapY + 0), Color.White);
            _spriteBatch.Draw(_spriteTextures[7], new Vector2(gapX + 300, gapY + 0), Color.White);
        }
    }
}
