## Môi trường phát triển
- Dotnet core: 5.0
- SQLServer: 2019 Developer
- Editor: Rider 2021.1.4

1. Script compile scss
```bash
$ sass -w Styles:wwwroot/css/
```

2. Script hot reload blazor
```bash
$ dotnet watch run
```

## Môi trường triển khai
- Aws Amplify
1. Cấu hình
```yml
version: 1
applications:
  - frontend:
      phases:
        preBuild:
          commands:
            - curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
            - chmod +x *.sh
            - ./dotnet-install.sh -c 5.0 -InstallDir ./dotnet5
            - ./dotnet5/dotnet --version
        build:
          commands:
            - ./dotnet5/dotnet publish -c Release -o release
      artifacts:
        baseDirectory: /release/wwwroot
        files:
          - '**/*'
      cache:
        paths: []
    appRoot: src/AioCore.Hosted/AioCore.Web
```
2. Rewrites and redirects
- Source address
```text
</^[^.]+$|\.(?!(css|gif|ico|jpg|js|png|txt|svg|woff|ttf|map|json|br|gz|html|md|eot|otf|dll|blat|wasm|dat)$)([^.]+$)/>
```
- Target address
```text
/index.html
```
- Type
```text
200 (Rewrite)
```
