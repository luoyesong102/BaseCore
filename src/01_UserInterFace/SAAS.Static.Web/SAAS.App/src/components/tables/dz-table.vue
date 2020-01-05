{
  "version": 3,
  "targets": {
    ".NETStandard,Version=v2.0": {
      "Microsoft.CodeAnalysis.Analyzers/2.6.2-beta2": {
        "type": "package"
      },
      "Microsoft.CodeAnalysis.Common/3.1.0": {
        "type": "package",
        "dependencies": {
          "Microsoft.CodeAnalysis.Analyzers": "2.6.2-beta2",
          "System.Collections.Immutable": "1.5.0",
          "System.Memory": "4.5.2",
          "System.Reflection.Metadata": "1.6.0",
          "System.Runtime.CompilerServices.Unsafe": "4.5.2",
          "System.Text.Encoding.CodePages": "4.5.1",
          "System.Threading.Tasks.Extensions": "4.5.2"
        },
        "compile": {
          "lib/netstandard2.0/Microsoft.CodeAnalysis.dll": {}
        },
        "runtime": {
          "lib/netstandard2.0/Microsoft.CodeAnalysis.dll": {}
        },
        "resource": {
          "lib/netstandard2.0/cs/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "cs"
          },
          "lib/netstandard2.0/de/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "de"
          },
          "lib/netstandard2.0/es/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "es"
          },
          "lib/netstandard2.0/fr/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "fr"
          },
          "lib/netstandard2.0/it/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "it"
          },
          "lib/netstandard2.0/ja/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "ja"
          },
          "lib/netstandard2.0/ko/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "ko"
          },
          "lib/netstandard2.0/pl/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "pl"
          },
          "lib/netstandard2.0/pt-BR/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "pt-BR"
          },
          "lib/netstandard2.0/ru/Microsoft.CodeAnalysis.resources.dll": {
            "locale": "ru"
          },
          "lib/netstandard2.0/tr/Microsoft.CodeAnalysis.r