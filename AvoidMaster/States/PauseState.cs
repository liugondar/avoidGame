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
        private List<Component> components;
        GameState gameState;
        public PauseState(MainGame game, GraphicsDeviceManager graphics, ContentManager content, GameState gameState) : base(game, graphics, content)
        {
            this.gameState = gameState;
            Button mainMenuButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/HomeButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = (game.Window.ClientBounds.Width - buttonTexture.Width * 3) / 2;
                var yPosition = (game.Window.ClientBounds.Height - buttonTexture.Height) / 2;
                mainMenuButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                mainMenuButton.Click += MainMenuButton_Click;
            }

            Button resumeButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/ResumeGame.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = (game.Window.ClientBounds.Width + buttonTexture.Width) / 2;
                var yPosition = (game.Window.ClientBounds.Height - buttonTexture.Height) / 2;
                resumeButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                resumeButton.Click += resumeButton_Click;
            }
            // Pause text
            var pauseFont = content.Load<SpriteFont>(@"Fonts/Pause");
            var xpausePosition = (GameBoundaries.Width - pauseFont.MeasureString("Paused").X) / 2;
            var ypausePosition = resumeButton.Position.Y - 100;
            var position = new Vector2(xpausePosition, ypausePosition);
            var pauseText = new Text("Paused", pauseFont, position, Color.WhiteSmoke);

            components = new List<Component>(){
                resumeButton,mainMenuButton,pauseText
            };
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphics, content));
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            gameState.gameObjects.IsPause = false;
            gameState.gameObjects.IsPlaying = true;

            game.ChangeState(gameState);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.gameObjects.IsPlaying = false;
            gameState.Draw(gameTime, spriteBatch);
            foreach (var item in components)
            {
                item.Draw(gameTime, spriteBatch);
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var item in components)
            {
                item.Update(gameTime);
            }
            base.Update(gameTime);
        }
    }
}
