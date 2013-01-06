using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.TowerDefense;
using Input;
using TowerDance.Views;

namespace TowerDance.Controllers
{
    class EndGameController : AController
    {
        ControlInput _controlInput;
        EndGameView _endGameView;
        State _gameResult;
        int _expGained;

        public EndGameController(State gameResult, int expGained)
        {
            _controlInput = new ControlInput();
            _gameResult = gameResult;
            _expGained = expGained;
            _endGameView = new EndGameView(_gameResult, _expGained);
            addView(_endGameView);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _controlInput.update();
            if (_controlInput.isPushed(ListKey.VALID)
                || _controlInput.isPushed(ListKey.PAUSE))
            {
                stop();
                return;
            }
        }

        public override void updateBackgrounded(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
