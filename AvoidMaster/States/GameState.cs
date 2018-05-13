using AvoidMaster.Bus;
using AvoidMaster.Components;
using AvoidMaster.Models;
using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.States
{
    public class GameState : State
    {
        protected List<Component> components;
        private Background background;
        protected Car blueCar;
        protected Car redCar;
        protected GameObjects gameObjects;
        protected ObstacleMangager obstacleMangager;
        protected CarSmokeManager carSmokeManager;
        protected ScoreDisplay scoreDisplay;
        protected ScoreManager scoreManager;
        protected Button pauseGameButton;

        public GameState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            //Background init
            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/GameBackGround.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
            }
;
            background = new Background(backgroundTexture, GameBoundaries);

            //Blue car init
            using (var stream = TitleContainer.OpenStream(@"Content/Image/BlueCar.png"))
            {
                var blueCarTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var location = new Vector2(blueCarTexture.Width - 5,
                    GameBoundaries.Height - blueCarTexture.Height);
                blueCar = new Car(Car.CarTypes.BlueCar, blueCarTexture, location, GameBoundaries, 2, 13, 13);
                blueCar.isHaveAnimation = true;
            }

            //Red car init
            using (var stream = TitleContainer.OpenStream(@"Content/Image/RedCar.png"))
            {
                var redCarTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var location = new Vector2(GameBoundaries.Width / 2 + redCarTexture.Width - 5,
                    GameBoundaries.Height - redCarTexture.Height);
                redCar = new Car(Car.CarTypes.RedCar, redCarTexture, location, GameBoundaries, 2, 13, 13);
                redCar.isHaveAnimation = true;
            }
            //Score init

            scoreDisplay = new ScoreDisplay(content.Load<SpriteFont>(@"Fonts/ScoreFont"), GameBoundaries);
            scoreManager = ScoreManager.Load();
            //Init obstacle
            obstacleMangager = new ObstacleMangager(GameBoundaries, graphics.GraphicsDevice);
            //Init smokes
            carSmokeManager = new CarSmokeManager(GameBoundaries, graphics.GraphicsDevice);
            //Init game objects
            gameObjects = new GameObjects(blueCar, redCar, obstacleMangager, scoreDisplay, scoreManager, carSmokeManager);
            // Pause game components
            using (var stream = TitleContainer.OpenStream(@"Content/Buttons/PauseGame.png"))
            {
                var buttonFont = content.Load<SpriteFont>(@"Fonts/Font");
                var buttonTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                pauseGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(20, 20) 
                };
                pauseGameButton.Click += PauseGameText_Click;
            }

            // Load component;
            components = new List<Component>(){
                pauseGameButton
            };

     
        }

        private void PauseGameText_Click(object sender, EventArgs e)
        {
            game.ChangeState(new PauseState(game, graphics, content,this));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            background.Draw(gameTime, spriteBatch);
            blueCar.Draw(gameTime, spriteBatch);
            redCar.Draw(gameTime, spriteBatch);
            obstacleMangager.Draw(gameTime, spriteBatch);
            carSmokeManager.Draw(gameTime, spriteBatch);
            scoreDisplay.Draw(spriteBatch);
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
                blueCar.Update(gameTime, gameObjects);
                redCar.Update(gameTime, gameObjects);
                obstacleMangager.Update(gameTime, gameObjects);
                carSmokeManager.Update(gameTime, gameObjects);
                gameObjects.Update(gameTime);
                scoreDisplay.Update(gameTime, gameObjects);
                foreach (var Component in components)
                {
                    Component.Update(gameTime);
                }
        }
           
    }
}
