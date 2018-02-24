using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines
{
    public enum GraphicsTypes:long
    {
        OtherGraphics=0,//其它图形
        Circle,
        Ellipse,
        CurveLine,
        Rectangle,
        RoundRectangle,
        Text,
        StraightLine,
        EraserGraphics,
        Eraser,
        GaussianBlur,
        Image,
        Scan,
        SelectRectangle,//没实现
        SelectObject
    }
}
