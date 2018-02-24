using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Controls;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.CanvasPartControls.BaseControl
{
    public class MoverManager : IDisposable
    {
        private BackgroundBorder backgroundBorder;
        private FrameworkElement outerControl;
        private bool isPointPressed;
        public MoverManager(FrameworkElement inercontrol, FrameworkElement outercontrol)
        {
            InterControl = inercontrol;
            OtherControl = outercontrol;
            OtherControl.PointerPressed += PointerPressed;
            OtherControl.PointerMoved += PointerMoved;
            OtherControl.PointerReleased += PointerReleased;           
            IsLock = false;
        }


        public MoverManager(FrameworkElement inercontrol, FrameworkElement outercontrol, FlyoutBase interflyout)
            : this(inercontrol, outercontrol)
        {
            FlyoutBase.SetAttachedFlyout(inercontrol, (InterControlFlyout = interflyout));
        }

        public MoverManager(BackgroundBorder backgroundBorder, FrameworkElement outerControl)
        {
            this.backgroundBorder = backgroundBorder;
            this.outerControl = outerControl;
        }

        private Size? SubSize { get; set; }
        public FrameworkElement InterControl { get; }
        public FrameworkElement OtherControl { get; }
        public FlyoutBase InterControlFlyout { get; }
        public bool IsLock { get; set; }
        public bool IsPointPressed => isPointPressed;
        public Rect SelectedBounds => new Rect(Canvas.GetLeft(InterControl), Canvas.GetTop(InterControl), InterControl.Width, InterControl.Height);
        private void PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Handled)
                return;
            isPointPressed = true;
            var p = e.GetCurrentPoint(sender as UIElement);
            var x = Canvas.GetLeft(InterControl);
            var y = Canvas.GetTop(InterControl);
            var subx = x + InterControl.Width - p.Position.X;
            var suby = y + InterControl.Height - p.Position.Y;
            var isOn = p.Position.X > x && p.Position.X < x + InterControl.Width &&
                p.Position.Y > y && p.Position.Y < y + InterControl.Height;
            if (InterControlFlyout != null && p.Properties.IsRightButtonPressed&&isOn)
            {
                FlyoutBase.ShowAttachedFlyout(InterControl);
            }
            if (isOn)
            {
                SubSize = new Size(p.Position.X - x, p.Position.Y - y);                
            }
            else
            {
                SubSize = null;
                if (!IsLock)
                {
                    Canvas.SetLeft(InterControl, p.Position.X);
                    Canvas.SetTop(InterControl, p.Position.Y);
                    InterControl.Width = InterControl.Height = 0;
                }
            }
        }

        private void PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (e.Handled||!isPointPressed)
                return;
            var p = e.GetCurrentPoint(sender as UIElement);
            if (p.Properties.IsLeftButtonPressed)
            {
                var x = Canvas.GetLeft(InterControl);
                var y = Canvas.GetTop(InterControl);
                var setx = p.Position.X - x;
                var sety = p.Position.Y - y;
                if (SubSize.HasValue)
                {
                    Canvas.SetLeft(InterControl, p.Position.X - SubSize.Value.Width);
                    Canvas.SetTop(InterControl, p.Position.Y - SubSize.Value.Height);
                }
                else
                {
                    if (!IsLock)
                    {
                        if (setx > 0)
                        {
                            InterControl.Width = setx;
                        }
                        if (sety > 0)
                        {
                            InterControl.Height = sety;
                        }

                    }
                }
            }
        }

        private void PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isPointPressed = false;
        }
        public void Dispose()
        {
            OtherControl.PointerPressed -= PointerPressed;
            OtherControl.PointerMoved -= PointerMoved;
        }
    }
}
