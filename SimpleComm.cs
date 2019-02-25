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
            TcpListener listener = null;
            try
            {
                // Listen at the specified IP and PORT
                IPAddress localAddr = IPAddress.Parse(serverIp);
                listener = new TcpListener(localAddr, port);
                listener.Start();
                    
                // Buffer for reading data
                Byte[] bytes = new Byte[256];

                // Enter the listening loop.
                while(true) 
                {
                    // Perform a blocking call to accept requests.
                    // You could also user listener.AcceptSocket() here.
                    TcpClient client = listener.AcceptTcpClient();            

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while((i = stream.Read(bytes, 0, bytes.Length))!=0) 
                    {   
                        // Convert the sent command and exit
                        string command = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        return command;
                    }
                    
                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                return "ERROR";
            }
            finally
            {
                // Stop listening for new clients.
                listener.Stop();
            }
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
