internal class LottoTester(UserRepository userRepo, LottoOptions options)
{
    public void AddUsers()
    {
        using var userRepo = new UserRepository(options);
        userRepo.AddUserIfMissing("Adam Smith", "1234567890", "adam@example.co");
        userRepo.AddUserIfMissing("Eve Smith", "0987654321", "eve@example.co");
        userRepo.SaveChanges();
    }

        // read and print to console all customers
    public void PrintUsers()
    {
        using var userRepo = new UserRepository(options);
        userRepo.PrintUsers();
    }

}