using System;
using System.Windows.Forms;

namespace Sitecore.ConfigBuilder.Tool
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      WinAPI.UpdateMenuContextButton();
      switch (WinAPI.TestCommandLine())
      {
        case WinAPI.StartOption.StartApp:
          Application.Run(new MainForm());
          break;
        case WinAPI.StartOption.ConfigDisable:
          WinAPI.FileRenameDisable();
          break;
        case WinAPI.StartOption.ConfigEnable:
          WinAPI.FileRenameEnable();
          break;
      }
    }
  }
}
