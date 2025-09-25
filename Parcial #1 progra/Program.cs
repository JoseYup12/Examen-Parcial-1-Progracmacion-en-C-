using System;
using System.Collections.Generic;

// Clase base Persona
public class Persona
{
    public string Nombre { get; set; }
    public string Dpi { get; set; }
    public string Correo { get; set; }

    public virtual void MostrarInformacion()
    {
        Console.WriteLine("Nombre: " + Nombre + ", DPI: " + Dpi + ", Correo: " + Correo);
    }
}

// Subclase Estudiante
public class Estudiante : Persona
{
    public string Carnet { get; set; }
    public Dictionary<string, float> Notas { get; set; }

    public Estudiante()
    {
        Notas = new Dictionary<string, float>();
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine("Estudiante: " + Nombre + ", Carnet: " + Carnet + ", Correo: " + Correo);
    }
}

// Subclase Profesor
public class Profesor : Persona
{
    public string Especialidad { get; set; }
    public List<Curso> CursosAsignados { get; set; }

    public Profesor()
    {
        CursosAsignados = new List<Curso>();
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine("Profesor: " + Nombre + ", Especialidad: " + Especialidad);
    }
}

// Clase Curso
public class Curso
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public Profesor ProfesorAsignado { get; set; }
    public List<Estudiante> Estudiantes { get; set; }

    public Curso()
    {
        Estudiantes = new List<Estudiante>();
    }

    public void AsignarProfesor(Profesor p)
    {
        ProfesorAsignado = p;
        p.CursosAsignados.Add(this);
    }

    public void AgregarEstudiante(Estudiante e)
    {
        Estudiantes.Add(e);
    }

    public void RegistrarNota(Estudiante e, float nota)
    {
        if (Estudiantes.Contains(e))
        {
            e.Notas[Nombre] = nota;
        }
    }

    public float CalcularPromedio()
    {
        float suma = 0;
        int contador = 0;

        foreach (var estudiante in Estudiantes)
        {
            if (estudiante.Notas.ContainsKey(Nombre))
            {
                suma += estudiante.Notas[Nombre];
                contador++;
            }
        }
        return contador > 0 ? suma / contador : 0;
    }
}

// Programa principal con menú
class Program
{
    static void Main(string[] args)
    {
        // Crear profesores
        Profesor prof1 = new Profesor { Nombre = "Carlos López", Dpi = "123456", Correo = "carlos@up.com", Especialidad = "Matemáticas" };
        Profesor prof2 = new Profesor { Nombre = "Ana Pérez", Dpi = "789012", Correo = "ana@up.com", Especialidad = "Programación" };

        // Crear cursos
        Curso curso1 = new Curso { Codigo = "MAT101", Nombre = "Matemáticas I" };
        Curso curso2 = new Curso { Codigo = "PRO201", Nombre = "Programación II" };
        Curso curso3 = new Curso { Codigo = "HIS301", Nombre = "Historia" };

        // Asignar profesores
        curso1.AsignarProfesor(prof1);
        curso2.AsignarProfesor(prof2);
        curso3.AsignarProfesor(prof1);

        // Crear estudiantes
        Estudiante est1 = new Estudiante { Nombre = "Luis Gómez", Carnet = "E001", Dpi = "111", Correo = "luis@up.com" };
        Estudiante est2 = new Estudiante { Nombre = "María Díaz", Carnet = "E002", Dpi = "222", Correo = "maria@up.com" };
        Estudiante est3 = new Estudiante { Nombre = "Pedro Ruiz", Carnet = "E003", Dpi = "333", Correo = "pedro@up.com" };
        Estudiante est4 = new Estudiante { Nombre = "Lucía Torres", Carnet = "E004", Dpi = "444", Correo = "lucia@up.com" };

        // Asignar estudiantes a cursos
        curso1.AgregarEstudiante(est1);
        curso1.AgregarEstudiante(est2);
        curso2.AgregarEstudiante(est3);
        curso2.AgregarEstudiante(est4);
        curso3.AgregarEstudiante(est1);
        curso3.AgregarEstudiante(est3);

        // Registrar notas
        curso1.RegistrarNota(est1, 80);
        curso1.RegistrarNota(est2, 90);
        curso2.RegistrarNota(est3, 85);
        curso2.RegistrarNota(est4, 95);
        curso3.RegistrarNota(est1, 70);
        curso3.RegistrarNota(est3, 75);

        // Menú
        int opcion = 0;
        while (opcion != 4)
        {
            Console.WriteLine("\n--- Sistema Académico ---");
            Console.WriteLine("1. Ver información de cursos");
            Console.WriteLine("2. Ver notas de estudiantes");
            Console.WriteLine("3. Calcular promedios");
            Console.WriteLine("4. Salir");
            Console.Write("Ingrese opción: ");
            opcion = int.Parse(Console.ReadLine());

            if (opcion == 1)
            {
                Console.WriteLine("\nCursos registrados:");
                Console.WriteLine(curso1.Nombre + " - Profesor: " + curso1.ProfesorAsignado.Nombre);
                Console.WriteLine(curso2.Nombre + " - Profesor: " + curso2.ProfesorAsignado.Nombre);
                Console.WriteLine(curso3.Nombre + " - Profesor: " + curso3.ProfesorAsignado.Nombre);
            }
            else if (opcion == 2)
            {
                Console.WriteLine("\nNotas de estudiantes:");
                foreach (var est in new List<Estudiante> { est1, est2, est3, est4 })
                {
                    est.MostrarInformacion();
                    foreach (var nota in est.Notas)
                    {
                        Console.WriteLine("Curso: " + nota.Key + " - Nota: " + nota.Value);
                    }
                }
            }
            else if (opcion == 3)
            {
                Console.WriteLine("\nPromedios de cursos:");
                Console.WriteLine(curso1.Nombre + ": " + curso1.CalcularPromedio());
                Console.WriteLine(curso2.Nombre + ": " + curso2.CalcularPromedio());
                Console.WriteLine(curso3.Nombre + ": " + curso3.CalcularPromedio());
            }
            else if (opcion == 4)
            {
                Console.WriteLine("Saliendo del sistema...");
            }
            else
            {
                Console.WriteLine("Opción inválida.");
            }
        }
    }
}