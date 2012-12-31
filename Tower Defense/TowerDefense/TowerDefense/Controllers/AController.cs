using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TowerDance.Views;
using TowerDance.Models;

namespace TowerDance.Controllers
{
    abstract class AController
    {
        public List<AController> children = new List<AController>();
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _contentManager;
        protected WindowConfiguration _windowConfiguration;
        private bool _isStopped = false;
        protected List<IView> _views = new List<IView>();
        protected List<IView> _backgroundedViews = new List<IView>();
        protected bool _contentIsLoaded = false;

        public virtual void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _contentIsLoaded = true;
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _windowConfiguration = windowConfiguration;
            foreach (IView v in _views)
                v.loadContent(_graphicsDevice, _contentManager, _windowConfiguration);
            foreach (IView v in _backgroundedViews)
                v.loadContent(_graphicsDevice, _contentManager, _windowConfiguration);
        }
        public void stop()
        {
            _isStopped = true;
        }
        public bool isStopped()
        {
            return _isStopped;
        }
        public void addView(IView view)
        {
            _views.Add(view);
            if (_contentIsLoaded)
                view.loadContent(_graphicsDevice, _contentManager, _windowConfiguration);
        }
        public void addBackgroundView(IView view)
        {
            _backgroundedViews.Add(view);
            if (_contentIsLoaded)
                view.loadContent(_graphicsDevice, _contentManager, _windowConfiguration);
        }
        public abstract void update(GameTime gameTime);
        public abstract void updateBackgrounded(GameTime gameTime);
        public virtual void draw(GameTime gameTime)
        {
            foreach (IView v in _views)
                v.draw();
        }
        public virtual void drawBackgrounded(GameTime gameTime)
        {
            foreach (IView v in _backgroundedViews)
                v.draw();
        }
    }
}
