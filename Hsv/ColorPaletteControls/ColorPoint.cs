﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace Hsv
{
    public class ColorPoint : DependencyObject,INotifyPropertyChanged
    {

        public event EventHandler<PropertyEventArgs> ColorChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            ColorChanged?.Invoke(this, new PropertyEventArgs(nameof(Color), oldValue, newValue));
        }


        private Color _color;

        /// <summary>
        /// 获取或设置 Color 的值
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                if (_color == value)
                    return;

                var oldValue = _color;
                _color = value;
                OnPropertyChanged("Color");
                ColorChanged?.Invoke(this, new PropertyEventArgs(nameof(Color), oldValue, _color));
            }
        }



        /// <summary>
        /// 获取或设置IsPrimary的值
        /// </summary>  
        public bool IsPrimary
        {
            get { return (bool)GetValue(IsPrimaryProperty); }
            set { SetValue(IsPrimaryProperty, value); }
        }

        /// <summary>
        /// 标识 IsPrimary 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsPrimaryProperty =
            DependencyProperty.Register("IsPrimary", typeof(bool), typeof(ColorPoint), new PropertyMetadata(false, OnIsPrimaryChanged));

        private static void OnIsPrimaryChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorPoint target = obj as ColorPoint;
            bool oldValue = (bool)args.OldValue;
            bool newValue = (bool)args.NewValue;
            if (oldValue != newValue)
                target.OnIsPrimaryChanged(oldValue, newValue);
        }

        protected virtual void OnIsPrimaryChanged(bool oldValue, bool newValue)
        {
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
