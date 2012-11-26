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

namespace PrototypeTowerDefense
{
    class Warrior
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

        public Warrior(Vector2 pos, int directionW, ContentManager contentW)
        {
            position = pos;
            currentSprite = 0;
            action = false;
            direction = directionW;
            confSprite = new ConfSprite[2];
            content = contentW;
        }

        public void load()
        {
            confSprite[0] = new ConfSprite(content.Load<Texture2D>("WarriorWalk"), 70, 71, 5, 120f, 5);
            confSprite[1] = new ConfSprite(content.Load<Texture2D>("WarriorHit"), 103, 95, 20, 100f, 20);
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
                    if (frame >= 20)
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
                if (direction == 2 && position.X >= 1280 / 2)
                    direction = 1;
                else if (direction == 1 && position.X <= 0)
                    direction = 2;

                if (direction == 4 && position.Y >= 720 / 2)
                    direction = 3;
                else if (direction == 3 && position.Y <= 0)
                    direction = 4;

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
