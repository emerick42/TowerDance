using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    class GameRessource
    {
        int gold;
        GameTime gameTime;

        public GameRessource()
        {
            gold = 100;
        }

        public void update(GameTime g)
        {
            gameTime = g;
        }

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public GameTime GameTime
        {
            get { return gameTime; }
        }
    }
}
