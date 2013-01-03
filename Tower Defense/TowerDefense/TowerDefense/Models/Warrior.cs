using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace TowerDance
{
    public class Warrior : Unit
    {
        ACTION currentAction;
        ACTION previousAction;
        DIRECTION direction;
        EntityUnit recipientUnit;

        public Warrior() : base(10, 3, 1000, 500, 40, false, ENTITYTYPE.WARRIOR)
        {
            currentAction = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(World.sizeWidth / 2, World.sizeHeight / 2);
        }

        public Warrior(bool newEnnemy)
            : base(10, 3, 1000, 500, 40, newEnnemy, ENTITYTYPE.WARRIOR)
        {
            currentAction = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(World.sizeWidth / 2, World.sizeHeight / 2);
        }

        public Warrior(int newLife, int newDamage, int coolDownShoot, 
                       int coolDownWalk, int newRange, bool newEnnemy)
            : base(newLife, newDamage, coolDownShoot, coolDownWalk, newRange, newEnnemy, ENTITYTYPE.WARRIOR)
        {
            currentAction = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(World.sizeWidth / 2, World.sizeHeight / 2);
        }

        public void setPositionDirection(Vector2 newPosition, DIRECTION newDirection)
        {
            position = newPosition;
            direction = newDirection;
        }

        public override void update(GameRessource gameRessource)
        {
            currentCoolDownShoot -= (float)gameRessource.GameTime.ElapsedGameTime.TotalMilliseconds;
            currentCoolDownWalk -= (float)gameRessource.GameTime.ElapsedGameTime.TotalMilliseconds;
            currentCoolDownDie -= (float)gameRessource.GameTime.ElapsedGameTime.TotalMilliseconds;

            if (currentAction == ACTION.DIE)
            {
                if (previousAction != ACTION.DIE)
                {
                    if (ennemy)
                    {
                        gameRessource.Gold += 15;
                        gameRessource.ExpCurrentGame += 15;
                    }
                    coolDownDie = 250;
                }
                if (coolDownDie <= 0)
                    outWorld = true;
            }

            if (currentAction == ACTION.ATTACK && currentCoolDownShoot <= 0)
            {
                    currentCoolDownShoot = coolDownShoot;
                    currentCoolDownWalk = coolDownWalk;

                    recipientUnit.LifePoint -= damage;
                    if (recipientUnit.LifePoint <= 0)
                        recipientUnit.LifePoint = 0;
            }

            else if (currentAction == ACTION.MOVE && currentCoolDownWalk <= 0)
                move(direction);

            if (lifePoint <= 0)
                currentAction = ACTION.DIE;

            previousAction = currentAction;

            boundingSphere = new CollideSphere(position.X, position.Y, range);
        }

        public override void setAction(EntityUnit entityUnit, bool attack)
        {
            if (currentAction != ACTION.DIE)
            {
                if (attack)
                {
                    currentAction = ACTION.ATTACK;
                    recipientUnit = entityUnit;
                }
                else
                    currentAction = ACTION.MOVE;
            }
        }

        public ACTION CurrentAction
        {
            get { return currentAction; }
            set { currentAction = value; }
        }

        public DIRECTION Direction
        {
            get { return direction; }
            set { direction = value; }
        }
    }
}
