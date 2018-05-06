using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster
{
    public class GameObjects
    {
        public GameObjects(Car blueCar, Car redCar, ObstacleMangager obstacleMangager)
        {
            BlueCar = blueCar;
            RedCar = redCar;
            ObstacleMangager = obstacleMangager;
            CollisionsManager = new CollisionsManager(this,blueCar.GameBoundaries);
        }

        public Car BlueCar { get; set; }
        public Car RedCar { get; set; }
        public ObstacleMangager ObstacleMangager { get; set; }
        public CollisionsManager CollisionsManager { get; set; }
        public void Update(GameTime gameTime)
        {
            CollisionsManager.Update(gameTime);
        }
    }
}
