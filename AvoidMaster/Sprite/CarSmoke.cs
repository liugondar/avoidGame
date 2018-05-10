using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Sprite
{
    public class CarSmoke : Sprite
    {
        public int CarSmokeType { get; set; }
        public enum CarSmokeTypes
        {
            BlueSmoke, RedSmoke
        }
        public CarSmoke(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
        }

        public CarSmoke(Texture2D texture, Vector2 location, Rectangle gameBoundaries, int rows, int columns, double framesPerSecond) : base(texture, location, gameBoundaries, rows, columns, framesPerSecond)
        {
        }

        public override void CheckBounds()
        {
            throw new NotImplementedException();
        }
    }
}
