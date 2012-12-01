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
    class ConfSprite
    {
        Texture2D texture;
        float timer = 0f;
        float interval;
        int currentFrame;
        int nbFrames;
        int spriteWidth;
        int spriteHeight;
        int nbSpriteWidth;
        Rectangle sourceRect;

        Vector2 origin;


        public ConfSprite(Texture2D textureW, float spriteWidthW, float spriteHeightW, int nbFramesW, float intervalW, int nbSpriteWidthW)
        {
            texture = textureW;
            spriteWidth = Convert.ToInt32(spriteWidthW);
            spriteHeight = Convert.ToInt32(spriteHeightW);
            currentFrame = 0;
            nbFrames = nbFramesW;
            interval = intervalW;
            nbSpriteWidth = nbSpriteWidthW;
        }

        public void unload()
        {
            texture.Dispose();
        }

        public void resetVar()
        {
            currentFrame = 0;
            timer = 0;
        }

        public void updateSprite(GameTime gameTime)
        {
            /* Si plusieurs etage de sprites
            
            int currentVerticalFrame = 0;
            int tmpCalc = currentFrame;

            while (tmpCalc >= nbSpriteWidth)
            {
                tmpCalc -= nbSpriteWidth;
                currentVerticalFrame += 1;
            }
             */

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame >= nbFrames)
                    currentFrame = 0;

                timer = 0f;
            }
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

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

    }
}
