using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Model.NewModels.Layer
{
    public class LayerReader:IDisposable
    {
        private XmlDocument xmlDoc;
        private StorageFolder tmpFolder;

        public List<LayerModel> Layers { get; }
        public Rect CanvasSize { get; set; }
        public StorageFile SourceFile { get; }
        public StorageFolder TmpFolder => tmpFolder;
        public bool IsDeWrapper { get; set; }
        public XmlDocument XmlDoc => xmlDoc;

        public readonly string TmpSourceFolderName = "tmpRead";
        public readonly string XmlDocName = "CanvasData.xml";
        public readonly string InkStrokesFolderName = "InkStrokes";

        public event Action<XmlDocument> Reading;
        public LayerReader(StorageFile sourceFile)
        {
            SourceFile = sourceFile;
            Layers = new List<LayerModel>();
        }
        public async Task<bool> InitFile()
        {
            if ((await ApplicationData.Current.LocalCacheFolder.GetFoldersAsync())
                    .Where(folder => folder.Name == TmpSourceFolderName).Count() > 0)
            {
                await (await ApplicationData.Current.LocalCacheFolder.GetFolderAsync(TmpSourceFolderName)).DeleteAsync();
            }
            await DeWarpper();
            try
            {
                xmlDoc = await XmlDocument.LoadFromFileAsync(await TmpFolder.GetFileAsync(XmlDocName));
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public async Task<bool> ReadInFolderAsync()
        {
            Debug.Assert(IsDeWrapper);
            var folders = (await TmpFolder.GetFoldersAsync()).Where(folder => folder.DisplayName == InkStrokesFolderName).ToList();
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
                    Debug.Assert(XmlDoc != null);
                    var xmlEle = XmlDoc.SelectSingleNode(filename);
                    if (xmlEle != null)
                    {
                        var layerData = xmlEle.ChildNodes.Where(xmlNode => xmlNode.NodeName == "IsLock" || xmlNode.NodeName == "IsAppear").ToArray();
                        Debug.Assert(layerData.Count() == 2);
                        layer.IsLock = Convert.ToBoolean(layerData[0].InnerText);
                        layer.IsAppear = Convert.ToBoolean(layerData[1].InnerText);
                    }
                    //TODO:Init Others
                    Reading?.Invoke(XmlDoc);
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
