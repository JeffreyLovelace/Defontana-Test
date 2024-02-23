using Defontana.Interfaces;
using Defontana.Models;
using Defontana.Services;
using Microsoft.EntityFrameworkCore;

namespace Defontana
{
    class Program
    {
        static void Main(string[] args)
        {
            ISalesService salesService = new SalesService(new PruebaContext());
            IStoreService presentationService = new StoreService();

            var sales = salesService.GetSalesLast30Days();
            presentationService.PrintTotalSalesLast30Days(sales);
            presentationService.PrintHighestSale(sales);
            presentationService.PrintProductWithHighestSales(sales);
            presentationService.PrintStoreWithHighestSales(sales);
            presentationService.PrintBrandWithHighestProfitMargin(sales);
            presentationService.PrintBestSellingProductByStore(sales);
        }
    }

   

  

}