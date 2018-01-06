using System;
using System.Net;

public class ipConfig
{
    private string localComputerName;
    private IPAddress[] localIPs;

    public ipConfig()
	{
        localComputerName = Dns.GetHostName();
        localIPs = Dns.GetHostAddresses(Dns.GetHostName);
        
    }

    public IPAddress[] getLocalIp()
    {
        return localIPs;
    }

    public string getLocalComputerName()
    {
        return localComputerName;
    }


}
