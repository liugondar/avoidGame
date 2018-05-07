using AvoidMaster.Models;
using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Bus
{
    public class GameObjects
    {
        public GameObjects(Car blueCar, Car redCar, ObstacleMangager obstacleMangager,ScoreDisplay scoreDisplay,ScoreManager scoreManager)
        {
            BlueCar = blueCar;
            RedCar = redCar;
            ObstacleMangager = obstacleMangager;
            ScoreDisplay = scoreDisplay;
            this.ScoreManager = scoreManager;
            CollisionsManager = new CollisionsManager(this,blueCar.GameBoundaries);
            IsLose = false;
            IsPlaying = true;
        }
        public Car BlueCar { get; set; }
        public Car RedCar { get; set; }
        public ObstacleMangager ObstacleMangager { get; set; }
        public CollisionsManager CollisionsManager { get; set; }
        public bool IsLose { get; set; }
        public bool IsPlaying { get; set; }
        public ScoreDisplay ScoreDisplay { get; set; }
        public ScoreManager ScoreManager { get; set; }
        public void Update(GameTime gameTime)
        {
            CollisionsManager.Update(gameTime);
        }
    }
}
