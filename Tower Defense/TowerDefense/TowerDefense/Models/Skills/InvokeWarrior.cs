using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models.Skills
{
    class InvokeWarrior : Skill
    {
        public InvokeWarrior()
        {
            _type = Type.Active;
            _script = "create warrior";
            _maxLevel = -1;
        }
    }
}
