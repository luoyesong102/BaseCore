using Microsoft.AspNetCore.Http;
using SAAS.FrameWork.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionMiddleService.DoTime
{
    public class CalculateExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;//下一个中间件
        Stopwatch stopwatch;
        public CalculateExecutionTimeMiddleware(RequestDelegate next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
           
            this._next = next;
          
        }

        public async Task Invoke(HttpContext context)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();//在下一个中间价处理前，启动计时器
            await _next.Invoke(context);

            stopwatch.Stop();//所有的中间件处理完后，停止秒表。
            
            Logger.Info($@"接口{context.Request.Path}耗时{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}