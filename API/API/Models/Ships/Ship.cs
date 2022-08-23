using API.Enums;

namespace API.Models.Ships
{
    public class Ship
    {
        public ShipType Type { get; protected set; }
        public int Size { get; protected set; }
        public int Hits { get; set; } = 0;

        public bool Sunk
        {
            get { return Hits >= Size; }
        }
    }
}
