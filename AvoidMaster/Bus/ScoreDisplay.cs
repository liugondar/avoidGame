using AvoidMaster.Models;
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
    public class ScoreDisplay
    {
        public ScoreDisplay(SpriteFont font, Rectangle gameBoundaries, Color color)
        {
            Font = font;
            GameBoundaries = gameBoundaries;
            Color = color;
            Score = new Score();
        }

        public SpriteFont Font { get; set; }
        public Rectangle GameBoundaries { get; set; }
        public Color Color { get; set; }
        public Score Score { get; set; }
        public int ScoreInLevel { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            var scoreText = Score.Value.ToString();
            var xPosition = GameBoundaries.Width - 20 - (Font.MeasureString(scoreText).X);
            var yPosition = 10;
            var position = new Vector2(xPosition, yPosition);

            spriteBatch.DrawString(Font, scoreText, position, Color.AliceBlue);
        }
        public void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (gameObjects.IsPlaying)
            {
                CheckBounds(gameObjects);
                UpdateSpeed(gameObjects);
            }
        }

        private void UpdateSpeed(GameObjects gameObjects)
        {
                ScoreInLevel = Score.Value % 100;
            if (ScoreInLevel == 20)
            {
            }
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
                obstacle => obstacle.ObstacleType == (int)ObstacleTypes.BlueRectangle ||
                obstacle.ObstacleType == (int)ObstacleTypes.RedRectangle).ToList();

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
               (int)ObstacleTypes.RedCircle).ToList();

            foreach (var item in listCircle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                {
                    gameObjects.ObstacleMangager.obstacles.Remove(item);
                    //TODO: add action when miss circle
                    gameObjects.IsLose = true;
                    gameObjects.IsPlaying = false;
                    gameObjects.ScoreManager.Add(gameObjects.ScoreDisplay.Score);
                    ScoreManager.Save(gameObjects.ScoreManager);
                }
            }
        }

        private void CheckBlueCircleOutOfBottom(GameObjects gameObjects)
        {
            var listCircle = gameObjects.ObstacleMangager.obstacles.Where(
                    obstacle => obstacle.ObstacleType ==
                    (int)ObstacleTypes.BlueCircle).ToList();

            foreach (var item in listCircle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                {
                    gameObjects.ObstacleMangager.obstacles.Remove(item);
                    //TODO: add action when miss circle
                    gameObjects.IsLose = true;
                    gameObjects.IsPlaying = false;

                    gameObjects.ScoreManager.Add(gameObjects.ScoreDisplay.Score);
                    ScoreManager.Save(gameObjects.ScoreManager);
                }
            }

        }
    }
}
