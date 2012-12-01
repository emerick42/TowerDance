using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DDR
{
    class WindowConfiguration : IWindowConfiguration
    {
        private int _width;
        public int width
        {
            set { this._width = value; }
            get { return this._width; }
        }
        private int _height;
        public int height
        {
            set { this._height = value; }
            get { return this._height; }
        }
        private GraphicsDeviceManager _graphics;

        public WindowConfiguration(GraphicsDeviceManager graphics, int width = 800, int height = 600)
        {
            _graphics = graphics;
            _width = width;
            _height = height;
        }

        public void applyChanges()
        {
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.PreferredBackBufferWidth = _width;
            _graphics.ApplyChanges();
        }
    }
}
