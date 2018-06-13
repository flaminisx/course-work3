using Football.DAL.Entities;
using System;

namespace Football.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Game> Games { get; }
        IRepository<Team> Teams { get; }
        IRepository<Player> Players { get; }
        IRepository<Stadium> Stadiums { get; }
        void Save();
    }
}