using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input
{
    public class ConfigInputXbox
    {

        
        #region ARROW LEFT STICK
        public Buttons LeftArrow;
        public Buttons DownArrow;
        public Buttons UpArrow;
        public Buttons RightArrow;
        #endregion

        #region ARROW RIGHT STICK
        public Buttons LeftArrowRight;
        public Buttons DownArrowRight;
        public Buttons UpArrowRight;
        public Buttons RightArrowRight;
        #endregion


        #region WAY
        public Buttons LeftWay;
        public Buttons DownWay;
        public Buttons RightWay;
        public Buttons UpWay;
        #endregion

        #region UNITS
        public Buttons FirstUnit;
        public Buttons SecondUnit;
        public Buttons ThirdUnit;
        public Buttons FourthUnit;
        #endregion

        #region PAUSE
        public Buttons Pause;
        #endregion

        public ConfigInputXbox()
        {
            // Attribuer les boutons correspondant par defaut
            LeftArrow = Buttons.LeftThumbstickLeft;
            DownArrow = Buttons.LeftThumbstickDown;
            UpArrow = Buttons.LeftThumbstickUp;
            RightArrow = Buttons.LeftThumbstickRight;

            LeftArrowRight = Buttons.RightThumbstickLeft;
            DownArrowRight = Buttons.RightThumbstickDown;
            UpArrowRight = Buttons.RightThumbstickUp;
            RightArrowRight = Buttons.RightThumbstickRight;

            LeftWay = Buttons.X;
            DownWay = Buttons.A;
            RightWay = Buttons.B;
            UpWay = Buttons.Y;

            FirstUnit = Buttons.LeftTrigger;
            SecondUnit = Buttons.RightTrigger;
            ThirdUnit = Buttons.LeftShoulder;
            FourthUnit = Buttons.RightShoulder;

            Pause = Buttons.Start;
        }

        public ConfigInputXbox(Dictionary<ListKey, Buttons> configKeys)
        {
            // Attribution des boutons par rapport au fichier de conf
            LeftArrow = configKeys[ListKey.LEFTARROW];
            DownArrow = configKeys[ListKey.DOWNARROW];
            UpArrow = configKeys[ListKey.UPARROW];
            RightArrow = configKeys[ListKey.RIGHTARROW];

            LeftArrowRight = configKeys[ListKey.LEFTARROWRIGHT];
            DownArrowRight = configKeys[ListKey.DOWNARROWRIGHT];
            UpArrowRight = configKeys[ListKey.UPARROWRIGHT];
            RightArrowRight = configKeys[ListKey.RIGHTARROWRIGHT];

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

