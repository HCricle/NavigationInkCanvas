using NaiveInkCanvas.Model.NewModels.GraphicsBuilder;
using NaiveInkCanvas.Model.NewModels.GraphicsBuilder.GraphicsBuilderDefs;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System.Collections.Generic;

namespace NaiveInkCanvas.Model.NewModels
{
    public class LayerPartManager
    {
        public Dictionary<GraphicsTypes, ILayerPartBuild> GraphicsBuildCollection { get; }
        public IGraphicsDraw Drawer { get; }
        public LayerPartManager(IGraphicsDraw draw)
        {
            Drawer = draw;
            GraphicsBuildCollection = new Dictionary<GraphicsTypes, ILayerPartBuild>();
            InitGraphicsBuilds();
        }
        private void InitGraphicsBuilds()
        {
            GraphicsBuildCollection.Add(GraphicsTypes.SelectObject, new SelectBuildPart(Drawer));
            var gbp = new GraphicsBuildPart(Drawer);
            GraphicsBuildCollection.Add(GraphicsTypes.Text, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.Circle, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.Ellipse, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.Rectangle, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.CurveLine, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.RoundRectangle, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.StraightLine, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.GaussianBlur, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.Scan, gbp);
            GraphicsBuildCollection.Add(GraphicsTypes.Eraser, new EraserBuildPart(Drawer));
            GraphicsBuildCollection.Add(GraphicsTypes.EraserGraphics, new EraserGraphicsBuildPart(Drawer));
        }
    }
}
