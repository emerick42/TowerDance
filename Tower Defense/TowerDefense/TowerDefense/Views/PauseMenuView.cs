using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using TowerDance.Models;

namespace TowerDance.Views
{
    class PauseMenuView : IView
    {
        GraphicsDevice _graphicsDevice;
        WindowConfiguration _windowConfiguration;
        Menu _menu;
        SpriteBatch _spriteBatch;
        SpriteFont _menuFont;

        public PauseMenuView(Menu menu)
        {
            _menu = menu;
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
            int menuHeight = 0;
            int maxWidth = 0;
            List<string> menu = _menu.getTitles();
            foreach (string t in menu)
            {
                Vector2 _msgSize = _menuFont.MeasureString(t);
                menuHeight += (int)_msgSize.Y;
                if ((int)_msgSize.X > maxWidth)
                    maxWidth = (int)_msgSize.X;
            }
            Vector2 pos;
            int gapX = 0;
            int gapY = (_windowConfiguration.height - menuHeight) / 2;
            _spriteBatch.Begin();
            int idx = 0;
            int gapY2 = 0;
            Color textColor = Color.Gray;
            foreach (string t in menu)
            {
                textColor = Color.Gray;
                if (idx == _menu.getSelectedTitleIndex())
                    textColor = Color.White;
                Vector2 _msgSize = _menuFont.MeasureString(t);
                gapX = (_windowConfiguration.width - (int)_msgSize.X) / 2;
                pos = new Vector2(gapX, gapY + gapY2);
                _spriteBatch.DrawString(_menuFont, t, pos, textColor);
                idx++;
                gapY2 += (int)_msgSize.Y;
            }
            _spriteBatch.End();
        }
    }
}
