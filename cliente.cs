 using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Cliente
{
    public string Nombre { get; set; }
    public decimal LimiteCredito { get; set; } = 1000000m; // Límite de crédito por defecto
    public DateTime FechaCumpleanos { get; set; }
    public bool DescuentoCumpleanosOtorgado { get; set; } = false;
    public List<Factura> Facturas { get; set; } = new List<Factura>();

    // Método para cargar clientes desde un archivo CSV
    public static List<Cliente> CargarClientes(string rutaArchivo)
    {
        var clientes = new List<Cliente>();
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
                        var cliente = new Cliente
                        {
                            Nombre = columnas[0],
                            LimiteCredito = decimal.Parse(columnas[1], CultureInfo.InvariantCulture),
                            FechaCumpleanos = DateTime.ParseExact(columnas[2], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            DescuentoCumpleanosOtorgado = bool.Parse(columnas[3])
                        };
                        clientes.Add(cliente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al cargar el cliente en la línea {i + 1}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Datos incompletos en la línea {i + 1}. Saltando...");
                }
            }
            Console.WriteLine("Clientes cargados exitosamente.\n");
        }
        else
        {
            Console.WriteLine("Archivo de clientes no encontrado. Se iniciará con lista de clientes vacía.\n");
        }
        return clientes;
    }

    // Método para guardar clientes en un archivo CSV
    public static void GuardarClientes(List<Cliente> clientes, string rutaArchivo)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                writer.WriteLine("Nombre,LimiteCredito,FechaCumpleanos,DescuentoCumpleanosOtorgado"); // Encabezados
                foreach (var cliente in clientes)
                {
                    writer.WriteLine($"{cliente.Nombre},{cliente.LimiteCredito.ToString(CultureInfo.InvariantCulture)},{cliente.FechaCumpleanos.ToString("dd/MM/yyyy")},{cliente.DescuentoCumpleanosOtorgado}");
                }
            }
            Console.WriteLine("Clientes guardados exitosamente.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar clientes: {ex.Message}\n");
        }
    }

    // Método para validar cumpleaños y otorgar descuento
    public static void ValidarCumpleanos(Cliente cliente)
    {
        if (!cliente.DescuentoCumpleanosOtorgado && cliente.FechaCumpleanos.Date == DateTime.Now.Date)
        {
            cliente.DescuentoCumpleanosOtorgado = true;
            // Aplicar descuento
            Console.WriteLine($"¡Feliz Cumpleaños, {cliente.Nombre}! Has recibido un descuento.");
        }
    }

    // Método para abonar cuenta
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

    // Método para cancelar cuenta
    public void CancelarCuenta(Factura factura)
    {
        factura.Estado = "Cancelada";
        Console.WriteLine($"La cuenta {factura.NumeroFactura} ha sido cancelada.");
    }
}
