using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance.Models.TowerDefense
{
    class Castle : Entity
    {
        public Castle()
        {
            initializeId();
            _maxHP = 1000;
            _HP = _maxHP;
            _position = new Vector2(0, 0);
            _size = new Vector2(100, 50);
            _type = "castle";
            _speed = 0.0f;
            _damage = 0;
            _range = 0.0f;
            _team = 0;
        }
    }
}
