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
    class ArcherView : IView
    {
        List<Archer> listAddArcher;
        Dictionary<ConfViewArcher, Archer> listArcher;
        SpriteBatch sb;
        DrawPrimitive dp;
        GraphicsDevice _graphicsDevice;
        ContentManager _contentManager;

        public ArcherView()
        {
            listAddArcher = new List<Archer>();
            listArcher = new Dictionary<ConfViewArcher, Archer>();
            dp = new DrawPrimitive();
        }

        public void setArcher(List<Archer> newArchers)
        {
            listAddArcher = newArchers;
            foreach (Archer a in listAddArcher)
            {
                ConfViewArcher newConf = new ConfViewArcher(a.Direction);
                listArcher[newConf] = a;
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
            foreach (KeyValuePair<ConfViewArcher, Archer> pair in listArcher)
            {
                if (pair.Value.Ennemy)
                    pair.Key.loadEnnemy(_contentManager);
                else
                    pair.Key.loadAllie(_contentManager);
            }
        }

        public void unload()
        {
            foreach (KeyValuePair<ConfViewArcher, Archer> pair in listArcher)
                pair.Key.unload();
        }

        public void setRefreshList(List<Archer> listRefreshArcher)
        {
            foreach (Archer w in listRefreshArcher)
            {
                Dictionary<ConfViewArcher, Archer> testArcher = new Dictionary<ConfViewArcher, Archer>(listArcher);
                foreach (KeyValuePair<ConfViewArcher, Archer> pair in testArcher)
                {
                    if (pair.Value.ObjectID == w.ObjectID)
                        listArcher[pair.Key] = w;
                }
            }
        }

        public void update(GameTime gameTime)
        {
            refreshList();

            foreach (KeyValuePair<ConfViewArcher, Archer> pair in listArcher)
                pair.Key.update(gameTime, pair.Value.CurrentAction);
        }

        private void refreshList()
        {
            Dictionary<ConfViewArcher, Archer> testArcher = new Dictionary<ConfViewArcher, Archer>(listArcher);

            foreach (KeyValuePair<ConfViewArcher, Archer> pair in testArcher)//Entity unit in listEntity)
            {
                if (pair.Key.isAvaible())
                    listArcher.Remove(pair.Key);
            }
        }

        public void draw()
        {
            sb.Begin();
            foreach (KeyValuePair<ConfViewArcher, Archer> pair in listArcher)
                pair.Key.draw(sb, dp, pair.Value.Position, pair.Value.LifePoint, pair.Value.MaxPoint);
            sb.End();
        }
    }
}
