using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleComm
{
    public class Manager
    {
        public void Manage(string[] args)
        {
            // Arg[0] is the IP
            // Arg[1] is the PORT
            // If Arg[2] exists, it is the CMD value (string), so send the command, otherwise listen for a command
            if (args.Length > 2)
            {
                Console.WriteLine("Sending command: \"{0}\" on: {1}:{2}", args[2], args[0], args[1]);
                Sender snd = new Sender();
                snd.Send(args[2], args[0], Int32.Parse(args[1]));
            }
            else
            {
                Console.WriteLine("Listen on: {0}:{1}", args[0], args[1]);
                Listener lsn = new Listener();
                lsn.Listen(args[0], Int32.Parse(args[1]));
            }
        }
    }

    public class Listener
    {
        public void Listen(string serverIp, int port)
        {
            // Listen at the specified IP and PORT
            IPAddress localAdd = IPAddress.Parse(serverIp);
            TcpListener listener = new TcpListener(localAdd, port);
            Console.WriteLine("Listening...");
            listener.Start();

            // Accept the client connection and get the sent command
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            // Convert the sent command
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received: " + dataReceived);

            // Send a success response
            Console.WriteLine("Sending response");
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes("success");
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            client.Close();
            listener.Stop();
            Console.ReadLine();
        }
    }

    public class Sender
    {
        public void Send(string cmd, string serverIp, int port)
        {
            // Prepare the command to send
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(cmd);
            // byte[] bytesToSend = BitConverter.GetBytes(cmd);

            // Create a TCPClient
            TcpClient client = new TcpClient(serverIp, port);
            NetworkStream nwStream = client.GetStream();

            // Send the command
            Console.WriteLine("Sending");
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            // Read the response
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received: " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
            client.Close();
        }
    }
}
