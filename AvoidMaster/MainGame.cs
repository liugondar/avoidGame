using AvoidMaster.Bus;
using AvoidMaster.Components;
using AvoidMaster.Sprite;
using AvoidMaster.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AvoidMaster
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Form MyGameForm;

        private State currentState;
        private State nextState;

        protected List<Components.Component> components;
        private Background background;
        public void ChangeState(State state)
        {
            nextState = state;
        }
        public MainGame()
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
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;

            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/MenuBackground.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var rectangle = new Rectangle(Window.Position.X, Window.Position.Y, Window.ClientBounds.Width, Window.ClientBounds.Height);
                background = new Background(backgroundTexture, rectangle, 3, 5, 15);
            }

            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();

            MyGameForm = (Form)Form.FromHandle(Window.Handle);
            MyGameForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Window.Position = new Point((graphics.GraphicsDevice.DisplayMode.Width - Window.ClientBounds.Width) / 2
                , (graphics.GraphicsDevice.DisplayMode.Height - Window.ClientBounds.Height) / 2); // xPos and yPos (pixel)
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new MenuState(this, graphics, Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            currentState.Update(gameTime);
            currentState.PostUpdate(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            currentState.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
