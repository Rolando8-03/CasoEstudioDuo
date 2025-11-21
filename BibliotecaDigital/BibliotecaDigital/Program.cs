using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital
{
    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }
        public string Descripcion { get; set; }
    }
    public static class SistemaBusqueda
    {
        public static void BusquedaLinealLibros(List<Libro> libros)
        {
            Console.Write("Ingrese el título del libro a buscar: ");
            string tituloBuscado = Console.ReadLine().ToLower();

            bool encontrado = false;
            int posicion = -1;
            for (int i = 0; i < libros.Count; i++)
            {
                if (libros[i].Titulo.ToLower() == tituloBuscado)
                {
                    encontrado = true;
                    posicion = i;
                    break;
                }
            }

            if (encontrado)
            {
                var libro = libros[posicion];
                Console.WriteLine($"✔ Libro encontrado en posición {posicion}: \"{libro.Titulo}\" ({libro.Anio}) – Autor: {libro.Autor}");
            }
            else
            {
                Console.WriteLine("✘ El libro no existe.");
            }
        }
        public static void BuscarCoincidenciaDescripcion(List<Libro> libros)
        {
            Console.Write("Ingrese una palabra para buscar en las descripciones: ");
            string palabra = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(palabra))
            {
                Console.WriteLine("Palabra vacía. Operación cancelada.");
                return;
            }

            foreach (var libro in libros)
            {
                int contador = 0;
                string descripcion = libro.Descripcion.ToLower();

                // recorrer la descripcion con ventana deslizante
                for (int i = 0; i <= descripcion.Length - palabra.Length; i++)
                {
                    bool coincide = true;
                    for (int j = 0; j < palabra.Length; j++)
                    {
                        if (descripcion[i + j] != palabra[j])
                        {
                            coincide = false;
                            break;
                        }
                    }
                    if (coincide) contador++;
                }

                Console.WriteLine($"El libro \"{libro.Titulo}\" tiene {contador} coincidencia(s) de \"{palabra}\" en su descripción.");
            }
        }
        public static void BusquedaBinariaAutores(List<string> autores)
        {
            if (autores == null || autores.Count == 0)
            {
                Console.WriteLine("La lista de autores está vacía.");
                return;
            }

            // Asegurarse que está ordenada (si no, se ordena aquí)
            autores.Sort(StringComparer.OrdinalIgnoreCase);

            Console.Write("Ingrese el nombre del autor a buscar: ");
            string autorBuscado = Console.ReadLine();

            int inicio = 0;
            int fin = autores.Count - 1;
            bool encontrado = false;
            int pos = -1;

            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;
                string nombreMedio = autores[medio];

                Console.WriteLine($"Probando posición {medio}: {nombreMedio}");

                int comparacion = string.Compare(nombreMedio, autorBuscado, StringComparison.OrdinalIgnoreCase);
                if (comparacion == 0)
                {
                    encontrado = true;
                    pos = medio;
                    break;
                }
                else if (comparacion < 0)
                {
                    inicio = medio + 1;
                }
                else
                {
                    fin = medio - 1;
                }
            }

            if (encontrado)
            {
                Console.WriteLine($"✔ Autor \"{autores[pos]}\" encontrado en la lista (posición {pos}).");
            }
            else
            {
                Console.WriteLine("✘ El autor no existe en la lista.");
            }
        }
        public static void BuscarExtremos(List<Libro> libros)
        {
            if (libros == null || libros.Count == 0)
            {
                Console.WriteLine("La lista de libros está vacía.");
                return;
            }

            Libro reciente = libros[0];
            Libro antiguo = libros[0];

            for (int i = 1; i < libros.Count; i++)
            {
                if (libros[i].Anio > reciente.Anio)
                {
                    reciente = libros[i];
                }
                if (libros[i].Anio < antiguo.Anio)
                {
                    antiguo = libros[i];
                }
            }

            Console.WriteLine($"📘 Libro más reciente: \"{reciente.Titulo}\" ({reciente.Anio}) - Autor: {reciente.Autor}");
            Console.WriteLine($"📙 Libro más antiguo: \"{antiguo.Titulo}\" ({antiguo.Anio}) - Autor: {antiguo.Autor}");
        }
    }
        internal class Program
        {
            static void Main(string[] args)
            {
            List<Libro> libros = new List<Libro>()
        {
            new Libro{Titulo="Programación en C#", Autor="Ana López", Anio=2020, Descripcion="Guía completa..."},
            new Libro{Titulo="Estructuras de Datos", Autor="Carlos Ruiz", Anio=2018, Descripcion="Conceptos esenciales..."},
            new Libro{Titulo="Algoritmos Modernos", Autor="Beatriz Torres", Anio=2023, Descripcion="Análisis de algoritmos..."},
            new Libro{Titulo="Inteligencia Artificial", Autor="Daniel Mejía", Anio=2019, Descripcion="Introducción a IA..."},
            new Libro{Titulo="Bases de Datos", Autor="Ernesto Silva", Anio=2016, Descripcion="Fundamentos de SQL..."}
        };

            List<string> autores = new List<string>()
        {
            "Ana López",
            "Beatriz Torres",
            "Carlos Ruiz",
            "Daniel Mejía",
            "Ernesto Silva"
        };

            int opcion = 0;
            do
            {
                Console.WriteLine("\n===== SISTEMA DE BÚSQUEDA – BIBLIOTECA DIGITAL =====");
                Console.WriteLine("1. Búsqueda lineal de libro por título");
                Console.WriteLine("2. Búsqueda binaria de autor");
                Console.WriteLine("3. Libro más reciente y más antiguo");
                Console.WriteLine("4. Búsqueda dentro de descripción");
                Console.WriteLine("5. Mostrar lista de libros");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                string entrada = Console.ReadLine();
                Console.WriteLine();

                if (!int.TryParse(entrada, out opcion))
                {
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        SistemaBusqueda.BusquedaLinealLibros(libros);
                        break;
                    case 2:
                        SistemaBusqueda.BusquedaBinariaAutores(autores);
                        break;
                    case 3:
                        SistemaBusqueda.BuscarExtremos(libros);
                        break;
                    case 4:
                        SistemaBusqueda.BuscarCoincidenciaDescripcion(libros);
                        break;
                    case 5:
                        MostrarLibros(libros);
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no reconocida.");
                        break;
                }

            } while (opcion != 6);
        }

        static void MostrarLibros(List<Libro> libros)
        {
            Console.WriteLine("Lista de libros:");
            for (int i = 0; i < libros.Count; i++)
            {
                var l = libros[i];
                Console.WriteLine($"{i}. \"{l.Titulo}\" - {l.Autor} ({l.Anio})");
            }
        }
    }
}
       
