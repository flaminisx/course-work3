using Ninject.Modules;
using Football.BLL.Services;
using Football.BLL.Interfaces;

namespace Football.PL.Util
{
    public class StaduimModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStadiumService>().To<StadiumService>();
        }
    }
}