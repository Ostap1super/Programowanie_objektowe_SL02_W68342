internal class Program
{
    private static void Main(string[] args)
    {
        int enter = Convert.ToInt32(Console.ReadLine());

        if (enter % 2 == 0) {
            Console.WriteLine("Parzysta " + enter);
        }

        else
        {
            Console.WriteLine("Nie Parzysta " + enter);
        };

          for(int i  = 0; i <= enter; i++)
        {
            if(i % 2 == 0)
            {
                Console.WriteLine(i);
            }
                                                                                                                









        }
    }
}