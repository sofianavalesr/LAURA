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


    public void AgregarProducto(Producto producto, int cantidad)
    {
        if (cantidad > 0 && producto.Cantidad >= cantidad)
        {
            Items.Add(new ItemFactura { Producto = producto, Cantidad = cantidad });
            Producto.ActualizarInventario(new List<Producto> { producto }, producto.Id, cantidad);
            CalcularTotales();
        }
    }
    public void AgregarProducto(Producto producto, int cantidad, Inventario inventario)
{
    if (cantidad > 0 && producto.Cantidad >= cantidad)
    {
        Items.Add(new ItemFactura { Producto = producto, Cantidad = cantidad });
        CalcularTotales();

        // Actualizar inventario después de agregar el producto a la factura
        inventario.ActualizarInventario(producto, cantidad);
    }
    else
    {
        Console.WriteLine("No hay suficiente stock o la cantidad no es válida.");
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
        Console.WriteLine($"Total: {Total:C}");
    }


    public void ActualizarTotal(decimal monto)
    {
        Total += monto; // Actualiza el total con el monto dado
    }
}
