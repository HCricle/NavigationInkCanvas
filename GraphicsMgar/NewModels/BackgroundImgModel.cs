using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace NaiveInkCanvas.Model.NewModels
{
    /// <summary>
    /// TODO:规则
    /// 一图一CanMoveControl
    /// </summary>
    public class BackgroundImgModel
    {
        public Canvas InstanceCanvas { get; private set; }
            = new Canvas();
        public List<Image> InstanceImages { get;private set; } 
            = new List<Image>();
        public StorageFile File { get; set; }
        /// <summary>
        /// 一般为图片名
        /// </summary>
        public string Name { get; set; }
        public BackgroundImgModel(StorageFile file)
        {
            File = file;
            var strs = file.Path.Split('.');
            Name = strs[strs.Count() - 1];
            InstanceImages.Add(new Image() { Source = new BitmapImage(new Uri(file.Path)) });
            InstanceCanvas.Children.Add(InstanceImages[0]);
            InitCanvas();
        }
        private void InitCanvas()
        {
            InstanceCanvas.AllowDrop = true;
            InstanceCanvas.PointerMoved += InstanceCanvas_PointerMoved;
        }

        private void InstanceCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
        }
    }
}
