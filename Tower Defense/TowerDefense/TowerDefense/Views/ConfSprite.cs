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

namespace TowerDance
{
    public class ConfSprite
    {
        Texture2D texture;
        ContentManager content;

        int sizeWidth;
        int sizeHeight;
        int nbFrames;

        float interval;

        public ConfSprite(Texture2D newTexture, ContentManager newContent, 
                          int spriteWidthW, int spriteHeightW, int nbFramesW, float intervalW, int nbSpriteWidthW)
        {
            texture = newTexture;
            content = newContent;

            sizeWidth = spriteWidthW;
            sizeHeight = spriteHeightW;
            nbFrames = nbFramesW;

            interval = intervalW;
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        public int SizeWidth
        {
            get { return sizeWidth; }
            set { sizeWidth = value; }
        }

        public int SizeHeight
        {
            get { return sizeHeight; }
            set { sizeWidth = value; }
        }

        public int NbFrames
        {
            get { return nbFrames; }
            set { nbFrames = value; }
        }

        public float Interval
        {
            get { return interval; }
            set { interval = value; }
        }
    }
}
