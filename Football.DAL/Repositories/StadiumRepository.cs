using Football.DAL.EF;
using Football.DAL.Entities;
using Football.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Football.DAL.Repositories
{
    public class StadiumRepository : IRepository<Stadium>
    {
        private FootballContext db;

        public StadiumRepository(FootballContext context)
        {
            this.db = context;
        }

        public void Create(Stadium item)
        {
            db.Stadiums.Add(item);
        }

        public void Delete(int id)
        {
            Stadium stadium = db.Stadiums.Find(id);
            if (stadium != null)
                db.Stadiums.Remove(stadium);
        }

        public IEnumerable<Stadium> Find(Func<Stadium, bool> predicate)
        {
            return db.Stadiums.Where(predicate).ToList();
        }

        public Stadium Get(int id)
        {
            return db.Stadiums.Find(id);
        }

        public IEnumerable<Stadium> GetAll()
        {
            return db.Stadiums;
        }

        public void Update(Stadium item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
