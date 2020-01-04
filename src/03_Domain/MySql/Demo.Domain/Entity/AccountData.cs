using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using SAAS.FrameWork.Module.SsApiDiscovery.ApiDesc.Attribute;
using System;

namespace App.AuthCenter.Logical.Entity
{
    [JsonObject(MemberSerialization.OptOut)]
    [Table(AccountData.tableName)]
    public class AccountData
    {
        public const string tableName = "tb_account";

        /// <summary>
        /// 自增组件
        /// </summary>
        [Key]
        public long userId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [SsExample("admin")]
        public string account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [JsonIgnore]
        public string pwd { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [SsExample("admin")]
        public string name { get; set; }


        /// <summary>
        /// 角色。如 "系统管理员"、 "录单员"、"派单员" 、"商家" 、"售后跟踪员"
        /// </summary>
        [SsExample("系统管理员")]
        public string role { get; set; }

        /// <summary>
        /// 账号状态。1:启用   2:停用
        /// </summary>
        [SsExample("1")]
        //[JsonIgnore]
        public int? account_state { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        [SsExample("test@163.com")]
        public string email { get; set; }

        /// <summary>
        /// 电话（可多个，逗号分隔）
        /// </summary>
        [SsExample("15000000000,15000000001")]
        public string tels { get; set; }


        /// <summary>
        /// 注册时间
        /// </summary>
        [SsExample("2019-04-02 12:06")]
        public DateTime? regist_time { get; set; }



        #region decorator

        /// <summary>
        /// 地址
        /// </summary>
        [SsExample("")]
        public string addr { get; set; }



        /// <summary>
        /// 公司类型（核心商家、保单商家、基本商家）
        /// </summary>
        [SsExample("核心商家")]
        public string company_level { get; set; }

        /// <summary>
        /// 装修公司接单区域（用逗号隔开多个）
        /// </summary>
        [SsExample("闵行区,奉贤区")]
        public string decorator_receive_area { get; set; }


        /// <summary>
        /// 装修公司等级
        /// </summary>
        [SsExample("10")]
        public int? decorator_level { get; set; }




        /// <summary>
        /// 装修公司标签（用逗号隔开多个）
        /// </summary>
        [SsExample("设计师棒,奉贤区")]
        public string decorator_tags { get; set; }

        #endregion




        /// <summary>
        /// 账户配置，json字符串存储。如 存储 保单数 等信息
        /// </summary>
        [SsExample("{\"decorator\":{}}")]
        public string account_config { get; set; }

        /// <summary>
        /// 加密密码
        /// </summary>
        [SsExample("MD5")]
        public string oldpwd { get; set; }
        
        /// <summary>
        /// 微信身份标识
        /// </summary>
        [SsExample("xxxx")]
        public string openid { get; set; }
        


    }

}
