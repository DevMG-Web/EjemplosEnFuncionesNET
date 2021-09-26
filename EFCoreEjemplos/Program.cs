using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EFCoreEjemplos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var context = new ApplicationDbContext())
            {
   
            }

            Console.WriteLine("Listo");
        }

        static void EjemploInsertarEstudiantes()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = new Estudiante()
                {
                    Nombre = "Claudia",
                    Apellido = "Rodriguez"
                };
                context.Estudiantes.Add(estudiante);
                context.SaveChanges();
            }
        }

        static void EjemploActualizarEstdianteConUnModeloConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantes = context.Estudiantes.Where(x => x.Nombre == "Claudia").ToList();

                ImprimirLista(estudiantes);

                estudiantes[0].Nombre = "Apellidos";
                context.SaveChanges();
                ImprimirLista(estudiantes);
            }
        }

        static void EjemploActualizarEstdianteConUnModeloDesonectado(Estudiante Felipe)
        {
            using (var context = new ApplicationDbContext())
            {
                Felipe = context.Estudiantes.FirstOrDefault(m => m.Nombre == "Felipe");
            }

            Felipe.Nombre += " Apellidos2";

            using (var context = new ApplicationDbContext())
            {
                //context.Entry(Felipe).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Estudiantes.Update(Felipe);
                context.SaveChanges();
            }

        }

        static void EjemploEliminarEstudianteConUnModeloConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                Estudiante Felipe = context.Estudiantes.Where(x => x.Nombre == "Felipe").FirstOrDefault();

                context.Estudiantes.Remove(Felipe);
                context.SaveChanges();
            }
        }

        static void ImprimirLista(IEnumerable<Estudiante> listado)
        {
            foreach (var item in listado)
            {
                Console.WriteLine(item.Id + " - " + item.Apellido+ ", " + item.Nombre);
            }
        }

        static void agregarModeloUnoAUnoEnElModeloConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = new Estudiante()
                {
                    Nombre = "Felipe",
                    Edad = 999
                };

                var direccion = new Direccion()
                {
                    Calle = "Ejemplo",
                };
                estudiante.Direccion = direccion;

                context.Add(estudiante);

                context.SaveChanges();
            }
        }

        static void AgregarModeloUnoAunoModeloDescontectado()
        {
            var estudianteClaudio = new Estudiante()
            {
                Id = 3,
            };

            var direccion = new Direccion()
            {
                Calle = "Ejemplo 2",
                EstudianteId = estudianteClaudio.Id
            };

            ///Modelo desconectado
            using (var context = new ApplicationDbContext())
            {
                context.Direcciones.Add(direccion);
                context.SaveChanges();
            }

        }

        static void ListarEstudiantesDireccionConIncludeLeftJoinLazyLoading()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantes = context.Estudiantes.Include(x => x.Direccion).ToList();
            }
        }

        static void ListarInstitucionesEstudiantes()
        {
            using (var context = new ApplicationDbContext())
            {
                //var institucion = 
                //    context.Instituciones.Where(x => x.Id == 1).Include(x => x.Estudiantes).ToList();

                //Proyeccion 
                //var persona = context.Estudiantes.Select(x => new { id = x.Id, nombre = x.Nombre }).FirstOrDefault();

                var institucionesEstudiantes = context.Instituciones.Where(x => x.Id == 1)
                     .Select(x => new { Institucion = x, Estudiante = x.Estudiantes.Where(e => e.Edad > 18).ToList() })
                     .ToList();
            }
        }

        static void InsertarDataTablaEstudianteCurso()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = context.Estudiantes.FirstOrDefault();
                var curso = context.Cursos.FirstOrDefault();
                var EstudianteCurso = new EstudianteCurso()
                {
                    CursoId = curso.Id,
                    EstudianteId = estudiante.Id,
                    Activo = true
                };
                context.EstudiantesCursos.Add(EstudianteCurso);
                context.SaveChanges();
            }
        }

        static void SelectDataTablaAsociadaEstudianteCurso()
        {
            using (var context = new ApplicationDbContext())
            {
                var curso = context.Cursos.Where(x => x.Id == 1)
                    .Include(x => x.EstudianteCursos)
                    .ThenInclude(y => y.Estudiante)
                    .FirstOrDefault();
            }
        }

        static void selectDataTablaEstudianteCurso()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantesCursos = context.EstudiantesCursos.ToList();
            }
        }

        static void InjectionStringInterpolation()
        {
            using (var context = new ApplicationDbContext())
            {
                string nombre = "Felipe or 1=1";
                var estudiante = context.Estudiantes
                    .FromSqlInterpolated($"select * from Estudiantes where Nombre = {nombre}").ToList();
            }
        }

        static void EjemploBorradoSuave()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = context.Estudiantes.FirstOrDefault();
                context.Remove(estudiante);
                context.SaveChanges();
            }
        }

        static void PruebaConcurrecyCheck()
        {
            using (var context = new ApplicationDbContext())
            {
                var est = context.Estudiantes.FirstOrDefault();
                est.Nombre += " 2";

                context.SaveChanges();
            }
        }

        static void EjemploFuncionesEscalares()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = context.Estudiantes
                    .Where(x => ApplicationDbContext.Cantidad_De_Cursos_Activos(x.Id) > 0).ToList();
            }
        }

        static void EjemploTableSplitting()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = new Estudiante();
                estudiante.Nombre = "Carlos";
                estudiante.Edad = 45;
                estudiante.EstaBorrado = false;
                estudiante.InstitucionId = 1;

                var detalle = new EstudianteDetalle();
                detalle.Becado = false;
                detalle.Carrera = "Lic en Matematicas";
                detalle.CategoriaDePago = 1;

                estudiante.Detalles = detalle;
                context.Add(estudiante);
                context.SaveChanges();

                //Para requerir la informacion completa por ser una tabla de uno a uno
                //Table Splitting; Mapear una tabla en varios modelos
                //var estudiantes = context.Estudiantes.Include(x => x.Detalles).ToList();
            }
        }
    }

    class Institucion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Estudiante> Estudiantes { get; set; }
    }

    class Estudiante
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public string Nombre { get; set; }

        private string _Apellido;
        public string Apellido { 
            get { return _Apellido; }
            set {
                _Apellido = value.ToUpper();
            }
        }
        public int Edad { get; set; }
        public int InstitucionId { get; set; }
        public bool EstaBorrado { get; set; }
        public Direccion Direccion { get; set; }
        public List<EstudianteCurso> EstudianteCursos { get; set; }
        public EstudianteDetalle Detalles { get; set; }
    }

    class EstudianteDetalle
    {
        public int Id { get; set; }
        public bool Becado { get; set; }
        public string Carrera { get; set; }
        public int CategoriaDePago { get; set; }
        public Estudiante Estudiante { get; set; }
    }

    class Direccion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public int EstudianteId { get; set; }
    }

    class Curso
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Nombre { get; set; }
        public List<EstudianteCurso> EstudianteCursos { get; set; }
    }

    class EstudianteCurso
    {
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public bool Activo { get; set; }
        public Estudiante Estudiante { get; set; }
        public Curso Curso { get; set; }
    }
}
