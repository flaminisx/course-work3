using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Football.DAL.Entities;


namespace Football.DAL.EF
{
    public class FootballContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Player> Players { get; set; }

        static FootballContext()
        {
            Database.SetInitializer<FootballContext>(new StoreDbInitializer());
        }
        public FootballContext(string connectionString)
            : base(connectionString)
        {
        }

    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<FootballContext>
    {
        protected override void Seed(FootballContext db)
        {
            db.Teams.Add(new Team { Name = "Liverpool" });
            db.SaveChanges();
        }
    }
}
