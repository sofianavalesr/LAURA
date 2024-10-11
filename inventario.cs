using System.Collections.Generic;


public class Inventario
{
    public List<Producto> Productos { get; set; } = new List<Producto>();


    public void ImprimirEstadoInventario()
    {
        Producto.ImprimirEstadoInventario(Productos);
    }
}
