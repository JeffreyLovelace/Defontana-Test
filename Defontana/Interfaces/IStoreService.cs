using Defontana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defontana.Interfaces
{
    public interface IStoreService
    {
        void PrintTotalSalesLast30Days(List<Ventum> sales);
        void PrintHighestSale(List<Ventum> sales);
        void PrintProductWithHighestSales(List<Ventum> sales);
        void PrintStoreWithHighestSales(List<Ventum> sales);
        void PrintBrandWithHighestProfitMargin(List<Ventum> sales);
        void PrintBestSellingProductByStore(List<Ventum> sales);
    }
}
