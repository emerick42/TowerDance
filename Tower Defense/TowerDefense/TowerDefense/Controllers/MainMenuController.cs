﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Models.Dance;
using TowerDance.Views.Dance;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TowerDance.Views;
using TowerDance.Models;
using Microsoft.Xna.Framework.Input;
using Input;

namespace TowerDance.Controllers
{
    class MainMenuController : AController
    {
        SongLibrary _songLibrary;
        MainMenuView _mainMenuView;
        Menu _menu;
        ControlInput _controlInput;

        public MainMenuController()
        {
            _controlInput = new ControlInput();
            _menu = new Menu(new List<string>() {"Campaign", "Free play", "Exit"});
            _songLibrary = new SongLibrary();
            _songLibrary.initialize();
            _mainMenuView = new MainMenuView(_menu);
            addView(_mainMenuView);
//            children.Add(new GameController(_songLibrary.songs[0].musicSheets[0]));
        }

        override public void update(GameTime gameTime)
        {
            _controlInput.update();
            /* We check inputs */
            if (_controlInput.isPressed(ListKey.DOWNARROW))
                _menu.selectNext();
            if (_controlInput.isPressed(ListKey.UPARROW))
                _menu.selectPrevious();
        }

        override public void updateBackgrounded(GameTime gameTime)
        {

        }
    }
}
