using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Bus
{

    public class Score
    {
        public Score(SpriteFont font, Rectangle gameBoundaries)
        {
            Font = font;
            GameBoundaries = gameBoundaries;
            PlayerScore = 0;
        }

        public int PlayerScore { get; set; }
        public SpriteFont Font { get; set; }
        public Rectangle GameBoundaries { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            var scoreText = string.Format("Score: {0}", PlayerScore);
            var xPosition = GameBoundaries.Width - 20 - (Font.MeasureString(scoreText).X);
            var yPosition = 10;
            var position = new Vector2(xPosition, yPosition);

            spriteBatch.DrawString(Font, scoreText, position, Color.AliceBlue);
        }
        public void Update(GameTime gameTime, GameObjects gameObjects)
        {
            CheckBounds(gameObjects);
        }

        private void CheckBounds(GameObjects gameObjects)
        {
            CheckRectangleOutOfBottom(gameObjects);
            CheckRedCircleOutOfBottom(gameObjects);
            CheckBlueCircleOutOfBottom(gameObjects);
        }

        private void CheckRectangleOutOfBottom(GameObjects gameObjects)
        {
            var listRectangle = gameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.BlueRectangle ||
                obstacle.ObstacleType == Obstacle.ObstacleTypes.RedRectangle).ToList();

            foreach (var item in listRectangle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                    gameObjects.ObstacleMangager.obstacles.Remove(item);
            }
        }
        private void CheckRedCircleOutOfBottom(GameObjects gameObjects)
        {
            var listCircle = gameObjects.ObstacleMangager.obstacles.Where(
               obstacle => obstacle.ObstacleType ==
               Obstacle.ObstacleTypes.RedCircle).ToList();

            foreach (var item in listCircle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                {
                    gameObjects.ObstacleMangager.obstacles.Remove(item);
                    //TODO: add action when miss circle
                    gameObjects.IsLose = true;
                    gameObjects.IsPlaying = false;
                }
            }
        }

        private void CheckBlueCircleOutOfBottom(GameObjects gameObjects)
        {
            var listCircle = gameObjects.ObstacleMangager.obstacles.Where(
                    obstacle => obstacle.ObstacleType ==
                    Obstacle.ObstacleTypes.BlueCircle).ToList();

            foreach (var item in listCircle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                {
                    gameObjects.ObstacleMangager.obstacles.Remove(item);
                    //TODO: add action when miss circle
                    gameObjects.IsLose = true;
                    gameObjects.IsPlaying = false;
                }
            }

        }

    }
}
