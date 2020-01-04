using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using YH.SAAS.Domain;

namespace DemoEF.Domain.Models
{
    public partial class db_lj_orderContext : DbContext
    {
        public db_lj_orderContext()
        {
        }

        public db_lj_orderContext(DbContextOptions<db_lj_orderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAccount> TbAccount { get; set; }
        public virtual DbSet<TbDecoratorPerson> TbDecoratorPerson { get; set; }
        public virtual DbSet<TbDecoratorSetting> TbDecoratorSetting { get; set; }
        public virtual DbSet<TbDecoratorSnapshot> TbDecoratorSnapshot { get; set; }
        public virtual DbSet<TbFinanceAccount> TbFinanceAccount { get; set; }
        public virtual DbSet<TbFinanceBill> TbFinanceBill { get; set; }
        public virtual DbSet<TbFinanceEntity> TbFinanceEntity { get; set; }
        public virtual DbSet<TbFinanceMonthbill> TbFinanceMonthbill { get; set; }
        public virtual DbSet<TbOrder> TbOrder { get; set; }
        public virtual DbSet<TbOrderActive> TbOrderActive { get; set; }
        public virtual DbSet<TbOrderAssign> TbOrderAssign { get; set; }
        public virtual DbSet<TbOrderCompleted> TbOrderCompleted { get; set; }
        public virtual DbSet<TbOrderDecoratorFlow> TbOrderDecoratorFlow { get; set; }
        public virtual DbSet<TbOrderDispatch> TbOrderDispatch { get; set; }
        public virtual DbSet<TbOrderFeedback> TbOrderFeedback { get; set; }
        public virtual DbSet<TbOrderFlow> TbOrderFlow { get; set; }
        public virtual DbSet<TbOrderLog> TbOrderLog { get; set; }
        public virtual DbSet<TbOrderMeasurement> TbOrderMeasurement { get; set; }
        public virtual DbSet<TbOrderOperation> TbOrderOperation { get; set; }
        public virtual DbSet<TbOrderRefund> TbOrderRefund { get; set; }
        public virtual DbSet<TbOrderRefundAudit> TbOrderRefundAudit { get; set; }
        public virtual DbSet<TbOrderServicemeasurement> TbOrderServicemeasurement { get; set; }
        public virtual DbSet<TbOrderServicesign> TbOrderServicesign { get; set; }
        public virtual DbSet<TbOrderServicetrack> TbOrderServicetrack { get; set; }
        public virtual DbSet<TbOrderSign> TbOrderSign { get; set; }
        public virtual DbSet<TbOrderStruck> TbOrderStruck { get; set; }
        public virtual DbSet<TbOrderSupervise> TbOrderSupervise { get; set; }
        public virtual DbSet<TbOrderTrack> TbOrderTrack { get; set; }
        public virtual DbSet<TbOrderWastage> TbOrderWastage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Data Source=localhost;port=3306;Initial Catalog=db_lj_order;user id=root;password=Jianglei105;Character Set=utf8;SslMode=None;");
            }
            //var loggerFactory = new LoggerFactory();
            //loggerFactory.AddProvider(new EFLoggerProvider());
            //optionsBuilder.UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAccount>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tb_account");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.AccountConfig)
                    .HasColumnName("account_config")
                    .HasColumnType("varchar(2048)");

                entity.Property(e => e.AccountState)
                    .HasColumnName("account_state")
                    .HasColumnType("int(200)");

