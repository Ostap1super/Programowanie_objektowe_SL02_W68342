internal class Program
{
    private static void Main(string[] args)
    {
        string menu = Console.ReadLine();

        switch (menu)
        {

            case "1":

                int enter = Convert.ToInt32(Console.ReadLine());

                if (enter % 2 == 0)
                {
                    Console.WriteLine("Parzysta " + enter);
                }
                break;

            case "2":

                int enter1 = Convert.ToInt32(Console.ReadLine());
                for (int i = 2; i <= enter1; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.WriteLine(i);
                    }
                    string exit = Console.ReadLine();
                    if (exit == "exit")
                    {
                        i = enter1;
                    }

                };
                break;





        };










        

    } 

    }
