namespace Domain;

public class User
{
    public int Id { get; private set; }
    public string Name { get;  }

    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public void SetId(int newId)
    {
        if (this.Id <= 0)
            this.Id = newId;
        else
            throw new ArgumentException("Id already set");
    }
}
