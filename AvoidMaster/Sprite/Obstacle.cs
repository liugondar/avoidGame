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
       
        public Obstacle(int obstacleType ,float Speed,Texture2D texture, Vector2 location, Rectangle gameBoundaries,Color color) : base(texture, location, gameBoundaries,color,1,1,1)
        {
            ObstacleType = obstacleType;
            this.Speed = Speed;
        }

        public Obstacle(int obstacleType,float Speed,Texture2D texture, Vector2 location, Rectangle gameBoundaries,Color color, int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, color,rows, columns, framesPerSecond)
        {
            ObstacleType = obstacleType;
            this.Speed=Speed;
        }

        public override void CheckBounds()
        {
        }
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Velocity = new Vector2(0,Speed);
            base.Update(gameTime, gameObjects);
        }
    }
}
