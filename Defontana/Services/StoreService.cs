using Defontana.Interfaces;
using Defontana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defontana.Services
{

    public class StoreService: IStoreService
    {
        public void PrintTotalSalesLast30Days(List<Ventum> sales)
        {
            var totalSales = sales.Sum(s => s.Total);
            var totalSalesCount = sales.Count;

            Console.WriteLine($"Total de ventas en los últimos 30 días: ${totalSales} ({totalSalesCount} ventas)");
        }

        public void PrintHighestSale(List<Ventum> sales)
        {
            var highestSale = sales.OrderByDescending(s => s.Total).FirstOrDefault();
            Console.WriteLine($"Venta más alta: {highestSale.Fecha} - Monto: ${highestSale.Total}");
        }

        public void PrintProductWithHighestSales(List<Ventum> sales)
        {
            var productWithHighestSales = sales
                .SelectMany(s => s.VentaDetalles)
                .GroupBy(d => new { d.IdProductoNavigation.Nombre, d.IdProducto })
                .Select(g => new
                {
                    ProductName = g.Key.Nombre,
                    TotalSales = g.Sum(d => d.TotalLinea)
                })
                .OrderByDescending(g => g.TotalSales)
                .FirstOrDefault();

            Console.WriteLine($"Producto con mayor monto total de ventas: {productWithHighestSales?.ProductName} - Monto total de ventas: ${productWithHighestSales?.TotalSales}");
        }

        public void PrintStoreWithHighestSales(List<Ventum> sales)
        {
            var storeWithHighestSales = sales
                .GroupBy(s => s.IdLocalNavigation)
                .OrderByDescending(g => g.Sum(s => s.Total))
                .First().Key.Nombre;
            Console.WriteLine($"Local con mayor monto de ventas: {storeWithHighestSales}");
        }

        public void PrintBrandWithHighestProfitMargin(List<Ventum> sales)
        {
            var brandWithHighestProfitMargin = sales
           .SelectMany(s => s.VentaDetalles)
           .GroupBy(d => d.IdProductoNavigation.IdMarcaNavigation)
           .OrderByDescending(g => g.Sum(d => d.TotalLinea))
           .First().Key.Nombre;

            Console.WriteLine($"Marca con mayor margen de ganancias: {brandWithHighestProfitMargin}");
        }

        public void PrintBestSellingProductByStore(List<Ventum> sales)
        {
            var productsByStore = sales
                .GroupBy(s => s.IdLocalNavigation)
                .Select(g => new
                {
                    StoreName = g.Key.Nombre,
                    BestSellingProduct = g.SelectMany(s => s.VentaDetalles)
                        .GroupBy(d => d.IdProductoNavigation)
                        .OrderByDescending(g2 => g2.Sum(d => d.Cantidad))
                        .First().Key.Nombre
                });
            foreach (var productByStore in productsByStore)
            {
                Console.WriteLine($"Producto más vendido en {productByStore.StoreName}: {productByStore.BestSellingProduct}");
            }
        }
    }
}

