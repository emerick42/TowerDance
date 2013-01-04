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
    class BackgroundView : IView
    {
        ConfViewBackground backView;
        Background back;
        SpriteBatch sb;
        DrawPrimitive dp;
        GraphicsDevice _graphicsDevice;
        ContentManager _contentManager;
        bool setBack;

        public BackgroundView()
        {
            setBack = false;
            dp = new DrawPrimitive();
        }

        public void setBackround()
        {
            setBack = true;
            backView = new ConfViewBackground();
            back = new Background();
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _graphicsDevice = graphicsDevice;
            sb = new SpriteBatch(_graphicsDevice);
            _contentManager = contentManager;
            dp.load(graphicsDevice);
        }

        public void loadSprite()
        {
            backView.load(_contentManager);
        }

        public void unload()
        {
            backView.unload();
        }

        public void update(GameTime gameTime)
        {
        }

        public void draw()
        {
            sb.Begin();
            backView.draw(sb, dp, back.Position, 0, 0);
            sb.End();
        }

        public bool SetBack
        {
            get { return setBack; }
        }
    }
}
