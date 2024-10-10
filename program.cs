using System;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        List<Cliente> clientes = new List<Cliente>();
        List<Producto> productos = new List<Producto>();
        List<Factura> facturas = new List<Factura>();
        // Agregar productos de ejemplo
        productos.Add(new Producto { Id = 1, Nombre = "Pizza", Precio = 10.00m, Cantidad = 50 });
        productos.Add(new Producto { Id = 2, Nombre = "Hamburguesa", Precio = 8.00m, Cantidad = 30 });


        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("=== Menú Principal ===");
            Console.WriteLine("1. Agregar Cliente");
            Console.WriteLine("2. Crear Factura");
            Console.WriteLine("3. Imprimir Estado de Mesas");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");


            switch (Console.ReadLine())
            {
                case "1":
                    AgregarCliente(clientes);
                    break;
                case "2":
                    CrearFactura(clientes, productos, facturas);
                    break;
                case "3":
                    ImprimirEstadoMesas(facturas);
                    break;
                case "4":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }


    private static void AgregarCliente(List<Cliente> clientes)
    {
        Console.Write("Ingrese el nombre del cliente: ");
        string nombre = Console.ReadLine();
        Console.Write("Ingrese la fecha de cumpleaños (dd/mm/yyyy): ");
        DateTime fechaCumpleanos = DateTime.Parse(Console.ReadLine());


        clientes.Add(new Cliente { Nombre = nombre, FechaCumpleanos = fechaCumpleanos });
        Console.WriteLine("Cliente agregado.");
    }


    private static void CrearFactura(List<Cliente> clientes, List<Producto> productos, List<Factura> facturas)
    {
        Console.Write("Ingrese el número de la mesa: ");
        int numeroMesa = int.Parse(Console.ReadLine());


        Console.Write("Seleccione un cliente por su nombre: ");
        string nombreCliente = Console.ReadLine();
        var cliente = clientes.Find(c => c.Nombre == nombreCliente);


        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }


        var factura = new Factura
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


            if (nombreProducto.ToLower() == "salir")
            {
                agregarProductos = false;
                continue;
            }


            var productoSeleccionado = productos.Find(p => p.Nombre == nombreProducto);
            if (productoSeleccionado == null)
            {
                Console.WriteLine("Producto no encontrado.");
                continue;
            }


            Console.Write("Ingrese la cantidad: ");
            int cantidad = int.Parse(Console.ReadLine());
            factura.AgregarProducto(productoSeleccionado, cantidad);
            Console.WriteLine("Producto agregado a la factura.");
        }


        facturas.Add(factura);
        factura.ImprimirFactura();
    }


    private static void ImprimirEstadoMesas(List<Factura> facturas)
    {
        Console.WriteLine("=== Estado de Mesas ===");
        foreach (var factura in facturas)
        {
            Console.WriteLine($"Mesa {factura.NumeroMesa} - Estado: {factura.Estado} | Total: {factura.Total:C}");
        }
    }
}


