
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

  Rework into 
  http://www.codeproject.com/Articles/512956/NET-Shell-Extensions-Shell-Context-Menus
  
    We should have an ability to apply these to big number of files.
  */
  public class WinAPI
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
          if (reset)
          {
            var shellKey = classesRoot.CreateSubKey(@"*\shell");
            foreach (string every1 in shellKey.GetSubKeyNames().Where(q => q.StartsWith(@"Sitecore.ConfigBuilder")))
            {
              try
              {
                shellKey.DeleteSubKeyTree(every1);
              }
              catch (Exception ex)
              {
                MessageBox.Show(ex.Message);
              }
            }
          }
          else
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

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigLink");
          key.SetValue("", "Open with Sitecore Config Builder");
          key.SetValue("Icon", ConfigBuilderExePath);
          key.SetValue("AppliesTo", "System.FileName:\"web.config*.link\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigLink\command");
          key.SetValue("", "\"" + ConfigBuilderExePath + "\" \"%1\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigDisable");
          key.SetValue("", "Disable configuration file");
          key.SetValue("Icon", ConfigBuilderExePath);
          key.SetValue("AppliesTo", "System.FileName:\"*.config\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigDisable\command");
          key.SetValue("", "\"" + ConfigBuilderExePath + "\" \"%1\" \"-disable\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigEnable1");
          key.SetValue("", "Enable configuration file");
          key.SetValue("Icon", ConfigBuilderExePath);
          key.SetValue("AppliesTo", "System.FileName:\"*.disabled\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigEnable1\command");
          key.SetValue("", "\"" + ConfigBuilderExePath + "\" \"%1\" \"-enable\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigEnable2");
          key.SetValue("", "Enable configuration file");
          key.SetValue("Icon", ConfigBuilderExePath);
          key.SetValue("AppliesTo", "System.FileName:\"*.example\"");

          key = classesRoot.CreateSubKey(@"*\shell\Sitecore.ConfigBuilder.ConfigEnable2\command");
          key.SetValue("", "\"" + ConfigBuilderExePath + "\" \"%1\" \"-enable\"");
        }
      }
      else if (result == System.Windows.Forms.DialogResult.No)
      {
        throw new NotImplementedException("NeverAskMeAboutContextMenu");
        //NeverAskMeAboutContextMenu = true;
      }
    }

    public enum StartOption
    {
      StartApp, ConfigDisable, ConfigEnable
    }
    public static StartOption TestCommandLine()
    {
      var args = Environment.GetCommandLineArgs();
      if (args.Length == 2)
      {
        return StartOption.StartApp;
      }
      if (args.Length == 3)
      {
        if (args[2].ToLower() == "-disable".ToLower())
        {
          return StartOption.ConfigDisable;
        }
        if (args[2].ToLower() == "-enable".ToLower())
        {
          return StartOption.ConfigEnable;
        }
      }
      return StartOption.StartApp;
    }

    public static bool FileRenameDisable()
    {
      var args = Environment.GetCommandLineArgs();
      string fullname = "";
      if (args.Length == 3 && args[2] == "-disable")
      {
        fullname = args[1];
      }
      else
      {
        return false;
      }
      int dot = fullname.LastIndexOf('.');
      var disableExt = ".disabled".ToLower();
      if (fullname.Substring(dot, fullname.Length - dot).ToLower() == disableExt.ToLower())
      {
        // done!!! cause it has the .disable at the end :P
        return true;
      }
      File.Move(fullname, fullname + disableExt);
      return true;
    }

    public static bool FileRenameEnable()
    {
      var args = Environment.GetCommandLineArgs();
      string fullname = "";
      if (args.Length == 3 && args[2] == "-enable")
      {
        fullname = args[1];
      }
      else
      {
        return false;
      }
      var disableExts = new string[] { ".disabled", ".example" };

      int dot = 0;
      string newFullName = fullname;
      string ext1 = "";
      do
      {
        dot = newFullName.LastIndexOf('.');
        if (dot == -1)
        {
          break;
        }
        else
        {
          ext1 = newFullName.Substring(dot, newFullName.Length - dot).ToLower();
          for (int i = 0; i < disableExts.Length && dot != -100500; ++i)
          {
            if (ext1 == disableExts[i].ToLower())
            {
              newFullName = fullname.Substring(0, dot);
              dot = -100500;
            }
          }
        }
      } while (dot == -100500);
      File.Move(fullname, newFullName);
      return true;
    }
  }
}