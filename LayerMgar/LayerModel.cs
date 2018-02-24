using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.Histories;
using NaiveInkCanvas.Model.NewModels.EventArgs;
using NaiveInkCanvas.Model.NewModels.Layer;
using Windows.UI.Input.Inking;
using NaiveInkCanvas.Background;
using NaiveInkCanvas.Controls;

namespace NaiveInkCanvas.Model.NewModels
{
    public class LayerModel: InkCanvasLayer,ILayerCanvas, IGraphicsDraw,ILayerCanSave
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {                    
                    return;
                }
                name = value;
                NameChanged?.Invoke(this, value);
            }
        }
        public CanvasControl Canvas { get; }
        public HistoryManager HistoriesManager { get; }
        public LayerPartManager PartManager { get; }
        public BackgroundManager BckManager { get; }
        public Color BackgroundColor { get; set; }
        public CanvasStrokeStyle StrokeStyle { get; set; }
        //Graphicses include texts
        public List<GraphicsRectSizeBase> GraphicsRects { get; }
        public CanvasFigureLoop LocFigureLoop { get; set; }
        public Rect CanvasSize { get; set; }//画布大小
        public Rect SelectCanvasSize { get; set; }//选取画
        public bool IsFill { get; set; }
        public string Text { get; set; }
        public PenAttributesControl PenControl { get; }
        private bool isLock;

        public bool IsLock
        {
            get => isLock;
            set
            {
                isLock = value;
                LockChanged?.Invoke(this, value);
            }
        }

        public bool IsStartPointCenter { get; set; }
        public bool IsOtherGraphics { get; set; }
        private bool isAppear;

        public bool IsAppear
        {
            get => isAppear;
            set
            {
                isAppear = value;
                AppearChanged?.Invoke(this, value);
            }
        }
        

        public PenModel LocPenModel { get; set; }
        public CanvasTextFormat TextFormat { get; set; }
        public StrokeRectangle SeletedRectangle { get; }
        private GraphicsTypes penType;

        public GraphicsTypes PenType
        {
            get => penType;
            set
            {
                penType = value;
                PenChanged?.Invoke(this, value);
            }
        }
        public event Action<CanvasControl, CanvasDrawEventArgs> LayerReDraw;
        public event Action<IGraphicsDraw, LayerPointerEventArgs> LayerPointerPressed;
        public event Action<IGraphicsDraw, LayerPointerEventArgs> LayerPointerRelaesed;
        public event Action<IGraphicsDraw, LayerPointerEventArgs> LayerPointerMove;
        public event Action<LayerModel, GraphicsTypes> PenChanged;
        public event Action<PenModelChangedEventArgs> PenModelChanged;
        public event Action<LayerModel,string> NameChanged;
        public event Action<LayerModel,bool> AppearChanged;
        public event Action<LayerModel,bool> LockChanged;
        public LayerModel()
        {
            Name = "未命名";
            PartManager = new LayerPartManager(this);
            Canvas = new CanvasControl()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            LocFigureLoop = CanvasFigureLoop.Open;

            GraphicsRects = new List<GraphicsRectSizeBase>();

            HistoriesManager = new HistoryManager();
            BckManager = BackgroundManager.Inst;
            LocPenModel = new PenModel();
            SeletedRectangle = new StrokeRectangle();
            TextFormat = new CanvasTextFormat();
            PenControl = new PenAttributesControl(this)
            {
                Width = 300,
                Height = 750
            };

            BackgroundColor = Colors.Transparent;
            IsAppear = true;
            IsOtherGraphics = false;
            IsFill = true;
            PenType = GraphicsTypes.SelectObject;

            Canvas.Draw += Canvas_Draw;
            //Canvas.PointerMoved += Canvas_PointerMoved;
            //Canvas.PointerPressed += Canvas_PointerPressed;
            //Canvas.PointerReleased += Canvas_PointerReleased;
            //Canvas.KeyDown += Canvas_KeyDown;
            Drawing += LayerModel_Drawing;
            Drawed += LayerModel_Drawed;
            BeginDraw += LayerModel_BeginDraw;
            PenChanged += LayerModel_PenChanged;
            //SeletedRectangle.RectangleDirectionChanged += SeletedRectangle_RectangleDirectionChanged;
            //SeletedRectangle.RectangleRectChanged += SeletedRectangle_RectangleRectChanged;
            //SeletedRectangle.RectangleStartPointChanged += SeletedRectangle_RectangleStartPointChanged;

            //PenChanged += (o, e) => 
            //  PartManager.GraphicsBuildCollection.ToList().ForEach(gbc => gbc.Value.CanvasPenTypeChanged?.Invoke(PenType));
        }
        public LayerModel(Rect bounds)
            :this()
        {
            CanvasSize = bounds;
        }
      
        private PenModel OldPen;
        private void LayerModel_PenChanged(LayerModel arg1, GraphicsTypes arg2)
        {
            if(arg2== GraphicsTypes.Eraser)
            {
                OldPen = LocPenModel.Copy();
            }
            else if(OldPen!=null)
            {
                LocPenModel = OldPen;
            }
        }

        private void LayerModel_BeginDraw(InkPoint arg1, PointerPoint p)
        {
            if (!IsLock && !IsOtherGraphics)
            {
                if (p.Properties.IsLeftButtonPressed)
                {
                    PartManager.GraphicsBuildCollection[PenType].CanvasPointerPressed?.Invoke(arg1, p);
                    if (PenType == GraphicsTypes.SelectRectangle)
                    {
                        var recRect = SeletedRectangle.LocalRect;
                        if (!(recRect.Left < p.Position.X && recRect.Right > p.Position.X
                            && recRect.Top < p.Position.Y && recRect.Bottom > p.Position.Y))
                        {
                            SeletedRectangle.CanOperation = false;
                            SeletedRectangle.LocalStartPoint = p.Position;
                            SeletedRectangle.LocalSize = new Size(0, 0);
                        }
                    }
                    else
                    {
                        PenModelChanged?.Invoke(new PenModelChangedEventArgs(LocPenModel));
                    }
                }
            }
            LayerPointerPressed?.Invoke(this, new LayerPointerEventArgs(p, true));
            Canvas.Invalidate();
        }

        private void LayerModel_Drawed(InkPoint arg1, PointerPoint p)
        {
            if (!IsLock && !IsOtherGraphics)
            {
                if (p.Properties.IsLeftButtonPressed)
                {
                    PartManager.GraphicsBuildCollection[PenType].CanvasPointerReleased?.Invoke(arg1, p);
                }
            }
            LayerPointerRelaesed?.Invoke(this, new LayerPointerEventArgs(p, !IsOtherGraphics));
            Canvas.Invalidate();
        }

        private void LayerModel_Drawing(InkPoint arg1, PointerPoint point)
        {

            if (!IsLock && !IsOtherGraphics)
            {
                if (point.Properties.IsLeftButtonPressed)
                {
                    PartManager.GraphicsBuildCollection[PenType].CanvasPointerMoved?.Invoke(arg1, point);
                    if (PenType == GraphicsTypes.SelectRectangle)
                    {
                        var x = Math.Abs(SeletedRectangle.LocalStartPoint.X - point.Position.X);
                        var y = Math.Abs(SeletedRectangle.LocalStartPoint.Y - point.Position.Y);
                        SeletedRectangle.LocalSize = new Size(x, y);
                    }
                }
            }
            LayerPointerMove?.Invoke(this, new LayerPointerEventArgs(point, !IsOtherGraphics));
            Canvas.Invalidate();
        }

        private void Canvas_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key== VirtualKey.Delete)
            {
                var tlst = GraphicsRects.Where(g => g.IsSelected);
                foreach (var gra in tlst)
                {
                    HistoriesManager.PushHistory(new DeleteGraphicsHistory(this, gra));
                    GraphicsRects.Remove(gra);
                }
            }
            else if(e.Key== VirtualKey.F5)
            {
                Canvas.Invalidate();
            }
            else if (e.Key== VirtualKey.Back)
            {
                HistoriesManager.PopHistory().Revocation();
            }
        }
        
        

        public void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (IsAppear)
            {
                this.GraphicsRects.ForEach(gr => PartManager.GraphicsBuildCollection[PenType].CanvasDraw?.Invoke(sender, args));
                //PartManager.GraphicsBuildCollection[PenType].CanvasDraw?.Invoke(sender, args);
                LayerReDraw?.Invoke(sender, args);                
            }            
        }
        private bool IsPointInSelected(Point point)
        {
            Debug.Assert(SelectCanvasSize != null);
            return point.X >= SelectCanvasSize.Left && point.X <= SelectCanvasSize.Right &&
                    point.Y >= SelectCanvasSize.Top &&point.Y<= SelectCanvasSize.Bottom;
        }
        private async Task<Canvas> GetGraphiceImage(Point point)
        {
            var memlst = await GetPointGraphicses(point);
            var c = new Canvas()
            {
                Height = 150,
                Width = 150
            };
            foreach (var mem in memlst)
            {
                var img = new Image()
                {
                    Height = 150,
                    Width = 150
                };
                var bitmap = new BitmapImage();
                bitmap.SetSource(mem);
                img.Source = bitmap;
                c.Children.Add(img);
            }
            return c;

        }
       
        private async Task<IList<IRandomAccessStream>> GetPointGraphicses(Point point)
        {
            var rlst = GraphicsRects.Where(g => g.PeekIsPointOn(point));
            var size = new Size(150, 150);
            var memlst = new List<IRandomAccessStream>();
            foreach (var g in rlst)
            {
                var memory = new InMemoryRandomAccessStream();
                await SaveGraphicsInStream(memory, g, size);
                memlst.Add(memory);
            }
            return memlst;
        }
        public override string ToString()
        {
            return Name;
        }
        private async Task SaveGraphicsInStream(IRandomAccessStream stream,GraphicsBase graphics,Size size)
        {
            await SaveInRandomAccessStream(stream, size, 
                (res, ds) => graphics.DrawGraphics(res, ds),
                CanvasBitmapFileFormat.Png);
        }
        public async Task SaveInRandomAccessStream(IRandomAccessStream stream, Size canvasSize,Action<ICanvasResourceCreator,CanvasDrawingSession> draw, CanvasBitmapFileFormat canvasBitmapFile = CanvasBitmapFileFormat.Auto, float dpi = 98f)
        {
            var device = CanvasDevice.GetSharedDevice();
            var renderTarget = new CanvasRenderTarget(device, (float)canvasSize.Width, (float)canvasSize.Height, dpi);
            using (var ds = renderTarget.CreateDrawingSession())
            {
                //Canvas_Draw(Canvas, new CanvasDrawEventArgs(ds));
                draw?.Invoke(device, ds);
            }
            await renderTarget.SaveAsync(stream, canvasBitmapFile);
        }

        public async Task SaveInMemory(IRandomAccessStream stream, Size canvasSize, float dpi = 98)
        {
            await SaveInRandomAccessStream(stream, canvasSize,(res,ds)=> Canvas_Draw(Canvas, new CanvasDrawEventArgs(ds)), CanvasBitmapFileFormat.Png, dpi);
        }
        public void UpdataLayer() => Canvas.Invalidate();
    }
    public enum BrushTypes
    {
        RadialColorBrush,
        SolidColorBrush,
        LinearColorBrush
    }
}
