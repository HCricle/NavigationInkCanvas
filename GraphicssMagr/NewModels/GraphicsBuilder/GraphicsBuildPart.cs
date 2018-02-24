using NaiveInkCanvas.Model.NewModels.GraphicsBuilder.GraphicsBuilderDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Windows.UI.Xaml.Input;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.Histories;
using Windows.UI.Xaml;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using Windows.UI.Input;
using Windows.UI.Input.Inking;

namespace NaiveInkCanvas.Model.NewModels.GraphicsBuilder
{
    public class GraphicsBuildPart : LayerPartBuildBase
    {

        public override Action<InkPoint, PointerPoint> CanvasPointerPressed { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerReleased { get; }

        public override Action<InkPoint, PointerPoint> CanvasPointerMoved { get; }

        private GraphicsRectSizeBase TmpRectGra;

        public GraphicsBuildPart(IGraphicsDraw drawer)
            : base(drawer)
        {
            this.CanvasPointerPressed = PointerPressed;
            this.CanvasPointerMoved = PointerMove;
            this.CanvasPointerReleased = PointerReleased;

        }

        private void PointerReleased(InkPoint point,  PointerPoint pointer)
        {
            //FIXME:关联大小？？？？
            //Drawer.TextFormat = new CanvasTextFormat() { FontSize = Drawer.TextFormat.FontSize };            
        }

        private void PointerMove(InkPoint point, PointerPoint pointer)
        {
            if (TmpRectGra != null)
            {
                TmpRectGra?.Drawing(point);
                //TmpRectGra.PenAttribute.SetLinerPoints(TmpRectGra.Bounds);
            }
        }

        private void PointerPressed(InkPoint point, PointerPoint pointer)
        {
            TmpRectGra = (GraphicsRectSizeBase)GraphicsInstCreateManager.CreateGraphics(Drawer.PenType, Drawer.LocPenModel, Drawer,InkCanvasSharp);
            Drawer.GraphicsRects.Add(TmpRectGra);
            TmpRectGra.BeginDraw(point,pointer);
            //TmpRectGra.PenAttribute.SetLinerPoints(TmpRectGra.Bounds);
            TmpRectGra.IsFill = Drawer.IsFill;
            Drawer.HistoriesManager.PushHistory(new CreateGraphicsHistory(Drawer, TmpRectGra));
            Drawer.LocPenModel = Drawer.LocPenModel.Copy();
            /*
            if (TmpRectGra is GraphicsTextModel)
            {
                var tm = (TmpRectGra as GraphicsTextModel);
                tm.Text = Drawer.Text;
                tm.TextFormat = Drawer.TextFormat;
                
                var ctf = new CanvasTextFormat();
                var propdic = new Dictionary<string, PropertyInfo>();
                foreach (var item in ctf.GetType().GetProperties())
                {
                    propdic.Add(item.Name, item);
                }
                Drawer.TextFormat.GetType().GetProperties().ToList().ForEach(prop =>propdic[prop.Name].SetValue(ctf, prop.GetValue(Drawer.TextFormat)));
               
                //??????
                Drawer.TextFormat = new CanvasTextFormat() { FontSize=Drawer.TextFormat.FontSize};
            }
            */

        }
    }
}
