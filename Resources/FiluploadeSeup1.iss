; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "FileUploader"
#define MyAppVersion "1.0"
#define MyAppPublisher "Evolute Systems"
#define MyAppURL "http://www.example.com/"
#define MyAppExeName "FileUploader.exe"
#define MyAppIcoName "fileUploader.ico"

#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt


[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{047291CD-09B7-4C0F-8D5D-9B5DBEADEC52}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\EvoluteSystems\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=D:\CHANGED SCRIPT
OutputBaseFilename=FileUploaderSetUp
SetupIconFile=fileUploader.ico
Compression=lzma
SolidCompression=yes
ChangesEnvironment=yes
                              
[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";

[Files]
Source: "C:\Users\user\source\EVOLUTE PROJECT\FileUploader\bin\Release\FileUploader.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\user\source\EVOLUTE PROJECT\FileUploader\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files


[Registry]
Root: HKCU; Subkey:"Environment"; ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}\Resources\platform-tools;"
Root: HKCU; Subkey:"software\MylistData"; ValueType: expandsz; ValueName: "Mylist"; ValueData: "192.168.210.253,192.168.210.251,192.168.43.54"

Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: "{app}\{#MyAppExeName}"; Flags: uninsdeletevalue uninsdeletekeyifempty
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey 
Root: HKA; Subkey: "Software\Classes\*\shell\{#MyAppName}"; ValueType:String; ValueName:"icon"; ValueData:"""{app}\{#MyAppExeName}"""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\*\shell\{#MyAppName}\command"; ValueType:String; ValueName:""; ValueData:"""{app}\{#MyAppExeName}"" ""%1"""; Flags: uninsdeletevalue uninsdeletekeyifempty;


[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
;Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}";IconFilename: "{app}\{#MyAppIcoName}"; Tasks: desktopicon

[Run]                                                                                                             
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}";Flags: postinstall nowait skipifsilent unchecked