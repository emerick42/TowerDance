using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDR
{
    interface IGameable
    {
        void initialize();
        void loadContent(GraphicsDevice graphicsDevice);
        void unloadContent();
        void update(GameTime gameTime);
        void draw(GameTime gameTime);
    }
}
