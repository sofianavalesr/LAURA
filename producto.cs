using System;
using System.Collections.Generic;
using System.Linq;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }

    // Método estático para actualizar el inventario
    public static void ActualizarInventario(List<Producto> productos, int idProducto, int cantidadVendida)
    {
        var producto = productos.FirstOrDefault(p => p.Id == idProducto);
        if (producto != null)
        {
            if (producto.Cantidad >= cantidadVendida)
            {
                producto.Cantidad -= cantidadVendida;
                Console.WriteLine($"Inventario actualizado: {producto.Nombre} ahora tiene {producto.Cantidad} unidades.");
            }
            else
            {
                Console.WriteLine($"No hay suficiente stock de {producto.Nombre}. Disponibles: {producto.Cantidad}");
            }
        }
        else
        {
            Console.WriteLine("Producto no encontrado en el inventario.");
        }
    }

    // Método para imprimir el estado del inventario
    public static void ImprimirEstadoInventario(List<Producto> productos)
    {
        Console.WriteLine("\n--- Estado del Inventario ---");
        foreach (var producto in productos)
        {
            Console.WriteLine($"ID: {producto.Id} | Nombre: {producto.Nombre} | Precio: {producto.Precio:C} | Cantidad: {producto.Cantidad}");
        }
        Console.WriteLine("------------------------------\n");
    }
}