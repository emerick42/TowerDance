using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDance
{
    abstract class AConfView
    {
        protected ACTION currentSprite;
        protected ACTION previousSprite;
        protected DIRECTION direction;
        protected int coolDownShoot;
        protected int coolDownWalk;
        protected int coolDownDie;
        protected float currentCoolDownShoot;
        protected float currentCoolDownWalk;
        protected float currentCoolDownDie;
        protected bool outWorld;

        public AConfView()
        {
            currentSprite = ACTION.MOVE;
            outWorld = false;
        }

        public AConfView(DIRECTION newDirection)
        {
            direction = newDirection;          
            currentSprite = ACTION.MOVE;
            outWorld = false;

        }

        public abstract void load(ContentManager content);
        public abstract void loadAllie(ContentManager content);
        public abstract void loadEnnemy(ContentManager content);
        public abstract void unload();
        public abstract void update(GameTime gameTime, ACTION newAction);
        public abstract void draw(SpriteBatch sb, DrawPrimitive dp, Vector2 position, int life, int maxLife);


        public ACTION CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }

        public ACTION PreviousSprite
        {
            get { return previousSprite; }
            set { previousSprite = value; }
        }

        public DIRECTION Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int CoolDownShoot
        {
            get { return coolDownShoot; }
            set { coolDownShoot = value; }
        }

        public int CoolDownWalk
        {
            get { return coolDownWalk; }
            set { coolDownWalk = value; }
        }

        public int CoolDownDie
        {
            get { return coolDownDie; }
            set { coolDownDie = value; }
        }

        public bool OutWorld
        {
            get { return outWorld; }
            set { outWorld = value; }
        }

        public float CurrentCoolDownShoot
        {
            get { return currentCoolDownShoot; }
            set { currentCoolDownShoot = value; }
        }

        public float CurrentCoolDownWalk
        {
            get { return currentCoolDownWalk; }
            set { currentCoolDownWalk = value; }
        }

        public float CurrentCoolDownDie
        {
            get { return currentCoolDownDie; }
            set { currentCoolDownDie = value; }
        }
    }
}
