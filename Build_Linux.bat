dotnet publish --configuration Release  --self-contained true ".\FolderAndFileCreator\FolderAndFileCreator.csproj" -r linux-x64 -p:PublishSingleFile=true --output ".\Build\Linux64\FolderAndFileCreator"