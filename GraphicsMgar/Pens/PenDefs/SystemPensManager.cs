using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.SystemPens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Pens.PenDefs
{
    /// <summary>
    /// 系统笔集合
    /// </summary>
    public class SystemPensManager : PensManagerBase
    {
        public static readonly string SystemPensBaseName = "系统笔";
        public SystemPensManager(IGraphicsDraw drawer,string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            var manager = RegiestPen(SystemPensBaseName + "_" + name);
            manager.RegiestPen(new SelectPen(drawer, this));
            manager.RegiestPen(new Pencil(drawer, this));
            //manager.RegiestPen(new RectanglePen(drawer, this));
            //manager.RegiestPen(new EclipsePen(drawer, this));
            manager.RegiestPen(new Eraser(drawer, this));
            manager.RegiestPen(new GaussianPen(drawer, this));
            manager.RegiestPen(new EraserGraphics(drawer, this));
            manager.RegiestPen(new TextPen(drawer, this));           
        }
    }
}
