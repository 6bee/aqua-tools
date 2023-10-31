@echo off
for /d /r . %%d in (artifacts,bin,obj,.vs) do @if exist "%%d" rd /s /q "%%d"
for    /r . %%f in (*.bak,*.user,*.suo)    do @if exist "%%f" del "%%f"