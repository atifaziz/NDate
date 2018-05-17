@echo off
pushd "%~dp0"
call :main %*
popd
goto :EOF

:main
setlocal
set VERSION_SUFFIX=
if not "%~1"=="" set VERSION_SUFFIX=/p:VersionSuffix=%1
call msbuild /v:m /t:Pack /p:Configuration=Release       ^
                          /p:IncludeSymbols=true         ^
                          /p:IncludeSource=true          ^
                          %VERSION_SUFFIX%
goto :EOF
