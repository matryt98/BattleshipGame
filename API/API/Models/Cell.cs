using API.Enums;

namespace API.Models
{
    public class Cell
    {
        public CellType Type { get; set; }
        public Coordinates Coordinates { get; set; }
        public bool Hit { get; set; }

        public Cell(int row, int col)
        {
            Coordinates = new(row, col);
            Type = CellType.Water;
        }
    }
}
