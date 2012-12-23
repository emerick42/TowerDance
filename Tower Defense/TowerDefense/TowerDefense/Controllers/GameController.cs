using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Models.Dance;

namespace TowerDance.Controllers
{
    class GameController : AController
    {
        MusicSheet _musicSheet;

        public GameController(MusicSheet musicSheet)
        {
            _musicSheet = musicSheet;
        }

        override public void update(GameTime gameTime)
        {

        }

        override public void updateBackgrounded(GameTime gameTime)
        {

        }

        override public void draw(GameTime gameTime)
        {

        }

        override public void drawBackgrounded(GameTime gameTime)
        {

        }
    }
}
