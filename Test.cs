using System;
using SimpleComm;

namespace test
{
    class Test
    {
        const int PORT_NO = 4000;
        const string SERVER_IP = "127.0.0.1";
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Sender sender = new Sender();
                sender.Send(args[0], SERVER_IP, PORT_NO);
                Console.WriteLine("Message Sent");
            }
            else
            {
                Listener listener = new Listener();
                string message = listener.Listen(SERVER_IP, PORT_NO);
                Console.WriteLine("Message: " + message);
            }
        }
    }
}