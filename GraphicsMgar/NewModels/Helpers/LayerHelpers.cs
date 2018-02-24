using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace GraphicsMgar.NewModels.Helpers
{
    public static class LayerHelpers
    {
        /// <summary>
        /// 取图像流的数据
        /// </summary>
        /// <param name="imgStream">图像流</param>
        /// <returns>数据</returns>
        public async static Task<ImgFormat> GetImgPixelData(IRandomAccessStream imgStream)
        {
            Debug.Assert(imgStream != null);
            var decoder = await BitmapDecoder.CreateAsync(imgStream);
            var provd = await decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                new BitmapTransform(), ExifOrientationMode.IgnoreExifOrientation, 
                ColorManagementMode.DoNotColorManage);
            var data= provd.DetachPixelData();
            return new ImgFormat(await data.ToColors(),
                decoder.PixelHeight, decoder.PixelWidth, decoder.DpiX, decoder.DpiY);
        }
        /// <summary>
        /// 将图像流的数据转换为颜色数组
        /// </summary>
        /// <param name="datas">图像流数据</param>
        /// <returns>颜色数组</returns>
        public async static Task<Color[]> ToColors(this byte[] datas)
        {            
            Color[] res=new Color[datas.Length/4];
            await Task.Run(() =>
            {
                for (int i = 0; i < datas.Length; i+=4)
                {
                    res[i % 4] = new Color()
                    {
                        B=datas[i],
                        G = datas[i+1],
                        R = datas[i+2],
                        A = datas[i+3]
                    };
                }
            });
            return res;
        }
        public static int SubColor(this Color c1,Color c2)
        {
            var res = 0;
            res += Math.Abs(c1.B - c2.B);
            res += Math.Abs(c1.G - c2.G);
            res += Math.Abs(c1.R - c2.R);
            res += Math.Abs(c1.A - c2.A);
            return res;
        }
    }
}
