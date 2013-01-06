using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.Skills;

namespace TowerDance.Models.Saves
{
    class Save
    {
        private static Save _instance;
        private int _exp;
        private int _level;
        private List<Upgrade> _upgrades;
        private List<Upgrade> _currentSkillList;

        public static Save getInstance()
        {
            if (_instance == null)
                _instance = new Save();
            return _instance;
        }

        private Save()
        {
            _exp = 0;
            _level = 1;
            _upgrades = new List<Upgrade>() { new Upgrade(new InvokeWarrior()) };
            _currentSkillList = new List<Upgrade>() { new Upgrade(new InvokeWarrior()) };
        }

        public void gainExp(int exp)
        {
            int lvl = 1;
            int expRequiredA = 0;
            int expRequiredB = 100;
            int tmpExpRequiredB;
            _exp += exp;
            while (expRequiredB <= _exp)
            {
                lvl++;
                /* Fibonnacci incrementation */
                if (expRequiredA == 0)
                    expRequiredA = expRequiredB;
                tmpExpRequiredB = expRequiredB;
                expRequiredB = expRequiredA + expRequiredB;
                expRequiredA = tmpExpRequiredB;
            }
        }

        public Upgrade getCurrentSkillAt(int currentSkillPosition)
        {
            if (currentSkillPosition < 0 || currentSkillPosition >= _currentSkillList.Count)
                return null;
            return _currentSkillList[currentSkillPosition];
        }

        public void reset()
        {
            _exp = 0;
            _level = 1;
            _upgrades.Clear();
        }
    }
}
