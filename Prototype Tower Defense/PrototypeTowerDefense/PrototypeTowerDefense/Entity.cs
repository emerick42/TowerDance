using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeTowerDefense
{
    class Entity : IObject
    {
        protected int lifePoint;
        protected bool ennemy;

        public Entity(int newLife, bool newEnnemy)
        {
            lifePoint = newLife;
            ennemy = newEnnemy;
        }

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
    }
}
