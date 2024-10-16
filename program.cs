using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string rutaInventario = "inventario.csv";  
        string rutaClientes = "clientes.csv"; 

        // Cargar clientes desde un archivo CSV
        List<Cliente> clientes = Cliente.CargarClientes(rutaClientes);

        Inventario inventario = new Inventario();
        IODatos ioDatos = new IODatos();
        List<Factura> facturas = new List<Factura>();

        // Cargar inventario desde un archivo CSV
        inventario.CargarInventario(rutaInventario);

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
            
            Console.Write("Seleccione una opción: ");
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║           Menú Principal           ║");
            Console.WriteLine("╠════════════════════════════════════╣");
            Console.WriteLine("║        1. Agregar Cliente          ║");
            Console.WriteLine("║        2. Crear Factura            ║");
            Console.WriteLine("║        3. Imprimir Cuentas         ║");
            Console.WriteLine("║        Pendientes por Pagar        ║");
            Console.WriteLine("║        4. Ver Inventario           ║");
            Console.WriteLine("║        5. Abonar Cuenta            ║");
            Console.WriteLine("║        6. Cancelar Cuenta          ║");
            Console.WriteLine("║        7. Salir                    ║");
            Console.Writeline("╚════════════════════════════════════╝ ");

            string opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    AgregarCliente(clientes, rutaClientes);
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
                    AbonarCuenta(clientes, facturas);
                    break;
                case "6":
                    CancelarCuenta(clientes, facturas);
                    break;
                case "7":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.\n");
                    break;
            }
        }

        // Guardar inventario y clientes al salir
        inventario.GuardarInventario(rutaInventario);
        Cliente.GuardarClientes(clientes, rutaClientes);
        Console.WriteLine("Inventario y clientes guardados. ¡Hasta luego!");
    }

    private static void AgregarCliente(List<Cliente> clientes, string rutaClientes)
    {
        Console.Write("Ingrese el nombre del cliente: ");
        string nombre = Console.ReadLine();

        DateTime fechaCumpleanos;
        while (true)
        {
            Console.Write("Ingrese la fecha de cumpleaños (dd/mm/yyyy): ");
            string input = Console.ReadLine();
            if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fechaCumpleanos))
            {
                break;
            }
            else
            {
                Console.WriteLine("Fecha inválida. Por favor, ingrese en el formato dd/mm/yyyy.\n");
            }
        }

        clientes.Add(new Cliente { Nombre = nombre, FechaCumpleanos = fechaCumpleanos });
        Console.WriteLine("Cliente agregado.\n");

        // Guardar inmediatamente después de agregar
        Cliente.GuardarClientes(clientes, rutaClientes);
    }

    private static void CrearFactura(List<Cliente> clientes, List<Producto> productos, List<Factura> facturas, Inventario inventario)
    {
        Console.Write("Ingrese el número de la mesa: ");
        if (!int.TryParse(Console.ReadLine(), out int numeroMesa))
        {
            Console.WriteLine("Número de mesa inválido.\n");
            return;
        }

        Console.Write("Seleccione un cliente por su nombre: ");
        string nombreCliente = Console.ReadLine();
        var cliente = clientes.Find(c => c.Nombre.Equals(nombreCliente, StringComparison.OrdinalIgnoreCase));

        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.\n");
            return;
        }

        // Validar si es el cumpleaños del cliente
        Cliente.ValidarCumpleanos(cliente);

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
            Console.WriteLine("Productos disponibles:");
            foreach (var producto in productos)
            {
                Console.WriteLine($"- {producto.Nombre} (Precio: {producto.Precio:C}, Cantidad: {producto.Cantidad})");
            }

            Console.Write("Ingrese el nombre del producto a agregar (o 'salir' para finalizar): ");
            string nombreProducto = Console.ReadLine();

            if (nombreProducto.Trim().ToLower() == "salir")
            {
                agregarProductos = false;
                Console.WriteLine();
                continue;
            }

            var productoSeleccionado = productos.Find(p => p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));
            if (productoSeleccionado == null)
            {
                Console.WriteLine("Producto no encontrado.\n");
                continue;
            }

            Console.Write("Ingrese la cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad inválida.\n");
                continue;
            }

            factura.AgregarProducto(productoSeleccionado, cantidad);
            Console.WriteLine("Producto agregado a la factura.\n");
        }

        facturas.Add(factura);
        factura.ImprimirFactura();
    }

    private static void ImprimirEstadoMesas(List<Factura> facturas)
{
    Console.WriteLine("\n=== Cuentas Pendientes por Pagar ===");
    bool hayPendientes = false;

    foreach (var factura in facturas)
    {
        if (factura.Estado == "Cuenta abierta")
        {
            Console.WriteLine($"Factura N°: {factura.NumeroFactura} | Mesa: {factura.NumeroMesa} | Cliente: {factura.Cliente.Nombre} | Total: {factura.Total:C}");
            hayPendientes = true;
        }
    }

    if (!hayPendientes)
    {
        Console.WriteLine("No hay cuentas pendientes por pagar.\n");
    }
    else
    {
        Console.WriteLine();
    }
}


        if (!hayPendientes)
        {
            Console.WriteLine("No hay cuentas pendientes por pagar.\n");
        }
        else
        {
            Console.WriteLine();
        }
    }

    private static void AbonarCuenta(List<Cliente> clientes, List<Factura> facturas)
    {
        Console.Write("Ingrese el número de factura a abonar: ");
        if (!int.TryParse(Console.ReadLine(), out int numeroFactura))
        {
            Console.WriteLine("Número de factura inválido.\n");
            return;
        }

        var factura = facturas.Find(f => f.NumeroFactura == numeroFactura && f.Estado != "Cancelada");
        if (factura == null)
        {
            Console.WriteLine("Factura no encontrada o ya cancelada.\n");
            return;
        }

        Console.Write("Ingrese el monto a abonar: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal monto) || monto <= 0)
        {
            Console.WriteLine("Monto inválido.\n");
            return;
        }

        var cliente = factura.Cliente;
        cliente.AbonarCuenta(monto, factura);
    }

    private static void CancelarCuenta(List<Cliente> clientes, List<Factura> facturas)
    {
        Console.Write("Ingrese el número de factura a cancelar: ");
        if (!int.TryParse(Console.ReadLine(), out int numeroFactura))
        {
            Console.WriteLine("Número de factura inválido.\n");
            return;
        }

        var factura = facturas.Find(f => f.NumeroFactura == numeroFactura && f.Estado != "Cancelada");
        if (factura == null)
        {
            Console.WriteLine("Factura no encontrada o ya cancelada.\n");
            return;
        }

        var cliente = factura.Cliente;
        cliente.CancelarCuenta(factura);
    }
}
