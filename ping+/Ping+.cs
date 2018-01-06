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
            IpConfig ipCf = new IpConfig();
            PingHandler pinger;

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
                    try
                    {
                        count = Int32.Parse(args[2]);
                    }
                    catch (FormatException)
                    {
                        ConsoleColor currentForeground = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bad [count] Parameter");
                        Console.ForegroundColor = currentForeground;
                        goto case 0;
                    }
                    goto case 2;
                case 2:
                    try
                    {
                        size = Int32.Parse(args[1]);
                    }
                    catch (FormatException)
                    {
                        ConsoleColor currentForeground = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bad [size] Parameter");
                        Console.ForegroundColor = currentForeground;
                        goto case 0;
                    }
                    goto case 1;
                case 1:
                    ip = args[0];

                    if(args[0].ToUpper() == "HELP" || args[0].ToUpper() == "-H")
                    {
                        Console.WriteLine(helpCmd);
                        System.Environment.Exit(1);
                    }
                    // prints the tests to console
                    pinger = new PingHandler(ip, (uint)size, (uint)count);
                    Console.WriteLine(ipCf);
                    //System.Environment.Exit(1);  // idk if i want to exit on a success with command line params yet
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

            if (ip.ToUpper() == "EXIT") 
            {
                System.Environment.Exit(1);
            }

            


            //****************************************************************************************
            string[] stringSeparators;
            string fullCommand;
            string[] commandAr = { "" };

            bool exitFlag;
            do
            {

                
                //ResetPingVals(ref ip, ref size, ref count);

                Console.WriteLine();
                Console.Write(">>> ");
                stringSeparators = new string[] { " " };
                fullCommand = Console.ReadLine();
                commandAr = fullCommand.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                if(commandAr.Length != 0 && !(commandAr[0].ToUpper() == "RUN"  || 
                                              commandAr[0].ToUpper() == "EXIT" || 
                                              commandAr[0].ToUpper() == "HELP") )
                {
                    switch (commandAr.Length)
                    {
                        case 3:
                            try
                            {
                                count = Int32.Parse(commandAr[2]);
                            }
                            catch (FormatException)
                            {
                                ConsoleColor currentForeground = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Bad [size] Parameter");
                                Console.ForegroundColor = currentForeground;
                                goto case 0;
                            }
                            goto case 2;
                        case 2:
                            try
                            {
                                size = Int32.Parse(commandAr[1]);
                            }
                            catch(FormatException)
                            {
                                ConsoleColor currentForeground = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Bad Parameter");
                                Console.ForegroundColor = currentForeground;
                                goto case 0;
                            }
                            
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

                    pinger = new PingHandler(ip, (uint)size, (uint)count);
                    Console.WriteLine(ipCf);

                }
                else if (commandAr.Length != 0 && commandAr[0].ToUpper() == "HELP")
                {
                    Console.WriteLine(helpInner);
        
                }
                else if((commandAr.Length != 0 && commandAr[0].ToUpper() == "RUN"))
                {
                    pinger = new PingHandler(); // will run ping tests
                    Console.WriteLine(ipCf);
                }

                // this is the exitng logic block.
                if(commandAr.Length != 0)
                {
                    exitFlag = (commandAr[0] == "exit" || commandAr[0] == "Exit" || commandAr[0] == "EXIT");
                }
                else
                {
                    exitFlag = false;
                }

                count = 4;
                size = 32;
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
            Console.WriteLine("Type \"run\" to start testing.");

            Console.WriteLine("Type \"help\" for help or \"exit\" to exit");
            Console.WriteLine("***************************************************************");

        }

       

        const string title = "\n\n" +
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



        const string usage = "USAGE: \n\t>>> <IP Address> [size] [count]";

        const string helpCmd = "\n\nOptions:\n\n" +
                            "Ping+ \t\tRuns Test with default Values.\n\n" +
                            "Ping+ <ip_address> [size] [count]\n" +
                                "\t[size] & [count] are optional.\n\n" +
                            "Ping+ <web_address> [size] [count]\n" +
                                "\t[size] & [count] are optional.\n\n" +
                            "Ping+ -h\t\t|Displays Help\n";

        const string helpInner = "\n\nOptions:\n\n" +
                                 "\texit \tReturns to command line.\n\n" +
                                 "\trun \tRuns tests will default values.\n\n" +
                                 "\t<ip_address> [size] [count]\n" +
                                    "\t[size] & [count] are optional.\n\n" +
                                "\t<web_address> [size] [count]\n" +
                                    "\t[size] & [count] are optional.\n\n" +
                                 "\t-h \t\t|Displays Help\n";


    }
}
