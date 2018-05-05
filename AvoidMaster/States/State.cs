using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.States
{
    public abstract class State
    {
        #region fields
        protected ContentManager content;
        protected GraphicsDeviceManager graphics;
        public Rectangle GameBoundaries => new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
        protected MainGame game;
        #endregion

        #region Methods
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void PostUpdate(GameTime gameTime);
        public State(MainGame game, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphics= graphics;
            this.content = content;
        }

        public abstract void Update(GameTime gameTime);
        #endregion
    }
}
