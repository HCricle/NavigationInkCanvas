using NaiveInkCanvas.Model.NewModels.GraphicsBuilder.GraphicsBuilderDefs;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.Histories;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.GraphicsBuilder
{
    public class EraserGraphicsBuildPart:LayerPartBuildBase
    {
        public override Action<InkPoint, PointerPoint> CanvasPointerPressed { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerReleased { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerMoved { get; }

        public override Action<GraphicsTypes> CanvasPenTypeChanged { get; }
        private PenModel OldPenModel { get; set; }
        public EraserGraphicsBuildPart(IGraphicsDraw drawer)
            : base(drawer)
        {
            CanvasPointerMoved = PointerMoved;
        }


        private void PointerMoved(InkPoint  point, PointerPoint p)
        {

            Drawer.GraphicsRects.Where(g => g.PeekIsPointOn(p.Position)).ToList().
                ForEach(g =>
            {
                g.Revocate(Drawer);
                //Drawer.GraphicsRects.Remove(g);
                //Drawer.HistoriesManager.PushHistory(new DeleteGraphicsHistory(Drawer, g));
            });
        }
    }
}
