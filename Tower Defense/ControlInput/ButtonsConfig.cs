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
    public class ButtonsConfig
    {
        private SerializeData<ConfigInputXbox> serializeData;
        private ConfigInputXbox configInput;

        Dictionary<ListKey, Buttons> configKeys;

        public ButtonsConfig()
        {
            configInput = new ConfigInputXbox();
            configKeys = new Dictionary<ListKey, Buttons>();
            serializeData = new SerializeData<ConfigInputXbox>("inputXbox.xml");
        }

        public Buttons getValue(ListKey key_)
        {
            return configKeys[key_];
        }

        public void setValue(ListKey getKey, Buttons value)
        {
            configKeys[getKey] = value;
        }

        public void SaveConfig()
        {
            ConfigInputXbox saveInput = new ConfigInputXbox(configKeys);

            serializeData.SaveGameConfiguration(saveInput, "Config");
        }

        public void LoadConfig()
        {

            configInput = serializeData.LoadGameConfiguration(configInput, "Config");

            configKeys.Add(ListKey.VALID, Buttons.A);

            #region ARROW
            configKeys.Add(ListKey.LEFTARROW, configInput.LeftArrow);
            configKeys.Add(ListKey.DOWNARROW, configInput.DownArrow);
            configKeys.Add(ListKey.UPARROW, configInput.UpArrow);
            configKeys.Add(ListKey.RIGHTARROW, configInput.RightArrow);
            #endregion

            #region ARROW RIGHT STICK
            configKeys.Add(ListKey.LEFTARROWRIGHT, configInput.LeftArrowRight);
            configKeys.Add(ListKey.DOWNARROWRIGHT, configInput.DownArrowRight);
            configKeys.Add(ListKey.UPARROWRIGHT, configInput.UpArrowRight);
            configKeys.Add(ListKey.RIGHTARROWRIGHT, configInput.RightArrowRight);
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
