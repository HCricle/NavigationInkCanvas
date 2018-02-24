using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Layer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Input.Inking;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Model.NewModels.Layer
{
    public class LayerKeeper:IDisposable
    {    
        private StorageFolder TmpSaveBackgroundFolder { get; set; }
        private StorageFolder TmpSaveLayersFolder { get; set; }
        private StorageFolder TmpBaseFolder { get; set; }
        private string FileName { get; }

        private List<CanvasData> Datas { get; }
        private bool IsInitFolder { get; set; }
        private List<LayerModel> Layers { get; }

        public Color BackgroundColor { get; set; }
        public Size CanvasSize { get; set; }
        public List<string> ErrMsg { get; }
        public bool IsError => ErrMsg.Count > 0;
        public event Action<XmlWriter> XmlKeeping;
        public readonly string XmlDocName = "CanvasData.xml";
        public readonly string ImageFileName = "CanvasImage.png";
        public readonly string BaseFolderName = "TmpBaseFolder";
        public readonly string TmpSaveLayersFolderName = "Layers";
        public readonly string TmpSaveBackgroundsFolderName = "Backgrounds";

        public LayerKeeper(string fileName)
        {
            BackgroundColor = Colors.White;
            ErrMsg = new List<string>();
            Datas = new List<CanvasData>();
            IsInitFolder = false;
            FileName = fileName;
            Layers = new List<LayerModel>();
            Task.WaitAll(Task.Run(async () =>
            {
                await GetTmpSaveFolder();
                IsInitFolder = true;
            }));
        }
        public LayerKeeper(string fileName,Size canvasSize)
            :this(fileName)
        {
            CanvasSize = canvasSize;
        }
        private async Task GetTmpSaveFolder()
        {
            TmpBaseFolder = await CreateFolder(StorageHelper.Default.LocCacheFolder, BaseFolderName);
            TmpSaveBackgroundFolder = await CreateFolder(TmpBaseFolder, TmpSaveBackgroundsFolderName);
            TmpSaveLayersFolder = await CreateFolder(TmpBaseFolder, TmpSaveLayersFolderName);

        }
        private async Task<StorageFolder> CreateFolder(StorageFolder folder,string name)
        {
            if (await StorageHelper.Default.IsFolderExist(folder, name))
            {
                await folder.DeleteAsync();
            }
            return await folder.CreateFolderAsync(name);
        }
        public string GetErrMsg()
        {
            string res = "";
            ErrMsg.ForEach(s => res += s);
            return res;
        }
        public async Task InsetSaveLayer(LayerModel layer)
        {
            try
            {
                var lkd = new CanvasData(layer.Name) { DataTypeString="Canvas"};
                lkd.InsertData(nameof(layer.IsLock), layer.IsLock);
                lkd.InsertData(nameof(layer.IsAppear), layer.IsAppear);
                Datas.Add(lkd);
                var gkd = new CanvasData("");
                Layers.Add(layer);
                var ic = new InkCanvas();
                var isb = new InkStrokeBuilder();
                var converter = InkStrokeConverter.Inst;
                foreach (var grap in layer.GraphicsRects)
                {
                    var res=converter.ExeistConver(grap.Graphics, new InkStrokeConverterArgs(grap,ic,isb));
                    if (res!=null)
                    {
                        Datas.Add(res);
                    }
                }
                //保存笔迹
                var file = await TmpSaveLayersFolder.CreateFileAsync(layer.Name + ".isf", CreationCollisionOption.GenerateUniqueName);
                using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await ic.InkPresenter.StrokeContainer.SaveAsync(fs);
                }
            }
            catch (Exception ex)
            {
                ErrMsg.Add("保存失败-错误原因:-" + ex.Message + "-请重试\n");
            }
        }
        public async Task<StorageFile> SaveInFile()
        {
            var file = await TmpBaseFolder.CreateFileAsync(XmlDocName, CreationCollisionOption.ReplaceExisting);
            var strBdr = new StringBuilder();
            using (var xw =XmlWriter.Create(new StringWriter(strBdr)))
            {
                var dataSer = new DataContractSerializer(typeof(List<CanvasData>));
                dataSer.WriteObject(xw, Datas);
                XmlKeeping?.Invoke(xw);
                xw.Flush();            
                await FileIO.WriteTextAsync(file, strBdr.ToString());
            }
            var imgfile = await TmpBaseFolder.CreateFileAsync(ImageFileName, CreationCollisionOption.ReplaceExisting);
            using (var fs = await imgfile.OpenAsync(FileAccessMode.ReadWrite)) 
            {
                await SaveInStreamAsync(fs);
                await fs.FlushAsync();                
            }

            return await Wrapper();
        }
        public async Task SaveInStreamAsync(IRandomAccessStream randomAccessStream)
        {
            var device = CanvasDevice.GetSharedDevice();
            using (var renderTarget = new CanvasRenderTarget(device, (float)CanvasSize.Width, (float)CanvasSize.Height, 97))
            {
                using (var ds = renderTarget.CreateDrawingSession())
                {
                    ds.Clear(BackgroundColor);
                    Layers.ToList().ForEach(lm => lm.Canvas_Draw(lm.Canvas, new CanvasDrawEventArgs(ds)));
                    ds.Flush();
                    await renderTarget.SaveAsync(randomAccessStream, CanvasBitmapFileFormat.Png);
                }
            }
        }
        private async Task<StorageFile> Wrapper()
        {
            var zk = new ZipKeeper(TmpBaseFolder);
            await zk.InsetWorkFolderAllFile(zk.WorkFolder);
            return await zk.SaveToZip(TmpBaseFolder, FileName);
        }

        public async void Dispose()
        {
            await TmpSaveLayersFolder?.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }
    }
}
