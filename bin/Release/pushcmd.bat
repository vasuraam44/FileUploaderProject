@echo off
Set arg1=%1
echo %arg1%
echo Don't Close Console.
echo Please wait file uploading started..
adb push %arg1% /sdcard 
start 
pause