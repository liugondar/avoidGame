using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Bus;
using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Bus
{
    public class ObstacleMangager
    {
        private Random random = new Random();
        public List<Obstacle> obstacles = new List<Obstacle>();
        public Rectangle GameBoundaries { get; }
        private double timeSinceLastObstacle;
        public float SpeedObstacle { get; set; }
        public float SpeedCreateObstacle { get; set; }

        private GraphicsDevice graphicsDevice;
        public Color color { get; set; }

        public ObstacleMangager(Rectangle gameBoundaries, GraphicsDevice graphicsDevice, Color color, float SpeedObstacle)
        {
            GameBoundaries = gameBoundaries;
            this.graphicsDevice = graphicsDevice;
            this.color = color;
            SpeedCreateObstacle = 1f;
            this.SpeedObstacle = SpeedObstacle;
            CreateEnemy();
        }

        private void CreateEnemy()
        {
            var obstacleType = RandomObstacleType();
            Obstacle obstacle;
            var position = RandomPosition(obstacleType);
            obstacle = InitObstacle(position, obstacleType);
            obstacles.Add(obstacle);
        }

        private Obstacle InitObstacle(Vector2 position, int obstacleType)
        {
            Texture2D textureObstacle = null;

            if (obstacleType == (int)ObstacleTypes.BlueCircle)
            {
                textureObstacle = InitTexture("BlueCircle.png");
                return new Obstacle((int)ObstacleTypes.BlueCircle, SpeedObstacle,
                    textureObstacle, position, GameBoundaries, this.color);
            }

            if (obstacleType == (int)ObstacleTypes.BlueRectangle)
            {
                textureObstacle = InitTexture("BlueRectangle.png");
                return new Obstacle((int)ObstacleTypes.BlueRectangle, SpeedObstacle,
                    textureObstacle, position, GameBoundaries, this.color);
            }

            if (obstacleType == (int)ObstacleTypes.RedCircle)
            {
                textureObstacle = InitTexture("RedCircle.png");
                return new Obstacle((int)ObstacleTypes.RedCircle, SpeedObstacle,
                    textureObstacle, position, GameBoundaries, this.color);
            }

            if (obstacleType == (int)ObstacleTypes.RedRectangle)
            {
                textureObstacle = InitTexture("RedRectangle.png");
                return new Obstacle((int)ObstacleTypes.RedRectangle, SpeedObstacle,
                    textureObstacle, position, GameBoundaries, this.color);
            }

            return new Obstacle((int)ObstacleTypes.BlueCircle, SpeedObstacle,
                    textureObstacle, position, GameBoundaries, this.color);
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

        private int RandomObstacleType()
        {
            int result = random.Next(0, 1001);
            if (result <= 250)
                return 0;
            if (result <= 500)
                return 1;
            if (result <= 750)
                return 2;
            if (result <= 1000)
                return 3;
            return 0;
        }

        private Vector2 RandomPosition(int obstacleType)
        {
            var Width = 50;
            var firstRoadPositionX = 0 + Width - 5;
            var secondRoadPositionX = (GameBoundaries.Width * (1.5f / 4)) - Width / 2;
            var thirdRoadPositionX = GameBoundaries.Width * (2f / 4) + Width - 5;
            var fourthRoadPositionX = (GameBoundaries.Width * (3.5f / 4)) - Width / 2;


            Vector2 finalRandomPosition = new Vector2();
            var result = random.Next(1, 3);
            if (obstacleType == (int)ObstacleTypes.BlueCircle ||
                obstacleType == (int)ObstacleTypes.BlueRectangle
                )
            {
                if (result == 1) finalRandomPosition = new Vector2(firstRoadPositionX, 0);
                if (result == 2) finalRandomPosition = new Vector2(secondRoadPositionX, 0);
            }
            else
            {
                if (result == 1) finalRandomPosition = new Vector2(thirdRoadPositionX, 0);
                if (result == 2) finalRandomPosition = new Vector2(fourthRoadPositionX, 0);
            }
            return finalRandomPosition;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.color = this.color;
                obstacle.Draw(gameTime, spriteBatch);
            }
        }
        public void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (!gameObjects.IsLose && gameObjects.IsPlaying)
            {
                var enemysCount = random.Next(1, 4);
                for (int i = 0; i < enemysCount; i++)
                {
                UpdateMoreObstacle(gameTime);
                }
            }

            foreach (var obstacle in obstacles)
                obstacle.Update(gameTime, gameObjects);
        }

        private void UpdateMoreObstacle(GameTime gameTime)
        {
            timeSinceLastObstacle += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastObstacle > SecondsBetweenTwoObstacle())
            {
                CreateEnemy();
                timeSinceLastObstacle = 0;
            }
        }

        private double SecondsBetweenTwoObstacle()
        {
            return SpeedCreateObstacle;
        }

    }
}
