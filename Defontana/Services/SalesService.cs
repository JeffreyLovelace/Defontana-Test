using Defontana.Interfaces;
using Defontana.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defontana.Services
{
    public class SalesService : ISalesService
    {
        private readonly PruebaContext _dbContext;

        public SalesService(PruebaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Ventum> GetSalesLast30Days()
        {
            var today = DateTime.Today;
            var last30Days = today.AddDays(-30);

            return _dbContext.Venta
                .Where(s => s.Fecha >= last30Days && s.Fecha <= today)
                .Include(s => s.VentaDetalles)
                .ThenInclude(d => d.IdProductoNavigation)
                .ThenInclude(p => p.IdMarcaNavigation) 
                .Include(s => s.IdLocalNavigation)
                .ToList();
        }
    }
}