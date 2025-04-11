// Leon.cs
public class Leon {
    public List<Item> Inventario { get; }
    public List<int> Cantidad { get; }
    public double Dinero { get; set; }

    public Leon(List<Item> inventario, List<int> cantidad, double dinero) {
        Inventario = inventario;
        Cantidad = cantidad;
        Dinero = dinero;
    }

    public void Vender(Vendedor vendedor) {
        bool salir = false;
        while (!salir) {
            Console.WriteLine("\n----------* INVENTARIO *----------");
            Console.WriteLine($"Dinero: {Dinero}");
            
            for (int i = 0; i < Inventario.Count; i++) {
                Console.Write($"{i + 1}. ");
                Inventario[i].invLeon(Inventario, Cantidad);
            }
            
            Console.WriteLine($"{Inventario.Count + 1}. Volver");
            Console.Write("Vender: ");
            
            if (!int.TryParse(Console.ReadLine(), out int opcion)) continue;

            if (opcion == Inventario.Count + 1) {
                salir = true;
            }
            else if (opcion >= 1 && opcion <= Inventario.Count) {
                Item item = Inventario[opcion - 1];
                int indice = opcion - 1;
                
                if (Cantidad[indice] > 0) {
                    Dinero += (item is Objeto) ? ((Objeto)item).GetPVenta() : ((Arma)item).GetPVenta();
                    Cantidad[indice]--;
                    
                    if (Cantidad[indice] == 0) {
                        Inventario.RemoveAt(indice);
                        Cantidad.RemoveAt(indice);
                    }
                    
                    vendedor.AgregarItem(item);
                    Console.WriteLine($"\n¡Vendido {item.GetNombre()}!");
                }
            }
        }
    }
}