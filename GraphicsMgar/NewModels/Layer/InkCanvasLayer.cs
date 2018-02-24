using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.Layer
{
    /// <summary>
    /// 使用InkCanvas过度，接受的所有输入不由墨水画版处理，
    /// 都由本类事件派发出去，可以使用转换模式来转换处理模式
    /// </summary>
    public abstract class InkCanvasLayer: IInkCanvasSharp
    {
        public InkCanvas TopCanvas { get; }

        public InkStrokeBuilder StrokeBuilder { get;}
        #region Events
        public event Action<InkPoint,PointerPoint> BeginDraw;
        public event Action<InkPoint,PointerPoint> Drawing;
        public event Action<InkPoint, PointerPoint> Drawed;
        public event Action<PointerPoint> PointerHovered;
        public event Action<PointerPoint> PointerLost;
        public event Action<KeyRoutedEventArgs> KeyDown;
        #endregion
        public InkCanvasLayer()
        {
            TopCanvas = new InkCanvas();
            StrokeBuilder = new InkStrokeBuilder();
            TopCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch;
            ChangeCanvasModel(true);
            TopCanvas.InkPresenter.UpdateDefaultDrawingAttributes(new InkDrawingAttributes()
            {
                Color=Colors.Black
            });
            TopCanvas.InkPresenter.UnprocessedInput.PointerPressed += UnprocessedInput_PointerPressed;
            TopCanvas.InkPresenter.UnprocessedInput.PointerMoved += UnprocessedInput_PointerMoved;
            TopCanvas.InkPresenter.UnprocessedInput.PointerReleased += UnprocessedInput_PointerReleased;
            TopCanvas.InkPresenter.UnprocessedInput.PointerHovered += UnprocessedInput_PointerHovered;
            TopCanvas.InkPresenter.UnprocessedInput.PointerLost += UnprocessedInput_PointerLost;
            TopCanvas.KeyDown += TopCanvas_KeyDown;
        }
        private void TopCanvas_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            KeyDown?.Invoke(e);
        }

        private void UnprocessedInput_PointerLost(InkUnprocessedInput sender, PointerEventArgs args)
        {
            PointerLost?.Invoke(args.CurrentPoint);
        }

        private void UnprocessedInput_PointerHovered(InkUnprocessedInput sender, PointerEventArgs args)
        {
            PointerHovered?.Invoke(args.CurrentPoint);
        }

        public void ChangeCanvasModel(bool iswin2d)
        {
            if (iswin2d)
            {
                TopCanvas.InkPresenter.InputProcessingConfiguration.RightDragAction = InkInputRightDragAction.LeaveUnprocessed;
                TopCanvas.InkPresenter.InputProcessingConfiguration.Mode = InkInputProcessingMode.None;
            }
            else
            {
                TopCanvas.InkPresenter.InputProcessingConfiguration.RightDragAction = InkInputRightDragAction.AllowProcessing;
                TopCanvas.InkPresenter.InputProcessingConfiguration.Mode = InkInputProcessingMode.Inking;
            }
        }
        private void UnprocessedInput_PointerReleased(InkUnprocessedInput sender, PointerEventArgs args)
        {
            Drawed?.Invoke(BuildInkPoint(args.CurrentPoint), args.CurrentPoint);
        }

        private void UnprocessedInput_PointerMoved(InkUnprocessedInput sender, PointerEventArgs args)
        {
            //FIXME:第二个图层后或者重新载入文件后无法画画
            Drawing?.Invoke(BuildInkPoint(args.CurrentPoint), args.CurrentPoint);
        }
        private void UnprocessedInput_PointerPressed(InkUnprocessedInput sender, PointerEventArgs args)
        {
            BeginDraw?.Invoke(BuildInkPoint(args.CurrentPoint), args.CurrentPoint);
        }
        private InkPoint BuildInkPoint(PointerPoint pointer)
        {
            var props = pointer.Properties;
            return new InkPoint(new Point(pointer.Position.X, pointer.Position.Y), props.Pressure, props.XTilt, props.YTilt, pointer.Timestamp);
        }
    }
}
