using Football.DAL.EF;
using Football.DAL.Entities;
using Football.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Football.DAL.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private FootballContext db;

        public PlayerRepository(FootballContext context)
        {
            this.db = context;
        }

        public void Create(Player item)
        {
            db.Players.Add(item);
        }

        public void Delete(int id)
        {
            Player player = db.Players.Find(id);
            if (player != null)
                db.Players.Remove(player);
        }

        public IEnumerable<Player> Find(Func<Player, bool> predicate)
        {
            return db.Players.Where(predicate).ToList();
        }

        public Player Get(int id)
        {
            return db.Players.Find(id);
        }

        public IEnumerable<Player> GetAll()
        {
            return db.Players;
        }

        public void Update(Player item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
