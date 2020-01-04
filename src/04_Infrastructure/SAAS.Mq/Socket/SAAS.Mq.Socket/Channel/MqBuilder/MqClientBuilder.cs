using SAAS.FrameWork.Mq;
using SAAS.FrameWork.Mq.Mng;
using SAAS.FrameWork.Util.ConfigurationManager;

namespace SAAS.Mq.Socket.Channel.MqBuilder
{
    public class MqClientBuilder : IClientMqBuilder
    {
        public IClientMq BuildMq()
        {
            var config = ConfigurationManager.Instance.GetByPath<ClientMqConfig>("SAAS.Mq.Socket");


            if (string.IsNullOrEmpty(config?.host))
            {
                return null;
            }
            return new ClientMq(config);

        }
    }
}
