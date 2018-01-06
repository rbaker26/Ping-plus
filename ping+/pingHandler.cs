using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace ping_
{
    class PingHandler
    {
        //************************************************************************************
        //* Ping info
        //************************************************************************************
        private const string DEFAULT_IP = "8.8.8.8";  // The Google DNS Server
        private const uint   SIZE  = 32;              // Size in bytes
        private const uint   COUNT = 4;               // Ping Count
        //************************************************************************************


        //************************************************************************************
        public PingHandler(string ip = DEFAULT_IP, uint size = SIZE, uint count = COUNT)
        {
            _ping(ip, size, count);
        }
        //************************************************************************************


        static void _ping(string ip, uint size, uint count)
        {
            //*****************************************************************
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            //*****************************************************************

            //*****************************************************************
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;
            //*****************************************************************


            //*****************************************************************
            // Create a buffer of 32 bytes or size of data to be transmitted
            string data = "";
            if (size == SIZE)
            {
                data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < SIZE; i++)
                {
                    sb.Append("a");
                }
                data = sb.ToString();
            }
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            //*****************************************************************

            int timeout = 5000;

            //*****************************************************************
            uint timeoutCount = 0;
            uint lostPackets = 0;
            long totalTime = 0;
            Console.WriteLine();
            Console.WriteLine("Ping+ " + ip + " with " + size + " byte of data:");
            for (int i = 0; i < count; i++)
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
                        totalTime += reply.RoundtripTime;
                    }
                    else if (reply.Status == IPStatus.BadDestination)
                    {
                        Console.WriteLine("bad ip");
                    }
                    else if (reply.Status == IPStatus.DestinationHostUnreachable)
                    {
                        Console.WriteLine("Destination Host Unreachable");
                        lostPackets++;
                    }
                    else if (reply.Status == IPStatus.TimedOut)
                    {
                        Console.WriteLine("Request Timed Out");
                        timeoutCount++;
                    }
                }
                catch (System.Net.NetworkInformation.PingException pExp)
                {
                    Console.WriteLine(pExp.ToString());
                }


            }
            //*****************************************************************

            //*****************************************************************
            uint received = count - lostPackets - timeoutCount;
            uint lost = lostPackets + timeoutCount;
            uint percLoss = (received == 0 ? 100 : (lost / received) * 100);
            
            Console.WriteLine("Ping Statistics for " + ip +":");
            Console.WriteLine("\tPackets: Sent = "   + count    + 
                              ", Received = "        + received + 
                              ", Lost = "            + lost     +
                              " (" + percLoss + "% loss)");
            Console.WriteLine("Average Time:\t" + (totalTime / count));
            //*****************************************************************
        }
        //************************************************************************************



    }




}
