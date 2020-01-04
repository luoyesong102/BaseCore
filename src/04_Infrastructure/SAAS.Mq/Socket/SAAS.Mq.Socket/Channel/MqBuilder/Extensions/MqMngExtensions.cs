using SAAS.FrameWork.Mq.Mng;
using SAAS.Mq.Socket.Channel.MqBuilder;

namespace SAAS.FrameWork.Extensions
{
    public static partial class MqMngExtensions
    {

        public static void UseSocket(this ClientMqMng mqMng)
        {
            if (null == mqMng) return;

            mqMng.AddMqBuilder(new MqClientBuilder());

        }


        public static void UseSocket(this ServerMqMng mqMng)
        {
            if (null == mqMng) return;

            mqMng.AddMqBuilder(new MqServerBuilder());

        }


    }
}
