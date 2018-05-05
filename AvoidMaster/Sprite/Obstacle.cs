using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Sprite
{
    public class Obstacle : Sprite
    {
        public ObstacleTypes ObstacleType { get; }
        public enum ObstacleTypes
        {
            BlueCirle,
            BlueRectangle,
            RedCirle,
            RedRectangle
        }
        public Obstacle(ObstacleTypes obstacleType ,Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
            ObstacleType = obstacleType;
            Velocity = new Vector2(0,7.5f);
        }

        public Obstacle(ObstacleTypes obstacleType,Texture2D texture, Vector2 location, Rectangle gameBoundaries, int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, rows, columns, framesPerSecond)
        {
            ObstacleType = obstacleType;
            Velocity = new Vector2(0,7.5f);
        }

        public override void CheckBounds()
        {
        }
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            base.Update(gameTime, gameObjects);
        }
    }
}
