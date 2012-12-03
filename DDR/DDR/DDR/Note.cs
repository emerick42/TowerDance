using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDR
{
    class Note
    {
        float _position;
        int _type;

        public Note(float position, int type)
        {
            _position = position;
            _type = type;
        }

        public float getPosition()
        {
            return _position;
        }

        public int getType()
        {
            return _type;
        }
    }
}
