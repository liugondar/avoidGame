using AvoidMaster.Sprite;
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
        }

        public Car BlueCar { get; set; }
        public Car RedCar { get; set; }
        public ObstacleMangager ObstacleMangager { get; set; }
    }
}
