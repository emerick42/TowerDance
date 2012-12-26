using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Models.Dance;
using TowerDance.Views.Dance;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TowerDance.Views;

namespace TowerDance.Controllers
{
    class MainMenuController : AController
    {
        SongLibrary _songLibrary;
        MainMenuView _mainMenuView;

        public MainMenuController()
        {
            _songLibrary = new SongLibrary();
            _songLibrary.initialize();
            _mainMenuView = new MainMenuView();
            addView(_mainMenuView);
            children.Add(new GameController(_songLibrary.songs[0].musicSheets[0]));
        }

        override public void update(GameTime gameTime)
        {
            if (children.Count <= 0)
                stop();
        }

        override public void updateBackgrounded(GameTime gameTime)
        {

        }
    }
}
