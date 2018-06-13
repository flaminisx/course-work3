using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Football.BLL.Services;
using Football.BLL.Interfaces;

namespace Football.PL.Util
{
    public class TeamModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITeamService>().To<TeamService>();
        }
    }
}