using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.CanvasPartControls.BaseControl
{
    public class CanMoveMoverManager
    {
        public FrameworkElement OtherControl { get; private set; }
        public FrameworkElement CanMoveControl { get;private set; }
        public bool IsInit { get;internal set; }
        public CanMoveMoverManager(FrameworkElement otherControl)
        {
            OtherControl = otherControl;
        }
        public void InitCanMoveControl(FrameworkElement canMoveControl)
        {
            CanMoveControl = canMoveControl;
            CanMoveControl.PointerPressed += CanMoveControl_PointerPressed;
            CanMoveControl.PointerMoved += CanMoveControl_PointerMoved;
            IsInit = true;
        }
        private Point TmpPoint;
        private void CanMoveControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(sender as UIElement).Position;
            TmpPoint = new Point(point.X, point.Y);
        }

        public void UnInitCanMoveControl()
        {
            CanMoveControl.PointerMoved -= CanMoveControl_PointerMoved;
            IsInit = false;
        }
        private void CanMoveControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (sender is FrameworkElement uie)
            {
                var point = e.GetCurrentPoint(uie);
                if (point.Properties.IsLeftButtonPressed)
                {
                    var x = Canvas.GetLeft(OtherControl);
                    var y = Canvas.GetTop(OtherControl);
                    x += point.Position.X - TmpPoint.X;
                    y += point.Position.Y - TmpPoint.Y;
                    OtherControl.SetValue(Canvas.LeftProperty, x);
                    OtherControl.SetValue(Canvas.TopProperty, y);
                }
            }
        }

    }
}
