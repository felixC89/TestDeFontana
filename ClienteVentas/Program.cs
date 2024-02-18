using ClienteVentas.Services.Implementaciones;
using ClienteVentas.Services.Interfaces;

namespace ClienteVentas
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            await MainAsync();
        }

        static async Task MainAsync()
        {
            Console.WriteLine("Cargando la aplicación");

            MostrarMensajeCargando();
            IVentasService services = new VentasService();
            var ventas = await services.GetVentasAsync(30);
            MostrarMensajeCargando();
            var ventasdetalle = await services.GetVentasDetallesAsync();
            MostrarMensajeCargando();
            var producto = await services.GetProductosAsync();
            MostrarMensajeCargando();
            var marcas = await services.GetMarcaAsync();
            MostrarMensajeCargando();
            var local = await services.GetLocalAsync();
            MostrarMensajeCargando();

            Console.WriteLine("\n\n¡La aplicación ha cargado completamente!");
            Thread.Sleep(1000);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Menú de la aplicación:");
                Console.WriteLine("1. El total de ventas de los últimos 30 días");
                Console.WriteLine("2. El día y hora en que se realizó la venta con el monto más alto");
                Console.WriteLine("3. Indicar cuál es el producto con mayor monto total de ventas");
                Console.WriteLine("4. Indicar el local con mayor monto de ventas");
                Console.WriteLine("5. ¿Cuál es la marca con mayor margen de ganancias?");
                Console.WriteLine("6. ¿cuál es el producto que más se vende en cada local?");
                Console.WriteLine("0. Salir");

                Console.Write("Selecciona una opción: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Opcion1();
                        break;

                    case "2":
                        Opcion2();
                        break;

                    case "3":
                        Opcion3();
                        break;
                    case "4":
                        Opcion4();
                        break;
                    case "5":
                        Opcion5();
                        break;
                    case "6":
                        Opcion6();
                        break;

                    case "0":
                        Console.WriteLine("Saliendo de la aplicación...");
                        return;

                    default:
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        break;
                }

                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }

            void Opcion1()
            {
                var resultado = (from vd in ventasdetalle
                                 join v in ventas on vd.IdVenta equals v.IdVenta
                                 join l in local on v.IdLocal equals l.IdLocal
                                 join p in producto on vd.IdProducto equals p.IdProducto
                                 join m in marcas on p.IdMarca equals m.IdMarca
                                 group new { vd.TotalLinea, vd.Cantidad } by 1 into g
                                 select new
                                 {
                                     Suma_TotalLinea = g.Sum(x => x.TotalLinea),
                                     Suma_Cantidad = g.Sum(x => x.Cantidad)
                                 }).FirstOrDefault();

                Console.WriteLine("\n");
                Console.WriteLine($"--> El monto total de ventas es:{resultado?.Suma_TotalLinea.ToString("C")} y la cantidad de ventas es:{resultado?.Suma_Cantidad}");

            }

            void Opcion2()
            {
                var resultado = (from vd in ventasdetalle
                                 join v in ventas on vd.IdVenta equals v.IdVenta
                                 join l in local on v.IdLocal equals l.IdLocal
                                 join p in producto on vd.IdProducto equals p.IdProducto
                                 join m in marcas on p.IdMarca equals m.IdMarca
                                 orderby vd.TotalLinea descending
                                 select new
                                 {
                                     v.Fecha,
                                     vd.TotalLinea
                                 }).FirstOrDefault();

                Console.WriteLine("\n");
                Console.WriteLine($"--> El día y hora {resultado?.Fecha.ToString()} se realizó la venta con el monto más alto de: {resultado?.TotalLinea.ToString("C")}");

            }

            void Opcion3()
            {
                var resultado = (from vd in ventasdetalle
                                 join v in ventas on vd.IdVenta equals v.IdVenta
                                 join l in local on v.IdLocal equals l.IdLocal
                                 join p in producto on vd.IdProducto equals p.IdProducto
                                 join m in marcas on p.IdMarca equals m.IdMarca
                                 group vd by new { p.Nombre } into g
                                 orderby g.Sum(x => x.TotalLinea) descending
                                 select new
                                 {
                                     NombreProd = g.Key.Nombre,
                                     Suma_TotalLinea = g.Sum(x => x.TotalLinea)
                                 }).FirstOrDefault();

                Console.WriteLine("\n");
                Console.WriteLine($"--> El producto con mayor monto total de ventas es: {resultado?.NombreProd}");

            }

            void Opcion4()
            {
                var resultado = (from vd in ventasdetalle
                                 join v in ventas on vd.IdVenta equals v.IdVenta
                                 join l in local on v.IdLocal equals l.IdLocal
                                 join p in producto on vd.IdProducto equals p.IdProducto
                                 join m in marcas on p.IdMarca equals m.IdMarca
                                 group vd by new { l.Nombre } into g
                                 orderby g.Sum(x => x.TotalLinea) descending
                                 select new
                                 {
                                     NombreLocal = g.Key.Nombre,
                                     Suma_TotalLinea = g.Sum(x => x.TotalLinea)
                                 }).FirstOrDefault();

                Console.WriteLine("\n");
                Console.WriteLine($"--> El local con mayor monto de ventas es:{resultado?.NombreLocal} con {resultado?.Suma_TotalLinea.ToString("C")}");

            }

            void Opcion5()
            {

                var resultado = (from vd in ventasdetalle
                                 join v in ventas on vd.IdVenta equals v.IdVenta
                                 join l in local on v.IdLocal equals l.IdLocal
                                 join p in producto on vd.IdProducto equals p.IdProducto
                                 join m in marcas on p.IdMarca equals m.IdMarca
                                 group vd by new { m.Nombre } into g
                                 orderby g.Sum(x => x.TotalLinea) descending
                                 select new
                                 {
                                     NombreMarca = g.Key.Nombre,
                                     Suma_TotalLinea = g.Sum(x => x.TotalLinea)
                                 }).FirstOrDefault();

                Console.WriteLine("\n");
                Console.WriteLine($"--> La marca con mayor margen de ganancias es: {resultado?.NombreMarca} con {resultado?.Suma_TotalLinea.ToString("C")}");

            }

            void Opcion6()
            {

                var ventasUltimoMes = from vd in ventasdetalle
                                      join v in ventas on vd.IdVenta equals v.IdVenta
                                      join lc in local on v.IdLocal equals lc.IdLocal
                                      join p in producto on vd.IdProducto equals p.IdProducto
                                      join m in marcas on p.IdMarca equals m.IdMarca
                                      select new
                                      {
                                          idLocal = lc.IdLocal,
                                          idProducto = p.IdProducto,
                                          NombreLocal = lc.Nombre,
                                          NombreProd = p.Nombre,
                                          cantidad_ventas = vd.Cantidad,
                                          monto_ventas = vd.TotalLinea,
                                          fechaHora = v.Fecha
                                      };

                var productoMasVendidoPorLocal = ventasUltimoMes.GroupBy(v => new { v.NombreProd, v.NombreLocal })
                    .Select(grupo => new { NomLocal = grupo.Key.NombreLocal, NomProducto = grupo.Key.NombreProd, MontoTotal = grupo.Sum(v => v.monto_ventas) })
                    .GroupBy(res => res.NomLocal)
                    .Select(grupoLocal => grupoLocal.OrderByDescending(resultado => resultado.MontoTotal).First());

                foreach (var item in productoMasVendidoPorLocal)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine($"--> El producto más vendido en local {item.NomLocal} es {item.NomProducto}");
                }

            }

        }

        static void MostrarMensajeCargando()
        {

            Thread.Sleep(1000);
            Console.Write(".");

        }
    }
}
