{
  "version": 3,
  "targets": {
    ".NETStandard,Version=v2.0": {
      "Microsoft.AspNetCore.Antiforgery/2.1.1": {
        "type": "package",
        "dependencies": {
          "Microsoft.AspNetCore.DataProtection": "2.1.1",
          "Microsoft.AspNetCore.Http.Abstractions": "2.1.1",
          "Microsoft.AspNetCore.Http.Extensions": "2.1.1",
          "Microsoft.AspNetCore.WebUtilities": "2.1.1",
          "Microsoft.Extensions.ObjectPool": "2.1.1"
        },
        "compile": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Antiforgery.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Antiforgery.dll": {}
        }
      },
      "Microsoft.AspNetCore.Authentication.Abstractions/2.1.1": {
        "type": "package",
        "dependencies": {
          "Microsoft.AspNetCore.Http.Abstractions": "2.1.1",
          "Microsoft.Extensions.Logging.Abstractions": "2.1.1",
          "Microsoft.Extensions.Options": "2.1.1"
        },
        "compile": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Authentication.Abstractions.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Authentication.Abstractions.dll": {}
        }
      },
      "Microsoft.AspNetCore.Authentication.Core/2.1.1": {
        "type": "package",
        "dependencies": {
          "Microsoft.AspNetCore.Authentication.Abstractions": "2.1.1",
          "Microsoft.AspNetCore.Http": "2.1.1",
          "Microsoft.AspNetCore.Http.Extensions": "2.1.1"
        },
        "compile": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Authentication.Core.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Authentication.Core.dll": {}
        }
      },
      "Microsoft.AspNetCore.Authorization/2.1.1": {
        "type": "package",
        "dependencies": {
          "Microsoft.Extensions.Logging.Abstractions": "2.1.1",
          "Microsoft.Extensions.Options": "2.1.1"
        },
        "compile": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Authorization.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Microsoft.AspNetCore.Authorization.dll": {}
        }
      },
      "Microsoft.AspNetCore.Authorization.Policy/2.1.1": {
        "type": "package",
        "dependencies": {
          "Microsoft.AspNetCore.Authentication.Abstractions": "2.1.1",
          "Microsoft.AspNetCore.Authorization": "2.1.1"
        },
        