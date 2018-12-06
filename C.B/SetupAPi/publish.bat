:: dotnet core auto publish
::@echo off
cls
echo ####################################################################################################################
echo #####
set rootPath=D:\repository\mine\CC-Base\C.B\StmWeb
set publishPath=E:\release\stm
set port=8090

CD /d %rootPath%
	dotnet restore
	dotnet build
	dotnet publish --output %publishPath%


for /f "tokens=1-5" %%i in ('netstat -ano^|findstr ":%port%"') do taskkill /pid %%m


CD /d %publishPath%
	start powershell.exe -Command "dotnet StmWeb.dll" -NoExit -WindowStyle "Minimized"