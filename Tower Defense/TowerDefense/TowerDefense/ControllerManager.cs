using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Input;
using TowerDance.Models;
using TowerDance.Controllers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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

        protected override void LoadContent()
        {
            _controller.loadContent(GraphicsDevice, Content, _windowConfiguration);
            foreach (AController c in _controller.getChildren())
                c.loadContent(GraphicsDevice, Content, _windowConfiguration);
        }

        protected override void Update(GameTime gameTime)
        {
            if (cleanControllerTree(_controller))
                Exit();
            updateController(gameTime, _controller);
            base.Update(gameTime);
        }

        private void updateController(GameTime gameTime, AController controller)
        {
            if (!controller.isContentLoaded())
                controller.loadContent(GraphicsDevice, Content, _windowConfiguration);
            if (controller.getChildren().Count <= 0)
            {
                if (!controller.isReady())
                    return;
                controller.update(gameTime);
            }
            else
            {
                controller.updateBackgrounded(gameTime);
                foreach (AController c in controller.getChildren())
                    updateController(gameTime, c);
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
            if (controller.getChildren().Count <= 0)
                controller.draw(gameTime);
            else
            {
                controller.drawBackgrounded(gameTime);
                foreach (AController c in controller.getChildren())
                    drawController(gameTime, c);
            }
        }

        private bool cleanControllerTree(AController controller)
        {
            controller.flushChildren();
            int i = 0;
            List<AController> children = controller.getChildren();
            if (children.Count > 0)
            {
                while (i < children.Count)
                {
                    if (!cleanControllerTree(children[i]))
                        i++;
                }
            }
            children = controller.getChildren();
            if (children.Count <= 0 && controller.isStopped())
            {
                if (controller == _controller)
                    Exit();
                else
                {
                    AController parent = controller.getParent();
                    parent.getChildren().Remove(controller);
                    return true;
                }
            }
            return false;
        }

/*        private void cleanControllerTree(AController controller)
        {
            int i = 0;

            if (controller == _controller && controller.isStopped())
                Exit();
            List<AController> children = controller.getChildren();
            if (children.Count > 0)
            {
                while (i < children.Count)
                {
                    if (children[i].isStopped())
                        children.RemoveAt(i);
                    else
                    {
                        cleanControllerTree(children[i]);
                        i++;
                    }
                }
                if (controller.getChildren().Count <= 0)
                    controller.switchState();
            }
        }*/
    }
}
