using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input
{
    [Serializable]      // To serialize the class
    public class ConfigInput
    {
        #region ARROW
        public Keys LeftArrow;
        public Keys DownArrow;
        public Keys UpArrow;
        public Keys RightArrow;
        #endregion

        #region WAY
        public Keys LeftWay;
        public Keys DownWay;
        public Keys RightWay;
        public Keys UpWay;
        #endregion

        #region UNITS
        public Keys FirstUnit;
        public Keys SecondUnit;
        public Keys ThirdUnit;
        public Keys FourthUnit;
        #endregion

        #region PAUSE
        public Keys Pause;
        #endregion

        public ConfigInput()
        {
            LeftArrow = Keys.Left;
            DownArrow = Keys.Down;
            UpArrow = Keys.Up;
            RightArrow = Keys.Right;

            LeftWay = Keys.A;
            DownWay = Keys.S;
            RightWay = Keys.D;
            UpWay = Keys.W;

            FirstUnit = Keys.D1;
            SecondUnit = Keys.D2;
            ThirdUnit = Keys.D3;
            FourthUnit = Keys.D4;

            Pause = Keys.Escape;
        }

        public ConfigInput(Dictionary<ListKey, Keys> configKeys)
        {
            LeftArrow = configKeys[ListKey.LEFTARROW];
            DownArrow = configKeys[ListKey.DOWNARROW];
            UpArrow = configKeys[ListKey.UPARROW];
            RightArrow = configKeys[ListKey.RIGHTARROW];

            LeftArrow = configKeys[ListKey.LEFTARROWRIGHT];
            DownArrow = configKeys[ListKey.DOWNARROWRIGHT];
            UpArrow = configKeys[ListKey.UPARROWRIGHT];
            RightArrow = configKeys[ListKey.RIGHTARROWRIGHT];

            LeftWay = configKeys[ListKey.LEFTWAY];
            DownWay = configKeys[ListKey.DOWNWAY];
            RightWay = configKeys[ListKey.RIGHTWAY];
            UpWay = configKeys[ListKey.UPWAY];

            FirstUnit = configKeys[ListKey.FIRSTUNIT];
            SecondUnit = configKeys[ListKey.SECONDUNIT];
            ThirdUnit = configKeys[ListKey.THIRDUNIT];
            FourthUnit = configKeys[ListKey.FOURTHUNIT];

            Pause = configKeys[ListKey.PAUSE];
        }
    }
}
