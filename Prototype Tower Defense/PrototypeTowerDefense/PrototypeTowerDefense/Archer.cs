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
    class Archer
    {
        public ConfSprite[] confSprite;
        int currentSprite;
        bool action;
        Vector2 position;
        int direction;
        ContentManager content;
        float timer;
        float interval = 100f;
        int frame = 0;
        bool ally;

        public Archer(Vector2 pos, int directionW, ContentManager contentW, bool all)
        {
            position = pos;
            currentSprite = 0;
            action = false;
            direction = directionW;
            confSprite = new ConfSprite[2];
            content = contentW;
            ally = all;
        }

        public void load()
        {
            if (ally == true)
            {
                confSprite[0] = new ConfSprite(content.Load<Texture2D>("ArcherWalkAllies"), 78, 88, 5, 120f, 5);
                confSprite[1] = new ConfSprite(content.Load<Texture2D>("ArcherHitAllies"), 112, 88, 29, 100f, 29);
            }
            else
            {
                confSprite[0] = new ConfSprite(content.Load<Texture2D>("ArcherWalkEnnemy"), 78, 88, 5, 120f, 5);
                confSprite[1] = new ConfSprite(content.Load<Texture2D>("ArcherHitEnnemy"), 112, 88, 29, 100f, 29);
            }
        }

        public void unload()
        {
            confSprite[0].unload();
            confSprite[1].unload();
        }

        public void update(GameTime gameTime, KeyboardState key, GamePadState pad)
        {
            if (action || key.IsKeyDown(Keys.Space))
            {
                action = true;
                currentSprite = 1;

                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > interval)
                {
                    //If is incrememnt current frame
                    frame += 1;


                    //Check frame is within direction frames, if not set back to standing
                    if (frame >= 29)
                    {
                        frame = 0;
                        action = false;
                        confSprite[currentSprite].resetVar();
                        currentSprite = 0;
                        confSprite[currentSprite].resetVar();
                    }
                    //Reset timer
                    timer = 0f;
                }
                confSprite[currentSprite].updateSprite(gameTime);
            }
            else
            {
                if (direction == 1 && position.X <= 1280 / 2)
                    direction = 2;
                else if (direction == 2 && position.X >= 1280)
                    direction = 1;

                if (direction == 3 && position.Y <= 720 / 2)
                    direction = 4;
                else if (direction == 4 && position.Y >= 720)
                    direction = 3;

                if (direction == 2)
                    position.X += 5;
                else if (direction == 1) position.X -= 5;
                else if (direction == 3) position.Y -= 5;
                else position.Y += 5;

                confSprite[currentSprite].updateSprite(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (direction == 1 || direction == 4)
                spriteBatch.Draw(confSprite[currentSprite].Texture, position, confSprite[currentSprite].SourceRect, Color.White, 0f, confSprite[currentSprite].Origin, 1f, SpriteEffects.None, 0);
            else if (direction == 2 || direction == 3)
                spriteBatch.Draw(confSprite[currentSprite].Texture, position, confSprite[currentSprite].SourceRect, Color.White, 0f, confSprite[currentSprite].Origin, 1f, SpriteEffects.FlipHorizontally, 0);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D getTtexture()
        {
            return confSprite[currentSprite].Texture;
        }

        public Rectangle getSource()
        {
            return confSprite[currentSprite].SourceRect;
        }

        public Vector2 getOrigin()
        {
            return confSprite[currentSprite].Origin;
        }
    }
}
