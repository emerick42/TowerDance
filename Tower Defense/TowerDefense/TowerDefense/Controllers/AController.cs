using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance.Controllers
{
    abstract class AController
    {
        public List<AController> children = new List<AController>();

        public abstract void update(GameTime gameTime);
        public abstract void updateBackgrounded(GameTime gameTime);
        public abstract void draw(GameTime gameTime);
        public abstract void drawBackgrounded(GameTime gameTime);
    }
}
