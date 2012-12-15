using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    class View
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        DrawPrimitive dp;

        public View()
        {
            dp = new DrawPrimitive();
        }

        public View(GraphicsDeviceManager g)
        {
            graphics = g;
            dp = new DrawPrimitive();
        }

        public void initialize()
        {
            this.graphics.PreferredBackBufferWidth = ControllerGame.sizeWidth;
            this.graphics.PreferredBackBufferHeight = ControllerGame.sizeHeight;
            this.graphics.IsFullScreen = false;
            this.graphics.ApplyChanges();
        }

        public void load(GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            dp.load(graphicsDevice);
        }

        public void draw(List<Entity> listEntity, World world)
        {
            spriteBatch.Begin();

            world.draw(spriteBatch);

            foreach (EntityUnit entity in listEntity)
                entity.draw(spriteBatch);
            foreach (EntityUnit entity in listEntity)
                entity.drawLife(spriteBatch, dp);

            spriteBatch.End();
        }


        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        public DrawPrimitive DrawPrimitive
        {
            get { return dp; }
            set { dp = value; }
        }

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return graphics; }
            set { graphics = value; }
        }

    }
}
