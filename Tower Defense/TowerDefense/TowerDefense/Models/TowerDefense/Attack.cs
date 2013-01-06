using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models.TowerDefense
{
    class Attack : Action
    {
        public Attack(Entity e)
        {
            _script = "attack " + e.getId().ToString();
            _castTime = TimeSpan.Zero;
            _castTimeLeft = _castTime;
            _duration = new TimeSpan(0, 0, 0, 0, 500);
            _durationLeft = _duration;
            _coolTime = new TimeSpan(0, 0, 0, 0, 200);
            _coolTimeLeft = _coolTime;
        }
    }
}
