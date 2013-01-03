using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models;
using Input;
using Microsoft.Xna.Framework;
using TowerDance.Views;

namespace TowerDance.Controllers
{
    class PauseMenuController : AController
    {
        Menu _menu;
        ControlInput _controlInput;

        public PauseMenuController()
        {
            _controlInput = new ControlInput();
            _menu = new Menu(new List<string>() {"Return to the game", "Exit the game"});
            addView(new PauseMenuView(_menu));
        }

        override public void update(GameTime gameTime)
        {
            _controlInput.update();
            /* We check inputs */
            if (_controlInput.isPushed(ListKey.PAUSE))
                stop();
            if (_controlInput.isPushed(ListKey.DOWNARROW))
                _menu.selectNext();
            if (_controlInput.isPushed(ListKey.UPARROW))
                _menu.selectPrevious();
            if (_controlInput.isPushed(ListKey.VALID))
                menuSelectExecute();
        }

        override public void updateBackgrounded(GameTime gameTime)
        {

        }

        private void menuSelectExecute()
        {
            if (_menu.getSelectedTitleIndex() == 0)
                stop();
            if (_menu.getSelectedTitleIndex() == 1)
            {
                _parent.signal("exit");
                stop();
            }
        }
    }
}
