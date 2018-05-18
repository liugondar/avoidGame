using AvoidMaster.Bus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Components
{
    public class Background : Component
    {
        #region fields
        private Texture2D Texture;
        public int Width => Texture.Width / Columns;
        public int Height => Texture.Height / Rows;
        private Rectangle gameBoundaries;
        protected double timeSinceLastFrame;
        protected int currentFrame;
        public bool isHaveAnimation;

        public Color Color { get; set; }
        public int Rows { get; }
        public int Columns { get; }
        public double FramesPerSecond { get; }
        public int TotalFrames { get; private set; }
        #endregion

        public Background(Texture2D texture, Rectangle gameBoundaries,Color color, int Rows, int Columns,
        int FramesPerSecond)
        {
            this.Texture = texture;
            this.gameBoundaries = gameBoundaries;
            Color = color;
            this.Rows = Rows;
            this.Columns = Columns;
            this.FramesPerSecond = FramesPerSecond;
            TotalFrames = Rows * Columns;
            timeSinceLastFrame = 0;
        }
        public Background(Texture2D texture, Rectangle gameBoundaries,Color color) : this(texture, gameBoundaries,color, 1, 1, 1) { }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var imageWidth = Texture.Width / Columns;
            var imageHeight = Texture.Height / Rows;

            var currentRow = currentFrame / Columns;
            var currentColumn = currentFrame % Columns;

            var sourceRectangle = new Rectangle(imageWidth * currentColumn,
                imageHeight * currentRow, imageWidth, imageHeight);

            var destinationRectangle = new Rectangle(0,
              0, imageWidth, imageHeight);
            if (!isHaveAnimation)
            {
                sourceRectangle = new Rectangle(imageWidth * 0, imageHeight * 0, imageWidth, imageHeight);
                destinationRectangle = new Rectangle(0,
                0, imageWidth, imageHeight);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color);
            }
            else
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color);
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

        public override void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
        }
    }
}
