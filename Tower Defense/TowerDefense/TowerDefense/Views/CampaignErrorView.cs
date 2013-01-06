using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TowerDance.Views
{
    class CampaignErrorView : IView
    {
        GraphicsDevice _graphicsDevice;
        WindowConfiguration _windowConfiguration;
        SpriteBatch _spriteBatch;
        SpriteFont _menuFont;

        public CampaignErrorView()
        {

        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _windowConfiguration = windowConfiguration;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _menuFont = contentManager.Load<SpriteFont>("MenuFont");
        }

        public void draw()
        {
            string t = "You don't have the required songs to play the campaign";
            Vector2 _msgSize = _menuFont.MeasureString(t);
            int gapX = (_windowConfiguration.width - (int)_msgSize.X) / 2;
            int gapY = (_windowConfiguration.height - (int)_msgSize.Y) / 2;
            Vector2 pos = new Vector2(gapX, gapY);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_menuFont, t, pos, Color.White);
            _spriteBatch.End();
        }
    }
}
