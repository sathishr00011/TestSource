using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System.IO;
using System.Xml.Linq;
using System.Configuration;
using System.Reflection;


namespace ReleaseManifests
{
    class TfsCheckoutCheckin
    {
        private const string tfsServer = @"http://tfsapp.dotcom.tesco.org/tfs/";
        private const string tfsWorkspacePath = "$/InternationalDeployment";
        private static string wsVersionConfigPath = tfsWorkspacePath + "/Tools/ReleaseManifest/ReleaseManifests/VersionConfig/";
        public static string CheckOutFromTFS(string fileName, string versionKeyName, string releaseVersion)
        {
            using (TfsTeamProjectCollection pc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(tfsServer)))
            {
                if (pc != null)
                {
                    var tfsVersionConfigPath = wsVersionConfigPath + releaseVersion + "/";
                    GetLatestVersionConfig(tfsVersionConfigPath, fileName, pc);

                    WorkspaceInfo workspaceInfo = GetTfsWorkspaceInfo(pc, tfsVersionConfigPath);
                    if (null != workspaceInfo)
                    {
                        Workspace workspace = workspaceInfo.GetWorkspace(pc);
                        var tempPath = Path.GetTempPath().TrimEnd('\\'); //@"\Local\Temp"
                        var folderPath = workspace.Folders.Where(f => Path.GetFullPath(f.LocalItem).Equals(tempPath)).Select(folder => folder).First();
                        var filePath = folderPath.ServerItem;

                        var fullFilePath = filePath + "/" + fileName;
                        var localFilePath = Path.GetTempPath() + fileName;

                        XDocument xmlConfig = null;
                        var newVersion = string.Empty;

                        string[] filePaths = new string[] { fullFilePath };
                        workspace.PendEdit(filePaths, RecursionType.None, FileType.BinaryFileType, LockLevel.CheckOut);

                        xmlConfig = XDocument.Load(localFilePath);
                        var elementNodes = xmlConfig.Descendants("appSettings").FirstOrDefault().Descendants("add");

                        foreach (XElement element in elementNodes)
                        {
                            if (element.Attribute("key").Value == versionKeyName)
                            {
                                newVersion = ReleaseManifest.IncreaseVersionNumber(element.Attribute("value").Value);
                                element.Attribute("value").Value = newVersion;
                                break;
                            }
                        }
                        xmlConfig.Save(localFilePath);
                        return newVersion;
                    }
                }
                pc.Dispose();
            }
            return string.Empty;
        }

        public static void CheckInToTFS(string fileName, string releaseVersion)
        {
            using (TfsTeamProjectCollection pc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(tfsServer)))
            {
                if (pc != null)
                {
                    WorkspaceInfo workspaceInfo = GetTfsWorkspaceInfo(pc, wsVersionConfigPath + releaseVersion + "/"); //Workstation.Current.GetLocalWorkspaceInfo(fileName);
                    if (null != workspaceInfo)
                    {
                        Workspace workspace = workspaceInfo.GetWorkspace(pc);
                        PendingChange[] pendingChanges = workspace.GetPendingChanges().Where(f => f.FileName == fileName).ToArray();
                        workspace.CheckIn(pendingChanges, "Manifest Generation version Update by" + workspace.OwnerName);
                    }
                }
                pc.Dispose();
            }
        }

        public static void UndoChangesToTFS(string fileName, string releaseVersion)
        {
            using (TfsTeamProjectCollection pc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(tfsServer)))
            {
                if (pc != null)
                {
                    WorkspaceInfo workspaceInfo = GetTfsWorkspaceInfo(pc, wsVersionConfigPath + releaseVersion + "/"); //Workstation.Current.GetLocalWorkspaceInfo(fileName);
                    if (null != workspaceInfo)
                    {
                        Workspace workspace = workspaceInfo.GetWorkspace(pc);
                        PendingChange[] pendingChanges = workspace.GetPendingChanges().Where(f => f.FileName == fileName).ToArray();
                        if (pendingChanges != null && pendingChanges.Any())
                        {
                            workspace.Undo(pendingChanges);
                        }
                    }
                }
                pc.Dispose();
            }
        }

        public static WorkspaceInfo GetTfsWorkspaceInfo(TfsTeamProjectCollection pc, string workSpacePath)
        {
            var workspaceInfo = Workstation.Current.GetLocalWorkspaceInfo(workSpacePath) ?? Workstation.Current.GetLocalWorkspaceInfo(Path.GetTempPath());
            return workspaceInfo;
        }

        private static void GetLatestVersionConfig(string workSpacePath, string fileName, TfsTeamProjectCollection pc)
        {
            pc.EnsureAuthenticated();
            VersionControlServer sourceControl = (VersionControlServer)pc.GetService(typeof(VersionControlServer));
            Workspace workspace = sourceControl.GetWorkspace(Environment.MachineName, sourceControl.AuthenticatedUser);
            if (workspace != null)
            {
                var workingfolder = new WorkingFolder(workSpacePath, Path.GetTempPath());
                workspace.CreateMapping(workingfolder);
                var items = sourceControl.GetItems(workSpacePath, RecursionType.OneLevel).Items.Select(i => i.ServerItem);
                workspace.Get(items.ToArray<string>(), VersionSpec.Latest, RecursionType.None, GetOptions.Overwrite);
            }
        }

        public static void GetTemplateFromTfs(string fileName)
        {
            using (TfsTeamProjectCollection pc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(tfsServer)))
            {
                var templatePath = tfsWorkspacePath + "/Tools/ReleaseManifest/ReleaseManifests/Templates/";
                VersionControlServer sourceControl = (VersionControlServer)pc.GetService(typeof(VersionControlServer));

                var items = sourceControl.GetItems(templatePath, VersionSpec.Latest, RecursionType.Full)
                                         .Items
                                         .Where(x => x.ItemType == ItemType.File && x.ServerItem == templatePath + fileName)
                                         .First();
                if (items != null)
                {
                    items.DownloadFile(Path.GetTempPath() + items.ServerItem.Substring(items.ServerItem.LastIndexOf('/')));
                }
                pc.Dispose();

                //for (int x = 0; x < items.Count; x++)
                //    items[x].DownloadFile(Path.GetTempPath() + items[x].ServerItem.Substring(items[x].ServerItem.LastIndexOf('/')));
            }
        }

    }
}
