using Sitecore.Diagnostics.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Sitecore.ConfigBuilder.Tool
{
  public static class ConfigBuilderProxy
  {
    const string linkPrefixContent = "[link]";
    const string linkExtension = ".link";
    const int MAX_PATH_LENGTH = 260;

    private static string GetInnerLinkFilePath(string linkFilePath)
    {
      if (!string.IsNullOrWhiteSpace(linkFilePath) && linkFilePath.EndsWith(linkExtension))
      {
        if (linkFilePath.Length > MAX_PATH_LENGTH)
        {
          throw new PathTooLongException($"Path too long -> {linkFilePath}");
        }
        if (File.Exists(linkFilePath))
        {
          using (var sr = new StreamReader(linkFilePath))
          {
            string line = sr.ReadLine();
            line = HttpUtility.UrlDecode(line).Trim();
            if (line.StartsWith(linkPrefixContent))
            {
              line = line.Split(new string[] { linkPrefixContent }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
            }
            string currentFileFolder = Path.GetDirectoryName(linkFilePath);
            line = Path.Combine(currentFileFolder, line);
            line = Path.GetFullPath(line);
            return line;
          }
        }
      }
      return null;
    }

    private static void ConvertLinkToRealFile(IDictionary<string, string> linksList)
    {
      while (true)
      {
        var links = linksList.Where(el => el.Value.EndsWith(linkExtension));
        if (!links.Any())
        {
          break;
        }
        var tempDict = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> link in links)
        {
          string innerFilePath = GetInnerLinkFilePath(link.Value);
          tempDict.Add(link.Key, innerFilePath);
        }
        foreach (var pair in tempDict)
        {
          linksList[pair.Key] = pair.Value;
        }
      }
    }

    private static IList<string> CreateRealFilesFromLinks(IDictionary<string, string> linksList)
    {
      var createdFiles = new List<string>();
      foreach (var pair in linksList)
      {
        if (!string.IsNullOrWhiteSpace(pair.Value))
        {
          string destFolder = Path.GetDirectoryName(pair.Key);
          string fileName = Path.GetFileName(pair.Value);
          string destFullPath = Path.Combine(destFolder, fileName);
          File.Copy(pair.Value, destFullPath);
          createdFiles.Add(destFullPath);
        }
      }
      return createdFiles;
    }

    private static IList<string> CreateRealConfigs(string webConfigFilePath)
    {
      string rootDirectory = Path.GetDirectoryName(webConfigFilePath);
      var linkFiles = Directory.GetFiles(rootDirectory, "*" + linkExtension, SearchOption.AllDirectories);
      var linksList = linkFiles.ToDictionary(key => key);
      ConvertLinkToRealFile(linksList);
      return CreateRealFilesFromLinks(linksList);
    }

    private static void RemoveRealConfigs(IList<string> createdFiles)
    {
      if (createdFiles != null)
      {
        foreach (string filePath in createdFiles)
        {
          if (File.Exists(filePath))
          {
            File.Delete(filePath);
          }
        }
      }
    }

    public static XmlDocument Build(string webConfigFilePath, bool buildWebConfigResult, bool normalizeOutput)
    {
      IList<string> createdFiles = CreateRealConfigs(webConfigFilePath);
      XmlDocument result = Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigFilePath, buildWebConfigResult, normalizeOutput);
      RemoveRealConfigs(createdFiles);
      return result;
    }

    public static XmlDocument Build(string webConfigFilePath, bool buildWebConfigResult, bool normalizeOutput, IFileSystem fileSystem)
    {
      IList<string> createdFiles = CreateRealConfigs(webConfigFilePath);
      XmlDocument result = Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigFilePath, buildWebConfigResult, normalizeOutput, fileSystem);
      RemoveRealConfigs(createdFiles);
      return result;
    }

    public static void Build(string webConfigFilePath, string outputFilePath, bool buildWebConfigResult, bool normalizeOutput)
    {
      IList<string> createdFiles = CreateRealConfigs(webConfigFilePath);
      Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigFilePath, outputFilePath, buildWebConfigResult, normalizeOutput);
      RemoveRealConfigs(createdFiles);
    }

    public static void Build(string webConfigFilePath, string outputFilePath, bool buildWebConfigResult, bool normalizeOutput, IFileSystem fileSystem)
    {
      IList<string> createdFiles = CreateRealConfigs(webConfigFilePath);
      Sitecore.Diagnostics.ConfigBuilder.ConfigBuilder.Build(webConfigFilePath, outputFilePath, buildWebConfigResult, normalizeOutput, fileSystem);
      RemoveRealConfigs(createdFiles);
    }
  }
}