                entity.Property(e => e.Addr)
                    .HasColumnName("addr")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CompanyLevel)
                    .HasColumnName("company_level")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DecoratorLevel)
                    .HasColumnName("decorator_level")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'10'");

                entity.Property(e => e.DecoratorReceiveArea)
                    .HasColumnName("decorator_receive_area")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.DecoratorTags)
                    .HasColumnName("decorator_tags")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Oldpwd)
                    .HasColumnName("oldpwd")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Openid)
                    .HasColumnName("openid")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Pwd)
                    .HasColumnName("pwd")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RegistTime)
                    .HasColumnName("regist_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Tels)
                    .HasColumnName("tels")
                    .HasColumnType("varchar(1000)");
            });

            modelBuilder.Entity<TbDecoratorPerson>(entity =>
            {
                entity.ToTable("tb_decorator_person");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OppUser)
                    .HasColumnName("opp_user")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.PersonTel)
                    .HasColumnName("person_tel")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<TbDecoratorSetting>(entity =>
            {
                entity.ToTable("tb_decorator_setting");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.CompanyLevel)
                    .HasColumnName("company_level")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DayPolicy)
                    .HasColumnName("day_policy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DecoratorStatus)
                    .HasColumnName("decorator_status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Jsontext)
                    .HasColumnName("jsontext")
                    .HasColumnType("text");

                entity.Property(e => e.MonthPolicy)
                    .HasColumnName("month_policy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OppUser)
                    .HasColumnName("opp_user")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OtherJsontext)
                    .HasColumnName("other_jsontext")
                    .HasColumnType("varchar(1024)");
            });

            modelBuilder.Entity<TbDecoratorSnapshot>(entity =>
            {
                entity.ToTable("tb_decorator_snapshot");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AskStarttime)
                    .HasColumnName("ask_starttime")
                    .HasColumnType("datetime");

                entity.Property(e => e.AskState)
                    .HasColumnName("ask_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssignType)
                    .HasColumnName("assign_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.AssignUser)
                    .HasColumnName("assign_user")
                    .HasColumnType("bigint(100)");

                entity.Property(e => e.AssignUsername)
                    .HasColumnName("assign_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.BuildType)
                    .HasColumnName("build_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Building)
                    .HasColumnName("building")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CurOrderstate)
                    .HasColumnName("cur_orderstate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CustomerAddrArea)
                    .HasColumnName("customer_addr_area")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerAddrCommunity)
                    .HasColumnName("customer_addr_community")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerAddrHourse)
                    .HasColumnName("customer_addr_hourse")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerBudget)
                    .HasColumnName("customer_budget")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerChudanbeizhu)
                    .HasColumnName("customer_chudanbeizhu")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerChudanyuan)
                    .HasColumnName("customer_chudanyuan")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerLevel)
                    .HasColumnName("customer_level")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerMeasurementTime)
                    .HasColumnName("customer_measurement_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customer_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderNo)
                    .HasColumnName("customer_order_no")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderStatus)
                    .HasColumnName("customer_order_status")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderTags)
                    .HasColumnName("customer_order_tags")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerPrice)
                    .HasColumnName("customer_price")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRegistTime)
                    .HasColumnName("customer_regist_time")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.CustomerRoomSize)
                    .HasColumnName("customer_room_size")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRoomType)
                    .HasColumnName("customer_room_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerSituation)
                    .HasColumnName("customer_situation")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerTel)
                    .HasColumnName("customer_tel")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DecorationStyle)
                    .HasColumnName("decoration_style")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DecorationType)
                    .HasColumnName("decoration_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DecorationWay)
                    .HasColumnName("decoration_way")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(200)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DispatchCount)
                    .HasColumnName("dispatch_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Isnew)
                    .HasColumnName("isnew")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Isurgent)
                    .HasColumnName("isurgent")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LastUpdateTime)
                    .HasColumnName("last_update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.ProjectCost)
                    .HasColumnName("project_cost")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ReceiveCount)
                    .HasColumnName("receive_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiveUsername)
                    .HasColumnName("receive_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.SignComment)
                    .HasColumnName("sign_comment")
                    .HasColumnType("text");

                entity.Property(e => e.SignCompany)
                    .HasColumnName("sign_company")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SignPrice)
                    .HasColumnName("sign_price")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignStartTime)
                    .HasColumnName("sign_start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SignState)
                    .HasColumnName("sign_state")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignTime)
                    .HasColumnName("sign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackAssignTime)
                    .HasColumnName("track_assign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackId)
                    .HasColumnName("track_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUser)
                    .HasColumnName("track_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUsername)
                    .HasColumnName("track_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TypeinComment)
                    .HasColumnName("typein_comment")
                    .HasColumnType("text");

                entity.Property(e => e.TypeinState)
                    .HasColumnName("typein_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TypeinTime)
                    .HasColumnName("typein_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TypeinUser)
                    .HasColumnName("typein_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TypeinUsername)
                    .HasColumnName("typein_username")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<TbFinanceAccount>(entity =>
            {
                entity.ToTable("tb_finance_account");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AccountPwd)
                    .HasColumnName("account_pwd")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AccountStatus)
                    .HasColumnName("account_status")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasColumnType("text");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbFinanceBill>(entity =>
            {
                entity.ToTable("tb_finance_bill");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerOrderNo)
                    .HasColumnName("customer_order_no")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.ExtData)
                    .HasColumnName("ext_data")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.Fk1Str)
                    .HasColumnName("fk1_str")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.Fk2Str)
                    .HasColumnName("fk2_str")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.Fk3Str)
                    .HasColumnName("fk3_str")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.Fk4Str)
                    .HasColumnName("fk4_str")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.FromBalance)
                    .HasColumnName("from_balance")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.FromEntityId)
                    .HasColumnName("from_entity_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.FromEntityType)
                    .HasColumnName("from_entity_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FromTradeMoney)
                    .HasColumnName("from_trade_money")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.FromTradeRemarks)
                    .HasColumnName("from_trade_remarks")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.PayType)
                    .HasColumnName("pay_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ToBalance)
                    .HasColumnName("to_balance")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.ToEntityId)
                    .HasColumnName("to_entity_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ToEntityType)
                    .HasColumnName("to_entity_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ToTradeMoney)
                    .HasColumnName("to_trade_money")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.ToTradeRemarks)
                    .HasColumnName("to_trade_remarks")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.TradeNo)
                    .HasColumnName("trade_no")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.TradeTime)
                    .HasColumnName("trade_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TransRemark)
                    .HasColumnName("trans_remark")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.TransType)
                    .HasColumnName("trans_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserRemarks)
                    .HasColumnName("user_remarks")
                    .HasColumnType("varchar(1024)");
            });

            modelBuilder.Entity<TbFinanceEntity>(entity =>
            {
                entity.ToTable("tb_finance_entity");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Dopolicycount)
                    .HasColumnName("dopolicycount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EndPolicytime)
                    .HasColumnName("end_policytime")
                    .HasColumnType("datetime");

                entity.Property(e => e.EntityType)
                    .HasColumnName("entity_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartPolicytime)
                    .HasColumnName("start_policytime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbFinanceMonthbill>(entity =>
            {
                entity.ToTable("tb_finance_monthbill");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Monthdate)
                    .HasColumnName("monthdate")
                    .HasColumnType("date");

                entity.Property(e => e.PreviousBalance)
                    .HasColumnName("previous_balance")
                    .HasColumnType("decimal(32,8)");
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.ToTable("tb_order");

                entity.HasIndex(e => e.CustomerOrderNo)
                    .HasName("customer_order_no");

                entity.HasIndex(e => e.CustomerTel)
                    .HasName("customer_tel");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AskStarttime)
                    .HasColumnName("ask_starttime")
                    .HasColumnType("datetime");

                entity.Property(e => e.AskState)
                    .HasColumnName("ask_state")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'20'");

                entity.Property(e => e.CustomerAddrArea)
                    .HasColumnName("customer_addr_area")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerAddrCommunity)
                    .HasColumnName("customer_addr_community")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerAddrHourse)
                    .HasColumnName("customer_addr_hourse")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerBudget)
                    .HasColumnName("customer_budget")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerChudanbeizhu)
                    .HasColumnName("customer_chudanbeizhu")
                    .HasColumnType("text");

                entity.Property(e => e.CustomerChudanyuan)
                    .HasColumnName("customer_chudanyuan")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerLevel)
                    .HasColumnName("customer_level")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerMeasurementTime)
                    .HasColumnName("customer_measurement_time")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customer_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderNo)
                    .HasColumnName("customer_order_no")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderStatus)
                    .HasColumnName("customer_order_status")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderTags)
                    .HasColumnName("customer_order_tags")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerPrice)
                    .HasColumnName("customer_price")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRegistTime)
                    .HasColumnName("customer_regist_time")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRoomSize)
                    .HasColumnName("customer_room_size")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRoomType)
                    .HasColumnName("customer_room_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerSituation)
                    .HasColumnName("customer_situation")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerTel)
                    .HasColumnName("customer_tel")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.LastUpdateTime)
                    .HasColumnName("last_update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReceiveCount)
                    .HasColumnName("receive_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiveUsername)
                    .HasColumnName("receive_username")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.SignComment)
                    .HasColumnName("sign_comment")
                    .HasColumnType("text");

                entity.Property(e => e.SignCompany)
                    .HasColumnName("sign_company")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SignPrice)
                    .HasColumnName("sign_price")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.SignStartTime)
                    .HasColumnName("sign_start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SignState)
                    .HasColumnName("sign_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SignTime)
                    .HasColumnName("sign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackAssignTime)
                    .HasColumnName("track_assign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackAssigner)
                    .HasColumnName("track_assigner")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUser)
                    .HasColumnName("track_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUsername)
                    .HasColumnName("track_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TypeinComment)
                    .HasColumnName("typein_comment")
                    .HasColumnType("text");

                entity.Property(e => e.TypeinTime)
                    .HasColumnName("typein_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TypeinUser)
                    .HasColumnName("typein_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TypeinUsername)
                    .HasColumnName("typein_username")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<TbOrderActive>(entity =>
            {
                entity.ToTable("tb_order_active");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ActiveReason)
                    .HasColumnName("active_reason")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.ActiveState)
                    .HasColumnName("active_state")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TbOrderAssign>(entity =>
            {
                entity.ToTable("tb_order_assign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AskTime)
                    .HasColumnName("ask_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignState)
                    .HasColumnName("assign_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssignTime)
                    .HasColumnName("assign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignUser)
                    .HasColumnName("assign_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ChargebackApplyComment)
                    .HasColumnName("chargeback_apply_comment")
                    .HasColumnType("text");

                entity.Property(e => e.ChargebackApplyTime)
                    .HasColumnName("chargeback_apply_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChargebackState)
                    .HasColumnName("chargeback_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DecoratorTels)
                    .HasColumnName("decorator_tels")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.MeasurementComment)
                    .HasColumnName("measurement_comment")
                    .HasColumnType("text");

                entity.Property(e => e.MeasurementState)
                    .HasColumnName("measurement_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MeasurementTime)
                    .HasColumnName("measurement_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PhoneBridgeCount)
                    .HasColumnName("phone_bridge_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PhoneBridgeExpireTime)
                    .HasColumnName("phone_bridge_expire_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReceiveComment)
                    .HasColumnName("receive_comment")
                    .HasColumnType("text");

                entity.Property(e => e.ReceiveState)
                    .HasColumnName("receive_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiveTime)
                    .HasColumnName("receive_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SignState)
                    .HasColumnName("sign_state")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TbOrderCompleted>(entity =>
            {
                entity.ToTable("tb_order_completed");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CompletedFinishWorktime)
                    .HasColumnName("completed_finish_worktime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CompletedPictureurl)
                    .HasColumnName("completed_pictureurl")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.CompletedStartWorktime)
                    .HasColumnName("completed_start_worktime")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TbOrderDecoratorFlow>(entity =>
            {
                entity.ToTable("tb_order_decorator_flow");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ActiveId)
                    .HasColumnName("active_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ActiveTime)
                    .HasColumnName("active_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AskTime)
                    .HasColumnName("ask_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignState)
                    .HasColumnName("assign_state")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AssignTime)
                    .HasColumnName("assign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignType)
                    .HasColumnName("assign_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AssignUser)
                    .HasColumnName("assign_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CompleteState)
                    .HasColumnName("complete_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompleteTime)
                    .HasColumnName("complete_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CompletedId)
                    .HasColumnName("completed_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CurDecoreatorstate)
                    .HasColumnName("cur_decoreatorstate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.DesignDirector)
                    .HasColumnName("design_director")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.DesignDirectorBridgetel)
                    .HasColumnName("design_director_bridgetel")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DesignDirectorTel)
                    .HasColumnName("design_director_tel")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DesignateState)
                    .HasColumnName("designate_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Designer)
                    .HasColumnName("designer")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.DesignerBridgetel)
                    .HasColumnName("designer_bridgetel")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DesignerTel)
                    .HasColumnName("designer_tel")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.DispatchId)
                    .HasColumnName("dispatch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.FeedbackId)
                    .HasColumnName("feedback_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.FeedbackTime)
                    .HasColumnName("feedback_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MeasurementComment)
                    .HasColumnName("measurement_comment")
                    .HasColumnType("varchar(3048)");

                entity.Property(e => e.MeasurementId)
                    .HasColumnName("measurement_id")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.MeasurementNextflowupTime)
                    .HasColumnName("measurement_nextflowup_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MeasurementState)
                    .HasColumnName("measurement_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MeasurementTime)
                    .HasColumnName("measurement_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PhoneBridgeCount)
                    .HasColumnName("phone_bridge_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PhoneBridgeExpireTime)
                    .HasColumnName("phone_bridge_expire_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReceiveComment)
                    .HasColumnName("receive_comment")
                    .HasColumnType("varchar(3048)");

                entity.Property(e => e.ReceiveState)
                    .HasColumnName("receive_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiveTime)
                    .HasColumnName("receive_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.RefundId)
                    .HasColumnName("refund_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RefundState)
                    .HasColumnName("refund_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceBridgetel)
                    .HasColumnName("service_bridgetel")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ServiceName)
                    .HasColumnName("service_name")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.ServiceTel)
                    .HasColumnName("service_tel")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SignId)
                    .HasColumnName("sign_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SignState)
                    .HasColumnName("sign_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SignTime)
                    .HasColumnName("sign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SnapshotId)
                    .HasColumnName("snapshot_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StartoperationState)
                    .HasColumnName("startoperation_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartoperationTime)
                    .HasColumnName("startoperation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StruckId)
                    .HasColumnName("struck_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StruckNextflowupTime)
                    .HasColumnName("struck_nextflowup_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StruckState)
                    .HasColumnName("struck_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StruckTime)
                    .HasColumnName("struck_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SuperviseId)
                    .HasColumnName("supervise_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SuperviseState)
                    .HasColumnName("supervise_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SuperviseTime)
                    .HasColumnName("supervise_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.WastageId)
                    .HasColumnName("wastage_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.WastageState)
                    .HasColumnName("wastage_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WastageTime)
                    .HasColumnName("wastage_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOrderDispatch>(entity =>
            {
                entity.ToTable("tb_order_dispatch");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AskTime)
                    .HasColumnName("ask_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignState)
                    .HasColumnName("assign_state")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AssignTime)
                    .HasColumnName("assign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignUser)
                    .HasColumnName("assign_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MeasurementTime)
                    .HasColumnName("measurement_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ReceiveComment)
                    .HasColumnName("receive_comment")
                    .HasColumnType("text");

                entity.Property(e => e.ReceiveState)
                    .HasColumnName("receive_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiveTime)
                    .HasColumnName("receive_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.RejectReason)
                    .HasColumnName("reject_reason")
                    .HasColumnType("varchar(1024)");
            });

            modelBuilder.Entity<TbOrderFeedback>(entity =>
            {
                entity.ToTable("tb_order_feedback");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.FeedbackRemarks)
                    .HasColumnName("feedback_remarks")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TbOrderFlow>(entity =>
            {
                entity.ToTable("tb_order_flow");

                entity.HasIndex(e => e.CustomerOrderNo)
                    .HasName("customer_order_no");

                entity.HasIndex(e => e.CustomerTel)
                    .HasName("customer_tel");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AskStarttime)
                    .HasColumnName("ask_starttime")
                    .HasColumnType("datetime");

                entity.Property(e => e.AskState)
                    .HasColumnName("ask_state")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'20'");

                entity.Property(e => e.AssignCount)
                    .HasColumnName("assign_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssignTime)
                    .HasColumnName("assign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignType)
                    .HasColumnName("assign_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BuildType)
                    .HasColumnName("build_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Building)
                    .HasColumnName("building")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.CurOrderstate)
                    .HasColumnName("cur_orderstate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CustomerAddrArea)
                    .HasColumnName("customer_addr_area")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerAddrCommunity)
                    .HasColumnName("customer_addr_community")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerAddrHourse)
                    .HasColumnName("customer_addr_hourse")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerBudget)
                    .HasColumnName("customer_budget")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerChudanbeizhu)
                    .HasColumnName("customer_chudanbeizhu")
                    .HasColumnType("text");

                entity.Property(e => e.CustomerChudanyuan)
                    .HasColumnName("customer_chudanyuan")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerLevel)
                    .HasColumnName("customer_level")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerMeasurementTime)
                    .HasColumnName("customer_measurement_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customer_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOldmeasurementTime)
                    .HasColumnName("customer_oldmeasurement_time")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.CustomerOrderNo)
                    .HasColumnName("customer_order_no")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderStatus)
                    .HasColumnName("customer_order_status")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerOrderTags)
                    .HasColumnName("customer_order_tags")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerPrice)
                    .HasColumnName("customer_price")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRegistTime)
                    .HasColumnName("customer_regist_time")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRoomSize)
                    .HasColumnName("customer_room_size")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerRoomType)
                    .HasColumnName("customer_room_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CustomerSituation)
                    .HasColumnName("customer_situation")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CustomerTel)
                    .HasColumnName("customer_tel")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DecorationStyle)
                    .HasColumnName("decoration_style")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.DecorationType)
                    .HasColumnName("decoration_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.DecorationWay)
                    .HasColumnName("decoration_way")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.DispatchCount)
                    .HasColumnName("dispatch_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Isnew)
                    .HasColumnName("isnew")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Isurgent)
                    .HasColumnName("isurgent")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LastUpdateTime)
                    .HasColumnName("last_update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProjectCost)
                    .HasColumnName("project_cost")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.ProjectOrderstate)
                    .HasColumnName("project_orderstate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.ReceiveCount)
                    .HasColumnName("receive_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiveUsername)
                    .HasColumnName("receive_username")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.ServiceMeasurementId)
                    .HasColumnName("service_measurement_id")
                    .HasColumnType("bigint(11)");

                entity.Property(e => e.ServiceMeasurementState)
                    .HasColumnName("service_measurement_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceSignId)
                    .HasColumnName("service_sign_id")
                    .HasColumnType("bigint(11)");

                entity.Property(e => e.ServiceSignState)
                    .HasColumnName("service_sign_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SignComment)
                    .HasColumnName("sign_comment")
                    .HasColumnType("text");

                entity.Property(e => e.SignCompany)
                    .HasColumnName("sign_company")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SignPrice)
                    .HasColumnName("sign_price")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.SignStartTime)
                    .HasColumnName("sign_start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SignState)
                    .HasColumnName("sign_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SignTime)
                    .HasColumnName("sign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackAssignTime)
                    .HasColumnName("track_assign_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackId)
                    .HasColumnName("track_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUser)
                    .HasColumnName("track_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUsername)
                    .HasColumnName("track_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TypeinComment)
                    .HasColumnName("typein_comment")
                    .HasColumnType("text");

                entity.Property(e => e.TypeinState)
                    .HasColumnName("typein_state")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TypeinTime)
                    .HasColumnName("typein_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TypeinUser)
                    .HasColumnName("typein_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TypeinUsername)
                    .HasColumnName("typein_username")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<TbOrderLog>(entity =>
            {
                entity.ToTable("tb_order_log");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.EventCategory)
                    .IsRequired()
                    .HasColumnName("event_category")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ExtData)
                    .HasColumnName("ext_data")
                    .HasColumnType("text");

                entity.Property(e => e.FlowRemark)
                    .HasColumnName("flow_remark")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.OrderAssignId)
                    .HasColumnName("order_assign_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TbOrderMeasurement>(entity =>
            {
                entity.ToTable("tb_order_measurement");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.MeasurementAddress)
                    .HasColumnName("measurement_address")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.MeasurementAppointmentTime)
                    .HasColumnName("measurement_appointment_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MeasurementFlowupTime)
                    .HasColumnName("measurement_flowup_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MeasurementIntention)
                    .HasColumnName("measurement_intention")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.MeasurementNextflowupTime)
                    .HasColumnName("measurement_nextflowup_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MeasurementRecordtxt)
                    .HasColumnName("measurement_recordtxt")
                    .HasColumnType("varchar(2048)");

                entity.Property(e => e.MeasurementRecordtype)
                    .HasColumnName("measurement_recordtype")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.MeasurementState)
                    .HasColumnName("measurement_state")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.MeasurementTime)
                    .HasColumnName("measurement_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TbOrderOperation>(entity =>
            {
                entity.ToTable("tb_order_operation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CurData)
                    .HasColumnName("cur_data")
                    .HasColumnType("text");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptRole)
                    .HasColumnName("opt_role")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(32)");

                entity.Property(e => e.OrderNo)
                    .HasColumnName("order_no")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.OrginData)
                    .HasColumnName("orgin_data")
                    .HasColumnType("text");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasColumnType("varchar(512)");
            });

            modelBuilder.Entity<TbOrderRefund>(entity =>
            {
                entity.ToTable("tb_order_refund");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuditId)
                    .HasColumnName("audit_id")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.BillId)
                    .HasColumnName("bill_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Prove)
                    .HasColumnName("prove")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.RefundContent)
                    .HasColumnName("refund_content")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.RefundReason)
                    .HasColumnName("refund_reason")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.RefundStatus)
                    .HasColumnName("refund_status")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TbOrderRefundAudit>(entity =>
            {
                entity.ToTable("tb_order_refund_audit");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AuditPerson)
                    .HasColumnName("audit_person")
                    .HasColumnType("bigint(32)");

                entity.Property(e => e.AuditRemark)
                    .HasColumnName("audit_remark")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.AuditResult)
                    .HasColumnName("audit_result")
                    .HasColumnType("int(32)");

                entity.Property(e => e.AuditTime)
                    .HasColumnName("audit_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.RefundId)
                    .HasColumnName("refund_id")
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<TbOrderServicemeasurement>(entity =>
            {
                entity.ToTable("tb_order_servicemeasurement");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ServiceMeasurementRecordtxt)
                    .HasColumnName("service_measurement_recordtxt")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.ServiceMeasurementTime)
                    .HasColumnName("service_measurement_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOrderServicesign>(entity =>
            {
                entity.ToTable("tb_order_servicesign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorUser)
                    .HasColumnName("decorator_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorUsername)
                    .HasColumnName("decorator_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ServiceRebateRate)
                    .HasColumnName("service_rebate_rate")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.ServiceSignMoney)
                    .HasColumnName("service_sign_money")
                    .HasColumnType("decimal(32,8)");

                entity.Property(e => e.ServiceSignTime)
                    .HasColumnName("service_sign_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOrderServicetrack>(entity =>
            {
                entity.ToTable("tb_order_servicetrack");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackComment)
                    .HasColumnName("track_comment")
                    .HasColumnType("text");

                entity.Property(e => e.TrackTime)
                    .HasColumnName("track_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackUser)
                    .HasColumnName("track_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUsername)
                    .HasColumnName("track_username")
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<TbOrderSign>(entity =>
            {
                entity.ToTable("tb_order_sign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SignAddress)
                    .HasColumnName("sign_address")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignBuildType)
                    .HasColumnName("sign_build_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignContract)
                    .HasColumnName("sign_contract")
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.SignCustomerName)
                    .HasColumnName("sign_customer_name")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignCustomerTel)
                    .HasColumnName("sign_customer_tel")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignDecorationWay)
                    .HasColumnName("sign_decoration_way")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignFinishTime)
                    .HasColumnName("sign_finish_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SignPrice)
                    .HasColumnName("sign_price")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.SignStartTime)
                    .HasColumnName("sign_start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SignTime)
                    .HasColumnName("sign_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOrderStruck>(entity =>
            {
                entity.ToTable("tb_order_struck");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StruckFlowupTime)
                    .HasColumnName("struck_flowup_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StruckIntention)
                    .HasColumnName("struck_intention")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.StruckNextflowupTime)
                    .HasColumnName("struck_nextflowup_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StruckProgress)
                    .HasColumnName("struck_progress")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.StruckRecordtxt)
                    .HasColumnName("struck_recordtxt")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.StruckRecordtype)
                    .HasColumnName("struck_recordtype")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.StruckState)
                    .HasColumnName("struck_state")
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<TbOrderSupervise>(entity =>
            {
                entity.ToTable("tb_order_supervise");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SuperviseContent)
                    .HasColumnName("supervise_content")
                    .HasColumnType("text");

                entity.Property(e => e.SuperviseState)
                    .HasColumnName("supervise_state")
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<TbOrderTrack>(entity =>
            {
                entity.ToTable("tb_order_track");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.NextTrackTime)
                    .HasColumnName("next_track_time")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackComment)
                    .HasColumnName("track_comment")
                    .HasColumnType("text");

                entity.Property(e => e.TrackTime)
                    .HasColumnName("track_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrackUser)
                    .HasColumnName("track_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TrackUsername)
                    .HasColumnName("track_username")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<TbOrderWastage>(entity =>
            {
                entity.ToTable("tb_order_wastage");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecoratorFlowId)
                    .HasColumnName("decorator_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OptUser)
                    .HasColumnName("opt_user")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.OptUsername)
                    .HasColumnName("opt_username")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OrderFlowId)
                    .HasColumnName("order_flow_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.WastageContent)
                    .HasColumnName("wastage_content")
                    .HasColumnType("text");

                entity.Property(e => e.WastageLoseReason)
                    .HasColumnName("wastage_lose_reason")
                    .HasColumnType("varchar(32)");
            });
        }
    }
}
