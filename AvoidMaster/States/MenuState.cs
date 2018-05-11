using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.States
{
    class MenuState : State
    {
        private List<Component> components;
        private Background background;
        public MenuState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            Button newGameButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                newGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(300, 200),
                    Text = "New Game"
                };
                newGameButton.Click += NewgameButton_Click;
            }

            Button hightScoreButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                hightScoreButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(300, 250),
                    Text = "Hight Score"
                };
                hightScoreButton.Click += HightScoreButton_Click;
            }

            Button quitGameButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                quitGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(300, 300),
                    Text = "Quit Game"
                };
                quitGameButton.Click += QuitgameButton_Click;
            }

            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream("Content/MenuBackground.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var rectangle = new Rectangle(game.Window.Position.X, game.Window.Position.Y, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
                background = new Background(backgroundTexture, rectangle, 3, 5, 15);
                background.isHaveAnimation = true;
            }
            game.Window.Position = new Point((graphics.GraphicsDevice.DisplayMode.Width - game.Window.ClientBounds.Width) / 2
               , (graphics.GraphicsDevice.DisplayMode.Height - game.Window.ClientBounds.Height) / 2); // xPos and yPos (pixel)

            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = background.Width;
            graphics.ApplyChanges();

            // load component
            components = new List<Component>()
            {
                newGameButton,hightScoreButton,quitGameButton
            };
        }

        private void QuitgameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void NewgameButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, this.graphics, content));
        }

        private void HightScoreButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new HightScoreState(game, this.graphics, content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(gameTime, spriteBatch);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
            background.Update(gameTime);
        }
    }
}
