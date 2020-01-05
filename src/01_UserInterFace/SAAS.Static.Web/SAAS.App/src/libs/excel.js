{
  "version": 3,
  "targets": {
    ".NETCoreApp,Version=v2.0": {
      "Libuv/1.10.0": {
        "type": "package",
        "dependencies": {
          "Microsoft.NETCore.Platforms": "1.0.1"
        },
        "runtimeTargets": {
          "runtimes/linux-arm/native/libuv.so": {
            "assetType": "native",
            "rid": "linux-arm"
          },
          "runtimes/linux-arm64/native/libuv.so": {
            "assetType": "native",
            "rid": "linux-arm64"
          },
          "runtimes/linux-armel/native/libuv.so": {
            "assetType": "native",
            "rid": "linux-armel"
          },
          "runtimes/linux-x64/native/libuv.so": {
            "assetType": "native",
            "rid": "linux-x64"
          },
          "runtimes/osx/native/libuv.dylib": {
            "assetType": "native",
            "rid": "osx"
          },
          "runtimes/win-arm/native/libuv.dll": {
            "assetType": "native",
            "rid": "win-arm"
          },
          "runtimes/win-x64/native/libuv.dll": {
            "assetType": "native",
            "rid": "win-x64"
          },
          "runtimes/win-x86/native/libuv.dll": {
            "assetType": "native",
            "rid": "win-x86"
          }
        }
      },
      "Microsoft.ApplicationInsights/2.4.0": {
        "type": "package",
        "dependencies": {
          "NETStandard.Library": "1.6.1",
          "System.Diagnostics.DiagnosticSource": "4.4.0",
          "System.Diagnostics.StackTrace": "4.3.0"
        },
        "compile": {
          "lib/netstandard1.3/Microsoft.ApplicationInsights.dll": {}
        },
        "runtime": {
          "lib/netstandard1.3/Microsoft.ApplicationInsights.dll": {}
        }
      },
      "Microsoft.ApplicationInsights.AspNetCore/2.1.1": {
        "type": "package",
        "dependencies": {
          "Microsoft.ApplicationInsights": "2.4.0",
          "Microsoft.ApplicationInsights.DependencyCollector": "2.4.1",
          "Microsoft.AspNetCore.Hosting": "1.0.0",
          "Microsoft.Extensions.Configuration": "1.0.0",
          "Microsoft.Extensions.Configuration.Json": "1.0.0",
          "Microsoft.Extensions.DiagnosticAdapter": "1.0.0",
          "Microsoft.Extensions.Logging.Abstractions": "1.0.0",
          "NETStandard.Library": "1.6.1",
          "System.Net.NameResolution": "4.3.0",
          "System.Text.Encodings.Web": "4.3.1"
        },
        "compile": {
          "lib/netstandard1.6/Microsoft.ApplicationInsights.AspNetCore.dll": {}
        },
        "runtime": {
          "lib/netstandard1.6/Microsoft.ApplicationInsights.AspNetCore.dll": {}
        }
      },
      "Microsoft.ApplicationInsights.DependencyCollector/2.4.1": {
        "type": "package",
        "dependencies": {
          "Microsoft.ApplicationInsights": "[2.4.0]",
          "Microsoft.Extensions.PlatformAbstractions": "1.1.0",
          "NETStandard.Library": "1.6.1",
          "System.Diagnostics.DiagnosticSource": "4.4.0",
          "System.Diagnostics.StackTrace": "4.3.0"
        },
        "compile": {
          "lib/netstandard1.6/Microsoft.AI.DependencyCollector.dll": {}
        },
        "runtime": {
          "lib/netstandard1.6/Microsoft.AI.DependencyCollector.dll": {}
        }
      },
      "Microsoft.AspNetCore/2.0.3": {
        "type": "package",
        "dependencies": {
          "Microsoft.AspNetCore.Diagnostics": "2.0.3",
          "Microsoft.AspNetCore.Hosting": "2.0.3",
          "Microsoft.AspNetCore.Routing": "2.0.3",
          "Microsoft.AspNetCore.Server.IISIntegration": "2.0.3",
          "Microsoft.AspNetCore.Server.Kestrel": "2.0.3",
          "Microsoft.AspNetCore.Server.Kestrel.Https": "2.0.3",
  