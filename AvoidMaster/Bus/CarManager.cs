using AvoidMaster.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Bus
{
    public class CarManager
    {
        public CarManager(Car blueCar, Car redCar)
        {
            BlueCar = blueCar;
            RedCar = redCar;
        }

        public Car BlueCar { get; set; }
        public Car RedCar { get; set; }
    }
}
