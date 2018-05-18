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
        private Button newGameButton;
        private Button hightScoreButton;
        private Button quitGameButton;

        public MenuState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/StartGame.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = 500 / 2 - buttonTexture.Width / 2;
                var yPosition = 889 / 2 - buttonTexture.Height * 2;
                newGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                newGameButton.Click += NewgameButton_Click;
            }

            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/RankButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = newGameButton.Position.X;
                var yPosition = newGameButton.Position.Y + 100 + newGameButton.Width;
                hightScoreButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                hightScoreButton.Click += HightScoreButton_Click;
            }

            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/QuitButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = hightScoreButton.Position.X + hightScoreButton.Width + 30;
                var yPosition = hightScoreButton.Position.Y;
                quitGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                quitGameButton.Click += QuitgameButton_Click;
            }

            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/MenuBackground.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var rectangle = new Rectangle(game.Window.Position.X, game.Window.Position.Y, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
                background = new Background(backgroundTexture, rectangle,Color.White, 3, 5, 15);
                background.isHaveAnimation = true;
            }

            // load component
            components = new List<Component>()
            {
                newGameButton,hightScoreButton,quitGameButton
            };

            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = 500;
            graphics.ApplyChanges();
        }

        private void QuitgameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void NewgameButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new TitleState(game, this.graphics, content));
        }

        private void HightScoreButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new HightScoreState(game, this.graphics, content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            background.Draw(gameTime, spriteBatch);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
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
