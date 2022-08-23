using API.Enums;

namespace API.Models.Ships
{
    public class Carrier : Ship
    {
        public Carrier()
        {
            Type = ShipType.Carrier;
            Size = 5;
        }
    }
}
