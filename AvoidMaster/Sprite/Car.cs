using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Bus;
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
            Speed = 7.5f;
            IsMovingLeft = false;
            IsMovingRight = false;
        }

        public Car(CarTypes carType, Texture2D texture, Vector2 location, Rectangle gameBoundaries, int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, rows, columns, framesPerSecond)
        {
            CarType = carType;
            IsMovingLeft = false;
            IsMovingRight = false;
            Speed = 7.5f;
        }
        public bool IsMovingLeft { get; set; }
        public bool IsMovingRight { get; set; }


        public override void CheckBounds()
        {
            float xLocation;
            if (CarType == CarTypes.BlueCar)
            {
                xLocation = MathHelper.Clamp(Position.X,
                    0, (GameBoundaries.Width * (1.5f / 4)) - Width / 2);
                Position = new Vector2(xLocation, Position.Y);
                if (Position.X == 0)
                {
                    IsMovingLeft = false;
                    IsMovingRight = false;
                }

                if (Position.X == (GameBoundaries.Width * (1.5f / 4)) - Width / 2)
                {
                    IsMovingLeft = false;
                    IsMovingRight = false;
                }
            }
            else
            {
                xLocation = MathHelper.Clamp(Position.X,
                    GameBoundaries.Width / 2, (GameBoundaries.Width * (3.5f / 4)) - Width / 2);
                Position = new Vector2(xLocation, Position.Y);

                if (Position.X == GameBoundaries.Width / 2)
                {
                    IsMovingLeft = false;
                    IsMovingRight = false;
                }

                if (Position.X == (GameBoundaries.Width * (3.5f / 4)) - Width / 2)
                {
                    IsMovingRight = false;
                    IsMovingLeft = false;
                }
            }

        }
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (CarType == CarTypes.BlueCar)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    Velocity = new Vector2(-Speed, 0);
                    
                    IsMovingLeft = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Velocity = new Vector2(Speed, 0);
                    IsMovingRight = true;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    Velocity = new Vector2(-Speed, 0);
                    IsMovingLeft = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    Velocity = new Vector2(Speed, 0);
                    IsMovingRight = true;
                }
            }

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var imageWidth = Texture.Width / Columns;
            var imageHeight = Texture.Height / Rows;

            var currentColumn = 0;
            int currentRow = 0;
            if (IsMovingLeft)
            {
                currentRow = 0;
                currentColumn = currentFrame % Columns;
            }

            if (IsMovingRight)
            {
                currentRow = 1;
                currentColumn = currentFrame % Columns;
            }

            var sourceRectangle = new Rectangle(imageWidth * currentColumn,
                imageHeight * currentRow, imageWidth, imageHeight);

            var destinationRectangle = new Rectangle((int)Position.X,
                (int)Position.Y, imageWidth, imageHeight);
            if (!isHaveAnimation)
            {
                sourceRectangle = new Rectangle(imageWidth * 0, imageHeight * 0, imageWidth, imageHeight);
                destinationRectangle = new Rectangle((int)Position.X,
                (int)Position.Y, imageWidth, imageHeight);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == TotalFrames)
                currentFrame = 0;
        }

        private double SecondsBetweenFrames()
        {
            return 1 / FramesPerSecond;
        }

    }
}
