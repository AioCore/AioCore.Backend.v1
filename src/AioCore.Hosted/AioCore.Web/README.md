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