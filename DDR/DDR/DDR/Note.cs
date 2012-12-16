using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DDR
{
    class Note
    {
        float _position;
        float _speed;
        int _type;
        float _positionStop;
        int _tempo;

        public Note(float position, float speed, int type, int tempo)
        {
            _position = position;
            _speed = speed;
            _type = type;
            _positionStop = 0.0f;
            _tempo = tempo;
        }

        public float getPosition()
        {
            return _position;
        }

        public float getSpeed()
        {
            return _speed;
        }

        public int getType()
        {
            return _type;
        }

        public float getPositionStop()
        {
            return _positionStop;
        }

        public void setPositionStop(float positionStop)
        {
            _positionStop = positionStop;
        }

        public int getTempo()
        {
            return _tempo;
        }
    }
}
