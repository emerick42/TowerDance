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
        protected List<AController> _children = new List<AController>();
        protected List<AController> _tmpChildren = new List<AController>();
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _contentManager;
        protected WindowConfiguration _windowConfiguration;
        private bool _isStopped = false;
        protected List<IView> _views = new List<IView>();
        protected List<IView> _backgroundedViews = new List<IView>();
        protected bool _contentIsLoaded = false;
        protected AController _parent = null;
        protected int _frame = 0;

        public bool isContentLoaded()
        {
            return _contentIsLoaded;
        }

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

        public virtual void stop()
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

        public virtual void signal(string signal)
        {

        }

        public void addChild(AController child)
        {
            _tmpChildren.Add(child);
            child._parent = this;
        }

        public void flushChildren()
        {
            if (_tmpChildren.Count > 0)
            {
                foreach (AController child in _tmpChildren)
                    _children.Add(child);
                _tmpChildren.Clear();
                switchState();
            }
        }

        public List<AController> getChildren()
        {
            return _children;
        }

        public AController getParent()
        {
            return _parent;
        }

        public virtual bool isReady()
        {
            if (_frame > 40)
                return true;
            _frame++;
            return false;
        }

        public void switchState()
        {
            _frame = 0;
        }
    }
}
