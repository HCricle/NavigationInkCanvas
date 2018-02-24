using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.MessageArgs.Helpers;
using NaiveInkCanvas.Model.NewModels;
using NaiveInkCanvas.View;
using NaiveInkCanvas.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NaiveInkCanvas.ViewModel
{
    public class ViewModelLocator
    {
        public static string FirstStartViewKey => "FirstStartV";
        public static string StartViewKey => "StartV";
        public static string CanvasViewKey => "CanvasV";
        static ViewModelLocator()
        {
            DispatcherHelper.Initialize();

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<DialogService>();

            SimpleIoc.Default.Register<StartPageViewModel>();
            SimpleIoc.Default.Register<SingleCanvasViewModel>();
            //MessageHelper.Default.InitDefaultMessager(10086);//注册消息通知
            var navSer = InitNavigationService();
            SimpleIoc.Default.Register(() => navSer);
            

        }
        private static INavigationService InitNavigationService()
        {
            var navSer = new NavigationService();
            navSer.Configure(FirstStartViewKey, typeof(FirstStartView));
            navSer.Configure(StartViewKey, typeof(StartPageView));
            navSer.Configure(CanvasViewKey, typeof(SingCanvasView));
            return navSer;
        }
        public static void ResetSingleCanvas()
        {
            SimpleIoc.Default.Unregister<SingleCanvasViewModel>();
            SimpleIoc.Default.Register<SingleCanvasViewModel>();
        }
        public SingleCanvasViewModel CanvasVM => 
            SimpleIoc.Default.GetInstance<SingleCanvasViewModel>();
        public StartPageViewModel StartPageVM =>
            SimpleIoc.Default.GetInstance<StartPageViewModel>();
    }
}
