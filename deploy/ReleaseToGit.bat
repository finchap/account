pushd

set GITREPO_URL=%1
set RELEASE_ENVIRONMENTNAME=%2
set RELEASE_PATH=%3
set WORK_FOLDER=%4

:: Clone if not already done
if not exist %WORK_FOLDER% (
    git clone %GITREPO_URL% %WORK_FOLDER%
)
cd %WORK_FOLDER%

:: Checkout the branch correspondingto the release environment
git branch -a --list */%RELEASE_ENVIRONMENTNAME% | find /v /c "*" > output.tmp
SET /P BRANCH_EXSISTS= <output.tmp
del output.tmp

if %BRANCH_EXSISTS% == 0 (
    git checkout -f -b %RELEASE_ENVIRONMENTNAME%
    git push --set-upstream origin %RELEASE_ENVIRONMENTNAME%
) else (
    git pull origin %RELEASE_ENVIRONMENTNAME%
    git checkout %RELEASE_ENVIRONMENTNAME%
)
git config --global user.email "%COMPUTERNAME%@dachapde.visualstudio.com"
git config --global user.name "VSTS Build Agent (%COMPUTERNAME%)

:: Cleanup the git folder
for /f "tokens=*" %%i in ('dir /b') do (
    if not %%i == .git del /s /q /f %%i
)

:: Move the files
robocopy %RELEASE_PATH%\ . /e /move
echo %Release_ReleaseId% > ReleaseId.txt

:: Push everything where expected!
git add .
git commit -m "***NO_CI*** publishing release %Release_ReleaseId%"
git pull
git push
git status
popd
exit /b 0