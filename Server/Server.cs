using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            Console.WriteLine("Сервер та база даних готові. Натисніть Enter для початку обробки запитань клієнта.");
            Console.ReadLine();

            StartServer();

            Console.WriteLine("Натисніть Enter для завершення програми.");
            Console.ReadLine();
        }

        static void StartServer()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            server.Start();

            Console.WriteLine("Сервер запущено. Очікування підключень...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Клієнт підключений.");

                ServerCommunication.HandleClient(client);
            }
        }
    }
}
