@ECHO off

REG DELETE "hkcr\*\shell\Sitecore.ConfigBuilder" /f

ECHO.
ECHO Context menu handlers were deleted successfully.
PAUSE