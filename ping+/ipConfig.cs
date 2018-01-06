using System;
using System.Net;
using System.Net.NetworkInformation;

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
            return mac;
        }
        //************************************************************************************

        //************************************************************************************
        public string getMac()
        {
            return this.mac;
        }
        //************************************************************************************
    }
}

