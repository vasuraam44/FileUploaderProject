@echo off
Set arg1=%1
title File sharing started please wait dont close console..
echo File sending
adb push %arg1% /sdcard 
start 
pause