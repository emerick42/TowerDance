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
    class Arrow : Unit
    {
        SpriteObject spriteObject;
        DIRECTION direction;
        EntityUnit recipientUnit;
        ACTION currentSprite;
        bool hit;

        public Arrow() 
            : base(1000, 4, 0, 0, 30, false)
        {
            hit = false;
            currentSprite = ACTION.MOVE;
        }

        public Arrow(bool newEnnemy)
            : base(1000, 4, 0, 0, 30, newEnnemy)
        {
            hit = false;
            currentSprite = ACTION.MOVE;
        }

        public override void load(ContentManager content)
        {
            spriteObject = new SpriteObject(content.Load<Texture2D>("Arrow"), 53, 6, 1, 100f);
        }

        public override void unload()
        {
            spriteObject.unload();
        }

        public void setPositionDirection(Vector2 newPosition, DIRECTION newD)
        {
            position = newPosition;
            direction = newD;
        }

        public void reset()
        {
            outWorld = false;
            lifePoint = 1000;
            currentSprite = ACTION.MOVE;
            spriteObject.reset();
        }

        public override void update(GameRessource gameRessource)
        {
            if (currentSprite == ACTION.ATTACK)
            {
                recipientUnit.LifePoint -= damage;
                outWorld = true;
                lifePoint = 0;
                hit = false;
            }
            else
                move(direction);

            boundingSphere = new CollideSphere(position.X, position.Y, range);
            spriteObject.Update(gameRessource);
        }

        public override void setAction(EntityUnit entityUnit, bool attack)
        {
            if (attack)
            {
                currentSprite = ACTION.ATTACK;
                recipientUnit = entityUnit;
            }
            else
                currentSprite = ACTION.MOVE;
        }

        public override void draw(SpriteBatch sb)
        {
            SpriteEffects effect;

            if (direction == DIRECTION.LEFT || direction == DIRECTION.DOWN)
                effect = SpriteEffects.None;
            else
                effect = SpriteEffects.FlipHorizontally;

            spriteObject.draw(sb, effect, position);
        }

        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }
    }
}
