using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDance.Controllers
{
    abstract class AController
    {
        public List<AController> children = new List<AController>();
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _contentManager;
        private bool _isStopped = false;

        public virtual void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
        }
        public void stop()
        {
            _isStopped = true;
        }
        public bool isStopped()
        {
            return _isStopped;
        }
        public abstract void update(GameTime gameTime);
        public abstract void updateBackgrounded(GameTime gameTime);
        public abstract void draw(GameTime gameTime);
        public abstract void drawBackgrounded(GameTime gameTime);
    }
}
