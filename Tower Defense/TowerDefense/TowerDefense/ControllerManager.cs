using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Input;
using TowerDance.Models;
using TowerDance.Controllers;

namespace TowerDance
{
    class ControllerManager : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        ControlInput _controlInput;
        WindowConfiguration _windowConfiguration;
        AController _controller;

        public ControllerManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _windowConfiguration = new WindowConfiguration(_graphics, this.Window);
            _controlInput = new ControlInput();
            _controller = new MainMenuController();
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            updateController(gameTime, _controller);
            base.Update(gameTime);
        }

        private void updateController(GameTime gameTime, AController controller)
        {
            if (controller.children.Count <= 0)
                controller.update(gameTime);
            else
            {
                controller.updateBackgrounded(gameTime);
                foreach (AController c in controller.children)
                {
                    updateController(gameTime, c);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            drawController(gameTime, _controller);
            base.Draw(gameTime);
        }

        private void drawController(GameTime gameTime, AController controller)
        {
            if (controller.children.Count <= 0)
                controller.draw(gameTime);
            else
            {
                controller.drawBackgrounded(gameTime);
                foreach (AController c in controller.children)
                {
                    drawController(gameTime, c);
                }
            }
        }
    }
}
