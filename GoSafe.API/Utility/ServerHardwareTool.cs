using System.Net.NetworkInformation;

namespace GoSafe.API.Utility
{
    public static class ServerHardwareTool
    {
        public static string GetMacAddress()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var adapter in interfaces)
            {
                var address = adapter.GetPhysicalAddress();
                if (address != null && address.ToString() != "")
                    return address.ToString();
            }
            return string.Empty;
        }
    }
}
