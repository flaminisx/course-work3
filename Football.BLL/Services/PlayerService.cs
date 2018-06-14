using System.Collections.Generic;
using Football.BLL.DTO;
using Football.BLL.Interfaces;
using Football.DAL.Interfaces;
using Football.DAL.Entities;
using Football.BLL.Infrastructure;
using AutoMapper;
using System.Text.RegularExpressions;

namespace Football.BLL.Services
{
    public class PlayerService : IPlayerService
    {
        IUnitOfWork Database { get; set; }

        public PlayerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(PlayerDTO playerDto)
        {
            if (playerDto.Name == null || playerDto.Name.Length < 1)
                throw new ValidationException("Имя игрока должно состоять минимум из одного символа", "Name");
            if (playerDto.Surname == null || playerDto.Surname.Length < 1)
                throw new ValidationException("Фамилия игрока должна состоять минимум из одного символа", "Surname");
            Regex regex = new Regex("^[a-zA-Zа-яА-Я]+$");
            if (!regex.IsMatch(playerDto.Name))
                throw new ValidationException("Имя игрока должно состоять только из букв", "Name");
            if (!regex.IsMatch(playerDto.Surname))
                throw new ValidationException("Фамилия игрока должна состоять только из букв", "Surname");
            var player = new Player { Name = playerDto.Name, Surname = playerDto.Surname, TeamId = playerDto.TeamId };
            Database.Players.Create(player);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public PlayerDTO GetPlayer(int? id)
        {
            if (id == null)
                throw new ValidationException("Нет Id игрока", "Id");
            var player = Database.Players.Get(id.Value);
            if (player == null)
                throw new ValidationException("Игрок не найден", "");
            return new PlayerDTO { Id = player.Id, Name = player.Name, Surname = player.Surname, TeamId = player.TeamId };
        }

        public IEnumerable<PlayerDTO> GetPlayers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Player>, List<PlayerDTO>>(Database.Players.GetAll());
        }

        public void DeletePlayer(int? id)
        {
            if (id == null)
                throw new ValidationException("Нет Id игрока", "Id");
            Database.Players.Delete(id.Value);
            Database.Save();

        }

        public void UpdatePlayer(PlayerDTO playerDto)
        {
            if (playerDto.Name == null || playerDto.Name.Length < 1)
                throw new ValidationException("Имя стадиона должно состоять минимум из одного символа", "Name");
            if (playerDto.Surname == null || playerDto.Surname.Length < 1)
                throw new ValidationException("Фамилия игрока должна состоять минимум из одного символа", "Surname");
            Regex regex = new Regex("^[a-zA-Zа-яА-Я]+$");
            if (!regex.IsMatch(playerDto.Name))
                throw new ValidationException("Имя игрока должно состоять только из букв", "Name");
            if (!regex.IsMatch(playerDto.Surname))
                throw new ValidationException("Фамилия игрока должна состоять только из букв", "Surname");
            var player = new Player { Id = playerDto.Id, Name = playerDto.Name, Surname = playerDto.Surname, TeamId = playerDto.TeamId };
            Database.Players.Update(player);
            Database.Save();
        }

        public IEnumerable<PlayerDTO> GetPlayersByTeam(int? teamId)
        {
            if (teamId == null)
                throw new ValidationException("Нет Id команды", "Id");
            var players = Database.Players.Find((p) => p.TeamId == teamId.Value);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Player>, List<PlayerDTO>>(players);
        }
    }
}
