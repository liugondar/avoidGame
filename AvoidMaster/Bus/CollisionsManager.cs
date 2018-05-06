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
        public CollisionsManager(GameObjects gameObjects, Rectangle gameBoundaries)
        {
            GameObjects = gameObjects;
            GameBoundaries = gameBoundaries;
        }

        public GameObjects GameObjects { get; set; }
        public Rectangle GameBoundaries { get; set; }

        public void Update(GameTime gameTime)
        {
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
                obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.RedCircle).ToList();
            foreach (var item in listsRedCircle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.RedCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                GameObjects.Score.PlayerScore++;
                }

            }
        }

        private void CheckCircleImpactBlueCar()
        {
            var listsBlueCircle = GameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.BlueCircle).ToList();
            foreach (var item in listsBlueCircle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.BlueCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                    GameObjects.Score.PlayerScore++;
                }

            }
        }

        private void CheckRectangleImpactRedCar()
        {
            var listsRedRectangle = GameObjects.ObstacleMangager.obstacles.Where(
               obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.RedRectangle).ToList();
            foreach (var item in listsRedRectangle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.RedCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                    GameObjects.IsLose = true;
                    GameObjects.IsPlaying = false;
                }
                //TODO: Minus score score & make anitmation collision

            }
        }

        private void CheckRectangleImpactBlueCar()
        {
            var listsBlueRectangle = GameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.BlueRectangle).ToList();
            foreach (var item in listsBlueRectangle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.BlueCar.BoundingBox);
                if (isIntersects)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                    //TODO: Minus score and make animation collision
                    GameObjects.IsLose = true;
                    GameObjects.IsPlaying = false;
                }
            }
        }
    }
}
