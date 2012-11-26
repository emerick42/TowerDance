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


namespace PrototypeTowerDefense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int WidthXbox = 1280;
        int HeightXbox = 720;
        Warrior warrior;
        Warrior warrior2;
        Archer archer;
        Archer archer2;

        Texture2D background;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = WidthXbox;
            this.graphics.PreferredBackBufferHeight = HeightXbox;
            this.graphics.IsFullScreen = false;
            this.graphics.ApplyChanges();


            this.Window.Title = "Prototype Tower Defense";

            this.Window.AllowUserResizing = true;

            // LEFT = 1; RIGHT = 2; UP = 3; DOWN = 4;
            warrior = new Warrior(new Vector2(0, HeightXbox / 2), 2, Content);
            warrior2 = new Warrior(new Vector2(WidthXbox / 2, 0), 4, Content);
            archer = new Archer(new Vector2(WidthXbox, HeightXbox / 2), 1, Content);
            archer2 = new Archer(new Vector2(WidthXbox / 2, HeightXbox), 3, Content);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            warrior.load();
            warrior2.load();
            archer.load();
            archer2.load();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            background.Dispose();
            warrior.unload();
            warrior2.unload();
            archer.unload();
            archer2.unload();
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

            warrior.update(gameTime, keyboard, gamepad);
            warrior2.update(gameTime, keyboard, gamepad);
            archer.update(gameTime, keyboard, gamepad);
            archer2.update(gameTime, keyboard, gamepad);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(), Color.White);
            warrior.draw(spriteBatch);
            warrior2.draw(spriteBatch);
            archer.draw(spriteBatch);
            archer2.draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
