using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDance.Views
{
    interface IView
    {
        void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager);
        void draw();
    }
}
