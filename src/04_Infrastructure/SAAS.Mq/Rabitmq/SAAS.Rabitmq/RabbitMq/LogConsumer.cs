using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAAS.FrameWork.RabbitMq
{
    /// <summary>
    /// 从MQ接收并处理数据
    /// 实现MassTransit的IConsumer接口
    /// </summary>
    public class LogConsumer : IConsumer<ActionLog>
    {
        /// <summary>
        /// 重写Consume方法
        /// 接收并处理数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Consume(ConsumeContext<ActionLog> context)
        {
            return Task.Run(async () =>
            {
                //获取接收到的对象
                var amsg = context.Message;
                Console.WriteLine($"Recevied By Consumer:{amsg}");
                Console.WriteLine($"Recevied By Consumer:{amsg.ActionLogId}");
            });
        }
    }
}


