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
        public GameObjects(Car blueCar, Car redCar, ObstacleMangager obstacleMangager,Score score)
        {
            BlueCar = blueCar;
            RedCar = redCar;
            ObstacleMangager = obstacleMangager;
            Score = score;
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
        public Score Score { get; set; }
        public void Update(GameTime gameTime)
        {
            CollisionsManager.Update(gameTime);
        }
    }
}
