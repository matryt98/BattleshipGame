using API.Enums;

namespace API.Models.Ships
{
    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Type = ShipType.Destroyer;
            Size = 2;
        }
    }
}
