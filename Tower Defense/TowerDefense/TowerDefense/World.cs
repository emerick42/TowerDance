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
    class World : Entity
    {
        Texture2D textureBack;
        CollisionAction collisionAction;

        public World()
        {
        }

        public void setTexture(Texture2D newTexture)
        {
            textureBack = newTexture;
        }


        public override void load(ContentManager content) { }

        public override void unload()
        {
            textureBack.Dispose();
        }

        public override void update(GameRessource gameRessource)
        { 
        }

        public override void draw(SpriteBatch sb)
        {
            sb.Draw(textureBack, position, Color.White);
        }

        public override bool isAvailable()
        {
            throw new NotImplementedException();
        }

        public CollisionAction CollisionAction
        {
            get { return collisionAction; }
            set { collisionAction = value; }
        }
    }
}
