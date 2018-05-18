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
    public enum CarSmokeTypes
    {
        BlueSmoke, RedSmoke
    }
    public class CarSmoke : Sprite
    {
        public int CarSmokeType { get; set; }
        public CarSmoke(int CarSmokeType, Texture2D texture, Vector2 location, Rectangle gameBoundaries,Color color) : base(texture, location, gameBoundaries,color)
        {
            this.CarSmokeType = CarSmokeType;
            Speed = 9.5f;
            Velocity = new Vector2(0, Speed);
        }

        public CarSmoke(int CarSmokeType, Texture2D texture, Vector2 location, Rectangle gameBoundaries, Color color,int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, color,rows, columns, framesPerSecond)
        {
            this.CarSmokeType = CarSmokeType;
            Speed = 9.5f;
            Velocity = new Vector2(0, Speed);
        }

        public override void CheckBounds()
        {
        }
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (!gameObjects.IsPlaying || gameObjects.IsLose) return;
            if (Position.Y > GameBoundaries.Height) gameObjects.CarSmokeManager.CarSmokes.Remove(this);
            base.Update(gameTime, gameObjects);
        }
    }
}
