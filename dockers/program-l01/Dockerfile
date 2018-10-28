FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY *.sln ./
COPY . ./

WORKDIR /app/src/ProgramL02
RUN dotnet restore

RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/dotnet:runtime
WORKDIR /app
COPY --from=build-env /app/src/ProgramL02/out .

ENTRYPOINT ["dotnet", "ESystems.Mentoring.ProgramL02.dll"]