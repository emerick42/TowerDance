using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance.Models.TowerDefense
{
    class Warrior : Entity
    {
        public Warrior()
        {
            initializeId();
            _maxHP = 200;
            _HP = _maxHP;
            _position = new Vector2(0, 0);
            _size = new Vector2(15, 40);
            _type = "warrior";
            _speed = 5.0f;
            _damage = 20;
            _range = 15.0f;
            _team = 0;
            _direction = 0;
        }
    }
}
