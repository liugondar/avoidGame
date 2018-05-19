using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Components
{
    public class Button : Component
    {
        #region fields
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previouseMouse;
        private Texture2D texture;
        #endregion

        #region Properties
        public Color PenColor { get; set; }
        public float Width => Texture.Width;
        public float Height => Texture.Height;
        public event EventHandler Click;
        public Vector2 Position { get; set; }
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        public string Text { get; set; }
        public Texture2D Texture { get => texture; set => texture = value; }
        #endregion

        #region Methods
        public Button(Texture2D texture, SpriteFont spriteFont)
        {
            this.Texture = texture;
            this.font = spriteFont;
            PenColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var _color = Color.White;
            if (isHovering)
            {
                _color = Color.Gray;

            }

            spriteBatch.Draw(Texture, Rectangle, _color);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2) + 5;

                spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColor);
            }
        }
        public override void Update(GameTime gameTime)
        {
            this.previouseMouse = currentMouse;
            this.currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            this.isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;
                if (currentMouse.LeftButton == ButtonState.Released &&
                    previouseMouse.LeftButton == ButtonState.Pressed
                    )
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }

        }
        #endregion
    }
}
