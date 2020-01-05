{
  "version": 3,
  "targets": {
    ".NETStandard,Version=v2.0": {
      "Dapper/1.60.6": {
        "type": "package",
        "dependencies": {
          "System.Data.SqlClient": "4.4.0",
          "System.Reflection.Emit.Lightweight": "4.3.0",
          "System.Reflection.TypeExtensions": "4.4.0"
        },
        "compile": {
          "lib/netstandard2.0/Dapper.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Dapper.dll": {}
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
      "Microsoft.NETCore.Targets/1.1.0": {
        "type": "package",
        "compile": {
          "lib/netstandard1.0/_._": {}
        },
        "runtime": {
          "lib/netstandard1.0/_._": {}
        }
      },
      "Microsoft.Win32.Registry/4.4.0": {
        "type": "package",
        "dependencies": {
          "System.Security.AccessControl": "4.4.0",
          "System.Security.Principal.Windows": "4.4.0"
        },
        "comp