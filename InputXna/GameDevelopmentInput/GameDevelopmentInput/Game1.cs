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

namespace GameDevelopmentInput
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 position;
        Texture2D texture2;
        Vector2 position2;
        Texture2D texture3;
        Vector2 position3;
        ControlInput controlInput;

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
            controlInput = new ControlInput();
            // TODO: Add your initialization logic here
            position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            position2 = new Vector2(Window.ClientBounds.Width - 100, Window.ClientBounds.Height / 2);
            position3 = new Vector2(0, 0);

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
            texture = Content.Load<Texture2D>(@"akuma");
            texture2 = Content.Load<Texture2D>(@"ryu");
            texture3 = Content.Load<Texture2D>(@"ken");
            controlInput.SaveInput();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyS = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            // Allows the game to exit
            if (controlInput.isPressed(ListKey.PAUSE))
//           if (keyS.IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            controlInput.update();
            ryu(keyS, gamepadState);
            akuma(keyS, gamepadState);
            ken(keyS, gamepadState);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Chocolate);

            spriteBatch.Begin();

            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture2, position2, Color.White);
            spriteBatch.Draw(texture3, position3, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void ryu(KeyboardState keyS, GamePadState gamepadState)
        {
            if (controlInput.isPressed(ListKey.LEFTARROW))
//            if (keyS.IsKeyDown(Keys.H) || (gamepadState.Buttons.X == ButtonState.Pressed))
            {
                if (position2.X > 0)
                    position2 += new Vector2(-5f, 0f);
                //                GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
            }

            if (controlInput.isPressed(ListKey.DOWNARROW))
            //if (keyS.IsKeyDown(Keys.N) || (gamepadState.Buttons.A == ButtonState.Pressed))
            {
                if (position2.Y < Window.ClientBounds.Height - texture2.Height)
                    position2 += new Vector2(0f, 5f);
                //                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }

            if (controlInput.isPressed(ListKey.RIGHTARROW))
            //if (keyS.IsKeyDown(Keys.J) || (gamepadState.Buttons.B == ButtonState.Pressed))
            {
                if (position2.X < Window.ClientBounds.Width - texture2.Width)
                    position2 += new Vector2(5f, 0f);
            }

            if (controlInput.isPressed(ListKey.UPARROW))
//            if (keyS.IsKeyDown(Keys.U) || (gamepadState.Buttons.Y == ButtonState.Pressed))
            {
                if (position2.Y > 0)
                    position2 += new Vector2(0f, -5f);
            }
        }

        protected void akuma(KeyboardState keyS, GamePadState gamepadState)
        {
            if (controlInput.isPressed(ListKey.LEFTWAY))
            {
                if (position.X > 0)
                    position += new Vector2(-5f, 0f);
                //                GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
            }

            if (controlInput.isPressed(ListKey.DOWNWAY))
            {
                if (position.Y < Window.ClientBounds.Height - texture.Height)
                    position += new Vector2(0f, 5f);
                //                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }


            if (controlInput.isPressed(ListKey.RIGHTWAY))
            {
                if (position.X < Window.ClientBounds.Width - texture.Width)
                    position += new Vector2(5f, 0f);
            }

            if (controlInput.isPressed(ListKey.UPWAY))
            {
                if (position.Y > 0)
                    position += new Vector2(0f, -5f);
            }
        }

        protected void ken(KeyboardState keyS, GamePadState gamepadState)
        {
            if (controlInput.isPressed(ListKey.FIRSTUNIT))
            {
                if (position3.X > 0)
                    position3 += new Vector2(-5f, 0f);
                //                GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
            }

            if (controlInput.isPressed(ListKey.SECONDUNIT))
            {
                if (position3.Y < Window.ClientBounds.Height - texture3.Height)
                    position3 += new Vector2(0f, 5f);
                //                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }

            if (controlInput.isPressed(ListKey.THIRDUNIT))
            {
                if (position3.X < Window.ClientBounds.Width - texture3.Width)
                    position3 += new Vector2(5f, 0f);
            }

            if (controlInput.isPressed(ListKey.FOURTHUNIT))
            {
                if (position3.Y > 0)
                    position3 += new Vector2(0f, -5f);
            }
        }
    }
}
