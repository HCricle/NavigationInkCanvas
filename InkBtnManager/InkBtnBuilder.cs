using InkBtnManager.Model.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace InkBtnManager
{
    public class InkBtnBuilder
    {
        public static ObservableCollection<InkBtnPenButtonBase> ocPens { get;private set; } = new ObservableCollection<InkBtnPenButtonBase>();
        public static ObservableCollection<InkBtnToggleBase> ocToggles { get; private set; } = new ObservableCollection<InkBtnToggleBase>();
        public static ObservableCollection<InkBtnToolBase> ocTool { get;private set; } = new ObservableCollection<InkBtnToolBase>();


        private static InkBtnBuilder Instance = null;
        private InkBtnBuilder()
        {
            //new InkBtnPenButtonBase(new InkBtnPenBase(new InkDrawingAttributes() { PenTip= PenTipShape.Rectangle}, new List<Brush>() { new SolidColorBrush(Colors.Black),new SolidColorBrush(Colors.Blue)}));
            new InkBtnToggleBase();
        }

        public static InkBtnBuilder GetInstance()
        {
            return Instance == null ? (Instance = new InkBtnBuilder()) : Instance;
        }
        public void UpdataCollection(DependencyObjectCollection items)
        {
            items.Clear();
            foreach (var item in ocPens)
            {
                items.Add(item);
            }
            foreach (var item in ocToggles)
            {
                items.Add(item);
            }
            foreach (var item in ocTool)
            {
                items.Add(item);
            }
        }

    }
}
