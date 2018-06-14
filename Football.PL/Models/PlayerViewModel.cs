using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Football.PL.Models
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TeamId { get; set; }
    }
}