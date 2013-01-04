using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TowerDance.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using TowerDance.Controllers;

namespace TowerDance.Views
{
    class ControlSelectView : IView
    {
        GraphicsDevice _graphicsDevice;
        WindowConfiguration _windowConfiguration;
        SpriteBatch _spriteBatch;
        SpriteFont _menuTitleFont;
        List<PlayerSelectPosition> _p = new List<PlayerSelectPosition>() { PlayerSelectPosition.NonAssigned, PlayerSelectPosition.NonExisting };
        List<PlayerSelectState> _pState = new List<PlayerSelectState>() { PlayerSelectState.Selecting, PlayerSelectState.NonExisting };

        public ControlSelectView()
        {

        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _windowConfiguration = windowConfiguration;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _menuTitleFont = contentManager.Load<SpriteFont>("MenuTitleFont");
        }

        public void draw()
        {
            _spriteBatch.Begin();
            drawTitle();
            drawPositionPrint();
            drawPlayersCursor();
            _spriteBatch.End();
        }

        public void setPlayersPosition(List<PlayerSelectPosition> p)
        {
            _p = p;
        }

        public void setPlayersState(List<PlayerSelectState> pState)
        {
            _pState = pState;
        }

        private void drawTitle()
        {
            string title = "SELECT YOUR ROLE";
            Vector2 msgSize = _menuTitleFont.MeasureString(title);
            int gapX = (int)((_windowConfiguration.width - msgSize.X) / 2.0f);
            int gapY = 50;
            _spriteBatch.DrawString(_menuTitleFont, title, new Vector2(gapX, gapY), Color.White);
        }

        private void drawPositionPrint()
        {
            string text = "Non assigned";
            Vector2 msgSize = _menuTitleFont.MeasureString(text);
            msgSize /= 2;
            Vector2 pos = new Vector2(_windowConfiguration.width / 2.0f, _windowConfiguration.height / 3.0f);
            _spriteBatch.DrawString(_menuTitleFont, text, pos - msgSize, Color.White);
            text = "Dance";
            msgSize = _menuTitleFont.MeasureString(text);
            msgSize /= 2;
            pos = new Vector2(_windowConfiguration.width / 4.0f, _windowConfiguration.height / 4.0f);
            _spriteBatch.DrawString(_menuTitleFont, text, pos - msgSize, Color.White);
            text = "Tower Defense";
            msgSize = _menuTitleFont.MeasureString(text);
            msgSize /= 2;
            pos = new Vector2(_windowConfiguration.width * 3.0f / 4.0f, _windowConfiguration.height / 4.0f);
            _spriteBatch.DrawString(_menuTitleFont, text, pos - msgSize, Color.White);
            text = "Crazy mode (both)";
            msgSize = _menuTitleFont.MeasureString(text);
            msgSize /= 2;
            pos = new Vector2(_windowConfiguration.width / 2.0f, _windowConfiguration.height * 2.0f / 3.0f);
            _spriteBatch.DrawString(_menuTitleFont, text, pos - msgSize, Color.White);
        }

        private void drawPlayersCursor()
        {
            int player = 0;
            int gapY = 0;
            foreach (PlayerSelectPosition p in _p)
            {
                Color usedColor = Color.Yellow;
                Color usedBackgroundColor = Color.DarkOrange;
                SpriteFont usedFont = _menuTitleFont;
                if (p == PlayerSelectPosition.NonExisting)
                    continue;
                string text = "Player " + (player + 1).ToString();
                if (_pState[player] == PlayerSelectState.Ready)
                {
                    usedColor = Color.GreenYellow;
                    usedBackgroundColor = Color.Green;
                }
                Vector2 msgSize = usedFont.MeasureString(text);
                msgSize /= 2;
                Vector2 pos = new Vector2();
                if (p == PlayerSelectPosition.NonAssigned)
                    pos = new Vector2(_windowConfiguration.width / 2.0f, _windowConfiguration.height / 3.0f);
                else if (p == PlayerSelectPosition.Dance)
                    pos = new Vector2(_windowConfiguration.width / 4.0f, _windowConfiguration.height / 4.0f);
                else if (p == PlayerSelectPosition.TowerDefense)
                    pos = new Vector2(_windowConfiguration.width * 3.0f / 4.0f, _windowConfiguration.height / 4.0f);
                else if (p == PlayerSelectPosition.Both)
                    pos = new Vector2(_windowConfiguration.width / 2.0f, _windowConfiguration.height * 2.0f / 3.0f);
                gapY += (int)msgSize.Y + 20;
                pos.Y += gapY;
                _spriteBatch.DrawString(usedFont, text, pos - msgSize + new Vector2(2, 2), usedBackgroundColor);
                _spriteBatch.DrawString(usedFont, text, pos - msgSize, usedColor);
                player++;
            }
        }
    }
}
