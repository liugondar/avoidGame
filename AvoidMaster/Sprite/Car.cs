using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AvoidMaster.Sprite
{
    public class Car : Sprite
    {
        public CarTypes CarType { get; }
        public enum CarTypes
        {
            BlueCar,
            RedCar
        }
        public Car(CarTypes carType, Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
            CarType = carType;
            Speed = 0.75f;
        }

        public Car(CarTypes carType, Texture2D texture, Vector2 location, Rectangle gameBoundaries, int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, rows, columns, framesPerSecond)
        {
            CarType = carType;
            Speed = 0.75f;
        }



        public override void CheckBounds()
        {
            float xLocation;
            if (CarType == CarTypes.BlueCar)
            {
                xLocation = MathHelper.Clamp(Position.X,
                    0 + Width - 5, (GameBoundaries.Width*(1.5f /4))-Width/2);
                Position = new Vector2(xLocation, Position.Y);
            }
            else
            {
                xLocation = MathHelper.Clamp(Position.X,
                    GameBoundaries.Width / 2 + Width - 5, (GameBoundaries.Width * (3.5f / 4))-Width/2 );
                Position = new Vector2(xLocation, Position.Y);
            }

        }
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (CarType == CarTypes.BlueCar)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    Velocity = new Vector2(-Speed, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    Velocity = new Vector2(Speed, 0);
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    Velocity = new Vector2(-Speed, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    Velocity = new Vector2(Speed, 0);
            }

            base.Update(gameTime, gameObjects);
        }
    }
}
