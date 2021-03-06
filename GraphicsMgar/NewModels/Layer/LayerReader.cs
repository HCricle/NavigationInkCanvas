﻿using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Layer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Model.NewModels.Layer
{
    public class LayerReader:IDisposable
    {
        private StorageFolder tmpFolder;
        private bool isInitSucceed;
        public List<CanvasData> datas;

        public bool IsInitSucceed => isInitSucceed;
        public List<CanvasData> Datas => datas;
        public List<LayerModel> Layers { get; }
        public Rect CanvasSize { get; set; }
        public StorageFile SourceFile { get; }
        public StorageFolder TmpFolder => tmpFolder;
        public bool IsDeWrapper { get; set; }

        public readonly string TmpSourceFolderName = "tmpRead";
        public readonly string XmlDocName = "CanvasData.xml";
        public readonly string InkStrokesFolderName = "InkStrokes";

        public event Action<LayerReader> FileRead;
        public LayerReader(StorageFile sourceFile)
        {
            SourceFile = sourceFile;
            isInitSucceed = false;
            Layers = new List<LayerModel>();
        }
        public async Task<bool> InitFile()
        {
            if(await StorageHelper.Default.IsFolderExist(ApplicationData.Current.LocalCacheFolder, TmpSourceFolderName))
            {
                await (await ApplicationData.Current.LocalCacheFolder.GetFolderAsync(TmpSourceFolderName)).DeleteAsync();
            }
            try
            {
                await DeWarpper();
                var ser = new DataContractSerializer(typeof(List<CanvasData>));
                var file = await TmpFolder.GetFileAsync(XmlDocName);
                using (var fs =await file.OpenAsync( FileAccessMode.Read))
                {
                    using (var reader = XmlReader.Create(fs.AsStream()))
                    {
                        datas = (List<CanvasData>)ser.ReadObject(reader);
                    }
                }                                
                isInitSucceed = true;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }
            return isInitSucceed;
        }
        public async Task<bool> ReadInFolderAsync()
        {
            Debug.Assert(IsDeWrapper && IsInitSucceed);
            var folders = (await TmpFolder.GetFoldersAsync())
                .Where(folder => folder.DisplayName == InkStrokesFolderName)
                .ToList();
            if (folders.Count!=0)
            {
                var folder = folders[0];
                var files = (await folder.GetFilesAsync()).Where(f => f.FileType.ToLower() == ".isf");
                foreach (var file in files)
                {
                    var ic = new InkCanvas();
                    var filename = file.Name.Substring(0, file.Name.Length - 4);
                    using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await ic.InkPresenter.StrokeContainer.LoadAsync(fs);
                    }
                    var strokes = ic.InkPresenter.StrokeContainer.GetStrokes().ToList();
                    var gslength = Enum.GetValues(typeof(GraphicsTypes)).Length;
                    var layer = new LayerModel();
                    foreach (var stroke in strokes)
                    {
                        var gi = stroke.StrokeDuration.Value.Ticks;
                        if (gi > 0 && gi < gslength)
                        {
                            var gr = (GraphicsRectSizeBase)GraphicsInstCreateManager.CreateGraphics((GraphicsTypes)gi, layer.LocPenModel, layer, layer);
                            Debug.Assert(gr != null);
                            stroke.GetInkPoints().ToList().ForEach(p => gr.Points.Add(p));
                            gr.PenAttribute = PenModel.ToPenModel(stroke.DrawingAttributes);
                            layer.GraphicsRects.Add(gr);
                        }
                    }
                    layer.Name = filename;
                    layer.CanvasSize = CanvasSize;
                    FileRead?.Invoke(this);
                    Layers.Add(layer);
                }
                return true;
            }
            return false;
        }
        private async Task DeWarpper()
        {
            IsDeWrapper = true;
            tmpFolder= await ZipReader.ReadZip(SourceFile, ApplicationData.Current.LocalCacheFolder, TmpSourceFolderName);
        }

        public async void Dispose()
        {
            await TmpFolder?.DeleteAsync( StorageDeleteOption.PermanentDelete);
        }
    }
}
