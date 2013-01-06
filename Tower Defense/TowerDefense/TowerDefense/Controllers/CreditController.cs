using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Views;
using Input;
using TowerDance.Models.Interface;

namespace TowerDance.Controllers
{
    class CreditController : AController
    {
        Menu _menu;
        CreditView _creditView;
        ControlInput _controlInput;

        public CreditController()
        {
            _controlInput = new ControlInput();
            _menu = new Menu(new List<string>() { "Developer, Manager, Artist, Player", "Benoit Mira", "Developer, Manager, Artist, Player", "Emeric Kasbarian"});
            _creditView = new CreditView(_menu);
            addView(_creditView);
        }

        public override void update(GameTime gameTime)
        {
            _controlInput.update();
            if (_controlInput.isPushed(ListKey.VALID)
                || _controlInput.isPushed(ListKey.PAUSE))
            {
                stop();
                return;
            }
        }

        public override void updateBackgrounded(GameTime gameTime)
        {
        }
    }
}
