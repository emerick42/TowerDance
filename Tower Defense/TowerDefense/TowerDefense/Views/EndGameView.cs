using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TowerDance.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using TowerDance.Models.TowerDefense;

namespace TowerDance.Views
{
    class EndGameView : IView
    {
        GraphicsDevice _graphicsDevice;
        WindowConfiguration _windowConfiguration;
        SpriteBatch _spriteBatch;
        SpriteFont _menuFont;
        SpriteFont _menuTitleFont;
        Texture2D _backgroundTexture;
        State _gameResult;
        int _expGained;

        public EndGameView(State gameResult, int expGained)
        {
            _gameResult = gameResult;
            _expGained = expGained;
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _windowConfiguration = windowConfiguration;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _menuFont = contentManager.Load<SpriteFont>("MenuFont");
            _menuTitleFont = contentManager.Load<SpriteFont>("MenuTitleFont");
            _backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
            _backgroundTexture.SetData(new Color[] { Color.Black });
        }

        public void draw()
        {
            string t = "";
            Color textColor = Color.White;
            if (_gameResult == State.Won)
            {
                t = "CONGRATULATIONS YOU WON THIS BATTLE !";
                textColor = Color.YellowGreen;
            }
            else
            {
                t = "OUR REALM IS NOW LOST ..";
                textColor = Color.LightGray;
            }
            _spriteBatch.Begin();
            /* Draw the background mask */
            Color c = Color.White;
            c.A = 200;
            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _windowConfiguration.width, _windowConfiguration.height), c);
            /* Draw the result message */
            Vector2 _msgSize = _menuTitleFont.MeasureString(t);
            int gapX = (_windowConfiguration.width - (int)_msgSize.X) / 2;
            int gapY = (_windowConfiguration.height - (int)_msgSize.Y) / 2;
            Vector2 pos = new Vector2(gapX, gapY);
            _spriteBatch.DrawString(_menuTitleFont, t, pos, textColor);
            /* Draw the exp gained */
            _msgSize = _menuFont.MeasureString("By the way, you gained " + _expGained.ToString() + "xp during the fight");
            gapX = (_windowConfiguration.width - (int)_msgSize.X) / 2;
            gapY = _windowConfiguration.height - (int)_msgSize.Y - 30;
            pos = new Vector2(gapX, gapY);
            _spriteBatch.DrawString(_menuFont, "By the way, you gained " + _expGained.ToString() + "xp during the fight", pos, Color.White);
            _spriteBatch.End();
        }
    }
}
