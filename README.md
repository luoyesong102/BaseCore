# 跨平台基础平台
 

# 说明  
 功能:VUE+WEBAPI,基础权限管理,统一认证管理，任务管理平台  
 中间件:JWT认证,异常,配置管理,sql管理，swagger接口管理  
 组件:ioc,反射，消息，db(支持mssql和mysql)，请求响应格式，DB验证，业务对象验证  
 部署:nginx+docker+k8s部署  
 微服务:cap,kong,istio运用  

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
