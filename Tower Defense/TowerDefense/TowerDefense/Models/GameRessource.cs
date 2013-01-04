using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance
{
    public class GameRessource
    {
        int gold;
        int expCurrentGame;
        GameTime gameTime;

        public GameRessource()
        {
            gold = 100;
            expCurrentGame = 0;
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

        public int ExpCurrentGame
        {
            get { return expCurrentGame; }
            set { expCurrentGame = value; }
        }
    }
}
