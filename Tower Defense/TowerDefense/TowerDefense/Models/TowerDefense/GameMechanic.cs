﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.Dance;
using Microsoft.Xna.Framework;
using TowerDance.Models.Saves;
using System.Text.RegularExpressions;

namespace TowerDance.Models.TowerDefense
{
    enum State
    {
        InProgress,
        Won,
        Lost
    };
    class GameMechanic
    {
        MusicSheet _musicSheet;
        TimeSpan _songTimePlayed;
        Castle _castle;
        Rectangle _map;
        State _currentState;
        List<Entity> _allies;
        List<Entity> _enemies;
        List<Entity> _entities;
        int _currentWave;
        int _maxWave;
        int _currentMana;
        TimeSpan _currentFrameTime;

        public GameMechanic(MusicSheet musicSheet)
        {
            _allies = new List<Entity>();
            _enemies = new List<Entity>();
            _entities = new List<Entity>();
            _songTimePlayed = new TimeSpan();
            _currentMana = 0;
            _currentWave = 0;
            _maxWave = 3;
            _musicSheet = musicSheet;
            _castle = new Castle();
            _allies.Add(_castle);
            _entities.Add(_castle);
            _map = new Rectangle(-200, -150, 400, 300);
        }

        public void update(GameTime gameTime, TimeSpan songTimePlayed)
        {
            _currentFrameTime = gameTime.ElapsedGameTime;
            _songTimePlayed = songTimePlayed;
            if (_currentState == State.InProgress)
            {
                /* We spawn a wave */
                if (shouldSpawnWave())
                    spawnWave();
                updateState();
                letThemDoSomething();
                foreach (Entity e in _entities)
                {
                    List<string> toExecute = e.makeTimePass(gameTime.ElapsedGameTime);
                    foreach (string action in toExecute)
                        execute(e, action);
                }
            }
        }

        public void execute(Upgrade upgrade)
        {
            if (upgrade != null)
            {
                if (upgrade.getScript().Equals("create warrior"))
                {
                    _allies.Add(new Warrior());
                    _entities.Add(new Warrior());
                }
            }
        }

        public void execute(Entity e, string action)
        {
            /* Check movement */
            Regex moveRegex = new Regex(@"move (?<x>[0-9]*\.?[0-9]*) (?<y>[0-9]*\.?[0-9]*)");
            Match match = moveRegex.Match(action);
            if (match.Success)
            {
                float x = (float)Convert.ToDouble(match.Groups["x"].ToString());
                float y = (float)Convert.ToDouble(match.Groups["y"].ToString());
                Vector2 destination = new Vector2(x, y);
                Vector2 direction = new Vector2();
                direction.X = destination.X - e.getPosition().X;
                direction.Y = destination.Y - e.getPosition().Y;
                float length = (float)Math.Sqrt((double)direction.Length());
                float mult = (e.getSpeed() * (float)_currentFrameTime.TotalSeconds) / length;
                direction *= mult;
                if (mult < 1)
                    e.move(direction);
                else
                    e.setPosition(destination);
                return;
            }
            /* Check attack */
            moveRegex = new Regex(@"attack (?<id>\d+)");
            match = moveRegex.Match(action);
            if (match.Success)
            {
                int id = Convert.ToInt32(match.Groups["id"].ToString());
                Entity opponent = getEntityById(id);
                if (e.canReach(opponent))
                    opponent.attack(e.getDamage());
            }
        }

        public void execute(string action)
        {
        }

        public void autoPlay()
        {

        }

        public State getCurrentState()
        {
            return _currentState;
        }

        public Rectangle getMap()
        {
            return _map;
        }

        public List<Entity> getEntities()
        {
            return _entities;
        }

        private bool shouldSpawnWave()
        {
            float musicDuration = (float)_musicSheet.getDuration().TotalSeconds;
            if (_currentWave < _maxWave
                && (float)_songTimePlayed.TotalSeconds >= (float)_currentWave * musicDuration / (float)_maxWave)
                return true;
            return false;
        }

        private void spawnWave()
        {
            int i = 0;
            while (i < 3)
            {
                Entity e = new Warrior();
                e.setTeam(1);
                if (_currentWave % 2 == 0)
                {
                    e.setPosition(new Vector2(_map.X, (i - 1) * e.getSize().Y / 2));
                    e.setDirection(3);
                }
                else
                {
                    e.setPosition(new Vector2(_map.X + _map.Width, (i - 1) * e.getSize().Y / 2));
                    e.setDirection(0);
                }
                _enemies.Add(e);
                _entities.Add(e);
                i++;
            }
            _currentWave++;
        }

        private void updateState()
        {
            if (_currentWave >= _maxWave && _enemies.Count <= 0)
                _currentState = State.Won;
            if (_castle.getHP() <= 0)
                _currentState = State.Lost;
        }

        private void letThemDoSomething()
        {
            foreach (Entity e in _enemies)
            {
                if (e.getNbActions() > 5)
                    continue;
                if (e.canReach(_castle))
                    e.addAction(new Attack(_castle));
                else if (e.getNbActions() < 1)
                    e.addAction(new Move(new Vector2(0, 0)));
            }
        }

        private Entity getEntityById(int id)
        {
            foreach (Entity e in _entities)
            {
                if (e.getId() == id)
                    return e;
            }
            return null;
        }
    }
}
