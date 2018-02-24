using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


namespace NaiveInkCanvas.View
{
    public sealed partial class FirstStartView : Page
    {
        public FirstStartView()
        {
            InitializeComponent();
        }
        private string[] AppearText = {
            "NavieInkCanvas",
            "你可以使用我去绘画，例如添加背景去临摹。",
            "你可以把画进行分享或保存",
            "你要问我是谁？",
            "不告诉你！",            
            "不说了，开启你的绘画吧！"
        };
        private int index = 1;
        private Storyboard Board1;
        private Storyboard Board2;
        private Storyboard BoardBtn1;
        private Storyboard BoardBtn2;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnHello.Content = "Hello";
            Board1 = grid.Resources["sbStart"] as Storyboard;
            Board2 = grid.Resources["sbStartTwo"] as Storyboard;
            BoardBtn1 = grid.Resources["sbShowBtn"] as Storyboard;
            BoardBtn2 = grid.Resources["sbExitBtn"] as Storyboard;
            BoardBtn2.Completed += BoardBtn2_Completed;
            Board1.Completed += Board1_Completed;
            Board2.Completed += Board2_Completed;
            txbTitle.Text = AppearText[0];
            BoardBtn1.Begin();
        }

        private void BoardBtn2_Completed(object sender, object e)
        {
            Board1.Begin();
        }

        private void Board2_Completed(object sender, object e)
        {
            if (index > AppearText.Length - 1)
            {
                btnHello.Content = "开启";
                btnHello.IsEnabled = true;
                BoardBtn1.Begin();
            
                return;
            }
            txbTitle.Text = AppearText[index++];
            Board1.Begin();

        }

        private void Board1_Completed(object sender, object e)
        {
            Board2.Begin();
        }

        private void btnHello_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnHello.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void btnHello_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnHello.Background = new SolidColorBrush(Colors.Transparent);
        }
        private bool IsFirst = false;
        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFirst)
            {
                btnHello.IsEnabled = false;
                BoardBtn2.Begin();                
                IsFirst = true;
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values["IsFirst"] = true.ToString();
                Frame.Navigate(typeof(StartPageView));
            }
        }
    }
}
