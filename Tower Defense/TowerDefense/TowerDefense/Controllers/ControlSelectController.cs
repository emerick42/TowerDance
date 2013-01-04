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
        List<PlayerSelectPosition> _p = new List<PlayerSelectPosition>() { PlayerSelectPosition.NonExisting, PlayerSelectPosition.NonExisting };
        List<PlayerSelectState> _pState = new List<PlayerSelectState>() { PlayerSelectState.NonExisting, PlayerSelectState.NonExisting };
        bool _shouldStop = false;

        public ControlSelectController(MusicSheet musicSheet, int defaultPlayerID = 0)
        {
            _p[defaultPlayerID] = PlayerSelectPosition.NonAssigned;
            _pState[defaultPlayerID] = PlayerSelectState.Selecting;
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
            /* Check the player 1 controls */
            if (_controlInput.isPushed(0, ListKey.LEFTARROW))
                tryToMovePlayerPosition(0, 0);
            if (_controlInput.isPushed(0, ListKey.DOWNARROW))
                tryToMovePlayerPosition(0, 1);
            if (_controlInput.isPushed(0, ListKey.UPARROW))
                tryToMovePlayerPosition(0, 2);
            if (_controlInput.isPushed(0, ListKey.RIGHTARROW))
                tryToMovePlayerPosition(0, 3);
            if (_controlInput.isPushed(0, ListKey.VALID))
                tryToSetPlayerReady(0);
            if (_controlInput.isPushed(0, ListKey.PAUSE))
                tryToSetPlayerNonExisting(0);
            /* Check the player 2 controls */
            if (_controlInput.isPushed(1, ListKey.LEFTARROW))
                tryToMovePlayerPosition(1, 0);
            if (_controlInput.isPushed(1, ListKey.DOWNARROW))
                tryToMovePlayerPosition(1, 1);
            if (_controlInput.isPushed(1, ListKey.UPARROW))
                tryToMovePlayerPosition(1, 2);
            if (_controlInput.isPushed(1, ListKey.RIGHTARROW))
                tryToMovePlayerPosition(1, 3);
            if (_controlInput.isPushed(1, ListKey.VALID))
                tryToSetPlayerReady(1);
            if (_controlInput.isPushed(1, ListKey.PAUSE))
                tryToSetPlayerNonExisting(1);
            _controlSelectView.setPlayersPosition(_p);
            _controlSelectView.setPlayersState(_pState);
            if (everyoneIsReady())
                launchGame();
            if (everyoneIsNonExisting())
                stop();
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

        private bool everyoneIsNonExisting()
        {
            foreach (PlayerSelectState pState in _pState)
            {
                if (pState != PlayerSelectState.NonExisting)
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
            stop();
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

        private void tryToSetPlayerNonExisting(int playerID)
        {
            _pState[playerID] = PlayerSelectState.NonExisting;
            _p[playerID] = PlayerSelectPosition.NonExisting;
        }

        private void tryToMovePlayerPosition(int playerID, int direction)
        {
            if (_pState[playerID] == PlayerSelectState.Ready)
                return;
            if (_p[playerID] == PlayerSelectPosition.NonExisting)
            {
                if (!oneTakeBoth())
                {
                    _p[playerID] = PlayerSelectPosition.NonAssigned;
                    _pState[playerID] = PlayerSelectState.Selecting;
                }
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
                    if (p == newPosition || p == PlayerSelectPosition.Both)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                    _p[playerID] = newPosition;
                if (newPosition == PlayerSelectPosition.Both)
                {
                    removeOtherPlayer(playerID);
                }
            }
        }

        private void removeOtherPlayer(int playerID)
        {
            int i = 0;
            while (i < _p.Count)
            {
                if (i != playerID)
                {
                    _p[i] = PlayerSelectPosition.NonExisting;
                    _pState[i] = PlayerSelectState.NonExisting;
                }
                i++;
            }
        }

        private bool oneTakeBoth()
        {
            foreach (PlayerSelectPosition p in _p)
            {
                if (p == PlayerSelectPosition.Both)
                    return true;
            }
            return false;
        }
    }
}
