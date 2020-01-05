{
  "version": 3,
  "targets": {
    ".NETStandard,Version=v2.0": {
      "aliyun-net-sdk-core/1.5.2": {
        "type": "package",
        "compile": {
          "lib/netstandard2.0/aliyun-net-sdk-core.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/aliyun-net-sdk-core.dll": {}
        }
      },
      "aliyun-net-sdk-dysmsapi/1.0.0": {
        "type": "package",
        "dependencies": {
          "aliyun-net-sdk-core": "1.0.0"
        },
        "compile": {
          "lib/netstandard2.0/aliyun-net-sdk-dysmsapi.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/aliyun-net-sdk-dysmsapi.dll": {}
        }
      },
      "Aliyun.OSS.SDK.NetCore/2.9.1": {
        "type": "package",
        "compile": {
          "lib/netstandard2.0/Aliyun.OSS.Core.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Aliyun.OSS.Core.dll": {}
        }
      },
      "Microsoft.NETCore.Platforms/1.1.0": {
        "type": "package",
        "compile": {
          "lib/netstandard1.0/_._": {}
        },
        "runtime": {
          "lib/netstandard1.0/_._": {}
        }
      },
      "NETStandard.Library/2.0.3": {
        "type": "package",
        "dependencies": {
          "Microsoft.NETCore.Platforms": "1.1.0"
        },
        "compile": {
          "lib/netstandard1.0/_._": {}
        },
        "runtime": {
          "lib/netstandard1.0/_._": {}
        },
        "build": {
          "build/netstandard2.0/NETStandard.Library.targets": {}
        }
      },
      "Newtonsoft.Json/12.0.2": {
        "type": "package",
        "compile": {
          "lib/netstandard2.0/Newtonsoft.Json.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Newtonsoft.Json.dll": {}
        }
      },
      "Pipelines.Sockets.Unofficial/2.0.22": {
        "type": "package",
        "dependencies": {
          "System.Buffers": "4.4.0",
          "System.IO.Pipelines": "4.5.1",
          "System.Runtime.CompilerServices.Unsafe": "4.5.2"
        },
        "compile": {
          "lib/netstandard2.0/Pipelines.Sockets.Unofficial.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Pipelines.Sockets.Unofficial.dll": {}
        }
      },
      "Snowflake.Core/2.0.0": {
        "type": "package",
        "compile": {
          "lib/netstandard2.0/Snowflake.Core.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Snowflake.Core.dll": {}
        }
      },
      "StackExchange.Redis/2.0.601": {
        "type": "package",
        "dependencies": {
          "Pipelines.Sockets.Unofficial": "2.0.22",
          "System.Diagnostics.PerformanceCounter": "4.5.0",
          "System.IO.Pipelines": "4.5.1",
          "System.Threading.Channels": "4.5.0"
        },
        "compile": {
          "lib/netstandard2.0/StackExchange.Redis.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/StackExchange.Redis.dll": {}
        }
      },
      "System.Buffers/4.4.0": {
        "type": "package",
        "compile": {
          "ref/netstandard2.0/System.Buffers.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/System.Buffers.dll": {}
        }
      },
      "System.Diagnostics.PerformanceCounter/4.5.0": {
        "type": "package",
        "compile": {
          "ref/netstandard2.0/System.Diagnostics.PerformanceCounter.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/System.Diagnostics.PerformanceCounter.dll": {}
        }
      },
      "System.IO.Pipelines/4.5.1": {
        "type": "package",
        "dependencies": {
          "System.Buffers": "4.4.0",
          "System.Memory": "4.5.1",
          "System.Threading.Tasks.Extensions": "4.5.1"
        },
        "compile": {