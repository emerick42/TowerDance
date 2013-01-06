using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models.Skills
{
    enum Type
    {
        Passive,
        Active
    }
    class Skill
    {
        protected Type _type;
        protected string _script;
        protected int _maxLevel;

        public Skill()
        {
            _type = Type.Passive;
            _script = "";
            _maxLevel = -1;
        }

        public string getScript(int _level)
        {
            if (_level > _maxLevel)
                _level = _maxLevel;
            string result = _script;
            result.Replace("%level%", _level.ToString());
            return result;
        }

        public int getMaxLevel()
        {
            return _maxLevel;
        }

        public Type getType()
        {
            return _type;
        }
    }
}
