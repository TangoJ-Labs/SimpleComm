using System;
using SimpleComm;

namespace test
{
    class Test
    {
        // const int PORT_NO = 4000;
        // const string SERVER_IP = "127.0.0.1";
        public static void Main(string[] args)
        {
            Console.WriteLine("test");
            Manager mgr = new Manager();
            mgr.Manage(args);
        }
    }
}