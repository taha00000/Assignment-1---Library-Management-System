FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0-bookworm
USER root
RUN dotnet workload update
USER vscode
RUN dotnet tool install --global dotnet-ef
