@echo off

cd E:\WorkSpace\WorkingTimeRecorder

:Main
cls
echo.
echo Please select the operation to be performed and press enter
echo [1] Copy release from the compilation directory to the current directory
echo [2] Run the release in the compilation directory
echo [3] Copy the new release file to the shared directory
echo [e] Exit

set /p input=
if defined input set input=%input:"=%
if /i "%input%" == "1" (goto fun1)
if /i "%input%" == "2" (goto fun2)
if /i "%input%" == "3" (goto fun3)
if /i "%input%" == "e" (goto Exit)
else goto error

:fun1
copy WorkingTimeRecorder\bin\Release\WorkingTimeRecorder.exe WorkingTimeRecorder.exe
goto end

:fun2
start WorkingTimeRecorder\bin\Release\WorkingTimeRecorder.exe
goto end

:fun3
copy WorkingTimeRecorder.exe F:\_readonly_share\WorkingTimeRecorder\WorkingTimeRecorder.exe
goto end

:error
echo input error!

:end
PAUSE
goto Main