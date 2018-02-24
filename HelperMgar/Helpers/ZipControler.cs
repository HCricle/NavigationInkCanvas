using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NaiveInkCanvas.Helpers
{
    public class ZipKeeper
    {
        public List<string> Paths { get; }
        public StorageFolder WorkFolder { get; }
        /// <summary>
        /// 构建Zip保存器
        /// </summary>
        /// <param name="workFolder">要操作的根目录</param>
        public ZipKeeper(StorageFolder workFolder)
        {
            WorkFolder = workFolder;
            Paths = new List<string>();
        }
        public async Task InsetWorkFolderAllFile(StorageFolder folder)
        {
            var files = await folder.GetFilesAsync();
            var folders = await folder.GetFoldersAsync();
            foreach (var file in files)
            {
                var str = file.Path.Replace(WorkFolder.Path, "");
                str = str.Substring(1);
                Debug.Assert(!string.IsNullOrEmpty(str));
                Paths.Add(str);
            }
            if (folders.Count > 0)
            {
                foreach (var f in folders)
                {
                    await InsetWorkFolderAllFile(f);
                }
            }
        }
        public async Task<StorageFile> SaveToZip(StorageFolder saveFolder, string fileName)
        {
            var savefile = await saveFolder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);
            await Task.Run(async () =>
            {
                var file = await saveFolder.GetFileAsync(fileName);
                using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (var zip = new ZipArchive(fs.AsStream(), ZipArchiveMode.Update))
                    {
                        foreach (var path in Paths)
                        {
                            zip.CreateEntryFromFile(WorkFolder.Path + "\\" + path, path);
                        }
                    }
                }
            });
            return savefile;
        }
    }
    public static class ZipReader
    {
        public static async Task<StorageFolder> ReadZip(StorageFile file, StorageFolder extractFolder, string folderName)
        {
            var fs = await file.OpenAsync(FileAccessMode.ReadWrite);
            var zip = new ZipArchive(fs.AsStream(), ZipArchiveMode.Update);
            zip.ExtractToDirectory(extractFolder.Path + "\\" + folderName);
            return await extractFolder.GetFolderAsync(folderName);
        }
    }
}
