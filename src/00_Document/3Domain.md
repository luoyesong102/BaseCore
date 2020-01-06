# 说明
0MSsql下的对象EFCORE及Dapper的领域操作   2：MySql下的对象EFcore及Dapper的领域操作  

1Codefirst生成对象：  Scaffold-DbContext "Data Source=SC-201610011543\MYDB;Initial Catalog=Sys_Base_Db;User ID=sa;Password=111111;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Sys_Base_Db  

2EFCORE教程:https://www.cnblogs.com/royzshare/p/9686706.html  

3仓储操作类封装：https://docs.microsoft.com/zh-cn/ef/core/extensions/index  

4表达对象之间的关系可以来进行表达（最好不映射至数据库）2延时加载最好只进行单体映射，否则会带来灾难的性能问题，可以用Include来通过表达的关系来进行赋值