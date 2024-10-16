using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Inventario
{
    public List<Producto> Productos { get; set; } = new List<Producto>();

    public void ImprimirEstadoInventario()
    {
        Producto.ImprimirEstadoInventario(Productos);
    }

    public void GuardarInventario(string rutaArchivo)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                writer.WriteLine("Id,Nombre,Precio,Cantidad");
                foreach (var producto in Productos)
                {
                    writer.WriteLine($"{producto.Id},{producto.Nombre},{producto.Precio.ToString(CultureInfo.InvariantCulture)},{producto.Cantidad}");
                }
            }
            Console.WriteLine("Inventario guardado exitosamente.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar inventario: {ex.Message}\n");
        }
    }

    public void CargarInventario(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            var lineas = File.ReadAllLines(rutaArchivo);
            for (int i = 1; i < lineas.Length; i++) // Saltar el encabezado
            {
                var columnas = lineas[i].Split(',');
                if (columnas.Length >= 4)
                {
                    try
                    {
                        var producto = new Producto
                        {
                            Id = int.Parse(columnas[0]),
                            Nombre = columnas[1],
                            Precio = decimal.Parse(columnas[2], CultureInfo.InvariantCulture),
                            Cantidad = int.Parse(columnas[3])
                        };
                        Productos.Add(producto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al cargar el producto en la línea {i + 1}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Datos incompletos en la línea {i + 1}. Saltando...");
                }
            }
            Console.WriteLine("Inventario cargado exitosamente.\n");
        }
        else
        {
            Console.WriteLine("Archivo de inventario no encontrado. Se iniciará con inventario vacío.\n");
        }
    }

    // Nueva función para actualizar el inventario
    public void ActualizarInventario(Producto producto, int cantidadVendida)
    {
        if (producto != null)
        {
            if (producto.Cantidad >= cantidadVendida)
            {
                producto.Cantidad -= cantidadVendida;
                Console.WriteLine($"Inventario actualizado: {producto.Nombre} ahora tiene {producto.Cantidad} unidades.");

                if (producto.Cantidad == 0)
                {
                    Console.WriteLine($"¡Alerta! El producto '{producto.Nombre}' se ha agotado.");
                }
                else if (producto.Cantidad <= 5) // Umbral para "próximo a acabarse"
                {
                    Console.WriteLine($"¡Alerta! El producto '{producto.Nombre}' está próximo a agotarse. Cantidad restante: {producto.Cantidad}");
                }
            }
            else
            {
                Console.WriteLine($"No hay suficiente stock de {producto.Nombre}. Disponibles: {producto.Cantidad}");
            }
        }
    }

    // Nueva función para restaurar inventario en caso de cancelación de factura o devolución de productos
    public void RestaurarInventario(Producto producto, int cantidadDevuelta)
    {
        if (producto != null)
        {
            producto.Cantidad += cantidadDevuelta;
            Console.WriteLine($"Inventario restaurado: {producto.Nombre} ahora tiene {producto.Cantidad} unidades.");
        }
    }
}

