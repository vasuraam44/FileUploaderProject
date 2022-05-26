@echo off
Set arg1=%1
title File Uploading started
echo Don't Close Console.
echo Please wait file uploading started..
adb push %arg1% /sdcard
start
pause