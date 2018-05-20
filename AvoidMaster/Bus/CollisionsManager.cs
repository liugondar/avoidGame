using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Bus
{
    public class CollisionsManager
    {
        public SoundManager soundManager { get; set; }
        public CollisionsManager(GameObjects gameObjects, Rectangle gameBoundaries, SoundManager soundManager)
        {
            GameObjects = gameObjects;
            GameBoundaries = gameBoundaries;
            this.soundManager = soundManager;
        }

        public GameObjects GameObjects { get; set; }
        public Rectangle GameBoundaries { get; set; }

        public void Update(GameTime gameTime)
        {
            if(GameObjects.IsPlaying)
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            CheckRectangleImpactBlueCar();
            CheckRectangleImpactRedCar();
            CheckCircleImpactBlueCar();
            CheckCircleImpactRedCar();
        }

        private void CheckCircleImpactRedCar()
        {
            var listsRedCircle = GameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType == (int)ObstacleTypes.RedCircle).ToList();
            foreach (var item in listsRedCircle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.RedCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                    GameObjects.ScoreDisplay.Score.Value++;
                    soundManager.PlayCollisionSound();
                }

            }
        }

        private void CheckCircleImpactBlueCar()
        {
            var listsBlueCircle = GameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType ==(int) ObstacleTypes.BlueCircle).ToList();
            foreach (var item in listsBlueCircle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.BlueCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                    GameObjects.ScoreDisplay.Score.Value++;
                    soundManager.PlayCollisionSound();
                }

            }
        }

        private void CheckRectangleImpactRedCar()
        {
            var listsRedRectangle = GameObjects.ObstacleMangager.obstacles.Where(
               obstacle => obstacle.ObstacleType ==(int) ObstacleTypes.RedRectangle).ToList();
            foreach (var item in listsRedRectangle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.RedCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.IsLose = true;
                    GameObjects.IsPlaying = false;

                    GameObjects.ScoreManager.Add(GameObjects.ScoreDisplay.Score);
                    soundManager.PlayExplosionSound();
                    ScoreManager.Save(GameObjects.ScoreManager);
                }

            }
        }

        private void CheckRectangleImpactBlueCar()
        {
            var listsBlueRectangle = GameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType ==(int) ObstacleTypes.BlueRectangle).ToList();
            foreach (var item in listsBlueRectangle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.BlueCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.IsLose = true;
                    GameObjects.IsPlaying = false;
                    GameObjects.ScoreManager.Add(GameObjects.ScoreDisplay.Score);
                    soundManager.PlayExplosionSound();
                    ScoreManager.Save(GameObjects.ScoreManager);
                }
            }
        }
    }
}
