using NaiveInkCanvas.Model.NewModels.GraphicsBuilder.GraphicsBuilderDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Model.NewModels.Graphicses;
using Windows.UI.Xaml;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using Windows.UI;
using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.Histories;
using Windows.UI.Input;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using Windows.Foundation;
using System.Diagnostics;
using Windows.UI.Input.Inking;

namespace NaiveInkCanvas.Model.NewModels.GraphicsBuilder
{
    public class EraserBuildPart : LayerPartBuildBase
    {
        private GraphicsRectSizeBase TmpRectGra;

        public override Action<InkPoint, PointerPoint> CanvasPointerPressed { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerReleased { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerMoved { get; }

        public override Action<GraphicsTypes> CanvasPenTypeChanged { get; }
        private PenModel OldPenModel { get; set; }
        public EraserBuildPart(IGraphicsDraw drawer)
            : base(drawer)
        {
            CanvasPointerPressed = PointerPressed;
            CanvasPointerMoved = PointerMoved;
            CanvasPenTypeChanged = PenTypeChanged;
        }

        private void PenTypeChanged(GraphicsTypes obj)
        {
            if (obj== GraphicsTypes.Eraser)
            {
                OldPenModel = Drawer.LocPenModel.Copy();
            }
            else if(obj!= GraphicsTypes.EraserGraphics)
            {
                Drawer.LocPenModel = OldPenModel;
            }
        }

        private void PointerMoved(InkPoint point, PointerPoint p)
        {
            if (TmpRectGra != null)
            {
                TmpRectGra?.Drawing(point);
                //TmpRectGra.PenAttribute.SetLinerPoints(GetBounds(TmpRectGra));
            }
        }
        private Rect GetBounds(GraphicsRectSizeBase graphicsRectSize)
        {
            Debug.Assert(graphicsRectSize.Points.Count > 0);
            var p1 = graphicsRectSize.Points[0];
            var p2 = graphicsRectSize.Points[graphicsRectSize.Points.Count - 1];
            return new Rect(p1.Position, p2.Position);
        }
        private void PointerPressed(InkPoint  point, PointerPoint p)
        {
            Drawer.LocPenModel.SetSolidColorBrush(Colors.White);//可以为背景颜色           
            TmpRectGra = (GraphicsRectSizeBase)GraphicsInstCreateManager.CreateGraphics( GraphicsTypes.CurveLine, Drawer.LocPenModel, Drawer,InkCanvasSharp);
            TmpRectGra.IsLock = true;
            Drawer.GraphicsRects.Add(TmpRectGra);
            TmpRectGra.BeginDraw(point,p);       
            Drawer.HistoriesManager.PushHistory(new CreateGraphicsHistory(Drawer, TmpRectGra));
            Drawer.LocPenModel =Drawer.LocPenModel.Copy();
        }
    }
}
