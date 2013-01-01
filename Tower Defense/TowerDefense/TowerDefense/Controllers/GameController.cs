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

namespace TowerDance.Controllers
{
    class GameController : AController
    {
        GameMechanic _danceGameMechanic;
        NotesView _notesView;

        public GameController(MusicSheet musicSheet)
        {
            _danceGameMechanic = new GameMechanic(musicSheet);
            _notesView = new NotesView(_danceGameMechanic.getNotes(), _danceGameMechanic.getTimePlayed());
            addView(_notesView);
            addBackgroundView(_notesView);
        }

        override public void update(GameTime gameTime)
        {
            _notesView.resumeSong();
            /* We check inputs */
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Left))
                _danceGameMechanic.tryToValid(0);
            if (keyState.IsKeyDown(Keys.Down))
                _danceGameMechanic.tryToValid(1);
            if (keyState.IsKeyDown(Keys.Up))
                _danceGameMechanic.tryToValid(2);
            if (keyState.IsKeyDown(Keys.Right))
                _danceGameMechanic.tryToValid(3);
            _danceGameMechanic.update(gameTime);
            if (_danceGameMechanic.needToPlaySong())
            {
                _notesView.playSong(_danceGameMechanic.getSongFileName());
            }
            if (!_danceGameMechanic.hasMusicStarted() && _notesView.hasSongStarted())
            {
                _danceGameMechanic.syncWithSong(_notesView.getSongPosition());
            }
            if (_danceGameMechanic.isFinished())
            {
                _notesView.stopSong();
                stop();
            }
            if (_danceGameMechanic.hasNewFlashMessage())
                _notesView.setFlashMessage(_danceGameMechanic.getFlashMessage());
            _notesView.setTimePlayed(_danceGameMechanic.getTimePlayed());
            _notesView.setCombo(_danceGameMechanic.getCombo());
        }

        override public void updateBackgrounded(GameTime gameTime)
        {
            _notesView.pauseSong();
        }
    }
}
