using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleComm
{
    public class Listener
    {
        public string Listen(string serverIp, int port)
        {
            // Listen at the specified IP and PORT
            IPAddress ipaddress = IPAddress.Parse(serverIp);
            TcpListener listener = new TcpListener(ipaddress, port);
            Console.WriteLine("Listening...");
            listener.Start();

            // Accept the client connection and get the sent command
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            // Convert the sent command and return
            string command = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return command;
        }
    }

    public class Sender
    {
        public void Send(string message, string serverIp, int port)
        {
            // Prepare the command to send
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);

            // Create a TCPClient
            TcpClient client = new TcpClient(serverIp, port);
            NetworkStream nwStream = client.GetStream();

            // Send the command
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            client.Close();
        }
    }
}
