using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvoidMaster.Bus
{
    public class ExplosionManager
    {
        private Texture2D redExplosionTexture;
        private Texture2D blueExplosionTexture;
        private Rectangle bounds;
        private List<Explosion> explosions;

        public Color colour { get; set; }

        public ExplosionManager(Texture2D redExplosionTexture, Texture2D blueExplosionTexture, Rectangle bounds)
        {
            this.redExplosionTexture = redExplosionTexture;
            this.blueExplosionTexture = blueExplosionTexture;
            this.bounds = bounds;
            explosions = new List<Explosion>();
        }

        internal void CreateExposion(Obstacle sprite)
        {
            var centerOfSprite = new Vector2(sprite.Position.X + (sprite.Width / 2),
                  sprite.Position.Y + (sprite.Height / 2));
            Explosion explosion;
            if (sprite.ObstacleType == (int)ObstacleTypes.RedRectangle)
            {
                explosion = new Explosion(redExplosionTexture, centerOfSprite, bounds, colour, 5, 3, 15);
                explosion.Position -= new Vector2(explosion.Width / 2, explosion.Height / 2);
                explosion.isHaveAnimation = true;
                explosion.color = Color.White * 0.2f;
                explosions.Add(explosion);
            }
            else
            {
                explosion = new Explosion(blueExplosionTexture, centerOfSprite, bounds, colour, 4, 3, 12);
                explosion.Position -= new Vector2(explosion.Width / 2, explosion.Height / 2);
                explosion.isHaveAnimation = true;
                explosion.color = Color.White * 0.2f;
                explosions.Add(explosion);
            }
           
        }

        internal void Update(GameTime gameTime,GameObjects gameObjects)
        {
            if(explosions.Count>0)
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Update(gameTime,gameObjects);
                if (explosions[i].IsDone())
                    explosions.Remove(explosions[i]);
            }
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var explosion in explosions)
            {
                explosion.Draw(gameTime, spriteBatch);
            }
        }
    }
}
