using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeTowerDefense
{
    class Unit : IObject
    {
        protected int lifePoint;
        protected int damage;
        protected int coolDown;
        protected int range;
        protected bool ennemy;

        public Unit(int life, int newDamage, int coolD, int newRange, bool newEnnemy)
        {
            lifePoint = life;
            damage = newDamage;
            coolDown = coolD;
            range = newRange;
            ennemy = newEnnemy;
        }

        public int LifePoint
        {
            get { return lifePoint; }
            set { lifePoint = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public int CoolDown
        {
            get { return coolDown; }
            set { coolDown = value; }
        }

        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        public bool Ennemy
        {
            get { return ennemy; }
            set { ennemy = value; }
        }
    }
}
