using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    class Unit : EntityUnit
    {
        protected int damage;
        protected int coolDownShoot;
        protected int coolDownWalk;
        protected int range;

        public Unit(int life, int newDamage, int coolD, int coolW, int newRange, bool ennemy)
            : base(life, ennemy)
        {
            damage = newDamage;
            coolDownShoot = coolD;
            coolDownWalk = coolW;
            range = newRange;
        }

        public override void load(ContentManager content) {}
        public override void unload() { }
        public override void update(GameTime gameTime) { }
        public override void draw(SpriteBatch sb) {}

        public void move(DIRECTION direction)
        {
            int limitX = 0;
            int limitY = 0;

            if (ennemy)
            {
                limitX = ControllerGame.sizeWidth / 2;
                limitY = ControllerGame.sizeHeight / 2;
            }
            else
            {
                if (direction == DIRECTION.UP)
                    limitY = ControllerGame.sizeHeight / 4;
                else if (direction == DIRECTION.DOWN)
                    limitY = ControllerGame.sizeHeight - (ControllerGame.sizeHeight / 4);
                else if (direction == DIRECTION.LEFT)
                    limitX = ControllerGame.sizeWidth / 4;
                else
                    limitX = ControllerGame.sizeWidth - (ControllerGame.sizeWidth / 4);
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

        public int Range
        {
            get { return range; }
            set { range = value; }
        }

    }
}
