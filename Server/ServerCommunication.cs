using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Text;
using Server;

public static class ServerCommunication
{
    public static void HandleClient(TcpClient client)
    {
        using (NetworkStream stream = client.GetStream())
        {
            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string clientMessage = Encoding.UTF8.GetString(data, 0, bytes);

            string response = ProcessMessage(clientMessage);

            data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);

            client.Close();
        }
    }

    public static string ProcessMessage(string message)
    {
        using (var context = new AppDbContext())
        {
            if (int.TryParse(message, out int id))
            {
                var country = context.Countries.FirstOrDefault(c => c.Id == id);

                if (country != null)
                {
                    return country.CountryName;
                }
                else
                {
                    return "Information was not found";
                }
            }
            else
            {
                return "Invalid input format";
            }
        }
    }
}
