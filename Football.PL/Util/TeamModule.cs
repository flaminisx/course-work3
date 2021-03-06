﻿using Ninject.Modules;
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