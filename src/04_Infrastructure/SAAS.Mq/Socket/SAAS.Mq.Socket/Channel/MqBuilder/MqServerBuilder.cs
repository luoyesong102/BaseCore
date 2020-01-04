using SAAS.FrameWork.Mq;
using SAAS.FrameWork.Mq.Mng;
using SAAS.FrameWork.Util.ConfigurationManager;

namespace SAAS.Mq.Socket.Channel.MqBuilder
{
    public class MqServerBuilder : IServerMqBuilder
    {
        public IServerMq BuildMq()
        {
            var config = ConfigurationManager.Instance.GetByPath<ServerMqConfig>("SAAS.Mq.Socket");
            if (null == config)
            {
                return null;
            }

            return new ServerMq(config);
        }
    }
}
