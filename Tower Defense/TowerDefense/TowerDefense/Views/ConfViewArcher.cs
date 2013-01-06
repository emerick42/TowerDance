using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDance.Models
{
    class ConfViewArcher : AConfView
    {
        SpriteObject[] spriteObject;

        public ConfViewArcher() : base()
        {
            spriteObject = new SpriteObject[3];
            coolDownShoot = 2390;
            coolDownWalk = 1990;
            coolDownDie = 250;
            currentCoolDownShoot = 0;
            currentCoolDownWalk = 0;
            currentCoolDownDie = 0;
        }

        public ConfViewArcher(DIRECTION d)
            : base(d)
        {
            spriteObject = new SpriteObject[3];

            coolDownShoot = 2390;
            coolDownWalk = 1990;
            coolDownDie = 250;
            currentCoolDownShoot = 0;
            currentCoolDownWalk = 0;
            currentCoolDownDie = 0;
        }

        public override void loadAllie(ContentManager content)
        {
            spriteObject[(int)(ACTION.DIE)] = new SpriteObject(content.Load<Texture2D>("ArcherDieAllies"), 74, 88, 3, 180f);
            spriteObject[(int)(ACTION.MOVE)] = new SpriteObject(content.Load<Texture2D>("ArcherWalkAllies"), 78, 88, 5, 120f);
            spriteObject[(int)(ACTION.ATTACK)] = new SpriteObject(content.Load<Texture2D>("ArcherHitAllies"), 112, 88, 29, 60f);
        }

        public override void loadEnnemy(ContentManager content)
        {
            spriteObject[(int)(ACTION.DIE)] = new SpriteObject(content.Load<Texture2D>("ArcherDieEnnemy"), 74, 88, 3, 180f);
            spriteObject[(int)(ACTION.MOVE)] = new SpriteObject(content.Load<Texture2D>("ArcherWalkEnnemy"), 78, 88, 5, 120f);
            spriteObject[(int)(ACTION.ATTACK)] = new SpriteObject(content.Load<Texture2D>("ArcherHitEnnemy"), 112, 88, 29, 60f);
        }

        public override void load(ContentManager content) { }

        public override void unload()
        {
            spriteObject[(int)(ACTION.DIE)].unload();
            spriteObject[(int)(ACTION.MOVE)].unload();
            spriteObject[(int)(ACTION.ATTACK)].unload();
        }

        public override void update(GameTime gameTime, ACTION newAction)
        {
            currentSprite = newAction;
            currentCoolDownShoot -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            currentCoolDownWalk -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            currentCoolDownDie -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (currentSprite == ACTION.DIE)
            {
                if (previousSprite != ACTION.DIE)
                {
                    spriteObject[(int)(previousSprite)].reset();
                    currentCoolDownDie = 250f;
                }
                spriteObject[(int)(currentSprite)].Update(gameTime);
                if (spriteObject[(int)(currentSprite)].Finish == true)
                    outWorld = true;
            }

            if (currentSprite == ACTION.ATTACK)
            {
                if (currentCoolDownShoot <= 0)
                {
                    if (previousSprite == ACTION.MOVE)
                        spriteObject[(int)(previousSprite)].reset();
                    spriteObject[(int)(currentSprite)].reset();
                    currentCoolDownShoot = coolDownShoot;
                    currentCoolDownWalk = coolDownWalk;
                }

                if (spriteObject[(int)(currentSprite)].Finish == false)
                    spriteObject[(int)(currentSprite)].Update(gameTime);
            }

            else if (currentSprite == ACTION.MOVE && currentCoolDownWalk <= 0)
            {
                if (previousSprite == ACTION.ATTACK)
                    spriteObject[(int)(previousSprite)].reset();
                spriteObject[(int)(currentSprite)].Update(gameTime);
            }

            previousSprite = currentSprite;
        }

        public override void draw(SpriteBatch sb, DrawPrimitive dp, Vector2 position, int life, int maxLife)
        {
            SpriteEffects effect;

            if (direction == DIRECTION.LEFT || direction == DIRECTION.DOWN)
                effect = SpriteEffects.None;
            else
                effect = SpriteEffects.FlipHorizontally;

            spriteObject[(int)(currentSprite)].draw(sb, effect, position);

            dp.drawLine(sb, 3.5f, Color.Green, new Vector2(position.X - 30, position.Y - 20), new Vector2(position.X + pourcentLife(life, maxLife, 30), position.Y - 20));
        }

        public int pourcentLife(int lifePoint, int maxPoint, int ratio)
        {
            int res;

            res = (int)(lifePoint * (ratio * 2) / maxPoint);
            return res - ratio;
        }

        public SpriteObject[] SpriteObject
        {
            get { return spriteObject; }
        }

        public SpriteObject this[int index]
        {
            get { return spriteObject[index]; }
        }

        public bool isAvaible()
        {
            if (outWorld)
                return true;
            return false;
        }
    }
}
