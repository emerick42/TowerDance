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
    class Background : EntityUnit
    {
        CollisionAction collisionAction;

        public Background() : base(0, false, ENTITYTYPE.BACKGROUND)
        {
            collisionAction = new CollisionAction();
        }

        public override void load(ContentManager content) { }

        public override void unload()
        {
        }

        public override void update(GameRessource gameRessource)
        { 
        }

        public override void draw(SpriteBatch sb)
        {
        }

        public override ENTITYTYPE getType()
        {
            throw new NotImplementedException();
        }

        public override bool isAvailable()
        {
            return true;
        }

        public CollisionAction CollisionAction
        {
            get { return collisionAction; }
            set { collisionAction = value; }
        }
    }
}
