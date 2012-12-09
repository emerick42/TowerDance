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
    class Castle : EntityUnit
    {
        SpriteObject spriteObject;

        public Castle() : base(30, false)
        {
            position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public Castle(int lifePoint) : base(lifePoint, false)
        {
            position = new Vector2(ControllerGame.sizeWidth / 2, ControllerGame.sizeHeight / 2);
        }

        public override void load(ContentManager content)
        {
            spriteObject = new SpriteObject(content.Load<Texture2D>("castle"), 179, 75, 1, 60f);
        }

        public override void unload()
        {
            spriteObject.unload();
        }

        public override void update(GameTime gameTime)
        {
            boundingSphere.Radius = 1;
            boundingSphere.Center.X = position.X;
            boundingSphere.Center.Y = position.Y;

            spriteObject.Update(gameTime);
        }

        public override void draw(SpriteBatch sb)
        {
            spriteObject.draw(sb, SpriteEffects.None, position);
        }

        public override void setAction(EntityUnit entityUnit, bool action) { }

        public SpriteObject SpriteObject
        {
            get { return spriteObject; }
        }
    }
}
