REM pyinstaller -F --add-data "templates;templates" --add-data "static;static" battery_diagnosisSrv.py
pyinstaller -F --icon=serverbattery.ico battery_diagnosisSrv.py
