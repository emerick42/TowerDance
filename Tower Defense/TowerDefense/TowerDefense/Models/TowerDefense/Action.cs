using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models.TowerDefense
{
    enum ActionState
    {
        Waiting,
        Casting,
        Executing,
        Cooling,
        Finished
    }
    abstract class Action
    {
        protected string _script;
        protected TimeSpan _castTime;
        protected TimeSpan _castTimeLeft;
        protected TimeSpan _coolTime;
        protected TimeSpan _coolTimeLeft;
        protected TimeSpan _duration;
        protected TimeSpan _durationLeft;
        protected ActionState _state;
        protected bool _isExecuted = false;

        public string getScript()
        {
            return _script;
        }

        public TimeSpan getCastTime()
        {
            return _castTime;
        }

        public TimeSpan getCastTimeLeft()
        {
            return _castTimeLeft;
        }

        public TimeSpan getDuration()
        {
            return _duration;
        }

        public TimeSpan getDurationLeft()
        {
            return _durationLeft;
        }

        public TimeSpan getCoolTime()
        {
            return _coolTime;
        }

        public TimeSpan getCoolTimeLeft()
        {
            return _coolTimeLeft;
        }

        public TimeSpan makeTimePass(TimeSpan time)
        {
            _state = ActionState.Waiting;
            TimeSpan tmp = _castTimeLeft;
            if (_castTimeLeft > TimeSpan.Zero)
            {
                _state = ActionState.Casting;
                _castTimeLeft -= time;
                if (_castTimeLeft > TimeSpan.Zero)
                    return TimeSpan.Zero;
            }
            time = time - tmp;
            tmp = _durationLeft;
            if (_durationLeft > TimeSpan.Zero)
            {
                _state = ActionState.Executing;
                _durationLeft -= time;
                if (_durationLeft > TimeSpan.Zero)
                    return TimeSpan.Zero;
            }
            time = time - tmp;
            tmp = _coolTimeLeft;
            if (_coolTimeLeft > TimeSpan.Zero)
            {
                _state = ActionState.Cooling;
                _coolTimeLeft -= time;
                if (_coolTimeLeft > TimeSpan.Zero)
                    return TimeSpan.Zero;
            }
            _state = ActionState.Finished;
            return time - tmp;
        }

        public ActionState getState()
        {
            return _state;
        }

        public bool isExecuted()
        {
            return _isExecuted;
        }

        public virtual void setExecuted(bool isExecuted)
        {
            _isExecuted = isExecuted;
        }
    }
}
