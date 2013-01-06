using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance.Models.TowerDefense
{
    abstract class Entity
    {
        private static int _lastId = 0;
        protected int _id;
        protected int _HP;
        protected int _maxHP;
        protected Vector2 _position;
        protected Vector2 _size;
        protected string _type;
        protected float _speed;
        protected int _damage;
        protected float _range;
        protected int _team;
        protected int _direction;
        protected List<Action> _actions = new List<Action>();

        public void initializeId()
        {
            _id = _lastId++;
        }

        public int getId()
        {
            return _id;
        }

        public int getHP()
        {
            return _HP;
        }

        public void attack(int damage)
        {
            if (_HP - damage <= 0)
                _HP = 0;
            else if (_HP - damage >= _maxHP)
                _HP = _maxHP;
            else
                _HP -= damage;
        }

        public int getMaxHP()
        {
            return _maxHP;
        }

        public Vector2 getPosition()
        {
            return _position;
        }

        public void setPosition(Vector2 position)
        {
            _position = position;
        }

        public void move(Vector2 movement)
        {
            _position += movement;
        }

        public Vector2 getSize()
        {
            return _size;
        }

        public void setSize(Vector2 size)
        {
            _size = size;
        }

        public string getType()
        {
            return _type;
        }

        public float getSpeed()
        {
            return _speed;
        }

        public int getDamage()
        {
            return _damage;
        }

        public float getRange()
        {
            return _range;
        }

        public int getTeam()
        {
            return _team;
        }

        public void setTeam(int team)
        {
            _team = team;
        }

        public int getDirection()
        {
            return _direction;
        }

        public void setDirection(int direction)
        {
            _direction = direction;
        }

        public List<string> makeTimePass(TimeSpan time)
        {
            List<string> result = new List<string>();
            while (_actions.Count > 0 && time > TimeSpan.Zero)
            {
                time = _actions[0].makeTimePass(time);
                if (!_actions[0].isExecuted() && _actions[0].getState() >= ActionState.Executing)
                {
                    result.Add(_actions[0].getScript());
                    _actions[0].setExecuted(true);
                }
                if (_actions[0].getState() == ActionState.Finished)
                    _actions.RemoveAt(0);
            }
            return result;
        }

        public List<Action> getActions()
        {
            return _actions;
        }

        public int getNbActions()
        {
            return _actions.Count;
        }

        public void addAction(Action action)
        {
            _actions.Add(action);
        }

        public bool canReach(Entity e)
        {
            if (Vector2.Distance(e.getPosition(), getPosition()) <= getRange())
                return true;
            return false;
        }
    }
}
