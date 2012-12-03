using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeTowerDefense
{
    class Unit : Entity
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

        public void move(DIRECTION direction)
        {
            int limitX = 0;
            int limitY = 0;

            if (ennemy)
            {
                limitX = Game1.sizeWidth / 2;
                limitY = Game1.sizeHeight / 2;
            }
            else
            {
                if (direction == DIRECTION.UP)
                    limitY = Game1.sizeHeight / 4;
                else if (direction == DIRECTION.DOWN)
                    limitY = Game1.sizeHeight - (Game1.sizeHeight / 4);
                else if (direction == DIRECTION.LEFT)
                    limitX = Game1.sizeWidth / 4;
                else
                    limitX = Game1.sizeWidth - (Game1.sizeWidth / 4);
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
