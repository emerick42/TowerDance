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
            _menu = new Menu(new List<string>() {"Campaign", "Exit"});
            _songLibrary = new SongLibrary();
            _songLibrary.initialize();
            _mainMenuView = new MainMenuView(_menu);
            addView(_mainMenuView);
        }

        override public void update(GameTime gameTime)
        {
            _controlInput.update();
            /* We check inputs */
            if (_controlInput.isPushed(ListKey.DOWNARROW))
                _menu.selectNext();
            if (_controlInput.isPushed(ListKey.UPARROW))
                _menu.selectPrevious();
            if (_controlInput.isPushed(0, ListKey.VALID))
                menuSelectExecute(0);
            if (_controlInput.isPushed(1, ListKey.VALID))
                menuSelectExecute(1);
            _mainMenuView.setElapsedTime(gameTime.ElapsedGameTime);
        }

        override public void updateBackgrounded(GameTime gameTime)
        {

        }

        private void menuSelectExecute(int defaultPlayerID)
        {
            if (_menu.getSelectedTitleIndex() == 0)
                addChild(new ControlSelectController(_songLibrary.songs[0].musicSheets[0], defaultPlayerID));
            if (_menu.getSelectedTitleIndex() == 1)
                stop();
        }
    }
}
