using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork.Message
{
    interface IMessageBus
    { 
        void Pubish(IMessage message);
        void RunIt();
        void StopIt();
    }
}
