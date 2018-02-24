using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Helpers.DefHelper
{
    public static class PaintingModelHelper
    {
        public static Dictionary<PaintingModels,string> ModelStrings { get; }
        static PaintingModelHelper()
        {
            ModelStrings = new Dictionary<PaintingModels, string>();
            ModelStrings.Add(PaintingModels.BackgroundPainting, "背景编辑模式");
            ModelStrings.Add(PaintingModels.LayerPainting, "图层编辑模式");
        }
    }
    public enum PaintingModels
    {
        LayerPainting,
        BackgroundPainting
    }
}
