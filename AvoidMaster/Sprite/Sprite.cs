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

        protected bool animationPlayedOnce;
        public bool isHaveAnimation;
        private Vector2 location;

        public int Rows { get; }
        public int Columns { get; }
        public double FramesPerSecond { get; }
        public int TotalFrames { get; private set; }

        public Vector2 Velocity { get; set; }
        public Color color { get; set; }

        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries,Color color) : this(texture, location, gameBoundaries, color, 1, 1, 1)
        {
        }
        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries, Color color,
            int rows, int columns, double framesPerSecond)
        {
            this.Texture = texture;
            this.Position = location;
            GameBoundaries = gameBoundaries;
            Rows = rows;
            Columns = columns;
            this.color = color;
            FramesPerSecond = framesPerSecond;
            TotalFrames = rows * columns;
            timeSinceLastFrame = 0;
        }

        protected Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries)
        {
            Texture = texture;
            this.location = location;
            GameBoundaries = gameBoundaries;
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
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
            }
            else
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);

        }
        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (gameObjects.IsPlaying)
            {
                Position += Velocity;
                CheckBounds();
            }
            UpdateAnimation(gameTime);
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
            {
                currentFrame = 0;
                animationPlayedOnce = true;
            }
        }

        private double SecondsBetweenFrames()
        {
            return 1 / FramesPerSecond;
        }

        public abstract void CheckBounds();

    }
}
