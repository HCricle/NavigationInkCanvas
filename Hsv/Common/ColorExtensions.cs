﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.Helpers;

namespace Hsv
{
  public static class ColorExtensions
    {
        public static HsvColor ToHsvEx(this Color color)
        {
           var hsv= color.ToHsv();
            hsv.H = Math.Round(hsv.H);
            hsv.S = Math.Round(hsv.S, 2);
            hsv.V = Math.Round(hsv.V, 2);
            return hsv;
        }


        public static Color FromHsvEx(double hue, double saturation, double value, double alpha = 1)
        {
           return Microsoft.Toolkit.Uwp.Helpers.ColorHelper.FromHsv(Math.Round(hue), Math.Round(saturation, 2), Math.Round(value, 2), alpha);
        }
    }
}
