using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDance.Models
{
    class ConfViewCastle : AConfView
    {
        SpriteObject spriteObject;
        int _life;

        public ConfViewCastle() : base()
        {
            _life = 100;
        }

        public override void loadAllie(ContentManager content) {}

        public override void loadEnnemy(ContentManager content) {}

        public override void load(ContentManager content)
        {
            spriteObject = new SpriteObject(content.Load<Texture2D>("castle"), 179, 75, 1, 60f);
        }

        public override void unload()
        {
            spriteObject.unload();
        }

        public override void update(GameTime gameTime, ACTION newAction)
        {
            if (_life <= 0)
                outWorld = true;
        }

        public override void draw(SpriteBatch sb, DrawPrimitive dp, Vector2 position, int life, int maxLife)
        {
            spriteObject.draw(sb, SpriteEffects.None, position);
            _life = life;

            dp.drawLine(sb, 5, Color.Brown, new Vector2(position.X - spriteObject.Width / 2, position.Y - spriteObject.Height / 2), new Vector2(position.X + pourcentLife(life, maxLife, spriteObject.Width / 2), position.Y - spriteObject.Height / 2));
        }

        public int pourcentLife(int lifePoint, int maxPoint, int ratio)
        {
            int res;

            res = (int)(lifePoint * (ratio * 2) / maxPoint);
            return res - ratio;
        }

        public SpriteObject SpriteObject
        {
            get { return spriteObject; }
        }

        public bool isAvaible()
        {
            if (outWorld)
                return true;
            return false;
        }
    }
}
