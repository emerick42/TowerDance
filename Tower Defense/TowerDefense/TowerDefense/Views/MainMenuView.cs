using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TowerDance.Views
{
    class MainMenuView
    {
        GraphicsDevice _graphicsDevice;

        public MainMenuView()
        {

        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void draw()
        {

        }
    }
}
