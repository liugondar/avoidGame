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
using System.Windows.Forms;

namespace AvoidMaster.States
{
    public class GameState : State
    {
        private List<Component> components;
        private Background background;
        private Car blueCar;
        private Car redCar;
        private GameObjects gameObjects;
        private ObstacleMangager obstacleMangager;
        private CarSmokeManager carSmokeManager;
        private ScoreDisplay scoreDisplay;
        private ScoreManager scoreManager;
        public GameState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            //Background init
            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream("Content/GameBackGround.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
            }

            game.Window.Position = new Point(game.Window.ClientBounds.Width / 2, 0); // xPos and yPos (pixel)
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = backgroundTexture.Width;
            graphics.ApplyChanges();
            background = new Background(backgroundTexture, GameBoundaries);

            //Blue car init
            using (var stream = TitleContainer.OpenStream("Content/BlueCar.png"))
            {
                var blueCarTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var location = new Vector2(blueCarTexture.Width - 5,
                    GameBoundaries.Height - blueCarTexture.Height);
                blueCar = new Car(Car.CarTypes.BlueCar, blueCarTexture, location, GameBoundaries, 2, 13, 13);
                blueCar.isHaveAnimation = true;
            }

            //Red car init
            using (var stream = TitleContainer.OpenStream("Content/RedCar.png"))
            {
                var redCarTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var location = new Vector2(GameBoundaries.Width / 2 + redCarTexture.Width - 5,
                    GameBoundaries.Height - redCarTexture.Height);
                redCar = new Car(Car.CarTypes.RedCar, redCarTexture, location, GameBoundaries, 2, 13, 13);
                redCar.isHaveAnimation = true;
            }
            //Score init
            scoreDisplay = new ScoreDisplay(content.Load<SpriteFont>("ScoreFont"), GameBoundaries);
            scoreManager = ScoreManager.Load();
            //Init obstacle
            obstacleMangager = new ObstacleMangager(GameBoundaries, graphics.GraphicsDevice);
            //Init smokes
            carSmokeManager = new CarSmokeManager(GameBoundaries, graphics.GraphicsDevice);
            //Init game objects
            gameObjects = new GameObjects(blueCar, redCar, obstacleMangager, scoreDisplay, scoreManager);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(gameTime, spriteBatch);
            blueCar.Draw(spriteBatch);
            redCar.Draw(spriteBatch);
            obstacleMangager.Draw(spriteBatch);
            carSmokeManager.Draw(spriteBatch);
            scoreDisplay.Draw(spriteBatch);
            spriteBatch.End();
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
        }
    }
}
