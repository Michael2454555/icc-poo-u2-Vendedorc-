// Item.cs
public interface Item {
    void invVendedor(List<Item> inventario, List<int> cantidad);
    void invLeon(List<Item> inventario, List<int> cantidad);
    string GetNombre();
    double GetPCompra();
    double GetPVenta();
}