using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.Model.NewModels;
using NaiveInkCanvas.View;
using NotificationBuilder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.ViewModel.News
{
    public class StartPageViewModel:ViewModelBase
    {
        public ObservableCollection<ProjectInfoModel> Projects{ get; }
            = new ObservableCollection<ProjectInfoModel>();
        public ObservableCollection<ProjectInfoModel> SearchedProjects{ get; } 
            = new ObservableCollection<ProjectInfoModel>();

        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set
            {
                Set("projectName", ref projectName, value);
                try { Name = value.Split('.')[0]; } catch (Exception) { Name = value; }
                foreach (var project in Projects)
                {
                    if(project.Name.IndexOf(value)!=-1)
                    {
                        OperatorButtonName = LoadOperator;
                        return;
                    }
                }
                OperatorButtonName = CreateOperator;
            }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { Set("Name", ref name, value); }
        }

        /// <summary>
        /// 创建工程按钮是否可见
        /// </summary>
        private Visibility createProjectButtonVisibility = Visibility.Collapsed;

        public Visibility CreateProjectButtonVisibility
        {
            get { return createProjectButtonVisibility; }
            set
            {
                Set("CreateProjectButtonVisibility", ref createProjectButtonVisibility, value);
            }
        }
        private static readonly string CreateOperator = "创建";
        private static readonly string LoadOperator = "加载";
        private string operatorButtonName = CreateOperator;

        public string OperatorButtonName
        {
            get { return operatorButtonName; }
            set
            {
                operatorButtonName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 加载工程按钮是否可见
        /// </summary>
        private Visibility loadProjectButtonVisibility = Visibility.Collapsed;

        public Visibility LoadProjectButtonVisibility
        {
            get { return loadProjectButtonVisibility; }
            set
            {
                Set("LoadProjectButtonVisibility", ref loadProjectButtonVisibility, value);
            }
        }
        public RelayCommand ProjectOperatorCommand { get; }
        public RelayCommand SaveOtherFolderCommand { get; }
        public RelayCommand<ProjectInfoModel> DeleteProjectCommand { get; }
        public StartPageViewModel()
        {
            ApplicationHelper.Default.SetWindowTitle("创建或选择项目");
            UpdataData();
            ProjectOperatorCommand = new RelayCommand(ProjectOperator);
            SaveOtherFolderCommand = new RelayCommand(SaveOtherFolder);
            DeleteProjectCommand = new RelayCommand<ProjectInfoModel>(DeleteProject);
        }
        public async void UpdataData()
        {
            await _UpdataData();
        }
        private async Task _UpdataData()
        {
            /*
            var datas = from pj in SettingHelper.ProjectContainer.Values
                        select new ProjectInfoModel(pj.Key, pj.Value.ToString());
            foreach (var data in datas)
            {
                if (!await StorageHelper.Default.IsFileExist(StorageHelper.Default.LocFolder,data.ProjectName+".isf"))
                {
                    StorageHelper.Default.DeleteFolder(StorageHelper.Default.BackgroundImgs, data.ProjectName);
                    continue;
                }
                Projects.Add(data);
            }
            */
            Projects.Clear();
            (from file in await StorageHelper.Default.LocFolder.GetFilesAsync()
             where file.FileType == ".isf"
             select file).ToList().ForEach(file => Projects.Add(new ProjectInfoModel(file.Name, file.Path)));
        }
        private async void ProjectOperator()
        {
            if (string.IsNullOrEmpty(ProjectName))
                return;
            var filename = ProjectName + ".isf";
            var file = await StorageHelper.Default.GetFile(StorageHelper.Default.LocFolder, filename);
            StorageFile newfile = null;
            var pim = new ProjectInfoModel(ProjectName+".isf",
                file != null ? file.Path :
               (newfile = await StorageHelper.Default.CreateFile(StorageHelper.Default.LocFolder, filename)).Path);
            if (file == null && newfile != null)
            {
                var LocSetting = SettingHelper.ProjectContainer;
                if (LocSetting.Values.Keys.Contains(newfile.Name))
                    LocSetting.Values[newfile.Name] = newfile.Path;
                else
                    LocSetting.Values.Add(newfile.Name, newfile.Path);
            }
            //SettingHelper.Default.SetSettingValue(SettingHelper.LocContainer, "IsOpen", "1", false);
            var bki = await StorageHelper.Default.GetFolder(StorageHelper.Default.LocFolder, "BackgroundImgs");
            await StorageHelper.Default.CreateFolder(bki, ProjectName);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            ApplicationHelper.Default.SetWindowTitle(ProjectName);
            var navSer = SimpleIoc.Default.GetInstance<INavigationService>();
            //ApplicationHelper.Default.LocProject = pim;            
            navSer.NavigateTo(ViewModelLocator.CanvasViewKey, pim);
        }
        private async Task<bool> IsSettingFileExit(ApplicationDataContainer appcon, StorageFolder appfolder, string filename)
        {
            if (appcon.Values.Keys.Contains(filename))
            {
                foreach (var file in await appfolder.GetFilesAsync())
                {
                    if (file.Name == filename + ".isf")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public event Action<ProjectInfoModel> DeleteProjected;
        private async void DeleteProject(ProjectInfoModel data)
        {

            if (!await ApplicationHelper.Default.Quire("是否删除此工程:" + data.ProjectName))
                return;
            var appset = SettingHelper.LocContainer.CreateContainer("Projects", ApplicationDataCreateDisposition.Always);
            try
            {
                await (await StorageHelper.Default.LocFolder.GetFileAsync(data.ProjectName)).DeleteAsync();
                var folder = await (await StorageHelper.Default.LocFolder.GetFolderAsync("BackgroundImgs")).GetFolderAsync(data.Name);
                await folder.DeleteAsync();
                Projects.Remove(data);
                DeleteProjected?.Invoke(data);
            }
            catch (Exception) { }

        }
        private async void SaveOtherFolder()
        {
            var fp = new FolderPicker();
            fp.FileTypeFilter.Add("*");
            fp.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            var folder = await fp.PickSingleFolderAsync();
            if (folder != null)
            {
                foreach (var pj in Projects)
                {
                    if (pj.Name == ProjectName)
                    {
                        try
                        {
                            await (await StorageHelper.Default.LocFolder.GetFileAsync(pj.ProjectName)).CopyAsync(folder);
                            var copto = await folder.CreateFolderAsync(pj.Name);
                            var sourcefolder = await (await StorageHelper.Default.LocFolder.GetFolderAsync("BackgroundImgs")).GetFolderAsync(pj.Name);
                            foreach (var file in await sourcefolder.GetFilesAsync())
                            {
                                await file.CopyAsync(copto);
                            }
                            NotificationManager.NotifyText("导出工程" + pj.Name + "完成");
                        }
                        catch (Exception)
                        {
                            NotificationManager.NotifyText("导出工程" + pj.Name + "失败");
                        }
                    }
                }
            }
        }
        public void UpdataSearchProject()
        {
            if (ProjectName != null)
            {
                SearchedProjects.Clear();
                foreach (var pj in Projects.Where(p => p.ProjectName.IndexOf(ProjectName) != -1))
                    SearchedProjects.Add(pj);
            }
        }    
        public void ItemQuerySubmitted(ProjectInfoModel model)
        {
            ProjectName = model.Name;
        }
    }
}
