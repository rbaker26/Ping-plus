using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace ping_
{
    public class IpConfig
    {
        //************************************************************************************
        private string localComputerName;
        private IPAddress[] localIPs;
        private string mac;
        //************************************************************************************

        //************************************************************************************
        public IpConfig()
        {
            this.localComputerName = Dns.GetHostName();
            this.localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            this.mac = findLocalWifiMac();
        }
        //************************************************************************************

        //************************************************************************************
        public IPAddress[] getLocalIp()
        {
            return this.localIPs;
        }
        //************************************************************************************

        //************************************************************************************
        public string getLocalComputerName()
        {
            return this.localComputerName;
        }
        //************************************************************************************

        //************************************************************************************
        private string findLocalWifiMac()
        {
            string mac = "";
            foreach (NetworkInterface nic in  NetworkInterface.GetAllNetworkInterfaces())
            {
                if(nic.NetworkInterfaceType != NetworkInterfaceType.Wireless80211)
                {
                    continue;
                }
                if(nic.OperationalStatus == OperationalStatus.Up)
                {
                    mac += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            StringBuilder sb = new StringBuilder(mac);
            // Puts the dashes in the mac address
            for(int i = mac.Length; i > 0; i--)
            {
                if(i != 0 && i % 2 == 0 && i != mac.Length)
                {
                    sb.Insert(i, "-");
                    i--;
                }
                
            }

            return sb.ToString();
        }
        //************************************************************************************

        //************************************************************************************
        public string getMac()
        {
            return this.mac;
        }
        //************************************************************************************

        //************************************************************************************
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nWireless LAN adapter Wi-Fi:\n");
            sb.Append("\tLocalhost Name:\t\t\t" + getLocalComputerName() + '\n');

            sb.Append("\tIPv4 Address\t\t\t" + localIPs[3] + "\n");
            sb.Append("\tIPv6 Address\t\t\t" + localIPs[2] + "\n");
            sb.Append("\tIPv6 Address\t\t\t" + localIPs[5] + "\n");
            sb.Append("\tTemporary IPv6 Address\t\t" + localIPs[1] + "\n");
            sb.Append("\tTemporary IPv6 Address\t\t" + localIPs[4] + "\n");


            sb.Append("\tLink-Local IPv6 Address:\t" + localIPs[0] + '\n');
            
           
            
            

            sb.Append("\tWiFi Mac Address:\t\t" + getMac());

            return sb.ToString();
        }
        //************************************************************************************
    }

}

