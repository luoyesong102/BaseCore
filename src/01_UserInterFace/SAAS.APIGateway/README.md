# 说明
 1网关目前来说并非必须，在应用层可使用ocelot配合identityserver4使用，也可使用kong.net来进行编写
 2resful Api在性能要求不高的情况下,可满足使用，性能要求高，使用rpc发布服务,WebApiClient调用服务
 3分布式一致性（CAP来使用）