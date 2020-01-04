using System;

namespace SAAS.FrameWork.Mq.Device
{
    public interface IMqDeviceContext
    {
        Object ext { get; set; }
        string deviceGuid { get;}
        DeviceDesc desc { get; set; }
        byte[] SendRequest(byte[] request, int millisecondsTimeout = 300000);
    }
}
