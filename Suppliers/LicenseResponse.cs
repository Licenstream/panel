using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierService
{
    internal class LicenseResponse
    {
        public IEnumerable<Domain.License> Value = Enumerable.Empty<Domain.License>();
    }
}
