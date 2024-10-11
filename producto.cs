using System;
using System.Collections.Generic;
using System.IO;


public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string Categoria { get; set; }


    public static List<Producto> CargarProductos(string rutaArchivo)
    {
        var productos = new List<Producto>();
        if (File.Exists(rutaArchivo))
        {
            var lineas = File.ReadAllLines(rutaArchivo);
            foreach (var linea in lineas)
            {
                var partes = linea.Split(';');
                if (partes.Length >= 5)
                {
                    var producto = new Producto
                    {
                        Id = int.Parse(partes[0]),
                        Nombre = partes[1],
                        Precio = decimal.Parse(partes[2]),
                        Cantidad = int.Parse(partes[3]),
                        Categoria = partes[4]
                    };
                    productos.Add(producto);
                }
            }
        }
        return productos;
    }


    public static void GuardarProductos(string rutaArchivo, List<Producto> productos)
    {
        using (var writer = new StreamWriter(rutaArchivo))
        {
            foreach (var producto in productos)
            {
                writer.WriteLine($"{producto.Id};{producto.Nombre};{producto.Precio};{producto.Cantidad};{producto.Categoria}");
            }
        }
    }


    public static void ActualizarInventario(List<Producto> productos, int idProducto, int cantidadVendida)
    {
        var producto = productos.Find(p => p.Id == idProducto);
        if (producto != null)
        {
            producto.Cantidad = Math.Max(0, producto.Cantidad - cantidadVendida);
        }
    }


    public static void ImprimirEstadoInventario(List<Producto> productos)
    {
        Console.WriteLine("\n--- Estado del Inventario ---");
        foreach (var producto in productos)
        {
            Console.WriteLine($"{producto.Nombre}: {producto.Cantidad} unidades disponibles.");
            if (producto.Cantidad == 0)
            {
                Console.WriteLine($"Alerta: {producto.Nombre} está agotado.");
            }
            else if (producto.Cantidad < 5)
            {
                Console.WriteLine($"Advertencia: {producto.Nombre} está a punto de agotarse.");
            }
        }
    }
}
