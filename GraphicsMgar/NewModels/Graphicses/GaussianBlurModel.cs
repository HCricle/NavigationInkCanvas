using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;
using System.Diagnostics;
using Windows.Graphics.Effects;
using Windows.UI.Input;
using Windows.UI.Input.Inking;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public class GaussianBlurModel : GraphicsRectSizeBase
    {
        public GaussianBlurModel(PenModel penModel, IGraphicsDraw draw)
            : base( GraphicsTypes.GaussianBlur, penModel, draw)
        {
        }
        public override void DrawEffect(IGraphicsEffectSource eff, CanvasDrawingSession session)
        {
            var gb = new GaussianBlurEffect()
            {
                Source=eff,
                BlurAmount=PenAttribute.BlurAmount
            };
            session.DrawImage(gb, Bounds, Bounds);
        }

    }
}
