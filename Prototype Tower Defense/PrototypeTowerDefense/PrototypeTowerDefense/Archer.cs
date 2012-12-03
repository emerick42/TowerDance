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
    class Archer : Unit
    {
        SpriteObject [] spriteObject;
        int currentSprite;
        DIRECTION direction;

        public Archer() : base(5, 2, 10, 8, 3, false)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
            if (ennemy == false)
                position = new Vector2(Game1.sizeWidth / 2, Game1.sizeHeight / 2);
        }

        public Archer(bool newEnnemy) : base(5, 2, 10, 8, 3, newEnnemy)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
            if (ennemy == false)
                position = new Vector2(Game1.sizeWidth / 2, Game1.sizeHeight / 2);
        }

        public Archer(int newLife, int newDamage, int coolDownShoot, 
                      int coolDownWalk, int newRange, bool newEnnemy)
            : base(newLife, newDamage, coolDownShoot, coolDownWalk, newRange, newEnnemy)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
            if (ennemy == false)
                position = new Vector2(Game1.sizeWidth / 2, Game1.sizeHeight / 2);
        }

        public void load(ContentManager content)
        {
            if (ennemy)
            {
                spriteObject[0] = new SpriteObject(content.Load<Texture2D>("ArcherWalkEnnemy"), 78, 88, 5, 120f);
                spriteObject[1] = new SpriteObject(content.Load<Texture2D>("ArcherWalkEnnemy"), 104, 88, 29, 100f);
            }
            else
            {
                spriteObject[0] = new SpriteObject(content.Load<Texture2D>("ArcherWalkAllies"), 78, 88, 5, 120f);
                spriteObject[1] = new SpriteObject(content.Load<Texture2D>("ArcherWalkAllies"), 104, 88, 29, 100f);
            }
        }

        public void unload()
        {
            spriteObject[0].unload();
            spriteObject[1].unload();
        }

        public void setPositionDirection(Vector2 newPosition, DIRECTION newDirection)
        {
            position = newPosition;
            direction = newDirection;
        }

        public void update(GameTime gameTime)
        {
            move(direction);

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
