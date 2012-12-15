﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerDefense
{
    public enum DIRECTION
    {
        LEFT = 0,
        RIGHT,
        UP,
        DOWN
    }

    class SpriteObject
    {
        Texture2D texture;

        int currentFrame;

        int sizeWidth;
        int sizeHeight;
        int nbFrames;

        Rectangle sourceRect;
        Vector2 origin;

        float timer;
        float interval;

        bool finish;

        public SpriteObject() { }

        public SpriteObject(ConfSprite confSprite)
        {
            texture = confSprite.Texture;

            sizeWidth = confSprite.SizeWidth;
            sizeHeight = confSprite.SizeHeight;
            nbFrames = confSprite.NbFrames;

            interval = confSprite.Interval;

            currentFrame = 0;
            timer = 0;

            finish = false;
        }

        public SpriteObject(Texture2D newTexture,
                            int newSizeWidth, int newSizeHeight, int nbFrame,
                            float newInterval) : base()
        {
            texture = newTexture;

            sizeWidth = newSizeWidth;
            sizeHeight = newSizeHeight;
            nbFrames = nbFrame;

            interval = newInterval;

            currentFrame = 0;
            timer = 0;

            finish = false;
        }

        public void unload()
        {
            texture.Dispose();
        }

        public void Update(GameRessource gameRessource)
        {
            sourceRect = new Rectangle(currentFrame * sizeWidth, 0, sizeWidth, sizeHeight);
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

            timer += (float)gameRessource.GameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame += 1;

                if (currentFrame >= nbFrames)
                {
                    finish = true;
                    currentFrame = 0;
                }

                timer = 0f;
            }

        }

        public void draw(SpriteBatch sb, SpriteEffects effect, Vector2 position)
        {
            sb.Draw(texture, position, sourceRect, Color.White, 0f, origin, 1f, effect, 0);
        }

        public void reset()
        {
            currentFrame = 0;
            finish = false;
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public bool Finish
        {
            get { return finish; }
            set { finish = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public int Width
        {
            get { return sizeWidth; }
            set { sizeWidth = value; }
        }

        public int Height
        {
            get { return sizeHeight; }
            set { sizeHeight = value; }
        }
    }
}
