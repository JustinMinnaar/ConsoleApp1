using Microsoft.Extensions.DependencyInjection;

public class LottoProgram(IServiceProvider services)
{
    public void Run()
    {
        var tester = services.GetService<LottoTester>()!;
        tester.AddUsers();
        tester.PrintUsers();
    }
}
