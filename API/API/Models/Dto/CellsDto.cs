namespace API.Models.Dto
{
    public class CellsDto
    {
        public IEnumerable<Cell> FriendlyCells { get; set; }
        public IEnumerable<Cell> OpponentCells { get; set; }
    }
}
