using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.DAL.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int FirstTeamId { get; set; }
        public int SecondTeamId { get; set; }
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public int StadiumId { get; set; }
        public Stadium Stadium { get; set; }
        public int FirstTeamResult { get; set; }
        public int SecondTeamResult { get; set; }
    }
}
