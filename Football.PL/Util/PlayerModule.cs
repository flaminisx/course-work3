﻿using Ninject.Modules;
using Football.BLL.Services;
using Football.BLL.Interfaces;

namespace Football.PL.Util
{
    public class PlayerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlayerService>().To<PlayerService>();
        }
    }
}