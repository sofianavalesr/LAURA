using System;
using System.Collections.Generic;


public class Cliente
{
    public string Nombre { get; set; }
    public decimal LimiteCredito { get; set; } = 1000000m; // Límite de crédito por defecto
    public DateTime FechaCumpleanos { get; set; }
    public bool DescuentoCumpleanosOtorgado { get; set; } = false;
    public List<Factura> Facturas { get; set; } = new List<Factura>();


    public static List<Cliente> CargarClientes(string rutaArchivo)
    {
        var clientes = new List<Cliente>();
        // Implementar lógica para cargar clientes desde CSV aquí
        return clientes;
    }


    public static void GuardarClientes(List<Cliente> clientes, string rutaArchivo)
    {
        // Implementar lógica para guardar clientes en CSV aquí
    }


    public static void ValidarCumpleanos(Cliente cliente)
    {
        if (!cliente.DescuentoCumpleanosOtorgado && cliente.FechaCumpleanos.Date == DateTime.Now.Date)
        {
            cliente.DescuentoCumpleanosOtorgado = true;
            // Aplicar descuento
            Console.WriteLine($"¡Feliz Cumpleaños, {cliente.Nombre}! Has recibido un descuento.");
        }
    }


    public void AbonarCuenta(decimal monto, Factura factura)
    {
        if (monto > 0 && monto <= LimiteCredito)
        {
            factura.ActualizarTotal(-monto); // Actualizar total en la factura
            LimiteCredito -= monto;
            Console.WriteLine($"Abono de {monto:C} realizado a la factura {factura.NumeroFactura}.");
        }
        else
        {
            Console.WriteLine("El monto debe ser mayor que cero y menor o igual al límite de crédito.");
        }
    }


    public void CancelarCuenta(Factura factura)
    {
        factura.Estado = "Cancelada";
        Console.WriteLine($"La cuenta {factura.NumeroFactura} ha sido cancelada.");
    }
}
