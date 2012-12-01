using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DDR
{
    interface IGameable
    {
        void initialize();
        void loadContent();
        void unloadContent();
        void update(GameTime gameTime);
        void draw(GameTime gameTime);
    }
}
