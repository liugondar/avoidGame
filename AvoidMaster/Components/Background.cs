using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Components
{
    class Background:Component
    {
        #region fields
        private Texture2D texture;
        private Rectangle gameBoundaries;
        #endregion

        public Background(Texture2D texture, Rectangle gameBoundaries)
        {
            this.texture = texture;
            this.gameBoundaries = gameBoundaries;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, gameBoundaries, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
