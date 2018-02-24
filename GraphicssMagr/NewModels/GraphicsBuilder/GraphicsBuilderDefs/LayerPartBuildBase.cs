using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.GraphicsBuilder.GraphicsBuilderDefs
{
    public abstract class LayerPartBuildBase: ILayerPartBuild
    {
        public IGraphicsDraw Drawer { get; }
        public IInkCanvasSharp InkCanvasSharp => Drawer as IInkCanvasSharp;//AFTER：以后再改
        public abstract Action<InkPoint, PointerPoint> CanvasPointerPressed { get; }
        public abstract Action<InkPoint, PointerPoint> CanvasPointerReleased { get; }
        public abstract Action<InkPoint, PointerPoint> CanvasPointerMoved { get; }
        public Action<CanvasControl, CanvasDrawEventArgs> CanvasDraw { get; }
        public virtual Action<GraphicsTypes> CanvasPenTypeChanged { get; }        
        public LayerPartBuildBase(IGraphicsDraw drawer)
        {
            Drawer = drawer;
            CanvasDraw = Draw;

        }
        protected virtual void Draw(CanvasControl canvas, CanvasDrawEventArgs e)
        {
            e.DrawingSession.Clear(Drawer.BackgroundColor);
            var cl = new CanvasCommandList(canvas);
            using (var ds = cl.CreateDrawingSession())
            {
                try
                {
                    Drawer.GraphicsRects.ForEach(gr => gr.DrawGraphics(canvas, ds));
                }
                catch (Exception) { }
            }
            Drawer.GraphicsRects.ForEach(gr => gr.DrawEffect(cl, e.DrawingSession));
            e.DrawingSession.DrawImage(cl);
        }
    }
}
