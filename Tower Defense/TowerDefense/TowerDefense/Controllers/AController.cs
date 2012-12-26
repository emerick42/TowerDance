using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TowerDance.Views;

namespace TowerDance.Controllers
{
    abstract class AController
    {
        public List<AController> children = new List<AController>();
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _contentManager;
        private bool _isStopped = false;
        protected List<IView> _views = new List<IView>();
        protected List<IView> _backgroundedViews = new List<IView>();
        protected bool _contentIsLoaded = false;

        public virtual void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _contentIsLoaded = true;
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            foreach (IView v in _views)
                v.loadContent(_graphicsDevice, _contentManager);
            foreach (IView v in _backgroundedViews)
                v.loadContent(_graphicsDevice, _contentManager);
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
                view.loadContent(_graphicsDevice, _contentManager);
        }
        public void addBackgroundView(IView view)
        {
            _backgroundedViews.Add(view);
            if (_contentIsLoaded)
                view.loadContent(_graphicsDevice, _contentManager);
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
