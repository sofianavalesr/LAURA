using System;

public class IU
{
    public void Logo()
    {
        console.Writeline(" ██████╗ ██████╗ ███╗   ███╗██╗     ██╗██╗████████╗ █████╗ ███████╗")
        console.Writeline("██╔════╝██╔═══██╗████╗ ████║██║     ██║██║╚══██╔══╝██╔══██╗██╔════╝")
        console.Writeline("██║     ██║   ██║██╔████╔██║██║╔██████║██║   ██║   ███████║███████╗")
        console.Writeline("██║     ██║   ██║██║╚██╔╝██║██║██══╗██║██║   ██║   ██╔══██║╚════██║")
        console.Writeline("╚██████╗╚██████╔╝██║ ╚═╝ ██║██║╚██████║██║   ██║   ██║  ██║███████║")
        console.Writeline(" ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═╝ ╚═════╝╚═╝   ╚═╝   ╚═╝  ╚═╝╚══════╝")
    }

    public void OpcionesPrograma()
    {
        Console.WriteLine("1. Ver productos");
        Console.WriteLine("2. Hacer reserva");
        Console.WriteLine("3. Mostrar reservas");
        Console.WriteLine("4. Editar producto en reserva");
        Console.WriteLine("5. Hacer otra reserva");
        Console.WriteLine("6. Mostrar facturas");
        Console.WriteLine("7. Editar producto en menú");
        Console.WriteLine("8. Guardar inventario");
        Console.WriteLine("9. Realizar analítica");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
    }
}
