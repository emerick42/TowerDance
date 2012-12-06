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

namespace TowerDefense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ControllerGame : Microsoft.Xna.Framework.Game
    {
        public static int sizeWidth = 1280;
        public static int sizeHeight = 720;

        GraphicsDeviceManager graphics;

        List<Entity> listEntity;
        View view;

        Warrior warrior;
        Warrior warrior2;
        Archer archer;
        Archer archer2;

        Castle castle;

        World world;

        public ControllerGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            world = new World();
            listEntity = new List<Entity>();
            view = new View();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = sizeWidth;
            this.graphics.PreferredBackBufferHeight = sizeHeight;
            this.graphics.IsFullScreen = false;
            this.graphics.ApplyChanges();

            this.Window.Title = "Tower Defense";

            this.Window.AllowUserResizing = false;


            // CONFIGURE BEFORE ADD IN DA LIST
            warrior = new Warrior();
            warrior2 = new Warrior(true);
            archer = new Archer();
            archer2 = new Archer(true);
            castle = new Castle();

            warrior.Direction = DIRECTION.LEFT;
            warrior2.setPositionDirection(new Vector2(sizeWidth / 2, 0), DIRECTION.DOWN);
            archer.Direction = DIRECTION.RIGHT;
            archer2.setPositionDirection(new Vector2(sizeWidth / 2, sizeHeight), DIRECTION.UP);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            view.load(GraphicsDevice);

            world.setTexture(Content.Load<Texture2D>("background"));

            listEntity.Add(world); // FIRST

            listEntity.Add(castle); // SECOND

            listEntity.Add(warrior);
            listEntity.Add(warrior2);
            listEntity.Add(archer);
            listEntity.Add(archer2);

            foreach (Entity entity in listEntity)
                entity.load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (Entity entity in listEntity)
                entity.unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);

            if (keyboard.IsKeyDown(Keys.Escape) || gamepad.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            foreach (Entity entity in listEntity)
                entity.update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            view.draw(listEntity);

            base.Draw(gameTime);
        }
    }
}
