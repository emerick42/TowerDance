using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDance
{
    class ConfViewBackground : AConfView
    {
        SpriteObject spriteObject;

        public ConfViewBackground() : base()
        {
        }

        public override void loadAllie(ContentManager content) { } 

        public override void loadEnnemy(ContentManager content) { }

        public override void load(ContentManager content)
        {
            spriteObject = new SpriteObject(content.Load<Texture2D>("background"), 1280, 720, 0, 0f);
        }

        public override void unload()
        {
            spriteObject.unload();
        }

        public override void update(GameTime gameTime, ACTION newAction)
        {        }

        public override void draw(SpriteBatch sb, DrawPrimitive dp, Vector2 position, int life, int maxLife)
        {
            spriteObject.draw(sb, SpriteEffects.None, position);
        }

        public bool isAvaible()
        {
            if (outWorld)
                return true;
            return false;
        }
    }
}
