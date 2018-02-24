using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NaiveInkCanvas.Helpers
{
    public class StorageHelper
    {
        private static StorageHelper Instance;
        public static StorageHelper Default => 
            Instance ?? (Instance = new StorageHelper());
        public StorageFolder LocFolder =>
            ApplicationData.Current.LocalFolder;
        public StorageFolder LocCacheFolder =>
            ApplicationData.Current.LocalCacheFolder;
        public StorageFolder BackgroundImgs { get; private set; }
        /// <summary>
        /// 目录文件是否存在
        /// </summary>
        /// <param name="rootPath">查找的目录</param>
        /// <param name="filename">查找的文件名字，包括扩展名</param>
        /// <param name="isupp">是否区分大小写</param>
        /// <returns></returns>
        public async Task<bool> IsFileExist(StorageFolder rootPath, string filename, bool isupp = true)
        {
            foreach (var file in await rootPath.GetFilesAsync())
            {
                if (isupp)
                {
                    if (file.Name == filename)
                        return true;
                }
                else
                    if (file.Name.ToLower() == filename.ToLower())
                    return true;

            }
            return false;
        }
        /// <summary>
        /// 目录目录是否存在
        /// </summary>
        /// <param name="rootPath">查找的目录</param>
        /// <param name="filename">查找的目录名字</param>
        /// <param name="isupp">是否区分大小写</param>
        /// <returns></returns>    
        public async Task<bool> IsFolderExist(StorageFolder rootPath, string filename, bool isupp = true)
        {
            foreach (var file in await rootPath.GetFoldersAsync())
            {
                if (isupp)
                {
                    if (file.Name == filename)
                        return true;
                }
                else
                    if (file.Name.ToLower() == filename.ToLower())
                    return true;

            }
            return false;
        }
        /// <summary>
        /// 取目录所有文件
        /// </summary>
        /// <param name="folder">目标目录</param>
        /// <returns>linq</returns>
        public async Task<IEnumerable<StorageFile>> GetFiles(StorageFolder folder)
        {
            return from file in await folder.GetFilesAsync() select file;
        }
        /// <summary>
        /// 取目录一文件
        /// </summary>
        /// <param name="folder">根目录</param>
        /// <param name="name">文件名，包括扩展名</param>
        /// <returns>如果失败返回null</returns>
        public async Task<StorageFile> GetFile(StorageFolder folder,string name)
        {
            try
            {
                return await folder.GetFileAsync(name);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 在目录创建文件，如果无权限或者已存在，返回null
        /// </summary>
        /// <param name="folder">根目录</param>
        /// <param name="name">文件名，包括扩展名</param>
        /// <returns></returns>
        public async Task<StorageFile> CreateFile(StorageFolder folder,string name)
        {
            try
            {
                return await folder.CreateFileAsync(name);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> IsSettingFileExist(ApplicationDataContainer appcon, StorageFolder appfolder, string filename)
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
        /// <summary>
        /// 创建目录，失败返回null
        /// </summary>
        /// <param name="folder">根目录</param>
        /// <param name="name">目录名</param>
        /// <returns></returns>
        public async Task<StorageFolder> CreateFolder(StorageFolder folder,string name)
        {
            try
            {
                return await folder.CreateFolderAsync(name);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 取目录目录
        /// </summary>
        /// <param name="folder">根目录</param>
        /// <param name="name">目录名</param>
        /// <param name="notfilecreate">不存在是否创建</param>
        /// <returns></returns>
        public async Task<StorageFolder> GetFolder(StorageFolder folder,string name,bool notfilecreate=true)
        {
            try
            {
                return await folder.GetFolderAsync(name);
            }
            catch (Exception)
            {
                if (notfilecreate)
                    return await folder.CreateFolderAsync(name);
                return null;
            }
        }
        public async void DeleteFolder(StorageFolder folder,string name)
        {
            try
            {
                await (await folder.GetFolderAsync(name)).DeleteAsync();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
