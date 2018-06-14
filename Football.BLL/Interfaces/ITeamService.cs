using System;
using System.Collections.Generic;
using Football.BLL.DTO;

namespace Football.BLL.Interfaces
{
    public interface ITeamService
    {
        void Create(TeamDTO teamDto);
        TeamDTO GetTeam(int? id);
        IEnumerable<TeamDTO> GetTeams();
        void DeleteTeam(int? id);
        void UpdateTeam(TeamDTO teamDto);
        void Dispose();
    }
}
