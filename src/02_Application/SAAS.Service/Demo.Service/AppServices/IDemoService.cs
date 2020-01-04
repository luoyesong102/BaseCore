using CommonInterface;
using System.Threading.Tasks;

namespace Demo.Service
{
    /// <summary>
    /// 演示
    /// </summary>
    public interface IDemoService: AutoInject
    {
         string GetStrTest();
    }
}
