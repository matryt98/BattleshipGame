using API.Enums;

namespace API.Models.Ships
{
    public class Battleship : Ship
    {
        public Battleship()
        {
            Type = ShipType.Battleship;
            Size = 4;
        }
    }
}
