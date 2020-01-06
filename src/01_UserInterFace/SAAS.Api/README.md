#  知识点说明    
1:配置文件使用    ConfigurationManager.Instance.Get<string>("ConnectionStrings", "Redis");  

2:自定义IOC容器   IocHelp.AddSingleton(item => new IdWorker(1, config.GetValue<int>("ServerNodeNo"))); var _valueServices =   IocHelp.Create<IdWorker>();//获取服务

3:调用IOC容器     ServiceLocatorManager.Configure(provider);//实例化容器以防止不是构造函数的注入的实例化  

4:内存初始化提供xml化sql管理    services.AddMemoryCache();//注入使用内存对象   CacheFactory.Init(_valueService);//初始化内存服务  string aaa = DataCommandHelper.GetDataCommandSql("LoadTopicByTopicName");//用之前需要初始化对象  

5:对象序列化  arg.orgin_data = new { arg.orgin_orderdata, arg.orgin_orderassigndata }.Serialize(); 组织新的对象   accountmodel.ConvertBySerialize<List<SellerAccountInfoModel>>();  var decoratoraddress =   decoratorsettingmodel.address.Deserialize<List<DecoratorAddressModel>>();      decoratoraddress.Serialize();  

6：传递对象   ApiReturn<T>  

7：异常封装   return new SsError{ errorCode = 2000,errorMessage = "订单编号重复，请重新确认订单编号！" }; return SsError.Err_NotAllowed;

8:分页对象    ApiReturn<PageData<Role1OrderListModel>> Opt11(PageInfo pager, DataFilter[] filter, SortItem[] sort)  
               var pageData = new PageData<Role1OrderListModel>(pager);  
                var sqlBuild = new Lj.Common.Module.Sql.DapperSqlBuild{  pager = pager, filter = filter, sort = sort };sqlBuild.Build();
                var sqlWhere = sqlBuild.sqlWhere; var sqlOrderBy = sqlBuild.sqlOrderBy;var sqlLimit = sqlBuild.sqlLimit;  
                sqlOrderBy = " order by " + (sqlOrderBy ?? "id desc");//   sqlWhere += " and isnew=1  and typein_user =" + userid;  
                pageData.totalCount = conn.ExecuteScalar<int>($"select count(1) from {OrderFlowData.tableName} where 1=1 " + sqlWhere, sqlBuild.sqlParam);  
                var sql = $"select * from {OrderFlowData.tableName} where 1=1 " + sqlWhere; sql += sqlOrderBy + sqlLimit;  
                pageData.rows = conn.Query<Role1OrderListModel>(sql, sqlBuild.sqlParam).ToList();  

9：获取XMLSQL string aaa = DataCommandHelper.GetDataCommandSql("LoadTopicByTopicName");//用之前需要初始化对象  

10:日志使用： Logger.log.info  

11:外部日志            services.AddSingleton<YH.SAAS.Helpers.Trace.ILogger, ExceptionLessLogger>();   
                       ExceptionlessClient.Default.Configuration.ApiKey = Configuration.GetSection("Exceptionless:ApiKey").Value;  
                       ExceptionlessClient.Default.Configuration.ServerUrl = Configuration.GetSection("Exceptionless:ServerUrl").Value;
                       app.UseExceptionless();     

12：Redis使用： using (var db = new DbRedis()){ string[] key = { "auth", "token" ,"at", token.at};    db.Set(token, token.at_ExpiresTime, key);}  

13:Redis注入服务使用     services.AddScoped<YH.SAAS.Helpers.Redis.Manage.RedisConnectionFactory>();   services.AddScoped<YH.SAAS.Helpers.Redis.Manage.RedisCache>(); 使用RedisCache  

14:MemoryCache使用 services.AddMemoryCache();   使用IMemoryCache    或者CacheFactory.Init(_valueService);      ICache instance = CacheFactory.GetInstance(cacheName);  

15:动态执行：proj文件下加入属性 <PropertyGroup><PreserveCompilationContext>true</PreserveCompilationContext> </PropertyGroup>   
string testClass = @" Action<ConsoleApp1.ModelA> action=(m)=>{ m.result = m.value + (int)args[0]; };return action;";  
                var action = CSharpCompiler.RunCodeBlock(new[] { "Sers.Framework.DynamicCompile.Demo.dll" }, testClass,3) as  Action<ConsoleApp1.ModelA>;  
                var m = new ModelA { value = 121 }; if (action != null){action(m);}  

				#region (x.1)获取项目匹配度算法源代码  
            string sourceCode = File.ReadAllText(CommonHelp.GetAbsPathByRealativePath("Data","PMR","PMR.cs"));  
            #endregion  
            #region (x.2)编译项目匹配度算法源代码并获取 计算的函数  
                var assembly = CSharpCompiler.CompileToAssembly(new[] { "App.ProjectMatchingRate.dll" }, sourceCode);  
                var type = assembly.GetType("Cache.Pmr.PmrAction");  
                var method = type?.GetMethod("GetAction");  
                var action = method?.Invoke(null, null) as Action<DecoratorInfo>;  
                var m = new DecoratorInfo {  };  
                if (action != null)  
                {  
                    action(m);ActionCalc = action;ret.success = true;  
                }  

16：excel导出：导入EXCEL本地存储,返回二进制流或者本地HTTP服务路径   Directory.CreateDirectory(CommonHelp.GetAbsPathByRealativePath("FileData"));  
            tempFile = CommonHelp.GetAbsPathByRealativePath("FileData", CommonHelp.NewGuidLong() + ".xlsx");  
                //(x.x.1)把数据集灌入生成的临时数据文件  
                ExcelHelp.SaveData(tempFile, ds); return  File.ReadAllBytes(absFilePath)  

17:图片上传:    生成本地文件传递路径byte[] bt = Convert.FromBase64String(basestr);  
                 File.WriteAllBytes(path, bt);  

18：内部消息服务：MessageBus.Instance.Pubish( new SystemBehaviorLogMessage(dbConnectionFactory.config, TableName, behaviorEnums, sql, strParam, tmpCurrentUser.Id, tmpCurrentUser.RealName, beginDateTime, endDateTime));  

19;外部消息：RabbitMQHelp 封装，以及CAP的使用，以及自己封装的发布订阅  

20 socket：HTTP请求通过TCP转发  

21：Linux下发布注意"configProperties": { "System.Globalization.Invariant": true }  路径需要注意 docker容器化，k8s管理容器及服务  

22：动态服务注入：将各层服务的对象注入进来  

23：认证：JWT认证，    有identityserver认证（分离资源服务器和认证服务器） 

24：Swagger管理接口及版本以及配合认证  

