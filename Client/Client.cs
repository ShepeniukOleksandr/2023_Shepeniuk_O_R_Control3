using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введіть ціле число:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int number))
            {
                string response = SendMessage(number.ToString());
                Console.WriteLine($"Відповідь від сервера: {response}");
            }
            else
            {
                Console.WriteLine("Невірний формат введеного числа. Завершення програми.");
            }
        }

        static string SendMessage(string message)
        {
            using (TcpClient client = new TcpClient("localhost", 8888))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                return Encoding.UTF8.GetString(data, 0, bytes);
            }
        }
    }
}
