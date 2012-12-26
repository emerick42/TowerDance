using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models.Dance
{
    enum Grade { Fail, Bad, Good, Fantastic };
    class Note
    {
        Grade _grade;
        float _position;
        float _speed;
        int _type;
        float _positionStop;
        int _tempo;
        bool _valid;

        public Note(float position, float speed, int type, int tempo)
        {
            _position = position;
            _speed = speed;
            _type = type;
            _positionStop = 0.0f;
            _tempo = tempo;
            _grade = Grade.Fail;
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

        public bool isValid()
        {
            return _valid;
        }

        public void validate(Grade grade)
        {
            _valid = true;
            _grade = grade;
        }

        public Grade getGrade()
        {
            return _grade;
        }
    }
}
