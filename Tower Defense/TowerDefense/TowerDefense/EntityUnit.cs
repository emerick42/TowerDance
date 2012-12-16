using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    class EntityUnit : Entity
    {
        protected int maxPoint;
        protected int lifePoint;
        protected bool ennemy;
        protected CollideSphere boundingSphere;
        protected bool outWorld;

        public EntityUnit(int newLife, bool newEnnemy)
        {
            maxPoint = newLife;
            lifePoint = newLife;
            ennemy = newEnnemy;
            outWorld = false;
        }

        public override void load(ContentManager content) {}
        public override void unload() { }
        public override void update(GameRessource gameRessource) { }
        public override void draw(SpriteBatch sb) { }

        public virtual void drawLife(SpriteBatch sb, DrawPrimitive dp)
        {
        }

        public override bool isAvailable()
        {
            if (outWorld)
                return false;
            return true;
        }

        public int pourcentLife(int ratio)
        {
            int res;

            res = (int)(lifePoint * (ratio * 2) / maxPoint);
            return res - ratio;
        }

        public virtual void setAction(EntityUnit entityUnit, bool attack) { }
        
        public int LifePoint
        {
            get { return lifePoint; }
            set { lifePoint = value; }
        }

        public bool Ennemy
        {
            get { return ennemy; }
            set { ennemy = value; }
        }

        public CollideSphere Sphere
        {
            get { return boundingSphere; }
        }

        public bool OutWorld
        {
            get { return outWorld; }
            set { outWorld = value; }
        }
    }
}
