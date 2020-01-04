using DemoEF.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DemoEF.Domain.Models
{
    /// <summary>
    /// 自动赋值http://www.cnblogs.com/OpenCoder/p/9772965.html
    /// </summary>
    //EFDbContext 修改的时候为修改赋值默认值
    public class EFDbContext : db_lj_orderContext
    {
        public EFDbContext()
        {
            //设置数据库Command永不超时
            this.Database.SetCommandTimeout(0);

            //DbContext.ChangeTracker.StateChanged事件，会在DbContext中被Track的实体其EntityState状态值发生变化时被触发
            this.ChangeTracker.StateChanged += (sender, entityStateChangedEventArgs) =>
            {
                //如果实体状态变为了EntityState.Modified，那么就尝试设置其UpdateTime属性为当前系统时间DateTime.Now，如果实体没有UpdateTime属性，会抛出InvalidOperationException异常，所以下面要用try catch来捕获异常避免系统报错
                if (entityStateChangedEventArgs.NewState == EntityState.Modified)
                {
                    try
                    {
                        //如果是Person表的实体那么下面的Entry.Property("UpdateTime")就不会抛出异常
                        entityStateChangedEventArgs.Entry.Property("UpdateDateTime").CurrentValue = DateTime.Now;
                    }
                    catch (InvalidOperationException)
                    {
                        //如果上面try中抛出InvalidOperationException，就是实体没有属性UpdateTime，应该是表Book的实体
                    }
                }

                //如果要自动更新多列，比如还要自动更新实体的UpdateUser属性值到数据库，可以像下面这样再加一个try catch来更新UpdateUser属性
                //if (entityStateChangedEventArgs.NewState == EntityState.Modified)
                //{
                //    try
                //    {
                //        entityStateChangedEventArgs.Entry.Property("UpdateUser").CurrentValue = currentUser;
                //    }
                //    catch (InvalidOperationException)
                //    {
                //    }
                //}
            };
        }

    }
}