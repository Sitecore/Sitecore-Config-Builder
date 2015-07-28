@ECHO off

IF NOT EXIST "%CD%\Sitecore.ConfigBuilder.Tool.exe" ECHO File "%CD%\Sitecore.ConfigBuilder.Tool.exe" should exist. Cannot complete operation.
IF NOT EXIST "%CD%\Sitecore.ConfigBuilder.Tool.exe" PAUSE
IF NOT EXIST "%CD%\Sitecore.ConfigBuilder.Tool.exe" exit

REG ADD "hkcr\*\shell\Sitecore.ConfigBuilder" /ve /t REG_SZ /d "Open with Sitecore ConfigBuilder" /f
REG ADD "hkcr\*\shell\Sitecore.ConfigBuilder" /v Icon /t REG_SZ /d "%CD%\Sitecore.ConfigBuilder.Tool.exe" /f
REG ADD "hkcr\*\shell\Sitecore.ConfigBuilder" /v "AppliesTo" /t REG_SZ /d "System.FileName:\"web.config\"" /f
REG ADD "hkcr\*\shell\Sitecore.ConfigBuilder\command" /ve /t REG_SZ /d "\"%CD%\Sitecore.ConfigBuilder.Tool.exe\" \"%%1\"" /f

ECHO.
ECHO Context menu handlers were added successfully.
PAUSE 