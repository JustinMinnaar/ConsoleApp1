using System.Data;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var options = new LottoOptions();

        AddSampleUsers(options);
        PrintUsers(options);
    }

    private static void PrintUsers(LottoOptions options)
    {
        var tester = new LottoTester(options);
        tester.PrintUsers();
    }

    private static void AddSampleUsers(LottoOptions options)
    {
        var tester = new LottoTester(options);
        tester.AddUsers();
    }
}
