using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Views;
using Microsoft.Xna.Framework;
using Input;

namespace TowerDance.Controllers
{
    class CampaignErrorController : AController
    {
        CampaignErrorView _campaignErrorView;
        ControlInput _controlInput;

        public CampaignErrorController()
        {
            _campaignErrorView = new CampaignErrorView();
            _controlInput = new ControlInput();
            addView(_campaignErrorView);
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
