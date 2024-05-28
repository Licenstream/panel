using Domain;
using Domain.Interfaces;

namespace InfrastructureEF;

public class UserEFDataHandler : IDataHandler<Domain.User>
{
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