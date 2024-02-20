
@echo off


REM    This script must be placed in the chrono_csharp directory of your Unity scripts, but not inside 'core' or any other subdirectory.
REM    This script defines a function :RemoveDuplicates that accepts a directory path as an argument. It is then called twice: once with the VEHICLE_DIR and once with the POSTPROCESS_DIR. The function iterates over .cs files in the given directory and deletes them if they are also present in the CORE_DIR

REM Get the full path of the directory where the batch file is located
set "ROOT_DIR=%~dp0"
REM Remove the trailing backslash for consistency in path definitions
set "ROOT_DIR=%ROOT_DIR:~0,-1%"

set "ROOT_DIR=%~dp0"
set "CORE_DIR=%ROOT_DIR%core"

echo Removing duplicate .cs files from vehicle and postprocess modules

REM Loop over vehicle directory
set "TARGET_DIR=%ROOT_DIR%vehicle"
echo Checking duplicates in "%TARGET_DIR%" against "%CORE_DIR%"
for %%f in ("%TARGET_DIR%\*.cs") do (
    if exist "%CORE_DIR%\%%~nxf" (
        echo Deleting duplicate: "%%f"
        del "%%f"
    )
)

REM Add space between print outs of directory for readability
echo.
echo.

REM Loop over postprocess directory
set "TARGET_DIR=%ROOT_DIR%postprocess"
echo Checking duplicates in "%TARGET_DIR%" against "%CORE_DIR%"
for %%f in ("%TARGET_DIR%\*.cs") do (
    if exist "%CORE_DIR%\%%~nxf" (
        echo Deleting duplicate: "%%f"
        del "%%f"
    )
)


REM Add space between print outs of directory for readability
echo.
echo.

REM Loop over irrlicht directory
set "TARGET_DIR=%ROOT_DIR%irrlicht"
echo Checking duplicates in "%TARGET_DIR%" against "%CORE_DIR%"
for %%f in ("%TARGET_DIR%\*.cs") do (
    if exist "%CORE_DIR%\%%~nxf" (
        echo Deleting duplicate: "%%f"
        del "%%f"
    )
)

echo Done removing duplicates.
