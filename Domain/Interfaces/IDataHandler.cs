using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDataHandler<TDataType>
    {
        TDataType Get(int id);

        IEnumerable<TDataType> GetAll();

        int Insert(TDataType dataType);
        
    }
}
