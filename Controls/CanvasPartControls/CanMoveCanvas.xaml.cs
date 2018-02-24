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
using Windows.UI.Xaml.Navigation;

namespace Controls.CanvasPartControls
{
    /// <summary>
    /// 一定要把我放在canvas里
    /// </summary>
    public sealed partial class CanMoveCanvas : UserControl
    {
        public CanMoveCanvas()
        {
            this.InitializeComponent();
        }


        public bool EnablePointerAnimal
        {
            get { return (bool)GetValue(EnablePointerAnimalProperty); }
            set { SetValue(EnablePointerAnimalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnablePointerAnimal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnablePointerAnimalProperty =
            DependencyProperty.Register("EnablePointerAnimal", typeof(bool), typeof(CanMoveCanvas), new PropertyMetadata(true));


        public UIElement Title
        {
            get { return (UIElement)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(UIElement), typeof(CanMoveCanvas), new PropertyMetadata(null));


        public UIElement ControlContent
        {
            get { return (UIElement)GetValue(ControlContentProperty); }
            set { SetValue(ControlContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlContentProperty =
            DependencyProperty.Register("ControlContent", typeof(UIElement), typeof(CanMoveCanvas), new PropertyMetadata(null));



        public double TitleHeight
        {
            get { return (double)GetValue(TitleHeightProperty); }
            set { SetValue(TitleHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleHeightProperty =
            DependencyProperty.Register("TitleHeight", typeof(double), typeof(CanMoveCanvas), new PropertyMetadata(50));



        public event Action<object, PointerRoutedEventArgs> ControlPressed;
        public event Action<object, RoutedEventArgs> Exiting;
        private void CanMoveControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ControlPressed?.Invoke(sender, e);
        }

        private void CanMoveControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            double x = 0,y=0;
            if (!(sender is FrameworkElement))
                return;
            if (sender is FrameworkElement uie)
            {
                var point = e.GetCurrentPoint(btnMove);
                if (point.Properties.IsLeftButtonPressed)
                {
                     x = Canvas.GetLeft(CanMoveControl);
                     y = Canvas.GetTop(CanMoveControl);
                    var xx = point.Position.X;
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Exiting?.Invoke(sender, e);
        }

        private void CanMoveControl_Loaded(object sender, RoutedEventArgs e)
        {
            //gridTitle.PointerPressed += CanMoveControl_PointerPressed;
            //gridTitle.PointerMoved += CanMoveControl_PointerMoved;
            //gridTitle.PointerReleased += GridTitle_PointerReleased; ;
        }

        private void GridTitle_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        }
    }
}
