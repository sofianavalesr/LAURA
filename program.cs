using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Cliente> clientes = new List<Cliente>();
        Inventario inventario = new Inventario();
        IODatos ioDatos = new IODatos();
        List<Factura> facturas = new List<Factura>();

        // Cargar inventario desde un archivo CSV
        inventario.CargarInventario("inventario.csv");

        // Agregar productos de ejemplo si el inventario está vacío
        if (inventario.Productos.Count == 0)
        {
            inventario.Productos.Add(new Producto { Id = 1, Nombre = "Pizza", Precio = 10.00m, Cantidad = 50 });
            inventario.Productos.Add(new Producto { Id = 2, Nombre = "Hamburguesa", Precio = 8.00m, Cantidad = 30 });
            inventario.Productos.Add(new Producto { Id = 3, Nombre = "Ensalada", Precio = 5.50m, Cantidad = 20 });
            // Puedes agregar más productos según sea necesario
        }

        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("=== Menú Principal ===");
            Console.WriteLine("1. Agregar Cliente");
            Console.WriteLine("2. Crear Factura");
            Console.WriteLine("3. Imprimir Estado de Mesas");
            Console.WriteLine("4. Ver Inventario");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AgregarCliente(clientes);
                    break;
                case "2":
                    CrearFactura(clientes, inventario.Productos, facturas, inventario);
                    break;
                case "3":
                    ImprimirEstadoMesas(facturas);
                    break;
                case "4":
                    inventario.ImprimirEstadoInventario();
                    break;
                case "5":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }

        // Guardar inventario al salir
        inventario.GuardarInventario("inventario.csv");
        Console.WriteLine("Inventario guardado.");
    }

    private static void AgregarCliente(List<Cliente> clientes)
    {
        Console.Write("Ingrese el nombre del cliente: ");
        string nombre = Console.ReadLine();

        DateTime fechaCumpleanos;
        while (true)
        {
            Console.Write("Ingrese la fecha de cumpleaños (dd/mm/yyyy): ");
            string input = Console.ReadLine();
            if (DateTime.TryParse(input, out fechaCumpleanos))
            {
                break;
            }
            else
            {
                Console.WriteLine("Fecha inválida. Por favor, intente de nuevo.");
            }
        }

        clientes.Add(new Cliente { Nombre = nombre, FechaCumpleanos = fechaCumpleanos });
        Console.WriteLine("Cliente agregado.\n");
    }

    private static void CrearFactura(List<Cliente> clientes, List<Producto> productos, List<Factura> facturas, Inventario inventario)
    {
        Console.Write("Ingrese el número de la mesa: ");
        if (!int.TryParse(Console.ReadLine(), out int numeroMesa))
        {
            Console.WriteLine("Número de mesa inválido.");
            return;
        }

        Console.Write("Seleccione un cliente por su nombre: ");
        string nombreCliente = Console.ReadLine();
        var cliente = clientes.Find(c => c.Nombre.Equals(nombreCliente, StringComparison.OrdinalIgnoreCase));

        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        var factura = new Factura(inventario.Productos)
        {
            NumeroFactura = facturas.Count + 1,
            NumeroMesa = numeroMesa,
            Cliente = cliente,
            Fecha = DateTime.Now
        };

        bool agregarProductos = true;
        while (agregarProductos)
        {
            Console.WriteLine("\nProductos disponibles:");
            foreach (var producto in productos)
            {
                Console.WriteLine($"- {producto.Nombre} (Precio: {producto.Precio:C}, Cantidad: {producto.Cantidad})");
            }

            Console.Write("Ingrese el nombre del producto a agregar (o 'salir' para finalizar): ");
            string nombreProducto = Console.ReadLine();

            if (nombreProducto.ToLower() == "salir")
            {
                agregarProductos = false;
                continue;
            }

            var productoSeleccionado = productos.Find(p => p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));
            if (productoSeleccionado == null)
            {
                Console.WriteLine("Producto no encontrado.");
                continue;
            }

            Console.Write("Ingrese la cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad inválida.");
                continue;
            }

            factura.AgregarProducto(productoSeleccionado, cantidad);
            Console.WriteLine("Producto agregado a la factura.");
        }

        facturas.Add(factura);
        factura.ImprimirFactura();
        Console.WriteLine();
    }

    private static void ImprimirEstadoMesas(List<Factura> facturas)
    {
        Console.WriteLine("\n=== Estado de Mesas ===");
        foreach (var factura in facturas)
        {
            Console.WriteLine($"Mesa {factura.NumeroMesa} - Estado: {factura.Estado} | Total: {factura.Total:C}");
        }
        Console.WriteLine();
    }
}
