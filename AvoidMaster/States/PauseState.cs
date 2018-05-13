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
    public class PauseState : GameState
    {
        private Button resumeButton;
        private Button mainMenuButton;
        GameState gameState;
        public PauseState(MainGame game, GraphicsDeviceManager graphics, ContentManager content, GameState gameState) : base(game, graphics, content)
        {
            this.gameState = gameState;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/HomeButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition=(game.Window.ClientBounds.Width-buttonTexture.Width*3)/ 2;
                var yPosition = (game.Window.ClientBounds.Height - buttonTexture.Height) / 2;
                mainMenuButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                mainMenuButton.Click += MainMenuButton_Click;
            }

            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/ResumeGame.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition=(game.Window.ClientBounds.Width+buttonTexture.Width)/ 2;
                var yPosition = (game.Window.ClientBounds.Height - buttonTexture.Height) / 2;
                resumeButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                resumeButton.Click += resumeButton_Click;
            }
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphics, content));
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(gameState);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.gameObjects.IsPlaying = false;
            gameState.Draw(gameTime,spriteBatch);
            resumeButton.Draw(gameTime, spriteBatch);
            mainMenuButton.Draw(gameTime, spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            resumeButton.Update(gameTime);
            mainMenuButton.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
