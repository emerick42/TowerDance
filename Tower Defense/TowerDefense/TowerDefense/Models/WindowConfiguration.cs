using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance.Models
{
    class WindowConfiguration
    {
        private int _width;
        private int _newWidth;
        public int width
        {
            set { this._newWidth = value; }
            get { return this._width; }
        }
        private int _height;
        private int _newHeight;
        public int height
        {
            set { this._newHeight = value; }
            get { return this._height; }
        }
        private string _name;
        private string _newName;
        public string name
        {
            set { this._newName = value; }
            get { return this._name; }
        }
        private GraphicsDeviceManager _graphics;
        private GameWindow _window;

        public WindowConfiguration(GraphicsDeviceManager graphics, GameWindow window, int width = 800, int height = 600, string name = "TowerDance")
        {
            _graphics = graphics;
            _window = window;
            _width = width;
            _height = height;
            _name = name;
        }

        public void applyChanges()
        {
            _height = _newHeight;
            _width = _newWidth;
            _name = _newName;
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.PreferredBackBufferWidth = _width;
            _window.Title = _name;
            _graphics.ApplyChanges();
        }
    }
}
