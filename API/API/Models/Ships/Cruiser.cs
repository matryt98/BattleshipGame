using API.Enums;

namespace API.Models.Ships
{
    public class Cruiser : Ship
    {
        public Cruiser()
        {
            Type = ShipType.Cruiser;
            Size = 3;
        }
    }
}
