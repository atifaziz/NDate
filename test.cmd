@echo off
pushd "%~dp0"
call build && dotnet test NDate.Tests
popd
