// Arma.cs
public class Arma : Item {
    private Objeto _objeto;
    public int Dano { get; set; }
    public int VelRecarga { get; set; }
    public int Cadencia { get; set; }
    public int Capacidad { get; set; }

    public Arma(string nombre, double pCompra, int dano, int velRecarga, int cadencia, int capacidad) {
        _objeto = new Objeto(nombre, pCompra);
        Dano = dano;
        VelRecarga = velRecarga;
        Cadencia = cadencia;
        Capacidad = capacidad;
    }

    public string GetNombre() => _objeto.GetNombre();
    public double GetPCompra() => _objeto.GetPCompra();
    public double GetPVenta() => _objeto.GetPVenta();

    public Arma Clone() => new Arma(GetNombre(), GetPCompra(), Dano, VelRecarga, Cadencia, Capacidad);

    public void invVendedor(List<Item> inventario, List<int> cantidad) {
        int indice = inventario.IndexOf(this);
        if (indice == -1) return;
        
        Console.WriteLine($"{GetNombre()} x{cantidad[indice]} ({GetPCompra()})");
        Console.WriteLine(" Daño   Vel.Recarga Cadencia Capacidad");
        MostrarBarras();
    }

    public void invLeon(List<Item> inventario, List<int> cantidad) {
        int indice = inventario.IndexOf(this);
        if (indice == -1) return;
        
        Console.Write($"{GetNombre()} x{cantidad[indice]} ({GetPVenta()}) ");
        MostrarBarras();
    }

    private void MostrarBarras() {
        Console.WriteLine($"|{new string('|', Math.Min(Dano, 6))}{new string('-', Math.Max(0, 6 - Dano))}| " +
            $"|{new string('|', Math.Min(VelRecarga, 3))}{new string('-', Math.Max(0, 3 - VelRecarga))}| " +
            $"|{new string('|', Math.Min(Cadencia, 6))}{new string('-', Math.Max(0, 6 - Cadencia))}| " +
            $"|{new string('|', Math.Min(Capacidad, 3))}{new string('-', Math.Max(0, 3 - Capacidad))}|");
    }

    public void invMejora() {
        Console.WriteLine($"\n=== MEJORAR {GetNombre().ToUpper()} ===");
        Console.WriteLine(" Atributo    | Progreso       | Costo");
        
        MostrarOpcionMejora("1. Daño", Dano, 6);
        MostrarOpcionMejora("2. Recarga", VelRecarga, 3);
        MostrarOpcionMejora("3. Cadencia", Cadencia, 6);
        MostrarOpcionMejora("4. Capacidad", Capacidad, 3);
    }

    private void MostrarOpcionMejora(string nombre, int valor, int max) {
        string barras = $"{new string('█', valor)}{new string('─', max - valor)}";
        double costo = (valor >= max) ? 0 : 10000 * (1 - (valor / (double)max));
        
        Console.WriteLine($" {nombre,-10} | [{barras,-10}] | " + 
            $"{(valor >= max ? " MAX " : $"{costo:0}$")}");
    }
}