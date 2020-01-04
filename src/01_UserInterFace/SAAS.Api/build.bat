
https://blog.csdn.net/cckevincyh/article/details/92088862  docker for windwos下安装
https://www.cnblogs.com/kingkangstudy/p/9221041.html   发布webapi



/*******************发布webapi后简单构建*******************/
# 添加基础镜像
FROM microsoft/dotnet:2.1-aspnetcore-runtime
#容器中系统的工作空间
WORKDIR /app
#拷贝当前文件夹下的文件到容器中系统的工作空间
COPY . /app
 
#设置Docker容器对外暴露的端口
EXPOSE 80
#容器中使用 ["dotnet","系统启动的dll"] 来运行应用程序
#使用ENTRYPOINT ["dotnet","系统启动的dll"]
#或使用 CMD ["dotnet","系统启动的dll"]
ENTRYPOINT ["dotnet", "SAAS.Api.dll"]
/*********************************制作docker镜像**********************************/
docker build -t saas.api:1.0.0 . 

docker tag  saas.api:1.0.0   luoyesong102/saas.api:1.0.1  //版本控制生产环境的发布
docker login --username=luoyesong102
docker push  luoyesong102/saas.api:1.0.1
docker pull  luoyesong102/saas.api:1.0.1

docker run --name=saas.api -d -p 8000:80 -v /root/project/saas.api:/app saas.api:1.0.2   挂载镜像文件映射至服务器路径下，方便与修改后的发布管理
docker run --name=saas.api -d  --net=host -v /root/project/saas.api:/app saas.api:1.0.2   挂载镜像文件映射至服务器路径下，方便与修改后的发布管理

docker run --name=saas.api.docker -p 8000:80 -d  saas.api:1.0.2 
docker run --name=saas.api.docker --restart=always --net=host -d  saas.api:1.0.2 

docker rmi 5ddf122f2354
docker images
docker ps
/****************************运行docker镜像*******************************/
运行中的所有容器
docker container ls -aq
终止运行的容器
Docker container kill 容器id
docker container rm dd02d3f38db8
 docker stop 容器id
docker restart  容器id
docker logs  容器id

--name：指定容器名称

-p：指定容器端口

-d：指定容器 后台运行

-v test:/soft
对应就是-v /宿主机：/容器内目录，意思就是宿主机的/test目录挂载到容器的/soft目录！
这个命令，很重要，但凡搭建redis，mysql这样的容器，一定会用到数据卷挂载！

