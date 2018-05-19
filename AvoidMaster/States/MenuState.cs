using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace AvoidMaster.States
{
    class MenuState : State
    {
        private List<Component> components;
        private Background background;
        private Button newGameButton;
        private Button hightScoreButton;
        private Button quitGameButton;
        private Button musicButton;
        private Button soundButton;

        public MenuState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            LoadContent(game, content);

            ChangeMusicButtonTexture();
            ChangeSoundButtonTexuture();

            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = 500;
            graphics.ApplyChanges();
        }


        private void LoadContent(MainGame game, ContentManager content)
        {
            //Start game button
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/StartGame.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = 500 / 2 - buttonTexture.Width / 2;
                var yPosition = 889 / 2 - buttonTexture.Height * 2 + 100;
                newGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                newGameButton.Click += NewgameButton_Click;
            }

            // Sounds  button
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/SoundButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = newGameButton.Position.X + newGameButton.Width / 2;
                var yPosition = newGameButton.Position.Y + 50 + newGameButton.Width;
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
                var xPosition = soundButton.Position.X - soundButton.Width - 20;
                var yPosition = soundButton.Position.Y;
                musicButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                musicButton.Click += MusicButton_Click;
            }
            // Hight score button
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/RankButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = musicButton.Position.X - musicButton.Width - 20;
                var yPosition = soundButton.Position.Y;
                hightScoreButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                hightScoreButton.Click += HightScoreButton_Click;
            }

            //Quit Button
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/QuitButton.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var xPosition = soundButton.Position.X + soundButton.Width + 20;
                var yPosition = soundButton.Position.Y;
                quitGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPosition),
                };
                quitGameButton.Click += QuitgameButton_Click;
            }

            // Back ground
            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/MenuBackground.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var rectangle = new Rectangle(game.Window.Position.X, game.Window.Position.Y, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
                background = new Background(backgroundTexture, rectangle, Color.White, 3, 5, 15);
                background.isHaveAnimation = true;
            }

            // load component
            components = new List<Component>()
            {
                newGameButton,
                soundButton,musicButton,
                hightScoreButton,quitGameButton
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
