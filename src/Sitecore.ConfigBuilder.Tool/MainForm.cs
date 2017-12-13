namespace Sitecore.ConfigBuilder.Tool
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.IO.Compression;
  using System.Linq;
  using System.Net;
  using System.Reflection;
  using System.Runtime.Remoting.Messaging;
  using System.Windows.Forms;
  using System.Xml.Linq;
  using JetBrains.Annotations;
  using Sitecore.Diagnostics.Base;
  using Sitecore.Diagnostics.ConfigBuilder;
  using Sitecore.Diagnostics.InfoService.Client;
  using Sitecore.Diagnostics.InfoService.Client.Model;

  internal partial class MainForm : Form
  {
    #region Fields and Constants

    private const string WebConfigOpenFilter = "*.config|*.config|Any file|*";

    private const string SaveFilter = "*.xml|*.xml|Any file|*";

    private const string ShowconfigFileName = "showconfig.xml";

    private const string WebConfigResultFileName = "web.config.result.xml";

    bool NeverAskMeAboutContextMenu = false;

    [NotNull]
    private static readonly string AppDataFolderPath = Environment.ExpandEnvironmentVariables("%APPDATA%\\Sitecore\\Sitecore.ConfigBuilder\\");

    [NotNull]
    private static readonly string SettingsFilePath = Path.Combine(AppDataFolderPath, "Settings.txt");

    #endregion

    public MainForm()
    {
      this.InitializeComponent();
    }

    #region Do Work

    private void SaveButton_Click([CanBeNull] object sender, [CanBeNull] EventArgs e)
    {
      this.SaveButton.Enabled = false;
      Save();
      UpdateSaveButton();
      this.ErrorLabel.Text = @"Config-files are processed.";
    }

    private void Save()
    {
      try
      {
        var webConfigPath = this.FilePathTextbox.Text.Trim(" \"".ToCharArray());
        var buildWebConfigResult = this.BuildWebConfigResult.Checked;
        var normalizeOutput = this.NormalizeOutput.Checked;
        var requireDefaultConfiguration = this.RequireDefaultConfiguration.Checked;
        var selectedItem = (ComboboxItem)this.SitecoreVersionComboBox.SelectedItem;
        var releaseInfo = selectedItem.Value;

        var outputShowConfigFile = this.GetShowConfigFilePath(ShowconfigFileName);

        if (string.IsNullOrEmpty(outputShowConfigFile))
        {
          return;
        }

        var outputWebConfigFile = string.Empty;
        if (buildWebConfigResult)
        {
          outputWebConfigFile = this.GetShowConfigFilePath(WebConfigResultFileName);
          Assert.IsNotNull(outputWebConfigFile, "outputWebConfigFile");
        }

        Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, false, false).Save(outputShowConfigFile);
        if (normalizeOutput)
        {
          Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, false, true).Save(GetNormalizedPath(outputShowConfigFile));
        }

        if (buildWebConfigResult)
        {
          Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, true, false).Save(outputWebConfigFile);
          if (normalizeOutput)
          {
            Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, true, true).Save(GetNormalizedPath(outputWebConfigFile));
          }
        }

        if (requireDefaultConfiguration)
        {


          var websiteFolder = Path.GetDirectoryName(webConfigPath);
          if (string.Equals(websiteFolder, Path.GetDirectoryName(outputShowConfigFile)))
          {
            websiteFolder += " " + releaseInfo.Version.MajorMinorUpdate;
            webConfigPath = Path.Combine(websiteFolder, Path.GetFileName(webConfigPath));
            outputShowConfigFile = Path.Combine(websiteFolder, Path.GetFileName(outputShowConfigFile));

            Directory.Delete(websiteFolder, true);

            {
              var filesZipPath = Path.GetTempFileName();
              new WebClient()
                .DownloadFile(releaseInfo.DefaultDistribution.Defaults.Configs.FilesUrl, filesZipPath);

              var tempFolder = Path.GetTempFileName();
              File.Delete(tempFolder);
              Directory.CreateDirectory(tempFolder);

              ZipFile.ExtractToDirectory(filesZipPath, tempFolder);

              tempFolder =
                (
                  Directory.GetDirectories(tempFolder, "Website", SearchOption.AllDirectories).First());
              Directory.Move(tempFolder, websiteFolder);

              var outputWebConfigFile1 = string.Empty;
              if (buildWebConfigResult)
              {
                outputWebConfigFile1 = Path.Combine(websiteFolder, "web.config.result.xml");
                Assert.IsNotNull(outputWebConfigFile1, "outputWebConfigFile");
              }

              Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, false, false).Save(outputShowConfigFile);
              if (normalizeOutput)
              {
                Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, false, true).Save(GetNormalizedPath(outputShowConfigFile));
              }

              if (buildWebConfigResult)
              {
                Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, true, false).Save(outputWebConfigFile1);
                if (normalizeOutput)
                {
                  Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigPath, true, true).Save(GetNormalizedPath(outputWebConfigFile1));
                }
              }
            }
          }
          else
          {
            try
            {
              var release = releaseInfo.Version.MajorMinorUpdate;
              var defaultShowConfig = outputShowConfigFile + "." + release + ".xml";
              var defaults = releaseInfo.DefaultDistribution.Defaults;
              Assert.IsNotNull(defaults, $"Defaults are not available for {release}");

              defaults.Configs.ShowConfig.Save(defaultShowConfig);
              var normalizer = new Normalizer();
              normalizer.Normalize(defaultShowConfig, GetNormalizedPath(defaultShowConfig));
              if (buildWebConfigResult)
              {
                var defaultWebConfigResult = outputWebConfigFile + "." + release + ".xml";
                defaults.Configs.Configuration.Save(defaultWebConfigResult);
                normalizer.Normalize(defaultWebConfigResult, GetNormalizedPath(defaultWebConfigResult));
              }
            }
            catch (Exception ex)
            {
              // Log.Error()
            }
          }
        }

        if (this.OpenFolder.Checked && File.Exists(outputWebConfigFile))
        {
          string argument = @"/select, """ + outputWebConfigFile + @"""";
          Process.Start("explorer.exe", argument);
        }

        if (this.CloseWhenDone.Checked)
        {
          this.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("The action failed with exception. " + ex.Message + Environment.NewLine + "Find details in the ConfigBuilder.ConfigBuilder.dll.log file", "Sitecore ConfigBuilder");
        File.AppendAllText("ConfigBuilder.ConfigBuilder.dll.log", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " ERROR " + ex.GetType().FullName + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Stack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine);
      }
    }

    private static string GetNormalizedPath(string defaultWebConfigResult)
    {
      return Path.Combine(Path.GetDirectoryName(defaultWebConfigResult), "norm." + Path.GetFileName(defaultWebConfigResult));
    }


    [CanBeNull]
    private string GetShowConfigFilePath([NotNull] string fileName, [CanBeNull] string webConfigFilePath = null)
    {
      Assert.ArgumentNotNull(fileName, "fileName");

      if (!this.NoDestinationPrompt.Checked)
      {
        var fileDialog = new SaveFileDialog
        {
          FileName = fileName,
          Filter = SaveFilter
        };

        if (fileDialog.ShowDialog(this) != DialogResult.OK)
        {
          return null;
        }

        var result = fileDialog.FileName;
        Assert.IsNotNull(result, "fileName");

        return result;
      }

      var directoryName = Path.GetDirectoryName(webConfigFilePath ?? this.FilePathTextbox.Text.Trim(" \"".ToCharArray()));
      Assert.IsNotNull(directoryName, "directoryName");

      return Path.Combine(directoryName, fileName);
    }

    #endregion

    #region Drag'n'Drop

    private void MainForm_DragEnter([CanBeNull] object sender, [NotNull] DragEventArgs e)
    {
      Assert.ArgumentNotNull(e, "e");

      var data = e.Data.GetData(DataFormats.FileDrop);
      if (data == null)
      {
        return;
      }

      var filePaths = data as string[];
      if (filePaths == null)
      {
        return;
      }

      var filePath = filePaths[0];
      if (filePath.Length < 1)
      {
        return;
      }

      e.Effect = DragDropEffects.Link;
    }

    private void MainForm_DragDrop([CanBeNull] object sender, [NotNull] DragEventArgs e)
    {
      Assert.ArgumentNotNull(e, "e");

      var data = e.Data.GetData(DataFormats.FileDrop);
      if (data == null)
      {
        return;
      }

      var filePaths = data as string[];
      if (filePaths == null)
      {
        return;
      }

      if (filePaths.Length == 1)
      {
        this.FilePathTextbox.Text = filePaths[0];
        this.UpdateSaveButton();
      }
    }

    #endregion

    #region UI Logic

    private void MainForm_Load([CanBeNull] object sender, [CanBeNull] EventArgs e)
    {
      try
      {
        this.ParseCommandLine();
        this.ReadSettings();
        UpdateMenuContextButton();
        new ToDoHandler(() =>
        {
          try
          {
            var vList = new List<IRelease>();
            foreach (var vv in new ServiceClient().GetVersions("Sitecore CMS").ToArray())
            {
              vList.AddRange(vv.Releases.Values);
            }

            return vList;
          }
          catch (Exception ex)
          {
            MessageBox.Show($"An issue occurred while updating available versions combobox. \r\nException: {ex.GetType().FullName}\r\nMessage: {ex.Message}");

            return new IRelease[0];
          }
        }).BeginInvoke(PopulateVersionsComboBox, null);
        this.Text = string.Format(this.Text ?? string.Empty, GetVersion());
      }
      catch (Exception ex)
      {
        MessageBox.Show("The form load failed with exception. " + ex.Message + Environment.NewLine + Environment.NewLine + "Find details in log file");
        File.AppendAllText("ConfigBuilder.Tool.exe.log", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " ERROR " + ex.GetType().FullName + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Stack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine);
      }
    }

    /// <summary>
    /// Ask a user about registry with answers:
    /// Yes - add to context menu.
    /// No - never add it to context menu.
    /// Cancel - skip for now.
    /// </summary>
    /// <returns>Return an answer from the user from MessageBox.Show</returns>
    public static System.Windows.Forms.DialogResult AskUserToUpdateRegistry(bool NoWriteRights)
    {
      var msg1 = "Click Yes if you would you like to embed a menu-item into Windows Explorer context menu for all \"web.config\" files in the system?";
      var msg2 = "If you want to enable this feature click Yes and re-run the application with Administrator privilegies.";
      var msg3 = "Click Cancel if you want to skip it for now or No if you don't want to see this message any longer.";
      var result = MessageBox.Show(
        msg1 + Environment.NewLine + Environment.NewLine + (NoWriteRights ? (msg2 + Environment.NewLine + Environment.NewLine) : string.Empty) + msg3,
        "ConfigBuilder settings",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      return result;
    }

    private void UpdateMenuContextButton(bool reset = false)
    {
      WinAPI.UpdateMenuContextButton(reset);
    }

    private void PopulateVersionsComboBox(IAsyncResult asyncRes)
    {
      try
      {
        var result1 = (AsyncResult)asyncRes;
        var result = ((ToDoHandler)result1.AsyncDelegate).EndInvoke(asyncRes);
        Action method = delegate
        {
          if (result != null)
          {
            var items = result.Select(x => new ComboboxItem($"{x.Version.MajorMinor} ({x.Label})", x)).ToArray<object>();
            this.SitecoreVersionComboBox.Items.AddRange(items);
            if (this.SitecoreVersionComboBox.Items.Count > 0)
            {
              this.SitecoreVersionComboBox.SelectedIndex = 0;
              if (this.FilePathTextbox.Text.Trim(" \"".ToCharArray()).Length > 0)
              {
                UpdateSaveButton();
              }
            }
          }
        };
        if (this.SitecoreVersionComboBox.InvokeRequired)
        {
          this.Invoke(method);
        }
        else
        {
          method();
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show($"An issue occurred while updating available versions combobox. \r\nException: {ex.GetType().FullName}\r\nMessage: {ex.Message}");
      }
    }

    private class ComboboxItem
    {
      [NotNull]
      public string Key { get; }

      [NotNull]
      public IRelease Value { get; }

      public ComboboxItem([NotNull] string key, [NotNull] IRelease value)
      {
        Key = key;
        Value = value;
      }

      public override string ToString() => Key;
    }

    private delegate IEnumerable<IRelease> ToDoHandler();

    [NotNull]
    private string GetVersion()
    {
      var assembly = Assembly.GetExecutingAssembly();
      var type = typeof(AssemblyFileVersionAttribute);
      var versionAttribute = assembly.GetCustomAttributes(type, true);
      if (versionAttribute.Length == 0)
      {
        return string.Empty;
      }

      var version = versionAttribute[0] as AssemblyFileVersionAttribute;
      return version != null ? version.Version ?? string.Empty : string.Empty;
    }

    private void ParseCommandLine()
    {
      var args = Environment.GetCommandLineArgs();
      if (args.Length == 2)
      {
        this.FilePathTextbox.Text = args[1];
        this.UpdateSaveButton();
      }
    }

    private void ReadSettings()
    {
      if (!File.Exists(SettingsFilePath))
      {
        return;
      }

      try
      {
        var boolParse = new Func<string[], int, bool>((p0, p1) =>
        {
          try
          {
            return bool.Parse(p0[p1]);
          }
          catch (Exception)
          {
            return false;
          }
        });

        var settings = File.ReadAllText(SettingsFilePath).Split('|');

        this.OpenFolder.Checked = boolParse(settings, 0);
        this.CloseWhenDone.Checked = boolParse(settings, 1);
        this.NormalizeOutput.Checked = boolParse(settings, 2);
        this.NoDestinationPrompt.Checked = boolParse(settings, 3);
        this.BuildShowConfig.Checked = boolParse(settings, 4);
        this.BuildWebConfigResult.Checked = boolParse(settings, 5);
        this.RequireDefaultConfiguration.Checked = boolParse(settings, 6);
        throw new NotImplementedException("NeverAskMeAboutContextMenu");
        //this.NeverAskMeAboutContextMenu = boolParse(settings, 7);
      }
      catch (Exception)
      {
        if (File.Exists(SettingsFilePath))
        {
          File.Delete(SettingsFilePath);
        }
      }
    }

    private void UpdateSaveButton()
    {
      var webConfigFilePath = this.FilePathTextbox.Text.Trim(" \"".ToCharArray());
      if (string.IsNullOrEmpty(webConfigFilePath))
      {
        this.SaveButton.Enabled = false;
        this.ErrorLabel.Text = @"The web.config file isn't chosen";
        return;
      }

      if (!File.Exists(webConfigFilePath))
      {
        this.SaveButton.Enabled = false;
        this.ErrorLabel.Text = @"The web.config file doesn't exist";
        return;
      }

      var websiteFolderPath = Path.GetDirectoryName(webConfigFilePath);
      var appConfigPath = Path.Combine(websiteFolderPath, "App_Config");
      if (!Directory.Exists(appConfigPath))
      {
        this.SaveButton.Enabled = false;
        this.ErrorLabel.Text = string.Format(@"The ""{0}"" folder doesn't exist", appConfigPath);
        return;
      }

      var version = TryDetectSitecoreVersion(websiteFolderPath);
      if (!string.IsNullOrEmpty(version))
      {
        foreach (var item in this.SitecoreVersionComboBox.Items)
        {
          if (item == null || !item.ToString().StartsWith(version))
          {
            continue;
          }

          this.SitecoreVersionComboBox.SelectedItem = item;
          break;
        }
      }

      this.SaveButton.Enabled = true;
      this.ErrorLabel.Text = @"Ready";
    }

    [CanBeNull]
    private string TryDetectSitecoreVersion([NotNull] string websiteFolderPath)
    {
      Assert.ArgumentNotNull(websiteFolderPath, "websiteFolderPath");

      try
      {
        var filePath = Path.Combine(websiteFolderPath, "sitecore.version.xml");
        if (!File.Exists(filePath))
        {
          var parentFolderPath = Path.GetDirectoryName(websiteFolderPath);
          Assert.IsNotNull(parentFolderPath, "parentFolderPath");

          filePath = Path.Combine(parentFolderPath, "sitecore.version.xml");
          if (!File.Exists(filePath))
          {
            var shellFolderPath = Path.Combine(websiteFolderPath, "sitecore\\shell");
            Assert.IsNotNull(shellFolderPath, "shellFolderPath");

            filePath = Path.Combine(shellFolderPath, "sitecore.version.xml");
            if (!File.Exists(filePath))
            {
              return null;
            }
          }
        }


        var doc = XDocument.Load(filePath);
        Assert.IsNotNull(doc, "doc");

        var major = doc.Descendants("major").FirstOrDefault().Value;
        var minor = doc.Descendants("minor").FirstOrDefault().Value;
        var build = doc.Descendants("build").FirstOrDefault().Value;
        var revision = doc.Descendants("revision").FirstOrDefault().Value.Substring(0, 6);
        var version = major + "." + minor + (string.IsNullOrEmpty(build) ? "" : ("." + build)) + " rev. " + revision;

        return version;
      }
      catch
      {
      }

      return null;
    }

    private void SaveSettings([CanBeNull] object sender, [CanBeNull] EventArgs e)
    {
      if (!Directory.Exists(AppDataFolderPath))
      {
        Directory.CreateDirectory(AppDataFolderPath);
      }

      var file = Path.Combine(AppDataFolderPath, "Settings.txt");
      var contents = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|",
        OpenFolder.Checked,
        CloseWhenDone.Checked,
        NormalizeOutput.Checked,
        NoDestinationPrompt.Checked,
        BuildShowConfig.Checked,
        BuildWebConfigResult.Checked,
        RequireDefaultConfiguration.Checked,
        NeverAskMeAboutContextMenu);
      File.WriteAllText(file, contents);
    }

    private void BrowseButton_Click([CanBeNull] object sender, [CanBeNull] EventArgs e)
    {
      try
      {
        var fileDialog = new OpenFileDialog { Filter = WebConfigOpenFilter };
        if (fileDialog.ShowDialog(this) == DialogResult.OK)
        {
          this.FilePathTextbox.Text = fileDialog.FileName;
        }

        this.UpdateSaveButton();
      }
      catch (Exception ex)
      {
        MessageBox.Show("The action failed with exception. " + ex.Message + Environment.NewLine + Environment.NewLine + "Find details in log file");
        File.AppendAllText("ConfigBuilder.Tool.exe.log", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " ERROR " + ex.GetType().FullName + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Stack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine);
      }
    }

    private void FilePathTextbox_TextChanged([CanBeNull] object sender, [CanBeNull] EventArgs e)
    {
      try
      {
        this.UpdateSaveButton();
      }
      catch (Exception ex)
      {
        MessageBox.Show("The action failed with exception. " + ex.Message + Environment.NewLine + Environment.NewLine + "Find details in log file");
        File.AppendAllText("ConfigBuilder.Tool.exe.log", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " ERROR " + ex.GetType().FullName + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Stack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine);
      }
    }

    #endregion

    private void NormalizeLink_Click([CanBeNull] object sender, [CanBeNull] LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        var fileDialog = new OpenFileDialog { Filter = "Showconfig.xml file|*" };
        if (fileDialog.ShowDialog(this) != DialogResult.OK)
        {
          return;
        }

        var showconfigPath = fileDialog.FileName;
        if (string.IsNullOrEmpty(showconfigPath))
        {
          return;
        }

        var outputFile = this.GetShowConfigFilePath(ShowconfigFileName, showconfigPath);
        if (string.IsNullOrEmpty(outputFile))
        {
          return;
        }

        new Normalizer().Normalize(showconfigPath, GetNormalizedPath(showconfigPath));

        if (this.OpenFolder.Checked && File.Exists(outputFile))
        {
          var argument = @"/select, """ + outputFile + @"""";
          Process.Start("explorer.exe", argument);
        }

        if (this.CloseWhenDone.Checked)
        {
          this.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("The action failed with exception. " + ex.Message + Environment.NewLine + Environment.NewLine + "Find details in log file");
        File.AppendAllText("ConfigBuilder.Tool.exe.log", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " ERROR " + ex.GetType().FullName + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Stack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine);
      }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveSettings(sender, e);
    }

    private void toolStripStatusLabelResetRegistry_Click(object sender, EventArgs e)
    {
      UpdateMenuContextButton(true);
    }
  }
}
