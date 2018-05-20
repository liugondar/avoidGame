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
        public SoundManager soundManager { get; set; }
        public ScoreDisplay(SpriteFont font, Rectangle gameBoundaries, Color color, SoundManager soundManager)
        {
            Font = font;
            GameBoundaries = gameBoundaries;
            Color = color;
            Score = new Score();
            this.soundManager = soundManager;
        }

        public SpriteFont Font { get; set; }
        public Rectangle GameBoundaries { get; set; }
        public Color Color { get; set; }
        public Score Score { get; set; }
        public bool IsLevel1{ get; set; }
        public bool IsLevel2{ get; set; }
        public bool IsLevel3{ get; set; }
        public bool IsLevel4{ get; set; }

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
            if (Score.Value==10&&!IsLevel1)
            {
                gameObjects.ObstacleMangager.SpeedObstacle += 1f;
                IsLevel1 = true;
            }

            if (Score.Value==20&&!IsLevel2)
            {
                gameObjects.ObstacleMangager.SpeedObstacle += 1.5f;
                gameObjects.ObstacleMangager.SpeedCreateObstacle -= 0.2f;
                IsLevel2 = true;
            }
            if (Score.Value==50&&!IsLevel3)
            {
                gameObjects.ObstacleMangager.SpeedObstacle += 1f;
                gameObjects.ObstacleMangager.SpeedCreateObstacle -= 0.2f;
                IsLevel3 = true;
            }
            if (Score.Value==100&&!IsLevel4)
            {
                gameObjects.ObstacleMangager.SpeedObstacle += 2f;
                gameObjects.ObstacleMangager.SpeedCreateObstacle -= 0.2f;
                IsLevel4 = true;
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
                if (item.Position.Y+item.Height > GameBoundaries.Height)
                {
                    item.isHaveAnimation = true;

                    soundManager.PlayMissCircle(); 
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
                if (item.Position.Y+item.Height > GameBoundaries.Height)
                {
                    item.isHaveAnimation = true;
                    soundManager.PlayMissCircle(); 
                    gameObjects.IsLose = true;
                    gameObjects.IsPlaying = false;

                    gameObjects.ScoreManager.Add(gameObjects.ScoreDisplay.Score);
                    ScoreManager.Save(gameObjects.ScoreManager);
                }
            }

        }
    }
}
