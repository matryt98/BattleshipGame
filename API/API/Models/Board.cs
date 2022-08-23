using API.Enums;
using API.Models.Ships;

namespace API.Models
{
    public class Board
    {
        public List<Cell> Cells { get; set; }

        public Board()
        {
            Cells = new(100);
            for (int i = 0; i < 100; i++)
            {
                Cells.Add(new Cell((i / 10) + 1, (i % 10) + 1));
            }
        }

        private void ResetBoard()
        {
            Cells = new(100);
            for (int i = 0; i < 100; i++)
            {
                Cells.Add(new Cell((i / 10) + 1, (i % 10) + 1));
            }
        }

        public Cell? GetCell(Coordinates coordinates)
        {
            return Cells.Where(x => x.Coordinates == coordinates).FirstOrDefault();
        }

        public Cell? GetCell(int row, int col)
        {
            return Cells.Where(x => x.Coordinates.Row == row && x.Coordinates.Col == col).FirstOrDefault();
        }

        public void DrawShips()
        {
            ResetBoard();

            List<Ship> ships = new()
            {
                new Carrier(),
                new Battleship(),
                new Cruiser(),
                new Submarine(),
                new Destroyer(),
            };

            foreach (Ship ship in ships)
            {
                bool isShipPlaced = false;
                while (!isShipPlaced)
                {
                    Random random = new();
                    int startRow = random.Next(1, 10);
                    int startCol = random.Next(1, 10);

                    Array shipPositionValues = Enum.GetValues(typeof(ShipPosition));
                    ShipPosition position = (ShipPosition)shipPositionValues.GetValue(random.Next(shipPositionValues.Length));

                    Array shipDirectionValues = Enum.GetValues(typeof(ShipDirection));
                    ShipDirection direction = (ShipDirection)shipDirectionValues.GetValue(random.Next(shipPositionValues.Length));

                    int endRow = startRow;
                    int endCol = startCol;
                    int cellsToShift = ship.Size - 1;

                    switch (position)
                    {
                        case ShipPosition.Horizontal:
                            switch (direction)
                            {
                                case ShipDirection.UpLeft:
                                    endCol -= cellsToShift;
                                    break;
                                case ShipDirection.DownRight:
                                    endCol += cellsToShift;
                                    break;
                            }
                            break;
                        case ShipPosition.Vertical:
                            switch (direction)
                            {
                                case ShipDirection.UpLeft:
                                    endRow -= cellsToShift;
                                    break;
                                case ShipDirection.DownRight:
                                    endRow += cellsToShift;
                                    break;
                            }
                            break;
                    }

                    List<Coordinates> coordinates = GetShipCoordinates(startRow, startCol, position, direction, ship.Size).OrderBy(x => x.Row).ThenBy(y => y.Col).ToList();

                    if (IsShipOutsideTheMap(coordinates) || IsShipTooCloseToOtherShip(coordinates))
                        continue;

                    //if (IsShipOutsideTheMap(coordinates))
                    //    continue;

                    //if (IsShipTooCloseToOtherShip(coordinates))
                    //    continue;

                    foreach (var coordinate in coordinates)
                    {
                        Cell? cell = GetCell(coordinate);
                        if (cell != null)
                            cell.Type = CellType.Ship;
                    }

                    isShipPlaced = true;
                }


                // draw start row + col
                // draw position (vertical or horizontal)
                // draw direction before/after (for vertical up/down, for horizontal left/right)
                // check if ship can be drawn:
                // 1. do not cross the borders
                // 2. isn't placed on another ship
                // 3. isnt' placed too close to another ship (must be one empty cell between ships)
            }
        }

        public Coordinates Attack()
        {
            Random random = new();
            IEnumerable<Cell>? filteredCells = Cells.Where(x => !x.Hit);
            int index = random.Next(0, filteredCells.Count());
            Coordinates coordinates = filteredCells.ElementAt(index).Coordinates;
            Cell cell = GetCell(coordinates);
            cell.Hit = true;

            return coordinates;
        }

        public bool Attack(Coordinates coordinates)
        {
            Cell? cell = GetCell(coordinates);
            if (cell == null)
                throw new Exception();

            cell.Hit = true;

            return cell.Type == CellType.Ship;
        }

        private static IEnumerable<Coordinates> GetShipCoordinates(int startRow, int startCol, ShipPosition position, ShipDirection direction, int shipSize)
        {
            switch (position)
            {
                case ShipPosition.Horizontal:
                    switch (direction)
                    {
                        case ShipDirection.UpLeft:
                            return Enumerable.Range(1, shipSize).Select(x => new Coordinates(startRow, startCol--));
                        case ShipDirection.DownRight:
                            return Enumerable.Range(1, shipSize).Select(x => new Coordinates(startRow, startCol++));
                    }
                    break;
                case ShipPosition.Vertical:
                    switch (direction)
                    {
                        case ShipDirection.UpLeft:
                            return Enumerable.Range(1, shipSize).Select(x => new Coordinates(startRow--, startCol));
                        case ShipDirection.DownRight:
                            return Enumerable.Range(1, shipSize).Select(x => new Coordinates(startRow++, startCol));
                    }
                    break;
            }

            return null;
        }

        private bool IsShipOutsideTheMap(IEnumerable<Coordinates> coordinates)
        {
            Coordinates firstPoint = coordinates.First();
            Coordinates lastPoint = coordinates.Last();

            bool result = firstPoint.Row < 1 || firstPoint.Col < 1 || lastPoint.Row > 10 || lastPoint.Col > 10;
            return result;
        }

        private bool IsShipTooCloseToOtherShip(IEnumerable<Coordinates> coordinates)
        {
            Coordinates firstPoint = coordinates.First();
            Coordinates lastPoint = coordinates.Last();

            Coordinates startPoint = new(firstPoint.Row - 1, firstPoint.Col - 1);
            Coordinates endPoint = new(lastPoint.Row + 1, lastPoint.Col + 1);

            return Cells.Any(x =>
                x.Type == CellType.Ship &&
                x.Coordinates.Row >= startPoint.Row &&
                x.Coordinates.Col >= startPoint.Col &&
                x.Coordinates.Row <= endPoint.Row &&
                x.Coordinates.Col <= endPoint.Col);
        }
    }
}
