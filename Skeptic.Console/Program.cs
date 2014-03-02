
namespace Skeptic.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var skeptic = new SkepticConsole(args);
            skeptic.Criticize();
        }
    }
}
