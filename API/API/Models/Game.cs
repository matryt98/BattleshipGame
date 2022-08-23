namespace API.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Board FirstBoard { get; set; }
        public Board SecondBoard { get; set; }

        public Game()
        {
            Id = Guid.NewGuid();
            FirstBoard = new Board();
            SecondBoard = new Board();
        }
    }
}
