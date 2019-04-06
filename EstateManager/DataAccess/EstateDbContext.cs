using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EstateManager.DataAccess
{
    public class EstateDbContext : DbContext
    {

        #region Singleton

        private static bool _isInitialized = false;
        public static EstateDbContext Current
        {
            get
            {
                if (!_isInitialized)
                    throw new NullReferenceException("La base de données doit être initialisée !");
                return new EstateDbContext
                    (
                        Path.Combine
                        (
                            System.AppDomain.CurrentDomain.BaseDirectory,
                            "estate.db"
                        )
                    );
            }
        }
        public async static Task Initialize()
        {
            if (!_isInitialized)
            {
                var ctx = new EstateDbContext
                    (
                        Path.Combine
                        (
                            System.AppDomain.CurrentDomain.BaseDirectory,
                            "estate.db"
                        )
                    );
                await ctx.Database.MigrateAsync();
                _isInitialized = true;
            }
        }
        //private static EstateDbContext _context = null;
        //public static EstateDbContext Current
        //{
        //    get
        //    {
        //        if (_context == null)
        //            throw new NullReferenceException("La base de données doit être initialisée !");
        //        return _context;
        //    }
        //}
        //public async static Task<EstateDbContext> Initialize()
        //{
        //    if (_context == null)
        //    {
        //        _context = new EstateDbContext
        //            (
        //                Path.Combine
        //                (
        //                    System.AppDomain.CurrentDomain.BaseDirectory,
        //                    "estate.db"
        //                )
        //            );
        //        await _context.Database.MigrateAsync();
        //    }
        //    return _context;
        //}

        #endregion

        public string DatabasePath { get; }

        public DbSet<Model.Contract> Contracts { get; set; }
        public DbSet<Model.Estate> Estates { get; set; }
        public DbSet<Model.Person> Persons { get; set; }
        public DbSet<Model.Photo> Photos { get; set; }

        private EstateDbContext(string databasePath)
        {
            DatabasePath = databasePath;
        }
        internal EstateDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }

        public static void deleteContractsOf(Model.Estate e)
        {
            if (e.Contracts == null)
                return;

            for (int i = 0; i < e.Contracts.Count; i++)
                Current.Contracts.Remove(e.Contracts[i]);
        }
    }
}
