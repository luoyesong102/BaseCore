# 跨平台基础平台
    采用Vue+WebApi前后端分离的方式进行开发，包含基础权限，统一认证，任务管理，消息中心，中间件  
    基础组件为业务系统提供基础服务，能更好的隔离业务和基础架构 ,业务系统架构在此基础上进行演变：中间件  
    基础组件将发布为NuGet包进行引用，其他认证，任务，消息将采用服务的方式来提供使用  
    该代码是极简的跨平台下的框架封装，已在linux下进行了生成环境运行  

# 开源地址
 GITHUB地址：[https://github.com/luoyesong102/BaseCore]

# 功能说明  
 功能:基础权限管理,IdentityS4统一认证管理(Oauth2.0客户端，用户密码,Code,单点模式)，任务管理平台  ，消息管理平台
 中间件:JWT认证,异常,配置管理,sql管理，swagger接口管理（Nuget包）  
 组件:ioc,反射，消息，db(支持mssql和mysql)，请求响应格式，DB验证，业务对象验证(Nuget包)  
 部署:nginx+docker+k8s部署  
 微服务:cap,kong,istio运用（未来应用的方向）

# 架构及部署
   1采用VUE+IVIEW+Dapper+Ef,分层采用领域建模的思想   
   2DDD领域分层原则,前后端分离，后端负责数据，前端负责路由和数据绑定,nginx负责反向代理,负载均衡，限流等,linux下docker镜像管理，守护进程或者K8S部署   
  3通用Framework的nuget包已上传https://nuget.org，大家可以搜索SAAS.FrameWork.IOC在项目中使用
  4封装最小组件封装：服务注入，IOC容器封装,AUTOFAC动态服务注入，Swagger定制版本管理中间件， JWT认证及扩展角色定制化中间件，全局配置文件管理,slqxml管理，全局用户对象管理,序列化赋值管理，Dapper多DB的操作，EF多DB操作，AOP管道中间件日志，内部发布订阅消息，缓存，EXCEL,文件操作,TCP的scoket通信，CAP外部消息及自定义消息组件，APM跟踪，EKL日志中心,gitlb源代码管理，jekens自动化发布，动态查询条件处理     
  5已采用docker进行发布镜像，大家可从https://cloud.docker.com拉取镜像docker pull  luoyesong102/saas.api:1.0.1 
  6配合gitlab-cicd使用，可参考.gitlab-ci.yml进行自动化构建 
  7单体linux下部署：主要服务：nginx，守护进程，或者docker镜像管理，KS8编排容器管理 ,linux下需要的软件开发组件：mysql ,gitlab ,jare,mindoc,jekens,dockerhub,nguget,k8s,redis,nginx,rabitmq,ekl,skaywaring   
  8大家可关注net_jiang公众号，有配套基础知识讲解。

#  跨平台改进			  
              IIS太重分离出来轻量级WEB服务器 
              MVC太重分离出来路由  
              Identityserver认证太重,采用jwt  
              配置文件太多，统一中间件管理  
              服务注册太多，统一AUTOFAC去管理  
              webapi注入中间件太重，抽象出网关中间件去认证（其他协议网关）分布式微服务，网关+服务治理 
              k8s太重，采用守护进程  
              EF太重,采用轻量级dapper（二者皆可实现）  
             分层太重，尽量在同一解决方案中,三层架构到DDD领域模型，只是设计上的改变  

#  未来的形态          
  企业级->互联网->微服务  
  1：企业级易开发维护的系统架构：注重设计和系统扩展性，稳定性，易维护性  
  2:  跨平台下单体webapi：在一定性能上能支持互联网并发量不大的用户体验，本身除了服务之间的通信需要上升至微服务架构，一般符合主流互联网产品开发模式
  3：跨平台微服务架构:由于认证，限流等具有通用性，采用网关统一入口，服务注册进行管理，服务发现进行服务调用，要求网关的性能，以及服务之间的解耦调用

# .NET Docker 发布 
# 前提条件  
Docker基本命令  
构建自己的镜像  
Docker Hub和私有仓库  
在云端运行容器  
Docker Compose 和 容器编排  
前提条件  
您在计算机上安装了以下软件：  

Docker Daemon或简称Docker  
.NET Core 2.1+ SDK  
代码编辑器，例如Visual Studio，VS Code。Visual Studio 不是必需的，但在整个Workshop 中可能会使用  
要在Windows上安装Docker，请点击此链接Docker For Windows  

# Docker基本命令
每个参与者必须了解Docker背后的基本命令和概念,请花点时间阅读它：.NET Core：入门的最简单方法

# 构建自己的镜像
在Fork 此仓库的根目录中有一个dockerfile文件，运行下面的命令：  
1：远程镜像发布  
docker build -t saas.api:4.0.0 .  
docker tag saas.api:4.0.0 luoyesong102/saas.api:4.0.0  //版本控制生产环境的发布  
docker login --username=luoyesong102  
docker push  luoyesong102/saas.api:4.0.0  
docker pull   luoyesong102/saas.api:4.0.0  
docker run --name=saas.api.docker -p 8000:80 -d  saas.api:4.0.0  
2：数据卷挂载发布运行（需要安装sdk）  
docker run --name=saas.api -d -p 8000:80 -v /root/webapi:/app saas.api:4.0.0   
docker build --pull -t aspnetapp .  
docker run --name aspnetcore_sample --rm -it -p 8000:80 aspnetapp  
Docker Hub和私有仓库  
Docker Hub 是Docker的官方存储库，它支持公共和私有存储库。大多数基本镜像都是从那里获取的，并由维护该镜像的公司（例如Microsoft，Node等）发布。  
3:执行数据库脚本及修改数据库连接地址以及前端API访问的地址（数据库脚本建议手动执行，webpack打包前更改API地址，已放至wwwwroot下）  

# Docker Compose 和 容器编排  
此项目上有一个docker-compose yaml文件，该文件运行并生成aspnet映像，并启动SQL Server容器。本次工作坊后面还有更复杂的微服务应用实例。你可以在根目录下通过docker-compose up来 初步体验一下，关于容器编排的k8s 相关内容参见后面的内容。  
version: '3.6'  

services:  
    aspnetapp:  
        image: saas.api:4.0.0  
        build:  
            context: .  
            dockerfile: Dockerfile  
        depends_on:  
            - mssql-express  
        ports:  
            - 80:80  
    mssql-express:  
        image: microsoft/mssql-server-windows-express  
        environment:  
            sa_password: L$clF^i2pue@8y  
            accept_eula: Y  
        ports:  
          - 1533:1433 # Use port 1533 to connect from the host machine using SSMS  
        #volumes:  
        #    - 'D:/DATA:C:/DATA'  
    redis:  
        image: "redis:nanoserver"  
 简单来讲：一体机部署的话，只需要一个docker-compose yaml运行起来即便可以立即访问站点，这对商业的演示是一个非常方便的做法。         
 # CICD发布程序  
  此项目中有一个.gitlab-ci.yml，在gitlab中CICD中执行  
  build_image:  
  script:  
    - cd /home/gitproject  
    - rm -rf /home/gitproject/Saas-Api  
    - git clone https://github.com/luoyesong102/BaseCore.git  
    - cd ./Saas-Api  
    - dotnet build --configuration Release   
   - rm -rf /home/project/Saas-Api  
    - docker stop gitci  
    - docker rm gitci  
    - docker rmi gitciimages  
    - dotnet publish "/home/gitproject/Saas-Api/src/01_UserInterFace/SAAS.Api/SAAS.Api.csproj" -c Release --output /home/project/Saas-Api  
    - cd /home/project/Saas-Api  
    - docker build -t gitciimages .  
    - docker run -d -v /home/project/Saas-Api:/app -p 8088:80 --name gitci gitciimages  
        
# 例子  
你可以在这个仓库里找到更多的 Docker 样例 : https://github.com/dotnet/dotnet-docker  

# 参考
跨平台  VUE+WebApi(前后端分离)+统一认证+EF(Dapper)
      https://cloud.tencent.com/developer/article/1375603  DncZeus
      https://github.com/cq-panda/Vue.NetCore    vue+netcore
      https://gitee.com/dugukuangshao/HPMessageCenter   rabbitmq
      https://github.com/zdz72113/NETCore_BasicKnowledge.Examples  base 
      https://github.com/EdisonChou/EDC.IdentityServer4.Samples  Indentity
      https://github.com/sersms/Sers_NetCore_HelloWorld  minsoft

微服务 (kong+k8s推荐)（基础设施：网关，服务治理，监控，部署,CI-CD）
    https://www.cnblogs.com/royzshare/p/10114198.html  JWT认证
    https://blog.csdn.net/playermaker57/article/details/86760521  devops
    https://www.cnblogs.com/edisonchou/p/aspnet_core_k8s_artcles_index.html  K8S
    https://www.cnblogs.com/edisonchou/p/dotnetcore_microservice_foundation_blogs_index_draft.html 微服务
    https://www.cnblogs.com/xishuai/p/microservices-and-service-mesh.html  微服务和服务网格
    https://www.cnblogs.com/WithLin/p/9343406.html   网关