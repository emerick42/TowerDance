using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Input;
using TowerDance.Views;

namespace TowerDance.Controllers
{
    class TextController : AController
    {
        TextView _textView;
        ControlInput _controlInput;
        string _text;

        public TextController(string text)
        {
            _text = text;
            _textView = new TextView(_text);
            _controlInput = new ControlInput();
            addView(_textView);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _controlInput.update();
            if (_controlInput.isPushed(ListKey.VALID)
                || _controlInput.isPushed(ListKey.PAUSE))
            {
                stop();
                return;
            }
        }

        public override void updateBackgrounded(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
