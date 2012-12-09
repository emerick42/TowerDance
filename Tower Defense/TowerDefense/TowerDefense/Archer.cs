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

namespace TowerDefense
{
    class Archer : Unit
    {
        SpriteObject [] spriteObject;
        ACTION currentSprite;
        ACTION previousSprite;
        DIRECTION direction;
        EntityUnit recipientUnit;

        public Archer() : base(5, 4, 900, 400, 200, false)
        {
            spriteObject = new SpriteObject[3];
            currentSprite = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public Archer(bool newEnnemy)
            : base(5, 4, 900, 400, 200, newEnnemy)
        {
            spriteObject = new SpriteObject[3];
            currentSprite = ACTION.MOVE;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public Archer(int newLife, int newDamage, int coolDownShoot, 
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
                spriteObject[(int)(ACTION.DIE)] = new SpriteObject(content.Load<Texture2D>("ArcherDieEnnemy"), 74, 88, 3, 180f);
                spriteObject[(int)(ACTION.MOVE)] = new SpriteObject(content.Load<Texture2D>("ArcherWalkEnnemy"), 78, 88, 5, 120f);
                spriteObject[(int)(ACTION.ATTACK)] = new SpriteObject(content.Load<Texture2D>("ArcherHitEnnemy"), 112, 88, 29, 60f);
            }
            else
            {
                spriteObject[(int)(ACTION.DIE)] = new SpriteObject(content.Load<Texture2D>("ArcherDieAllies"), 74, 88, 3, 180f);
                spriteObject[(int)(ACTION.MOVE)] = new SpriteObject(content.Load<Texture2D>("ArcherWalkAllies"), 78, 88, 5, 120f);
                spriteObject[(int)(ACTION.ATTACK)] = new SpriteObject(content.Load<Texture2D>("ArcherHitAllies"), 112, 88, 29, 60f);
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

        public override void update(GameTime gameTime)
        {
            currentCoolDownShoot -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            currentCoolDownWalk -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (currentSprite == ACTION.DIE)
            {
                spriteObject[(int)(currentSprite)].Update(gameTime);
                if (spriteObject[(int)(currentSprite)].Finish == true)
                    outWorld = true;
            }

            if (currentSprite == ACTION.ATTACK)
            {
                if (previousSprite == ACTION.MOVE)
                    spriteObject[(int)(previousSprite)].reset();

                if (currentCoolDownShoot <= 0)
                {
                    spriteObject[(int)(currentSprite)].Update(gameTime);

                    if (spriteObject[(int)(currentSprite)].Finish == true)
                    {
                        currentCoolDownShoot = coolDownShoot;
                        currentCoolDownWalk = coolDownWalk;

                        recipientUnit.LifePoint -= damage;
                        spriteObject[(int)(currentSprite)].Finish = false;
                    }
                }
            }

            if (currentSprite == ACTION.MOVE && currentCoolDownWalk <= 0)
            {
                if (previousSprite == ACTION.ATTACK)
                    spriteObject[(int)(previousSprite)].reset();

                move(direction);
                spriteObject[(int)(currentSprite)].Update(gameTime);
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
