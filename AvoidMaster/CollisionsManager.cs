using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster
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
            CheckBounds();
        }

        private void CheckBounds()
        {
            CheckRectangleOutOfBottom();
            CheckRedCircleOutOfBottom();
            CheckBlueCircleOutOfBottom();
        }

        private void CheckRectangleOutOfBottom()
        {
            var listRectangle = GameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.BlueRectangle ||
                obstacle.ObstacleType == Obstacle.ObstacleTypes.RedRectangle).ToList();

            foreach (var item in listRectangle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
            }
        }
        private void CheckRedCircleOutOfBottom()
        {
            var listCircle = GameObjects.ObstacleMangager.obstacles.Where(
               obstacle => obstacle.ObstacleType ==
               Obstacle.ObstacleTypes.RedCircle).ToList();

            foreach (var item in listCircle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                    //TODO: add action when miss circle
                }
            }
        }

        private void CheckBlueCircleOutOfBottom()
        {
            var listCircle = GameObjects.ObstacleMangager.obstacles.Where(
                    obstacle => obstacle.ObstacleType ==
                    Obstacle.ObstacleTypes.BlueCircle).ToList();

            foreach (var item in listCircle)
            {
                if (item.Position.Y > GameBoundaries.Height)
                {
                    GameObjects.ObstacleMangager.obstacles.Remove(item);
                    //TODO: add action when miss circle
                }
            }

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
                if (isIntersects) GameObjects.ObstacleMangager.obstacles.Remove(item);
                //TODO: Add score 
            }
        }

        private void CheckCircleImpactBlueCar()
        {
            var listsBlueCircle = GameObjects.ObstacleMangager.obstacles.Where(
                obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.BlueCircle).ToList();
            foreach (var item in listsBlueCircle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.BlueCar.BoundingBox);
                if (isIntersects) GameObjects.ObstacleMangager.obstacles.Remove(item);
                //TODO: Add score 
            }
        }

        private void CheckRectangleImpactRedCar()
        {
            var listsRedRectangle = GameObjects.ObstacleMangager.obstacles.Where(
               obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.RedRectangle).ToList();
            foreach (var item in listsRedRectangle)
            {
                bool isIntersects = item.BoundingBox.Intersects(GameObjects.RedCar.BoundingBox);
                if (isIntersects) GameObjects.ObstacleMangager.obstacles.Remove(item);
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
                if (isIntersects) GameObjects.ObstacleMangager.obstacles.Remove(item);
                //TODO: Minus score and make animation collision
            }
        }
    }
}
