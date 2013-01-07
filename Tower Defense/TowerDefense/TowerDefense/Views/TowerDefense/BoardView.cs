using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using TowerDance.Models.TowerDefense;

namespace TowerDance.Views.TowerDefense
{
    class BoardView : IView
    {
        TowerDance.Models.WindowConfiguration _windowConfiguration;
        SpriteBatch _spriteBatch;
        List<Texture2D> _spriteTextures = new List<Texture2D>();
        GraphicsDevice _graphicsDevice;
        Rectangle _map;
        List<Entity> _entities;
        int _gapX = 0;
        int _gapY = 0;
        float _multiplicator = 1.0f;
        float _maxMana;
        float _currentMana;

        public BoardView(Rectangle map, List<Entity> entities)
        {
            _map = map;
            _entities = entities;
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, TowerDance.Models.WindowConfiguration windowConfiguration)
        {
            _windowConfiguration = windowConfiguration;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            Texture2D rectangleTexture = new Texture2D(graphicsDevice, 1, 1);
            rectangleTexture.SetData(new Color[1] { Color.White });
            _spriteTextures.Add(rectangleTexture);
            _spriteTextures.Add(contentManager.Load<Texture2D>("BackGround"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("castle"));
            _spriteTextures.Add(contentManager.Load<Texture2D>("WarriorStandAllies"));
        }

        public void draw()
        {
            calculateRatios();
            _spriteBatch.Begin();
            drawBackground();
            foreach (Entity e in _entities)
            {
                if (!e.isAlive())
                    continue;
                if (e.getType().Equals("castle"))
                    drawCastle(e);
                else
                    drawEntity(e);
            }
            drawBlackBorder();
            drawMana();
            _spriteBatch.End();
        }

        public void refresh(Rectangle map, List<Entity> entities, float currentMana)
        {
            _map = map;
            _entities = entities;
            _currentMana = currentMana;
        }

        public void setMaxMana(float maxMana)
        {
            _maxMana = maxMana;
        }

        private void drawBackground()
        {
            _spriteBatch.Draw(_spriteTextures[1], new Rectangle(_gapX, _gapY, (int)(_map.Width * _multiplicator), (int)(_map.Height * _multiplicator)), Color.White);
        }

        private void drawBlackBorder()
        {
            if (_gapX > 0)
            {
                _spriteBatch.Draw(_spriteTextures[0], new Rectangle(0, 0, _gapX, _windowConfiguration.height), Color.Black);
                _spriteBatch.Draw(_spriteTextures[0], new Rectangle(_windowConfiguration.width - _gapX, 0, _gapX, _windowConfiguration.height), Color.Black);
            }
            if (_gapY > 0)
            {
                _spriteBatch.Draw(_spriteTextures[0], new Rectangle(0, 0, _windowConfiguration.width, _gapY), Color.Black);
                _spriteBatch.Draw(_spriteTextures[0], new Rectangle(0, _windowConfiguration.height - _gapY, _windowConfiguration.width, _gapY), Color.Black);
            }
        }

        private void drawCastle(Entity castle)
        {
            float percentageHP = (float)castle.getHP() / (float)castle.getMaxHP();
            _spriteBatch.Draw(_spriteTextures[2], applyViewTransform(castle.getPosition(), castle.getSize()), Color.White);
            _spriteBatch.Draw(_spriteTextures[0], applyViewTransform(castle.getPosition() + new Vector2(0, -20), new Vector2(62, 6)), Color.Black);
            _spriteBatch.Draw(_spriteTextures[0], applyViewTransform(castle.getPosition() + new Vector2(0, -20), new Vector2(60 * percentageHP, 4)), Color.Green);
        }

        private void drawEntity(Entity e)
        {
            float percentageHP = (float)e.getHP() / (float)e.getMaxHP();
            Texture2D usedTexture = null;
            Color usedColor = Color.White;
            Color usedHPColor = Color.YellowGreen;
            if (e.getType().Equals("warrior"))
                usedTexture = _spriteTextures[3];
            if (e.getTeam() != 0)
            {
                usedColor = Color.Orange;
                usedHPColor = Color.Red;
            }
            _spriteBatch.Draw(usedTexture, applyViewTransform(e.getPosition(), e.getSize()), usedColor);
            _spriteBatch.Draw(_spriteTextures[0], applyViewTransform(e.getPosition() + new Vector2(0, -15), new Vector2(27, 4)), Color.Black);
            _spriteBatch.Draw(_spriteTextures[0], applyViewTransform(e.getPosition() + new Vector2(0, -15), new Vector2(25 * percentageHP, 2)), usedHPColor);
        }

        private void calculateRatios()
        {
            float ratio = (float)_windowConfiguration.width / (float)_windowConfiguration.height;
            _gapX = 0;
            _gapY = 0;
            if ((float)_map.Width / (float)_map.Height > ratio)
            {
                _multiplicator = (float)_windowConfiguration.width / (float)_map.Width;
                _gapY = (int)(((float)_windowConfiguration.height - _map.Height * _multiplicator) / 2.0f);
            }
            else
            {
                _multiplicator = (float)_windowConfiguration.height / (float)_map.Height;
                _gapX = (int)(((float)_windowConfiguration.width - _map.Width * _multiplicator) / 2.0f);
            }
        }

        private Rectangle applyViewTransform(Vector2 position, Vector2 size)
        {
            position.X = position.X + _map.Width / 2.0f;
            position.X -= size.X / 2.0f;
            position.Y = position.Y + _map.Height / 2.0f;
            position.Y -= size.Y / 2.0f;
            position *= _multiplicator;
            size *= _multiplicator;
            position.X += _gapX;
            position.Y += _gapY;
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        private void drawMana()
        {
            float ratio = _currentMana / _maxMana;
            int gapX = 10;
            int gapY = (int)((_windowConfiguration.height - 9 * (_windowConfiguration.height / 10)) / 2.0f);
            _spriteBatch.Draw(_spriteTextures[0], new Rectangle(gapX - 2, gapY - 2, 34, _windowConfiguration.height - 2 * gapY + 4), Color.Blue);
            _spriteBatch.Draw(_spriteTextures[0], new Rectangle(gapX, gapY, 30, (int)(ratio * (_windowConfiguration.height - 2 * gapY))), Color.Aquamarine);
        }
    }
}
