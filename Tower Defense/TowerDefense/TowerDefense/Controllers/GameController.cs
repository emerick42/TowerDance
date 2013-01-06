using Microsoft.Xna.Framework;
using Input;
using TowerDance.Models.Dance;
using TowerDance.Views.Dance;
using TowerDance.Views.TowerDefense;
using TowerDance.Models.Saves;

namespace TowerDance.Controllers
{
    class GameController : AController
    {
        Save _save;
        TowerDance.Models.Dance.GameMechanic _danceGameMechanic;
        TowerDance.Models.TowerDefense.GameMechanic _towerDefenseGameMechanic;
        NotesView _notesView;
        BoardView _boardView;
        ControlInput _controlInput;
        /* Useless
        World _world;
        WarriorView _warriorView;
        ArcherView _archerView;
        CastleView _castleView;
        BackgroundView _backView;*/
        int _dancePlayerID;
        int _towerDefensePlayerID;

        public GameController(MusicSheet musicSheet, int dancePlayerID, int towerDefensePlayerID)
        {
            _save = Save.getInstance();
            _dancePlayerID = dancePlayerID;
            _towerDefensePlayerID = towerDefensePlayerID;
            _controlInput = new ControlInput();
            _danceGameMechanic = new TowerDance.Models.Dance.GameMechanic(musicSheet);
            _towerDefenseGameMechanic = new TowerDance.Models.TowerDefense.GameMechanic(musicSheet);
            _notesView = new NotesView(_danceGameMechanic.getNotes(), _danceGameMechanic.getTimePlayed());
            _boardView = new BoardView(_towerDefenseGameMechanic.getMap(), _towerDefenseGameMechanic.getEntities());

            /* Register views */
            addView(_boardView);
            addBackgroundView(_boardView);
            addView(_notesView);
            addBackgroundView(_notesView);

            /* Useless
            _world = new World();
            _archerView = new ArcherView();
            _warriorView = new WarriorView();
            _castleView = new CastleView();
            _backView = new BackgroundView();
            //addView(_backView);
            //addBackgroundView(_backView);
            addView(_castleView);
            addBackgroundView(_castleView);
            addView(_warriorView);
            addBackgroundView(_warriorView);
            addView(_archerView);
            addBackgroundView(_archerView);*/
        }

        override public void update(GameTime gameTime)
        {
            _controlInput.update();
            /* We check global inputs */
            if (_controlInput.isPushed(ListKey.PAUSE))
                menuPause();
            updateDance(gameTime);
            updateTowerDefense(gameTime);
            if ((_danceGameMechanic.isFinished() && _towerDefenseGameMechanic.getCurrentState() != Models.TowerDefense.State.InProgress)
                || _towerDefenseGameMechanic.getCurrentState() != Models.TowerDefense.State.InProgress)
            {
                _notesView.stopSong();
                stop();
            }
            /* Useless
            _world.Update(gameTime);

            // A Rajouter apres le travail sur le view
//            if (!_backView.SetBack)
//            {
//                _backView.setBackround();
//                _backView.loadSprite();
//            }

//            if (_castleView.isNewCastle())
//            {
////                _castleView.setCastle(_world.getCastle());
//                _castleView.loadSprite();
//            }
            _castleView.setRefreshList(_world.getCastle());

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

            //_backView.update(gameTime);
            _castleView.update(gameTime);
            _warriorView.update(gameTime);
            _archerView.update(gameTime);*/
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

        private void updateDance(GameTime gameTime)
        {
            _danceGameMechanic.update(gameTime);
            _notesView.resumeSong();
            if (_dancePlayerID >= 0)
            {
                if (_controlInput.isPushed(_dancePlayerID, ListKey.LEFTARROW)
                    || _controlInput.isPushed(_dancePlayerID, ListKey.LEFTARROWRIGHT))
                    _danceGameMechanic.tryToValid(0);
                if (_controlInput.isPushed(_dancePlayerID, ListKey.DOWNARROW)
                    || _controlInput.isPushed(_dancePlayerID, ListKey.DOWNARROWRIGHT))
                    _danceGameMechanic.tryToValid(1);
                if (_controlInput.isPushed(_dancePlayerID, ListKey.UPARROW)
                    || _controlInput.isPushed(_dancePlayerID, ListKey.UPARROWRIGHT))
                    _danceGameMechanic.tryToValid(2);
                if (_controlInput.isPushed(_dancePlayerID, ListKey.RIGHTARROW)
                    || _controlInput.isPushed(_dancePlayerID, ListKey.RIGHTARROWRIGHT))
                    _danceGameMechanic.tryToValid(3);
            }
            else
                _danceGameMechanic.autoValid();
            /* Manage interaction with NotesView */
            if (_danceGameMechanic.needToPlaySong())
                _notesView.playSong(_danceGameMechanic.getSongFileName());
            if (!_danceGameMechanic.hasMusicStarted() && _notesView.hasSongStarted())
                _danceGameMechanic.syncWithSong(_notesView.getSongDiffPosition());
            if (_danceGameMechanic.hasNewFlashMessage())
                _notesView.setFlashMessage(_danceGameMechanic.getFlashMessage());
            _notesView.setTimePlayed(_danceGameMechanic.getTimePlayed());
            _notesView.setCombo(_danceGameMechanic.getCombo());
        }

        private void updateTowerDefense(GameTime gameTime)
        {
            _towerDefenseGameMechanic.update(gameTime, _danceGameMechanic.getTimePlayed());
            if (_towerDefensePlayerID >= 0)
            {
                if (_controlInput.isPushed(_towerDefensePlayerID, ListKey.FIRSTUNIT))
                    _towerDefenseGameMechanic.execute(_save.getCurrentSkillAt(0));
            }
            else
                _towerDefenseGameMechanic.autoPlay();
            /* Manage interaction with TowerDefenseView */
            _boardView.refresh(_towerDefenseGameMechanic.getMap(), _towerDefenseGameMechanic.getEntities());
        }

        private void menuPause()
        {
            _danceGameMechanic.setMusicStarted(false);
            _notesView.pauseSong();
            addChild(new PauseMenuController());
        }
    }
}
