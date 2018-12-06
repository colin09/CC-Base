:: dotnet core auto publish
@echo off
cls
echo ####################################################################################################################
echo #####
set gitPath=D:\repository\mine\CC-Base\
set rootPath=D:\repository\mine\CC-Base\C.B\StmWeb
set publishPath=E:\release\stm
set port=8090



CD /d %gitPath%
	git co core
	git pull origin core

CD /d %rootPath%
	dotnet restore
	dotnet build
	dotnet publish --output %publishPath%


for /f "tokens=1-5" %%i in ('netstat -ano^|findstr ":%port%"') do taskkill /pid %%m


CD /d %publishPath%
	start powershell.exe -Command "dotnet StmWeb.dll" -NoExit -WindowStyle "Minimized"