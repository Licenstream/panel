using Domain;
using Domain.Interfaces;

namespace InfrastructureEF;

public class UserEFDataHandler : IDataHandler<Domain.User>
{
    private readonly string _connectionString;

    public UserEFDataHandler(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public User Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public int Insert(User dataType)
    {
        throw new NotImplementedException();
    }
}