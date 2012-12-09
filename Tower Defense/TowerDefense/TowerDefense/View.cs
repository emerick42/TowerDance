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

        public View()
        {
        }

        public void load(GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void draw(List<Entity> listEntity, World world)
        {
            spriteBatch.Begin();

            world.draw(spriteBatch);

            foreach (EntityUnit entity in listEntity)
                entity.draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
