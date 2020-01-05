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
  