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
    class Warrior : Unit
    {
        SpriteObject[] spriteObject;
        int currentSprite;
        DIRECTION direction;

        public Warrior() : base(10, 2, 15, 10, 1, false)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public Warrior(bool newEnnemy) : base(10, 2, 15, 10, 1, newEnnemy)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public Warrior(int newLife, int newDamage, int coolDownShoot, 
                       int coolDownWalk, int newRange, bool newEnnemy)
            : base(newLife, newDamage, coolDownShoot, coolDownWalk, newRange, newEnnemy)
        {
            spriteObject = new SpriteObject[2];
            currentSprite = 0;
            if (ennemy == false)
                position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public override void load(ContentManager content)
        {
            if (ennemy)
            {
                spriteObject[0] = new SpriteObject(content.Load<Texture2D>("WarriorWalkEnnemy"), 70, 95, 5, 120f);
                spriteObject[1] = new SpriteObject(content.Load<Texture2D>("WarriorHitEnnemy"), 102, 95, 20, 100f);
            }
            else
            {
                spriteObject[0] = new SpriteObject(content.Load<Texture2D>("WarriorWalkAllies"), 70, 95, 5, 120f);
                spriteObject[1] = new SpriteObject(content.Load<Texture2D>("WarriorHitAllies"), 102, 95, 20, 100f);
            }
        }

        public override void unload()
        {
            spriteObject[0].unload();
            spriteObject[1].unload();
        }

        public void setPositionDirection(Vector2 newPosition, DIRECTION newDirection)
        {
            position = newPosition;
            direction = newDirection;
        }

        public override void update(GameTime gameTime)
        {
            move(direction);

            spriteObject[currentSprite].Update(gameTime);
        }

        public override void draw(SpriteBatch sb)
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

        public SpriteObject this[int index]
        {
            get { return spriteObject[index]; }
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
