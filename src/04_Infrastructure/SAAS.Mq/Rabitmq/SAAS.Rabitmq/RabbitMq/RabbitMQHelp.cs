using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAAS.FrameWork.RabbitMq

{
    /// <summary>
    /// RabbitMQ公共操作类，基于MassTransit库
    /// </summary>
    public class RabbitMQHelp
    {
        #region 交换器

        /// <summary>
        /// 操作日志交换器
        /// 同时需在RabbitMQ的管理后台创建同名交换器
        /// </summary>
        public static readonly string actionLogExchange = "Lezhima.ActionLogExchange";


        #endregion


        #region 声明变量

        /// <summary>
        /// MQ联接地址，建议放到配置文件
        /// </summary>
        private static readonly string mqUrl = "rabbitmq://192.168.6.181/";

        /// <summary>
        /// MQ联接账号，建议放到配置文件
        /// </summary>
        private static readonly string mqUser = "admin";

        /// <summary>
        /// MQ联接密码，建议放到配置文件
        /// </summary>
        private static readonly string mqPwd = "admin";

        #endregion

        /// <summary>
        /// 创建连接对象
        /// 不对外公开
        /// </summary>
        private static IBusControl CreateBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            //通过MassTransit创建MQ联接工厂
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(mqUrl), hst =>
                {
                    hst.Username(mqUser);
                    hst.Password(mqPwd);
                });
                registrationAction?.Invoke(cfg, host);
            });
        }


        /// <summary>
        /// MQ生产者
        /// 这里使用fanout的交换类型
        /// </summary>
        /// <param name="obj"></param>
        public async static Task PushMessage(string exchange, object obj)
        {
            var bus = CreateBus();
            var sendToUri = new Uri($"{mqUrl}{exchange}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            await endPoint.Send(obj);
        }

        /// <summary>
        /// MQ消费者
        /// 这里使用fanout的交换类型
        /// consumer必需是实现IConsumer接口的类实例
        /// </summary>
        /// <param name="obj"></param>
        public static void ReceiveMessage(string exchange, object consumer)
        {
            var bus = CreateBus((cfg, host) =>
            {
                //从指定的消息队列获取消息 通过consumer来实现消息接收
                cfg.ReceiveEndpoint(host, exchange, e =>
                {
                    e.Instance(consumer);
                });
            });
            bus.Start();
        }
    }
}



///// <summary>
///// 测试MQ生产者
///// </summary>
///// <returns></returns>
//[HttpGet]
//public async Task<MobiResult> AddMessageTest()
//{
//    //声明一个实体对象
//    var model = new ActionLog();
//    model.ActionLogId = Guid.NewGuid();
//    model.CreateTime = DateTime.Now;
//    model.UpdateTime = DateTime.Now;
//    //调用MQ
//    await RabbitMQHelp.PushMessage(RabbitMQHelp.actionLogExchange, model);

//    return new MobiResult(1000, "操作成功");
//}

////调用接收者
//RabbitMQHelp.ReceiveMessage(RabbitMQHelp.actionLogExchange,
//             new LogConsumer()
//            );
