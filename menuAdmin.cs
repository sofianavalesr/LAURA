using System;
using System.Collections.Generic;
using System.Linq;

public class MenuAdmin
{
    private readonly Inventario inventario;
    private readonly IODatos ioDatos;

    public MenuAdmin(Inventario inventario, IODatos ioDatos)
    {
        this.inventario = inventario;
        this.ioDatos = ioDatos;
    }

    public void VerProductos(int categoria)
    {
        var productosFiltrados = inventario.Productos; // Filtrar por categoría si es necesario
        foreach (var producto in productosFiltrados)
        {
            Console.WriteLine($"{producto.Id} - {producto.Nombre} - {producto.Precio:C} - {producto.Cantidad} unidades");
        }
    }

    public Producto BuscarProductoPorId(int id)
    {
        return inventario.Productos.FirstOrDefault(p => p.Id == id);
    }

    public void EditarProductoEnMenu(int idProd, string nuevoNombre, decimal nuevoPrecio)
    {
        var producto = BuscarProductoPorId(idProd);
        if (producto != null)
        {
            producto.Nombre = nuevoNombre;
            producto.Precio = nuevoPrecio;
            Console.WriteLine("Producto editado con éxito.");
        }
        else
        {
            Console.WriteLine("Producto no encontrado.");
        }
    }
}
