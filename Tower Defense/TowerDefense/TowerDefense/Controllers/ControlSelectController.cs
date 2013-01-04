using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Input;
using TowerDance.Views;
using TowerDance.Models.Dance;

namespace TowerDance.Controllers
{
    enum PlayerSelectPosition
    {
        NonExisting,
        NonAssigned,
        Dance,
        TowerDefense,
        Both
    };
    enum PlayerSelectState
    {
        NonExisting,
        Selecting,
        Ready
    };
    class ControlSelectController : AController
    {
        ControlInput _controlInput;
        ControlSelectView _controlSelectView;
        MusicSheet _musicSheet;
        List<PlayerSelectPosition> _p = new List<PlayerSelectPosition>() { PlayerSelectPosition.NonAssigned, PlayerSelectPosition.NonExisting };
        List<PlayerSelectState> _pState = new List<PlayerSelectState>() { PlayerSelectState.Selecting, PlayerSelectState.NonExisting };
        bool _shouldStop = false;

        public ControlSelectController(MusicSheet musicSheet)
        {
            _controlInput = new ControlInput();
            _controlSelectView = new ControlSelectView();
            _musicSheet = musicSheet;
            addView(_controlSelectView);
        }

        public override void update(GameTime gameTime)
        {
            if (_shouldStop)
                stop();
            _controlInput.update();
            if (_controlInput.playerOneisPushed(ListKey.PAUSE))
            {
                stop();
                return;
            }
            /* Check the player 1 controls */
            if (_controlInput.playerOneisPushed(ListKey.LEFTARROW))
                tryToMovePlayerPosition(0, 0);
            if (_controlInput.playerOneisPushed(ListKey.DOWNARROW))
                tryToMovePlayerPosition(0, 1);
            if (_controlInput.playerOneisPushed(ListKey.UPARROW))
                tryToMovePlayerPosition(0, 2);
            if (_controlInput.playerOneisPushed(ListKey.RIGHTARROW))
                tryToMovePlayerPosition(0, 3);
            if (_controlInput.playerOneisPushed(ListKey.VALID))
                tryToSetPlayerReady(0);
            _controlSelectView.setPlayersPosition(_p);
            _controlSelectView.setPlayersState(_pState);
            if (everyoneIsReady())
                launchGame();
        }

        public override void updateBackgrounded(GameTime gameTime)
        {
        }

        private bool everyoneIsReady()
        {
            foreach (PlayerSelectState pState in _pState)
            {
                if (pState != PlayerSelectState.NonExisting && pState != PlayerSelectState.Ready)
                    return false;
            }
            return true;
        }

        private void launchGame()
        {
            int dancePlayerID = -1;
            int towerDefensePlayerID = -1;
            int i = 0;
            foreach (PlayerSelectPosition p in _p)
            {
                if (p == PlayerSelectPosition.Dance)
                    dancePlayerID = i;
                if (p == PlayerSelectPosition.TowerDefense)
                    towerDefensePlayerID = i;
                if (p == PlayerSelectPosition.Both)
                {
                    dancePlayerID = i;
                    towerDefensePlayerID = i;
                }
                i++;
            }
            addChild(new GameController(_musicSheet, dancePlayerID, towerDefensePlayerID));
            _shouldStop = true;
        }

        private void tryToSetPlayerReady(int playerID)
        {
            if (_pState[playerID] == PlayerSelectState.Ready)
                _pState[playerID] = PlayerSelectState.Selecting;
            else if (_pState[playerID] == PlayerSelectState.Selecting && _p[playerID] != PlayerSelectPosition.NonAssigned)
                _pState[playerID] = PlayerSelectState.Ready;
            else if (_pState[playerID] == PlayerSelectState.NonExisting)
            {
                _pState[playerID] = PlayerSelectState.Selecting;
                _p[playerID] = PlayerSelectPosition.NonAssigned;
            }
        }

        private void tryToMovePlayerPosition(int playerID, int direction)
        {
            if (_pState[playerID] == PlayerSelectState.Ready)
                return;
            if (playerID == 1)
            {
                _p[playerID] = PlayerSelectPosition.NonAssigned;
                _pState[playerID] = PlayerSelectState.Selecting;
                return;
            }
            /* Player can go in every direction */
            if (_p[playerID] == PlayerSelectPosition.NonAssigned)
            {
                if (direction == 0)
                    tryToSetPlayerPosition(playerID, PlayerSelectPosition.Dance);
                else if (direction == 1)
                    tryToSetPlayerPosition(playerID, PlayerSelectPosition.Both);
                else if (direction == 3)
                    tryToSetPlayerPosition(playerID, PlayerSelectPosition.TowerDefense);
            }
            /* Player can only go in non-assigned state */
            if (_p[playerID] == PlayerSelectPosition.Dance && direction == 3)
                tryToSetPlayerPosition(playerID, PlayerSelectPosition.NonAssigned);
            else if (_p[playerID] == PlayerSelectPosition.Both && direction == 2)
                tryToSetPlayerPosition(playerID, PlayerSelectPosition.NonAssigned);
            else if (_p[playerID] == PlayerSelectPosition.TowerDefense && direction == 0)
                tryToSetPlayerPosition(playerID, PlayerSelectPosition.NonAssigned);
        }

        private void tryToSetPlayerPosition(int playerID, PlayerSelectPosition newPosition)
        {
            if (newPosition == PlayerSelectPosition.NonAssigned)
                _p[playerID] = newPosition;
            /* Only one player per role */
            else
            {
                bool isValid = true;
                foreach (PlayerSelectPosition p in _p)
                {
                    if (p == newPosition)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                    _p[playerID] = newPosition;
            }
        }
    }
}
