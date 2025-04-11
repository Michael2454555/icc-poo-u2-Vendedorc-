// Objeto.cs
public class Objeto : Item {
    public string Nombre { get; }
    public double PCompra { get; }
    public double PVenta { get; }

    public Objeto(string nombre, double pCompra) {
        Nombre = nombre;
        PCompra = pCompra;
        PVenta = pCompra * 1.25;
    }

    public string GetNombre() => Nombre;
    public double GetPCompra() => PCompra;
    public double GetPVenta() => PVenta;

    public void invVendedor(List<Item> inventario, List<int> cantidad) {
        int indice = inventario.IndexOf(this);
        if (indice == -1) return;
        Console.WriteLine($"{Nombre} x{cantidad[indice]} ({PCompra})");
    }

    public void invLeon(List<Item> inventario, List<int> cantidad) {
        int indice = inventario.IndexOf(this);
        if (indice == -1) return;
        Console.WriteLine($"{Nombre} x{cantidad[indice]} ({PVenta})");
    }

    public override bool Equals(object obj) => obj is Objeto other && Nombre == other.Nombre;
    public override int GetHashCode() => Nombre.GetHashCode();
}