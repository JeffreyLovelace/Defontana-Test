using Defontana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defontana.Interfaces
{
    public interface ISalesService
    {
        List<Ventum> GetSalesLast30Days();
    }
}
