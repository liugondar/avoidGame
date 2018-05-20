using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Bus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Sprite
{
    public class Explosion:Sprite
    {
        public Explosion(Texture2D texture, Vector2 location, Rectangle gameBoundaries, Color color) : base(texture, location, gameBoundaries, color)
        {
        }

        public Explosion(Texture2D texture, Vector2 location, Rectangle gameBoundaries, Color color, int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, color, rows, columns, framesPerSecond)
        {
        }

        protected Explosion(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
        }

        public override void CheckBounds()
        {
        }

        internal bool IsDone()
        {
            return animationPlayedOnce;
        }
       
    }
}
