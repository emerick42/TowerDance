using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.Skills;

namespace TowerDance.Models.Saves
{
    class Upgrade
    {
        private Skill _skill;
        int _level;

        public Upgrade(Skill skill)
        {
            _skill = skill;
            _level = 0;
        }

        public Skill getSkill()
        {
            return _skill;
        }

        public string getScript()
        {
            return _skill.getScript(_level);
        }
    }
}
