﻿using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork.Util.Common
{
    public class IdWorkerHelper
    {
        private static IdWorker worker = null;
        private static readonly object locker = new object();
        private IdWorkerHelper()
        {
        }
        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        private static IdWorker GetInstance()
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            // 双重锁定只需要一句判断就可以了
            if (worker == null)
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (worker == null)
                    {
                        worker = new IdWorker(1, 1);
                    }
                }
            }
            return worker;
        }

        public static void Init()
        {
            if (worker == null)
            {
                worker = new IdWorker(1, 1);
            }
        }
        public static long NewId()
        {
            return GetInstance().NextId();
           
        }

    }
}