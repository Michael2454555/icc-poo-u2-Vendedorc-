// Program.cs
using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        // Crear objetos
        Objeto granada = new Objeto("Granada", 2000);
        Objeto colgante = new Objeto("Colgante", 2500);
        Arma magnum = new Arma("Magnum", 5500, 5, 2, 1, 3);
        Arma escopeta = new Arma("Escopeta", 3200, 4, 1, 1, 3);
        Arma pistola = new Arma("Pistola", 4000, 4, 2, 2, 3);
        Objeto spray = new Objeto("Spray", 2000);

        // Inventarios iniciales
        var inventarioLeon = new List<Item> { spray, pistola };
        var cantidadLeon = new List<int> { 4, 1 };
        
        var inventarioTienda = new List<Item> { granada, colgante, magnum, escopeta, spray };
        var cantidadTienda = new List<int> { 3, 2, 2, 1, 3 };

        Leon leon = new Leon(inventarioLeon, cantidadLeon, 200000);
        Vendedor vendedor = new Vendedor(inventarioTienda, cantidadTienda, 100000);

        bool salir = false;
        while (!salir) {
            Console.WriteLine("\n------ MENÚ PRINCIPAL ------");
            Console.WriteLine("1. Comprar");
            Console.WriteLine("2. Vender");
            Console.WriteLine("3. Mejorar pistolas");
            Console.WriteLine("4. Salir");
            Console.Write("Elige: ");
            
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int opcion)) continue;

            switch (opcion) {
                case 1:
                    vendedor.Vender(leon);
                    break;
                case 2:
                    leon.Vender(vendedor);
                    break;
                case 3:
                    vendedor.Mejorar(leon);
                    break;
                case 4:
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        }
    }
}