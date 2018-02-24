using NaiveInkCanvas.CanvasPartControls.BaseControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NaiveInkCanvas.Controls
{
    public sealed partial class BackgroundBorder : UserControl,INotifyPropertyChanged
    {
        public BackgroundBorder(FrameworkElement outercontrol, bool islock = false)
        {
            this.InitializeComponent();
            OuterControl = outercontrol;
            IsLock = islock;
        }
        private bool IsLock;
        private FrameworkElement OuterControl { get; }
        private StorageFile imgFile;
        public MoverManager Mover { get; set; }
        public StorageFile ImgFile
        {
            get => imgFile;
            set
            {
                imgFile = value;
                Debug.Assert(value != null);
            }
        }
        private BitmapSource ImgSource { get; set; }
        private double imgOpacity;

        public event PropertyChangedEventHandler PropertyChanged;

        public double ImgOpacity
        {
            get => imgOpacity / 100;
            set
            {
                imgOpacity = value;
                border.Opacity = value / 100;
            }
        }       
        public FlyoutBase ImgFlyout => (FlyoutBase)border.Resources["fyMenu"];
        private void Border_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(sender as UIElement);
            if (p.Properties.IsRightButtonPressed)
            {

            }
        }
        private async Task GetImage()
        {
            using (var stream = await ImgFile.OpenAsync(FileAccessMode.Read))
            {
                ImgSource = new BitmapImage();
                await ImgSource.SetSourceAsync(stream);
                img.Source = ImgSource;
            }
        }
        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            /*var bd = new Binding()
            {
                Source = this,
                Path = new PropertyPath("ImgSource")
            };
            img.SetBinding(Image.SourceProperty, bd);
            */
            Mover = new MoverManager(this, OuterControl, ImgFlyout)
            {
                IsLock = IsLock
            };
        }
        private void RaisePropertyChanged([CallerMemberName] string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private async void btnSFile_Click(object sender, RoutedEventArgs e)
        {
            await LoadImg();
        }

        public async Task LoadImg()
        {
            var fp = new FileOpenPicker();
            fp.FileTypeFilter.Add(".jpg");
            fp.FileTypeFilter.Add(".bmp");
            fp.FileTypeFilter.Add(".jpeg");
            fp.FileTypeFilter.Add(".png");
            fp.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            var file = await fp.PickSingleFileAsync();
            if (file != null)
            {
                var ip = await file.Properties.GetImagePropertiesAsync();
                if (ip != null)
                {
                    ImgFile = file;
                    await GetImage();
                    Width = ip.Width;
                    Height = ip.Height;
                }
                else
                {
                    await new MessageDialog("错误图形文件").ShowAsync();
                }

            }
        }
    }
}
