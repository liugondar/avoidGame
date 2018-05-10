using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Bus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Sprite
{
    public class ObstacleMangager
    {
        private Random random = new Random();
        public List<Obstacle> obstacles = new List<Obstacle>();
        public Rectangle GameBoundaries { get; }
        private double timeSinceLastObastacle;
        public float SpeedCreateObstacle { get; set; }

        private GraphicsDevice graphicsDevice;
        public ObstacleMangager(Rectangle gameBoundaries, GraphicsDevice graphicsDevice)
        {
            GameBoundaries = gameBoundaries;
            this.graphicsDevice = graphicsDevice;
            SpeedCreateObstacle = 1f;
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
                textureObstacle = InitTexture("BlueCircle");
                return new Obstacle((int)ObstacleTypes.BlueCircle,
                    textureObstacle, position, GameBoundaries);
            }

            if (obstacleType == (int)ObstacleTypes.BlueRectangle)
            {
                textureObstacle = InitTexture("BlueRectangle");
                return new Obstacle((int)ObstacleTypes.BlueRectangle,
                    textureObstacle, position, GameBoundaries);
            }

            if (obstacleType == (int)ObstacleTypes.RedCircle)
            {
                textureObstacle = InitTexture("RedCircle");
                return new Obstacle((int)ObstacleTypes.RedCircle,
                    textureObstacle, position, GameBoundaries);
            }

            if (obstacleType == (int)ObstacleTypes.RedRectangle)
            {
                textureObstacle = InitTexture("RedRectangle");
                return new Obstacle((int)ObstacleTypes.RedRectangle,
                    textureObstacle, position, GameBoundaries);
            }

            return new Obstacle((int)ObstacleTypes.BlueCircle,
                    textureObstacle, position, GameBoundaries);
        }

        private Texture2D InitTexture(string textureName)
        {
            Texture2D textureObstacle = null;
            using (var stream = TitleContainer.OpenStream("Content/" + textureName + ".png"))
            {
                textureObstacle = Texture2D.FromStream(this.graphicsDevice, stream);
            }
            return textureObstacle;
        }

        private int RandomObstacleType()
        {
            int result=random.Next(0,1001);
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
            var thirdRoadPositionX = GameBoundaries.Width*(2f / 4) + Width -5;
            var fourthRoadPositionX = (GameBoundaries.Width * (3.5f / 4)) - Width / 2;


            Vector2 finalRandomPosition = new Vector2();
            var result = random.Next(1,3);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obstacle in obstacles)
                obstacle.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime,GameObjects gameObjects)
        {
            if (gameObjects.IsLose||!gameObjects.IsPlaying) return;
            UpdateMoreObstacle(gameTime);
            foreach (var obstacle in obstacles)
                obstacle.Update(gameTime, gameObjects);
        }

        private void UpdateMoreObstacle(GameTime gameTime)
        {
            timeSinceLastObastacle+= gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastObastacle> SecondsBetweenTwoObastacle())
            {
                CreateEnemy();
                timeSinceLastObastacle= 0;
            }
        }

        private double SecondsBetweenTwoObastacle()
        {
            return SpeedCreateObstacle;
        }
    }
}
