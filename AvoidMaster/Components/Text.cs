using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Components
{
    public class Text : Component
    {
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

        public string Value { get; set; }
        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; private set; }
        public float Width => Font.MeasureString(Value).X;
        public float Height => Font.MeasureString(Value).Y;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Value, Position, Color);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
