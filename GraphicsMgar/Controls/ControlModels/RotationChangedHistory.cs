using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace NaiveInkCanvas.Controls.ControlModels
{
    public class RotationChangedHistory : IOperation, ICanResolution
    {
        public float OldRotation { get; }

        public SolidColorBrush UnUsedBrush { get; }

        public SolidColorBrush UsedBrush { get; }

        public bool IsUsed { get; set; }

        public RotationChangedHistory(float rotation)
        {
            OldRotation = rotation;
            UnUsedBrush = new SolidColorBrush(Colors.Black);
            UsedBrush = new SolidColorBrush(Colors.Gray);
            IsUsed = false;
        }
        public void Revocation()
        {
        }
    }
}
