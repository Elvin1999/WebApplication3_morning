namespace WebApplication3.Services
{
    public class CalculateService : ICalculateService
    {
        int value = 100;
        public CalculateService()
        {
            Console.WriteLine("Services Constructor");
        }
        public int CalculateSomething(int num1, int num2)
        {
            value += 100;
            return value;
        }
    }
}
