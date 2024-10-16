using System;
using System.Collections.Generic;

public class Orden
{
    public int NumeroMesa { get; set; }
    public Cliente Cliente { get; set; }
    public List<ItemFactura> Items { get; set; } = new List<ItemFactura>();
    public decimal Total { get; set; }

    public void AgregarItem(ItemFactura item)
    {
        Items.Add(item);
        Total += item.Producto.Precio * item.Cantidad;
    }

   public void ImprimirTirilla()
{
    Console.WriteLine("╔════════════════════════════════════════╗")
    Console.WriteLine("║         Factura de Venta               ║");
    Console.WriteLine("╠════════════════════════════════════════╣");
    Console.WriteLine("║  Producto         | Cantidad |  Precio ║");
    Console.WriteLine("║  ------------------------------------  ║");
    
    foreach (var item in Items)
    {
        // Ajusta el formato de la línea de producto
        Console.WriteLine($"║  {item.Producto.Nombre,-16} | {item.Cantidad,8} | {item.Producto.Precio * item.Cantidad,8:C} ║");
    }

    Console.WriteLine("║  ------------------------------------  ║");
    Console.WriteLine($"║  Total:                    {Total,8:C}      ║");
    Console.WriteLine("╚════════════════════════════════════════╝");
    Console.WriteLine("       ¡Gracias por su compra!  ");
}

}
