using NaiveInkCanvas.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NaiveInkCanvas.Model.NewModels
{
    public class ProjectInfoModel
    {
        public string ProjectName { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public StorageFile SourceFile { get; private set; }
        public StorageFolder BackgroundFolder { get; private set; }
        public ProjectInfoModel(string pjn, string p)
        {
            ProjectName = pjn;
            Path = p;
            try
            {
                Name = ProjectName.Split('.')[0];
            }
            catch (Exception) { Name = ProjectName; }
            InitStorage();
        }
        private async void InitStorage()
        {
            try
            {
                var strs = Path.Substring(0, Path.LastIndexOf('\\'));
                BackgroundFolder = await StorageFolder.GetFolderFromPathAsync(strs);
                SourceFile = await StorageFile.GetFileFromPathAsync(Path);
            }
            catch (Exception e)
            {
                Debug.Assert(false);
            }
        }
        public async void DeleteBackgroundImgFolder()
        {
            await BackgroundFolder.DeleteAsync();
        }
        public async void CreateNewBackgroundImgFolder()
        {
            await StorageHelper.Default.BackgroundImgs.CreateFolderAsync(ProjectName);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
