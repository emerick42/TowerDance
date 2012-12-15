using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    abstract class Entity : Positionnable
    {
        public abstract void load(ContentManager content);
        public abstract void unload();
        public abstract void update(GameRessource gameRessource);
        public abstract void draw(SpriteBatch sb);
        public abstract bool isAvailable();
    }
}
