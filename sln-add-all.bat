@echo on

for /f "%%p" in ('dir /s /d /b *.csproj') do dotnet sln add "%%p"