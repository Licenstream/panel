using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Keyless]
    public class PrepaidUnits
    {
        public int Enabled { get; set; }
        public int Suspended { get; set; }
        public int Warning { get; set; }
        public int LockedOut { get; set; }
    }
}
