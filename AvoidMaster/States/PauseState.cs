using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AvoidMaster.States
{
    public class PauseState : GameState
    {
        private List<Component> components;
        GameState gameState;
        private Button musicButton;
        private Button soundButton;
        public PauseState(MainGame game, GraphicsDeviceManager graphics, ContentManager content, GameState gameState) : base(game, graphics, content)
        {
            this.gameState = gameState;
            LoadContent(game, content);
            ChangeMusicButtonTexture();
            ChangeSoundButtonTexuture();
        }

        private void LoadContent(MainGame game, ContentManager content)
        {
           

            Button resumeButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/ResumeGame.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = (game.Window.ClientBounds.Width / 2)-buttonTexture.Width;
                var yPosition = (game.Window.ClientBounds.Height - buttonTexture.Height) /2;
                resumeButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                resumeButton.Click += resumeButton_Click;
            }
            //Main menu button
            Button mainMenuButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/HomeButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = resumeButton.Position.X - resumeButton.Width - 20;
                var yPosition = resumeButton.Position.Y;
                mainMenuButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                mainMenuButton.Click += MainMenuButton_Click;
            }
            // Sounds  button
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/SoundButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = resumeButton.Position.X + resumeButton.Width + 20;
                var yPosition = resumeButton.Position.Y;
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
                var xPosition = soundButton.Position.X + soundButton.Width +20;
                var yPosition = soundButton.Position.Y;
                musicButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                musicButton.Click += MusicButton_Click;
            }
            // Pause text
            var pauseFont = content.Load<SpriteFont>(@"Fonts/Pause");
            var xpausePosition = (GameBoundaries.Width - pauseFont.MeasureString("Paused").X) / 2;
            var ypausePosition = resumeButton.Position.Y - 100;
            var position = new Vector2(xpausePosition, ypausePosition);
            var pauseText = new Text("Paused", pauseFont, position, Color.WhiteSmoke);

            components = new List<Component>(){
                resumeButton,mainMenuButton,pauseText,musicButton,soundButton
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


        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphics, content));
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            ChangeToGameState();
        }

        private void ChangeToGameState()
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
             if (Keyboard.GetState().IsKeyDown(Keys.Space)) ChangeToGameState();
            foreach (var item in components)
            {
                item.Update(gameTime);
            }
            base.Update(gameTime);
        }
    }
}
