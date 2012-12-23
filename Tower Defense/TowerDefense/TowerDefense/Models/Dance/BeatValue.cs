using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models.Dance
{
    class BeatValue
    {
        int _beat;
        float _value;

        public BeatValue(int beat, float value)
        {
            _beat = beat;
            _value = value;
        }

        public int getBeat()
        {
            return _beat;
        }

        public float getValue()
        {
            return _value;
        }
    }
}
