using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCoreEjemplos
{
    class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreEjemplos;Trusted_Connection=True;MultipleActiveResultSets=true")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new LoggerFactory());
                //.UseLoggerFactory(new LoggerFactory().AddConsole((category, level) => level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true) );
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstudianteCurso>().HasKey(x => new { x.CursoId, x.EstudianteId });
            modelBuilder.Entity<EstudianteCurso>().HasQueryFilter(x => x.Activo == true);

            //TableSplitting
            modelBuilder.Entity<Estudiante>()
                .HasOne(x => x.Detalles)
                .WithOne(x => x.Estudiante)
                .HasForeignKey<EstudianteDetalle>(x => x.Id);
            modelBuilder.Entity<EstudianteDetalle>().ToTable("Estudiantes");

            //Mapeo Flexible; para aplicar transformaciones 
            modelBuilder.Entity<Estudiante>().Property(x => x.Apellido).HasField("_Apellido"); 
        }

        public override int SaveChanges()
        {
            //Borrado suave
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted &&
                e.Metadata.GetProperties().Any(x => x.Name == "EstaBorrado")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["EstaBorrado"] = true;
            }

            return base.SaveChanges();
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<EstudianteCurso> EstudiantesCursos { get; set; }
        
        [DbFunction(Schema = "dbo")]
        public static int Cantidad_De_Cursos_Activos(int EstudianteId)
        {
            throw new Exception();
        }
    }
}
