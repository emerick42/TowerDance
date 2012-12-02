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

namespace PrototypeTowerDefense
{
    class Warrior : Unit
    {
        SpriteObject[] spriteObject;
        int currentSprite;
        DIRECTION direction;

        public Warrior() : base(10, 2, 15, 1, false)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
        }

        public Warrior(bool newEnnemy) : base(5, 2, 10, 3, newEnnemy)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
        }

        public Warrior(int newLife, int newDamage, int newCoolDown, int newRange, bool newEnnemy)
            : base(newLife, newDamage, newCoolDown, newRange, newEnnemy)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
        }

        public void load(ContentManager content)
        {
            if (ennemy)
            {
                spriteObject[0] = new SpriteObject(content.Load<Texture2D>("WarriorWalkAllies"), 70, 95, 5, 120f);
                spriteObject[1] = new SpriteObject(content.Load<Texture2D>("WarriorHitAllies"), 102, 95, 20, 100f);
            }
            else
            {
                spriteObject[0] = new SpriteObject(content.Load<Texture2D>("WarriorWalkEnnemy"), 70, 95, 5, 120f);
                spriteObject[1] = new SpriteObject(content.Load<Texture2D>("WarriorHitEnnemy"), 102, 95, 20, 100f);
            }
        }

        public void unload()
        {
            spriteObject[0].unload();
            spriteObject[1].unload();
        }

        public void setPosition(Vector2 newPosition, DIRECTION newDirection)
        {
            position = newPosition;
            direction = newDirection;
        }

        public void update(GameTime gameTime)
        {
            moveSprite();

            spriteObject[currentSprite].Update(gameTime);
        }

        public void draw(SpriteBatch sb)
        {
            SpriteEffects effect;

            if (direction == DIRECTION.LEFT || direction == DIRECTION.DOWN)
                effect = SpriteEffects.None;
            else
                effect = SpriteEffects.FlipHorizontally;

            spriteObject[currentSprite].draw(sb, effect, position);
        }

        private void moveSprite()
        {
            switch (direction)
            {
                case DIRECTION.DOWN:
                    {
                        if (position.Y <= Game1.sizeHeight / 2)
                            position.Y += 1;
                    }
                    break;
                case DIRECTION.UP:
                    {
                        if (position.Y >= Game1.sizeHeight / 2)
                            position.Y -= 1;
                    }
                    break;
                case DIRECTION.RIGHT:
                    {
                        if (position.X <= Game1.sizeWidth / 2)
                            position.X += 1;
                    }
                    break;
                case DIRECTION.LEFT:
                    {
                        if (position.X >= Game1.sizeWidth / 2)
                            position.X -= 1;
                    }
                    break;
            }
        }

        public SpriteObject[] SpriteObject
        {
            get { return spriteObject; }
        }

        public int CurrentSprite
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
