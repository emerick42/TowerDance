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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class World
    {
        public static int sizeWidth = 1280;
        public static int sizeHeight = 720;

        List<Entity> listEntity;
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
            view = new View(graphics);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected void Initialize()
        {
            view.initialize();

            //this.Window.Title = "Tower Defense";

            //this.Window.AllowUserResizing = false;

            world = new Background();
            world.CollisionAction = new CollisionAction();
            listEntity = new List<Entity>();
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

            listEntity.Add(castle); // FIRST

            listEntity.Add(warrior);
            listEntity.Add(warrior2);
            listEntity.Add(archer);

            //foreach (Entity entity in listEntity)
            //    entity.load(Content);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected void LoadContent()
        {
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected void UnloadContent()
        {
            foreach (Entity entity in listEntity)
                entity.unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected void Update(GameTime gameTime)
        {
            gameRessource.update(gameTime);
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);

            controlInput.update();

            player.updateInput(controlInput);

            //if (player.newUnit(gameRessource))
            //    listEntity.Add(player.createUnit(Content));

            listEntity = refreshListEntity();

            foreach (EntityUnit entity in listEntity)
                entity.update(gameRessource);

            world.CollisionAction.start(listEntity);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected void Draw(GameTime gameTime)
        {
            view.draw(listEntity, world);
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
