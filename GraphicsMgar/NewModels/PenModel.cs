using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;
using System.Numerics;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Input.Inking;
using Microsoft.Graphics.Canvas.Effects;

namespace NaiveInkCanvas.Model.NewModels
{
    [XmlRoot]
    public class PenModel : ICanSetBrush
    {
        //[XmlIgnore]
        //public CanvasStrokeStyle StrokeStyle { get; set; }
        public float StrokeWidth { get; set; }
        //public CanvasGradientStop[] CanvasColorStops { get; internal set; }
        //public BrushTypes BrushType { get; internal set; }
        public Color SelectColor { get; internal set; }
        //public Vector2 StartPoint { get; set; }
        public int Rotation { get; set; }
        //public Vector2 EndPoint { get; set; }
        //[XmlIgnore]
        public float BlurAmount { get; set; }
        public static readonly CanvasStrokeStyle DefaultCanvasStrokeStyle
            = new CanvasStrokeStyle()
            {
                DashCap = CanvasCapStyle.Round,
                DashStyle = CanvasDashStyle.Solid,
                EndCap = CanvasCapStyle.Round,
                LineJoin = CanvasLineJoin.Round,
                MiterLimit = 1f,
                StartCap = CanvasCapStyle.Round,
                TransformBehavior = CanvasStrokeTransformBehavior.Fixed
            };
        public static readonly float DefaultStrokeWidth = 1f;

        public ICanvasBrush GetBrush(ICanvasResourceCreator canvasResourceCreator)
        {
            //ICanvasBrush Brush;
            //if (BrushType == BrushTypes.SolidColorBrush)
            var Brush = new CanvasSolidColorBrush(canvasResourceCreator, SelectColor);
            //else if (BrushType == BrushTypes.LinearColorBrush)
            //{
            //    Brush = new CanvasLinearGradientBrush(canvasResourceCreator, CanvasColorStops);
            //    (Brush as CanvasLinearGradientBrush).StartPoint=StartPoint;
            //    (Brush as CanvasLinearGradientBrush).EndPoint = EndPoint;
            //}
            //else
            //    Brush = new CanvasRadialGradientBrush(canvasResourceCreator, CanvasColorStops);
            return Brush;
        }
        /*
        public void SetLinearColorBrush(params CanvasGradientStop[] canvasGradientStops)
        {
            //Brush = new CanvasLinearGradientBrush(Canvas, canvasGradientStops);
            CanvasColorStops = canvasGradientStops;
            BrushType = BrushTypes.LinearColorBrush;
        }
        */
        
        public void SetSolidColorBrush(Color color)
        {
            //FIXME:ArgumentException in Device           
            //Brush = new CanvasSolidColorBrush(Canvas,color);
            SelectColor = color;
            //BrushType = BrushTypes.SolidColorBrush;
        }
        /*
        public void SetRadialColorBrush(params CanvasGradientStop[] canvasGradientStops)
        {
            //Brush = new CanvasRadialGradientBrush(Canvas,canvasGradientStops);
            CanvasColorStops = canvasGradientStops;
            BrushType = BrushTypes.RadialColorBrush;
        }
        */
        public PenModel()
        {            
            //StrokeStyle = DefaultCanvasStrokeStyle;
            SetSolidColorBrush(Colors.White);
            /*
            BlurAmount = new GaussianBlurEffect()
            {
                BorderMode = EffectBorderMode.Soft,
                Optimization = EffectOptimization.Speed,
                BlurAmount = 10f
            };
            */
            StrokeWidth = 1f;
        }
        /*
        public void SetLinerPoints(Rect rect)
        {
            StartPoint = new Vector2((float)rect.Left, (float)rect.Top);
            EndPoint = new Vector2((float)rect.Right, (float)rect.Bottom);
        }
        */
        public PenModel Copy()
        {
            var pm = new PenModel
            {
                //BrushType = BrushType,
                /*
                CanvasColorStops = CanvasColorStops?.Select(ccs => new CanvasGradientStop()
                {
                    Color = new Color()
                    {
                        R = ccs.Color.R,
                        G = ccs.Color.G,
                        A = ccs.Color.A,
                        B = ccs.Color.B
                    },
                    Position = ccs.Position
                }).ToArray(),
                */
                StrokeWidth = StrokeWidth,
                BlurAmount = BlurAmount,
                Rotation = Rotation,
                //StartPoint = new Vector2(StartPoint.X, StartPoint.Y),
                //EndPoint = new Vector2(StartPoint.X, StartPoint.Y),
                /*
                BlurAmount = new GaussianBlurEffect()
                {
                    BlurAmount = BlurAmount.BlurAmount,
                    BorderMode = BlurAmount.BorderMode,
                    BufferPrecision = BlurAmount.BufferPrecision,
                    CacheOutput = BlurAmount.CacheOutput,
                    Optimization = BlurAmount.Optimization
                },*/
                SelectColor = new Color()
                {
                    R = SelectColor.R,
                    G = SelectColor.G,
                    A = SelectColor.A,
                    B = SelectColor.B
                }/*,
                StrokeStyle = new CanvasStrokeStyle()
                {
                    CustomDashStyle = (from data in StrokeStyle.CustomDashStyle select data).ToArray(),
                    DashCap = StrokeStyle.DashCap,
                    DashOffset = StrokeStyle.DashOffset,
                    DashStyle = StrokeStyle.DashStyle,
                    EndCap = StrokeStyle.EndCap,
                    LineJoin = StrokeStyle.LineJoin,
                    MiterLimit = StrokeStyle.MiterLimit,
                    StartCap = StrokeStyle.StartCap,
                    TransformBehavior = StrokeStyle.TransformBehavior                    
                }
                */
            };
            pm.StrokeWidth = pm.StrokeWidth;            
            return pm;
        }      

        public static InkDrawingAttributes ToInkDrawingAttributes(PenModel pen,XmlDocument xmldoc=null)
        {
            return new InkDrawingAttributes()
            {
                Size=new Size(pen.StrokeWidth,pen.StrokeWidth)         ,
                Color=pen.SelectColor,

            };
        }
        public static PenModel ToPenModel(InkDrawingAttributes drawingAttributes,XmlDocument xmldoc=null)
        {
            return new PenModel()
            {
                SelectColor = drawingAttributes.Color,
                StrokeWidth = (float)drawingAttributes.Size.Width
            };
        }
    }
}
