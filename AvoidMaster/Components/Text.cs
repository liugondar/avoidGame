using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AvoidMaster.Components
{
    public class Text : Component
    {
        #region fields
        private MouseState currentMouse;
        private bool isHovering;
        #endregion

        public event EventHandler Click;

        public string Value { get; set; }
        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; private set; }
        public float Width => Font.MeasureString(Value).X;
        public float Height => Font.MeasureString(Value).Y;
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);

        public MouseState PreviousMouse { get; set; }

        public Text()
        {
        }

        public Text(string value, SpriteFont font, Vector2 position, Color color)
        {
            Value = value;
            Font = font;
            Position = position;
            Color = color;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Value, Position, Color);
        }

        public override void Update(GameTime gameTime)
        {
            this.PreviousMouse = currentMouse;
            this.currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            this.isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;
                if (currentMouse.LeftButton == ButtonState.Released &&
                    PreviousMouse.LeftButton == ButtonState.Pressed
                    )
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
