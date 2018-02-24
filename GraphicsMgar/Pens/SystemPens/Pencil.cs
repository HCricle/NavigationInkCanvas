using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.PenPart;
using System.Reflection;
using Windows.Foundation;
using NaiveInkCanvas.Model.NewModels;
using NaiveInkCanvas.Converter;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using Windows.UI.Xaml.Input;
using Microsoft.Graphics.Canvas.Geometry;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class Pencil : PenBase
    {
        public Pencil(IGraphicsDraw drawer,PensManagerBase manager) 
            : base(drawer, manager, "铅笔")
        {
            #region BuildPenAttritube
            //drawer.LocPenModel.StrokeStyle.
            PenParts.Add(new SliderPenPart(drawer, "LocPenModel.StrokeWidth", "笔宽", new Rect(0, 0, 100, 100)) { ValueConverter = new FloatDoubleConverter() });
            PenParts.Add(new ToggerPenPart(drawer, "IsFill", "填充"));
            //PenParts.Add(new EnumSelectorPenPart(drawer, "LocFigureLoop", "闭合", new EnumStringConverter<CanvasFigureLoop>(), typeof(CanvasFigureLoop)));
            #endregion
        }

        public override Action PenChanged => () => Drawer.PenType = GraphicsTypes.CurveLine;
    }
}
