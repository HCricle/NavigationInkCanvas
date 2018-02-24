using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.HistoryDefs;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.Histories
{
    class CreateGraphicsHistory : GraphicsHistory
    {
        public CreateGraphicsHistory(IGraphicsDraw drawer, GraphicsBase graphics)
        {
            this.Description = "创建图形";
            Graphics = graphics;
            Drawer = drawer;
            if (graphics is GraphicsLineModel)
            {
                this.Revocation = () =>
                {
                    var glm = (Graphics as GraphicsLineModel);
                    glm.Revocate(Drawer);
                };
                this.Redo = () =>
                {
                    var glm = (Graphics as GraphicsLineModel);
                    glm.Redo(Drawer, null);
                };
            }
            else
            {
                this.Revocation = () => (Graphics as GraphicsBase).Revocate(drawer);
                this.Redo = () => (Graphics as GraphicsBase).Redo(drawer, graphics);
            }
        }
    }
    class DeleteGraphicsHistory: GraphicsHistory
    {
        public DeleteGraphicsHistory(IGraphicsDraw drawer, GraphicsBase graphics)
        {
            this.Description = "删除图形";
            Graphics = graphics;
            Drawer = drawer;
            this.Revocation = () => (Graphics as GraphicsBase).Redo(drawer, graphics);
            this.Redo = () => (Graphics as GraphicsBase).Revocate(drawer);
        }
    }
    class MoveGraphicsHistory:GraphicsHistory
    {
        private Rect OldBounds;
        public MoveGraphicsHistory(IGraphicsDraw drawer,GraphicsBase graphics)
        {
            this.Description = "移动图形";
            Graphics = graphics;
            Drawer = drawer;
            this.Revocation = () => OldBounds = (graphics as GraphicsRectSizeBase).Bounds;
            //this.Redo = () => (graphics as GraphicsRectSizeBase).Bounds = OldBounds;
        }
    }
}
