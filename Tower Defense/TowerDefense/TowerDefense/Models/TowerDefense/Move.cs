using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance.Models.TowerDefense
{
    class Move : Action
    {
        public Move(Vector2 position)
        {
            _script = "move " + position.X.ToString() + " " + position.Y.ToString();
            _castTime = TimeSpan.Zero;
            _castTimeLeft = _castTime;
            _duration = new TimeSpan(0, 0, 0, 0, 200);
            _durationLeft = _duration;
            _coolTime = new TimeSpan(0, 0, 0, 0, 200);
            _coolTimeLeft = _coolTime;
        }

        public override void setExecuted(bool isExecuted)
        {
            _isExecuted = false;
        }
    }
}
