using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.ComponentModel;
using System.Threading;

using System.Text.RegularExpressions;

namespace ping_
{
    class Program
    {
        static void Main(string[] args)
        {
          

            // ping <ip> <size> <count>
            InitConsole();

            // the default vals for the command line params
            // prevents errors if switch fails
            string ip  = "0.0.0.0";// = args[0];
            int size = 32; // defualt size
            int count = 4;

            // exits program if "exit" is passed
            if(args.Length != 0)
            {
                if (args[0].ToUpper() == "EXIT")
                {
                    System.Environment.Exit(1);
                }
            }
            
            //init the command line params
            switch (args.Length)
            {
                case 3:
                    count = Int32.Parse(args[2]);
                    goto case 2;
                case 2:
                    size = Int32.Parse(args[1]);
                    goto case 1;
                case 1:
                    ip = args[0];
                    break;
                case 0:
                    //Console.WriteLine("Enter an IP address:");
                    //Console.Write(">>> ");
                    //ip = Console.ReadLine();
                    //Console.WriteLine();
                    break;
                default:
                    // todo - make a throw exeption.
                    Console.WriteLine("Error");
                    break;
            }
         
            if(ip == "exit" || ip == "EXIT" || ip == "Exit")
            {
                System.Environment.Exit(1);
            }
            if(args.Length != 0)
            {
                _ping(ip, size, count);
            }
            


            //****************************************************************************************
            string[] stringSeparators;
            string fullCommand;
            string[] commandAr = { "" };

            bool exitFlag;
            do
            {
                

                ResetPingVals(ref ip, ref size, ref count);

                Console.WriteLine();
                Console.Write(">>> ");
                stringSeparators = new string[] { " " };
                fullCommand = Console.ReadLine();
                commandAr = fullCommand.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                if(commandAr.Length != 0 && !(commandAr[0].ToUpper() == "EXIT" || commandAr[0].ToUpper() == "HELP") )
                {
                    switch (commandAr.Length)
                    {
                        case 3:
                            count = Int32.Parse(commandAr[2]);
                            goto case 2;
                        case 2:
                            size = Int32.Parse(commandAr[1]);
                            goto case 1;
                        case 1:
                            ip = commandAr[0];
                            break;
                        case 0:
                            Console.WriteLine("Enter an IP address:");
                            Console.Write(">>> ");
                            ip = Console.ReadLine();
                            Console.WriteLine();
                            break;
                        default:
                            // todo - make a throw exeption.
                            Console.WriteLine("Error");
                            break;
                    }


                    _ping(ip, size, count);
                }
                else if (commandAr.Length != 0 && commandAr[0].ToUpper() == "HELP")
                {
                    Console.WriteLine(usage);
        
                }
                  
                if(commandAr.Length != 0)
                {
                    exitFlag = (commandAr[0] == "exit" || commandAr[0] == "Exit" || commandAr[0] == "EXIT");
                }
                else
                {
                    exitFlag = false;
                }


            } while (!exitFlag);
            
        }

        static void GetPingParams(ref string ip, ref int size, ref int count)
        {

        }
        static void InitConsole()
        {
            Console.Title = "Ping+ v1.0.0";
            Console.WriteLine(title);
            Console.WriteLine("Ping+ is a simple network diagnostic tool.");
            Console.WriteLine("It will run a series of tests in one run.");
            Console.WriteLine("Type \"help\" for help or \"exit\" to exit");
            Console.WriteLine("***************************************************************");

        }

        static void _ping(string ip, int size, int count)
        {
            
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            int timeout = 5000;

            Console.WriteLine("Ping+ " + ip + " with " + size + " byte of data:");
            for(int i = 0; i < count; i++)
            {
                try
                {
                    PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        //Console.WriteLine("Address: {0}", reply.Address.ToString());
                        //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                        //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                        //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                        //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                        Console.WriteLine("Reply from " + ip + ": bytes=" + size + " time=" + reply.RoundtripTime + "ms");
                    }
                    else if (reply.Status == IPStatus.BadDestination)
                    {
                        Console.WriteLine("bad ip");
                    }
                    else if (reply.Status == IPStatus.DestinationHostUnreachable)
                    {
                        Console.WriteLine("Destination Host Unreachable");
                    }
                    else if (reply.Status == IPStatus.TimedOut)
                    {
                        Console.WriteLine("Request Timed Out");
                    }
                }
                catch (System.Net.NetworkInformation.PingException pExp)
                {
                    Console.WriteLine(pExp.ToString());
                }
               

            }


        }

        static void ResetPingVals(ref string ip, ref int size, ref int count)
        {
            ip = "0.0.0.0";
            size = 32;
            count = 4;
        }

        static string title = "\n\n" +
                "***************************************************************\n" +
                "*            /$$                                  \n"+
                "*           |__/                        /$$       \n"+ 
                "*   /$$$$$$  /$$ /$$$$$$$   /$$$$$$    | $$       \n"+
                "*  /$$__  $$| $$| $$__  $$ /$$__  $$ /$$$$$$$$    \n"+
                "* | $$  \\ $$| $$| $$  \\ $$| $$  \\ $$|__  $$__/ \n"+
                "* | $$  | $$| $$| $$  | $$| $$  | $$   | $$       \n"+
                "* | $$$$$$$/| $$| $$  | $$|  $$$$$$$   |__/       \n"+
                "* | $$____/ |__/|__/  |__/ \\____  $$             \n"+
                "* | $$                     /$$  \\ $$             \n"+
                "* | $$                    |  $$$$$$/              \n"+
                "* |__/                     \\______/              \n"+
                "***************************************************************";



        static string usage = "USAGE: \nPing+ <IP Address> [size] [count]";
    }
}
