
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;

namespace ejercicio1
{
    public class Cajon //creo un objeto de tipo "Cajon"
    {
        public int elementos; // atributo para indicar cuantos elementos guarda el cajon
        public Cajon(int CantElementos) // Constructor para indicar que al crear un objeto "nace" con una cantidad de elementos
        {
            this.elementos = CantElementos;
        }
        public int entregarElementos() // metodo del objeto cajon, para entregar elementos, 
        {
            lock (this) // para que solo 1 hilo a la vez pueda ingresar
            {
                if (this.elementos > 0) // si tiene elementos
                {
                    this.elementos--; // decuento uno del atributo
                    return 1;         // entrego 1 elemento
                }
                else
                {
                    return 0;  // sino tiene elementos devuelvo 0
                }
            }
        }
    }
    internal class Program
    {
        static Cajon cajon = new Cajon(10); //creo un objeto cajon con 10 elementos
        static bool cajonVacio = false; // variable para cortar el ciclo      
        static void sacarElementos(object hilo)
        {
            while (true)
            {
                try
                {
                    if (cajonVacio == false) // si cajon no esta vacio ingreso
                    {
                        int elementoSacado = cajon.entregarElementos(); //asigno a la variable el objeto con su metodo.
                        if (elementoSacado > 0) // compruebo que la cantidad de elementos sea mayor  a cero
                        {
                            Console.WriteLine($"{hilo} saco un elemento del cajon. Quedan: {cajon.elementos}"); // muestro por consola cuantos elementos quedan del objeto.
                        }
                        else
                        {
                            cajonVacio = true; // cambio el estado de la variable porque cajon ya no tiene elementos
                            Console.WriteLine($"{hilo} no pudo sacar elementos, el cajon esta vacio."); // muestro por consola.
                            break; //corto el ciclo while.
                        }
                        
                    }
                    Thread.Sleep(1000); //
                }
                catch (Exception ex) // captura el error.
                {

                    ex.ToString();
                }

            }
        }
        static void Main(string[] args)
        {

            Thread hiloPersona1 = new Thread(sacarElementos); // creo  hilo1
            Thread hilopersona2 = new Thread(sacarElementos); // creo hilo2

            hiloPersona1.Start("Pepe"); 
            hilopersona2.Start("Pepa");
            Console.ReadKey();
        }
    }
}
