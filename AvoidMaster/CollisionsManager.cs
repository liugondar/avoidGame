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
        public CollisionsManager(GameObjects gameObjects)
        {
            GameObjects = gameObjects;
        }

        public GameObjects GameObjects { get; set; }

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
                obstacle => obstacle.ObstacleType == Obstacle.ObstacleTypes.RedCirle).ToList();
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
            var listsRedRectangle= GameObjects.ObstacleMangager.obstacles.Where(
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
            var listsBlueRectangle= GameObjects.ObstacleMangager.obstacles.Where(
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
