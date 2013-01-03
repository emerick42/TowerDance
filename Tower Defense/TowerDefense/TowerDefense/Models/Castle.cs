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
    public class Castle : EntityUnit
    {
        SpriteObject spriteObject;

        public Castle()
            : base(25, false, ENTITYTYPE.CASTLE)
        {
            position = new Vector2(World.sizeWidth / 2, World.sizeHeight / 2);
        }

        public Castle(int lifePoint)
            : base(lifePoint, false, ENTITYTYPE.CASTLE)
        {
            position = new Vector2(World.sizeWidth / 2, World.sizeHeight / 2);
        }

        public override void load(ContentManager content)
        {
            spriteObject = new SpriteObject(content.Load<Texture2D>("castle"), 179, 75, 1, 60f);
        }

        public override void unload()
        {
            spriteObject.unload();
        }

        public override void update(GameRessource gameRessource)
        {
            boundingSphere = new CollideSphere(position.X, position.Y, 1);
            if (lifePoint <= 0)
                outWorld = true;

            //spriteObject.Update(gameRessource);
        }

        public override void draw(SpriteBatch sb)
        {
            spriteObject.draw(sb, SpriteEffects.None, position);
        }

        public override void drawLife(SpriteBatch sb, DrawPrimitive dp)
        {
            dp.drawLine(sb, 5, Color.Brown, new Vector2(position.X - spriteObject.Width / 2, position.Y - spriteObject.Height / 2), new Vector2(position.X + pourcentLife(spriteObject.Width / 2), position.Y - spriteObject.Height / 2));
        }

        public override void setAction(EntityUnit entityUnit, bool action) { }

        public SpriteObject SpriteObject
        {
            get { return spriteObject; }
        }
    }
}
