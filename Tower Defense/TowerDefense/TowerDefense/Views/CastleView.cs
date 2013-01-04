using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.Dance;
using TowerDance.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using TowerDance.Models;

namespace TowerDance
{
    class CastleView : IView
    {
        ConfViewCastle castleConfView;
        Castle castle;
        SpriteBatch sb;
        DrawPrimitive dp;
        GraphicsDevice _graphicsDevice;
        ContentManager _contentManager;
        bool newCastle;

        public CastleView()
        {
            dp = new DrawPrimitive();
            castleConfView = new ConfViewCastle();
            newCastle = true;
        }

        public void setCastle(Castle _newCastle)
        {
            castle = _newCastle;
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _graphicsDevice = graphicsDevice;
            sb = new SpriteBatch(_graphicsDevice);
            _contentManager = contentManager;
            dp.load(graphicsDevice);
        }

        public bool isNewCastle()
        {
            if (newCastle == true)
                return true;
            return false;
        }

        public void loadSprite()
        {
            newCastle = false;
            castleConfView.load(_contentManager);
        }

        public void unload()
        {
            castleConfView.unload();
        }

        public void setRefreshList(Castle refreshCastle)
        {
            castle = new Castle(refreshCastle);
        }

        public void update(GameTime gameTime)
        {
            if (castleConfView.isAvaible())
                castleConfView.update(gameTime, 0);
        }

        public void draw()
        {
            sb.Begin();
            castleConfView.draw(sb, dp, castle.Position, castle.LifePoint, castle.MaxPoint);
            sb.End();
        }
    }
}
