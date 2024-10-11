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
        Console.WriteLine($"--- Tirilla de Factura ---");
        Console.WriteLine($"Mesa: {NumeroMesa} | Cliente: {Cliente.Nombre}");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.Producto.Nombre} x {item.Cantidad} = {item.Producto.Precio * item.Cantidad:C}");
        }
        Console.WriteLine($"Total a pagar: {Total:C}");
    }
}
