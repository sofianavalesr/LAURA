using System.Collections.Generic;
using System.IO;

public class Inventario
{
    public List<Producto> Productos { get; set; } = new List<Producto>();

    public void ImprimirEstadoInventario()
    {
        Producto.ImprimirEstadoInventario(Productos);
    }

    // Método para guardar el inventario en un archivo CSV
    public void GuardarInventario(string rutaArchivo)
    {
        using (StreamWriter writer = new StreamWriter(rutaArchivo))
        {
            writer.WriteLine("Id,Nombre,Precio,Cantidad"); // Encabezados
            foreach (var producto in Productos)
            {
                writer.WriteLine($"{producto.Id},{producto.Nombre},{producto.Precio},{producto.Cantidad}");
            }
        }
    }

    // Método para cargar el inventario desde un archivo CSV
    public void CargarInventario(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            var lineas = File.ReadAllLines(rutaArchivo);
            for (int i = 1; i < lineas.Length; i++) // Saltar el encabezado
            {
                var columnas = lineas[i].Split(',');
                var producto = new Producto
                {
                    Id = int.Parse(columnas[0]),
                    Nombre = columnas[1],
                    Precio = decimal.Parse(columnas[2]),
                    Cantidad = int.Parse(columnas[3])
                };
                Productos.Add(producto);
            }
            Console.WriteLine("Inventario cargado exitosamente.");
        }
        else
        {
            Console.WriteLine("Archivo de inventario no encontrado. Se iniciará con inventario vacío.");
        }
    }
}
