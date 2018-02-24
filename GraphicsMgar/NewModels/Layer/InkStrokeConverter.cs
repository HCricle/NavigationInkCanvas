using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Layer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Layer
{
    /// <summary>
    /// 将笔迹转换成xml文档
    /// </summary>
    public class InkStrokeConverter
    {
        private static InkStrokeConverter inst=null;
        public static InkStrokeConverter Inst => inst ?? (inst = new InkStrokeConverter());
        
        private Dictionary<GraphicsTypes,Func<InkStrokeConverterArgs, CanvasData>> Converters { get; }
        public event Action<InkStrokeConverterArgs> UnHandleConverte;
        private InkStrokeConverter()
        {
            Converters = new Dictionary<GraphicsTypes, Func<InkStrokeConverterArgs, CanvasData>>
            {
                { GraphicsTypes.CurveLine, LineConverter },
                { GraphicsTypes.StraightLine,LineConverter },
                { GraphicsTypes.Eraser,LineConverter},
                { GraphicsTypes.GaussianBlur,GaussianBlurConverter}
            };
        }
        public CanvasData LineConverter(InkStrokeConverterArgs args)
        {
            var sk = args.StrokeBuilder.CreateStrokeFromInkPoints(args.Graphics.Points, args.PenAttribute.PenTipTransform);
            sk.StrokeDuration = new TimeSpan((long)args.Graphics.Graphics);
            sk.DrawingAttributes = PenModel.ToInkDrawingAttributes(args.Graphics.PenAttribute);
            args.Canvas.InkPresenter.StrokeContainer.AddStroke(sk);
            return null;
        }
        public CanvasData GaussianBlurConverter(InkStrokeConverterArgs args)
        {
            var cd = new CanvasData("GaussianBlur") { DataTypeString="Effect"};    
            var gra=args.Graphics as GaussianBlurModel;
            cd.InsertData(nameof(gra.PenAttribute.BlurAmount), gra.PenAttribute.BlurAmount);
            return cd;
        }
        public CanvasData ExeistConver(GraphicsTypes types,InkStrokeConverterArgs args)
        {
            try
            {
                return Converters[types]?.Invoke(args);
            }
            catch (Exception)
            {
                UnHandleConverte?.Invoke(args);
                return null;
            }
        }
    }
}
