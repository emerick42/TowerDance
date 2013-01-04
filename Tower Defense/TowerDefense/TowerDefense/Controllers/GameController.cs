using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Models.Dance;
using TowerDance.Views.Dance;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TowerDance.Models;
using Microsoft.Xna.Framework.Input;
using Input;

namespace TowerDance.Controllers
{
    class GameController : AController
    {
        GameMechanic _danceGameMechanic;
        NotesView _notesView;
        ControlInput _controlInput;
        World _world;
        WarriorView _warriorView;
        ArcherView _archerView;

        public GameController(MusicSheet musicSheet)
        {
            _controlInput = new ControlInput();
            _danceGameMechanic = new GameMechanic(musicSheet);
            _notesView = new NotesView(_danceGameMechanic.getNotes(), _danceGameMechanic.getTimePlayed());
            _world = new World();
            _archerView = new ArcherView();
            _warriorView = new WarriorView();

            addView(_notesView);
            addBackgroundView(_notesView);

            addView(_warriorView);
            addBackgroundView(_warriorView);

            addView(_archerView);
            addBackgroundView(_archerView);
        }

        override public void update(GameTime gameTime)
        {
            _danceGameMechanic.update(gameTime);
            _controlInput.update();
            _notesView.resumeSong();
            /* We check inputs */
            if (_controlInput.playerOneisPushed(ListKey.PAUSE))
            {
                menuPause();
                return;
            }
            if (_controlInput.playerOneisPushed(ListKey.LEFTARROW))
                _danceGameMechanic.tryToValid(0);
            if (_controlInput.playerOneisPushed(ListKey.DOWNARROW))
                _danceGameMechanic.tryToValid(1);
            if (_controlInput.playerOneisPushed(ListKey.UPARROW))
                _danceGameMechanic.tryToValid(2);
            if (_controlInput.playerOneisPushed(ListKey.RIGHTARROW))
                _danceGameMechanic.tryToValid(3);
            /* Manage interaction with NotesView */
            if (_danceGameMechanic.needToPlaySong())
                _notesView.playSong(_danceGameMechanic.getSongFileName());
            if (!_danceGameMechanic.hasMusicStarted() && _notesView.hasSongStarted())
                _danceGameMechanic.syncWithSong(_notesView.getSongPosition());
            if (_danceGameMechanic.isFinished())
            {
                _notesView.stopSong();
                stop();
            }
            if (_danceGameMechanic.hasNewFlashMessage())
                _notesView.setFlashMessage(_danceGameMechanic.getFlashMessage());
            _notesView.setTimePlayed(_danceGameMechanic.getTimePlayed());
            _notesView.setCombo(_danceGameMechanic.getCombo());

            _world.Update(gameTime);

            if (_world.isNewWarrior())
            {
                _warriorView.setWarrior(_world.getWarrior());
                _warriorView.loadSprite();
            }
            if (_world.isNewArcher())
            {
                _archerView.setArcher(_world.getArcher());
                _archerView.loadSprite();
            }

            _warriorView.setRefreshList(_world.getAllWarrior());
            _archerView.setRefreshList(_world.getAllArcher());

            _warriorView.update(gameTime);
            _archerView.update(gameTime);
        }

        override public void updateBackgrounded(GameTime gameTime)
        {
        }

        public override void signal(string signal)
        {
            if (signal.Equals("exit"))
                stop();
        }

        public override void stop()
        {
            _notesView.stopSong();
            base.stop();
        }

        private void menuPause()
        {
            _danceGameMechanic.setMusicStarted(false);
            _notesView.pauseSong();
            addChild(new PauseMenuController());
        }
    }
}
