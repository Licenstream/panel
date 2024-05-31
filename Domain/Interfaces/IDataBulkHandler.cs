namespace Domain.Interfaces;

public interface IDataBulkHandler<TDataType>
{
    void InsertBulk(IEnumerable<TDataType> dataTypes, int customerId);
}