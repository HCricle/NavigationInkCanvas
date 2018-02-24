using NaiveInkCanvas.Background.Models.Defs;
using NaiveInkCanvas.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NaiveInkCanvas.Background.Models
{
    public class ImgBackground : IImgBackground
    {
        public ImgBackground()
        {
        }
        public ImgBackground(string name,FrameworkElement outcontrol)
            :this()
        {
            Name = name;
            BorderControl = new BackgroundBorder(outcontrol, true);
        }
        public string Name { get;}
        public BackgroundBorder BorderControl { get; }
    }
}
