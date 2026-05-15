[Setup]
AppName=CarRentalApp
AppVersion=1.0
DefaultDirName={pf}\CarRentalApp
DefaultGroupName=CarRentalApp
OutputDir=Output
OutputBaseFilename=CarRentalSetup
Compression=lzma
SolidCompression=yes

[Files]
Source: "bin\Release\net9.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\CarRentalApp"; Filename: "{app}\CarRentalApp.exe"

[Run]
Filename: "{app}\CarRentalApp.exe"; Description: "Запустить приложение"; Flags: nowait postinstall skipifsilent