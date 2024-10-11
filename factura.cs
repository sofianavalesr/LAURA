using System;
using System.Collections.Generic;
using System.Linq;

public class Factura
{
    public int NumeroFactura { get; set; }
    public int NumeroMesa { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Cuenta abierta";
    public List<ItemFactura> Items { get; set; } = new List<ItemFactura>();
    public decimal Total { get; private set; }

    // Referencia al inventario para actualizarlo
    private List<Producto> inventario;

    public Factura(List<Producto> inventario)
    {
        this.inventario = inventario;
    }

    public void AgregarProducto(Producto producto, int cantidad)
    {
        if (cantidad > 0 && producto.Cantidad >= cantidad)
        {
            Items.Add(new ItemFactura { Producto = producto, Cantidad = cantidad });
            Producto.ActualizarInventario(inventario, producto.Id, cantidad);
            CalcularTotales();
        }
        else
        {
            Console.WriteLine("Cantidad no válida o stock insuficiente.");
        }
    }

    public void CalcularTotales()
    {
        Total = Items.Sum(i => i.Producto.Precio * i.Cantidad);
    }

    public void ImprimirFactura()
    {
        Console.WriteLine($"\nFactura N°: {NumeroFactura} | Mesa: {NumeroMesa} | Cliente: {Cliente.Nombre} | Fecha: {Fecha}");
        foreach (var item in Items)
        {
            Console.WriteLine($"- {item.Producto.Nombre} x {item.Cantidad} = {item.Producto.Precio * item.Cantidad:C}");
        }
        Console.WriteLine($"Total: {Total:C}\n");
    }

    public void ActualizarTotal(decimal monto)
    {
        Total += monto; // Actualiza el total con el monto dado
    }
}
