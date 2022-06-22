@echo off
Set arg1=%1
title ADB File Upload
echo Don't Close Console.
echo Your Selected file %arg1% 
echo Please wait Started Uploading...
adb push %arg1% /sdcard 
start
pause
