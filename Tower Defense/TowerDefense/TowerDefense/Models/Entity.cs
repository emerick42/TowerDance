using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDance.Models
{
    public abstract class Entity : Positionnable
    {
        protected ENTITYTYPE type;

        public abstract void load(ContentManager content);
        public abstract void unload();
        public abstract void update(GameRessource gameRessource);
        public abstract void draw(SpriteBatch sb);
        public abstract bool isAvailable();
        public abstract ENTITYTYPE getType();

        public ENTITYTYPE Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
