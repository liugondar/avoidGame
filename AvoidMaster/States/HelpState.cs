using AvoidMaster.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AvoidMaster.States
{
    public class HelpState : State
    {
        Background avoidSquare;
        Background doNotMiss;
        Background move;
        Background resumeGame;
        Background takeCircle;
        Background background;
        List<Component> components;

        int page;
        public HelpState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            LoadContent();
        }
        /// <summary>
        /// load 5 image background for 5 pages
        /// load back and next button
        /// </summary>
        private void LoadContent()
        {
            //Move page 1
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/Help/Move.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                move = new Background(texture, GameBoundaries, Color.White);
            }
            // do not miss circle page 2
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/Help/DoNotMiss.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                doNotMiss = new Background(texture, GameBoundaries, Color.White);
            }

            //Take circle page 3
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/Help/TakeCircle.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                takeCircle = new Background(texture, GameBoundaries, Color.White);
            }

            //avoid square page 4
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/Help/AvoidRectangle.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                avoidSquare = new Background(texture, GameBoundaries, Color.White);
            }
            // how to resume Game page 5
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/Help/ResumeGame.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                resumeGame = new Background(texture, GameBoundaries, Color.White);
            }
            //Init backround=how to move 
            background = move;
            page = 1;
            Button nextButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/NextButton.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                nextButton = new Button(texture);
                nextButton.Position = new Vector2(GameBoundaries.Width - 20 - texture.Width, GameBoundaries.Height / 2 - texture.Height / 2);
            }
            nextButton.Click += NextButton_Click;

            Button backButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/BackButton.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                backButton = new Button(texture);
                backButton.Position = new Vector2(20, nextButton.Position.Y);
            }
            backButton.Click += BackButton_Click;

            Button homeButton; 
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/HomeButton.png"))
            {
                var texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                homeButton= new Button(texture);
                homeButton.Position = new Vector2(GameBoundaries.Width/2-texture.Width/2, GameBoundaries.Height-20-texture.Height);
            }
            homeButton.Click += HomeButton_Click;

            components = new List<Component>()
            {
                nextButton,backButton,homeButton
            };

        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphics, content));
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (page == 1)
            {
                background = resumeGame;
                page = 5;
            }
            else
            {
                if (page == 2) background = move;
                if (page == 3) background = doNotMiss;
                if (page == 4) background = takeCircle;
                if (page == 5) background = avoidSquare;
                page--;

            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (page == 5)
            {
                background = move;
                page = 1;
            }
            else
            {
                if (page == 1) background = doNotMiss;
                if (page == 2) background = takeCircle;
                if (page == 3) background = avoidSquare;
                if (page == 4) background = resumeGame;
                page++;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            background.Draw(gameTime, spriteBatch);
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
        }
    }
}