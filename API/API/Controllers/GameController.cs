using API.Models;
using API.Models.Dto;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GameController : BaseApiController
    {
        private readonly ICacheService _cacheService;

        public GameController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [Route("StartNew")]
        [HttpGet]
        public IActionResult StartNewGame()
        {
            try
            {
                Game game = new();

                _cacheService.SetCacheItem($"Game-{game.Id}", game, DateTime.Now.AddHours(1));

                return Ok(game.Id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("GetGameCells")]
        [HttpGet]
        public IActionResult GetGameCells(Guid gameId)
        {
            try
            {
                Game? game = _cacheService.GetItemFromCache<Game>($"Game-{gameId}");

                if (game == null)
                    return BadRequest();

                CellsDto result = new()
                {
                    FriendlyCells = game.FirstBoard.Cells,
                    OpponentCells = game.SecondBoard.Cells,
                };

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("DrawShips")]
        [HttpPost]
        public IActionResult DrawShips(Guid gameId)
        {
            try
            {
                Game? game = _cacheService.GetItemFromCache<Game>($"Game-{gameId}");

                if (game == null)
                    return BadRequest();

                game.FirstBoard.DrawShips();
                game.SecondBoard.DrawShips();

                _cacheService.SetCacheItem($"Game-{game.Id}", game, DateTime.Now.AddHours(1));

                CellsDto result = new()
                {
                    FriendlyCells = game.FirstBoard.Cells,
                    OpponentCells = game.SecondBoard.Cells,
                };

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("Attack")]
        [HttpPost]
        public IActionResult Attack(Guid gameId, Coordinates coordinates)
        {
            try
            {
                Game? game = _cacheService.GetItemFromCache<Game>($"Game-{gameId}");

                if (game == null)
                    return BadRequest();

                bool shipHit = game.SecondBoard.Attack(coordinates);

                _cacheService.SetCacheItem($"Game-{game.Id}", game, DateTime.Now.AddHours(1));

                return Ok(shipHit);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("OpponentAttack")]
        [HttpPost]
        public IActionResult OpponentAttack(Guid gameId)
        {
            try
            {
                Game? game = _cacheService.GetItemFromCache<Game>($"Game-{gameId}");

                if (game == null)
                    return BadRequest();

                Coordinates coordinates = game.FirstBoard.Attack();

                _cacheService.SetCacheItem($"Game-{game.Id}", game, DateTime.Now.AddHours(1));

                return Ok(coordinates);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
