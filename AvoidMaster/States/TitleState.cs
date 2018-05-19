using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AvoidMaster.States
{
    public class TitleState : State
    {
        public MainGame Game { get; }
        public GraphicsDeviceManager Graphics { get; }
        public ContentManager Content { get; }
        private Background Background { get; set; }
        public TitleState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            Game = game;
            Graphics = graphics;
            Content = content;
            using (var stream = TitleContainer.OpenStream(@"Content/Backgrounds/TitleScreen.png"))
            {
                var Font = content.Load<SpriteFont>(@"Fonts/Font");
                var Texture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                Background = new Background(Texture, GameBoundaries, Color.White,1, 4, 8);
                Background.isHaveAnimation = true;
            }
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = Background.Width;
            graphics.ApplyChanges();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Background.Draw(gameTime, spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                game.ChangeState(new GameState(game, graphics, content));

            Background.Update(gameTime);
        }
    }
}
