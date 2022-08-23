using API.Enums;

namespace API.Models.Ships
{
    public class Submarine : Ship
    {
        public Submarine()
        {
            Type = ShipType.Submarine;
            Size = 3;
        }
    }
}
