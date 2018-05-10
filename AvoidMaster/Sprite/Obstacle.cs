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
    public enum ObstacleTypes
    {
        BlueCircle,
        BlueRectangle,
        RedCircle,
        RedRectangle
    }
    public class Obstacle : Sprite
    {
        public int ObstacleType { get; }
       
        public Obstacle(int obstacleType ,Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
            ObstacleType = obstacleType;
            Speed = 7.5f;
            Velocity = new Vector2(0,Speed);
        }

        public Obstacle(int obstacleType,Texture2D texture, Vector2 location, Rectangle gameBoundaries, int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, rows, columns, framesPerSecond)
        {
            ObstacleType = obstacleType;
            Speed = 7.5f;
            Velocity = new Vector2(0,Speed);
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
