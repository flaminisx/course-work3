using System.Collections.Generic;
using Football.BLL.DTO;
using Football.BLL.Interfaces;
using Football.DAL.Interfaces;
using Football.DAL.Entities;
using Football.BLL.Infrastructure;
using AutoMapper;

namespace Football.BLL.Services
{
    public class TeamService : ITeamService
    {

        IUnitOfWork Database { get; set; }

        public TeamService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(TeamDTO teamDto)
        {
            if (teamDto.Name.Length < 1)
                throw new ValidationException("Имя команды должно состоять минимум из одного символа", "Name");
            Team team = new Team { Name = teamDto.Name };
            Database.Teams.Create(team);
            Database.Save();
        }

        public TeamDTO GetTeam(int? id)
        {
            if (id == null)
                throw new ValidationException("Нет Id команды", "id");
            var team = Database.Teams.Get(id.Value);
            if (team == null)
                throw new ValidationException("Команда не найдена", "");
            return new TeamDTO { Id = team.Id, Name = team.Name };
        }

        public IEnumerable<TeamDTO> GetTeams()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Team>, List<TeamDTO>>(Database.Teams.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
