using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Models.Dance;

namespace TowerDance.Controllers
{
    class MainMenuController : AController
    {
        SongLibrary _songLibrary;

        public MainMenuController()
        {
            _songLibrary = new SongLibrary();
            _songLibrary.initialize();
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
