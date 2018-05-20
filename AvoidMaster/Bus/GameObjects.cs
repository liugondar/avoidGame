using AvoidMaster.Components;
using AvoidMaster.Models;
using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Bus
{
    public class GameObjects
    {
        public GameObjects(Car blueCar, Car redCar, ObstacleMangager obstacleMangager,ScoreDisplay scoreDisplay,ScoreManager scoreManager,CarSmokeManager carSmokeManager, SoundManager soundManager, ExplosionManager explosionManager)
        {
            BlueCar = blueCar;
            RedCar = redCar;
            ObstacleMangager = obstacleMangager;
            ScoreDisplay = scoreDisplay;
            this.ScoreManager = scoreManager;
            this.CarSmokeManager = carSmokeManager;
            ExplosionManager = explosionManager;
            CollisionsManager = new CollisionsManager(this,blueCar.GameBoundaries,soundManager,explosionManager);
            IsLose = false;
            IsPlaying = true;
            IsPause = false;
        }
        public Car BlueCar { get; set; }
        public Car RedCar { get; set; }
        public ObstacleMangager ObstacleMangager { get; set; }
        public CollisionsManager CollisionsManager { get; set; }
        public bool IsLose { get; set; }
        public bool IsPause{ get; set; }
        public bool IsPlaying { get; set; }
        public ScoreDisplay ScoreDisplay { get; set; }
        public ScoreManager ScoreManager { get; set; }
        public CarSmokeManager CarSmokeManager { get; set; }
        public ExplosionManager ExplosionManager { get; }

        public void Update(GameTime gameTime)
        {
            BlueCar.Update(gameTime, this);
            RedCar.Update(gameTime, this);
            ObstacleMangager.Update(gameTime, this);
            CarSmokeManager.Update(gameTime, this);
            ScoreDisplay.Update(gameTime, this);
            CollisionsManager.Update(gameTime);
            ExplosionManager.Update(gameTime,this);
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            BlueCar.Draw(gameTime, spriteBatch);
            RedCar.Draw(gameTime, spriteBatch);
            ObstacleMangager.Draw(gameTime, spriteBatch);
            CarSmokeManager.Draw(gameTime, spriteBatch);
            ScoreDisplay.Draw(spriteBatch);
            ExplosionManager.Draw(gameTime,spriteBatch);
        }
    }
}
