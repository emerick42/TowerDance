using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TowerDance.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TowerDance.Views
{
    class TextView : IView
    {
        GraphicsDevice _graphicsDevice;
        WindowConfiguration _windowConfiguration;
        string _text;
        SpriteBatch _spriteBatch;
        SpriteFont _menuFont;

        public TextView(string text)
        {
            _text = text;
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _windowConfiguration = windowConfiguration;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _menuFont = contentManager.Load<SpriteFont>("TextFont");
        }

        public void draw()
        {
            Vector2 _msgSize = _menuFont.MeasureString(_text);
            int gapX = (_windowConfiguration.width - (int)_msgSize.X) / 2;
            int gapY = (_windowConfiguration.height - (int)_msgSize.Y) / 2;
            Vector2 pos = new Vector2(gapX, gapY);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_menuFont, _text, pos, Color.White);
            _spriteBatch.End();
        }
    }
}
