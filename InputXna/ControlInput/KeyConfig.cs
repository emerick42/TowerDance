using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Data;

namespace Input
{
    public class KeyConfig
    {
        private SerializeData<ConfigInput> serializeData;
        private ConfigInput configInput;

        Dictionary<ListKey, Keys> configKeys;

        public KeyConfig()
        {
            configInput = new ConfigInput();
            configKeys = new Dictionary<ListKey, Keys>();
            serializeData = new SerializeData<ConfigInput>("input.cfg");
        }

        public Keys getValue(ListKey key_)
        {
            return configKeys[key_];
        }

        public void setValue(ListKey getKey, Keys value)
        {
            configKeys[getKey] = value;
        }

        public void SaveConfig()
        {
            ConfigInput saveInput = new ConfigInput(configKeys);

            serializeData.SaveGameConfiguration(saveInput, "Config");
        }

        public void LoadConfig()
        {

            configInput = serializeData.LoadGameConfiguration(configInput, "Config");

            #region ARROW
            configKeys.Add(ListKey.LEFTARROW, configInput.LeftArrow);
            configKeys.Add(ListKey.DOWNARROW, configInput.DownArrow);
            configKeys.Add(ListKey.UPARROW, configInput.UpArrow);
            configKeys.Add(ListKey.RIGHTARROW, configInput.RightArrow);
            #endregion

            #region WAY
            configKeys.Add(ListKey.LEFTWAY, configInput.LeftWay);
            configKeys.Add(ListKey.DOWNWAY, configInput.DownWay);
            configKeys.Add(ListKey.RIGHTWAY, configInput.RightWay);
            configKeys.Add(ListKey.UPWAY, configInput.UpWay);
            #endregion

            #region UNITS
            configKeys.Add(ListKey.FIRSTUNIT, configInput.FirstUnit);
            configKeys.Add(ListKey.SECONDUNIT, configInput.SecondUnit);
            configKeys.Add(ListKey.THIRDUNIT, configInput.ThirdUnit);
            configKeys.Add(ListKey.FOURTHUNIT, configInput.FourthUnit);
            #endregion

            #region PAUSE
            configKeys.Add(ListKey.PAUSE, configInput.Pause);
            #endregion
        }
    }
}
