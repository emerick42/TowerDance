﻿using System;
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

namespace TowerDefense
{
    class Warrior : Unit
    {
        SpriteObject[] spriteObject;
        ACTION currentSprite;
        ACTION previousSprite;
        DIRECTION direction;
        EntityUnit recipientUnit;

        public Warrior() : base(10, 3, 1000, 500, 40, false)
        {
            spriteObject = new SpriteObject[3];
            currentSprite = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public Warrior(bool newEnnemy) : base(10, 3, 1000, 500, 40, newEnnemy)
        {
            spriteObject = new SpriteObject[3];
            currentSprite = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public Warrior(int newLife, int newDamage, int coolDownShoot, 
                       int coolDownWalk, int newRange, bool newEnnemy)
            : base(newLife, newDamage, coolDownShoot, coolDownWalk, newRange, newEnnemy)
        {
            spriteObject = new SpriteObject[3];
            currentSprite = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public override void load(ContentManager content)
        {
            if (ennemy)
            {
                spriteObject[(int)(ACTION.DIE)] = new SpriteObject(content.Load<Texture2D>("WarriorDieEnnemy"), 64, 95, 3, 180f);
                spriteObject[(int)(ACTION.MOVE)] = new SpriteObject(content.Load<Texture2D>("WarriorWalkEnnemy"), 70, 95, 5, 120f);
                spriteObject[(int)(ACTION.ATTACK)] = new SpriteObject(content.Load<Texture2D>("WarriorHitEnnemy"), 102, 95, 20, 70f);
            }
            else
            {
                spriteObject[(int)(ACTION.DIE)] = new SpriteObject(content.Load<Texture2D>("WarriorDieAllies"), 64, 95, 1, 180f);
                spriteObject[(int)(ACTION.MOVE)] = new SpriteObject(content.Load<Texture2D>("WarriorWalkAllies"), 70, 95, 5, 120f);
                spriteObject[(int)(ACTION.ATTACK)] = new SpriteObject(content.Load<Texture2D>("WarriorHitAllies"), 102, 95, 20, 70f);
            }
        }

        public override void unload()
        {
            spriteObject[(int)(ACTION.DIE)].unload();
            spriteObject[(int)(ACTION.MOVE)].unload();
            spriteObject[(int)(ACTION.ATTACK)].unload();
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

            if (currentSprite == ACTION.DIE)
            {
                spriteObject[(int)(currentSprite)].Update(gameRessource);
                if (spriteObject[(int)(currentSprite)].Finish == true)
                {
                    if (ennemy)
                    {
                        gameRessource.Gold += 15;
                        gameRessource.ExpCurrentGame += 15;
                    }

                    outWorld = true;
                }
            }

            if (currentSprite == ACTION.ATTACK)
            {
                if (previousSprite == ACTION.MOVE)
                    spriteObject[(int)(previousSprite)].reset();

                if (currentCoolDownShoot <= 0)
                {
                    spriteObject[(int)(currentSprite)].Update(gameRessource);

                    if (spriteObject[(int)(currentSprite)].Finish == true)
                    {
                        currentCoolDownShoot = coolDownShoot;
                        currentCoolDownWalk = coolDownWalk;

                        recipientUnit.LifePoint -= damage;
                        if (recipientUnit.LifePoint <= 0)
                            recipientUnit.LifePoint = 0;
                        spriteObject[(int)(currentSprite)].Finish = false;
                    }
                }
            }

            else if (currentSprite == ACTION.MOVE && currentCoolDownWalk <= 0)
            {
                if (previousSprite == ACTION.ATTACK)
                    spriteObject[(int)(previousSprite)].reset();

                    move(direction);
                    spriteObject[(int)(currentSprite)].Update(gameRessource);
            }

            if (lifePoint <= 0)
                currentSprite = ACTION.DIE;

            previousSprite = currentSprite;

            boundingSphere = new CollideSphere(position.X, position.Y, range);
        }

        public override void draw(SpriteBatch sb)
        {
            SpriteEffects effect;

            if (direction == DIRECTION.LEFT || direction == DIRECTION.DOWN)
                effect = SpriteEffects.None;
            else
                effect = SpriteEffects.FlipHorizontally;

            spriteObject[(int)(currentSprite)].draw(sb, effect, position);
        }

        public override void drawLife(SpriteBatch sb, DrawPrimitive dp)
        {
            dp.drawLine(sb, 3.5f, Color.Green, new Vector2(position.X - 30, position.Y - 20), new Vector2(position.X + pourcentLife(30), position.Y - 20));
        }

        public override void setAction(EntityUnit entityUnit, bool attack)
        {
            if (currentSprite != ACTION.DIE)
            {
                if (attack)
                {
                    currentSprite = ACTION.ATTACK;
                    recipientUnit = entityUnit;
                }
                else
                    currentSprite = ACTION.MOVE;
            }
        }

        public SpriteObject[] SpriteObject
        {
            get { return spriteObject; }
        }

        public SpriteObject this[int index]
        {
            get { return spriteObject[index]; }
        }

        public ACTION CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }

        public DIRECTION Direction
        {
            get { return direction; }
            set { direction = value; }
        }
    }
}
