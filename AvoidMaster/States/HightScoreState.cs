using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Bus;
using AvoidMaster.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.States
{
    public class HightScoreState : State
    {
        private Background background;
        private List<Component> components;
        private Text title;
        private ScoreManager scoreManager;
        public HightScoreState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            //background
            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/MenuBackground.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var rectangle = new Rectangle(game.Window.Position.X, game.Window.Position.Y, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
                background = new Background(backgroundTexture, rectangle, Color.White, 3, 5, 15);
                background.isHaveAnimation = true;
            }

            //back button
            Button BackMainMenuButton;
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                BackMainMenuButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(20, GameBoundaries.Height - buttonTexture.Height - 20),
                    Text = "Back"
                };
                BackMainMenuButton.Click += BackMainMenuButton_Click;
            }
            //title font load
            var titleFont = content.Load<SpriteFont>(@"Fonts/HightScoreTitle");
            var value = "Hight Score";
            var xPosition = (GameBoundaries.Width - titleFont.MeasureString(value).X) / 2;
            var titlePosition = new Vector2(xPosition, 100);
            title = new Text(value, titleFont, titlePosition, Color.WhiteSmoke);
            //Load score
            scoreManager = ScoreManager.Load();
            var scoreFont = content.Load<SpriteFont>(@"Fonts/ScoreFont");
            var yScorePosition = 100 + title.Height;
            //Add components
            components = new List<Component>(){
                BackMainMenuButton,title
            };
            foreach (var score in scoreManager.HighScores)
            {
                var xScorePosition = (GameBoundaries.Width) / 2 - titleFont.MeasureString(score.Value.ToString()).X;
                yScorePosition += 80;
                var scorePosition = new Vector2(xScorePosition, yScorePosition);
                var tempText = new Text(score.Value.ToString(), scoreFont, scorePosition, Color.White);

                components.Add(tempText);
            }
        }

        private void BackMainMenuButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphics, content));
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
            background.Update(gameTime);
            foreach (var component in components)
                component.Update(gameTime);
        }
    }
}
