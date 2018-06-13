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
        void Dispose();
    }
}
