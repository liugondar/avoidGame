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
    public class GameOverState : State
    {
        public GameState GameState { get; }
        public List<Component> Components { get; set; }
        Texture2D backgroundTexture;
        public GameOverState(MainGame game, GraphicsDeviceManager graphics, ContentManager content, GameState gameState) : base(game, graphics, content)
        {
            GameState = gameState;
            //Restart button
            Button restartButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/RestartButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = (game.Window.ClientBounds.Width - buttonTexture.Width) / 2;
                var yPosition = (game.Window.ClientBounds.Height) / 2 - buttonTexture.Height;
                restartButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                restartButton.Click += restartButton_Click;
            }
            //Home button
            Button HomeButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/HomeButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = (game.Window.ClientBounds.Width - buttonTexture.Width) / 2;
                var yPosition = restartButton.Position.Y + restartButton.Height + 50;
                HomeButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                HomeButton.Click += HomeButton_Click;
            }

            //Rank button
            Button rankButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/RankButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = HomeButton.Position.X - 100;
                var yPosition = HomeButton.Position.Y;
                rankButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                rankButton.Click += rankButton_Click;
            }
            // Game over text
            var gameOverFont = content.Load<SpriteFont>(@"Fonts/Pause");
            var xGameOverPosition = (GameBoundaries.Width - gameOverFont.MeasureString("Game Over").X) / 2;
            var yGameOverPosition = restartButton.Position.Y - 100;
            var gameOverPosition = new Vector2(xGameOverPosition, yGameOverPosition);
            var gameOverText = new Text("Game Over", gameOverFont, gameOverPosition, Color.WhiteSmoke);


            //Background 
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/GameBackGround.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
            };

            Components = new List<Component>(){
                restartButton,gameOverText,HomeButton,rankButton
            };
        }

        private void rankButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new HightScoreState(game, graphics, content));
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphics, content));
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            GameState.gameObjects.IsPause = false;
            GameState.gameObjects.IsPlaying = true;
            game.ChangeState(new GameState(game, graphics, content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            GameState.Draw(gameTime, spriteBatch);
            foreach (var component in GameState.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            foreach (var item in Components)
                item.Draw(gameTime, spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //Old gamestate object update
            GameState.blueCar.Update(gameTime, GameState.gameObjects);
            GameState.redCar.Update(gameTime, GameState.gameObjects);
            GameState.obstacleMangager.Update(gameTime, GameState.gameObjects);
            GameState.carSmokeManager.Update(gameTime, GameState.gameObjects);
            GameState.gameObjects.Update(gameTime);
            GameState.scoreDisplay.Update(gameTime, GameState.gameObjects);

            //Game over component update
            foreach (var item in Components)
                item.Update(gameTime);
        }
    }
}
