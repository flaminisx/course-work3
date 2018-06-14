using System.Collections.Generic;
using Football.BLL.DTO;
using Football.BLL.Interfaces;
using Football.DAL.Interfaces;
using Football.DAL.Entities;
using Football.BLL.Infrastructure;
using AutoMapper;

namespace Football.BLL.Services
{
    public class GameService : IGameService
    {
        IUnitOfWork Database { get; set; }

        public GameService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(GameDTO gameDto)
        {
            Team team1 = Database.Teams.Get(gameDto.FirstTeamId);
            if (team1 == null)
                throw new ValidationException("Первой команды не существует", "FirstTeamId");

            Team team2 = Database.Teams.Get(gameDto.SecondTeamId);
            if (team2 == null)
                throw new ValidationException("Второй команды не существует", "SecondTeamId");

            Stadium stadium = Database.Stadiums.Get(gameDto.StadiumId);
            if (stadium == null)
                throw new ValidationException("Второй команды не существует", "StadiumId");
            if(gameDto.FirstTeamId == gameDto.SecondTeamId)
            {
                throw new ValidationException("Команда не может играть сама с собой", "SecondTeamId");
            }
            var game = new Game {
                FirstTeamId = gameDto.FirstTeamId,
                SecondTeamId = gameDto.SecondTeamId,
                StadiumId = gameDto.StadiumId,
                FirstTeamResult = gameDto.FirstTeamResult,
                SecondTeamResult = gameDto.SecondTeamResult
            };
            Database.Games.Create(game);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public GameDTO GetGame(int? id)
        {
            if (id == null)
                throw new ValidationException("Нет Id игры", "id");
            var game = Database.Games.Get(id.Value);
            if (game == null)
                throw new ValidationException("Игра не найдена", "");
            return new GameDTO {
                Id = game.Id,
                FirstTeamId = game.FirstTeamId,
                SecondTeamId = game.SecondTeamId,
                StadiumId = game.StadiumId,
                FirstTeamResult = game.FirstTeamResult,
                SecondTeamResult = game.SecondTeamResult,
                FirstTeamName = game.FirstTeam.Name,
                SecondTeamName = game.SecondTeam.Name,
                StadiumName = game.Stadium.Name,
            };
        }

        public IEnumerable<GameDTO> GetGames()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Game, GameDTO>()
                .ForMember(gameDTO => gameDTO.FirstTeamName, opts => opts.MapFrom(game => game.FirstTeam.Name))
                .ForMember(gameDTO => gameDTO.SecondTeamName, opts => opts.MapFrom(game => game.SecondTeam.Name))
                .ForMember(gameDTO => gameDTO.StadiumName, opts => opts.MapFrom(game => game.Stadium.Name))).CreateMapper();
            return mapper.Map<IEnumerable<Game>, List<GameDTO>>(Database.Games.GetAll());
        }

        public void DeleteGame(int? id)
        {
            if (id == null)
                throw new ValidationException("Нет Id игры", "id");
            Database.Games.Delete(id.Value);
            Database.Save();
        }

        public void UpdateGame(GameDTO gameDto)
        {
            Team team1 = Database.Teams.Get(gameDto.FirstTeamId);
            if (team1 == null)
                throw new ValidationException("Первой команды не существует", "FirstTeamId");

            Team team2 = Database.Teams.Get(gameDto.SecondTeamId);
            if (team2 == null)
                throw new ValidationException("Второй команды не существует", "SecondTeamId");

            Stadium stadium = Database.Stadiums.Get(gameDto.StadiumId);
            if (stadium == null)
                throw new ValidationException("Второй команды не существует", "StadiumId");

            Game game = new Game
            {
                Id = gameDto.Id,
                FirstTeamId = gameDto.FirstTeamId,
                SecondTeamId = gameDto.SecondTeamId,
                StadiumId = gameDto.StadiumId,
                FirstTeamResult = gameDto.FirstTeamResult,
                SecondTeamResult = gameDto.SecondTeamResult
            };
            Database.Games.Update(game);
            Database.Save();
        }
    }
}
