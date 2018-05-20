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
        private Button musicButton;
        private Button soundButton;
        public GameOverState(MainGame game, GraphicsDeviceManager graphics, ContentManager content, GameState gameState) : base(game, graphics, content)
        {
            GameState = gameState;
            LoadContent(game, content);
            ChangeMusicButtonTexture();
            ChangeSoundButtonTexuture();
        }

        private void LoadContent(MainGame game, ContentManager content)
        {
            // Game over text
            var gameOverFont = content.Load<SpriteFont>(@"Fonts/Pause");
            var xGameOverPosition = (GameBoundaries.Width - gameOverFont.MeasureString("Game Over").X) / 2;
            var yGameOverPosition = (game.Window.ClientBounds.Height) / 6 ;
            var gameOverPosition = new Vector2(xGameOverPosition, yGameOverPosition);
            var gameOverText = new Text("Game Over", gameOverFont, gameOverPosition, Color.WhiteSmoke);
            //Score text
            var scoreTextFont = content.Load<SpriteFont>(@"Fonts/SegoeUIMono25");
            var xScorePosition = gameOverText.Position.X;
            var yScorePosition = gameOverText.Position.Y + gameOverText.Height +20;
            var scorePosition = new Vector2(xScorePosition, yScorePosition);
            var scoreText = new Text("Score: "+GameState.scoreDisplay.Score.Value,scoreTextFont, scorePosition, Color.WhiteSmoke);

            // Hight Score text
           
            var hightscoreTextFont = content.Load<SpriteFont>(@"Fonts/SegoeUIMono25");
            var xhightScorePosition = scoreText.Position.X;
            var yhightScorePosition = scoreText.Position.Y + scoreText.Height + 50;
            var hightscorePosition = new Vector2(xhightScorePosition, yhightScorePosition);
            var hightscoreText = new Text("Hight Score: " + GameState.scoreManager.HighScores[0].Value, hightscoreTextFont, hightscorePosition, Color.WhiteSmoke);

            //Restart button
            Button restartButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/RestartButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = (game.Window.ClientBounds.Width - buttonTexture.Width) / 2;
                var yPosition = hightscoreText.Position.Y + buttonTexture.Height;
                restartButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                restartButton.Click += restartButton_Click;
            }
            //Rank button
            Button rankButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/RankButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = restartButton.Position.X;
                var yPosition = restartButton.Position.Y + restartButton.Height + 50;
                rankButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                rankButton.Click += rankButton_Click;
            }
            //Home button
            Button HomeButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/HomeButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = rankButton.Position.X - rankButton.Width-20;
                var yPosition = rankButton.Position.Y;
                HomeButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                HomeButton.Click += HomeButton_Click;
            }
            // Sounds  button
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/SoundButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = rankButton.Position.X + rankButton.Width +20;
                var yPosition = rankButton.Position.Y;
                soundButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                soundButton.Click += SoundButton_Click;
            }
            // Music background button
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/MusicButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = soundButton.Position.X + soundButton.Width + 20;
                var yPosition = soundButton.Position.Y;
                musicButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                musicButton.Click += MusicButton_Click;
            }

            //Background 
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/GameBackGround.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
            };

            Components = new List<Component>(){
                gameOverText,
                scoreText,hightscoreText,
                restartButton,HomeButton,rankButton,soundButton,musicButton,

            };
        }

        private void SoundButton_Click(object sender, EventArgs e)
        {
            game.soundManager.ChangeStateSound();
            ChangeSoundButtonTexuture();
        }

        private void MusicButton_Click(object sender, EventArgs e)
        {
            game.soundManager.ChangeStateBackgroundSound();
            ChangeMusicButtonTexture();
        }
        private void ChangeSoundButtonTexuture()
        {
            if (game.soundManager != null)
            {
                Texture2D buttonTexture;
                if (game.soundManager.IsSoundPlaying)
                {
                    using (var stream = TitleContainer.OpenStream(@"Content/Buttons/SoundButton.png"))
                    {
                        buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                        soundButton.Texture = buttonTexture;
                    }
                }
                else
                {
                    using (var stream = TitleContainer.OpenStream(@"Content/Buttons/MutedSoundButton.png"))
                    {
                        buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                        soundButton.Texture = buttonTexture;
                    }
                }
            }

        }

        private void ChangeMusicButtonTexture()
        {
            if (game.soundManager != null)
            {
                Texture2D buttonTexture;
                if (game.soundManager.IsBackgroundMusicPlaying)
                {
                    using (var stream = TitleContainer.OpenStream(@"Content/Buttons/MusicButton.png"))
                    {
                        buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                        musicButton.Texture = buttonTexture;
                    }
                }
                else
                {
                    using (var stream = TitleContainer.OpenStream(@"Content/Buttons/MutedMusicButton.png"))
                    {
                        buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                        musicButton.Texture = buttonTexture;
                    }
                }
            }
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
