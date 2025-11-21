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
            }
        }
}
