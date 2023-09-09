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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Jumper.Helpers;
using Jumper.Enums;

namespace Jumper
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class JumperGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int Width;
        public static int Height;

        public static GameStateEnum GameState = GameStateEnum.Menu;

        KeyboardState keyboardState = new KeyboardState();
        KeyboardState oldState;

        GameManager manager;
        MainMenu menu;

        public JumperGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Width = graphics.PreferredBackBufferWidth = 600;
            Height = graphics.PreferredBackBufferHeight = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
            LoadHelper.Load(Content);
            manager = new GameManager(Width/20,Height/20);
            menu = new MainMenu();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (GameState == GameStateEnum.Menu)
            {
                menu.Update();
            }
            else if (GameState == GameStateEnum.Game)
            {
                KeyboardInput();
                manager.Update(gameTime);
            }
            else
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.WhiteSmoke);

            // TODO: Add your drawing code here
            if (GameState == GameStateEnum.Menu)
            {
                GraphicsDevice.Clear(Color.White);
                menu.Draw(spriteBatch);
            }
            else if (GameState == GameStateEnum.Game)
            {
                spriteBatch.Begin();               

                manager.Draw(spriteBatch);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }


        private void KeyboardInput()
        {
            keyboardState = Keyboard.GetState();

            #region SonicKeys

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                manager.RunHero(HeroEnum.Sonic, false);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                manager.RunHero(HeroEnum.Sonic, true);
            }
            else
                manager.StopHero(HeroEnum.Sonic);


            if (keyboardState.IsKeyDown(Keys.Up))
            {
                manager.JumpHero(HeroEnum.Sonic);
            }

            if (keyboardState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                manager.ShootHero(HeroEnum.Sonic);                
            }
            #endregion            

            #region MegamanKeys

            if (keyboardState.IsKeyDown(Keys.A))
            {
                manager.RunHero(HeroEnum.Megaman, false);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                manager.RunHero(HeroEnum.Megaman, true);
            }
            else
                manager.StopHero(HeroEnum.Megaman);


            if (keyboardState.IsKeyDown(Keys.W))
            {
                manager.JumpHero(HeroEnum.Megaman);
            }

            if (keyboardState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                manager.ShootHero(HeroEnum.Megaman);            
            }
            #endregion

            if (keyboardState.IsKeyDown(Keys.Escape))
                GameState = GameStateEnum.Menu;

            if (GameState == GameStateEnum.Game)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    manager.NextLevel();  
                }
            }

            oldState = keyboardState;
        }

    }
}
