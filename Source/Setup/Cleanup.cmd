@echo off
echo.
echo ======================================================
echo Uninstall Visual Studio Code Snippets for the lab
echo ======================================================
echo.

DEL "%USERPROFILE%\Documents\Visual Studio 2010\Code Snippets\Visual C#\My Code Snippets\ServiceRemotingWithServiceBus*.snippet"
DEL "%USERPROFILE%\Documents\Visual Studio 2010\Code Snippets\Visual Basic\My Code Snippets\ServiceRemotingWithServiceBus*.snippet"
DEL "%USERPROFILE%\Documents\Visual Studio 2010\Code Snippets\XML\My Xml Snippets\ServiceRemotingWithServiceBus*.snippet"

echo Lab Code Snippets have been removed!