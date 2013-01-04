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

        public Castle(Castle newCastle)
            : base(newCastle.lifePoint, false, ENTITYTYPE.CASTLE)
        {
            position = newCastle.position;
        }
        public override void update(GameRessource gameRessource)
        {
            boundingSphere = new CollideSphere(position.X, position.Y, 1);
            if (lifePoint <= 0)
                outWorld = true;
        }

        public override void setAction(EntityUnit entityUnit, bool action) { }
    }
}
