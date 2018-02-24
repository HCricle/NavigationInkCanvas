using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using NaiveInkCanvas.Background;
using NaiveInkCanvas.Background.Models;
using NaiveInkCanvas.Controls;
using NaiveInkCanvas.Controls.PenAttributesModels;
using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.Helpers.DefHelper;
using NaiveInkCanvas.Model.NewModels;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Layer;
using NaiveInkCanvas.Pens.PenDefs;
using NaiveInkCanvas.Pens.PenPart.PenPartDefs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace NaiveInkCanvas.ViewModel.News
{
    public class SingleCanvasViewModel : ViewModelBase, ICanObservation, ICanNotifyPropertyChanged
    {
        private ProjectInfoModel Project { get; set; }
        /// <summary>
        /// 头菜单集合
        /// </summary>
        public ObservableCollection<MenuModel> HeadMenus { get; }
            = new ObservableCollection<MenuModel>();
        /// <summary>
        /// 辅助菜单集合
        /// </summary>
        public ObservableCollection<MenuContextModel> MenuContexts { get; }
            = new ObservableCollection<MenuContextModel>();
        /// <summary>
        /// 图层集合
        /// </summary>
        public ObservableCollection<LayerModel> Layers { get; }
            = new ObservableCollection<LayerModel>();
        public List<LayerModel> OtherLayers { get; }
            = new List<LayerModel>();
        /// <summary>
        /// 背景图,里面的都是可移动控件
        /// </summary>
        private Canvas mainCanvas = new Canvas();

        public Canvas MainCanvas
        {
            get => mainCanvas;
            set => Set("MainCanvas", ref mainCanvas, value);
        }

        private LayerModel locLayer;

        public LayerModel LocLayer
        {
            get => locLayer;
            set
            {
                if (locLayer != null)
                {
                    locLayer.PenChanged -= LayerModel_PenChanged;
                }
                value.PenChanged += LayerModel_PenChanged;
                value.ChangeCanvasModel(true);
                Set("LocLayer", ref locLayer, value);
                OtherLayers.Clear();
                Layers.ToList().Where(lm => lm.IsAppear && lm != locLayer).ToList()
                    .ForEach(lm => OtherLayers.Add(lm));
                //PenControl.ViewModel.LocalLayerModel = value;
                PenControl = value.PenControl;
                ValueChanged?.Invoke(LocLayer);
            }
        }
        private Thickness paintBorderWidth = new Thickness(1);

        public Thickness PaintBorderWidth
        {
            get => paintBorderWidth;
            set => Set("PaintBorderWidth", ref paintBorderWidth, value);
        }
        private Brush paintBorderBrush = new SolidColorBrush(Colors.DarkGray);

        public Brush PaintBorderColor
        {
            get => paintBorderBrush;
            set => Set("PaintBorderBrush", ref paintBorderBrush, value);
        }
        private PenAttributesControl penControl;

        public PenAttributesControl PenControl
        {
            get => penControl;
            set
            {
                PenControlChanged?.Invoke(penControl, value);
                Set("penControl", ref penControl, value);
            }
        }
        private PaintingModels paintModel;

        public PaintingModels PaintModel
        {
            get => paintModel;
            set
            {
                paintModel = value;
                PaintModelChanged?.Invoke(value);
            }
        }

        public Size CanvasSize { get; set; }
        public float CanvasDpi { get; set; } = 100f;

        /// <summary>
        /// 这个属性等同于SetPenAttribute
        /// 设置未来画画的笔属性
        /// </summary>
        public PenModel LocCanvasPenModel { get => LocLayer.LocPenModel; }

        #region Events
        public event Action<LayerModel> SelectedLayer;
        public event Action<LayerModel> RectangleSelecting;
        public event Action<PenAttributesControl, PenAttributesControl> PenControlChanged;
        public event Action<object> ValueChanged;
        public event Action<PaintingModels> PaintModelChanged;
        public event Action<LayerModel, string> NameChanged;
        public event Action<LayerModel, bool> AppearChanged;
        public event Action<LayerModel, bool> LockChanged;
        #endregion

        private CoreDispatcher UIThreadPath;
        #region CanMoveControls

        #endregion

        #region Commands
        public RelayCommand FullScreenCommand { get; }
        public RelayCommand ShareDataCommand { get; }
        public RelayCommand AboutCommand { get; }
        public RelayCommand DeleteCanvasCommand { get; }
        public RelayCommand DeleteAllCanvasCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand SaveAsCommand { get; }
        public RelayCommand DeleteAllBackgroundCommand { get; }
        public RelayCommand NewDefaultLayerCommand { get; }
        public RelayCommand ReconverteCommand { get; }
        public RelayCommand RedoCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand PaintModelChangeCommand { get; }
        public RelayCommand<LayerModel> NewLayerCommand { get; }
        public RelayCommand<LayerModel> RemoveLayerCommand { get; }
        public RelayCommand<LayerModel> SelectLayerCommand { get; }
        public RelayCommand<ImgBackground> AddBackgroundCommand { get; }
        #endregion
        public SingleCanvasViewModel()
        {
            PenPartControlBase.LayerObservated = this;
            PenAttributeModel.LayerObservated = this;

            CanvasSize = new Size(1000, 1000);
            AddBackgroundCommand = new RelayCommand<ImgBackground>(AddBackground);
            FullScreenCommand = new RelayCommand(FullScreen);
            ShareDataCommand = new RelayCommand(ShareData);
            AboutCommand = new RelayCommand(About);
            DeleteCanvasCommand = new RelayCommand(DeleteCanvas);
            DeleteAllCanvasCommand = new RelayCommand(DeleteAllCanvas);
            SaveCommand = new RelayCommand(Save);
            SaveAsCommand = new RelayCommand(SaveAs);
            DeleteAllBackgroundCommand = new RelayCommand(DeleteAllBackground);
            ExitCommand = new RelayCommand(Exit);
            PaintModelChangeCommand = new RelayCommand(PaintModelChange);
            //news
            NewDefaultLayerCommand = new RelayCommand(NewDefaultLayer);
            ReconverteCommand = new RelayCommand(Reconverte);
            RedoCommand = new RelayCommand(Redo);
            NewLayerCommand = new RelayCommand<LayerModel>(NewLayer);
            RemoveLayerCommand = new RelayCommand<LayerModel>(RemoveLayer);
            SelectLayerCommand = new RelayCommand<LayerModel>(SelectLayer);

            #region MenuContext Define 辅助菜单定义
            MenuContexts.Add(new MenuContextModel() { SymbolIco = Symbol.Link, Text = "加入背景", MenuCommad = AddBackgroundCommand });
            var mcm = new MenuContextModel()
            {
                SymbolIco = Symbol.Scan,
                MenuCommad = PaintModelChangeCommand
            };
            PaintModelChanged += (data) =>
                mcm.Text = PaintingModelHelper.ModelStrings[data];
            MenuContexts.Add(mcm);
            MenuContexts.Add(new MenuContextModel() { SymbolIco = Symbol.Link, Text = "分享", MenuCommad = ShareDataCommand });
            MenuContexts.Add(new MenuContextModel() { SymbolIco = Symbol.FullScreen, Text = "全屏", MenuCommad = FullScreenCommand });
            #endregion

            #region HeadMenu Define 主菜单定义
            HeadMenus.Add(new MenuModel() { Ico = Symbol.NewFolder, Text = "新建画布(图层)", Command = NewDefaultLayerCommand });
            HeadMenus.Add(new MenuModel() { Ico = Symbol.Document, Text = "保存", Command = SaveCommand });
            HeadMenus.Add(new MenuModel() { Ico = Symbol.Document, Text = "另存为", Command = SaveAsCommand });
            HeadMenus.Add(new MenuModel() { Ico = Symbol.Delete, Text = "删除画布", Command = DeleteCanvasCommand });
            HeadMenus.Add(new MenuModel() { Ico = Symbol.Delete, Text = "删除所有画布", Command = DeleteAllCanvasCommand });
            HeadMenus.Add(new MenuModel() { Ico = Symbol.Go, Text = "退出", Command = ExitCommand });
            #endregion
            PaintModel = PaintingModels.LayerPainting;
        }
        ~SingleCanvasViewModel()
        {
            Layers.ToList().ForEach(lm => lm.Canvas.RemoveFromVisualTree());
        }
        private void PaintModelChange()
        {
            PaintModel = (PaintingModels)((((int)PaintModel) + 1) % Enum.GetValues(typeof(PaintingModels)).Length);
        }
        private async void Exit()
        {
            if (await ApplicationHelper.Default.Quire("是否退出(记得保存呀！)"))
            {
                Application.Current.Exit();
            }
        }
        private async void AddBackground(ImgBackground background)
        {
            await background.BorderControl.LoadImg();
            BackgroundManager.Inst.Backgrounds.Add(background);
        }
        private void Reconverte()
        {
            if (LocLayer.HistoriesManager.CanGoback)
            {
                LocLayer.HistoriesManager.PopHistory().Revocation();
            }
        }
        private void Redo()
        {
            if (LocLayer.HistoriesManager.CanGoahead)
            {
                LocLayer.HistoriesManager.PopRedo().Redo();
            }
        }
        private void LayerModel_PenChanged(LayerModel arg1, GraphicsTypes arg2)
        {
            if (arg2 == GraphicsTypes.SelectRectangle)
            {
                RectangleSelecting?.Invoke(arg1);
            }
        }
        public LayerModel this[int i]
        {
            get
            {
                Debug.Assert(i < Layers.Count && i >= 0);
                return Layers[i];
            }
        }
        public void SetSenderDependencyObject(CoreDispatcher dependency)
        {
            UIThreadPath = dependency;
        }
        private void SelectLayer(LayerModel layer)
        {
            SelectSingleLayer(layer);
        }
        public void SelectSingleLayer(LayerModel layer)
        {
            Debug.Assert(layer != null);
            LocLayer = layer;
            SelectedLayer?.Invoke(layer);
        }
        public int GetSelectIndex(LayerModel layer)
        {
            Debug.Assert(Layers.IndexOf(layer) != -1);
            return Layers.IndexOf(layer);
        }
        private void RemoveLayer(LayerModel layer)
        {
            Layers.Remove(layer);
        }
        private void NewLayer(LayerModel layerModel)
        {
            //if (PenControl == null)
            //    PenControl = new PenAttributesControl(layerModel) { Width=300,Height=750};
            Layers.Add(layerModel);
        }
        private async void NewDefaultLayer()
        {
            await NewDefaultLayerCustom();
        }
        public async Task NewDefaultLayerCustom(string name = null)
        {
            await UIThreadPath.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var layer = new LayerModel() { Name = name ?? "未命名" };
                layer.CanvasSize = new Rect(new Point(0, 0), CanvasSize);
                layer.AppearChanged += Layer_AppearChanged;
                layer.NameChanged += Layer_NameChanged;
                layer.LockChanged += Layer_LockChanged;
                var bdWidth = new Binding() { Source = CanvasSize, Path = new PropertyPath("Width") };
                var bdHeight = new Binding() { Source = CanvasSize, Path = new PropertyPath("Height") };
                layer.Canvas.SetBinding(FrameworkElement.WidthProperty, bdWidth);
                layer.Canvas.SetBinding(FrameworkElement.HeightProperty, bdHeight);
                NewLayer(layer);
            });
        }

        private void Layer_LockChanged(LayerModel arg1, bool arg2)
        {
            LockChanged?.Invoke(arg1, arg2);
        }

        private void Layer_NameChanged(LayerModel arg1, string arg2)
        {
            NameChanged?.Invoke(arg1, arg2);
        }

        private void Layer_AppearChanged(LayerModel arg1, bool arg2)
        {
            AppearChanged?.Invoke(arg1, arg2);
        }

        private async void DeleteAllBackground()
        {
            if (!await ApplicationHelper.Default.Quire("是否删除所有背景，一旦删除此应用数据，这个工程的所有背景副本都会被删除"))
                return;
            Project.DeleteBackgroundImgFolder();
            Project.CreateNewBackgroundImgFolder();
        }
        private async void SaveAs()
        {
            try
            {
                var file = await BuildKeepFile();
                var folderpk = new FolderPicker();
                folderpk.FileTypeFilter.Add("*");
                folderpk.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                var folder = await folderpk.PickSingleFolderAsync();
                if (folder != null)
                {
                    await file.CopyAsync(folder, file.Name, NameCollisionOption.GenerateUniqueName);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog("保存失败(错误原因:" + ex.Message + "),请重试").ShowAsync();
            }

        }
        private async void Save()
        {
            try
            {
                var file = await BuildKeepFile();
                var newfile = await file.CopyAsync(ApplicationData.Current.LocalFolder, file.Name, NameCollisionOption.ReplaceExisting);
                var rd = new LayerReader(file);
                await rd.InitFile();
                await rd.ReadInFolderAsync();
                
            }
            catch (Exception ex)
            {
                await new MessageDialog("保存失败(错误原因:" + ex.Message + "),请重试").ShowAsync();
            }
            /*
            var newfile= await ApplicationData.Current.LocalFolder.GetFileAsync("Miao.cdt");
            var lr = new LayerReader(newfile) { CanvasSize = new Rect(0, 0, CanvasSize.Width, CanvasSize.Height) };
            await lr.InitFile();
            await lr.ReadInFolderAsync();
            Layers.Clear();
            lr.Layers.ForEach(l => Layers.Add(l));
            Layers.ToList().ForEach(l => l.UpdataLayer());
            */

        }
        private async Task<StorageFile> BuildKeepFile()
        {
            var lk = new LayerKeeper(Project.Name, CanvasSize) { BackgroundColor = Colors.White };
            Layers.ToList().ForEach(async layer => await lk.InsetSaveLayer(layer));
            var f = await lk.SaveInFile();
            if (f == null)
                lk.ErrMsg.Add("返回文件为空!\n");
            if(lk.IsError)
                await new MessageDialog(lk.GetErrMsg()).ShowAsync();
            return f;
        }
        private async void DeleteAllCanvas()
        {
            if (await ApplicationHelper.Default.Quire("是否删除所有画布(图层)?"))
            {
                Layers.Clear();
                await NewDefaultLayerCustom();
            }
        }
        private async void DeleteCanvas()
        {
            if (LocLayer != null)
            {
                if (await ApplicationHelper.Default.Quire("是否删除画布(图层):" + LocLayer.Name))
                {
                    Layers.Remove(LocLayer);
                    if (Layers.Count == 0)
                    {
                        await NewDefaultLayerCustom();
                    }
                }
            }
        }
        private void About()
        {
            var navSer = SimpleIoc.Default.GetInstance<DialogService>();
            navSer.ShowMessage("Copyright@ 2017", "关于NaiveInkCanvas");
        }
        private void FullScreen()
        {
            var view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
                view.TryEnterFullScreenMode();
            else
                view.ExitFullScreenMode();
        }
        private void ShareData()
        {
        }
        public void SetProject(ProjectInfoModel project)
        {
            Project = project;
        }
        public void LoadBackgrounds()
        {
        }
        public void LoadStokes()
        {

        }
        public void UpdataCanvas()
        {
            Layers.ToList().ForEach(lm => lm.Canvas.Invalidate());
        }
        public async Task SaveInStreamAsync(IRandomAccessStream randomAccessStream, CanvasBitmapFileFormat canvasBitmapFileFormat = CanvasBitmapFileFormat.Auto)
        {
            var device = CanvasDevice.GetSharedDevice();
            using (var renderTarget = new CanvasRenderTarget(device, (float)CanvasSize.Width, (float)CanvasSize.Height, CanvasDpi))
            {
                using (var ds = renderTarget.CreateDrawingSession())
                {
                    Layers.ToList().ForEach(lm => lm.Canvas_Draw(lm.Canvas, new CanvasDrawEventArgs(ds)));
                }

                await renderTarget.SaveAsync(randomAccessStream, canvasBitmapFileFormat);
            }

        }
        public async Task SaveInFile(string filename)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.GenerateUniqueName);
            var fs = await file.OpenAsync(FileAccessMode.ReadWrite);
            var xmlSer = new XmlSerializer(typeof(List<LayerModel>));
            xmlSer.Serialize(fs.AsStream(), Layers);
        }
    }
}
