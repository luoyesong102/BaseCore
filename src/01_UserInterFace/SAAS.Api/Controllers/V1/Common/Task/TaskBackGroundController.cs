using Microsoft.AspNetCore.Mvc;
using TaskService.Attr;
using TaskService.Extensions;
using TaskService.Models;
using TaskService.Utility;
using System.Threading.Tasks;
using Quartz;
using SAAS.FrameWork.Module.Api.Data;

namespace SAAS.Api
{
    /// <summary>
    /// 任务列表
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TaskBackGroundController : ControllerBase
    {
        private readonly ISchedulerFactory _schedulerFactory;
        public TaskBackGroundController(ISchedulerFactory schedulerFactory)
        {
            this._schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// 获取所有的作业
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ApiReturn<object>> GetJobs()
        {
            return await _schedulerFactory.GetJobs();
        }
        /// <summary>
        /// 获取作业运行日志string taskName, string groupName, int page = 1
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="groupName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public ApiReturn<object> GetRunLog()
        {
            return FileQuartz.GetJobRunLog("", "", 1);
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>

        [HttpPost("[action]")]
        public async Task<ApiReturn<object>> Add(TaskOptions taskOptions)
        {
            return await taskOptions.AddJob(_schedulerFactory);
        }
        [HttpPost("[action]")]
        public async Task<ApiReturn<object>> Remove(TaskOptions taskOptions)
        {
            return await _schedulerFactory.Remove(taskOptions);
        }
        [HttpPost("[action]")]
        public async Task<ApiReturn<object>> Update(TaskOptions taskOptions)
        {
            return await _schedulerFactory.Update(taskOptions);
        }
        [HttpPost("[action]")]
        public async Task<ApiReturn<object>> Pause(TaskOptions taskOptions)
        {
            return await _schedulerFactory.Pause(taskOptions);
        }
        [HttpPost("[action]")]
        public async Task<ApiReturn<object>> Start(TaskOptions taskOptions)
        {
            return await _schedulerFactory.Start(taskOptions);
        }
        [HttpPost("[action]")]
        public async Task<ApiReturn<object>> Run(TaskOptions taskOptions)
        {
            return await _schedulerFactory.Run(taskOptions);
        }
    }




}