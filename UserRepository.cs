
using Microsoft.EntityFrameworkCore;

public class UserRepository(LottoOptions options) : IDisposable
{
    private readonly LottoDbContext db = new(options.dbOptions);

    public void Dispose()
    {
        db.Dispose();
        GC.SuppressFinalize(this);
    }

    public void SaveChanges() => db.SaveChanges();

    public void AddUser(User user)
    {
        db.Users.Add(user);
    }

    public User? GetUserByName(string name)
    {
        return db.Users.FirstOrDefault(u => u.Name!.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public User? GetUserByEmail(string email)
    {
        return db.Users.FirstOrDefault(u => u.Email == email);
    }

    public User AddUserIfMissing(string name, string phone, string email)
    {
        var user = db.Users.FirstOrDefault(u => u.Name == name);
        if (user != null) return user;

        return AddUser(name, phone, email, out user);
    }

    public User AddUser(string name, string phone, string email, out User? user)
    {
        user = new User { Name = name, Phone = phone, Email = email };
        db.Users.Add(user);
        return user;
    }

    internal void PrintUsers()
    {
        var users = db.Users.ToList();
        foreach (var user in users)
        {
            Console.WriteLine($"User ID: {user.Id}");
            Console.WriteLine($"Name:    {user.Name}");
            Console.WriteLine($"Phone:   {user.Phone}");
            Console.WriteLine($"Email:   {user.Email}");
            Console.WriteLine();
        }
    }
}

