using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Windows.UI.Xaml.Input;
using NaiveInkCanvas.Model.NewModels.GraphicsBuilder.GraphicsBuilderDefs;
using Windows.UI.Xaml;
using Windows.Foundation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.Histories;
using Windows.UI.Input;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using Windows.UI.Input.Inking;

namespace NaiveInkCanvas.Model.NewModels.GraphicsBuilder
{
    public class SelectBuildPart : LayerPartBuildBase
    {
        public override Action<InkPoint, PointerPoint> CanvasPointerPressed { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerReleased { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerMoved { get; }

        private Point TmpPoint;

        public SelectBuildPart(IGraphicsDraw drawer) 
            : base(drawer)
        {
            this.CanvasPointerPressed = PointerPressed;
            this.CanvasPointerMoved = PointerMove;
            this.CanvasPointerReleased = PointerReleased;
        }

        private void PointerReleased(InkPoint point, PointerPoint p)
        {
            Drawer.GraphicsRects.ForEach(gr => 
            {
                if(gr.IsPointOn(point,p))
                {
                    Drawer.HistoriesManager.PushHistory(new MoveGraphicsHistory(Drawer, gr));
                }

            });
        }

        private void PointerMove(InkPoint canvasSharp, PointerPoint p)
        {
            Drawer.GraphicsRects.ForEach(gr => gr.Move(canvasSharp,p));
        }

        private void PointerPressed(InkPoint canvasSharp, PointerPoint p)
        {
            TmpPoint = p.Position;
            Drawer.GraphicsRects.ForEach(g => g.BeginMove(canvasSharp,p));
        }
    }
}
