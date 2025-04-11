// Vendedor.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class Vendedor {
    public List<Item> Inventario { get; }
    public List<int> Cantidad { get; }
    public double Dinero { get; set; }

    public Vendedor(List<Item> inventario, List<int> cantidad, double dinero) {
        Inventario = inventario;
        Cantidad = cantidad;
        Dinero = dinero;
    }

    public void AgregarItem(Item item) {
        int indice = Inventario.IndexOf(item);
        if (indice != -1) {
            Cantidad[indice]++;
        }
        else {
            Inventario.Add(item);
            Cantidad.Add(1);
        }
    }

    public void Vender(Leon leon) {
        bool salir = false;
        while (!salir) {
            Console.Clear();
            Console.WriteLine("\n----------* TIENDA *----------");
            Console.WriteLine($"Dinero: {leon.Dinero}$\n");
            
            int numItem = 1;
            var itemsConStock = new List<Item>();
            for (int i = 0; i < Inventario.Count; i++) {
                if (Cantidad[i] > 0) {
                    Console.Write($"{numItem++}. ");
                    Inventario[i].invVendedor(Inventario, Cantidad);
                    itemsConStock.Add(Inventario[i]);
                }
            }
                
            Console.WriteLine($"{Inventario.Count + 1}. Volver");
            Console.Write("Comprar: ");
            
            if (!int.TryParse(Console.ReadLine(), out int opcion)) continue;

            if (opcion == Inventario.Count + 1) {
                salir = true;
            }
            else if (opcion >= 1 && opcion <= Inventario.Count) {
                int indice = opcion - 1;
                Item item = Inventario[indice];
                
                if (Cantidad[indice] == 0) {
                    Console.WriteLine("\n¡Objeto agotado!");
                    continue;
                }
                
                if (leon.Dinero >= item.GetPCompra()) {
                    leon.Dinero -= item.GetPCompra();
                    Cantidad[indice]--;
                    
                    if (Cantidad[indice] == 0) {
                        Inventario.RemoveAt(indice);
                        Cantidad.RemoveAt(indice);
                    }
                    
                    if (item is Arma arma) {
                        leon.Inventario.Add(arma.Clone());
                    }
                    else {
                        leon.Inventario.Add(item);
                    }
                    leon.Cantidad.Add(1);
                    Console.WriteLine($"\n¡Comprado {item.GetNombre()}!");
                }
                else {
                    Console.WriteLine("\n¡Dinero insuficiente!");
                }
            }
        }
    }

    public void Mejorar(Leon leon) {
    bool salir = false;
    while (!salir) {
        Console.WriteLine("\n----------* MEJORAR *----------");
        Console.WriteLine($"Dinero: {leon.Dinero}");
        var pistolas = leon.Inventario
                          .OfType<Arma>()
                          .Where(a => a.GetNombre() == "Pistola")
                          .ToList();

        if (pistolas.Count == 0) {
            Console.WriteLine("¡No tienes pistolas!");
            return;
        }

        // Muestra el menú de mejoras (invMejora debe listar 1–4)
        pistolas[0].invMejora();
        Console.WriteLine("5. Volver");
        Console.Write("Selección: ");

        if (!int.TryParse(Console.ReadLine(), out int opcion))
            continue;

        if (opcion == 5) {
            salir = true;
        }
        else if (opcion >= 1 && opcion <= 4) {
            double costoTotal = 0;

            // Calcula el coste total según el nivel actual de cada pistola
            foreach (var pistola in pistolas) {
                int valorActual = 0, maximo = 0;

                switch (opcion) {
                    case 1:
                        valorActual = pistola.Dano;
                        maximo = 6;
                        break;
                    case 2:
                        valorActual = pistola.VelRecarga;
                        maximo = 3;
                        break;
                    case 3:
                        valorActual = pistola.Cadencia;
                        maximo = 6;
                        break;
                    case 4:
                        valorActual = pistola.Capacidad;
                        maximo = 3;
                        break;
                }

                if (valorActual < maximo) {
                    costoTotal += 10000 - (int)(8000 * (1 - (valorActual / (double)maximo)));
                }
            }

            if (costoTotal == 0) {
                Console.WriteLine("¡Atributo al máximo!");
                continue;
            }

            if (leon.Dinero < costoTotal) {
                Console.WriteLine($"¡Necesitas ${costoTotal - leon.Dinero} más!");
                continue;
            }

            // Aplica la mejora
            foreach (var pistola in pistolas) {
                switch (opcion) {
                    case 1 when pistola.Dano < 6:
                        pistola.Dano++;
                        break;
                    case 2 when pistola.VelRecarga < 3:
                        pistola.VelRecarga++;
                        break;
                    case 3 when pistola.Cadencia < 6:
                        pistola.Cadencia++;
                        break;
                    case 4 when pistola.Capacidad < 3:
                        pistola.Capacidad++;
                        break;
                }
            }

            leon.Dinero -= costoTotal;
            Console.WriteLine($"¡Mejora aplicada! (-${costoTotal})");
        }
    }
}

}