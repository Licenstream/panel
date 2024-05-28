using Domain.Interfaces;

namespace Domain;

public class UserService
{
    private readonly IDataHandler<User> _userDataHandler;

    public UserService(IDataHandler<User> userDataHandler)
    {
        _userDataHandler = userDataHandler;
    }

    public User GetById(int id)
    {
        return _userDataHandler.Get(id);
    }

    public IEnumerable<User> GetAll()
    {
        return _userDataHandler.GetAll();
    }

    public User Save(User user)
    {
        var newId = _userDataHandler.Insert(user);
        
        user.SetId(newId);

        return user;
    }
}