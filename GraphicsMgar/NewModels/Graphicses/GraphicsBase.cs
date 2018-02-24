using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Input;
using Microsoft.Graphics.Canvas;
using System.Xml.Serialization;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using Windows.UI.Input.Inking;
using System.Numerics;
using System.Diagnostics;
using Windows.Graphics.Effects;
using System.Xml;
using System.Xml.Schema;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public abstract class GraphicsBase : IGraphicsOperator,IGraphicsCanDraw,IGraphicsType, IGraphicsDrawToLayer,IGraphicsOperation
    {
        public GraphicsBase(GraphicsTypes graphics, PenModel penModel,IGraphicsDraw draw,IInkCanvasSharp canvasSharp)
        {
            Graphics = graphics;
            Drawer = draw;
            InkCanvasSharp = canvasSharp;
            PenAttribute = penModel;
            IsFill = false;
            IsLock = false;
            IsSelected = false;
            IsAppear = true;
        }


        public bool IsFill { get; set; }
        public bool IsAppear { get; set; }
        public bool IsSelected { get; set; }
        public PenModel PenAttribute { get; set; }
        public bool IsLock { get; set; }
        public GraphicsTypes Graphics { get;}
        public IGraphicsDraw Drawer { get;  }
        public IInkCanvasSharp InkCanvasSharp { get; }
        public List<InkPoint> Points { get; set; }
        public Matrix3x2 PenMatrix
        {
            get
            {
                var penattr = InkCanvasSharp.TopCanvas.InkPresenter.CopyDefaultDrawingAttributes();
                Debug.Assert( penattr!= null);
                return InkCanvasSharp.TopCanvas.InkPresenter.CopyDefaultDrawingAttributes().PenTipTransform;
            }
        }
        public bool CanOperation()
        {
            return !IsLock;
        }
        public virtual bool IsPointOn(InkPoint ipoint,PointerPoint point)
        {
            return false;
        }
        public virtual bool Move(InkPoint  iPoint,PointerPoint point)
        {
            return false;
        }
        /// <summary>
        /// 当用户发出查询是否为空数据（CanRemove）时，如果返回真
        /// 查询发起方法讲去除此图形
        /// </summary>
        /// <returns></returns>
        public virtual bool CanRemove()
        {
            return false;
        }

        public abstract void DrawGraphics(ICanvasResourceCreator creator, CanvasDrawingSession session);
        public abstract void DrawEffect(IGraphicsEffectSource eff, CanvasDrawingSession session);

        public abstract void BeginDraw(InkPoint  point, PointerPoint pointer);

        public abstract void Drawing(InkPoint  point);
        public abstract void Revocate(IGraphicsDraw drawer);
        public abstract void Redo(IGraphicsDraw drawer, object data);
        public abstract void BeginMove(InkPoint ipoint,PointerPoint point);

        public virtual void KeyDown(KeyRoutedEventArgs args)
        {
        }
    }
}
