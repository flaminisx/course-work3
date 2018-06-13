using System;
using System.Collections.Generic;
using System.Linq;
using Football.DAL.Entities;
using Football.DAL.EF;
using Football.DAL.Interfaces;
using System.Data.Entity;

namespace Football.DAL.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private FootballContext db;

        public TeamRepository(FootballContext context)
        {
            this.db = context;
        }

        public IEnumerable<Team> GetAll()
        {
            return db.Teams;
        }

        public Team Get(int id)
        {
            return db.Teams.Find(id);
        }

        public void Create(Team team)
        {
            db.Teams.Add(team);
        }

        public void Update(Team team)
        {
            db.Entry(team).State = EntityState.Modified;
        }

        public IEnumerable<Team> Find(Func<Team, Boolean> predicate)
        {
            return db.Teams.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Team team = db.Teams.Find(id);
            if (team != null)
                db.Teams.Remove(team);
        }
    }
}