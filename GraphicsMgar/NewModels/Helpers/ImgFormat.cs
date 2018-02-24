using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace GraphicsMgar.NewModels.Helpers
{
    public class ImgFormat
    {
        public ImgFormat(Color[] formatedColors, uint pixelHeight, uint pixelWidth, double dpiX, double dpiY)
        {
            FormatedColors = formatedColors;
            PixelHeight = pixelHeight;
            PixelWidth = pixelWidth;
            DpiX = dpiX;
            DpiY = dpiY;
        }

        public Color[] FormatedColors { get; }
        /// <summary>
        /// 生成一个PixelWidth x PixelHeight的矩阵
        /// </summary>
        /// <returns></returns>
        public async Task<List<List<Color>>> To2DColors()
        {
            Debug.Assert(FormatedColors!=null);
            var res = new List<List<Color>>();
            await Task.Run(() =>
            {
                var colorList = FormatedColors.ToList();
                List<Color> list = null;
                for (int i = 0; i < FormatedColors.Length; i++)
                {
                    if (i % PixelWidth == 0) 
                    {
                        res.Add(list);
                        list = new List<Color>();
                    }
                    list.Add(colorList[i]);
                }
            });
            return res;
        }
        public uint PixelHeight { get; }
        public uint PixelWidth { get; }
        public double DpiX { get; }
        public double DpiY { get; }
    }
}
