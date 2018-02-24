using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Input;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using Windows.UI.Input.Inking;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Windows.Graphics.Effects;
using System.Xml.Serialization;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public abstract class GraphicsRectSizeBase:GraphicsBase,IGraphicsBounds,IGraphicsPeekIsPointOn
    {
        public GraphicsRectSizeBase(GraphicsTypes graphics, PenModel penModel,IGraphicsDraw draw)//AFTER:以后再改 
            : base(graphics, penModel,draw, draw as IInkCanvasSharp)
        {
            Points = new List<InkPoint>();
            StrokeStyle = new CanvasStrokeStyle()
            {
                DashCap = CanvasCapStyle.Round,
                LineJoin = CanvasLineJoin.Round,
                StartCap = CanvasCapStyle.Round,
                EndCap = CanvasCapStyle.Round
            };            
        }
        public Rect Bounds
        {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[0];
                var p2 = Points[Points.Count - 1];
                return new Rect(p1.Position, p2.Position);
            }
        }
        public Matrix3x2 Transfrom { get; set; }
        private int rotation;

        public int Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                var bounds = Bounds;
                Transfrom = Matrix3x2.CreateRotation(value,
                    new Vector2((float)(bounds.X+bounds.Width/2), (float)(bounds.Y + bounds.Height / 2)));
            }
        }
        private CanvasCommandList commandList;

        public CanvasCommandList CommandList
        {
            get => commandList;
        }
        public CanvasStrokeStyle StrokeStyle { get; }
        public override bool Move(InkPoint ipoint ,PointerPoint point)
        {
            Debug.Assert(Points != null);
            if (IsSelected && CanOperation())
            {
                var p = point.Position;
                var lst = new List<InkPoint>();
                var b = new InkStrokeBuilder();
                var Stroke = b.CreateStrokeFromInkPoints(Points, new Matrix3x2());
                var x = p.X - Stroke.BoundingRect.Width;
                var y = p.Y - Stroke.BoundingRect.Height;
                //Bounds = new Rect(new Point(),new Size(Stroke.BoundingRect.Width,Stroke.BoundingRect.Height));
                Points.ToList().ForEach(ip =>
                {
                    lst.Add(new InkPoint(new Point(ip.Position.X - x, ip.Position.Y - y), ip.Pressure, ip.TiltX, ip.TiltY, ip.Timestamp));
                });
                //Stroke = canvasSharp.StrokeBuilder.CreateStrokeFromInkPoints(lst, Stroke.PointTransform);
                Drawer.UpdataLayer();
                return true;
            }
            return false;
        }
        public override bool IsPointOn(InkPoint  ipoint, PointerPoint point)
        {
            IsSelected = false;
            return IsSelected = PeekIsPointOn(point.Position);
        }
        public override void BeginDraw(InkPoint point,PointerPoint pointer)
        {
            Points.Add(new InkPoint(new Point(point.Position.X, point.Position.Y), point.Pressure, point.TiltX, point.TiltY, point.Timestamp));
            Rotation = PenAttribute.Rotation;
        }
        public override void Drawing(InkPoint point)
        {
            Points.Add(new InkPoint(new Point(point.Position.X,point.Position.Y),point.Pressure,point.TiltX,point.TiltY,point.Timestamp));
        }
        public bool PeekIsPointOn(Point point)
        {
            Debug.Assert(Bounds != null);
            if (!CanOperation())
                return IsSelected = false;
            var bounds = Bounds;
            return IsSelected=(point.X <= bounds.Right && point.X >= bounds.Left &&
                point.Y <= bounds.Bottom && point.Y >= bounds.Top);
        }
        public override void Redo(IGraphicsDraw drawer, object data)
        {
            drawer.GraphicsRects.Add(this);
            drawer.UpdataLayer();
        }
        public override void Revocate(IGraphicsDraw drawer)
        {
            drawer.GraphicsRects.Remove(this);
            drawer.UpdataLayer();
        }
        public override void DrawEffect(IGraphicsEffectSource eff, CanvasDrawingSession session)
        {

        }
        /// <summary>
        /// 不要重写啊
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="session"></param>
        public override void DrawGraphics(ICanvasResourceCreator creator, CanvasDrawingSession session)
        {
            var cl = commandList = new CanvasCommandList(creator);
            using (var ds=cl.CreateDrawingSession())
            {
                ds.Transform *= Transfrom;
                Draw(creator,ds);
            }
            session.DrawImage(cl);
        }
        public virtual void Draw(ICanvasResourceCreator creator,CanvasDrawingSession session)
        {

        }
        public override void BeginMove(InkPoint iPoint, PointerPoint point)
        {

        }
        
    }
}
