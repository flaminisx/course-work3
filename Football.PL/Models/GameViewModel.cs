using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Football.PL.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public int FirstTeamId { get; set; }
        public int SecondTeamId { get; set; }
        public int StadiumId { get; set; }
        public int FirstTeamResult { get; set; }
        public int SecondTeamResult { get; set; }
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public string StadiumName { get; set; }
    }
}