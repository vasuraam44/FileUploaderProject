@echo off
Set arg1=%1
title ADB File Uploading 
echo Don't Close Console.
echo Please wait file uploading started..
adb push %arg1% /sdcard
start
pause