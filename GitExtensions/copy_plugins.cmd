@echo off
set config=%1
md Plugins\
md ComEmu\
echo Microsoft.TeamFoundation.WorkItemTracking.Client.DataStoreLoader.dll > exclude.txt
echo Microsoft.WITDataStore.dll >> exclude.txt

echo p0=%~p0
echo I="%%I\"

for /d %%I in ("%~p0..\Plugins\*", "%~p0..\Plugins\Statistics\*", "%~p0..\Plugins\BuildServerIntegration\*") do (
    if exist "%%I\bin\%config%\" (
        xcopy /y /r "%%I\bin\%config%\*.dll" "%~p0..\bin\%config%\Plugins\" /EXCLUDE:exclude.txt
        xcopy /y /r "%%I\bin\%config%\*.pdb" "%~p0..\bin\%config%\Plugins\"
    )
)