using Ninject.Modules;
using Football.BLL.Services;
using Football.BLL.Interfaces;

namespace Football.PL.Util
{
    public class GameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameService>().To<GameService>();
        }
    }
}