using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Input;

namespace TowerDance
{
    public class World
    {
        public static int sizeWidth = 1280;
        public static int sizeHeight = 720;

        List<Entity> listEntity;
        List<Entity> listNewEntity;
        View view;
        Player player;

        Warrior warrior;
        Warrior warrior2;
        Archer archer;

        Castle castle;

        ControlInput controlInput;

        Background world;

        GameRessource gameRessource;

        public World()
        {
            //graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
            //view = new View(graphics);

            //view.initialize();

            //this.Window.Title = "Tower Defense";

            //this.Window.AllowUserResizing = false;

            world = new Background();
            world.CollisionAction = new CollisionAction();
            listEntity = new List<Entity>();
            listNewEntity = new List<Entity>();
            player = new Player();

            player.setChoiceUnit();

            controlInput = new ControlInput();

            gameRessource = new GameRessource();

            // CONFIGURE BEFORE ADD IN DA LIST

            warrior = new Warrior(true);
            warrior2 = new Warrior(true);
            archer = new Archer(true);
            castle = new Castle();

            warrior.setPositionDirection(new Vector2(sizeWidth / 2, 0), DIRECTION.DOWN);
            warrior2.setPositionDirection(new Vector2(0, sizeHeight / 2), DIRECTION.RIGHT);
            archer.setPositionDirection(new Vector2(sizeWidth, sizeHeight / 2), DIRECTION.LEFT);

            // Create a new SpriteBatch, which can be used to draw textures.
            //view.load(GraphicsDevice);

            //world.setTexture(Content.Load<Texture2D>("background"));

            controlInput.SaveInput();

            //listEntity.Add(castle); // FIRST

            listEntity.Add(warrior);
            listEntity.Add(warrior2);
            listNewEntity.Add(warrior);
            listNewEntity.Add(warrior2);
            //listEntity.Add(archer);

            //foreach (Entity entity in listEntity)
            //    entity.load(Content);
        }

        public void UnloadContent()
        {
            foreach (Entity entity in listEntity)
                entity.unload();
        }

        public void Update(GameTime gameTime)
        {
            gameRessource.update(gameTime);
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);

            controlInput.update();

            player.updateInput(controlInput);

            if (player.newUnit(gameRessource))
                ;//    listEntity.Add(player.createUnit(Content));

            listEntity = refreshListEntity();

            foreach (EntityUnit entity in listEntity)
                entity.update(gameRessource);

            world.CollisionAction.start(listEntity);
        }

        public bool isNew()
        {
            if (listNewEntity.Count > 0)
                return true;
            return false;
        }

        public List<Warrior> getWarrior()
        {
            List<Warrior> res = new List<Warrior>();
            int i = 0;

            foreach (Entity unit in listNewEntity)
            {
                if (unit.getType() == ENTITYTYPE.WARRIOR)
                    res.Add((Warrior)unit);
            }
            while (i < listNewEntity.Count)
            {
                if (listNewEntity[i].getType() == ENTITYTYPE.WARRIOR)
                {
                    listNewEntity.RemoveAt(i);
                    i = 0;
                }
                else
                    i += 1;
            }
            return res;
        }

        public List<Archer> getArcher()
        {
            List<Archer> res = new List<Archer>();
            int i = 0;

            foreach (Entity unit in listNewEntity)
            {
                if (unit.getType() == ENTITYTYPE.ARCHER)
                    res.Add((Archer)unit);
            }
            while (i < listNewEntity.Count)
            {
                if (listNewEntity[i].getType() == ENTITYTYPE.ARCHER)
                {
                    listNewEntity.RemoveAt(i);
                    i = 0;
                }
                else
                    i += 1;
            }
            return res;
        }

        public Castle getCastle()
        {
            Castle res = new Castle();
            int i = 0;

            foreach (Entity unit in listNewEntity)
            {
                if (unit.getType() == ENTITYTYPE.CASTLE)
                    res = (Castle)unit;
            }
            while (i < listNewEntity.Count)
            {
                if (listNewEntity[i].getType() == ENTITYTYPE.CASTLE)
                {
                    listNewEntity.RemoveAt(i);
                    i = 0;
                }
                else
                    i += 1;
            }
            return res;
        }

        private List<Entity> refreshListEntity()
        {
            List<Entity> res = new List<Entity>();

            foreach (Entity unit in listEntity)
            {
                if (unit.isAvailable())
                    res.Add(unit);
            }
            return res;
        }
    }
}
