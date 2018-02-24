using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
namespace NaiveInkCanvas.Controls
{
    public sealed partial class StrokeRectangle : UserControl
    {
        public StrokeRectangle()
        {
            this.InitializeComponent();
            RotationHistories = new Stack<double>();
        }
        public Rectangle Rectangle => SeletedRectangle;
        public event Action<StrokeRectangle, Rect> RectangleRectChanged;
        public event Action<StrokeRectangle, Point> RectangleStartPointChanged;
        public event Action<StrokeRectangle, double> RectangleDirectionChanged;
        public Stack<double> RotationHistories { get;  }
        public bool CanOperation { get; set; }
        public Rect LocalRect
        {
            get => new Rect(LocalStartPoint, LocalSize);
            set
            {
                LocalStartPoint = new Point(value.X, value.Y);
                LocalSize = new Size(value.Width, value.Height);
            }
        }

        public Point LocalStartPoint
        {
            get => new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            set
            {
                Canvas.SetLeft(this,value.X);
                Canvas.SetTop(this, value.Y);
            }
        }

        public Size LocalSize
        {
            get => new Size(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        private bool IsRectChanged;
        private void DoubleAnimation_Completed(object sender, object e)
        {
            BeginAnimal();
        }
        private void BeginAnimal()
        {
            (Resources["moveAnimal"] as Storyboard).Begin();
        }

        private void Uc_Loaded(object sender, RoutedEventArgs e)
        {
            BeginAnimal();
        }

        private void EllRectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (!CanOperation)
                return;
            ct.Rotation += e.Cumulative.Rotation;            
        }

        private void EllRectangle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (!CanOperation)
                return;
            //RotationHistories.Push(ct.Rotation);
            RectangleDirectionChanged?.Invoke(this, ct.Rotation);
        }
        private void SeletedRectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (!CanOperation)
                return;
            if (e.Delta.Scale != 1)
            {
                var x = Canvas.GetLeft(this);
                var y = Canvas.GetTop(this);
                LocalStartPoint = new Point(x + e.Delta.Translation.X, y + e.Delta.Translation.Y);
                Width += e.Delta.Translation.X;
                Height += e.Delta.Translation.Y;
                IsRectChanged = true;
            }
        }       
        private void SeletedRectangle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (!CanOperation)
                return;
            if (IsRectChanged)
                RectangleRectChanged?.Invoke(this, new Rect(new Point(Canvas.GetLeft(this), Canvas.GetTop(this)), new Size(Width, Height)));
        }

        private void SeletedRectangle_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (!CanOperation)
                return;
            IsRectChanged = false;
        }

        private void SeletedRectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(sender as UIElement);
            if (pointer.Properties.IsRightButtonPressed)
            {
                FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
                e.Handled = true;
                return;
            }
            SeletedRectangle_ManipulationStarted(sender, null);
        }

        private void SeletedRectangle_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }

        private void SeletedRectangle_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (!CanOperation)
                return;
            var p = e.GetCurrentPoint(sender as UIElement);            
            if (p.Properties.IsLeftButtonPressed)
            {
                var x =LocalSize.Width / 2;
                var y = LocalSize.Height / 2;
                LocalStartPoint = new Point(p.Position.X - x, p.Position.Y - y);
                RectangleStartPointChanged?.Invoke(this, LocalStartPoint);
            }
        }        
    }
}
