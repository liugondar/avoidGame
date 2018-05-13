using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Bus
{
    public class CarSmokeManager
    {
        private Random random = new Random();
        public List<CarSmoke> CarSmokes = new List<CarSmoke>();
        public Rectangle GameBoundaries { get; }
        private double timeSinceLastObstacle;
        public float SpeedCreateObstacle { get; set; }

        private GraphicsDevice graphicsDevice;

        public CarSmokeManager(Rectangle gameBoundaries, GraphicsDevice graphicsDevice)
        {
            SpeedCreateObstacle = 0.1f;
            GameBoundaries = gameBoundaries;
            this.graphicsDevice = graphicsDevice;
        }
        private void InitSmokes(GameObjects gameObjects, int smokeType)
        {
            Vector2 position = InitPosition(smokeType, gameObjects);
            CarSmoke carSmoke = InitCarSmokeObject(smokeType, position);
            CarSmokes.Add(carSmoke);
        }

        private CarSmoke InitCarSmokeObject(int smokeType, Vector2 position)
        {
            Texture2D smokeTexture = InitTexture("BlueSmoke.png");

            if (smokeType == (int)CarSmokeTypes.BlueSmoke)
                smokeTexture = InitTexture("BlueSmoke.png");
            if (smokeType == (int)CarSmokeTypes.RedSmoke)
                smokeTexture = InitTexture("RedSmoke.png");
            return new CarSmoke((int)smokeType, smokeTexture, position, GameBoundaries, 1, 5, 15);
        }

        private Vector2 InitPosition(int smokeType, GameObjects gameObjects)
        {
            Vector2 finalPosition = new Vector2();
            if (smokeType == (int)CarSmokeTypes.BlueSmoke)
            {
                var xPosition = (gameObjects.BlueCar.Position.X + gameObjects.BlueCar.BoundingBox.Width / 2);
                var yPosition = (gameObjects.BlueCar.Position.Y + gameObjects.BlueCar.BoundingBox.Height);
                finalPosition = new Vector2(xPosition, yPosition);
            }
            if (smokeType == (int)CarSmokeTypes.RedSmoke)
            {
                var xPosition = (gameObjects.RedCar.Position.X + gameObjects.RedCar.BoundingBox.Width / 2);
                var yPosition = (gameObjects.RedCar.Position.Y + gameObjects.RedCar.BoundingBox.Height);
                finalPosition = new Vector2(xPosition, yPosition);

            }
            return finalPosition;
        }

        private Texture2D InitTexture(string textureName)
        {
            Texture2D textureObstacle = null;
            using (var stream = TitleContainer.OpenStream(@"Content/Image/" + textureName))
            {
                textureObstacle = Texture2D.FromStream(this.graphicsDevice, stream);
            }
            return textureObstacle;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var smoke in CarSmokes)
                smoke.Draw(gameTime, spriteBatch);
        }
        public void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (!gameObjects.IsLose && gameObjects.IsPlaying)
            {
                UpdateMoreObstacle(gameTime, gameObjects);
            }
            for (int i = 0; i < CarSmokes.Count; i++)
            {
                CarSmokes[i].Update(gameTime, gameObjects);
            }
        }

        private void UpdateMoreObstacle(GameTime gameTime, GameObjects gameObjects)
        {
            timeSinceLastObstacle += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastObstacle > SecondsBetweenTwoObstacle())
            {
                InitSmokes(gameObjects, (int)CarSmokeTypes.BlueSmoke);
                InitSmokes(gameObjects, (int)CarSmokeTypes.RedSmoke);
                timeSinceLastObstacle = 0;
            }
        }

        private double SecondsBetweenTwoObstacle()
        {
            return SpeedCreateObstacle;
        }
    }
}
