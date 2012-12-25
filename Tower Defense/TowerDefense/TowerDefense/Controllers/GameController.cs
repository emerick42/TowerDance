using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Models.Dance;
using TowerDance.Views.Dance;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDance.Controllers
{
    class GameController : AController
    {
        GameMechanic _danceGameMechanic;
        NotesView _notesView;

        public GameController(MusicSheet musicSheet)
        {
            _danceGameMechanic = new GameMechanic(musicSheet);
            _notesView = new NotesView();
        }

        public override void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _notesView.loadContent(_graphicsDevice, _contentManager);
        }

        override public void update(GameTime gameTime)
        {
            stop();
            return;
//            _notesView.resumeSong();
            _danceGameMechanic.update(gameTime);
            if (_danceGameMechanic.needToPlaySong())
            {
                _notesView.playSong(_danceGameMechanic.getSongFileName());
                _danceGameMechanic.setMusicStarted(true);
            }
            if (_danceGameMechanic.isFinished())
                stop();
        }

        override public void updateBackgrounded(GameTime gameTime)
        {
            _notesView.pauseSong();
        }

        override public void draw(GameTime gameTime)
        {
            _notesView.draw(_danceGameMechanic.getNotes(), _danceGameMechanic.getTimePlayed());
        }

        override public void drawBackgrounded(GameTime gameTime)
        {
            _notesView.draw(_danceGameMechanic.getNotes(), _danceGameMechanic.getTimePlayed());
        }
    }
}
