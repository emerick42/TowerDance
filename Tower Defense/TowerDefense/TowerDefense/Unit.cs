using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDance
{
    public enum ACTION
    {
        DIE = 0,
        MOVE,
        ATTACK
    }

    class Unit : EntityUnit
    {
        protected int damage;
        protected int coolDownShoot;
        protected int coolDownWalk;
        protected int coolDownDie;
        protected float currentCoolDownShoot;
        protected float currentCoolDownWalk;
        protected float currentCoolDownDie;
        protected float timer;
        protected int range;

        public Unit(int life, int newDamage, int coolD, int coolW, int newRange, bool ennemy, ENTITYTYPE type)
            : base(life, ennemy, type)
        {
            damage = newDamage;
            coolDownShoot = coolD;
            coolDownWalk = coolW;
            coolDownDie = 250;
            currentCoolDownShoot = 0;
            currentCoolDownWalk = 0;
            currentCoolDownDie = 0;
            range = newRange;
        }

        public override void load(ContentManager content) {}
        public override void unload() { }
        public override void update(GameRessource gameRessource) { }
        public override void draw(SpriteBatch sb) {}
        public override void setAction(EntityUnit entityUnit, bool attack) { }
        public override void drawLife(SpriteBatch sb, DrawPrimitive dp) { }

        public void move(DIRECTION direction)
        {
            int limitX = 0;
            int limitY = 0;

            if (ennemy)
            {
                limitX = World.sizeWidth / 2;
                limitY = World.sizeHeight / 2;
            }
                
            else
            {
                if (direction == DIRECTION.UP)
                    limitY = 0;
                else if (direction == DIRECTION.DOWN)
                    limitY = World.sizeHeight;
                else if (direction == DIRECTION.LEFT)
                    limitX = 0;
                else
                    limitX = World.sizeWidth;
            }

            switch (direction)
            {
                case DIRECTION.DOWN:
                    {
                        if (position.Y <= limitY)
                            position.Y += 1;
                    }
                    break;
                case DIRECTION.UP:
                    {
                        if (position.Y >= limitY)
                            position.Y -= 1;
                    }
                    break;
                case DIRECTION.RIGHT:
                    {
                        if (position.X <= limitX)
                            position.X += 1;
                    }
                    break;
                case DIRECTION.LEFT:
                    {
                        if (position.X >= limitX)
                            position.X -= 1;
                    }
                    break;
            }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
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

        public float Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        public float CurrentCoolDownDie
        {
            get { return currentCoolDownDie; }
            set { currentCoolDownDie = value; }
        }
    }
}
