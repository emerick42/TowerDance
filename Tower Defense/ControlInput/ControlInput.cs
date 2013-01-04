using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Data;

namespace Input
{
    // Union a changer pour choisir les mouvements
    // Enum for the dicitonnary's key
    public enum ListKey
    {
        NONE,

        #region ARROW
        LEFTARROW,
        DOWNARROW,
        UPARROW,
        RIGHTARROW,
        #endregion

        #region ARROW RIGHT STICK
        LEFTARROWRIGHT,
        DOWNARROWRIGHT,
        UPARROWRIGHT,
        RIGHTARROWRIGHT,
        #endregion

        #region WAY
        LEFTWAY,
        DOWNWAY,
        RIGHTWAY,
        UPWAY,
        #endregion

        #region UNITS
        FIRSTUNIT,
        SECONDUNIT,
        THIRDUNIT,
        FOURTHUNIT,
        #endregion

        #region VALID
        VALID,
        #endregion

        #region PAUSE
        PAUSE
        #endregion
    }

    

    public class ControlInput
    {
        //  START VARIABLE
        public KeyConfig keyConfig;
        public ButtonsConfig buttonsConfig;

        private KeyboardState keysState;
        public KeyboardState KeysState
        {
            get { return keysState; }
        }

        public KeyboardState previousKeysState;
        public KeyboardState PreviousKeysState
        {
            get { return previousKeysState; }
        }

        private GamePadState gamepadStatePlayerOne;
        public GamePadState GamepadStatePlayerOne
        {
            get { return gamepadStatePlayerOne; }
        }

        private GamePadState previousGamepadStatePlayerOne;
        public GamePadState PreviousGamepadStatePlayerOne
        {
            get { return previousGamepadStatePlayerOne; }
        }

        private GamePadState gamepadStatePlayerTwo;
        public GamePadState GamepadStatePlayerTwo
        {
            get { return gamepadStatePlayerTwo; }
        }

        private GamePadState previousGamepadStatePlayerTwo;
        public GamePadState PreviousGamepadStatePlayerTwo
        {
            get { return previousGamepadStatePlayerTwo; }
        }

        bool checkUpdate = false;
        // END VARIABLES


        public ControlInput()
        {
            keyConfig = new KeyConfig();
            buttonsConfig = new ButtonsConfig();

            keyConfig.LoadConfig();
            buttonsConfig.LoadConfig();
        }

        // Sauvegarde le fichier de conf
        public void SaveInput()
        {
            keyConfig.SaveConfig();
            buttonsConfig.SaveConfig();
        }

        public bool isFirstPlayer()
        {
#if XBOX
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                return true;
            return false;
#else
            return true;
#endif
        }

        public bool isSecondPlayer()
        {
#if XBOX
            if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                return true;
#else
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                return true;
#endif
            return false;
        }


        public bool playerOneisPressed(ListKey key_)
        {
#if XBOX
            if (gamepadStatePlayerOne.IsButtonDown(buttonsConfig.getValue(key_)))
#else
            if (keysState.IsKeyDown(keyConfig.getValue(key_)) )
#endif
                return true;
            return false;
        }

        public bool playerOneisUp(ListKey key_)
        {
#if XBOX
            if (gamepadStatePlayerOne.IsButtonUp(buttonsConfig.getValue(key_)))
#else
            if (keysState.IsKeyUp(keyConfig.getValue(key_)))
#endif
                return true;
            return false;
        }

        public bool playerOneisPreviousPressed(ListKey key_)
        {
#if XBOX
            if (previousGamepadStatePlayerOne.IsButtonDown(buttonsConfig.getValue(key_)))
#else
            if (previousKeysState.IsKeyDown(keyConfig.getValue(key_)))
#endif
                return true;
            return false;
        }

        public bool playerOneisPreviousUp(ListKey key_)
        {
#if XBOX
            if (previousGamepadStatePlayerOne.IsButtonUp(buttonsConfig.getValue(key_)))
#else
            if (previousKeysState.IsKeyUp(keyConfig.getValue(key_)))
#endif
                return true;
            return false;
        }

        public bool playerOneisPushed(ListKey key_)
        {
            if (playerOneisPressed(key_) && !playerOneisPreviousPressed(key_))
                return true;
            return false;
        }

        public bool playerTwoisPressed(ListKey key_)
        {
#if XBOX
            if (gamepadStatePlayerTwo.IsButtonDown(buttonsConfig.getValue(key_)))
#else
            if (gamepadStatePlayerOne.IsButtonDown(buttonsConfig.getValue(key_)))
#endif
                return true;
            return false;
        }

        public bool playerTwoisUp(ListKey key_)
        {
#if XBOX
            if (gamepadStatePlayerTwo.IsButtonUp(buttonsConfig.getValue(key_)))
#else
            if (gamepadStatePlayerOne.IsButtonUp(buttonsConfig.getValue(key_)))
#endif
                return true;
            return false;
        }

        public bool playerTwoisPreviousPressed(ListKey key_)
        {
#if XBOX
            if (previousGamepadStatePlayerTwo.IsButtonDown(buttonsConfig.getValue(key_)))
#else
            if (previousGamepadStatePlayerOne.IsButtonDown(buttonsConfig.getValue(key_)))
#endif
                return true;
            return false;
        }

        public bool playerTwoisPreviousUp(ListKey key_)
        {
#if XBOX
            if (previousGamepadStatePlayerTwo.IsButtonUp(buttonsConfig.getValue(key_)))
#else
            if (previousGamepadStatePlayerOne.IsButtonUp(buttonsConfig.getValue(key_)))
#endif
                return true;
            return false;
        }

        public bool playerTwoisPushed(ListKey key_)
        {
            if (playerTwoisPressed(key_) && !playerTwoisPreviousPressed(key_))
                return true;
            return false;
        }


        public void update()
        {
            if (!checkUpdate)
                checkUpdate = true;
            else
            {
                previousKeysState = keysState;
                previousGamepadStatePlayerOne = gamepadStatePlayerOne;
                previousGamepadStatePlayerTwo = gamepadStatePlayerTwo;
            }
            keysState = Keyboard.GetState();
            gamepadStatePlayerOne = GamePad.GetState(PlayerIndex.One);
            gamepadStatePlayerTwo = GamePad.GetState(PlayerIndex.Two);
        }
    }
}
