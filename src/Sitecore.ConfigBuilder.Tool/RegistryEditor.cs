
namespace Sitecore.ConfigBuilder.Tool
{
  using Microsoft.Win32;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Reflection;
  using System.Text;
  using System.Windows.Forms;
  /*
  MUIVerb ~ name for the element
  (default) ~ name for the element
  Icon ~ icon for the menu element (example: SHELL32.dll,69  ; example: c:q\1\i1.exe )

    
  Readme:
  https://paulkravchenko.wordpress.com/2010/11/03/add-cascading-menus-for-your-favorite-programs-in-windows-7-desktop-context-menu/
  http://www.cyberforum.ru/windows/thread617978.html

  Computer
    Hkey_Classes_Roots\*\shell\Sitecore.ConfigBuilder
          (default) = Open with Sitecore Config Builder
          AppliesTo = System.FileName:"web.config"
          Icon      = E:\Program Files (portable)\Sitecore ConfigBuilder\Sitecore.ConfigBuilder.Tool.exe
    Hkey_Classes_Roots\*\shell\Sitecore.ConfigBuilder\command
          (default) = "E:\Program Files (portable)\Sitecore ConfigBuilder\Sitecore.ConfigBuilder.Tool.exe" "%1"
    Hkey_Classes_Roots\*\shell\Sitecore.ConfigBuilder.ConfigDisable
          (default) = Rename into --> *.disable
          AppliesTo = System.FileName:"*.config"
          Icon      = 
    Hkey_Classes_Roots\*\shell\Sitecore.ConfigBuilder.ConfigDisable\command
          (default) = "E:\Program Files (portable)\Sitecore ConfigBuilder\Sitecore.ConfigBuilder.Tool.exe" "%1" "-disable"
    Hkey_Classes_Roots\*\shell\Sitecore.ConfigBuilder.ConfigEnable
          (default) = Remove *.disable extension
          AppliesTo = System.FileName:"*.disable"
          Icon      = 
    Hkey_Classes_Roots\*\shell\Sitecore.ConfigBuilder.ConfigEnable\command
          (default) = "E:\Program Files (portable)\Sitecore ConfigBuilder\Sitecore.ConfigBuilder.Tool.exe" "%1" "-enable"

    We should have an ability to apply these to big number of files.
  */
  public class RegistryEditor
  {
    public static void UpdateMenuContextButton(bool reset = false)
    {
      var classesRoot = Registry.ClassesRoot;
      if (classesRoot == null)
      {
        return;
      }
      //Getting PATH of the ConfigBuilder.exe application.
      var ConfigBuilderExePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sitecore.ConfigBuilder.Tool.exe");

      try
      {
        RegistryKey keyConfigBuilder = classesRoot.OpenSubKey(@"*\shell\Sitecore.ConfigBuilder", RegistryKeyPermissionCheck.ReadSubTree);
        if (keyConfigBuilder != null)
        {
          keyConfigBuilder.Close();
          if (!reset)
          {
            // No need to Reset registry
            return;
          }
        }
      }
      catch (Exception)
      {
        throw new System.Security.SecurityException(@"Don't have read access to the registry: hkcr\*\shell\Sitecore.ConfigBuilder");
      }
      bool NoWriteRights = false;
      try
      {
        classesRoot.CreateSubKey(@"*\shell\");
      }
      catch (Exception)
      {
        //I am not sure, whether we should catch any exception. It should be UnauthorizedAccessException
        NoWriteRights = true;
      }
      var result = MainForm.AskUserToUpdateRegistry(NoWriteRights);
      if (result == System.Windows.Forms.DialogResult.Yes)
      {
        if (NoWriteRights)
        {
          Application.Exit();
          return;
        }
        else
        {
          RegistryKey key = default(RegistryKey);
          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder");
          key.SetValue("", "Open with Sitecore Config Builder");
          key.SetValue("Icon", ConfigBuilderExePath);
          key.SetValue("AppliesTo", "System.FileName:\"web.config\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder\command");
          key.SetValue("", "\"" + ConfigBuilderExePath + "\" \"%1\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigDisable");
          key.SetValue("", "Rename into --> *.disable");
          key.SetValue("Icon", "SHELL32.dll,69");
          key.SetValue("AppliesTo", "System.FileName:\"*.config\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigDisable\command");
          key.SetValue("", "\"" + ConfigBuilderExePath + "\" \"%1\" \"-disable\"");


          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigEnable");
          key.SetValue("", "Remove *.disable extension");
          key.SetValue("Icon", "SHELL32.dll,69");
          key.SetValue("AppliesTo", "System.FileName:\"*.disable\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigEnable\command");
          key.SetValue("", "\"" + ConfigBuilderExePath + "\" \"%1\" \"-enable\"");

        }
      }
      else if (result == System.Windows.Forms.DialogResult.No)
      {
        throw new NotImplementedException("NeverAskMeAboutContextMenu");
        //NeverAskMeAboutContextMenu = true;
      }
    }
  }
}
