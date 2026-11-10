using System;

namespace Lab14v18
{
    public class ServiceStatus
    {
        public string ServiceName { get; private set; }
        public bool IsOnline { get; private set; }
        public int ResponseMs { get; private set; }

        public ServiceStatus(string serviceName, bool isOnline, int responseMs)
        {
            ServiceName = serviceName;
            IsOnline = isOnline;
            ResponseMs = responseMs;
        }

        public override string ToString()
        {
            string state = IsOnline ? "online" : "OFFLINE";
            return ServiceName + " - " + state + " (" + ResponseMs + " мс)";
        }
    }
}