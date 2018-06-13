using System;
using Football.DAL.EF;
using Football.DAL.Interfaces;
using Football.DAL.Entities;

namespace Football.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private FootballContext db;
        private TeamRepository teamRepository;
        private GameRepository gameRepository;
        private PlayerRepository playerRepository;
        private StadiumRepository stadiumRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new FootballContext(connectionString);
        }
        public IRepository<Team> Teams
        {
            get
            {
                if (teamRepository == null)
                    teamRepository = new TeamRepository(db);
                return teamRepository;
            }
        }

        public IRepository<Stadium> Stadiums
        {
            get
            {
                if (stadiumRepository == null)
                    stadiumRepository = new StadiumRepository(db);
                return stadiumRepository;
            }
        }
        public IRepository<Player> Players
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new PlayerRepository(db);
                return playerRepository;
            }
        }
        public IRepository<Game> Games
        {
            get
            {
                if (gameRepository == null)
                    gameRepository = new GameRepository(db);
                return gameRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}