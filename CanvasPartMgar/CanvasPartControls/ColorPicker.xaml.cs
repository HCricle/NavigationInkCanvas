using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Hsv;
using System.Collections.ObjectModel;
using Windows.UI;
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NaiveInkCanvas.CanvasPartControls
{
    public sealed partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
            ColorPoints = new ObservableCollection<ColorPoint>
            {
                new ColorPoint() { Color = Colors.White }
            };
            ColorPoints[0].ColorChanged += ColorPicker_ColorChanged;
        }

        private void ColorPicker_ColorChanged(object sender, PropertyEventArgs e)
        {
            UsingColor = ColorPoints[0].Color;
        }

        public Color UsingColor
        {
            get { return (Color)GetValue(LocalColorProperty); }
            set { SetValue(LocalColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LocalColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocalColorProperty =
            DependencyProperty.Register("LocalColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.White));



        public ObservableCollection<ColorPoint> ColorPoints
        {
            get { return (ObservableCollection<ColorPoint>)GetValue(ColorPointsProperty); }
            set { SetValue(ColorPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColorPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorPointsProperty =
            DependencyProperty.Register("ColorPoints", typeof(ColorPoint), typeof(ColorPicker), new PropertyMetadata(null));
        public Color LocalColor => ColorPoints[0].Color;

    }
}
