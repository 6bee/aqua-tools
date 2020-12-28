@echo off

call :prepare

echo.

if "%1" neq "" (
  for %%I in (%*) do call :pack "%%I"
) else (
  for /r "%~dp0" %%d in (*.nuspec) do call :pack "%%d"
)

echo.
echo done.
pause
goto :eof

:pack
echo pack %~1
"%~dp0.tools\NuGet.exe" pack "%~1" -OutputDirectory "%~dp0artifacts" >>"%~dp0pack.log"
set resultcode=%errorlevel%
if %resultcode% neq 0 (
  echo.
  pause
)
exit /b %resultcode% 

:prepare
echo.
echo clean
del /q "%~dp0pack.log" 2>nul

if not exist "%~dp0.tools\nuget.exe" (
  echo download nuget cmd tool
  md "%~dp0.tools\temp\"
  powershell -Command "Invoke-WebRequest -Uri 'https://www.nuget.org/api/v2/package/NuGet.CommandLine/' -OutFile '%~dp0.tools\temp\NuGet.CommandLine.nupkg.zip'"
  powershell -Command "Expand-Archive -Path '%~dp0.tools\temp\NuGet.CommandLine.nupkg.zip' -DestinationPath '%~dp0.tools\temp\'"
  move "%~dp0.tools\temp\tools\NuGet.exe" "%~dp0.tools\"
  rd /s /q "%~dp0.tools\temp\"
)
echo.
exit /b 0
