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
using Microsoft.Xna.Framework.Input;
using Input;
using TowerDance.Models.Interface;

namespace TowerDance.Controllers
{
    class MainMenuController : AController
    {
        MainMenuView _mainMenuView;
        Menu _menu;
        ControlInput _controlInput;

        public MainMenuController()
        {
            _controlInput = new ControlInput();
            _menu = new Menu(new List<string>() {"Campaign", "Credits", "Exit"});
            _mainMenuView = new MainMenuView(_menu);
            addView(_mainMenuView);
        }

        override public void update(GameTime gameTime)
        {
            _controlInput.update();
            /* We check inputs */
            if (_controlInput.isPushed(ListKey.PAUSE))
            {
                stop();
                return;
            }
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
                addChild(new CampaignController(defaultPlayerID));
            if (_menu.getSelectedTitleIndex() == 1)
                addChild(new CreditController());
            if (_menu.getSelectedTitleIndex() == 2)
                stop();
        }
    }
}
