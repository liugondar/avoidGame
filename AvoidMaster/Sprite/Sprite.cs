﻿using AvoidMaster.Bus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Sprite
{
    public abstract class Sprite
    {
        public Texture2D Texture { get; set; }
        public int Width => Texture.Width / Columns;
        public int Height => Texture.Height / Rows;
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        public Vector2 Position { get; set; }
        public Rectangle GameBoundaries { get; }
        public float Speed { get; set; }
        protected double timeSinceLastFrame;
        protected int currentFrame;
        public bool isHaveAnimation;
        public int Rows { get; }
        public int Columns { get; }
        public double FramesPerSecond { get; }
        public int TotalFrames { get; private set; }

        public Vector2 Velocity { get; set; }

        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : this(texture, location, gameBoundaries, 1, 1, 1)
        {
        }
        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries,
            int rows, int columns, double framesPerSecond)
        {
            this.Texture = texture;
            this.Position = location;
            GameBoundaries = gameBoundaries;
            Rows = rows;
            Columns = columns;
            FramesPerSecond = framesPerSecond;
            TotalFrames = rows * columns;
            timeSinceLastFrame = 0;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var imageWidth = Texture.Width / Columns;
            var imageHeight = Texture.Height / Rows;

            var currentRow = currentFrame / Columns;
            var currentColumn = currentFrame % Columns;

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
        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (gameObjects.IsPlaying)
            {
                UpdateAnimation(gameTime);
                Position += Velocity;
                CheckBounds();
            }
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

        public abstract void CheckBounds();

    }
}
