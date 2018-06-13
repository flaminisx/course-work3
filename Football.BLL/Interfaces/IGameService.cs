using Football.BLL.DTO;
using System.Collections.Generic;

namespace Football.BLL.Interfaces
{
    public interface IGameService
    {
        void Create(GameDTO gameDto);
        GameDTO GetGame(int? id);
        IEnumerable<GameDTO> GetGames();
        void Dispose();
    }
}
