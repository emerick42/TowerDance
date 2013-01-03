using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDance.Models.Dance;
using TowerDance.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using TowerDance.Models;

namespace TowerDance
{
    class WarriorView : IView
    {
        List<Warrior> listAddWarrior;
        Dictionary<ConfViewWarrior, Warrior> listWarrior;
        SpriteBatch sb;
        DrawPrimitive dp;
        GraphicsDevice _graphicsDevice;
        ContentManager _contentManager;

        public WarriorView()
        {
            listAddWarrior = new List<Warrior>();
            listWarrior = new Dictionary<ConfViewWarrior, Warrior>();
            dp = new DrawPrimitive();
        }

        public void setWarrior(List<Warrior> newWarriors)
        {
            listAddWarrior = newWarriors;
            foreach (Warrior w in listAddWarrior)
            {
                ConfViewWarrior newConf = new ConfViewWarrior(w.Direction);
                listWarrior[newConf] = w;
            }
        }

        public void loadContent(GraphicsDevice graphicsDevice, ContentManager contentManager, WindowConfiguration windowConfiguration)
        {
            _graphicsDevice = graphicsDevice;
            sb = new SpriteBatch(_graphicsDevice);
            _contentManager = contentManager;
            dp.load(graphicsDevice);
        }

        public void loadSprite()
        {
            foreach (KeyValuePair<ConfViewWarrior, Warrior> pair in listWarrior)
            {
                if (pair.Value.Ennemy)
                    pair.Key.loadEnnemy(_contentManager);
                else
                    pair.Key.loadAllie(_contentManager);
            }
        }

        public void unload()
        {
            foreach (KeyValuePair<ConfViewWarrior, Warrior> pair in listWarrior)
                pair.Key.unload();
        }

        public void update(GameTime gameTime)
        {
            foreach (KeyValuePair<ConfViewWarrior, Warrior> pair in listWarrior)
                pair.Key.update(gameTime, pair.Value.CurrentAction);
        }

        public void draw()
        {
            sb.Begin();
            foreach (KeyValuePair<ConfViewWarrior, Warrior> pair in listWarrior)
                pair.Key.draw(sb, dp, pair.Value.Position, pair.Value.LifePoint, pair.Value.MaxPoint);
            sb.End();
        }
    }
}
