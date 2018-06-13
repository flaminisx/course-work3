using Football.DAL.EF;
using Football.DAL.Entities;
using Football.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Football.DAL.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private FootballContext db;

        public GameRepository(FootballContext context)
        {
            this.db = context;
        }

        public void Create(Game item)
        {
            db.Games.Add(item);
        }

        public void Delete(int id)
        {
            Game game = db.Games.Find(id);
            if (game != null)
                db.Games.Remove(game);
        }

        public IEnumerable<Game> Find(Func<Game, bool> predicate)
        {
            return db.Games.Where(predicate).ToList();
        }

        public Game Get(int id)
        {
            return db.Games.Find(id);
        }

        public IEnumerable<Game> GetAll()
        {
            return db.Games;
        }

        public void Update(Game item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
